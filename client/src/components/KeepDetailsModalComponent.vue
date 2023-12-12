<template>
  <div class="modal fade bg-theme-beige" id="keepDetailsModal" tabindex="-1" aria-labelledby="keepDetailsModal Label">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="container">
          <section v-if="activeKeep" class="row">
            <div class="col-12 col-md-6 mx-0 px-0">
              <img :src="activeKeep.img" alt="Keep picture" :title="activeKeep.name"
                class="keep-image rounded-start m-0 p-0">
            </div>
            <div class="col-12 col-md-6 d-flex flex-column justify-content-between">
              <div class="d-flex align-self-center mt-2 text-theme-gray fs-5">
                <p class="me-3" :title="`${activeKeep.views} views`"><i class="mdi mdi-eye">{{ activeKeep.views }}</i></p>
                <p :title="[activeKeep.kept == 1 ? `kept ${activeKeep.kept} time` : `kept ${activeKeep.kept} times`]"><i
                    class="mdi mdi-safe">{{ activeKeep.kept }}</i></p>
              </div>
              <div class="text-center">
                <p class="fs-2 text-theme-dracula-orchid font-titles" title="Name of this keep">{{ activeKeep.name }}</p>
                <p class="fs-5 ms-3 text-start text-theme-gray font-descriptions" title="Description of this keep">{{
                  activeKeep.description }}</p>
              </div>
              <div class="d-flex justify-content-between mb-2 align-items-center font-menu fs-5">
                <div v-if="account.id && route.name == 'Vault' && activeVault.creatorId == account.id" role="button"
                  @click="removeFromVault(activeKeep.id)">
                  <p class="mb-0 ms-2 text-theme-purple onhover-underline"><i class="mdi mdi-cancel"></i>Remove</p>
                </div>
                <!-- <select v-model="editableVault" class="form-select w-50 me-2 font-menu vault-menu"
                    aria-label="Select Vault" required>
                    <option v-for=" vault  in  myVaults " :key="vault.id" :value="vault">
                      <p class="font-menu vault-menu">{{ vault.name }}</p>
                    </option>
                  </select> -->
                <div v-else-if="account.id && myVaults.length" class="d-flex align-items-center">

                  <ul class="navbar-nav" title="Select from your vaults">
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
                  <p @click="addToVault(myVaults[0].id, myVaults[0].name)"
                    class="text-theme-white bg-theme-purple fw-bold rounded font-menu ms-3 me-2 py-1 px-3 fs-md-4 mb-0 invisible-on-mobile"
                    title="Select from the menu to the left">save</p>
                </div>
                <div v-else-if="account.id && myVaults.length <= 0">
                  <p class="mb-0">You don't have any vaults</p>
                  <p>to save this Keep in</p>
                  <p class="text-theme-dracula-orchid font-description" data-bs-toggle="modal"
                    data-bs-target="#createVaultModal" role="button">Create a vault!</p>
                </div>
                <div v-else-if="!account.id">
                  <p class="mb-0 text-theme-dracula-orchid font-description"><span class="text-info" role="button"
                      @click="login">Log in</span> to collect keeps!</p>
                </div>
                <div @click.stop="goProfilePage(activeKeep.creatorId)" class="d-flex align-items-stretch ell-container"
                  :role="route.name == 'Profile' ? '' : 'button'">
                  <img :src="activeKeep.creator.picture" alt="Keep Creator Picture"
                    :title="`Keep Creator: ${activeKeep.creator.name}`" class="user-image rounded-circle">
                  <p class="text-overflow-ellipsis  mb-0 ms-2 fs-5 fw-bold align-self-center text-theme-dracula-orchid font-menu"
                    :title="`Keep Creator: ${activeKeep.creator.name}`">{{
                      activeKeep.creator.name }}</p>
                </div>
              </div>
            </div>
          </section>
          <section v-else class="row justify-content-center">
            <div class="col-12 text-center">
              <p class="fs-2">Loading... <i class="mdi mdi-loading mdi-spin"></i></p>
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
        if (AppState.account.id) {
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
  border-radius: 4px, 0px, 0px, 4px;
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

.text-theme-purple:hover {
  border-bottom: 1px solid var(--theme-purple);
}

.text-overflow-ellipsis {
  text-overflow: ellipsis;
  overflow: hidden;
}

.ell-container {
  white-space: nowrap;
  overflow: hidden;
}

.modal-xl {
  --bs-modal-width: 1200px !important;
}


//for screen sizes larger than the specified max-width--so the below is for very large screens
// @media screen and (min-width: 1800px) {


// }
</style>