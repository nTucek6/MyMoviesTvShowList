<script setup lang="ts">
import { useTvShowAdminStore } from '@/stores/admin/tvshowadmin'
import AdminNavigationComponent from '@/app/shared/components/AdminNavigationComponent.vue'
import { tvShowParams } from '@/app/views/3.admin/tvshow/tvshowparams'
import { onMounted, computed, ref, watch } from 'vue'
import type { TVShowDTO } from '@/app/shared/models/tvshow.mode'
import router from '@/router'

const tvshowApi = useTvShowAdminStore()

const page = ref<number>(1)

const search = ref<string>('')

const postPerPage = 10

const disableShowMore = ref<boolean>(false)

const maxTVShowCount = computed(() => tvshowApi.TVShowCount)

onMounted(async () => {
  await tvshowApi.GetTVShowCount()
  await tvshowApi.GetTVShow(postPerPage, page.value, search.value)
  checkTVShowCount()
})

const TVShowList = computed<TVShowDTO[]>(() => tvshowApi.TVShowData)

const EditTVShow = (data: any) => {
  tvshowApi.setEditTVShow(data)
  router.push({ name: 'Add & Edit TVShow' })
}

const showMore = () => {
  page.value++
  tvshowApi.GetTVShow(postPerPage, page.value, search.value)
}

const checkTVShowCount = () => {
  if (TVShowList.value.length == maxTVShowCount.value) {
    disableShowMore.value = true
  }
}

watch(TVShowList, () => {
  checkTVShowCount()
})
</script>

<template>
  <AdminNavigationComponent :routes="tvShowParams" />

  <table class="table table-striped" v-if="TVShowList">
    <thead>
      <tr>
        <th>Title</th>
        <th>Genres</th>
        <th>Runtime</th>
        <th>Actors</th>
        <th>Creators</th>
        <th>Total seasons</th>
        <th>Total episodes</th>
        <th>Edit</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="m in TVShowList" :key="m.Id">
        <td data-cell="title">{{ m.Title }}</td>
        <td data-cell="genres">
          <span v-for="(g, index) in m.Genres" :key="index"
            >{{ g.label }}<span v-if="index !== m.Genres.length - 1">, </span>
          </span>
        </td>
        <td data-cell="runtime">{{ m.Runtime }}</td>
        <td data-cell="actors">
          <span v-for="(a, index) in m.Actors" :key="index"
            >{{ a.FirstName }} {{ a.LastName }}<span v-if="index !== m.Actors.length - 1">, </span>
          </span>
        </td>
        <td data-cell="creators">
          <span v-for="(d, index) in m.Creators" :key="index"
            >{{ d.FirstName }} {{ d.LastName
            }}<span v-if="index !== m.Creators.length - 1">, </span>
          </span>
        </td>
        <td data-cell="total seasons">{{ m.TotalSeason }}</td>
        <td data-cell="total episodes">{{ m.TotalEpisode }}</td>
        <td data-cell="edit">
          <span
            style="cursor: pointer"
            @click="
              () => {
                EditTVShow(m)
              }
            "
            ><font-awesome-icon :icon="['fas', 'edit']"
          /></span>
        </td>
      </tr>
    </tbody>
  </table>
  <button @click="showMore" class="btn" :disabled="disableShowMore">Show more</button>
</template>

<style scoped>
table {
  margin-top: 30px;
}
</style>
