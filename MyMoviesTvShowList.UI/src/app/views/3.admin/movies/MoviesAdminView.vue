<script setup lang="ts">
import { useRouter, RouterView, useRoute } from 'vue-router'
import { onMounted, computed, ref, watch } from 'vue'
import { useMoviesAdminApi } from '@/stores/admin/moviesadmin'
import type { MoviesDTO } from '@/app/shared/models/movies.model'
import AdminNavigationComponent from '@/app/shared/components/AdminNavigationComponent.vue'
import { moviesParams } from '@/app/views/3.admin/movies/moviesparams'

const MoviesAdminApi = useMoviesAdminApi()
const router = useRouter()

const route = useRoute()

const page = ref<number>(1)

const search = ref<string>('')

const postPerPage = 10

const disableShowMore = ref<boolean>(false)

const maxMoviesCount = computed(() => MoviesAdminApi.MovieCount)

const isAddEditMovieRoute = computed(() => route.path.includes('addeditmovie'))

onMounted(async () => {
  await MoviesAdminApi.GetMoviesCount()
  await MoviesAdminApi.GetMovies(postPerPage, page.value, search.value)
  checkMovieCount()
})

const MovieList = computed<MoviesDTO[]>(() => MoviesAdminApi.MovieData)

const EditMovie = (data: any) => {
  MoviesAdminApi.setEditMovie(data)
  router.push({ name: 'Add & Edit movie' })
}

const showMore = () => {
  page.value++
  MoviesAdminApi.GetMovies(postPerPage, page.value, search.value)
}

const checkMovieCount = () => {
  if (MovieList.value.length == maxMoviesCount.value) {
    disableShowMore.value = true
  }
}

watch(MovieList, () => {
  checkMovieCount()
})
</script>

<template>
  <AdminNavigationComponent :routes="moviesParams" />
  <template v-if="!isAddEditMovieRoute">
    <div class="wrapper">
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
            <td data-cell="title">{{ m.MovieName }}</td>
            <td data-cell="genres">
              <span v-for="(g, index) in m.Genres" :key="index"
                >{{ g.label }}<span v-if="index !== m.Genres.length - 1">, </span>
              </span>
            </td>
            <td data-cell="duration">{{ m.Duration }}</td>
            <td data-cell="actors">
              <span v-for="(a, index) in m.Actors" :key="index"
                >{{ a.FirstName }} {{ a.LastName
                }}<span v-if="index !== m.Actors.length - 1">, </span>
              </span>
            </td>
            <td data-cell="director">
              <span v-for="(d, index) in m.Director" :key="index"
                >{{ d.FirstName }} {{ d.LastName
                }}<span v-if="index !== m.Director.length - 1">, </span>
              </span>
            </td>
            <td data-cell="writers">
              <span v-for="(d, index) in m.Writers" :key="index"
                >{{ d.FirstName }} {{ d.LastName
                }}<span v-if="index !== m.Writers.length - 1">, </span>
              </span>
            </td>
            <td data-cell="edit">
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

    <button @click="showMore" class="btn" :disabled="disableShowMore">Show more</button>
  </template>
  <RouterView />
</template>

<style scoped>
table {
  margin-top: 30px;
}
</style>
