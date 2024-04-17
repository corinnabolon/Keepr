<template>
  <div
    class="navbar navbar-expand-lg container-fluid justify-content-between align-items-center bg-theme-beige text-theme-dracula-orchid font-menu box-shadow nav-height">
    <section class="row">
      <div class="col-2 me-2">
        <router-link :to="{ name: 'Home' }">
          <img src="../assets/img/Keeprlogo.png" alt="Keepr logo" title="Keepr logo" class="theme-img ms-3 mt-2">
        </router-link>
      </div>
      <div class="col-6 mt-2 ms-5 d-flex flex-column justify-content-center">
        <!-- <ul class="navbar-nav">
          <li v-if="account.id" class="font-menu fw-bold fs-4"> -->
        <div v-if="account.id && !showingLinks" @click="flipShowingLinks" role="button">
          <p class="px-3 font-menu fw-bold fs-5 mb-0">Create</p>
        </div>
        <div v-if="account.id && showingLinks">
          <p @click="flipShowingLinks" class="px-3 menu-theme font-menu fw-bold" data-bs-toggle="modal"
            data-bs-target="#createKeepModal">new
            keep
          </p>
          <p @click="flipShowingLinks" class="px-3 menu-theme font-menu fw-bold" data-bs-toggle="modal"
            data-bs-target="#createVaultModal">new
            vault</p>

        </div>

        <!-- <a class="nav-link dropdown-toggle" href="#collapseExample" role="button" data-toggle="collapse"
              data-target="#collapseExample">
              Create
            </a> -->
        <!-- <ul class="dropdown-menu bg-theme-violet">
              <li><a class="dropdown-item menu-theme font-menu fw-bold fs-4" href="#" data-bs-toggle="modal"
                  data-bs-target="#createKeepModal">new keep</a>
              </li>
              <li><a class="dropdown-item font-menu menu-theme fw-bold fs-4" href="#" data-bs-toggle="modal"
                  data-bs-target="#createVaultModal">new
                  vault</a></li>
            </ul> -->
      </div>
    </section>
    <div class="col-3">
      <LoginForMobile />
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref } from 'vue';
import CreateKeepModal from "./CreateKeepModal.vue";
import CreateVaultModal from "./CreateVaultModal.vue";
import LoginForMobile from './LoginForMobile.vue';
import { logger } from "../utils/Logger.js";


export default {
  setup() {
    let showingLinks = ref(AppState.mobileShowingLinks);

    return {
      showingLinks,
      account: computed(() => AppState.account),

      flipShowingLinks() {
        showingLinks.value = !showingLinks.value;
        logger.log(showingLinks);
      }

    }
  },
  components: { LoginForMobile, CreateKeepModal, CreateVaultModal }
}
</script>


<style lang="scss" scoped>
.menu-theme {
  background-color: var(--theme-violet);
  border-bottom: 2px solid var(--theme-violet);
}

.menu-theme:hover {
  background-color: var(--theme-violet);
  border-bottom: 2px solid var(--theme-gray);
}

.theme-img {
  height: 66px;
}

.home {
  height: 100%;
}

.nav-height {
  height: 17dvh;
  width: 100%;
}

.menu-theme {
  background-color: var(--theme-violet);
  border-bottom: 2px solid var(--theme-violet);
}
</style>