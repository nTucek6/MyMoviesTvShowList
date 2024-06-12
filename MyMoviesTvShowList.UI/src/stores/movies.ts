import { API_URLS } from '@/config'
import { defineStore } from 'pinia'
import axios from 'axios'
import { ref } from 'vue'
import type { MoviesListDTO } from '@/app/shared/models/movies-list.model'
import { MoviesDTO } from '@/app/shared/models/movies.model'

export const useMoviesStore = defineStore('MoviesStore', () => {

    const MoviesList = ref<MoviesListDTO[]>()
    const MovieInfo = ref<MoviesDTO>(new MoviesDTO())


    function GetMoviesData()
    {
        return MoviesList.value
    }

    function GetMovie()
    {
      return MovieInfo.value;
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

    async function GetMovieInfo(Id:number) {
      try {
        await axios({
          method: 'get',
          url: API_URLS.GETMOVIEINFO,
          params: {
            Id: Id,
          }
        }).then((response) => {
            MovieInfo.value = response.data
        })
      } catch (error) {
        console.log(error)
      }
    }


    return {GetMoviesList, GetMoviesData, GetMovieInfo,GetMovie}

})