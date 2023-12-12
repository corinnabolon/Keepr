<template>
  <div v-if="activeProfile" class="container bg-theme-beige text-theme-charcoal">
    <section class="row justify-content-center">
      <div class="col-7 mt-5">
        <img :src="activeProfile.coverImg" alt="Profile cover image"
          :title="`${activeProfile.name}'s Profile cover image`" class="profile-coverImg rounded">
        <img :src="activeProfile.picture" alt="Profile picture" :title="`${activeProfile.name}'s Profile picture`"
          class="profile-picture rounded-circle box-shadow">
      </div>
    </section>
    <section class="row justify-content-center">
      <div class="col-7 text-center large-margin-top">
        <p class="fs-1 mt-4 fw-bold mb-1 font-menu">{{ activeProfile.name }}</p>
        <p class="font-menu fs-5">{{ vaultsToShow.length }} Vaults | {{ profileKeeps.length }} Keeps</p>
      </div>
    </section>
    <section v-if="vaultsToShow.length > 0" id="vaults" class="row">
      <div class="d-flex align-items-center mt-5">
        <p v-if="account.id && route.params.profileId == account.id" class="fs-3 mb-0" @click="flipWantsToDeleteVaults"
          role="button"><i class="mdi mdi-dots-vertical text-end" title="Choose Vaults to Delete"></i></p>
        <p class="fs-2 fw-bold mb-0 ms-1">Vaults</p>
      </div>
      <div v-for="vault in vaultsToShow" :key="vault.id" class="col-3">
        <VaultSmallComponent :vaultProp="vault" />
      </div>
    </section>
    <section v-if="profileKeeps.length" id="keeps" class="row">
      <div class="d-flex align-items-center mt-5">
        <p v-if="account.id && route.params.profileId == account.id" class="fs-3 mb-0" @click="flipWantsToDeleteKeeps"
          role="button"><i class="mdi mdi-dots-vertical text-end" title="Choose Keeps to Delete"></i></p>
        <p class="fs-2 fw-bold mb-0 ms-1">Keeps</p>
      </div>
      <div class="masonry mt-4">

        <div v-for="keep in profileKeeps" :key="keep.id" class="col-3 mx-2 basis">
          <KeepSmallComponent :keepProp="keep" />
        </div>
      </div>
    </section>
  </div>
  <div v-else class="container">
    <section class="row justify-content-center">
      <div class="col-12 text-center">
        <p class="fs-2">Loading... <i class="mdi mdi-loading mdi-spin"></i></p>
      </div>
    </section>
  </div>
  <KeepDetailsModalComponent />
</template>


<script>
import { useRoute } from "vue-router";
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watch } from 'vue';
import Pop from "../utils/Pop.js";
import { profilesService } from "../services/ProfilesService.js";
import KeepSmallComponent from "../components/KeepSmallComponent.vue"
import VaultSmallComponent from "../components/VaultSmallComponent.vue"
import KeepDetailsModalComponent from "../components/KeepDetailsModalComponent.vue"
import { keepsService } from "../services/KeepsService.js";
import { accountService } from "../services/AccountService.js";
import { vaultsService } from "../services/VaultsService.js";

export default {
  setup() {
    const route = useRoute();
    const account = computed(() => AppState.account);
    const watchableProfileId = computed(() => route.params.profileId);

    onMounted(() => {
      keepsService.clearKeepData();
      profilesService.clearData();
      vaultsService.clearVaultData();
      setActiveProfile();
    })

    watch(watchableProfileId, () => {
      keepsService.clearKeepData();
      vaultsService.clearVaultData();
      profilesService.clearData();
      setActiveProfile();
    }, { immediate: true })

    watch(account, () => {
      getMyVaults();
    })

    async function setActiveProfile() {
      try {
        const profileId = route.params.profileId;
        await profilesService.setActiveProfile(profileId);
      } catch (error) {
        Pop.error(error);
      }
    }

    async function getMyVaults() {
      try {
        await accountService.getMyVaults();
      } catch (error) {
        Pop.error(error);
      }
    }

    return {
      account,
      route,
      activeProfile: computed(() => AppState.activeProfile),
      profileKeeps: computed(() => AppState.profileKeeps),
      vaultsToShow: computed(() => {
        if (AppState.activeProfile.id == AppState.account.id) {
          return AppState.myVaults
        } else {
          return AppState.profileVaults
        }
      }),

      flipWantsToDeleteKeeps() {
        AppState.wantsToDeleteKeeps = !AppState.wantsToDeleteKeeps;
      },

      flipWantsToDeleteVaults() {
        AppState.wantsToDeleteVaults = !AppState.wantsToDeleteVaults;
      }


    }
  },
  components: { KeepSmallComponent, VaultSmallComponent, KeepDetailsModalComponent }
};
</script>


<style lang="scss" scoped>
.profile-coverImg {
  position: relative;
  width: 100%;
  aspect-ratio: 2/1;
  object-fit: cover;
}

.profile-picture {
  position: absolute;
  height: 20dvh;
  aspect-ratio: 1/1;
  object-fit: cover;
  left: 46%;
  top: 67%;
  border: 4px solid var(--theme-white);
}

.box-shadow {
  box-shadow: 2px 3px 5px var(--theme-gray);
}


.large-margin-top {
  margin-top: 5rem;
}
</style>