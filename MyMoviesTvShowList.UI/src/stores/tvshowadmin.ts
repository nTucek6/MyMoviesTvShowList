import axios from 'axios'
import { defineStore } from 'pinia'
import { ref } from 'vue'
import { API_URLS_ADMIN } from '@/config'

export const useTvShowAdminStore = defineStore('tvshowsAdminStore', () => {
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

  return { SaveTVShow }
})
