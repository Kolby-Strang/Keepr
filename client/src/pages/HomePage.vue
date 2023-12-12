<template>
  <div class="container mt-4">

    <div class="masonry-with-columns">
      <div v-for="keep in keeps" :key="keep.id" class="mb-4">
        <KeepCard :keep="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService'
import { AppState } from "../AppState"
import KeepCard from '../components/KeepCard.vue';


export default {
  setup() {
    // VARIABLES
    const keeps = computed(() => AppState.allKeeps);
    // FUNCTIONS
    async function getAllKeepsIntoAppState() {
      try {
        keepsService.getAllKeepsIntoAppState();
      }
      catch (error) {
        Pop.error(error);
      }
    }
    // LIFECYCLE
    onMounted(() => {
      getAllKeepsIntoAppState();
    });
    return {
      keeps
    };
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss"></style>
