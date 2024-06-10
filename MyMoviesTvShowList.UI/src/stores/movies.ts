import { API_URLS } from '@/config'
import { defineStore } from 'pinia'
import axios from 'axios'
import { ref } from 'vue'
import type { MoviesListDTO } from '@/app/shared/models/movies-list.model'

export const useMoviesStore = defineStore('MoviesStore', () => {

    const MoviesList = ref<MoviesListDTO[]>()

    function GetMoviesData()
    {
        return MoviesList.value
    }

    async function GetMoviesList(PostPerPage: number, Page: number, Search: string) {
        try {
            await axios({
              method: 'get',
              url: API_URLS.GETMOVIESLIST,
              params: {
                PostPerPage: PostPerPage,
                Page: Page,
                Search: Search
              }
            }).then((response) => {
                MoviesList.value = response.data
            })
          } catch (error) {
            console.log(error)
          }
    }


    return {GetMoviesList, GetMoviesData}

})