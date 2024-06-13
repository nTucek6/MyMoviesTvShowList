<script setup lang="ts">
import { ref, onBeforeMount, computed } from 'vue'
import { useTvShowAdminStore } from '@/stores/admin/tvshowadmin'
import Multiselect from '@vueform/multiselect'

import AdminNavigationComponent from '@/app/shared/components/AdminNavigationComponent.vue'
import { tvShowParams } from '@/app/views/3.admin/tvshow/tvshowparams'
import { SaveTVShowDTO } from '@/app/shared/models/save-tvshow.model'
import { useMoviesAdminApi } from '@/stores/admin/moviesadmin'
import { useGlobalHelper } from '@/stores/globalhelper'

const TvShowAdminApi = useTvShowAdminStore()
const MoviesAdminApi = useMoviesAdminApi()
const globalhelper = useGlobalHelper()

const TVShow = ref<SaveTVShowDTO>(new SaveTVShowDTO())

const Genres = ref()
const Creators = ref()
const Actors = ref()

const ImagePreview = ref()

const GenresDefault = ref()

const CreatorsDefault = ref()
const ActorsDefault = ref()

const query = ref()

const isEdit = ref(false)

const loading = ref(false)

const GetGenres = computed(() => MoviesAdminApi.Genres)

const d = computed(() => TvShowAdminApi.EditTVShow)

onBeforeMount(async () => {
  await MoviesAdminApi.GetGenres()
  setEditValues()
})

const setEditValues = () => {

  if (d.value != undefined) {
    isEdit.value = true;

    TVShow.value.Id = d.value.Id
    TVShow.value.Title = d.value.Title
    TVShow.value.Description = d.value.Description
    TVShow.value.Runtime = d.value.Runtime
    TVShow.value.TotalSeason = d.value.TotalSeason
    TVShow.value.TotalEpisode = d.value.TotalEpisode
    d.value.Genres.map((item: any) => GenresDefault.value.select(item.value))
    d.value.Creators.map((item: any) =>
      CreatorsDefault.value.select({ value: item.Id, label: item.FirstName + ' ' + item.LastName })
    )
    d.value.Actors.map((item: any) =>
      ActorsDefault.value.select({
        value: item.Id,
        label: item.FirstName + ' ' + item.LastName,
        CharacterName: item.CharacterName,
        Description: item.CharacterDescription
      })
    )

    ImagePreview.value = 'data:image/png;base64,' + d.value.TVShowImageData

    //const date = globalhelper.formatInputDate(new Date(d.value.ReleaseDate))
    //Movie.value.ReleaseDate = date

    TvShowAdminApi.setEditTVShow(undefined)
  }
}

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
  Genres.value = null
  Creators.value = null
  Actors.value = null

  GenresDefault.value.clear()
  CreatorsDefault.value.clear()
  ActorsDefault.value.clear()

  query.value = ""
  isEdit.value = false
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

const handleTVShowSearchSearch = async () => {
  loading.value = true
  await TvShowAdminApi.GetTVShowFromAPI(query.value)
  setEditValues()
  loading.value = false
}

</script>

<template>
  <AdminNavigationComponent :routes="tvShowParams" />

  <div class="text-center mt-5" v-if="!isEdit">
      <div class="form-group mb-3">
        <input
          type="text"
          v-model="query"
          class="w-50"
          id="query"
          placeholder="Search for movie..."
        />
      </div>
      <div v-if="loading" class="lds-dual-ring"></div>
      <button class="btn" @click="handleTVShowSearchSearch" v-else>Search</button>
    </div>

  <form @submit.prevent="addTVShowFormSubmit" class="text-center mb-3 mt-5">
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
        id="totalEpisode"
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

    <button type="button" class="btn w-25 mr-1" @click="ClearFormData">Clear</button>
    <button type="submit" class="btn w-25">Save TvShow</button>
  </form>
</template>

<style scoped>
@import '@/assets/custom/spinner.css';
</style>

<style src="@vueform/multiselect/themes/default.css"></style>
