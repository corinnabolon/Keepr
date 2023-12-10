<template>
  <div v-if="activeProfile" class="container">
    <section class="row justify-content-center">
      <div class="col-7 mt-5">
        <img :src="activeProfile.coverImg" alt="Profile cover image"
          :title="`${activeProfile.name}'s Profile cover image`" class="profile-coverImg">
        <img :src="activeProfile.picture" alt="Profile picture" :title="`${activeProfile.name}'s Profile picture`"
          class="profile-picture rounded-circle">
      </div>
    </section>
    <section class="row justify-content-center">
      <div class="col-7 text-center large-margin-top">
        <p class="fs-2">{{ activeProfile.name }}</p>
        <p>{{ profileVaults.length }} Vaults | {{ profileKeeps.length }} Keeps</p>
      </div>
    </section>
    <section class="row">
      <p class="fs-2 fw-bold mb-0 ms-1">Vaults</p>
      <div v-for="vault in profileVaults" :key="vault.id" class="col-3">
        <VaultSmallComponent :vaultProp="vault" />
      </div>
    </section>
    <section class="row">
      <p class="fs-2 fw-bold mt-5 mb-0 ms-1">Keeps</p>
      <div v-for="keep in profileKeeps" :key="keep.id" class="col-3">
        <KeepSmallComponent :keepProp="keep" />
      </div>
    </section>
  </div>
  <KeepDetailsModalComponent />
</template>


<script>
import { useRoute } from "vue-router";
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from "../utils/Pop.js";
import { profilesService } from "../services/ProfilesService.js";
import KeepSmallComponent from "../components/KeepSmallComponent.vue"
import VaultSmallComponent from "../components/VaultSmallComponent.vue"
import KeepDetailsModalComponent from "../components/KeepDetailsModalComponent.vue"
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js";

export default {
  setup() {
    const route = useRoute();

    onMounted(() => {
      keepsService.clearKeepData();
      profilesService.clearData();
      setActiveProfile();
    })

    async function setActiveProfile() {
      try {
        const profileId = route.params.profileId;
        await profilesService.setActiveProfile(profileId);
      } catch (error) {
        Pop.error(error);
      }
    }

    return {
      activeProfile: computed(() => AppState.activeProfile),
      profileKeeps: computed(() => AppState.profileKeeps),
      profileVaults: computed(() => AppState.profileVaults),


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
}

.large-margin-top {
  margin-top: 5rem;
}
</style>