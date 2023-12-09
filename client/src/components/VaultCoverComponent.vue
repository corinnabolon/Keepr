<template>
  <div class="vault-coverImg text-center d-flex flex-column justify-content-end">
    <p class="fs-4 text-light fw-bold mb-0">{{ vaultProp.name.toUpperCase() }}</p>
    <p class="text-light fw-bold mb-1">by {{ vaultProp.creator.name }}</p>


  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from "../models/Vault.js";

export default {
  props: { vaultProp: { type: Vault, required: true } },

  setup(props) {
    return {
      vaultCoverImg: computed(() => `url(${props.vaultProp.img})`),
    }
  }
};
</script>


<style lang="scss" scoped>
.vault-coverImg {
  background-image: v-bind(vaultCoverImg);
  background-size: cover;
  background-position: top;
  width: 100%;
  aspect-ratio: 2.5/1;
  margin-top: 2rem;
}
</style>