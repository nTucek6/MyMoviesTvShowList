import { defineStore } from 'pinia'
import {ref} from 'vue'
import axios from "axios"

export const GetApiRequest = defineStore('GetApiRequest', () => {
  const message = ref("");

  async function fetchMessage() {
    try {
      const request = await axios.get('MoviesAdmin/GetApiRequest')
        message.value = request.data
      }
      catch (error) {
        //alert(error)
        console.log(error)
    }
  }

  return { message, fetchMessage }
})
