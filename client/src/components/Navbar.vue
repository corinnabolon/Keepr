<template>
  <nav class="navbar navbar-expand-lg bg-theme-beige text-theme-dracula-orchid font-menu box-shadow nav-height">
    <div class="container-fluid justify-content-between">
      <div class="d-flex order-2 order-md-1">
        <a class="invisible-on-mobile navbar-brand bg-theme-pink home rounded mt-1 ms-3 px-3 py-1 fs-4" href="#">
          <p class="mb-0">Home</p>
        </a>
        <div>
          <ul class="navbar-nav">
            <li v-if="account.id" class="dropdown font-menu fw-bold fs-4">
              <a class="nav-link dropdown-toggle" href="#" id="navbarDarkDropdownMenuLink" role="button"
                data-bs-toggle="dropdown">
                Create
              </a>
              <ul class="dropdown-menu bg-theme-violet">
                <li><a class="dropdown-item menu-theme font-menu fw-bold fs-4" href="#" data-bs-toggle="modal"
                    data-bs-target="#createKeepModal">new keep</a>
                </li>
                <li><a class="dropdown-item font-menu menu-theme fw-bold fs-4" href="#" data-bs-toggle="modal"
                    data-bs-target="#createVaultModal">new
                    vault</a></li>
              </ul>
            </li>
          </ul>
        </div>
      </div>
      <div class="order-1 order-md-2">
        <router-link :to="{ name: 'Home' }">
          <img src="../assets/img/Keeprlogo.png" alt="Keepr logo" title="Keepr logo" class="theme-img">
        </router-link>
      </div>
      <div class="order-3">
        <Login />
      </div>
    </div>
  </nav>
</template>

<script>
import { onMounted, ref, computed } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import CreateKeepModal from "./CreateKeepModal.vue";
import CreateVaultModal from "./CreateVaultModal.vue";
import { AppState } from '../AppState';
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

.theme-img {
  height: 66px;
}

.home {
  height: 100%;
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

@media screen and (max-width: 768px) {
  .invisible-on-mobile {
    display: none;
  }
}
</style>
