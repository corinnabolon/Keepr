<template>
  <div v-if="activeVault" class="container bg-theme-beige text-theme-charcoal">
    <section class="row justify-content-center">
      <div v-for="vault in activeVaultArray" :key="vault.id" class="col-6">
        <VaultCoverComponent :vaultProp="vault" />
      </div>
    </section>
    <section class="row justify-content-center">
      <div class="col-2"></div>
      <div class="col-3 text-description fs-4 text-center d-flex flex-column align-items-center mt-3">
        <p>{{ activeVault.description }}</p>
        <p class="text-dracula-orchid bg-theme-violet font-menu rounded mx-3 w-50">{{ vaultKeeps.length }} {{
          keepPluralOrSingular }}</p>
      </div>
      <div class="col-2">
        <p class="fs-4 mt-1 ms-5 text-center" :class="[wantsToEditVault ? 'hidden' : '']" @click="flipWantsToEditVault"
          role="button"><i class="mdi mdi-dots-horizontal text-end" title="Edit Vault Information"></i></p>
        <button @click="cancelEdits" class="btn btn-theme-pink mt-2 py-0 cancel-button"
          :class="[wantsToEditVault ? '' : 'hidden']">Cancel
          Edits</button>
      </div>
    </section>
    <section class="row justify-content-center">
      <div class="masonry mt-4">
        <div v-for="keep in vaultKeeps" :key="keep.id" class="col-3 mx-2 basis">
          <KeepSmallComponent :keepProp="keep" />
        </div>
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
        if (error.response.data.includes("Something")) {
          router.push({ name: "Home" })
        }
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


<style lang="scss" scoped>
.cancel-button {
  height: 2.5rem;
}

.hidden {
  display: none;
}
</style>