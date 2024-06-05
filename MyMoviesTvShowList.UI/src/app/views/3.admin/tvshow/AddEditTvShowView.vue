<script setup lang="ts">
import { ref, onBeforeMount, computed } from 'vue'
import { useTvShowAdminStore } from '@/stores/tvshowadmin'
import Multiselect from '@vueform/multiselect'

import AdminNavigationComponent from '@/app/shared/components/AdminNavigationComponent.vue'
import { tvShowParams } from '@/app/views/3.admin/tvshow/tvshowparams'
import { SaveTVShowDTO } from '@/app/shared/models/save-tvshow.model'
import { useMoviesAdminApi } from '@/stores/moviesadmin'

const TvShowAdminApi = useTvShowAdminStore()
const MoviesAdminApi = useMoviesAdminApi()

const TVShow = ref<SaveTVShowDTO>(new SaveTVShowDTO())

const Genres = ref()
//const Directors = ref()
//const Screenwriter = ref()
const Creators = ref()
const Actors = ref()

const ImagePreview = ref()

const GenresDefault = ref()
//const DirectorDefault = ref()
//const ScreenwriterDefault = ref()
const CreatorsDefault = ref()
const ActorsDefault = ref()

const GetGenres = computed(() => MoviesAdminApi.Genres)

onBeforeMount(async () => {
  await MoviesAdminApi.GetGenres()
})

const SearchCrew = async (search: any) => {
  if (search != null) {
    await MoviesAdminApi.GetCrew(search)

    const data = computed(() => MoviesAdminApi.MovieCrew)

    if (data.value != undefined) {
      return data.value.map((item: any) => {
        return { value: item.value, label: item.label }
      })
    }
  }
  return null
}

const handleImageChange = (event: any) => {
  TVShow.value.TVShowImageData = event.target.files[0]

  if (TVShow.value.TVShowImageData && TVShow.value.TVShowImageData.type.startsWith('image/')) {
    const reader = new FileReader()
    reader.onload = (e: any) => {
      ImagePreview.value = e.target.result
    }
    reader.readAsDataURL(TVShow.value.TVShowImageData)
  }
}

const ClearFormData = () => {
  TVShow.value = new SaveTVShowDTO()
  ImagePreview.value = null
}

const addTVShowFormSubmit = async () => {
  const t = new FormData()
  t.append('Id', TVShow.value.Id.toString())
  t.append('Title', TVShow.value.Title)

  t.append('Description', TVShow.value.Description)
  t.append(
    'Genres',
    Genres.value.map((x: any) => x)
  )
  t.append('Runtime', TVShow.value.Runtime)
  //t.append('Director',Directors.value.map((x: any) => x))
  //t.append('Writers',Screenwriter.value.map((x: any) => x))
  t.append(
    'Creators',
    Creators.value.map((x: any) => x)
  )

  t.append('Actors', JSON.stringify(Actors.value))
  t.append('TVShowImageData', TVShow.value.TVShowImageData)
  t.append('TotalSeason', TVShow.value.TotalSeason.toString())
  t.append('TotalEpisode', TVShow.value.TotalEpisode.toString())

  TvShowAdminApi.SaveTVShow(t).then(() => {
    ClearFormData()
  })
}
</script>

<template>
  <AdminNavigationComponent :routes="tvShowParams" />

  <form @submit.prevent="addTVShowFormSubmit" class="text-center">
    <div class="form-group mb-3">
      <input
        type="text"
        v-model="TVShow.Title"
        class="w-50"
        id="title"
        placeholder="TvShow title"
      />
    </div>

    <div class="form-group mb-3">
      <textarea
        type="text"
        v-model="TVShow.Description"
        class="w-50"
        id="description"
        placeholder="TvShow description"
      ></textarea>
    </div>

    <div class="form-group mb-3">
      <input
        type="number"
        v-model="TVShow.TotalSeason"
        class="w-50"
        id="totalSeason"
        placeholder="TvShow total seasons"
      />
    </div>

    <div class="form-group mb-3">
      <input
        type="number"
        v-model="TVShow.TotalEpisode"
        class="w-50"
        id="totalEpisodes"
        placeholder="TvShow total episodes"
      />
    </div>

    <div class="form-group mb-3">
      <input type="text" v-model="TVShow.Runtime" class="w-50" id="runtime" placeholder="Runtime" />
    </div>

    <div class="form-group mb-3">
      <Multiselect
        placeholder="Select genres..."
        v-model="Genres"
        :options="GetGenres"
        mode="tags"
        :close-on-select="false"
        :searchable="true"
        :create-option="true"
        :preselect-first="true"
        ref="GenresDefault"
      />
    </div>

    <div class="form-group mb-3">
      <Multiselect
        placeholder="Select creators..."
        v-model="Creators"
        mode="tags"
        :close-on-select="false"
        :filter-results="false"
        :min-chars="1"
        :resolve-on-load="false"
        :delay="500"
        :searchable="true"
        ref="CreatorsDefault"
        :options="
          async function (query: any, select$: any) {
            return await SearchCrew(query) // check JS block in JSFiddle for implementation
          }
        "
      />
    </div>

    <div class="form-group mb-3">
      <Multiselect
        placeholder="Select actors..."
        v-model="Actors"
        mode="tags"
        :close-on-select="false"
        :filter-results="false"
        :min-chars="1"
        :resolve-on-load="false"
        :delay="500"
        :searchable="true"
        :object="true"
        ref="ActorsDefault"
        :options="
          async function (query: any, select$: any) {
            return await SearchCrew(query) // check JS block in JSFiddle for implementation
          }
        "
      />
    </div>

    <div class="form-group mb-3">
      <div v-for="a in Actors" :key="a.value">
        <label>{{ a.label }} </label>
        <div>
          <input
            type="text"
            class="w-50 ml-2"
            placeholder="Character name..."
            v-model="a.CharacterName"
            required
          />
          <textarea
            class="w-50 ml-2 mt-2"
            placeholder="Description..."
            v-model="a.Description"
            required
          ></textarea>
        </div>
      </div>
    </div>

    <div class="form-group mb-3">
      <input
        type="file"
        @change="handleImageChange"
        class="w-50"
        id="surname"
        placeholder="Insert movie image"
      />
    </div>

    <div class="form-group mb-3">
      <img v-if="ImagePreview" :src="ImagePreview" alt="Image Preview" />
    </div>

    <button type="submit" class="btn w-25">Save TvShow</button>
  </form>
</template>

<style scoped>
form {
  margin-top: 30px;
  margin-bottom: 30px;
}
</style>

<style src="@vueform/multiselect/themes/default.css"></style>
