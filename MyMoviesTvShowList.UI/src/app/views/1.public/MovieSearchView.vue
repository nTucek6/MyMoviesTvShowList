<script setup lang="ts">
import {computed, onBeforeMount, ref } from 'vue'
import { useMoviesAdminApi } from '@/stores/moviesadmin'

const MoviesAdminApi = useMoviesAdminApi();

const GetGenres = computed(() => MoviesAdminApi.Genres);

const GridNumber = ref()

onBeforeMount(async () => {
  await MoviesAdminApi.GetGenres(); 

  let gl = GetGenres.value.length
  let number = 0
  if(gl != undefined)
    {
    if((gl % 2) != 0)
    {
        while((gl % 4) != 0)
        {
            gl++
            number++ 
        }
    }
    }
    GridNumber.value = number
})


</script>


<template>
    <div>
      

    <hr />

    <div class="grid-container">
        <div class="grid-item" v-for="g in GetGenres">{{ g.label }}</div>
        <div class="grid-item" v-for="index in GridNumber"></div>
       
    </div>

    </div>
</template>

<style scoped>
.grid-container {
    display: flex;
    flex-wrap: wrap;
}

.grid-item {
    flex: 1 1 25%;
 
  padding: 5px; 
  box-sizing: border-box;
  text-align: center;
}


</style>