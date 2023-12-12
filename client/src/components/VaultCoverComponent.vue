<template>
  <div class="vault-coverImg rounded text-center d-flex flex-column justify-content-end position-relative"
    title="Vault cover image">
    <p v-if="wantsToEditVault" class="fs-2 text-danger align-self-end delete-icon" type="button">
      <i class="mdi mdi-delete" title="Delete this Vault" @click="destroyVault(vaultProp.id)"></i>
    </p>
    <div class="d-flex" :class="[vaultProp.isPrivate ? 'justify-content-between' : 'justify-content-center']">
      <p v-if="vaultProp.isPrivate" class="fs-3 invisible">column</p>
      <div class="d-flex flex-column align-items-center justify-content-center my-1">
        <p class="fs-1 text-theme-white font-vault vault-title title-label fw-bold mb-0 px-3" title="Vault title">{{
          vaultProp.name.toUpperCase() }}</p>
        <p class="fs-5 text-theme-white font-vault title-label fw-bold mb-1 px-3" title="Vault creator name">by {{
          vaultProp.creator.name
        }}</p>
      </div>
      <div v-if="vaultProp.isPrivate" class="align-self-end bg-light lock-bg m-1 ms-3 pe-4" title="This vault is private">
        <i class="fs-4 ms-2 mdi mdi-lock ms-1"></i>
      </div>


    </div>
    <form v-if="wantsToEditVault" @submit.prevent="editVault" class="form-position text-theme-white d-flex">
      <div>
        <div class="mb-3 mx-2">
          <p class="form-label px-2 py-1 w-25 text-center font-descriptions">Name:</p>
          <input v-model="editableVault.name" type="text" class="form-control fs-5" id="name" required maxLength="255"
            minLength="2" title="Vault name">
        </div>
        <div class="mb-3 mx-2">
          <p class="form-label py-1 w-50 font-descriptions">Description:</p>
          <textarea v-model="editableVault.description" class="form-control fs-5" id="description" required
            maxLength="1000" title="Vault description" />
        </div>
      </div>
      <div>
        <div class="mb-3 mx-2">
          <p class="form-label py-1 w-50 font-descriptions">Image URL:</p>
          <input v-model="editableVault.img" type="url" class="form-control fs-5" id="img" required maxLength="1000"
            title="Vault cover image">
        </div>
        <div class="form-check me-2">
          <div class="d-flex justify-content-end align-items-center fs-5">
            <input v-model="editableVault.isPrivate" class="form-check-input mb-3 me-2 fs-4" type="checkbox"
              value="editableVault.isPrivate" id="flexCheckDefault" title="Checkbox for making vault private">
            <p class="form-label font-menu fs-4" title="Checkbox for making vault private">
              Make vault private?
            </p>
          </div>
          <div class="d-flex justify-content-end">
            <button class="btn btn-theme-charcoal submit-button mt-3" type="submit" title="Save changes">Save
              Changes</button>
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

.lock-bg {
  border-radius: 50%;
  width: 40px;
}

.title-label {
  background-color: #877a8f69;
  backdrop-filter: blur(13px);
  border-radius: 25px;
}
</style>