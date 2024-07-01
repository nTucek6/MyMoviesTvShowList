<script setup lang="ts">
import { ref, onMounted, onBeforeUnmount } from 'vue'
import { RouterView } from 'vue-router'
import HeaderNewView from './app/shared/components/header/HeaderNewView.vue'
import HeaderMobileView from './app/shared/components/header/HeaderMobileView.vue'
import { useAuthentication } from './stores/admin/authentication'

import { library } from '@fortawesome/fontawesome-svg-core'

import {
  faUser,
  faSignOut,
  faList,
  faFilm,
  faVideo,
  faTv,
  faEdit,
  faBars,
  faShapes,
  faCaretDown,
  faAngleRight,
  faRightToBracket,
  faUserPlus,
  faCog
} from '@fortawesome/free-solid-svg-icons'

library.add(
  faUser,
  faSignOut,
  faList,
  faFilm,
  faVideo,
  faTv,
  faEdit,
  faBars,
  faShapes,
  faCaretDown,
  faAngleRight,
  faRightToBracket,
  faUserPlus,
  faCog
)
/*
const authentication = useAuthentication()

const isMobile = ref<boolean>(false)

onMounted(() => {
  authentication.CheckUserLogin()

  checkScreenSize()
  window.addEventListener('resize', checkScreenSize)
})

onBeforeUnmount(() => {
  window.removeEventListener('resize', checkScreenSize)
})

function checkScreenSize() {
  isMobile.value = window.innerWidth < 769
}*/
</script>

<template>
   <RouterView />
  <!-- <main class="container">
    <HeaderNewView v-if="!isMobile" />
    <HeaderMobileView v-else />
    <RouterView />
  </main> -->
</template>
