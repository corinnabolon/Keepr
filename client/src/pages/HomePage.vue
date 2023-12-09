<template>
  <div class="container">
    <section class="row">
      <div v-for="keep in keeps" :key="keep.id" class="col-3">
        <KeepSmallComponent :keepProp="keep" />
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { keepsService } from "../services/KeepsService.js";
import KeepSmallComponent from "../components/KeepSmallComponent.vue"

export default {
  setup() {
    const account = computed(() => AppState.accounts);
    const keeps = computed(() => AppState.keeps);


    onMounted(() => {
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
