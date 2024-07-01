import { API_URLS } from '@/config'
import { defineStore } from 'pinia'
import axios from 'axios'
import { ref } from 'vue'
import type { MoviesListDTO } from '@/app/shared/models/movies-list.model'
import { MoviesDTO } from '@/app/shared/models/movies.model'
import { Select } from '@/app/shared/models/select.model'

export const useMoviesStore = defineStore('MoviesStore', () => {
  const MoviesList = ref<MoviesListDTO[]>()
  const MovieInfo = ref<MoviesDTO>(new MoviesDTO())

  const Genres = ref<Select[]>()

  function GetMoviesData() {
    return MoviesList.value
  }

  function GetMovie() {
    return MovieInfo.value
  }

  function getGenres(){
    return Genres.value;
  }

  function resetMovieInfo() {
    MovieInfo.value = new MoviesDTO()
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

  async function GetMovieInfo(Id: number) {
    try {
      await axios({
        method: 'get',
        url: API_URLS.GETMOVIEINFO,
        params: {
          Id: Id
        }
      }).then((response) => {
        MovieInfo.value = response.data
      })
    } catch (error) {
      console.log(error)
    }
  }

  async function GetGenres() {
    try {
      await axios({
        method: 'get',
        url: API_URLS.GETGENRES
      }).then((response) => {
        Genres.value = response.data
      })
    } catch (error) {
      console.log(error)
    }
  }


  return { GetMoviesList, GetMoviesData, GetMovieInfo, GetMovie, resetMovieInfo, GetGenres, getGenres }
})
