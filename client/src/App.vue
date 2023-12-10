<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <KeepDetailsModalComponent />
  <CreateKeepModal />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import KeepDetailsModalComponent from "./components/KeepDetailsModalComponent.vue"
import CreateKeepModal from "./components/CreateKeepModal.vue"

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, KeepDetailsModalComponent, CreateKeepModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
