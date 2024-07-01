<script setup lang="ts">
import { ref, onMounted, onBeforeUnmount } from 'vue'
import { RouterView } from 'vue-router'
import HeaderNewView from './app/shared/components/header/HeaderNewView.vue'
import HeaderMobileView from './app/shared/components/header/HeaderMobileView.vue'
import { useAuthentication } from './stores/admin/authentication'

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
}
</script>

<template>
  <main class="container">
    <HeaderNewView v-if="!isMobile" />
    <HeaderMobileView v-else />
    <RouterView />
  </main>
</template>
