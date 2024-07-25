<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'
import { useAuthentication } from '@/stores/admin/authentication'
import { useTVShowStore } from '@/stores/tvshow'
import { TVShowDTO } from '@/app/shared/models/tvshow.model'
import { useMoviesStore } from '@/stores/movies'
import { ChangeWatchStatusDTO } from '@/app/shared/models/change-watch-status.model'
import type { Select } from '@/app/shared/models/select.model'
import MediaInfoComponent from '@/app/shared/components/MediaInfoComponent.vue'

const authApi = useAuthentication()

const route = useRoute()

const TVShowApi = useTVShowStore()

const MoviesApi = useMoviesStore()

const isLoading = ref<boolean>(true)

const Id = Number(route.params.id)

const TVShow = ref<TVShowDTO>(new TVShowDTO())

const StatusOptions = computed(() => MoviesApi.MovieWatchStatus)

const InitialSelectedStatus = ref<Select>()

const UserListData = ref<ChangeWatchStatusDTO>(new ChangeWatchStatusDTO())

onMounted(async () => {
  await TVShowApi.GetTVShowInfo(Id)
  TVShow.value = TVShowApi.getTvShow()

  console.log(TVShow.value)

  await MoviesApi.GetMovieWatchStatus()
  await TVShowApi.CheckUserTVShowStatus(authApi.UserData.Id, TVShow.value.Id)

  if (TVShowApi.getUserWatchStatus().value > 0) {
    InitialSelectedStatus.value = TVShowApi.getUserWatchStatus()
  }

  UserListData.value.Id = TVShow.value.Id
  UserListData.value.UserId = authApi.UserData.Id

  TVShowApi.resetTVShowInfo()

  isLoading.value = false
})

const changeSelectedStatus = (selectedStatus: Select) => {
  UserListData.value.Status = selectedStatus.value
  TVShowApi.ChangeTVShowListStatus(UserListData.value)
}
</script>

<template>
  <div v-if="isLoading">Loading...</div>
  <template v-else>
    <MediaInfoComponent
      :Title="TVShow.Title"
      :Synopsis="TVShow.Description"
      :ImageData="TVShow.TVShowImageData"
      :StatusOptions="StatusOptions"
      :InitialStatus="InitialSelectedStatus"
      :Genres="TVShow.Genres"
      @selectedStatus="changeSelectedStatus"
    />
  </template>
</template>
