<template>
  <nav class="container-fluid px-5 py-2 h-100">
    <div class="row">

      <div class="col-4 d-flex align-items-center">
        <button class="btn btn-secondary fs-5 fw-bold">Create</button>
      </div>

      <div class="col-4 d-flex justify-content-center align-items-center">
        <router-link :to="{ name: 'Home' }">
          <img class="logo" src="src/assets/img/Keepr Logo.png" alt="Keepr Co. Logo">
        </router-link>
      </div>

      <div class="col-4 d-flex justify-content-end align-items-center">
        <button class="btn" :class="theme == 'light' ? 'text-dark' : 'text-light'" @click="toggleTheme">
          <i class="mdi" :class="theme == 'light' ? 'mdi-weather-sunny' : 'mdi-weather-night'">
          </i>
        </button>

        <!-- LOGIN COMPONENT HERE -->
        <Login />
      </div>

    </div>
  </nav>
</template>

<script>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
export default {
  setup() {

    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      theme,
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}

nav {
  border-bottom: solid 3px #DED6E9;
}

.logo {
  width: 12vh;
}
</style>
