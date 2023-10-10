<script setup lang="ts">
import {RouterLink} from 'vue-router'



</script>

<template>
 <div>
   <RouterLink to="/moviesadmin" class="btn">View all movies</RouterLink>
   <RouterLink to="/addeditmovie" class="btn">Add movie</RouterLink>
   <hr />

   <table class="table">
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


      </tbody>
   </table>


 </div>
</template>