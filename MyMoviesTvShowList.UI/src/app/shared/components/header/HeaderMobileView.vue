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
  console.log("called")
})

watch(route, () => {
  setRoutePageName()
})

function setRoutePageName()
{
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
        :class="showMobileMenu ? 'dropdown-menu-open':'dropdown-menu-close'"
        ref="dropdownMenu"
      >
        <font-awesome-icon icon="bars" @click="ToggleMobileMenu" />
        <div class="dropdown-content" :class="{ 'dropdown-content-open': showMobileMenu }"></div>
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

// -------------------------------------------------------------------------------

.dropdown-content {
  visibility: hidden;
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
}

.dropdown-content-open {
  width: 80%;
}

.dropdown-menu-open .dropdown-content {
  visibility: visible;
  width: 80%;
}

.dropdown-menu-close .dropdown-content {
  visibility: visible;
  width: 0;
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
