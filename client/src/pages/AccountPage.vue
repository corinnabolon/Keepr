<template>
  <div v-if="account.id" class="container bg-theme-beige text-theme-charcoal font-menu fs-5">
    <section class="row justify-content-center large-margin-bottom">
      <div class="col-12 col-lg-9 mt-5 position-relative">
        <p class="fs-1 text-center text-theme-dracula-orchid font-menu">Welcome, {{ account.name }}!</p>
        <img v-if="account.coverImg" :src="account.coverImg" alt="Account cover image" title="Your Profile cover image"
          class="account-coverImg rounded">
        <form v-if="account.id" @submit.prevent="editAccount" class="form-position text-theme-white"
          :class="editingAccountInfo ? 'visible' : 'invisible'">
          <div class="mb-3 mx-4">
            <p class="form-label ps-1 px-md-2 py-1 w-25 text-md-center">Name:</p>
            <input v-model="editableAccount.name" type="text" class="form-control fs-5" id="name" required maxLength="255"
              minLength="2" title="Your name">
          </div>
          <div class="mb-3 mx-4">
            <p class="form-label ps-1 ps-md-4 py-1 w-75 w-md-50">User Picture URL:</p>
            <input v-model="editableAccount.picture" type="url" class="form-control fs-5" id="picture" required
              maxLength="1000" title="Your picture URL">
          </div>
          <div class="mb-3 mx-4">
            <p class="form-label ps-1 ps-md-4 py-1 w-75 w-md-50">Cover Image URL:</p>
            <input v-model="editableAccount.coverImg" type="url" class="form-control fs-5" id="coverImg" maxLength="1000"
              required title="Your cover image URL">
          </div>
          <div class="d-flex mb-3">
            <button class="btn btn-theme-charcoal submit-button" type="submit">Save Changes</button>
          </div>
        </form>
        <img :src="account.picture" alt="Account picture" title="Your Account picture"
          class="account-picture rounded-circle box-shadow">
      </div>
      <div class="col-12 col-md-8">
        <section class="row">
          <div class="col-4 mb-5"></div>
          <div v-if="account.id" class="col-7 col-md-5 mt-1 mt-md-3">
            <p class="ms-2 ms-md-5 fs-5 mb-5 text-theme-dracula-orchid font-descriptions">
              <router-link v-if="myVaults && myVaults.length"
                :to="{ name: 'Profile', params: { profileId: account.id }, hash: '#vaults' }">
                {{ myVaults.length }} Vaults
              </router-link>
              <span v-else>0 Vaults</span>
              |
              <router-link v-if="myKeeps && myKeeps.length"
                :to="{ name: 'Profile', params: { profileId: account.id }, hash: '#keeps' }">
                {{ myKeeps.length }} Keeps
              </router-link>
              <span v-else>0 Keeps</span>
            </p>
          </div>
          <div class="col-1 col-md-3 d-flex justify-content-end position-relative">
            <p v-if="!editingAccountInfo" class="fs-4 mt-1 mt-md-2" @click="flipWantsToEditAccountInfo" role="button"><i
                class="mdi mdi-dots-horizontal text-end" title="Edit Account Information"></i></p>
            <button v-else @click="cancelEdits"
              class="btn btn-theme-pink mt-2 me-3 px-3 py-0 cancel-button mobile-cancel-button"
              :class="[account.coverImg ? '' : 'invisible']">Cancel
              Edits</button>
          </div>
        </section>
      </div>
    </section>
  </div>
  <div v-else class="container">
    <section class="row justify-content-center">
      <div class="col-12 text-center">
        <p class="fs-2">Loading... <i class="mdi mdi-loading mdi-spin"></i></p>
      </div>
    </section>
  </div>
  <KeepDetailsModalComponent />
</template>

