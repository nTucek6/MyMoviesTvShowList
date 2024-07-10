import { API_URLS } from '@/config'
import { defineStore } from 'pinia'
import axios from 'axios'
import { ref } from 'vue'
import { Select } from '@/app/shared/models/select.model'
import { TVShowDTO } from '@/app/shared/models/tvshow.model'
import type { ChangeWatchStatusDTO } from '@/app/shared/models/change-watch-status.model'
import type { MediaListDTO } from '@/app/shared/models/media-list.model'

export const useTVShowStore = defineStore('TVShowStore', () => {
  const TVShowList = ref<MediaListDTO[]>(new Array<MediaListDTO>)
  const TVShowInfo = ref<TVShowDTO>(new TVShowDTO())
  const UserWatchStatus = ref<Select>(new Select())

  const token = localStorage.getItem('token')

  function getTVShowList(){
    return TVShowList.value
  }

  function getTvShow() {
    return TVShowInfo.value
  }

  function resetTVShowInfo() {
    TVShowInfo.value = new TVShowDTO()
  }

  function getUserWatchStatus() {
    return UserWatchStatus.value
  }

  async function GetTVShowList(PostPerPage: number, Page: number, Search: string) {
    try {
      await axios({
        method: 'get',
        url: API_URLS.GETTVSHOWLIST,
        params: {
          PostPerPage: PostPerPage,
          Page: Page,
          Search: Search
        }
      }).then((response) => {
        TVShowList.value = response.data
      })
    } catch (error) {
      console.log(error)
    }
  }

  async function GetTVShowInfo(Id: number) {
    try {
      await axios({
        method: 'get',
        url: API_URLS.GETTVSHOWINFO,
        params: {
          Id: Id
        }
      }).then((response) => {
        TVShowInfo.value = response.data
      })
    } catch (error) {
      console.log(error)
    }
  }

  async function ChangeTVShowListStatus(WatchStatus: ChangeWatchStatusDTO) {
    try {
      await axios({
        method: 'post',
        url: API_URLS.CHANGETVSHOWLISTSTATUS,
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

  async function CheckUserTVShowStatus(UserId: number, TVShowId: number) {
    try {
      await axios({
        method: 'get',
        url: API_URLS.CHECKUSERTVSHOWSTATUS,
        params: {
          UserId: UserId,
          TVShowId: TVShowId
        }
      }).then((response) => {
        if(response.status != 204)
        {
          UserWatchStatus.value = response.data
        }
      })
    } catch (error) {
      console.log(error)
    }
  }

  return {
    getTVShowList,
    GetTVShowList,
    GetTVShowInfo,
    getTvShow,
    resetTVShowInfo,
    getUserWatchStatus,
    ChangeTVShowListStatus,
    CheckUserTVShowStatus,
  }
})
