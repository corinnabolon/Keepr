<template>
  <div class="container">
    <section class="row justify-content-center">
      <div class="col-8 mt-5 position-relative">
        <p class="fs-1 text-center">Welcome, {{ account.name }}!</p>
        <img :src="account.coverImg" alt="Account cover image" title="Your Profile cover image" class="account-coverImg">
        <form v-if="account.id" @submit.prevent="editAccount" class="form-position text-light"
          :class="editingAccountInfo ? 'visible' : 'invisible'">
          <div class="mb-3 mx-4">
            <p class="form-label px-2 py-1 w-25 text-center">Name:</p>
            <input v-model="editableAccount.name" type="text" class="form-control" id="name" required maxLength="255"
              minLength="2">
          </div>
          <div class="mb-3 mx-4">
            <p class="form-label ps-4 py-1 w-50">User Picture URL:</p>
            <input v-model="editableAccount.picture" type="url" class="form-control" id="picture"
              placeholder="Picture URL..." required maxLength="1000">
          </div>
          <div class="mb-3 mx-4">
            <p class="form-label ps-4 py-1 w-50">Cover Image URL:</p>
            <input v-model="editableAccount.coverImg" type="url" class="form-control" id="coverImg"
              placeholder="Cover Image URL..." required maxLength="1000">
          </div>
          <div class="d-flex">
            <button class="btn btn-success submit-button" type="submit">Save Changes</button>
          </div>
        </form>
        <img :src="account.picture" alt="Account picture" title="Your Account picture"
          class="account-picture rounded-circle">
      </div>
      <div class="col-8 d-flex justify-content-between">
        <p></p>
        <p class="mt-3 ms-5 fs-5">{{ myVaults.length }} Vaults | {{ myKeeps.length }} Keeps</p>
        <p v-if="!editingAccountInfo" class="fs-4" @click="flipWantsToEditAccountInfo" role="button"><i
            class="mdi mdi-dots-horizontal text-end" title="Edit Account Information"
            :class="editingAccountInfo ? 'invisible' : 'visible'"></i></p>
        <button v-else @click="cancelEdits" class="btn btn-danger mt-2 me-3 px-3">Cancel Edits</button>
      </div>
    </section>
    <section class="row justify-content-center">
      <div class="col-7 text-center large-margin-top">
        <p>{{ myVaults.length }} Vaults | {{ myKeeps.length }} Keeps</p>
      </div>
    </section>
    <section class="row">
      <p class="fs-2 fw-bold mb-0 ms-1">Vaults</p>
      <div v-for="vault in myVaults" :key="vault.id" class="col-3">
        <VaultSmallComponent :vaultProp="vault" />
      </div>
    </section>
    <section class="row">
      <p class="fs-2 fw-bold mt-5 mb-0 ms-1">Keeps</p>
      <div v-for="keep in myKeeps" :key="keep.id" class="col-3">
        <KeepSmallComponent :keepProp="keep" />
      </div>
    </section>
  </div>
  <KeepDetailsModalComponent />
</template>

<script>
import { computed, onMounted, ref, watch } from 'vue';
import { AppState } from '../AppState';
import KeepSmallComponent from "../components/KeepSmallComponent.vue"
import VaultSmallComponent from "../components/VaultSmallComponent.vue"
import KeepDetailsModalComponent from "../components/KeepDetailsModalComponent.vue"
import { keepsService } from "../services/KeepsService.js";
import { accountService } from "../services/AccountService.js";
import { vaultsService } from "../services/VaultsService.js";
import { profilesService } from "../services/ProfilesService.js";
import { logger } from "../utils/Logger.js";
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
      getMyVaults();
      setActiveProfile();
    })

    watch(account, () => {
      editableAccount.value = AppState.account;
    })

    async function setActiveProfile() {
      try {
        const profileId = AppState.account.id
        await profilesService.setActiveProfile(profileId);
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
      //TODO: make sure keeps would be added on this page if you added one from Navbar--also with profile page

      flipWantsToEditAccountInfo() {
        editingAccountInfo.value = true;
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
}

.large-margin-top {
  margin-top: 5rem;
}

.form-position {
  position: absolute;
  top: 21%;
  left: 40%;
}
</style>
