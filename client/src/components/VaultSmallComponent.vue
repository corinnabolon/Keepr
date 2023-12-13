<template>
  <div>
    <div class="vault-coverImg rounded box-shadow my-3 mx-1 d-flex flex-column justify-content-end position-relative"
      @click="goVaultPage(vaultProp.id)" role="button" :title='`Go to Vault page of "${vaultProp.name}"`'>
      <p v-if="wantsToDeleteVaults" @click.stop="destroyVault(vaultProp.id)"
        class="fs-4 text-danger align-self-end delete-icon">
        <i class="mdi mdi-close-circle" title="Delete this Vault"></i>
      </p>
      <div class="d-flex align-items-end justify-content-between my-1">
        <p class="text-theme-white title-label font-vault vault-title fw-bold ms-1 mb-1 px-2">{{
          vaultProp.name.toUpperCase()
        }}</p>
        <div v-if="vaultProp.isPrivate" class="bg-light lock-bg m-1">
          <i class="mdi mdi-lock ms-1"></i>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed } from 'vue';
import { Vault } from "../models/Vault.js";
import { useRouter } from "vue-router";
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";

export default {
  props: { vaultProp: { type: Vault, required: true } },

  setup(props) {
    const router = useRouter();

    return {
      router,
      wantsToDeleteVaults: computed(() => AppState.wantsToDeleteVaults),
      vaultCoverImg: computed(() => `url(${props.vaultProp?.img})`),


      goVaultPage(vaultId) {
        router.push({ name: 'Vault', params: { vaultId: vaultId } });
      },

      async destroyVault(vaultId) {
        try {
          let yes = await Pop.confirm("Are you sure you want to delete this Vault?")
          if (!yes) {
            return
          }
          await vaultsService.destroyVault(vaultId)
          Pop.success("Vault deleted")
        } catch (error) {
          Pop.error(error)
        }
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
  height: 32dvh;
  aspect-ratio: 1.7/1;
}

.box-shadow {
  box-shadow: 2px 5px 5px gray;
}

.lock-bg {
  border-radius: 50%;
  width: 25px;
}

.delete-icon {
  position: absolute;
  top: -10%;
  left: 95%;
}

.vault-title {
  text-transform: uppercase;
  letter-spacing: 2px;
  font-size: 22px;
}

.title-label {
  background-color: #877a8f69;
  backdrop-filter: blur(13px);
  border-radius: 25px;
}



@media screen and (max-width: 768px) {
  .vault-coverImg {
    height: 14dvh;
  }

  .vault-title {
    letter-spacing: 1px;
    font-size: 14px;
  }
}


@media screen and (min-width: 1800px) {
  .vault-coverImg {
    background-image: v-bind(vaultCoverImg);
    background-size: cover;
    background-position: center;
    height: 20dvh;
    aspect-ratio: 1.7/1;
  }

}
</style>