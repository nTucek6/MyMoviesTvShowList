<script setup lang="ts">
import { RouterView } from 'vue-router'
import Navigation from './navigation/Navigation/NavigationView.vue'
//import Footer from './navigation/FooterView.vue';
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
  faAngleRight
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
  faAngleRight
)
</script>

<template>
  <main class="container">
    <Navigation />
    <RouterView />
  </main>
</template>
<!--./navigation/NavigationView.vue./navigation/Navigation/NavigationView.vue-->
