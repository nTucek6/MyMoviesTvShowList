import { defineStore } from 'pinia'
import axios from "axios"
import { ref } from 'vue'

export const Authentication = defineStore('Authentication', () => {

  const token = ref("")

  async function Register(Email:string,Username:string ,Password:string) {
    try {
        await axios({
            method: 'post',
            url: 'Authentication/Register',
            data: {
              Email: Email,
              Username:  Username,
              Password: Password,
            }
          })
          .then((response)=>{
            //console.log(response.data)
            token.value = response.data;
          })
        
      }
      catch (error) {
        //alert(error)
        console.log(error)
    }
  }
  async function Login(Email:string ,Password:string) {
    try {
        await axios({
            method: 'post',
            url: 'Authentication/Login',
            data: {
              Email: Email,
              Password: Password,
            }
          })
          .then((response)=>{
            console.log(response.data)
            token.value = response.data;
          })
        
      }
      catch (error) {
        //alert(error)
        console.log(error)
    }
  }

  return { token,Register,Login }
})
