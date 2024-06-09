<script setup lang="ts">
import { ref, onBeforeMount, computed } from 'vue'
import { useMoviesAdminApi } from '@/stores/moviesadmin'
import Multiselect from '@vueform/multiselect'
import { useGlobalHelper } from '@/stores/globalhelper'
import { SaveMovieDTO } from '@/app/shared/models/save-movie.model'
import AdminNavigationComponent from '@/app/shared/components/AdminNavigationComponent.vue'
import { moviesParams } from '@/app/views/3.admin/movies/moviesparams'

const MoviesAdminApi = useMoviesAdminApi()
const globalhelper = useGlobalHelper()

const Movie = ref<SaveMovieDTO>(new SaveMovieDTO())

const Genres = ref()
const Directors = ref()
const Screenwriter = ref()
const Actors = ref()

const ImagePreview = ref()

const GenresDefault = ref()
const DirectorDefault = ref()
const ScreenwriterDefault = ref()
const ActorsDefault = ref()

const GetGenres = computed(() => MoviesAdminApi.Genres)

const query = ref()

const isEdit = ref()

const d = computed(() => MoviesAdminApi.EditMovie)

onBeforeMount(async () => {
  await MoviesAdminApi.GetGenres()
  isEdit.value = MoviesAdminApi.isEdit
  setEditValues()
})

const setEditValues = () => {
  if (d.value != undefined) {
    Movie.value.Id = d.value.Id
    Movie.value.MovieName = d.value.MovieName
    Movie.value.Duration = d.value.Duration
    Movie.value.Synopsis = d.value.Synopsis
    d.value.Genres.map((item: any) => GenresDefault.value.select(item.value))
    d.value.Director.map((item: any) =>
      DirectorDefault.value.select({ value: item.Id, label: item.FirstName + ' ' + item.LastName })
    )
    d.value.Writers.map((item: any) =>
      ScreenwriterDefault.value.select({
        value: item.Id,
        label: item.FirstName + ' ' + item.LastName
      })
    )
    d.value.Actors.map((item: any) =>
      ActorsDefault.value.select({
        value: item.Id,
        label: item.FirstName + ' ' + item.LastName,
        CharacterName: item.CharacterName,
        Description: item.CharacterDescription
      })
    )

    if (d.value.MovieImageData != null) {
      ImagePreview.value = 'data:image/png;base64,' + d.value.MovieImageData
    }

    const date = globalhelper.formatInputDate(new Date(d.value.ReleaseDate))
    Movie.value.ReleaseDate = date

    MoviesAdminApi.setEditMovie(undefined)
    MoviesAdminApi.setIsEdit(false)
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
  Movie.value.MovieImageData = event.target.files[0]

  if (Movie.value.MovieImageData && Movie.value.MovieImageData.type.startsWith('image/')) {
    const reader = new FileReader()
    reader.onload = (e: any) => {
      ImagePreview.value = e.target.result
    }
    reader.readAsDataURL(Movie.value.MovieImageData)
  }
}

const ClearFormData = () => {
  Movie.value = new SaveMovieDTO()
  ImagePreview.value = null
  Genres.value = null
  Directors.value = null
  Screenwriter.value = null
  Actors.value = null

  GenresDefault.value.clear()
  DirectorDefault.value.clear()
  ScreenwriterDefault.value.clear()
  ActorsDefault.value.clear()

  isEdit.value = false
}

const addMovieFormSubmit = async () => {
  const m = new FormData()
  m.append('Id', Movie.value.Id.toString())
  m.append('MovieName', Movie.value.MovieName)
  m.append('Duration', Movie.value.Duration.toString())
  m.append('Synopsis', Movie.value.Synopsis)
  m.append(
    'Genres',
    Genres.value.map((x: any) => x)
  )
  m.append('ReleaseDate', Movie.value.ReleaseDate)
  m.append(
    'Director',
    Directors.value.map((x: any) => x)
  )
  m.append(
    'Writers',
    Screenwriter.value.map((x: any) => x)
  )
  m.append('Actors', JSON.stringify(Actors.value))
  m.append('MovieImageData', Movie.value.MovieImageData)

  MoviesAdminApi.SaveMovie(m).then(() => {
    ClearFormData()
  })
}

const handleMovieSearch = async () => {
  await MoviesAdminApi.GetMovieFromAPI(query.value)
  setEditValues()
}
</script>

<template>
  <div>
    <AdminNavigationComponent :routes="moviesParams" />

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
      <button class="btn" @click="handleMovieSearch">Search</button>
    </div>

    <form @submit.prevent="addMovieFormSubmit" class="text-center mb-3 mt-5">
      <div class="form-group mb-3">
        <input
          type="text"
          v-model="Movie.MovieName"
          class="w-50"
          id="title"
          placeholder="Movie title"
        />
      </div>
      <div class="form-group mb-3">
        <input
          type="text"
          v-model="Movie.Duration"
          class="w-50"
          id="title"
          placeholder="Duration"
        />
      </div>
      <div class="form-group mb-3">
        <textarea
          type="text"
          v-model="Movie.Synopsis"
          class="w-50"
          id="title"
          placeholder="Synopsis"
        ></textarea>
      </div>
      <div class="form-group mb-3">
        <input
          type="date"
          v-model="Movie.ReleaseDate"
          class="w-50"
          id="title"
          placeholder="Release date"
        />
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
          v-model="Directors"
          mode="tags"
          placeholder="Search director..."
          :close-on-select="false"
          :filter-results="false"
          :min-chars="1"
          :resolve-on-load="false"
          :delay="1000"
          :searchable="true"
          ref="DirectorDefault"
          :options="
            async function (query: any, select$: any) {
              return await SearchCrew(query) // check JS block in JSFiddle for implementation
            }
          "
        />
      </div>
      <div class="form-group mb-3">
        <Multiselect
          placeholder="Select screenwriter..."
          v-model="Screenwriter"
          mode="tags"
          :close-on-select="false"
          :filter-results="false"
          :min-chars="1"
          :resolve-on-load="false"
          :delay="1000"
          :searchable="true"
          ref="ScreenwriterDefault"
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
          :delay="1000"
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
      <button type="submit" class="btn w-25">Save movie</button>
    </form>
  </div>
</template>

<style src="@vueform/multiselect/themes/default.css"></style>
