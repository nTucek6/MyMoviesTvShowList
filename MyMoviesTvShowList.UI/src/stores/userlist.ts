import { defineStore } from 'pinia'
import axios from 'axios'
import { ref } from 'vue'

import { API_URLS } from '@/config'

export const useUserListStore = defineStore('UserListStore', () => {
  const MoviesList = ref()
  const TVShowList = ref()

  async function GetUserMoviesList(Username: string) {
    try {
      await axios({
        method: 'get',
        url: API_URLS.GETUSERMOVIESLIST,
        params: {
          Username: Username
        }
      }).then((response) => {
        MoviesList.value = response.data
        console.log(response.data)
      })
    } catch (error) {
      console.log(error)
    }
  }

  async function GetUserTVShowList(Username: string) {
    try {
      await axios({
        method: 'get',
        url: API_URLS.GETUSERTVSHOWLIST,
        params: {
          Username: Username
        }
      }).then((response) => {
        TVShowList.value = response.data
      })
    } catch (error) {
      console.log(error)
    }
  }

  return { GetUserMoviesList, GetUserTVShowList, MoviesList, TVShowList }
})
