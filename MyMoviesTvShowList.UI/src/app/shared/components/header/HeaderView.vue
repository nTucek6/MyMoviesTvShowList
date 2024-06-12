<script setup lang="ts">
import { ref, computed, onMounted, watch, onBeforeUnmount } from 'vue'
import { RouterLink, useRoute } from 'vue-router'
import { useAuthentication } from '@/stores/admin/authentication'
import { dropdownMenu } from './menu-items'

import ProfileImage from '@/assets/images/profileImg.webp'

const authentication = useAuthentication()

authentication.CheckUserLogin()

const userLogIn = computed(() => authentication.userLogIn)
const UserData = computed(() => authentication.UserData)

const route = useRoute()
const page = ref()

const showMobileMenu = ref(false)

const showProfileMenu = ref(false)
const showListMenu = ref(false)
const showAdminMenu = ref(false)

const dropdownList = ref()
const dropdownProfile = ref()
const dropdownAdmin = ref()

function handleClickOutside(event: any) {
  if (
    dropdownList.value &&
    !dropdownList.value.contains(event.target) &&
    showListMenu.value == true
  ) {
    showListMenu.value = false
  }
  if (
    dropdownProfile.value &&
    !dropdownProfile.value.contains(event.target) &&
    showProfileMenu.value == true
  ) {
    showProfileMenu.value = false
  }
  if (
    dropdownAdmin.value &&
    !dropdownAdmin.value.contains(event.target) &&
    showAdminMenu.value == true
  ) {
    showAdminMenu.value = false
  }
}

onMounted(() => {
  document.addEventListener('click', handleClickOutside)
})

const ToggleMobileMenu = () => {
  showMobileMenu.value = !showMobileMenu.value
}

onBeforeUnmount(() => {
  document.removeEventListener('click', handleClickOutside)
})

