<script setup lang="ts">
import { useRouter } from 'vue-router'
import { onMounted, computed } from 'vue'
import { useMoviesAdminApi } from '@/stores/moviesadmin'
import type { MoviesDTO } from '@/app/shared/models/movies.model'
import AdminNavigationComponent from '@/app/shared/components/AdminNavigationComponent.vue'
import { moviesParams } from '@/app/views/3.admin/movies/moviesparams'

const MoviesAdminApi = useMoviesAdminApi()
const router = useRouter()

onMounted(() => {
  MoviesAdminApi.GetMovies(10, 1, '')
})

const MovieList = computed<MoviesDTO[]>(() => MoviesAdminApi.MovieData)

const EditMovie = (data: any) => {
  MoviesAdminApi.setEditMovie(data)
  MoviesAdminApi.setIsEdit(true)
  router.push({ name: 'Add & Edit movie' })
}
</script>

<template>
  <div>
    <AdminNavigationComponent :routes="moviesParams" />

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
              >{{ d.FirstName }} {{ d.LastName
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
table {
  margin-top: 30px;
}
</style>
