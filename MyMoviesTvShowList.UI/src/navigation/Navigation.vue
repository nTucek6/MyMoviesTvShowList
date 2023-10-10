<script setup lang="ts">
import {ref, computed, onMounted,watch} from 'vue'
import { RouterLink,useRoute  } from 'vue-router'
import { useAuthentication } from '@/stores/Authentication/authentication'
import  useJwt  from 'jwt-decode'

import { library } from '@fortawesome/fontawesome-svg-core';
import { faUser,faSignOut,faList,faFilm,faVideo,faTv } from '@fortawesome/free-solid-svg-icons';

library.add(faUser,faSignOut,faList,faFilm,faVideo,faTv)

class UserClass
{
    Id = 0;
    Username = '';
    Email = '';
    Role = '';
}

const authentication = useAuthentication();

authentication.CheckUserLogin();
const Username = ref("");
const UserRole = ref("");

const userLogIn = computed(() => authentication.userLogIn);

const route = useRoute();
const page = ref()

onMounted(() => {
  CheckUserLogIn();
});

watch(userLogIn, () => {
      CheckUserLogIn();
    });

const CheckUserLogIn = () =>{
  if(userLogIn.value)
   {
    const token = localStorage.getItem('token');
    if(token != null)
     {
      //const user:{Id:number,Username:string,Email:string,Role:string} = useJwt(token);
      const user : UserClass = useJwt(token);
      Username.value = user.Username;
      UserRole.value = user.Role;
     }
    }
}

watch(route, () => {
      if(route.path == "/profile/"+route.params.username)
      {
        page.value = route.params.username+"'s profile";
      }
      else
      {
        page.value = route.name;
      }
      });

</script>

<template>
<header>
      <div>
        <RouterLink to="/" id="app-name"><h1>MyMoviesTvShowList</h1></RouterLink>
       
        <nav class="signup" >
          <div v-if="!userLogIn">
            <RouterLink to="/register" class="btn ">Sign up</RouterLink>
            <RouterLink to="/login" class="btn">Login</RouterLink>
          </div>
           
          <div class="dropdown" v-if="userLogIn && UserRole == 'Admin'">
          <span class="">Admin</span>
          <div class="dropdown-content">
            <RouterLink to="/moviesadmin" class="btn dropdown-item"><font-awesome-icon :icon="['fas', 'film']" /> Movies</RouterLink>
            <RouterLink to="/" class="btn" dropdown-item><font-awesome-icon :icon="['fas', 'tv']" /> Tv Shows</RouterLink>
            <RouterLink to="/viewcrew" class="btn dropdown-item"><font-awesome-icon :icon="['fas', 'video']" /> Film & Show crew</RouterLink>
            <RouterLink to="/" class="btn dropdown-item"><font-awesome-icon icon="fa-solid fa-user" class="icon"/> Users</RouterLink>
          </div>
          </div> 


          <div class="dropdown" v-if="userLogIn">
          <span class="">{{ Username }}</span>
          <div class="dropdown-content">
          <RouterLink :to="`/profile/${Username}`" class="btn dropdown-item"><font-awesome-icon icon="fa-solid fa-user" class="icon"/> Profile</RouterLink>
          <RouterLink to="/topmovies" class="btn dropdown-item"><font-awesome-icon :icon="['fas', 'list']" /> My List</RouterLink>
          <RouterLink to="/" class="btn btn-red dropdown-item" @click="authentication.LogOut()"><font-awesome-icon :icon="['fas', 'sign-out']" class="icon" /> Log Out</RouterLink>
        </div>
        </div>

      </nav>
      </div>

      <nav class="nav">
        <div class="dropdown dropdown-hover">
          <span class="dropdown-link">Movies</span>
        <div class="dropdown-content">
          <RouterLink to="/moviessearch" class="btn">Movies Search</RouterLink>
          <RouterLink to="/topmovies" class="btn">Top movies</RouterLink>
        </div>
        </div>

        <div class="dropdown dropdown-hover">
          <span class="dropdown-link">Community</span>
        <div class="dropdown-content">
          <RouterLink to="/" class="btn">Discussions</RouterLink>
        </div>
        </div>
  
      </nav>
   
      <div class="page-name">
        <h4>{{ page }}</h4>
      </div>
  </header>
</template>

<style scoped>
header
{
  position: relative;
  margin-bottom: 40px;
}
#app-name
{
 text-decoration: none;
 color: black;
}
#app-name > h1
{
  display: inline;
  
}
.signup
{
 display: inline-flex; 
 position: absolute;
 right: 0;
}
.nav
{
  background-color: rgb(0,155,214)
}
.dropdown {
  position: relative;
  display: inline-block;
  cursor: pointer;
  padding: 12px;
  
}
.dropdown-hover:hover
{
  background-color: rgb(0,31,43);
}

.dropdown-link
{
 color: white;
}

.dropdown-item
{
  display: block;
  text-align: left;
}

.dropdown-content {
  display: none;
  position: absolute;
  background-color: #f9f9f9;
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
  padding: 5px ;
  z-index: 1;
}

.dropdown:hover .dropdown-content {
  display: block;
}

.page-name
{
  border: 1px solid black;
  border-top: 0px;
}

.page-name > h4
{
  margin: 0;
  padding: 5px;
}

</style>
