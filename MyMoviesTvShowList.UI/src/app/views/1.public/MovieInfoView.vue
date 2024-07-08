<script setup lang="ts">
import { computed, onMounted, ref, watch, nextTick } from 'vue'
import { useRoute } from 'vue-router'
import { useMoviesStore } from '@/stores/movies'
import { MoviesDTO } from '@/app/shared/models/movies.model'
import { ChangeWatchStatusDTO } from '@/app/shared/models/change-watch-status.model'
import { useAuthentication } from '@/stores/admin/authentication'
import '@/assets/custom/select.css'
import MediaInfoComponent from '@/app/shared/components/MediaInfoComponent.vue'
import { Select } from '@/app/shared/models/select.model'

const authApi = useAuthentication()

const route = useRoute()

const MoviesApi = useMoviesStore()

const InitialSetupDone = ref<boolean>(false)

const Id = Number(route.params.id)

const Movie = ref<MoviesDTO>(new MoviesDTO())

const showList = ref(false)

const StatusOptions = computed(() => MoviesApi.MovieWatchStatus)

const InitialSelectedStatus = ref<Select>()

const UserListData = ref<ChangeWatchStatusDTO>(new ChangeWatchStatusDTO())

onMounted(async () => {
  await MoviesApi.GetMovieInfo(Id)
  Movie.value = MoviesApi.GetMovie()

  await MoviesApi.GetMovieWatchStatus()
  await MoviesApi.CheckUserMovieStatus(authApi.UserData.Id, Movie.value.Id)

  InitialSelectedStatus.value = MoviesApi.getUserWatchStatus()

  UserListData.value.Id = Movie.value.Id
  UserListData.value.UserId = authApi.UserData.Id

  MoviesApi.resetMovieInfo()
})

const changeSelectedStatus = (selectedStatus: Select) => {
  toggleList()
  UserListData.value.Status = selectedStatus.value
  MoviesApi.ChangeMovieListStatus(UserListData.value)
}

const toggleList = () => {
  showList.value = !showList.value
}
</script>

<template>
  <MediaInfoComponent
    :Title="Movie.MovieName"
    :Synopsis="Movie.Synopsis"
    :ImageData="Movie.MovieImageData"
    :StatusOptions="StatusOptions"
    :InitialStatus="InitialSelectedStatus"
    @selectedStatus="changeSelectedStatus"
    v-if="InitialSelectedStatus != undefined"
  />
</template>

<style scoped></style>
