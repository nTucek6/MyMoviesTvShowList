<script setup lang="ts">
import { RouterLink } from 'vue-router'

</script>

<template>

<header>
    <div class="wrapper">
      
      <div>
        <RouterLink to="/" class="btn"><h1>MyMoviesTvShow</h1></RouterLink>
       
        <div class="buttons">
          <RouterLink to="/login" class="btn">Login</RouterLink>
          <RouterLink to="/register" class="btn ">Register</RouterLink>
        </div>
      </div>

      <nav class="nav">
        <RouterLink to="/moviesadmin" class="btn">Movies Admin</RouterLink>
      </nav>
    </div>
  </header>



</template>

<style scoped>

header
{
  position: relative;
  margin-bottom: 40px;
}

h1
{
  display: inline;
}

.buttons
{
 display: inline-flex;
 position: absolute;
 right: 0;
 
}

.nav
{
  background-color: rgb(185, 207, 255);

}

</style>
