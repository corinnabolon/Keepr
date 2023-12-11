<template>
  <div v-if="activeVault" class="container">
    <section class="row justify-content-center">
      <div v-for="vault in activeVaultArray" :key="vault.id" class="col-6">
        <VaultCoverComponent :vaultProp="vault" />
      </div>
    </section>
    <section class="row justify-content-center mt-3">
      <div class="col-3"></div>
      <div class="col-4 text-center d-flex flex-column align-items-center">
        <p>{{ activeVault.description }}</p>
        <p class="bg-info rounded-pill mx-3 w-25">{{ vaultKeeps.length }} {{ keepPluralOrSingular }}</p>
      </div>
      <div class="col-2 d-flex">
        <p v-if="!wantsToEditVault" class="fs-4 mt-1" @click="flipWantsToEditVault" role="button"><i
            class="mdi mdi-dots-horizontal text-end" title="Edit Vault Information"></i></p>
        <button v-else @click="cancelEdits" class="btn btn-danger mt-2 me-3 px-3 py-0 cancel-button">Cancel
          Edits</button>
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
import { keepsService } from "../services/KeepsService.js";

export default {
  setup() {
    const route = useRoute();
    const router = useRouter();
    const watchableVaultId = computed(() => route.params.vaultId);
    const vaultKeeps = computed(() => AppState.vaultKeeps);

    watch(watchableVaultId, () => {
      keepsService.clearKeepData();
      vaultsService.clearVaultData();
      getVaultById();
    }, { immediate: true })

    async function getVaultById() {
      try {
        const vaultId = route.params.vaultId;
        await vaultsService.getVaultById(vaultId);
        await vaultsService.getKeepsInVault(vaultId);
      } catch (error) {
        Pop.error(error)
      }
    }


    return {
      vaultKeeps,
      getVaultById,
      wantsToEditVault: computed(() => AppState.wantsToEditVault),
      activeKeep: computed(() => AppState.activeKeep),
      activeVault: computed(() => AppState.activeVault),
      activeVaultArray: computed(() => AppState.activeVaultArray),
      keepPluralOrSingular: computed(() => {
        if (AppState.vaultKeeps.length == 1) {
          return "Keep"
        }
        return "Keeps"
      }),

      flipWantsToEditVault() {
        AppState.wantsToEditVault = !AppState.wantsToEditVault;
      },

      cancelEdits() {
        AppState.wantsToEditVault = false;
        this.getVaultById();
      }

    }
  },
  components: { VaultCoverComponent, KeepSmallComponent }
};
</script>


<style lang="scss" scoped></style>