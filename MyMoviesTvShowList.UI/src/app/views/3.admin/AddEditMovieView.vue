<script setup lang="ts">
import { RouterLink } from 'vue-router'
import { ref, watch, onBeforeMount, computed } from 'vue'
import { useMoviesAdminApi } from '@/stores/MoviesAdmin/moviesadmin'
import Multiselect from '@vueform/multiselect'
import { useGlobalHelper } from '@/stores/globalhelper'
import { SaveMovieDTO } from '@/app/shared/models/save-movie.model'

const MoviesAdminApi = useMoviesAdminApi();
const globalhelper = useGlobalHelper()


const Movie = ref<SaveMovieDTO>(new SaveMovieDTO());

const Genres = ref()
const Directors = ref()
const Screenwriter = ref()
const Actors = ref()


const ImagePreview = ref()

const GenresDefault = ref()
const DirectorDefault = ref()
const ScreenwriterDefault = ref()
const ActorsDefault = ref()

const GetGenres = computed(() => MoviesAdminApi.Genres);

onBeforeMount(async () => {
  await MoviesAdminApi.GetGenres();

  const d = computed(() => MoviesAdminApi.EditMovie);
  if (d.value != undefined) {
    Movie.value.Id = d.value.id
    Movie.value.MovieName = d.value.movieName
    Movie.value.Duration = d.value.duration
    Movie.value.Synopsis = d.value.synopsis
    d.value.genres.map((item: any) => GenresDefault.value.select(item.value));
    d.value.director.map((item: any) => DirectorDefault.value.select({ value: item.id, label: item.firstName + " " + item.lastName }))
    d.value.writers.map((item: any) => ScreenwriterDefault.value.select({ value: item.id, label: item.firstName + " " + item.lastName }))
    d.value.actors.map((item: any) => ActorsDefault.value.select({ value: item.id, label: item.firstName + " " + item.lastName, CharacterName: item.characterName }))

    ImagePreview.value = "data:image/png;base64," + d.value.movieImageData

    const date = globalhelper.formatInputDate(new Date(d.value.releaseDate))
    Movie.value.ReleaseDate = date

    MoviesAdminApi.setEditMovie(undefined)
  }
});

const SearchCrew = async (search: any) => {
  if (search != null) {
    await MoviesAdminApi.GetCrew(search)

    const data = computed(() => MoviesAdminApi.MovieCrew);

    if (data.value != undefined) {
      //MoviesAdminApi.setMovieCrew(undefined)
      return data.value.map((item: any) => {
        return { value: item.value, label: item.label }
      })
    }
  }
  return null
}

const handleImageChange = (event: any) => {
  Movie.value.MovieImageData = event.target.files[0];

  if ( Movie.value.MovieImageData &&  Movie.value.MovieImageData.type.startsWith('image/')) {
    const reader = new FileReader();
    reader.onload = (e: any) => {
      ImagePreview.value = e.target.result;
    };
    reader.readAsDataURL(Movie.value.MovieImageData);
  }
}

const ClearFormData = () => {
  Movie.value = new SaveMovieDTO()
  ImagePreview.value = null
}


const addMovieFormSubmit = async () => {
  const m = new FormData();
  m.append("Id", Movie.value.Id.toString());
  m.append("MovieName", Movie.value.MovieName);
  m.append("Duration", Movie.value.Duration.toString());
  m.append("Synopsis", Movie.value.Synopsis);
  m.append("Genres", Genres.value.map((x: any) => x))
  m.append("ReleaseDate", Movie.value.ReleaseDate);
  m.append("Director", Directors.value.map((x: any) => x));
  m.append("Writers", Screenwriter.value.map((x: any) => x));
  m.append("Actors", JSON.stringify(Actors.value));
  m.append("MovieImageData", Movie.value.MovieImageData);

  MoviesAdminApi.SaveMovie(m).then(() => {
    ClearFormData()
  });
}

</script>

<template>
  <div>
    <RouterLink to="/moviesadmin" class="btn">View all movies</RouterLink>
    <RouterLink to="/addeditmovie" class="btn">Add movie</RouterLink>
    <hr />

    <form @submit.prevent="addMovieFormSubmit" class="text-center">
      <div class="form-group mb-3">
        <input type="text" v-model="Movie.MovieName" class="w-50" id="title" placeholder="Movie title" />
      </div>
      <div class="form-group mb-3">
        <input type="text" v-model="Movie.Duration" class="w-50" id="title" placeholder="Duration" />
      </div>
      <div class="form-group mb-3">
        <textarea type="text" v-model="Movie.Synopsis" class="w-50" id="title" placeholder="Synopsis"></textarea>
      </div>
      <div class="form-group mb-3">
        <input type="date" v-model="Movie.ReleaseDate" class="w-50" id="title" placeholder="Release date" />
      </div>
      <div class="form-group mb-3 ">
        <Multiselect placeholder="Select genres..." v-model="Genres" :options="GetGenres" mode="tags"
          :close-on-select="false" :searchable="true" :create-option="true" :preselect-first="true" ref="GenresDefault" />
      </div>

      <div class="form-group mb-3 ">
        <Multiselect v-model="Directors" mode="tags" placeholder="Search director..." :close-on-select="false"
          :filter-results="false" :min-chars="1" :resolve-on-load="false" :delay="0" :searchable="true"
          ref="DirectorDefault" :options="async function (query: any, select$: any) {
            return await SearchCrew(query) // check JS block in JSFiddle for implementation
          }" />
      </div>
      <div class="form-group mb-3 ">
        <Multiselect placeholder="Select screenwriter..." v-model="Screenwriter" mode="tags" :close-on-select="false"
          :filter-results="false" :min-chars="1" :resolve-on-load="false" :delay="0" :searchable="true"
          ref="ScreenwriterDefault" :options="async function (query: any, select$: any) {
            return await SearchCrew(query) // check JS block in JSFiddle for implementation
          }" />
      </div>

      <div class="form-group mb-3 ">
        <Multiselect placeholder="Select actors..." v-model="Actors" mode="tags" :close-on-select="false"
          :filter-results="false" :min-chars="1" :resolve-on-load="false" :delay="0" :searchable="true" :object="true"
          ref="ActorsDefault" :options="async function (query: any, select$: any) {
            return await SearchCrew(query) // check JS block in JSFiddle for implementation
          }" />
      </div>
      <div class="form-group mb-3">
        <div v-for="a in Actors" :key="a.value">
          <label>{{ a }} </label>
          <input type="text" class="w-50 ml-2" placeholder="Character name..." v-model="a.CharacterName" required />
        </div>

      </div>

      <div class="form-group mb-3 ">
        <input type="file" @change="handleImageChange" class="w-50" id="surname" placeholder="Insert movie image" />
      </div>

      <div class="form-group mb-3">
        <img v-if="ImagePreview" :src="ImagePreview" alt="Image Preview" />

      </div>

      <button type="submit" class="btn w-25">Add movie</button>

    </form>

  </div>
</template>

<style src="@vueform/multiselect/themes/default.css"></style>