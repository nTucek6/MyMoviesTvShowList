<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import { useCrewsAdmin } from '@/stores/crewsadmin'
import { useGlobalHelper } from '@/stores/globalhelper'
import AdminNavigationComponent from '@/app/shared/components/AdminNavigationComponent.vue';
import {crewParams} from '@/app/views/3.admin/crew/crewparams'
import { SavePersonDTO } from '@/app/shared/models/save-person.model';

const Person = ref<SavePersonDTO>(new SavePersonDTO())

const ImagePreview = ref()

const api = useCrewsAdmin()
const globalhelper = useGlobalHelper()

const handleImageChange = (event: any) => {
  Person.value.PersonImage = event.target.files[0]

  if (Person.value.PersonImage && Person.value.PersonImage.type.startsWith('image/')) {
    const reader = new FileReader()
    reader.onload = (e: any) => {
      ImagePreview.value = e.target.result
    }
    reader.readAsDataURL(Person.value.PersonImage)
  }
}

onMounted(() => {
  const d = computed(() => api.EditPerson)
  if (d.value != undefined) {
    Person.value.Id = d.value.id
    Person.value.FirstName = d.value.FirstName
    Person.value.LastName = d.value.LastName
    Person.value.BirthPlace = d.value.BirthPlace
    ImagePreview.value = 'data:image/png;base64,' + d.value.PersonImageData

    const date = globalhelper.formatInputDate(new Date(d.value.BirthDate))
    Person.value.BirthDate = date

    api.setEditPerson(undefined)
  }
})

const ClearFormData = () => {
  Person.value = new SavePersonDTO()
  ImagePreview.value = null
}

const addPersonFormSubmit = async () => {
  const p = new FormData()
  p.append('Id', Person.value.Id.toString())
  p.append('FirstName', Person.value.FirstName)
  p.append('LastName', Person.value.LastName)
  p.append('BirthDate', Person.value.BirthDate)
  p.append('BirthPlace', Person.value.BirthPlace)
  p.append('PersonImage', Person.value.PersonImage)

  api.SavePerson(p).then(() => {
    ClearFormData()
  })
}
</script>

<template>
  <div>
    <AdminNavigationComponent :routes="crewParams" />

    <form @submit.prevent="addPersonFormSubmit" class="text-center">
      <div class="form-group mb-3">
        <input type="text" v-model="Person.FirstName" class="w-50" id="name" placeholder="Name" />
      </div>
      <div class="form-group mb-3">
        <input type="text" v-model="Person.LastName" class="w-50" id="lastname" placeholder="Surname" />
      </div>
      <div class="form-group mb-3">
        <input type="date" v-model="Person.BirthDate" class="w-50" id="surname" placeholder="Birth Date" />
      </div>
      <div class="form-group mb-3">
        <input
          type="text"
          v-model="Person.BirthPlace"
          class="w-50"
          id="surname"
          placeholder="Birth Place"
        />
      </div>
      <div class="form-group mb-3">
        <input
          type="file"
          @change="handleImageChange"
          class="w-50"
          id="surname"
          placeholder="Insert person image"
          accept="image/*"
        />
      </div>
      <div class="form-group mb-3">
        <img v-if="ImagePreview" :src="ImagePreview" alt="Image Preview" />
      </div>

      <button type="submit" class="btn w-25">Save person</button>
    </form>
  </div>
</template>

<style scoped>
form{
  margin-top: 30px;
  margin-bottom: 30px
}

</style>
