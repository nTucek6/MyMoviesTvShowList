<script setup lang="ts">
import { RouterView } from 'vue-router'
import Navigation from './navigation/Navigation.vue';
import Footer from './navigation/Footer.vue';
</script>

<template>
    <main class="container">
        <Navigation />
        <RouterView />

    </main>
</template>