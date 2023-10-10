<script setup lang="ts">
import {RouterLink,useRouter} from 'vue-router'
import {ref, onMounted, computed} from 'vue'
import {useCrewsAdmin} from '@/stores/CrewsAdmin/crewsadmin'

const PeopleList = computed(() => api.PeopleData);

const api = useCrewsAdmin();
const router = useRouter()

onMounted(() => {
  api.GetPeople(10,1,"")
});

const EditPerson = (data:any) =>
{
   //console.log(data);
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
         <tr v-for="p in PeopleList">
            <td>
               <!-- <RouterLink :to="{name: 'Add & Edit person', params: {Person:Person},}">CLIKC ME</RouterLink> -->
               {{ p.firstName }}
            </td>
            <td>{{ p.lastName }}</td>
            <td>{{ p.birthDate }}</td>
            <td>{{ p.birthPlace }}</td>
            <td><button class="btn" @click="()=>{EditPerson(p)}">Edit</button></td>
            
         </tr>
       
   
      </tbody>
   </table>


 </div>
</template>