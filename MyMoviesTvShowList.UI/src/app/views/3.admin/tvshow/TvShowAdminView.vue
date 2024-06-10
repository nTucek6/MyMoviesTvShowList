<script setup lang="ts">
import { useTvShowAdminStore } from '@/stores/admin/tvshowadmin'
import AdminNavigationComponent from '@/app/shared/components/AdminNavigationComponent.vue'
import { tvShowParams } from '@/app/views/3.admin/tvshow/tvshowparams'
import { onMounted, computed } from 'vue'
import type { TVShowDTO } from '@/app/shared/models/tvshow.mode'
import router from '@/router'

const tvshowApi = useTvShowAdminStore()

onMounted(() => {
  tvshowApi.GetTVShow(10, 1, '')
})

const TVShowList = computed<TVShowDTO[]>(() => tvshowApi.TVShowData)

const EditTVShow = (data: any) => {
  tvshowApi.setEditTVShow(data)
  tvshowApi.setIsEdit(true)
  router.push({ name: 'Add & Edit TVShow' })
}
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
        <td>{{ m.Title }}</td>
        <td>
          <span v-for="(g, index) in m.Genres" :key="index"
            >{{ g.label }}<span v-if="index !== m.Genres.length - 1">, </span>
          </span>
        </td>
        <td>{{ m.Runtime }}</td>
        <td>
          <span v-for="(a, index) in m.Actors" :key="index"
            >{{ a.FirstName }} {{ a.LastName }}<span v-if="index !== m.Actors.length - 1">, </span>
          </span>
        </td>
        <td>
          <span v-for="(d, index) in m.Creators" :key="index"
            >{{ d.FirstName }} {{ d.LastName
            }}<span v-if="index !== m.Creators.length - 1">, </span>
          </span>
        </td>
        <td>{{ m.TotalSeason }}</td>
        <td>{{ m.TotalEpisode }}</td>
        <td>
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
</template>

<style scoped>
table {
  margin-top: 30px;
}
</style>
