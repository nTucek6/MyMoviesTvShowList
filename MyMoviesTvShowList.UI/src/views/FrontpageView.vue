<script setup lang="ts">
import MoviesSlider from '@/components/Frontpage/MoviesSlider.vue';
import {ref, onMounted, computed } from 'vue'
import { GetApiRequest } from '@/stores/Frontpage/APICalls';

const data = GetApiRequest()

const message = computed(() => {
  return data.message;
});
onMounted(() => {
  data.fetchMessage();
});
</script>

<template>
  <main>
   <h1>Hello</h1>
   {{ message }}
   <MoviesSlider />
  </main>
</template>
