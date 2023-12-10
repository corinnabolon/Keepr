<template>
  <div class="modal fade" id="keepDetailsModal" tabindex="-1" aria-labelledby="keepDetailsModal Label">
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
              <img :src="activeKeep.img" alt="Keep picture" :title="activeKeep.name" class="keep-image m-0 p-0 rounded">
            </div>
            <div class="col-6 d-flex flex-column justify-content-between">
              <div class="d-flex align-self-center mt-2">
                <p class="me-3"><i class="mdi mdi-eye">{{ activeKeep.views }}</i></p>
                <p><i class="mdi mdi-safe">{{ activeKeep.kept }}</i></p>
              </div>
              <div class="text-center">
                <p class="fs-4">{{ activeKeep.name }}</p>
                <p class="fs-5">{{ activeKeep.description }}</p>
              </div>
              <div class="d-flex justify-content-between mb-2"
                :class="account.id ? 'align-items-center' : 'align-self-end'">
                <form v-if="account.id && route.name == 'Home'" @submit.prevent="addToVault"
                  class="d-flex justify-content-start">
                  <select v-model="editableVault" class="form-select w-50 me-2" aria-label="Select Vault" required>
                    <option v-for="vault in myVaults" :key="vault.id" :value="vault">{{ vault.name }}
                    </option>
                  </select>
                  <button type="submit" class="btn btn-success">Save</button>
                </form>
                <div v-else-if="account.id && route.name == 'Vault'">
                  <p class="mb-0"><i class="mdi mdi-cancel"></i>Remove</p>
                </div>
                <div class="d-flex" :class="account.id ? 'align-items-stretch' : ''">
                  <img :src="activeKeep.creator.picture" alt="Keep Creator Picture" :title="`{activeKeep.creator.name}`"
                    class="user-image rounded-circle">
                  <p class="mb-0 ms-2 align-self-center">{{ activeKeep.creator.name }}</p>
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
import { computed, ref, onMounted } from 'vue';
import Pop from "../utils/Pop.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { Modal } from "bootstrap";
import { accountService } from "../services/AccountService.js";
import { useRoute } from "vue-router";

export default {

  setup() {
    let editableVault = ref({});
    const route = useRoute();

    onMounted(() => {
      let keepDetailsModalElem = document.getElementById('keepDetailsModal')
      keepDetailsModalElem.addEventListener('show.bs.modal', function (event) {
        if (AppState.account.id && AppState.myVaults.length == 0 && route.name == "Home") {
          getMyVaults()
        }
      })
    })

    async function getMyVaults() {
      try {
        await accountService.getMyVaults();
      } catch (error) {
        Pop.error(error);
      }
    }

    return {
      editableVault,
      route,
      account: computed(() => AppState.account),
      activeKeep: computed(() => AppState.activeKeep),
      myVaults: computed(() => AppState.myVaults),

      async addToVault() {
        try {
          const vault = editableVault.value
          const vaultId = vault.id
          await vaultKeepsService.addToVault(vaultId);
          Pop.success(`${AppState.activeKeep.name} added to ${vault.name}`)
        } catch (error) {
          Pop.error(error);
        }
      },

    }
  }
}
</script>


<style lang="scss" scoped>
.keep-image {
  height: 100dvh;
  width: 100%;
  object-fit: cover;
  object-position: center;
}

.user-image {
  height: 8dvh;
  aspect-ratio: 1/1;
  object-fit: cover;
}
</style>