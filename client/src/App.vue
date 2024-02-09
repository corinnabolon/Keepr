<template>
  <header class="d-none d-md-block">
    <Navbar />
  </header>
  <main>
    <router-view />
    <div class="d-md-none sticky-bottom">
      <NavbarForMobile />
    </div>
  </main>
  <KeepDetailsModalComponent />
  <CreateKeepModal />
  <CreateVaultModal />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import NavbarForMobile from './components/NavbarForMobile.vue'
import KeepDetailsModalComponent from "./components/KeepDetailsModalComponent.vue"
import CreateKeepModal from "./components/CreateKeepModal.vue"
import CreateVaultModal from "./components/CreateVaultModal.vue"

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, KeepDetailsModalComponent, CreateKeepModal, CreateVaultModal, NavbarForMobile }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
  --theme-gray: #919191;
  --theme-darkgray: #636E72;
  --theme-charcoal: #2D2D2D;
  --theme-dracula-orchid: #2D3436;
  --theme-purple: #877A8F;
  --theme-violet: #DED6E9;
  --theme-pink: #E9D8D6;
  --theme-white: #F9F6FA;
  --theme-beige: #FEF6F0;
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}

main,
body,
html {
  background-color: var(--theme-beige);
}

.bg-theme-beige {
  background-color: var(--theme-beige);
}

.bg-theme-pink {
  background-color: var(--theme-pink);
}

.bg-theme-violet {
  background-color: var(--theme-violet);
}

.bg-theme-purple {
  background-color: var(--theme-purple);
}

.bg-theme-gray {
  background-color: var(--theme-gray);
}

.text-theme-dracula-orchid {
  color: var(--theme-dracula-orchid);
}

.text-theme-white {
  color: var(--theme-white);
}

.text-theme-purple {
  color: var(--theme-purple);
}

.text-theme-gray {
  color: var(--theme-gray);
}

.text-theme-darkgray {
  color: var(--theme-darkgray);
}

.font-descriptions {
  font-family: 'Inter', sans-serif;
}

.font-titles {
  font-family: 'Marko One', serif;
}

.font-menu {
  font-family: 'Oxygen', sans-serif;
}

.font-vault {
  font-family: 'Quando', serif
}

.btn-theme-charcoal {
  background-color: var(--theme-purple);
  color: var(--theme-white);
  border: 1px solid var(--theme-violet);
  font-family: 'Oxygen', sans-serif;
}

.btn-theme-charcoal:hover {
  background-color: var(--theme-charcoal);
  color: var(--theme-white);
  border: 1px solid var(--theme-violet);
}

.btn-theme-pink {
  background-color: var(--theme-violet);
  color: var(--theme-charcoal);
  border: 1px solid var(--theme-charcoal);
  font-family: 'Oxygen', sans-serif;
}

.btn-theme-pink:hover {
  background-color: var(--theme-pink);
  color: var(--theme-charcoal);
  border: 1px solid var(--theme-charcoal);
  font-family: 'Oxygen', sans-serif;
}

.masonry {
  column-count: 4;
}

.basis {
  display: inline-block;
  width: 100%;
}

@media screen and (max-width: 768px) {
  .invisible-on-mobile {
    display: none;
  }

  .masonry {
    column-count: 2;
  }
}


@media screen and (min-width: 768px) {
  .invisible-on-desktop {
    display: none;
  }
}
</style>
