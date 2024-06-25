<script setup lang="ts">
import { ref, onMounted, computed, watch, onBeforeUnmount } from 'vue'
import { RouterLink, useRoute } from 'vue-router'
import { useAuthentication } from '@/stores/admin/authentication'
import { DropdownMenu } from '@/app/shared/models/dropdown-menu'

import ProfileImage from '@/assets/images/profileImg.webp'

const authentication = useAuthentication()
const route = useRoute()

const userLogIn = computed(() => authentication.userLogIn)
const UserData = computed(() => authentication.UserData)

const showMobileMenu = ref(false)

const showProfileMenu = ref(false)
const showListMenu = ref(false)
const showAdminMenu = ref(false)

const dropdownList = ref()
const dropdownProfile = ref()
const dropdownAdmin = ref()

const page = ref()

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

const ToggleMobileMenu = () => {
  showMobileMenu.value = !showMobileMenu.value
}

onMounted(() => {
  authentication.CheckUserLogin()
  document.addEventListener('click', handleClickOutside)
})

onBeforeUnmount(() => {
  document.removeEventListener('click', handleClickOutside)
})

watch(route, () => {
  if (route.path == '/profile/' + route.params.username) {
    page.value = route.params.username + "'s profile"
  } else if (route.path == `/movie/${route.params.id}/${route.params.title}`) {
    page.value = route.params.title
  } else {
    page.value = route.name
  }
  showListMenu.value = false
  showProfileMenu.value = false
  showAdminMenu.value = false
})

const dropdownMenu: DropdownMenu[] = [
  {
    name: 'Movies',
    children: [
      {
        name: 'Movies Search',
        route: '/moviessearch'
      },
      {
        name: 'Top Movies',
        route: 'topmovies'
      }
    ]
  },
  {
    name: 'Communitiy',
    children: []
  }
]
</script>

<template>
  <header>
    <div id="top-nav">
      <div id="site-name">
        <RouterLink to="/"
          ><font-awesome-icon icon="film" />W<font-awesome-icon icon="shapes" />tchBuddy</RouterLink
        >
        <div id="mobile-view-icon" @click="ToggleMobileMenu()">
          <font-awesome-icon icon="bars" />
        </div>
      </div>

      <div id="auth" :class="{ open: showMobileMenu }">
        <div class="auth-menu" v-if="!userLogIn">
          <RouterLink to="/login" id="login">Login</RouterLink>
          <RouterLink to="/register" id="register">Signup</RouterLink>
        </div>

        <div class="auth-menu" v-else>
          <div
            id="admin-dropdown"
            ref="dropdownAdmin"
            class="dropdown-top"
            :class="{ 'dropdown-admin-open': showAdminMenu }"
            v-if="UserData.Role == 'Admin'"
          >
            <span @click="showAdminMenu = !showAdminMenu">Admin</span>
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
            id="user-list-dropdown"
            ref="dropdownList"
            class="dropdown-top"
            :class="{ 'dropdown-list-open': showListMenu }"
          >
            <span @click="showListMenu = !showListMenu">
              <font-awesome-icon id="l-icon" icon="list"
            /></span>

            <div class="dropdown-content">
              <RouterLink :to="`/movielist/${UserData?.Username}`" class="btn dropdown-item"
                ><font-awesome-icon icon="fa-solid fa-user" class="icon" /> Movies List</RouterLink
              >
            </div>
          </div>
          <div
            id="profile-dropdown"
            ref="dropdownProfile"
            class="dropdown-top"
            :class="{ 'dropdown-profile-open': showProfileMenu }"
          >
            <span id="profile-btn" @click="showProfileMenu = !showProfileMenu"
              >{{ UserData.Username }}
              <font-awesome-icon id="c-icon" icon="caret-down" />
              <img :src="ProfileImage" />
            </span>

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
        </div>
      </div>
    </div>

    <div id="bottom-nav" :class="{ open: showMobileMenu }">
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
    </div>
    <div id="page-name">
      <h3>{{ page }}</h3>
    </div>
  </header>
</template>

<style scoped lang="scss">
$theme_color: rgb(0, 155, 214);
$app_name_color: black;

header {
  display: block;
  padding: 10px 0px 2px;
}

a {
  text-decoration: none;
  color: #000;
}

/* --------------------------------Top navigation--------------------------------------- */

