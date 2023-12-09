<template>
  <div v-if="activeVault" class="container">
    <section class="row justify-content-center">
      <div v-for="vault in activeVaultArray" :key="vault.id" class="col-6">
        <VaultCoverComponent :vaultProp="vault" />
      </div>
    </section>
    <section class="row justify-content-center mt-3">
      <div class="col-2 text-center">
        <p class="bg-info rounded-pill mx-3">{{ vaultKeeps.length }} {{ keepPluralOrSingular }}</p>
      </div>
    </section>

    <section class="row justify-content-center">
      <div v-for="keep in vaultKeeps" :key="keep.id" class="col-6">
        <KeepSmallComponent :keepProp="keep" />
      </div>
    </section>

  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watch, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";
import VaultCoverComponent from "../components/VaultCoverComponent.vue"
import KeepSmallComponent from "../components/KeepSmallComponent.vue"

export default {
  setup() {
    const route = useRoute();
    const router = useRouter();
    const watchableVaultId = computed(() => route.params.vaultId);

    watch(watchableVaultId, () => {
      vaultsService.clearVaultData();
      getVaultById();
    }, { immediate: true })

    async function getVaultById() {
      try {
        const vaultId = route.params.vaultId;
        await vaultsService.getVaultById(vaultId);
      } catch (error) {
        Pop.error(error)
      }
    }

    return {
      activeVault: computed(() => AppState.activeVault),
      activeVaultArray: computed(() => AppState.activeVaultArray),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      keepPluralOrSingular: computed(() => {
        if (AppState.vaultKeeps.length == 1) {
          return "Keep"
        }
        return "Keeps"
      })

    }
  },
  components: { VaultCoverComponent, KeepSmallComponent }
};
</script>


<style lang="scss" scoped></style>