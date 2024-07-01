<script setup lang="ts">
import { RouterView } from 'vue-router'
import AdminHeader from './app/shared/components/admin/header/AdminHeader.vue'
</script>

<template>
  <main>
    <AdminHeader v-if="true" />
    <RouterView />
  </main>
</template>
