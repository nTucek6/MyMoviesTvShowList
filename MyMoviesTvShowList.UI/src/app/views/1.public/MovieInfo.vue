<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useRoute } from 'vue-router'
import { useMoviesStore } from '@/stores/movies';
import { MoviesDTO } from '@/app/shared/models/movies.model';

const route = useRoute()

const MoviesApi = useMoviesStore()

const Id = Number(route.params.id)

const Movie = ref<MoviesDTO>(new MoviesDTO())

onMounted(async ()=>{
    await MoviesApi.GetMovieInfo(Id)
    Movie.value = MoviesApi.GetMovie() 
    console.log(Movie.value)
})

</script>

<template>
    <section>
        <table>
            <tbody>
                <tr>
                    <td id="left-side">
                        <div id="movie-img">
                            <img :src="`data:image/jpeg;base64,${Movie.MovieImageData}`" />
                        </div>

                    </td>
                    <td></td>

                </tr>
            </tbody>
        </table>
    </section>
</template>

<style scoped>

#left-side{
    width: 225px;
    padding: 3px;
}

#movie-img > img{
    width: 100%;
}

</style>