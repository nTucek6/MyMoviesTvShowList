<script setup lang="ts">
import {ref, onMounted, computed } from 'vue'
import { useMoviesStore } from '@/stores/movies';
import type { MoviesListDTO } from '@/app/shared/models/movies-list.model';
import { RouterLink } from 'vue-router';

const MoviesStore = useMoviesStore()

const MoviesList = ref<MoviesListDTO[]>()

onMounted(async ()=> {
 await MoviesStore.GetMoviesList(5,1,"")
  MoviesList.value = MoviesStore.GetMoviesData()
})


</script>

<template>
  <section>
    <ul>
      <li v-for="m in MoviesList" :key="m.Id">
        <RouterLink :to="`movie/${m.Id}/${m.MovieName}`">{{ m.MovieName }}</RouterLink>
      </li>
    </ul>
  
  </section>
</template>

<style scoped lang="scss">



</style>