<script setup lang="ts">
import { useTvShowAdminStore } from '@/stores/tvshowadmin'
import AdminNavigationComponent from '@/app/shared/components/AdminNavigationComponent.vue'
import {tvShowParams} from '@/app/views/3.admin/tvshow/tvshowparams'

const tvshowApi = useTvShowAdminStore()



</script>

<template>
  <AdminNavigationComponent :routes="tvShowParams" />
</template>
