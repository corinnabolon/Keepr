<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <KeepDetailsModalComponent />
  <CreateKeepModal />
  <CreateVaultModal />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import KeepDetailsModalComponent from "./components/KeepDetailsModalComponent.vue"
import CreateKeepModal from "./components/CreateKeepModal.vue"
import CreateVaultModal from "./components/CreateVaultModal.vue"

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, KeepDetailsModalComponent, CreateKeepModal, CreateVaultModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
  --theme-gray: #919191;
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

.bg-theme-beige {
  background-color: var(--theme-beige);
}

.text-theme-dracula-orchid {
  color: var(--theme-dracula-orchid);
}

.text-theme-white {
  color: var(--theme-white);
}

.text-theme-gray {
  color: var(--theme-gray);
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

.masonry {
  column-count: 4;
}

.basis {
  display: inline-block;
  width: 100%;
}
</style>
