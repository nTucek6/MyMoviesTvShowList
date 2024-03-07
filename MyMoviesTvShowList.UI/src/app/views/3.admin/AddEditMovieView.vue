<script setup lang="ts">
import { RouterLink } from 'vue-router'
import { ref, watch, onBeforeMount, computed, onMounted } from 'vue'
import { useMoviesAdminApi } from '@/stores/MoviesAdmin/moviesadmin'
import Multiselect from '@vueform/multiselect'
import { useGlobalHelper } from '@/stores/globalhelper'

const MoviesAdminApi = useMoviesAdminApi();
const globalhelper = useGlobalHelper()

const Id = ref(0)
const Title = ref()
const Duration = ref(0)
const Synopsis = ref()
const ReleaseDate = ref()
const Genres = ref()
const Directors = ref()
const Screenwriter = ref()
const Actors = ref()
const Image = ref()
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
    Id.value = d.value.id
    Title.value = d.value.movieName
    Duration.value = d.value.duration
    Synopsis.value = d.value.synopsis
    //Genres.value = d.value.genres.map((item: any) => item.value);
    d.value.genres.map((item: any) => GenresDefault.value.select(item.value));
    //Directors.value = d.value.director.map((item: any) => { return { value: item.id, label: item.firstName + " " + item.lastName } })
    d.value.director.map((item: any) => DirectorDefault.value.select({ value: item.id, label: item.firstName + " " + item.lastName }))
    //Screenwriter.value = d.value.writers.map((item: any) => { return { value: item.id, label: item.firstName + " " + item.lastName } })
    d.value.writers.map((item: any) => ScreenwriterDefault.value.select({ value: item.id, label: item.firstName + " " + item.lastName }))
    //Actors.value = d.value.actors.map((item: any) => { return { value: item.id, label: item.firstName + " " + item.lastName, CharacterName: item.characterName } })
    d.value.actors.map((item: any) => ActorsDefault.value.select({ value: item.id, label: item.firstName + " " + item.lastName, CharacterName: item.characterName }))

    ImagePreview.value = "data:image/png;base64," + d.value.movieImageData

    const date = globalhelper.formatInputDate(new Date(d.value.releaseDate))
    ReleaseDate.value = date

    MoviesAdminApi.setEditMovie(undefined)
  }
});


watch(Genres, () => {
  console.log(Genres.value);
});


const SearchCrew = async (search: any) => {
  if (search != null) {
    await MoviesAdminApi.GetCrew(search)

    const data = computed(() => MoviesAdminApi.MovieCrew);

    if (data != undefined) {
      //MoviesAdminApi.setMovieCrew(undefined)
      return data.value.map((item: any) => {
        return { value: item.value, label: item.label }
      })
    }
  }
  return null
}

const handleImageChange = (event: any) => {
  Image.value = event.target.files[0];

  if (Image.value && Image.value.type.startsWith('image/')) {
    const reader = new FileReader();
    reader.onload = (e: any) => {
      ImagePreview.value = e.target.result;
    };
    reader.readAsDataURL(Image.value);
  }
}

const ClearFormData = () => {
  Id.value = 0
  Title.value = null
  Duration.value = 0
  Synopsis.value = null
  Genres.value = null
  ReleaseDate.value = null
  Directors.value = null
  Screenwriter.value = null
  Actors.value = null
  Image.value = null
  ImagePreview.value = null
}


const addMovieFormSubmit = async () => {
  const Movie = new FormData();
  Movie.append("Id", Id.value.toString());
  Movie.append("MovieName", Title.value);
  Movie.append("Duration", Duration.value.toString());
  Movie.append("Synopsis", Synopsis.value);
  Movie.append("Genres", Genres.value.map((x: any) => x))
  Movie.append("ReleaseDate", ReleaseDate.value);
  Movie.append("Director", Directors.value.map((x: any) => x));
  Movie.append("Writers", Screenwriter.value.map((x: any) => x));
  Movie.append("Actors", JSON.stringify(Actors.value));
  Movie.append("MovieImageData", Image.value);

  MoviesAdminApi.SaveMovie(Movie).then(() => {
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
        <input type="text" v-model="Title" class="w-50" id="title" placeholder="Movie title" />
      </div>
      <div class="form-group mb-3">
        <input type="text" v-model="Duration" class="w-50" id="title" placeholder="Duration" />
      </div>
      <div class="form-group mb-3">
        <textarea type="text" v-model="Synopsis" class="w-50" id="title" placeholder="Synopsis"></textarea>
      </div>
      <div class="form-group mb-3">
        <input type="date" v-model="ReleaseDate" class="w-50" id="title" placeholder="Release date" />
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
        <div v-for="a in Actors">
          <label>{{ a.label }} </label>
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