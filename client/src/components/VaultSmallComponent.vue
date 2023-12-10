<template>
  <div>
    <div class="vault-coverImg rounded box-shadow my-3 mx-1 d-flex flex-column justify-content-end"
      @click="goVaultPage(vaultProp.id)" role="button" :title='`Go to Vault page of "${vaultProp.name}"`'>
      <div class=" d-flex align-items-end justify-content-between my-1">
        <p class="text-light fw-bold ms-1 mb-1">{{ vaultProp.name.toUpperCase() }}</p>
        <div v-if="vaultProp.isPrivate" class="bg-light lock-bg m-1">
          <i class="mdi mdi-lock ms-1"></i>
        </div>
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
  height: 25dvh;
  aspect-ratio: 1.7/1;
}

.box-shadow {
  box-shadow: 2px 5px 5px gray;
}

.lock-bg {
  border-radius: 50%;
  width: 25px;
}
</style>