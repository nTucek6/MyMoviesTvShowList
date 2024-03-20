<script setup lang="ts">
import {RouterLink} from 'vue-router'
import {ref, onMounted, computed} from 'vue'

import {useCrewsAdmin} from '@/stores/CrewsAdmin/crewsadmin'
import { useGlobalHelper } from '@/stores/globalhelper';

const Id = ref(0)
const Name = ref()
const Surname = ref()
const BirthDate = ref()
const BirthPlace = ref()
const Image = ref()
const ImagePreview = ref()


const api = useCrewsAdmin();
const globalhelper = useGlobalHelper()

const handleImageChange = (event:any) => {
      Image.value = event.target.files[0];
      
      if (Image.value && Image.value.type.startsWith('image/')) {
        const reader = new FileReader();
        reader.onload = (e:any) => {
          ImagePreview.value = e.target.result;
        };
        reader.readAsDataURL(Image.value);
      }

    }

onMounted(()=>{
const d =  computed(() => api.EditPerson);
if(d.value != undefined)
{
  Id.value = d.value.id
  Name.value = d.value.firstName
  Surname.value = d.value.lastName
  BirthPlace.value = d.value.birthPlace
  ImagePreview.value = "data:image/png;base64,"+ d.value.personImageData

  const date = globalhelper.formatInputDate(new Date(d.value.birthDate))
  BirthDate.value = date

  api.setEditPerson(undefined)  
}
})

const ClearFormData = () =>{
      Id.value = 0
      Name.value = null
      Surname.value = null
      BirthDate.value = null
      BirthPlace.value = null
      Image.value = null
      ImagePreview.value = null
}

const addPersonFormSubmit = async () =>
{
     const Person = new FormData();
          Person.append("Id", Id.value.toString());
          Person.append("FirstName", Name.value);
          Person.append("LastName", Surname.value);
          Person.append("BirthDate", BirthDate.value);
          Person.append("BirthPlace", BirthPlace.value);
          Person.append("PersonImage", Image.value);

          api.SavePerson(Person).then(()=>{
            ClearFormData()
          });
}
</script>

<template>
 <div>
   <RouterLink to="/viewcrew" class="btn">View all crew</RouterLink>
   <RouterLink to="/addeditperson" class="btn">Add person</RouterLink>
   <hr />

   <form @submit.prevent="addPersonFormSubmit" class="text-center">
    <div class="form-group mb-3">
     <input type="text" v-model="Name" class="w-50" id="name" placeholder="Name" />
    </div>
    <div class="form-group mb-3">
     <input type="text" v-model="Surname" class="w-50" id="surname" placeholder="Surname" />
    </div>
    <div class="form-group mb-3">
     <input type="date" v-model="BirthDate" class="w-50" id="surname" placeholder="Birth Date" />
    </div>
    <div class="form-group mb-3">
     <input type="text" v-model="BirthPlace" class="w-50" id="surname" placeholder="Birth Place" />
    </div>
    <div class="form-group mb-3">
     <input type="file" @change="handleImageChange" class="w-50" id="surname" placeholder="Insert person image" />
     
    </div>
    <div class="form-group mb-3">
      <img v-if="ImagePreview" :src="ImagePreview" alt="Image Preview" />
     
    </div>
  

    <button type="submit" class="btn w-25">Save person</button>
   </form>

 </div>
</template>

<style scoped>
*{
   color: white;
}

input{
  color:black;
}

</style>