#top-nav {
  display: flex;
  justify-content: space-between;
  height: 34px;
}

#site-name > a {
  color: $app_name_color;
  font-size: 30px;
}

#site-name > a > svg {
  color: red;
}

#site-name > a > svg:first-child {
  margin-right: 5px;
}

#site-name > a > svg:last-child {
  margin: 0 2px;
  font-size: 20px;
}


#mobile-view-icon{
  display: none;
}

.auth-menu {
  display: flex;
  align-items: center;
  flex-direction: row;
  height: 100%;
}

.auth-menu div {
  margin-right: 10px;
}

.auth-menu div:last-of-type {
  margin-right: 0;
}

.auth-menu div span {
  display: flex;
  align-items: center;
  height: 100%;
}

#login,
#register {
  border-radius: 5px;
  padding: 5px 8px;
}

#login {
  border: 1px solid green;
  margin-right: 2px;
}
#register {
  border: 1px solid red;
}

#profile-btn {
  display: flex;
  flex-direction: row;
  align-items: center;
}

#profile-btn > #c-icon {
  margin-left: 4px;
  margin-right: 5px;
}

#profile-btn > img {
  height: 34px;
}

/* ------------------------------------------------------------------------------------------------ */

/* --------------------------------Bottom navigation--------------------------------------- */

#bottom-nav {
  display: flex;
}

#site-pages {
  background-color: $theme_color;
  height: 34px;
  width: 100%;
}

#site-pages ul,
#site-pages ul li {
  height: 100%;
}

#site-pages ul > li > span {
  font-size: 16px;
  font-weight: 700;
  color: black;
  display: flex;
  align-items: center;
  height: 100%;
  padding: 5px;
}

#site-pages ul > li {
  margin-right: 2px;
}

#site-pages ul > li:last-of-type {
  margin-right: 0;
}

#site-pages ul > li:hover {
  cursor: pointer;
  background-color: rgb(146, 213, 255);
}

/* ------------------------------------------------------------------------------------------------ */

/* --------------------------------Dropdown --------------------------------------------- */

.dropdown-top {
  position: relative;
  display: inline-block;
  cursor: pointer;
  color: $app_name_color;
  height: 100%;
}

.dropdown-content {
  visibility: hidden;
  position: absolute;
  background-color: white;
  color: black;
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
  z-index: 1;

  transform-origin: top;
  transition: 0.2s ease-out;
  transform: scaleY(0);
  opacity: 0;
}

.dropdown-profile-open .dropdown-content,
.dropdown-list-open .dropdown-content,
.dropdown-admin-open .dropdown-content {
  visibility: visible;
  transform: scaleY(1);
  opacity: 1;
}

.dropdown {
  position: relative;
  display: inline-block;
  cursor: pointer;

  color: $app_name_color;
  align-items: center;
}

.dropdown:hover .dropdown-content {
  visibility: visible;
  transform: scaleY(1);
  opacity: 1;
}

/* ------------------------------------------------------------------------------------------------ */

/* -----------------------------------Page title------------------------------------------- */

#page-name {
  border-bottom: 2px solid grey;
  border-top: 0px;
  display: block;
}

#page-name > h3 {
  margin: 0;
  padding: 5px;
  color: black;
}

/* ------------------------------------------------------------------------------------------------ */

/* -----------------------------------Responsive------------------------------------------- */

@media screen and (max-width: 768px) {
  header > div {
    flex-direction: column;
  }

  .auth-menu {
    flex-direction: column-reverse;
    align-items: start;
  }

  #site-pages ul > li {
    display: flex;
    flex-direction: column;
  }

  #top-nav {
    display: flex;
    height: auto;
  }

  #bottom-nav {
    display: flex;
  }

  #site-name {
    display: flex;
    justify-content: space-between;
  }

  #mobile-view-icon{
    display: block;
  }

  #auth, #bottom-nav{
  visibility: hidden;
  position: absolute;
  background-color: white;
  color: black;
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
  z-index: 1;

  transform-origin: top;
  transition: 0.2s ease-out;
  transform: scaleY(0);
  opacity: 0;
}

.open #mobile-view-icon {
  transform: rotate(-90deg);
}

.open #auth, .open #bottom-nav {
  visibility: visible;
  transform: scaleY(1);
  opacity: 1;
  
}

}

/* ----------------------------------------------------------------------------------------- */
</style>
