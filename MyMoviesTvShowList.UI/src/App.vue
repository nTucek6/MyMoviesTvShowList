<script setup lang="ts">
import { RouterView } from 'vue-router'
import HeaderView from './app/shared/components/header/HeaderView.vue'
import HeaderNewView from './app/shared/components/header/HeaderNewView.vue'
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
</script>

<template>
  <main class="container">
    <!-- <HeaderView /> -->
    <HeaderNewView />
    <RouterView />
  </main>
</template>