<script>
import { computed, onMounted, ref, watch } from 'vue';
import { AppState } from '../AppState';
import KeepSmallComponent from "../components/KeepSmallComponent.vue";
import VaultSmallComponent from "../components/VaultSmallComponent.vue";
import KeepDetailsModalComponent from "../components/KeepDetailsModalComponent.vue";
import { keepsService } from "../services/KeepsService.js";
import { accountService } from "../services/AccountService.js";
import { vaultsService } from "../services/VaultsService.js";
import { profilesService } from "../services/ProfilesService.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    let account = computed(() => AppState.account)
    let editingAccountInfo = ref(false)
    let editableAccount = ref(AppState.account)

    onMounted(() => {
      keepsService.clearKeepData();
      profilesService.clearData();
      vaultsService.clearVaultData();
      setActiveProfile();
    })

    watch(account, () => {
      editableAccount.value = AppState.account;
      getMyVaults();
      setActiveProfile();
    })

    async function setActiveProfile() {
      try {
        const profileId = AppState.account.id
        await profilesService.setActiveProfile(profileId);
        if (AppState.account.coverImg == null) {
          accountService.getAccount()
        }
      } catch (error) {
        Pop.error(error);
      }
    }

    async function getMyVaults() {
      try {
        await accountService.getMyVaults();
      } catch (error) {
        Pop.error(error);
      }
    }

    return {
      account,
      editingAccountInfo,
      editableAccount,
      myVaults: computed(() => AppState.myVaults),
      myKeeps: computed(() => AppState.profileKeeps),

      flipWantsToEditAccountInfo() {
        editingAccountInfo.value = true;
      },

      flipWantsToDeleteVaults() {
        AppState.wantsToDeleteVaults = !AppState.wantsToDeleteVaults;
      },

      async cancelEdits() {
        try {
          editingAccountInfo.value = false;
          await accountService.getAccount();
          editableAccount.value = AppState.account;
        } catch (error) {
          Pop.error(error)
        }
      },


      async editAccount() {
        try {
          const accountData = editableAccount.value
          await accountService.editAccount(accountData)
          Pop.success("Account details edited!")
          editableAccount.value = AppState.account
          editingAccountInfo.value = false
        } catch (error) {
          Pop.error(error)
        }
      },

      flipWantsToDeleteKeeps() {
        AppState.wantsToDeleteKeeps = !AppState.wantsToDeleteKeeps;
      }


    }
  },
  components: { KeepSmallComponent, VaultSmallComponent, KeepDetailsModalComponent }
}
</script>

<style scoped>
input {
  width: 400px;
}

.form-label {
  background-color: rgba(82, 69, 69, 0.12);
  backdrop-filter: blur(13px);
  font-weight: bold;
  border-radius: 25px;
}

.submit-button {
  margin-left: 67%;
  margin-right: 2%;
}

.cancel-button {
  height: 2.5rem;
}

.mobile-cancel-button {
  position: absolute;
  top: 50%;
}


.account-coverImg {
  position: relative;
  width: 100%;
  aspect-ratio: 2.1/1;
  object-fit: cover;
}

.account-picture {
  position: absolute;
  height: 30dvh;
  aspect-ratio: 1/1;
  object-fit: cover;
  left: 5%;
  top: 83%;
  border: 4px solid var(--theme-white);
}

.box-shadow {
  box-shadow: 2px 3px 5px var(--theme-gray);
}

.large-margin-top {
  margin-top: 5rem;
}


.large-margin-bottom {
  margin-bottom: 10rem;
}

.form-position {
  position: absolute;
  top: 21%;
  left: 40%;
}

@media screen and (max-width: 768px) {
  .account-picture {
    height: 16dvh;
    left: 6%;
    top: 78%;
  }

  .large-margin-top {
    margin-top: 3rem;
  }

  .large-margin-bottom {
    margin-bottom: 2rem;
  }

  .form-position {
    top: 350px;
    left: 0px;
  }

  input {
    width: 320px;
  }

  .form-label {
    color: var(--theme-dracula-orchid);
  }

  .submit-button {
    margin-left: 37%;
    height: 4rem;
  }

  .cancel-button {
    height: 4rem;
  }
}


@media screen and (min-width: 1800px) {
  .account-coverImg {
    aspect-ratio: 1.9/1;
  }

  .account-picture {
    height: 24dvh;
    top: 78%;
  }

}
</style>