watch(route, () => {
  if (showMobileMenu.value) {
    ToggleMobileMenu()
  }
  if (route.path == '/profile/' + route.params.username) {
    page.value = route.params.username + "'s profile"
  }
  else if(route.path == `/movie/${route.params.id}/${route.params.title}`)
  {
    page.value = route.params.title
  }
  else {
    page.value = route.name
  }
  showListMenu.value = false
  showProfileMenu.value = false
  showAdminMenu.value = false
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
            <li id="signup"><RouterLink to="/register">Signup</RouterLink></li>
          </ul>

          <div
            id="dropdown-admin"
            class="dropdown-top"
            ref="dropdownAdmin"
            v-if="userLogIn && UserData.Role == 'Admin'"
            :class="{ 'dropdown-admin-open': showAdminMenu }"
          >
            <div @click="showAdminMenu = !showAdminMenu">
              <span class="" >Admin</span>
            </div>

            <div class="dropdown-content">
              <RouterLink to="/moviesadmin" class="btn dropdown-item"
                ><font-awesome-icon :icon="['fas', 'film']" /> Movies
              </RouterLink>
              <RouterLink to="/tvshowadmin" class="btn dropdown-item"
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

          <div
            ref="dropdownList"
            id="dropdown-list"
            class="dropdown-top"
            v-if="userLogIn"
            :class="{ 'dropdown-list-open': showListMenu }"
          >
            <div @click="showListMenu = !showListMenu">
              <font-awesome-icon id="f-icon" icon="list" />
            </div>

            <div class="dropdown-content dropdown-content-list">
              <RouterLink :to="`/movielist/${UserData?.Username}`" class="btn dropdown-item"
                ><font-awesome-icon icon="fa-solid fa-user" class="icon" /> Movies List</RouterLink
              >
            </div>
          </div>

          <div
            ref="dropdownProfile"
            id="dropdown-profile"
            class="dropdown-top"
            v-if="userLogIn"
            :class="{ 'dropdown-profile-open': showProfileMenu }"
          >
            <div class="dropdown-user" @click="showProfileMenu = !showProfileMenu">
              <span>{{ UserData.Username }} </span>
              <font-awesome-icon id="f-icon" icon="caret-down" />
              <img :src="ProfileImage" />
            </div>

            <div class="dropdown-content">
              <RouterLink :to="`/profile/${UserData?.Username}`" class="btn dropdown-item"
                ><font-awesome-icon icon="fa-solid fa-user" class="icon" /> Profile</RouterLink
              >
              <RouterLink to="/accountsettings" class="btn dropdown-item"
                ><font-awesome-icon :icon="['fas', 'cog']" /> Account settings
              </RouterLink>
              <RouterLink
                to="/"
                class="btn dropdown-item dropdown-item-logout"
                @click="authentication.LogOut()"
                ><font-awesome-icon :icon="['fas', 'sign-out']" class="icon" />Log Out</RouterLink
              >
            </div>
          </div>
        </nav>
      </div>
      <nav id="site-pages">
        <ul>
          <li class="dropdown dropdown-hover" v-for="menu in dropdownMenu" :key="menu.name">
            <span class="dropdown-link">{{ menu.name }}</span>
            <div class="dropdown-content" v-if="menu.children.length > 0">
              <RouterLink
                v-for="submenu in menu.children"
                :key="submenu.name"
                :to="submenu.route"
                class="btn"
                >{{ submenu.name }}</RouterLink
              >
            </div>
          </li>
        </ul>
      </nav>
      <div class="page-name">
        <h3>{{ page }}</h3>
      </div>
    </div>

    <div id="mobile-view" :class="{ open: showMobileMenu }">
      <div id="app-icon" @click="ToggleMobileMenu()">
        <font-awesome-icon icon="bars" />
      </div>

      <div id="mobile-menu">
        <ul v-if="!userLogIn">
          <li id="login-mobile">
            <font-awesome-icon icon="right-to-bracket" /><RouterLink to="/login">Login</RouterLink>
          </li>
          <li id="signup-mobile">
            <font-awesome-icon icon="user-plus" /><RouterLink to="">Signup</RouterLink>
          </li>
        </ul>
        <ul v-if="userLogIn">
          <li class="dropdown-user">
            <img :src="ProfileImage" />
            <span>{{ UserData?.Username }} </span>
          </li>
        </ul>

        <ul id="mobile-site-pages">
          <li>Movies <font-awesome-icon icon="angle-right" /></li>
          <li>Community <font-awesome-icon icon="angle-right" /></li>

          <li v-if="userLogIn">
            <RouterLink to="/" class="" @click="authentication.LogOut()"
              ><font-awesome-icon icon="sign-out" />Log Out</RouterLink
            >
          </li>
        </ul>
      </div>
    </div>
  </header>
  <div id="mobile-page-name">
    <h3>{{ page }}</h3>
  </div>
</template>

<style scoped lang="scss">
$theme_color: rgb(0, 155, 214);

$app_name_color: rgb(0, 0, 0);

$app_background_color: #121212;

$app_text_color: #000000;

$login_btn_color: rgb(40, 111, 134);

$signup_btn_color: #a50000;

header {
  display: block;
  padding: 10px 0px 2px;
}

a {
  text-decoration: none;
  color: $app_text_color;
}

#aplication-name {
  display: flex;
}

#aplication-name > a {
  color: $app_name_color;
  font-size: 30px;
}

#app-logo > svg {
  color: $app_name_color;
}

#app-logo > svg:first-child {
  margin-right: 5px;
  color: $app_name_color;
}

#app-logo > svg:last-child {
  margin: 0 2px;
  font-size: 20px;
}

#top-navigation {
  display: flex;
  justify-content: space-between;
  height: 34px;
  align-items: flex-end;
}

#login,
#signup {
  border-radius: 5px;
  padding: 5px 8px;
}

#login > a,
#signup > a {
  text-decoration: none;
}

#login {
  border: 1px solid $login_btn_color;
}
#signup {
  border: 1px solid $signup_btn_color;
}

#login > a {
  color: $login_btn_color;
}
#signup > a {
  color: $signup_btn_color;
}

#computer-view {
  margin-bottom: 10px;
}

#computer-view ul {
  list-style: none;
  display: flex;
}

#login-nav{
  height: 30px;
}

