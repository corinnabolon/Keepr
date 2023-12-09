<template>
  <div @click="setActiveKeep(keepProp.id)" class="text-center">
    <div class="keep-coverImg rounded box-shadow my-3 mx-1 d-flex flex-column justify-content-end" @click="openModal"
      role="button" :title='`See details of this keep, "${keepProp.name}"`'>
      <div class="d-flex align-items-center my-2"
        :class="[route.name == 'Home' ? 'justify-content-around' : 'justify-content-start ms-2']">
        <p class="mb-0 text-light fw-bold">{{ keepProp.name }}</p>
        <img v-if="route.name == 'Home'" @click.stop="goProfilePage(keepProp.creatorId)" :src="keepProp.creator.picture"
          class="user-image rounded-circle">
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from "../models/Keep.js";
import Pop from '../utils/Pop';
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import { Modal } from "bootstrap";
import { useRouter, useRoute } from "vue-router";

export default {
  props: { keepProp: { type: Keep, required: true } },

  setup(props) {
    const router = useRouter();
    const route = useRoute();

    return {
      router,
      route,
      keepCoverImg: computed(() => `url(${props.keepProp?.img})`),

      async setActiveKeep(keepId) {
        try {
          await keepsService.setActiveKeep(keepId);
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
  }
};
</script>


<style lang="scss" scoped>
.keep-coverImg {
  // position: relative;
  background-image: v-bind(keepCoverImg);
  background-size: cover;
  background-position: center;
  height: 30dvh;
  aspect-ratio: 1/1;
  // object-fit: cover;
}

.user-image {
  height: 8dvh;
  aspect-ratio: 1/1;
  object-fit: cover;
}

.box-shadow {
  box-shadow: 2px 5px 5px gray;
}
</style>