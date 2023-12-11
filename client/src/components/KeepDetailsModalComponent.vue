<template>
  <div class="modal fade bg-theme-beige" id="keepDetailsModal" tabindex="-1" aria-labelledby="keepDetailsModal Label">
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
              <div class="d-flex align-self-center mt-2 text-theme-gray">
                <p class="me-3"><i class="mdi mdi-eye">{{ activeKeep.views }}</i></p>
                <p><i class="mdi mdi-safe">{{ activeKeep.kept }}</i></p>
              </div>
              <div class="text-center">
                <p class="fs-4 text-theme-dracula-orchid font-titles">{{ activeKeep.name }}</p>
                <p class="fs-5 text-theme-gray font-descriptions">{{ activeKeep.description }}</p>
              </div>
              <div class="d-flex justify-content-between mb-2 align-items-center">
                <div v-if="account.id && route.name == 'Vault' && activeVault.creatorId == account.id" role="button"
                  @click="removeFromVault(activeKeep.id)">
                  <p class="mb-0 ms-2"><i class="mdi mdi-cancel"></i>Remove</p>
                </div>
                <!-- <select v-model="editableVault" class="form-select w-50 me-2 font-menu vault-menu"
                    aria-label="Select Vault" required>
                    <option v-for=" vault  in  myVaults " :key="vault.id" :value="vault">
                      <p class="font-menu vault-menu">{{ vault.name }}</p>
                    </option>
                  </select> -->

                <ul v-else-if="account.id" class="navbar-nav">
                  <li v-if="account.id" class="dropdown font-menu fw-bold">
                    <a class="nav-link dropdown-toggle font-menu menu-theme fw-bold vault-menu" href="#"
                      id="navbarDarkDropdownMenuLink" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                      {{ myVaults[0].name }}
                    </a>
                    <ul class="dropdown-menu bg-theme-violet" aria-labelledby="navbarDarkDropdownMenuLink">
                      <li><a class="dropdown-item font-menu menu-theme fw-bold vault-menu" type="submit"
                          v-for=" vault  in  myVaults " :key="vault.id" :value="vault"
                          @click="addToVault(vault.id, vault.name)">
                          <p @click="`addToVault(${vault.id})`">{{
                            vault.name }}</p>
                        </a>
                      </li>
                    </ul>
                  </li>
                </ul>
                <div v-else-if="!account.id">
                  <p class="mb-0 text-theme-dracula-orchid font-description"><span class="text-info" role="button"
                      @click="login">Log in</span> to collect keeps!</p>
                </div>
                <div @click.stop="goProfilePage(activeKeep.creatorId)" class="d-flex align-items-stretch"
                  :role="route.name == 'Profile' ? '' : 'button'">
                  <img :src="activeKeep.creator.picture" alt="Keep Creator Picture" :title="`${activeKeep.creator.name}`"
                    class="user-image rounded-circle">
                  <p class="mb-0 ms-2 align-self-center text-theme-dracula-orchid font-description">{{
                    activeKeep.creator.name }}</p>
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
import { useRoute, useRouter } from "vue-router";
import { AuthService } from '../services/AuthService'
import { logger } from "../utils/Logger.js";

export default {

  setup() {
    let editableVault = ref({});
    const route = useRoute();
    const router = useRouter();

    onMounted(() => {
      let keepDetailsModalElem = document.getElementById('keepDetailsModal')
      keepDetailsModalElem.addEventListener('show.bs.modal', function (event) {
        if (AppState.account.id && AppState.myVaults.length == 0) {
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
      activeVault: computed(() => AppState.activeVault),
      myVaults: computed(() => AppState.myVaults),

      async addToVault(vaultId, vaultName) {
        try {
          // const vault = editableVault.value
          // const vaultId = vault.id
          const routename = route.name
          await vaultKeepsService.addToVault(vaultId, routename);
          Pop.success(`${AppState.activeKeep.name} added to ${vaultName}`)
        } catch (error) {
          // Pop.error(error);
          logger.log(error)
        }
      },

      async removeFromVault(keepId) {
        try {
          await vaultKeepsService.removeFromVault(keepId);
          Pop.success(`${AppState.activeKeep.name} removed from ${AppState.activeVault.name}`)
          Modal.getOrCreateInstance("#keepDetailsModal").hide()
        } catch (error) {
          Pop.error(error);
        }
      },

      async login() {
        AuthService.loginWithPopup()
      },

      goProfilePage(profileId) {
        router.push({ name: 'Profile', params: { profileId: profileId } });
        Modal.getOrCreateInstance("#keepDetailsModal").hide();
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

.vault-menu {
  text-transform: uppercase;
  letter-spacing: 2px;
}

.user-image {
  height: 8dvh;
  aspect-ratio: 1/1;
  object-fit: cover;
}
</style>