<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <KeepDetailsModal />
  <CreateKeepModal />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import KeepDetailsModal from './components/modals/KeepDetailsModal.vue'
import CreateKeepModal from './components/modals/CreateKeepModal.vue'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, KeepDetailsModal, CreateKeepModal }
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
