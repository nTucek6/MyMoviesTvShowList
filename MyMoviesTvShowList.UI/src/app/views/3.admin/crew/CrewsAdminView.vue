<script setup lang="ts">
import { useRouter } from 'vue-router'
import { onMounted, computed, ref, watch } from 'vue'
import { useCrewsAdmin } from '@/stores/admin/crewsadmin'
import { useGlobalHelper } from '@/stores/globalhelper'
import { crewParams } from '@/app/views/3.admin/crew/crewparams'
import AdminNavigationComponent from '@/app/shared/components/AdminNavigationComponent.vue'

const PeopleList = computed(() => api.PeopleData)

const api = useCrewsAdmin()
const globalhelper = useGlobalHelper()
const router = useRouter()

const page = ref<number>(1)

const search = ref<string>('')

const postPerPage = 10

const disableShowMore = ref(false)

const maxPeopleCount = computed(() => api.PeopleCount)

onMounted(async () => {
  await api.GetPeopleCount()
  await api.GetPeople(postPerPage, page.value, search.value)
  checkPeopleCount()
})

const editPerson = (data: any) => {
  api.setEditPerson(data)
  api.setIsEdit(true)
  router.push({ name: 'Add & Edit person' })
}

const showMore = () => {
  page.value++
  api.GetPeople(postPerPage, page.value, search.value)
}

const checkPeopleCount = () => {
  if (PeopleList.value.length == maxPeopleCount.value) {
    disableShowMore.value = true
  }
}

watch(PeopleList, () => {
  checkPeopleCount()
})
</script>

<template>
  <div>
    <AdminNavigationComponent :routes="crewParams" />

    <table class="table table-striped">
      <thead>
        <tr>
          <th>#</th>
          <th>Name</th>
          <th>Surname</th>
          <th>Birth date</th>
          <th>Birth place</th>
          <th>Edit</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(p, index) in PeopleList" :key="p.Id">
          <td data-cell="#">{{ index + 1 }}</td>
          <td data-cell="name">{{ p.FirstName }}</td>
          <td data-cell="surname">{{ p.LastName }}</td>
          <td data-cell="birth place">{{ globalhelper.formatDate(new Date(p.BirthDate)) }}</td>
          <td data-cell="birth place">{{ p.BirthPlace }}</td>
          <td data-cell="edit">
            <span
              style="cursor: pointer"
              @click="
                () => {
                  editPerson(p)
                }
              "
              ><font-awesome-icon :icon="['fas', 'edit']"
            /></span>
          </td>
        </tr>
      </tbody>
    </table>

    <button @click="showMore" class="btn" :disabled="disableShowMore">Show more</button>
  </div>
</template>

<style scoped>
table {
  margin-top: 30px;
}
</style>
