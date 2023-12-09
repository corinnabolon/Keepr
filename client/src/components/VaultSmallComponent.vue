<template>
  <div>
    <div class="vault-coverImg rounded box-shadow my-3 mx-1 d-flex flex-column justify-content-end"
      @click="goVaultPage(vaultProp.id)" role="button" :title='`Go to Vault page of "${vaultProp.name}"`'>
      <div class=" d-flex align-items-end my-2">
        <p class="text-light fw-bold ms-1 mb-0">{{ vaultProp.name.toUpperCase() }}</p>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from "../models/Vault.js";
import { useRouter } from "vue-router";

export default {
  props: { vaultProp: { type: Vault, required: true } },

  setup(props) {
    const router = useRouter();

    return {
      router,
      vaultCoverImg: computed(() => `url(${props.vaultProp?.img})`),


      goVaultPage(vaultId) {
        router.push({ name: 'Vault', params: { vaultId: vaultId } });
      },


    }
  }
};
</script>


<style lang="scss" scoped>
.vault-coverImg {
  background-image: v-bind(vaultCoverImg);
  background-size: cover;
  background-position: center;
  height: 20dvh;
  aspect-ratio: 1.5/1;
}

.box-shadow {
  box-shadow: 2px 5px 5px gray;
}
</style>