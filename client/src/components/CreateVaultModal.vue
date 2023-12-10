<template>
  <div class="modal fade" id="createVaultModal" tabindex="-1" aria-labelledby="createVaultModal Label" aria-hidden="true">
    <div class="modal-dialog modal-md">
      <div class="modal-content">
        <div class="modal-header mb-0 pb-0 d-flex align-items-center">
          <p class="mb-0 fs-4" id="createVaultModal Label">Add your vault</p>
          <button type="button" class="btn-close mb-0" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div>
          <form class="mt-3" @submit.prevent="createVault">
            <div class="mb-4 mx-4">
              <input v-model="editableVault.name" type="text" class="form-control" id="title" placeholder="Title..."
                required maxLength="255" minLength="2">
            </div>
            <div class="mb-4 mx-4">
              <input v-model="editableVault.img" type="url" class="form-control" id="title" placeholder="Image URL..."
                required maxLength="1000">
            </div>
            <div class="mb-4 mx-4">
              <textarea v-model="editableVault.description" required type="text" class="form-control" id="description"
                placeholder="Vault description..." maxLength="1000" minLength="3"></textarea>
            </div>
            <div class="form-check text-end me-2">
              <div class="form-text">Private vaults can only be seen by you.</div>
              <div class="d-flex justify-content-end fs-5">
                <input v-model="editableVault.isPrivate" class="form-check-input me-2" type="checkbox"
                  value="editableVault.isPrivate" id="flexCheckDefault">
                <label class="form-check-label me-4" for="flexCheckDefault">
                  Make vault private?
                </label>
              </div>
            </div>
            <div class="text-end mt-3">
              <button type="submit" class="btn btn-success me-3 mb-3 w-50">Create Vault</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";
import { Modal } from "bootstrap";

export default {
  setup() {
    let editableVault = ref({})

    onMounted(() => {
      const createVaultModalElem = document.getElementById('createVaultModal')
      createVaultModalElem.addEventListener('hidden.bs.modal', function (event) {
        editableVault.value = {}
      })
    })

    return {
      editableVault,

      async createVault() {
        try {
          const vaultData = editableVault.value
          await vaultsService.createVault(vaultData)
          Pop.success("Vault created!")
          editableVault.value = {}
          Modal.getOrCreateInstance("#createVaultModal").hide()
        } catch (error) {
          Pop.error(error)
        }
      },
    }
  }
};
</script>


<style lang="scss" scoped>
input {
  outline: 0;
  border: 0;
  border-bottom: 1px solid black;
}

textarea {
  outline: 0;
  border: 0;
  border-bottom: 1px solid black;
  height: 30dvh;
}
</style>