#login-nav ul > li {
  padding: 5px;
  margin-right: 5px;
}

#login-nav ul > li:last-child {
  margin-right: 0;
}

#site-pages {
  background-color: $theme_color;
  height: 34px;
  text-align: center;
}

#site-pages ul {
  height: 100%;
}

#site-pages ul > li {
  padding: 10px 5px 0;
  font-size: 16px;
  font-weight: 700;
  color: $app_text_color;
}

#site-pages ul > li:hover {
  //transform: scale(1.1);
  //transition: 0.3s;
  cursor: pointer;
  background-color: rgb(2, 35, 56);
}

// -------------------------------------------------------------------------

#mobile-view {
  margin: auto 0;
  display: none;
}

#mobile-view div:first-of-type {
  background-color: $app_background_color;
  margin: 6px 0;
  transition: 0.4s;
  color: $app_name_color;
  font-size: 25px;
  background-color: inherit;
}

.open #app-icon {
  transform: rotate(-90deg);
}

.open #mobile-menu {
  display: block;
}

#mobile-menu {
  display: none;
  position: absolute;
  top: 50px;
  left: 0;
  height: calc(100vh - 50px);
  width: 100%;
  background-color: $app_background_color;
}

#mobile-menu ul {
  display: flex;
  flex-direction: column;
}

#mobile-menu ul li {
  width: 100%;
  color: $app_text_color;
  padding: 10px;
  background-color: grey;
}

#mobile-site-pages li {
  display: flex;
  justify-content: space-between;
}

#mobile-page-name {
  display: none;
  border-bottom: 2px solid grey;
  text-align: center;
}

#mobile-page-name > h3 {
  padding: 5px;
}

#login-mobile {
  background-color: inherit;
}

#signup-mobile {
  background-color: inherit;
}

#login-mobile > svg,
#signup-mobile > svg {
  margin-right: 8px;
  font-size: 16px;
  height: 16px;
  width: 16px;
}

@media screen and (max-width: 768px) {
  header {
    display: flex;
    justify-content: space-between;
    background-color: $theme_color;
    height: 50px;
  }

  #login-nav,
  #site-pages,
  .page-name {
    display: none;
  }

  #mobile-view {
    display: flex;
  }

  #mobile-page-name {
    display: block;
  }

  .dropdown-user img {
    height: 35px !important;
    width: 35px !important;
    margin-right: 8px;
  }
}

.dropdown {
  position: relative;
  display: inline-block;
  cursor: pointer;

  color: $app_name_color;
  align-items: center;
}

.dropdown-user span {
  margin-right: 4px;
}

.dropdown-user {
  display: flex;
  /*align-items: center; */
}

.dropdown-user #f-icon {
  margin-right: 5px;
}

.dropdown-user img {
  height: 100%;
  width: 100%;
  object-fit: contain;
  border: 1px solid $app_name_color;
}

.dropdown-hover:hover {
  background-color: rgb(0, 31, 43);
}

.dropdown-link {
  color: white;
}

.dropdown-item {
  display: block;
  text-align: left;
}

.dropdown-item:hover {
  background-color: lightgrey;
}

.dropdown-item-logout:hover {
  background-color: rgb(255, 0, 0);
}

.dropdown-content {
  display: none;
  position: absolute;
  background-color: white;
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
  z-index: 1;
}

.dropdown-content-list {
  left: 50%;
  transform: translateX(-50%);
}

.dropdown-profile-open .dropdown-content {
  display: block;
}

.dropdown:hover .dropdown-content {
  display: block;
}

.page-name {
  border-bottom: 2px solid grey;
  border-top: 0px;
}

.page-name > h3 {
  margin: 0;
  padding: 5px;
  color: $app_text_color;
}

.dropdown-list-open .dropdown-content {
  display: block;
}

.dropdown-admin-open .dropdown-content {
  display: block;
}

.dropdown-top {
  position: relative;
  display: inline-block; 
  cursor: pointer;
  color: $app_name_color;
  margin-left: 10px;
  height: 100%;
}

.dropdown-top > div:nth-of-type(1){
  height: 100%;
}

</style>
