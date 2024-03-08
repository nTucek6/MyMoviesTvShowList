<script setup lang="ts">
import { ref, computed, onMounted, watch } from 'vue'
import { RouterLink, useRoute } from 'vue-router'
import { useAuthentication } from '@/stores/Authentication/authentication'

import ProfileImage from '../../assets/images/profileImg.webp'

const authentication = useAuthentication()

authentication.CheckUserLogin()

const userLogIn = computed(() => authentication.userLogIn);
const UserData =  computed(() => authentication.UserData);

const route = useRoute()
const page = ref()

//const isPhone = ref(false)

const showMobileMenu = ref(false)
const showProfileMenu = ref(false)

onMounted(() => {

})

watch(userLogIn, () => {
  //CheckUserLogIn();
  console.log(userLogIn.value);
})


const ToggleMobileMenu = () => {
  showMobileMenu.value = !showMobileMenu.value
}

watch(route, () => {
  if (showMobileMenu.value) {
    ToggleMobileMenu()
  }
  if (route.path == '/profile/' + route.params.username) {
    page.value = route.params.username + "'s profile"
  } else {
    page.value = route.name
  }
})
</script>

<template>
  <header>
    <div id="computer-view">
      <div id="top-navigation">
        <div id="aplication-name">
          <RouterLink to="/" id="app-logo"
            ><font-awesome-icon icon="film" />W<font-awesome-icon
              icon="shapes"
            />tchBuddy</RouterLink
          >
        </div>
        <nav id="login-nav">
          <ul v-if="!userLogIn">
            <li id="login"><RouterLink to="/login">Login</RouterLink></li>
            <li id="signup"><RouterLink to="">Signup</RouterLink></li>
          </ul>

          <div
            class="dropdown"
            v-if="userLogIn"
            :class="{ 'dropdown-profile-open': showProfileMenu }"
          >
            <div id="dropdown-user" @click="showProfileMenu = !showProfileMenu">
              <span>{{ UserData?.Username }} </span>
              <font-awesome-icon id="f-icon" icon="caret-down" />
              <img :src="ProfileImage" />
            </div>

            <div class="dropdown-content">
              <RouterLink :to="`/profile/${UserData?.Username}`" class="btn dropdown-item"
                ><font-awesome-icon icon="fa-solid fa-user" class="icon" />Profile</RouterLink
              >
              <RouterLink to="/topmovies" class="btn dropdown-item"
                ><font-awesome-icon :icon="['fas', 'list']" /> My List
              </RouterLink>
              <RouterLink to="/" class="btn btn-red dropdown-item" @click="authentication.LogOut()"
                ><font-awesome-icon :icon="['fas', 'sign-out']" class="icon" />Log Out</RouterLink
              >
            </div>
          </div>
        </nav>
      </div>
      <nav id="site-pages">
        <ul>
          <li>Movies</li>
          <li>Community</li>
        </ul>
      </nav>
      <div class="page-name">
        <h4>{{ page }}</h4>
      </div>
    </div>

    <div id="mobile-view" :class="{ open: showMobileMenu }">
      <div id="app-icon" @click="ToggleMobileMenu()">
        <font-awesome-icon icon="bars" />
      </div>

      <ul class="mobile-menu">
        <li>Movies <font-awesome-icon icon="angle-right" /></li>
        <li>Community <font-awesome-icon icon="angle-right" /></li>
        <li id="login-mobile"><RouterLink to="/login">Login</RouterLink></li>
        <li id="signup-mobile"><RouterLink to="">Signup</RouterLink></li>
      </ul>
    </div>
  </header>

  <!-- <header id="header" >
      <div id="top-navigation">
        <RouterLink to="/" id="app-name">
          <h1>MyMoviesTvShowList</h1>
        </RouterLink>

        <nav class="signup">
          <div v-if="!userLogIn">
            <RouterLink to="/register" class="btn">Sign up</RouterLink>
            <RouterLink to="/login" class="btn">Login</RouterLink>
          </div>

          <div class="dropdown" v-if="userLogIn && UserRole == 'Admin'">
            <span class="">Admin</span>
            <div class="dropdown-content">
              <RouterLink to="/moviesadmin" class="btn dropdown-item"
                ><font-awesome-icon :icon="['fas', 'film']" /> Movies
              </RouterLink>
              <RouterLink to="/" class="btn" dropdown-item
                ><font-awesome-icon :icon="['fas', 'tv']" /> Tv Shows</RouterLink
              >
              <RouterLink to="/viewcrew" class="btn dropdown-item"
                ><font-awesome-icon :icon="['fas', 'video']" /> Film & Show crew</RouterLink
              >
              <RouterLink to="/" class="btn dropdown-item"
                ><font-awesome-icon icon="fa-solid fa-user" class="icon" /> Users
              </RouterLink>
            </div>
          </div>

          <div class="dropdown" v-if="userLogIn">
            <span class="">{{ Username }}</span>
            <div class="dropdown-content">
              <RouterLink :to="`/profile/${Username}`" class="btn dropdown-item"
                ><font-awesome-icon icon="fa-solid fa-user" class="icon" /> Profile</RouterLink
              >
              <RouterLink to="/topmovies" class="btn dropdown-item"
                ><font-awesome-icon :icon="['fas', 'list']" /> My List
              </RouterLink>
              <RouterLink to="/" class="btn btn-red dropdown-item" @click="authentication.LogOut()"
                ><font-awesome-icon :icon="['fas', 'sign-out']" class="icon" /> Log Out</RouterLink
              >
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
    </header> -->
</template>

<style scoped>
@import url('./navigation.scss');
</style>@/app/shared/models/user-login.model
