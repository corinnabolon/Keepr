<template>
  <div class="modal fade" id="keepDetailsModal" tabindex="-1" aria-labelledby="keepDetailsModal Label" aria-hidden="true">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <!-- <div class="modal-header mb-0 pb-0 d-flex align-items-center">
          <p class="mb-0" :id="modalId + 'Label'">
            Title
          </p>
          <button type="button" class="btn-close mb-0" data-bs-dismiss="modal" aria-label="Close"></button>
        </div> -->
        <div class="container">
          <section v-if="activeKeep" class="row">
            <div class="col-6 mx-0 px-0">
              <img :src="activeKeep.img" alt="Keep picture" :title="activeKeep.name" class="container m-0 p-0 rounded">
            </div>
            <div class="col-6 d-flex flex-column align-items-center justify-content-between">
              <div class="d-flex mt-2">
                <p class="me-3"><i class="mdi mdi-eye">{{ activeKeep.views }}</i></p>
                <p><i class="mdi mdi-safe">{{ activeKeep.kept }}</i></p>
              </div>
              <div>
                <p class="fs-4">{{ activeKeep.name }}</p>
                <p class="fs-5">{{ activeKeep.description }}</p>
              </div>
              <div class="d-flex mb-2">
                <form @submit.prevent="addToVault" class="d-flex">
                  <select v-model="editableVault" class="form-select" aria-label="Select Vault" required>
                    <option v-for="vault in myVaults" :key="vault.id" :value="vault">{{ vault.name }}
                    </option>
                  </select>
                  <button type="submit" class="btn btn-success">Save</button>
                </form>
                <div class="d-flex align-items-center">
                  <img :src="activeKeep.creator.picture" alt="Keep Creator Picture" :title="`{activeKeep.creator.name}`"
                    class="user-image rounded-circle">
                  <p class="mb-0">{{ activeKeep.creator.name }}</p>
                </div>
              </div>
            </div>
          </section>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref } from 'vue';
import Pop from "../utils/Pop.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";


export default {

  setup() {
    let editableVault = ref({});

    return {
      editableVault,
      activeKeep: computed(() => AppState.activeKeep),
      myVaults: computed(() => AppState.myVaults),

      async addToVault() {
        try {
          const vault = editableVault.value
          const vaultId = vault.id
          await vaultKeepsService.addToVault(vaultId);
        } catch (error) {
          Pop.error(error);
        }
      },

    }
  }
}
</script>


<style lang="scss" scoped>
.user-image {
  height: 8dvh;
  aspect-ratio: 1/1;
  object-fit: cover;
}
</style>