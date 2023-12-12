<template>
  <span class="navbar-text">
    <button class="btn selectable text-theme-charcoal lighten-30 text-uppercase my-2 my-lg-0" @click="login"
      v-if="!user.isAuthenticated">
      Login
    </button>
    <div v-else>
      <div class="dropdown my-2 my-lg-0">
        <div type="button" class="bg-theme-beige border-0 no-select" data-bs-toggle="dropdown">
          <div v-if="account.picture || user.picture">
            <img :src="account.picture || user.picture" alt="account photo" height="40"
              class="user-image rounded-circle box-shadow" />
          </div>
        </div>
        <div class="dropdown-menu p-0 fs-5" aria-labelledby="authDropdown">
          <div class="list-group">
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Manage Account
              </div>
            </router-link>
            <router-link v-if="account.id" :to="{ name: 'Profile', params: { profileId: account.id } }">
              <div class="list-group-item dropdown-item list-group-item-action">
                My Profile
              </div>
            </router-link>
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </div>
    </div>
  </span>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.user-image {
  height: 10dvh;
  aspect-ratio: 1/1;
  object-fit: cover;
}

.box-shadow {
  box-shadow: 1px 2px 4px var(--theme-gray);
}
</style>
