import { defineStore } from 'pinia'
import axios from 'axios'
import { ref } from 'vue'
import router from '@/router'
import useJwt from 'jwt-decode'
import { UserDTO } from '@/app/shared/models/user.model'
import { UserRegisterDTO } from '@/app/shared/models/user-register.model'
import type { UserLoginDTO } from '@/app/shared/models/user-login.model'
import { ErrorResponse } from '@/app/shared/models/error_response.model'

export const useAuthentication = defineStore('Authentication', () => {
  const userLogIn = ref(false)

  const UserData = ref<UserDTO>(new UserDTO())

  const errorData = ref<ErrorResponse>(new ErrorResponse())

  async function Register(User: UserRegisterDTO) {
    try {
      await axios({
        method: 'post',
        url: 'Authentication/Register',
        data: User
      }).then((response) => {
        localStorage.setItem('token', response.data)
        CheckUserLogin()
        router.push('/')
      })
    } catch (error: any) {
      errorData.value = error.response.data
      console.log(error)
    }
  }
  async function Login(User: UserLoginDTO) {
    try {
      await axios({
        method: 'post',
        url: 'Authentication/Login',
        data: User
      }).then((response) => {
        localStorage.setItem('token', response.data)
        CheckUserLogin()
        GetUserData()
        router.replace('/')
      })
    } catch (error: any) {
      errorData.value = error.response.data
    }
  }

  function LogOut() {
    localStorage.removeItem('token')
    CheckUserLogin()
  }

  function CheckUserLogin() {
    const token = localStorage.getItem('token')
    if (token != null) {
      userLogIn.value = true
      GetUserData()
    } else {
      userLogIn.value = false
    }
  }

  function IsAdmin() {
    return false
  }

  function GetUserData() {
    const token = localStorage.getItem('token')
    if (token != null) {
      const user: UserDTO = useJwt(token)
      UserData.value = user
    }
  }

  return { userLogIn, Register, Login, CheckUserLogin, LogOut, IsAdmin, UserData, errorData }
})
