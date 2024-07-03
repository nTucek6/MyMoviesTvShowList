<script setup lang="ts">
import { ref, watch, onMounted, onBeforeUnmount, computed } from 'vue'
import { useRoute } from 'vue-router'
import { useAuthentication } from '@/stores/admin/authentication'

const authentication = useAuthentication()
const route = useRoute()

const userLogIn = computed(() => authentication.userLogIn)
const UserData = computed(() => authentication.UserData)

const showMobileMenu = ref(false)

const dropdownMenu = ref()

const showProfileMenu = ref(false)
const showListMenu = ref(false)
const showAdminMenu = ref(false)

const page = ref()

const ToggleMobileMenu = () => {
  showMobileMenu.value = !showMobileMenu.value
}

function handleClickOutside(event: any) {
  if (
    dropdownMenu.value &&
    !dropdownMenu.value.contains(event.target) &&
    showMobileMenu.value == true
  ) {
    showMobileMenu.value = false
  }
}

onMounted(() => {
  document.addEventListener('click', handleClickOutside)
  setRoutePageName()
})

onBeforeUnmount(() => {
  document.removeEventListener('click', handleClickOutside)
  console.log('called')
})

watch(route, () => {
  setRoutePageName()
})

function setRoutePageName() {
  if (route.path == '/profile/' + route.params.username) {
    page.value = route.params.username + "'s profile"
  } else if (route.path == `/movie/${route.params.id}/${route.params.title}`) {
    page.value = route.params.title
  } else {
    page.value = route.name
  }
  showMobileMenu.value = false
}
</script>

<template>
  <header>
    <nav>
      <div id="site-name">
        <RouterLink to="/"
          ><font-awesome-icon icon="film" />W<font-awesome-icon icon="shapes" />tchBuddy</RouterLink
        >
      </div>

      <div
        id="mobile-view-icon"
        :class="showMobileMenu ? 'sidebar-menu-open' : 'sidebar-menu-close'"
        ref="dropdownMenu"
      >
        <font-awesome-icon icon="bars" @click="ToggleMobileMenu" />
        <div class="sidebar-content" :class="{ 'sidebar-content-open': showMobileMenu }">
          <div class="auth-menu" v-if="!userLogIn">
            <RouterLink to="/login" id="login">Login</RouterLink>
            <RouterLink to="/register" id="register">Signup</RouterLink>
          </div>
          <div class="auth-menu" v-else>
            <div
              id="profile-dropdown"
              ref="dropdownProfile"
              :class="{ 'dropdown-open': showProfileMenu }"
            >
              <div class="dropdown-link" @click="showProfileMenu = !showProfileMenu">
                <span id="profile-btn"
                  >{{ UserData.Username }}
                  <font-awesome-icon :icon="['fas', 'angle-right']" />
                  <!-- <img :src="ProfileImage" /> -->
                </span>
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
          </div>
        </div>
      </div>
    </nav>

    <div id="page-name">
      <h3>{{ page }}</h3>
    </div>
  </header>
</template>

<style scoped lang="scss">
$theme_color: rgb(0, 155, 214);
$app_name_color: black;

header {
  padding: 10px 0px 2px;
}

a {
  text-decoration: none;
  color: #000;
}

nav {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 5px;
}

#site-name > a {
  color: $app_name_color;
  font-size: 30px;
}

#site-name > a > svg:first-child {
  margin-right: 5px;
}

#site-name > a > svg:last-child {
  margin: 0 2px;
  font-size: 20px;
}

// ------------------------sidebar------------------------------------

.sidebar-content {
  display: none;
  position: absolute;
  background-color: white;
  color: black;
  min-height: 100svh;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
  z-index: 1;
  width: 0;
  height: 100%;
  top: 0;
  right: 0;
  transition: width 0.3s ease-out;
  list-style: none;
  overflow: hidden;

}

.sidebar-content-open {
  width: 80%;
}

.sidebar-menu-open .sidebar-content {
  display: block;
  width: 80%;
}

.sidebar-menu-close .sidebar-content {
  display: block;
  width: 0;
}

// -------------------------------------------------------------------------------

// --------------------------------Dropdown content ------------------------------

.dropdown-link {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 5px 10px;
  background-color: grey;
  margin-bottom: 1px;
  height: 100%;
}

.dropdown-link:last-of-type {
  margin-bottom: 0;
}

.dropdown-content {
  visibility: hidden;
  position: absolute;
  background-color: white;
  color: black;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);

  transform: scaleY(0);
  transform-origin: top;
  transition: 0.3s ease-out;

  display: flex;
  flex-direction: column;
  width: 100%;
  align-items: start;
}

.dropdown-open .dropdown-content {
  visibility: visible;
  transform: scaleY(1);
}

// -------------------------------------------------------------------------------

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

// ---------------------------------------------------------------------------------
</style>
