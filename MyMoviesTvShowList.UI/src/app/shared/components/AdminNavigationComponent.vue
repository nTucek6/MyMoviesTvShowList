<script setup lang="ts">
import { RouterLink } from 'vue-router'
import type { LinkParams } from '../models/link-params'

defineProps<{
  routes: LinkParams[]
}>()
</script>

<template>
  <RouterLink v-for="i in routes" v-bind:key="i.title" :to="i.route" class="btn">{{
    i.title
  }}</RouterLink>
  <hr />
</template>
