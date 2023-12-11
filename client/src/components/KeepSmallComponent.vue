<template>
  <div @click="setActiveKeep(keepProp.id)" class="text-center">
    <div class="keep-coverImg rounded box-shadow mb-4 position-relative" @click="openModal" role="button"
      :title='`See details of this keep, "${keepProp.name}"`'>
      <img :src="keepProp.img" class="invisible img-fluid">
      <p v-if="wantsToDeleteKeeps" @click.stop="destroyKeep(keepProp.id)" class="fs-4 text-danger delete-icon">
        <i class="mdi mdi-close-circle" title="Delete this Keep"></i>
      </p>
      <div class="d-flex align-items-center justify-content-between mt-5 ms-2">
        <p class="mb-0 text-light fw-bold">{{ keepProp.name }}</p>
        <img v-if="route.name == 'Home'" @click.stop="goProfilePage(keepProp.creatorId)" :src="keepProp.creator.picture"
          class="user-image rounded-circle me-2" :title="`Go to ${keepProp.creator.name}'s Profile`">
      </div>
    </div>

  </div>
  <KeepDetailsModalComponent />
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from "../models/Keep.js";
import Pop from '../utils/Pop';
import { keepsService } from "../services/KeepsService.js";
import { Modal } from "bootstrap";
import { useRouter, useRoute } from "vue-router";
import KeepDetailsModalComponent from "../components/KeepDetailsModalComponent.vue"

export default {
  props: { keepProp: { type: Keep, required: true } },

  setup(props) {
    const router = useRouter();
    const route = useRoute();

    return {
      router,
      route,
      wantsToDeleteKeeps: computed(() => AppState.wantsToDeleteKeeps),
      keepCoverImg: computed(() => `url(${props.keepProp?.img})`),

      async setActiveKeep(keepId) {
        try {
          await keepsService.getKeepByIdAndSetAsActive(keepId)
        } catch (error) {
          Pop.error(error)
        }
      },

      async destroyKeep(keepId) {
        try {
          let yes = await Pop.confirm("Are you sure you want to delete this Keep?")
          if (!yes) {
            return
          }
          await keepsService.destroyKeep(keepId, route)
          Pop.success("Keep deleted")
        } catch (error) {
          Pop.error(error)
        }
      },

      goProfilePage(profileId) {
        router.push({ name: 'Profile', params: { profileId: profileId } });
      },


      openModal() {
        Modal.getOrCreateInstance('#keepDetailsModal').show();
      }

    }
  },
  components: { KeepDetailsModalComponent }
};
</script>


<style lang="scss" scoped>
.keep-coverImg {
  position: relative;
  background-image: v-bind(keepCoverImg);
  background-size: cover;
  background-position: center;
  width: 100%;
  aspect-ratio: 1/1;
}

.user-image {
  height: 8dvh;
  aspect-ratio: 1/1;
  object-fit: cover;
  margin-bottom: 10px;
}

.box-shadow {
  box-shadow: 2px 5px 5px gray;
}

.delete-icon {
  position: absolute;
  top: -4%;
  left: 94%;
}

.hidden {
  display: none;
}
</style>