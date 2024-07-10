<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import { useMoviesStore } from '@/stores/movies'
import { MediaListDTO } from '@/app/shared/models/media-list.model'
import { RouterLink } from 'vue-router'
import { useTVShowStore } from '@/stores/tvshow'

const MoviesStore = useMoviesStore()
const TVShowStore = useTVShowStore()

const movies = ref<MediaListDTO[]>(new Array<MediaListDTO>())
const tvshow = ref<MediaListDTO[]>(new Array<MediaListDTO>())

const MediaList = computed(() => {
  return [...movies.value, ...tvshow.value]
})

onMounted(async () => {
  await MoviesStore.GetMoviesList(5, 1, '')
  await TVShowStore.GetTVShowList(5, 1, '')
  movies.value = MoviesStore.getMoviesList()
  tvshow.value = TVShowStore.getTVShowList()
})
</script>

<template>
  <section>
    <ul>
      <li v-for="m in MediaList" :key="m.Id">
        <RouterLink :to="`/movie/${m.Id}/${m.Title}`" v-if="m.Type == 'movie'">{{
          m.Title
        }}</RouterLink>
        <RouterLink :to="`/tvshow/${m.Id}/${m.Title}`" v-else>{{ m.Title }}</RouterLink>
      </li>
    </ul>
  </section>
</template>

<style scoped lang="scss">
li {
  list-style: none;
}
</style>
