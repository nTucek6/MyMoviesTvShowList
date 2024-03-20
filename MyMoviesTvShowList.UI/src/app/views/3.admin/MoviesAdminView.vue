<script setup lang="ts">
import { RouterLink, useRouter } from 'vue-router'
import { onMounted, computed } from 'vue'
import { useMoviesAdminApi } from '@/stores/MoviesAdmin/moviesadmin'
import { useGlobalHelper } from '@/stores/globalhelper'
import type { MoviesDTO } from '@/app/shared/models/movies.model'

const MoviesAdminApi = useMoviesAdminApi()
//const globalhelper = useGlobalHelper()
const router = useRouter()

onMounted(() => {
  MoviesAdminApi.GetMovies(10, 1, '')
})

const MovieList = computed<MoviesDTO[]>(() => MoviesAdminApi.MovieData)

const EditMovie = (data: any) => {
  MoviesAdminApi.setEditMovie(data)
  router.push({ name: 'Add & Edit movie' })
}
</script>

<template>
  <div>
    <RouterLink to="/moviesadmin" class="btn">View all movies</RouterLink>
    <RouterLink to="/addeditmovie" class="btn">Add movie</RouterLink>
    <hr />

    <table class="table table-striped" v-if="MovieList">
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
        <tr v-for="m in MovieList" :key="m.Id">
          <td>{{ m.MovieName }}</td>
          <td>
            <span v-for="(g, index) in m.Genres" :key="index"
              >{{ g.label }}<span v-if="index !== m.Genres.length - 1">, </span>
            </span>
          </td>
          <td>{{ m.Duration }}</td>
          <td>
            <span v-for="(a, index) in m.Actors" :key="index"
              >{{ a.FirstName }} {{ a.LastName
              }}<span v-if="index !== m.Actors.length - 1">, </span>
            </span>
          </td>
          <td>
            <span v-for="(d, index) in m.Director" :key="index"
              >{{ d.FirstName }} {{ d.LastName
              }}<span v-if="index !== m.Director.length - 1">, </span>
            </span>
          </td>
          <td>
            <span v-for="(d, index) in m.Writers" :key="index"
              >{{ d.FirstName }} {{ d.FirstName
              }}<span v-if="index !== m.Writers.length - 1">, </span>
            </span>
          </td>
          <td>
            <span
              style="cursor: pointer"
              @click="
                () => {
                  EditMovie(m)
                }
              "
              ><font-awesome-icon :icon="['fas', 'edit']"
            /></span>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped>

*{
  color:white;
}

</style>
