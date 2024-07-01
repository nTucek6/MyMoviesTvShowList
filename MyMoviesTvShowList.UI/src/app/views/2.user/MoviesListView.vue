<script setup lang="ts">
import { ref, computed } from 'vue'

const MoviesList = ref([
  {
    Id: 1,
    Title: 'Inception',
    Score: 5
  },
  {
    Id: 2,
    Title: 'Parasite',
    Score: 5
  },
  {
    Id: 3,
    Title: 'Predestination',
    Score: 5
  }
])
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
        <td data-cell="#">{{ index }}</td>
        <td data-cell="movie">{{ movie.Title }}</td>
        <td data-cell="score">{{ movie.Score }}</td>
      </tr>
    </tbody>
  </table>
</template>

<style scoped></style>
