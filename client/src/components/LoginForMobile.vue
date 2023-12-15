<template>
  <span class="navbar-text">
    <button class="btn selectable text-theme-charcoal lighten-30 text-uppercase my-2 my-lg-0" @click="login"
      v-if="!user.isAuthenticated">
      Login
    </button>
    <div v-else>
      <div v-if="!showMobileLoginMenu" @click="flipShowMobileLoginMenu" type="button" class="bg-theme-beige">
        <div v-if="account.picture || user.picture">
          <img :src="account.picture || user.picture" alt="account photo" height="40"
            class="user-image rounded-circle box-shadow" />
        </div>
      </div>
      <div v-else>
        <router-link :to="{ name: 'Account' }">
          <p @click="flipShowMobileLoginMenu" class="mb-0">
            Manage Account
          </p>
        </router-link>
        <router-link v-if="account.id" :to="{ name: 'Profile', params: { profileId: account.id } }">
          <p @click="flipShowMobileLoginMenu" class="mb-0">
            My Profile
          </p>
        </router-link>
        <div class="mb-0 text-danger" @click="logout">
          <i class="mdi mdi-logout"></i>
          logout
        </div>
      </div>

    </div>
  </span>
</template>

<script>
import { computed, ref } from 'vue';
import { AppState } from '../AppState';
import { AuthService } from '../services/AuthService';
import { useRouter } from "vue-router";

export default {
  setup() {
    const router = useRouter();
    let showMobileLoginMenu = ref(AppState.showMobileLoginMenu);

    return {
      router,
      showMobileLoginMenu,
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      },

      flipShowMobileLoginMenu() {
        showMobileLoginMenu.value = !showMobileLoginMenu.value;
      }
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

.box-shadow {
  box-shadow: 1px 2px 4px var(--theme-gray);
}
</style>
