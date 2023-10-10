<script setup lang="ts">
import {RouterLink} from 'vue-router'
import {ref,watch,onBeforeMount,computed, onMounted } from 'vue'
import {useMoviesAdminApi}  from '@/stores/MoviesAdmin/moviesadmin'
import Multiselect from '@vueform/multiselect'
import { useGlobalHelper } from '@/stores/globalhelper'

const MoviesAdminApi = useMoviesAdminApi();
const globalhelper = useGlobalHelper()

const Title = ref()
const Duration = ref(0)
const Synopsis = ref()
const ReleaseDate = ref()
const Genres  = ref() 
const Directors = ref()
const Screenwriter = ref()
const Actors = ref()
const Image = ref()
const ImagePreview = ref()

const GetGenres = computed(() => MoviesAdminApi.Genres);
const GetCrew = ref()

onBeforeMount(async () => {
 await MoviesAdminApi.GetGenres();
});

onMounted(()=>{
const d =  computed(() => MoviesAdminApi.EditMovie);

if(d.value != undefined)
{
  Title.value = d.value.movieName
  Duration.value = d.value.Duration
  Synopsis.value = d.value.Synopsis
  ReleaseDate.value = globalhelper.formatInputDate(new Date(d.value.releaseDate))
  ImagePreview.value = "data:image/png;base64,"+ d.value.movieImageData
  MoviesAdminApi.setEditMovie(undefined)
}
})

/*watch(Genres, () => {
      console.log(Genres.value);
      });
      */

const SearchCrew = async (search:any) =>
{
if(search != null)
{
  await MoviesAdminApi.GetCrew(search)

const data =  computed(() => MoviesAdminApi.MovieCrew);

if(data != undefined)
{
  //MoviesAdminApi.setMovieCrew(undefined)
  return data.value.map((item:any) => {
    return { value: item.value, label: item.label }
  })
}
else
{
  return null
}
}
else
{
  return null
}
} 

const handleImageChange = (event:any) => {
      Image.value = event.target.files[0];
      
      if (Image.value && Image.value.type.startsWith('image/')) {
        const reader = new FileReader();
        reader.onload = (e:any) => {
          ImagePreview.value = e.target.result;
        };
        reader.readAsDataURL(Image.value);
      }

    }

const addMovieFormSubmit = async () =>
{
 
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
    <input type="text" v-model="Synopsis" class="w-50" id="title" placeholder="Synopsis" />
   </div>
   <div class="form-group mb-3">
    <input type="date" v-model="ReleaseDate" class="w-50" id="title" placeholder="Release date" />
   </div>
   <div class="form-group mb-3 ">
    <Multiselect
      placeholder="Select genres..."
      v-model="Genres"
      :options="GetGenres"
      mode="tags"
      :close-on-select="false"
      :searchable="true"
      :create-option="true"
    />
   </div>

   <div class="form-group mb-3 ">
    <Multiselect
      v-model="Directors"
      mode="tags"
      placeholder="Search director..."
      :close-on-select="false"
      :filter-results="false"
      :min-chars="1"
      :resolve-on-load="false"
      :delay="0"
      :searchable="true"
      :options="async function(query:any, select$:any) {
       return await SearchCrew(query) // check JS block in JSFiddle for implementation
  }"
/>
   </div>

  

   <div class="form-group mb-3 ">
    <Multiselect
      placeholder="Select screenwriter..."
      v-model="Screenwriter"
      mode="tags"
      :close-on-select="false"
      :searchable="true"
      :create-option="true"
      :options="async function(query:any, select$:any) {
       return await SearchCrew(query) // check JS block in JSFiddle for implementation
  }"
    />
   </div>

   <div class="form-group mb-3 ">
    <Multiselect
      placeholder="Select actors..."
      v-model="Actors"
      :options="GetGenres"
      mode="tags"
      :close-on-select="false"
      :searchable="true"
      :create-option="true"
    />
   </div>

   <div class="form-group mb-3 ">
    <input type="file" @change="handleImageChange" class="w-50" id="surname" placeholder="Insert movie image" />
   </div>

   <button type="submit" class="btn w-25">Add movie</button>

  </form>

 </div>
</template>

<style src="@vueform/multiselect/themes/default.css"></style>