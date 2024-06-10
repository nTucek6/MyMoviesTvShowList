<script setup lang="ts">
import { useRouter } from 'vue-router'
import { onMounted, computed } from 'vue'
import { useCrewsAdmin } from '@/stores/admin/crewsadmin'
import { useGlobalHelper } from '@/stores/globalhelper'
import { crewParams } from '@/app/views/3.admin/crew/crewparams'
import AdminNavigationComponent from '@/app/shared/components/AdminNavigationComponent.vue'

const PeopleList = computed(() => api.PeopleData)

const api = useCrewsAdmin()
const globalhelper = useGlobalHelper()
const router = useRouter()

onMounted(() => {
  api.GetPeople(15, 1, '')
})

const EditPerson = (data: any) => {
  api.setEditPerson(data)
  api.setIsEdit(true)
  router.push({ name: 'Add & Edit person'})
}
</script>

<template>
  <div>
    <AdminNavigationComponent :routes="crewParams" />

    <table class="table table-striped">
      <thead>
        <tr>
          <th>Name</th>
          <th>Surname</th>
          <th>Birth date</th>
          <th>Birth place</th>
          <th>Edit</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="p in PeopleList" :key="p.Id">
          <td>{{ p.FirstName }}</td>
          <td>{{ p.LastName }}</td>
          <td>{{ globalhelper.formatDate(new Date(p.BirthDate)) }}</td>
          <td>{{ p.BirthPlace }}</td>
          <td>
            <span
              style="cursor: pointer"
              @click="
                () => {
                  EditPerson(p)
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
