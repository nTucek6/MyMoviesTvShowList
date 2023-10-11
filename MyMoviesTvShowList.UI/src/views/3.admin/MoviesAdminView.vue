<script setup lang="ts">
import {RouterLink,useRouter} from 'vue-router'
import {ref, onMounted, computed} from 'vue'
import {useMoviesAdminApi}  from '@/stores/MoviesAdmin/moviesadmin'

const MoviesAdminApi = useMoviesAdminApi()
const router = useRouter()

onMounted(() => {
   MoviesAdminApi.GetMovies(10,1,"")
});

const MovieList = computed(() => MoviesAdminApi.MovieData);

const EditMovie = (data:any) =>
{
   //console.log(data);
   MoviesAdminApi.setEditMovie(data)

   router.push({ name: 'Add & Edit movie' })
}

</script>

<template>
 <div>
   <RouterLink to="/moviesadmin" class="btn">View all movies</RouterLink>
   <RouterLink to="/addeditmovie" class="btn">Add movie</RouterLink>
   <hr />

   <table class="table">
      <thead>
         <tr>
            <th>Movie title</th>
            <th>Genres</th>
            <th>Duration</th>
            <th>Actors</th>
            <th>Director</th>
            <th>Writers</th>
            <th>Edit</th>
         </tr>

      </thead>
      <tbody>
         <tr v-for="m in MovieList">
            <td>{{ m.movieName }}</td>
            <td><span v-for="(g, index) in m.genres">{{ g.label }}<span v-if="index !== m.genres.length - 1">, </span> </span></td>
            <td>{{ m.duration }}</td>
            <td><span v-for="(a, index)  in m.actors">{{ a.firstName }} {{ a.lastName }}<span v-if="index !== m.actors.length - 1">, </span> </span></td>
            <td><span v-for="(d, index)  in m.director">{{ d.firstName }} {{ d.lastName }}<span v-if="index !== m.director.length - 1">, </span> </span></td>
            <td><span v-for="(d, index)  in m.writers">{{ d.firstName }} {{ d.lastName }}<span v-if="index !== m.writers.length - 1">, </span> </span></td>
            <td><button @click="()=>{EditMovie(m)}">Edit</button></td>
         </tr>


      </tbody>
   </table>


 </div>
</template>