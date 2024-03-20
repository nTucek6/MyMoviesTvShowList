<script setup lang="ts">
import { RouterLink, useRouter } from 'vue-router'
import { ref, onMounted, computed } from 'vue'
import { useCrewsAdmin } from '@/stores/CrewsAdmin/crewsadmin'
import { useGlobalHelper } from '@/stores/globalhelper'

const PeopleList = computed(() => api.PeopleData)

const api = useCrewsAdmin()
const globalhelper = useGlobalHelper()
const router = useRouter()

onMounted(() => {
  api.GetPeople(10, 1, '')
})

const EditPerson = (data: any) => {
  api.setEditPerson(data)
  router.push({ name: 'Add & Edit person' })
}
</script>

<template>
  <div>
    <RouterLink to="/viewcrew" class="btn">View all crew</RouterLink>
    <RouterLink to="/addeditperson" class="btn">Add person</RouterLink>
    <hr />

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
          <td>{{ p.firstName }}</td>
          <td>{{ p.lastName }}</td>
          <td>{{ globalhelper.formatDate(new Date(p.birthDate)) }}</td>
          <td>{{ p.birthPlace }}</td>
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
*{
   color: white;
}

</style>
