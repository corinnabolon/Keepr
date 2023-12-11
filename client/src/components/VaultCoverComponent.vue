<template>
  <div class="vault-coverImg text-center d-flex flex-column justify-content-end position-relative">
    <p v-if="wantsToEditVault" class="fs-2 text-danger align-self-end delete-icon" type="button">
      <i class="mdi mdi-delete" title="Delete this Vault" @click="destroyVault(vaultProp.id)"></i>
    </p>
    <p class="fs-4 text-light fw-bold mb-0">{{ vaultProp.name.toUpperCase() }}</p>
    <p class="text-light fw-bold mb-1">by {{ vaultProp.creator.name }}</p>
    <form v-if="wantsToEditVault" @submit.prevent="editVault" class="form-position text-light d-flex">
      <div>
        <div class="mb-3 mx-2">
          <p class="form-label px-2 py-1 w-25 text-center">Name:</p>
          <input v-model="editableVault.name" type="text" class="form-control" id="name" required maxLength="255"
            minLength="2">
        </div>
        <div class="mb-3 mx-2">
          <p class="form-label ps-4 py-1 w-50">Description:</p>
          <textarea v-model="editableVault.description" class="form-control" id="description" required maxLength="1000" />
        </div>
      </div>
      <div>
        <div class="mb-3 mx-2">
          <p class="form-label py-1 w-50">Image URL:</p>
          <input v-model="editableVault.img" type="url" class="form-control" id="img" required maxLength="1000">
        </div>
        <div class="form-check me-2">
          <div class="d-flex justify-content-end fs-5">
            <input v-model="editableVault.isPrivate" class="form-check-input me-2" type="checkbox"
              value="editableVault.isPrivate" id="flexCheckDefault">
            <p class="form-label p-1">
              Make vault private?
            </p>
          </div>
          <div class="d-flex justify-content-end">
            <button class="btn btn-success submit-button mt-3" type="submit">Save Changes</button>
          </div>
        </div>
      </div>
    </form>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { Vault } from "../models/Vault.js";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";
import { useRouter } from 'vue-router';

export default {
  props: { vaultProp: { type: Vault, required: true } },

  setup(props) {
    let editableVault = ref(AppState.activeVault)
    const router = useRouter();
    const account = computed(() => AppState.account)

    return {
      editableVault,
      router,
      account,
      wantsToEditVault: computed(() => AppState.wantsToEditVault),
      wantsToEditVault: computed(() => AppState.wantsToEditVault),
      wantsToDeleteVaults: computed(() => AppState.wantsToDeleteVaults),
      vaultCoverImg: computed(() => `url(${props.vaultProp.img})`),

      async destroyVault(vaultId) {
        try {
          let yes = await Pop.confirm("Are you sure you want to delete this Vault?")
          if (!yes) {
            return
          }
          await vaultsService.destroyVault(vaultId)
          Pop.success("Vault deleted")
          const profileId = AppState.account.id
          router.push({ name: "Profile", params: { profileId: profileId } })
        } catch (error) {
          Pop.error(error)
        }
      },

      async editVault() {
        try {
          const vaultData = editableVault.value
          await vaultsService.editVault(vaultData)
          Pop.success("Vault details edited!")
          editableVault.value = AppState.activeVault
          AppState.wantsToEditVault = false
        } catch (error) {
          Pop.error(error)
        }
      }

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
  aspect-ratio: 1.8/1;
  margin-top: 2rem;
}

.delete-icon {
  position: absolute;
  top: -2%,
}

.form-label {
  background-color: rgba(82, 69, 69, 0.12);
  backdrop-filter: blur(13px);
  font-weight: bold;
  border-radius: 25px;
}

.form-position {
  position: absolute;
  top: 10%;
  left: 0%;
}
</style>