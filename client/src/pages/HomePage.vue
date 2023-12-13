<template>
  <div class="container bg-theme-beige text-theme-charcoal">
    <section class="row">
      <div v-if="keeps.length" class="masonry mt-4">
        <div v-for="keep in keeps" :key="keep.id" class="col-6 col-md-3 mx-md-2 basis">
          <KeepSmallComponent :keepProp="keep" />
        </div>
      </div>
      <div v-else>
        <div class="col-12 text-center">
          <p class="fs-2">Loading... <i class="mdi mdi-loading mdi-spin"></i></p>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted, watch } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { keepsService } from "../services/KeepsService.js";
import KeepSmallComponent from "../components/KeepSmallComponent.vue";
import { vaultsService } from "../services/VaultsService.js";
import { profilesService } from "../services/ProfilesService.js";


export default {
  setup() {
    const account = computed(() => AppState.account);
    const keeps = computed(() => AppState.keeps);



    onMounted(() => {
      keepsService.clearKeepData();
      vaultsService.clearVaultData();
      profilesService.clearData();
      getKeeps();
    })

    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      } catch (error) {
        Pop.error(error);
      }
    }

    return {
      account,
      keeps

    }
  },
  components: { KeepSmallComponent }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
