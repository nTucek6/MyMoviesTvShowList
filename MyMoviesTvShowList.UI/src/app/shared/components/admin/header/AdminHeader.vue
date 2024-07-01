<script setup lang="ts">
import { RouterLink } from 'vue-router'
</script>

<template>
  <nav>
    <RouterLink to="/admin/moviesadmin" class="btn dropdown-item"
      ><font-awesome-icon :icon="['fas', 'film']" /> Movies
    </RouterLink>
    <RouterLink to="/admin/tvshowadmin" class="btn dropdown-item"
      ><font-awesome-icon :icon="['fas', 'tv']" /> Tv Shows</RouterLink
    >
    <RouterLink to="/admin/viewcrew" class="btn dropdown-item"
      ><font-awesome-icon :icon="['fas', 'video']" /> Film & Show crew</RouterLink
    >
    <RouterLink to="/admin" class="btn dropdown-item"
      ><font-awesome-icon icon="fa-solid fa-user" class="icon" /> Users
    </RouterLink>
  </nav>
</template>


<style scoped lang="scss">
nav{
  display: flex;
  justify-content: start;

}


</style>
