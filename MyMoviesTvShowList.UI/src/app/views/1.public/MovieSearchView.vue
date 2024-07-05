<script setup lang="ts">
import { computed, onBeforeMount, ref } from 'vue'
import { useMoviesStore } from '@/stores/movies'

const MoviesApi = useMoviesStore()

const Genres = computed(() => MoviesApi.Genres)

onBeforeMount(async () => {
  await MoviesApi.GetGenres()
  //Genres.value = MoviesApi.getGenres()
})
</script>

<template>
  <section>
    <ul id="genres">
      <li v-for="(genre, index) in Genres" :key="index">
        <span>{{ genre.label }}</span>
      </li>
    </ul>
  </section>
</template>

<style scoped lang="scss">
#genres {
  display: flex;
  flex-wrap: wrap;
  margin-top: 10px;
  border-bottom: 1px solid black;
  padding-bottom: 10px;
}

#genres > li {
  list-style: none;
  flex: 0 1 20%;
  padding: 15px;
  text-align: center;
  cursor: pointer;
}

#genres > li:hover {
  background-color: lightblue;
}
</style>
