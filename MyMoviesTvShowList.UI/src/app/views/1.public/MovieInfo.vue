<script setup>
import { MoviesDTO } from '@/app/shared/models/movies.model';
import { onMounted, ref } from 'vue';
import { useRoute } from 'vue-router'

const route = useRoute()

const Id = route.params.id

const Movie = ref<MoviesDTO>(new MoviesDTO())

onMounted(()=>{

})

</script>

<template>
    <section>
        
    </section>
</template>