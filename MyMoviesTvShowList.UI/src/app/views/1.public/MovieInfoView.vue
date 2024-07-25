<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'
import { useMoviesStore } from '@/stores/movies'
import { MoviesDTO } from '@/app/shared/models/movies.model'
import { ChangeWatchStatusDTO } from '@/app/shared/models/change-watch-status.model'
import { useAuthentication } from '@/stores/admin/authentication'
import MediaInfoComponent from '@/app/shared/components/MediaInfoComponent.vue'
import { Select } from '@/app/shared/models/select.model'

const authApi = useAuthentication()

const route = useRoute()

const MoviesApi = useMoviesStore()

const isLoading = ref<boolean>(true)

const Id = Number(route.params.id)

const Movie = ref<MoviesDTO>(new MoviesDTO())

const StatusOptions = computed(() => MoviesApi.MovieWatchStatus)

const InitialSelectedStatus = ref<Select>()

const UserListData = ref<ChangeWatchStatusDTO>(new ChangeWatchStatusDTO())

onMounted(async () => {
  await MoviesApi.GetMovieInfo(Id)
  Movie.value = MoviesApi.GetMovie()

  await MoviesApi.GetMovieWatchStatus()
  await MoviesApi.CheckUserMovieStatus(authApi.UserData.Id, Movie.value.Id)

  if (MoviesApi.getUserWatchStatus().value > 0) {
    InitialSelectedStatus.value = MoviesApi.getUserWatchStatus()
  }

  UserListData.value.Id = Movie.value.Id
  UserListData.value.UserId = authApi.UserData.Id

  MoviesApi.resetMovieInfo()

  isLoading.value = false
})

const changeSelectedStatus = (selectedStatus: Select) => {
  UserListData.value.Status = selectedStatus.value
  MoviesApi.ChangeMovieListStatus(UserListData.value)
}
</script>

<template>
  <div v-if="isLoading">Loading...</div>
  <template v-else>
    <MediaInfoComponent
      :Title="Movie.MovieName"
      :Synopsis="Movie.Synopsis"
      :ImageData="Movie.MovieImageData"
      :StatusOptions="StatusOptions"
      :InitialStatus="InitialSelectedStatus"
      :Genres="Movie.Genres"
      @selectedStatus="changeSelectedStatus"
    />
  </template>
</template>
