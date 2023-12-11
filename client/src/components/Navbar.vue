<template>
  <nav class="navbar navbar-expand-lg bg-theme-beige text-theme-dracula-orchid font-menu box-shadow nav-height">
    <div class="container-fluid justify-content-between">
      <div class="d-flex">
        <a class="navbar-brand bg-theme-pink rounded ms-3 px-3 py-1" href="#">Home</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavDarkDropdown"
          aria-controls="navbarNavDarkDropdown" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div>
          <ul class="navbar-nav">
            <li v-if="account.id" class="dropdown font-menu fw-bold">
              <a class="nav-link dropdown-toggle" href="#" id="navbarDarkDropdownMenuLink" role="button"
                data-bs-toggle="dropdown" aria-expanded="false">
                Create
              </a>
              <ul class="dropdown-menu bg-theme-violet" aria-labelledby="navbarDarkDropdownMenuLink">
                <li><a class="dropdown-item menu-theme font-menu fw-bold" href="#" data-bs-toggle="modal"
                    data-bs-target="#createKeepModal">New Keep</a>
                </li>
                <li><a class="dropdown-item font-menu menu-theme fw-bold" href="#" data-bs-toggle="modal"
                    data-bs-target="#createVaultModal">New
                    Vault</a></li>
              </ul>
            </li>
          </ul>
        </div>
      </div>
      <!-- <div class="collapse navbar-collapse"> -->
      <div>
        <img src="../assets/img/Keeprlogo.png" alt="Keepr logo" title="Keepr logo" class="theme-img">
      </div>
      <Login />
    </div>
  </nav>
  <!-- <nav class="navbar navbar-expand-sm navbar-dark bg-dark px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img alt="logo" src="../assets/img/cw-logo.png" height="45" />
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link>
        </li>
      </ul>
  <div>
    <button class="btn text-light" @click="toggleTheme"><i class="mdi"
        :class="theme == 'light' ? 'mdi-weather-sunny' : 'mdi-weather-night'"></i></button>
  </div>
  <Login />
  </div>
  </nav> -->
</template>

<script>
import { onMounted, ref, computed } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import CreateKeepModal from "./CreateKeepModal.vue";
import CreateVaultModal from "./CreateVaultModal.vue";
import { AppState } from '../AppState'

import Login from './Login.vue';
export default {
  setup() {
    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      theme,
      account: computed(() => AppState.account),
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      }
    }
  },
  components: { Login, CreateKeepModal, CreateVaultModal }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-height {
  height: 15dvh;
}


.menu-theme {
  background-color: var(--theme-violet);
  border-bottom: 2px solid var(--theme-violet);
}

.menu-theme:hover {
  background-color: var(--theme-violet);
  border-bottom: 2px solid var(--theme-gray);
}

/* .menu-theme:focus {
  background-color: var(--theme-purple);
} */


/* .nav-link {
  text-transform: uppercase;
} */

.theme-img {
  height: 56px;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.box-shadow {
  box-shadow: 0px 2px 2px var(--theme-violet);
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
