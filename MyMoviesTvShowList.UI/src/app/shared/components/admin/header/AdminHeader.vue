<script setup lang="ts">
import { RouterLink } from 'vue-router'



</script>

<template>
  <nav>
    <RouterLink to="/admin/moviesadmin" class="btn"
      ><font-awesome-icon :icon="['fas', 'film']" /> Movies
    </RouterLink>
    <RouterLink to="/admin/tvshowadmin" class="btn" 
      ><font-awesome-icon :icon="['fas', 'tv']" /> Tv Shows</RouterLink
    >
    <RouterLink to="/admin/viewcrew" class="btn" 
      ><font-awesome-icon :icon="['fas', 'video']" /> Film & Show crew</RouterLink
    >
    <!-- <RouterLink to="/admin/users" class="btn "
      ><font-awesome-icon icon="fa-solid fa-user" class="icon" /> Users
    </RouterLink> -->
  </nav>
</template>


<style scoped lang="scss">
nav{
  display: flex;
  flex-direction: column;
  width: 15%;
  height: 100svh;
  background-color: #143AA2;
  color: #04060D;
  position: fixed;
  top: 0;
  left: 0;
}

nav > a{
  font-size: 15px;
  font-weight: 700;
  padding:10px;
  width: 100%;

  display: flex;
}

nav > a:hover{
  background-color: azure;
}


.active{
  background-color: burlywood;
}

</style>
