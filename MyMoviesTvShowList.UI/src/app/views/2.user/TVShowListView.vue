<script setup lang="ts">
import { onMounted, ref, computed } from 'vue'
import { useUserListStore } from '@/stores/userlist'
import { useRoute } from 'vue-router'

const route = useRoute()

const username = ref(route.params.username as string)

const api = useUserListStore()

const TVShowList = computed(() => api.TVShowList)

onMounted(async () => {
  await api.GetUserTVShowList(username.value)
})
</script>

<template>
  <table class="table">
    <thead>
      <tr>
        <th>#</th>
        <th>Series</th>
        <th>Score</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="(tvshow, index) in TVShowList" :key="tvshow.Id">
        <td data-cell="#">{{ index + 1 }}</td>
        <td data-cell="movie">{{ tvshow.Title }}</td>
        <td data-cell="score">{{ tvshow.Score }}</td>
      </tr>
    </tbody>
  </table>
</template>
