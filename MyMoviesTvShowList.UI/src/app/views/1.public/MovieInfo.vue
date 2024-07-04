<script setup lang="ts">
import { computed, onMounted, onUnmounted, ref, watch } from 'vue'
import { useRoute } from 'vue-router'
import { useMoviesStore } from '@/stores/movies'
import { MoviesDTO } from '@/app/shared/models/movies.model'
import { ChangeWatchStatusDTO } from '@/app/shared/models/change-watch-status.model'
import { useAuthentication } from '@/stores/admin/authentication'
import '@/assets/custom/select.css'

const authApi = useAuthentication()

const route = useRoute()

const MoviesApi = useMoviesStore()

const Id = Number(route.params.id)

const Movie = ref<MoviesDTO>(new MoviesDTO())

const showList = ref(false)

const statusDropdown = ref()

const selectedStatus = ref()

const UserListData = ref<ChangeWatchStatusDTO>(new ChangeWatchStatusDTO())

const displaySelectedStatus = computed(() => {
  return selectedStatus.value || { label: 'Add to list' }
})

const ListOptions = [
  {
    value: 1,
    label: 'Watching'
  },
  {
    value: 4,
    label: 'Plan To Watch'
  },
  {
    value: 2,
    label: 'Completed'
  }
]

const handleClickOutside = (event: any) => {
  if (
    statusDropdown.value &&
    !statusDropdown.value.contains(event.target) &&
    showList.value == true
  ) {
    showList.value = false
  }
}

onMounted(async () => {
  document.addEventListener('click', handleClickOutside)
  await MoviesApi.GetMovieInfo(Id)
  Movie.value = MoviesApi.GetMovie()

  UserListData.value.Id = Movie.value.Id
  UserListData.value.UserId = authApi.UserData.Id

  MoviesApi.resetMovieInfo()
})

onUnmounted(() => {
  document.removeEventListener('click', handleClickOutside)
})

watch(selectedStatus, () => {
  toggleList()
  UserListData.value.Status = selectedStatus.value.value
  MoviesApi.ChangeMovieListStatus(UserListData.value)
})

const toggleList = () => {
  showList.value = !showList.value
}
</script>

<template>
  <section>
    <div id="left-side">
      <div id="movie-img">
        <img :src="`data:image/jpeg;base64,${Movie.MovieImageData}`" />
      </div>
    </div>

    <div id="center-side">
      <div id="synopsis">
        <h4>Synopsis</h4>
        <p>
          {{ Movie.Synopsis }}
        </p>
      </div>
    </div>

    <div id="right-side">
      <ul id="list-menu">
        <li id="watched-select" ref="statusDropdown">
          <div class="custom-select" :class="showList ? 'active' : null">
            <button class="select-button" @click="toggleList">
              <span class="selected-value">{{ displaySelectedStatus.label }}</span
              ><span class="arrow"></span>
            </button>
            <ul class="select-dropdown" role="listbox" id="select-dropdown">
              <li role="option" v-for="i in ListOptions" :key="i.value" @click="toggleList">
                <input type="radio" :value="i" :id="i.label" v-model="selectedStatus" />
                <label :for="i.label">{{ i.label }}</label>
              </li>
            </ul>
          </div>
        </li>
        <li>Rate</li>
        <li></li>
        <li></li>
      </ul>
    </div>
  </section>
</template>

<style scoped>
section {
  display: flex;
}

#left-side {
  width: 25%;
  height: 100%;
  padding: 3px;
}

#left-side > #movie-img > img {
  width: 100%;
}

/* ---------------------------------------------------------------------- */

#center-side {
  width: 45%;
  height: 100%;
  padding: 0px 10px;
}

#center-side > #synopsis > h4 {
  border-bottom: 1px solid rgb(0, 0, 0);
  margin-bottom: 4px;
  padding-bottom: 2px;
  font-size: 20px;
}

/* ---------------------------------------------------------------------- */

#right-side {
  width: 30%;
  height: 100%;
}

#right-side > #list-menu {
  list-style: none;
  background-color: rgb(173, 196, 214);
}

#right-side > #list-menu > li {
  padding: 10px 0;
  border-bottom: 1px solid rgb(122, 113, 100);
}

#right-side > #list-menu > li:last-child {
  border-bottom: none;
}

#watched-select {
  display: flex;
  justify-content: center;
}
</style>
