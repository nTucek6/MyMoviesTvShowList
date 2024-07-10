import { API_URLS } from '@/config'
import { defineStore } from 'pinia'
import axios from 'axios'
import { ref } from 'vue'
import type { MediaListDTO } from '@/app/shared/models/media-list.model'
import { MoviesDTO } from '@/app/shared/models/movies.model'
import { Select } from '@/app/shared/models/select.model'
import { ChangeWatchStatusDTO } from '@/app/shared/models/change-watch-status.model'

export const useMoviesStore = defineStore('MoviesStore', () => {
  const MoviesList = ref<MediaListDTO[]>(new Array<MediaListDTO>())
  const MovieInfo = ref<MoviesDTO>(new MoviesDTO())

  const Genres = ref<Select[]>()

  const MovieWatchStatus = ref<Select[]>()

  const UserWatchStatus = ref<Select>(new Select())

  //const IsOnList = ref<boolean>(false)

  const token = localStorage.getItem('token')

  function getMoviesList() {
    return MoviesList.value
  }

  function GetMovie() {
    return MovieInfo.value
  }

  function getGenres() {
    return Genres.value
  }

  function getUserWatchStatus() {
    return UserWatchStatus.value
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
        url: API_URLS.GETGENRES,
        headers: {
          Authorization: `Bearer ${token}`
        }
      }).then((response) => {
        Genres.value = response.data
      })
    } catch (error) {
      console.log(error)
    }
  }

  async function ChangeMovieListStatus(WatchStatus: ChangeWatchStatusDTO) {
    try {
      await axios({
        method: 'post',
        url: API_URLS.CHANGEMOVIELISTSTATUS,
        data: WatchStatus,
        headers: {
          Authorization: `Bearer ${token}`
        }
      }).then((response) => {
        console.log(response)
      })
    } catch (error) {
      console.log(error)
    }
  }

  async function GetMovieWatchStatus() {
    try {
      await axios({
        method: 'get',
        url: API_URLS.GETMOVIEWATCHSTATUS
      }).then((response) => {
        if (response.data != null) {
          MovieWatchStatus.value = response.data
          // IsOnList.value = true
        }
      })
    } catch (error) {
      console.log(error)
    }
  }

  async function CheckUserMovieStatus(UserId: number, MovieId: number) {
    try {
      await axios({
        method: 'get',
        url: API_URLS.CHECKUSERMOVIESTATUS,
        params: {
          UserId: UserId,
          MovieId: MovieId
        }
      }).then((response) => {
        UserWatchStatus.value = response.data
      })
    } catch (error) {
      console.log(error)
    }
  }

  return {
    GetMoviesList,
    getMoviesList,
    GetMovieInfo,
    GetMovie,
    resetMovieInfo,
    GetGenres,
    getGenres,
    Genres,
    ChangeMovieListStatus,
    GetMovieWatchStatus,
    MovieWatchStatus,
    CheckUserMovieStatus,
    getUserWatchStatus
  }
})
