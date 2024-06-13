import axios from 'axios'
import { defineStore } from 'pinia'
import { ref } from 'vue'
import { API_URLS_ADMIN } from '@/config'
import { TVShowDTO } from '@/app/shared/models/tvshow.mode'

export const useTvShowAdminStore = defineStore('tvshowsAdminStore', () => {
  const TVShowData = ref<TVShowDTO[]>([])

  const EditTVShow = ref()

  function setEditTVShow(data: any) {
    EditTVShow.value = data
  }

  async function SaveTVShow(TvShow: Object) {
    try {
      await axios({
        method: 'post',
        url: API_URLS_ADMIN.SAVETVSHOW,
        headers: {
          'Content-Type': 'multipart/form-data'
        },
        data: TvShow
      }).then((response) => {
        console.log(response.data)
      })
    } catch (error) {
      console.log(error)
    }
  }

  async function GetTVShow(PostPerPage: number, Page: number, Search: string) {
    try {
      await axios({
        method: 'get',
        url: API_URLS_ADMIN.GETTVSHOW,
        params: {
          PostPerPage: PostPerPage,
          Page: Page,
          Search: Search
        }
      }).then((response) => {
        TVShowData.value = response.data
      })
    } catch (error) {
      console.log(error)
    }
  }

  async function GetTVShowFromAPI(Query: string) {
    try {
      await axios({
        method: 'get',
        url: API_URLS_ADMIN.GETTVSHOWFROMAPI,
        params: {
          Title: Query
        }
      }).then((response) => {
        EditTVShow.value = response.data
      })
    } catch (error) {
      console.log(error)
    }
  }

  return {
    SaveTVShow,
    GetTVShow,
    TVShowData,
    EditTVShow,
    setEditTVShow,
    GetTVShowFromAPI,
  }
})
