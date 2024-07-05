<script setup lang="ts">
import { onMounted, ref, computed } from 'vue'
import { useUserListStore } from '@/stores/userlist'
import { useRoute } from 'vue-router'

const route = useRoute()

const username = ref(route.params.username as string)

const api = useUserListStore()

const MoviesList = computed(()=> api.MoviesList)

onMounted(async () => {
  await api.GetUserMoviesList(username.value)
})
</script>

<template>
  <table class="table">
    <thead>
      <tr>
        <th>#</th>
        <th>Movie</th>
        <th>Score</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="(movie, index) in MoviesList" :key="movie.Id">
        <td data-cell="#">{{ index+1 }}</td>
        <td data-cell="movie">{{ movie.Title }}</td>
        <td data-cell="score">{{ movie.Score }}</td>
      </tr>
    </tbody>
  </table>
</template>

<style scoped></style>
