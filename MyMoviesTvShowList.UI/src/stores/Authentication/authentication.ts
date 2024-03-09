import { defineStore } from 'pinia'
import axios from 'axios'
import { ref } from 'vue'
import router from '@/router'
import useJwt from 'jwt-decode'
import { UserDTO } from '@/app/shared/models/user.model'
import { UserRegisterDTO } from '@/app/shared/models/user-register.model'
import type { UserLoginDTO } from '@/app/shared/models/user-login.model'

export const useAuthentication = defineStore('Authentication', () => {
  const userLogIn = ref(false)

  const UserData = ref<UserDTO>(new UserDTO())

  async function Register(User:UserRegisterDTO) {
    try {
      await axios({
        method: 'post',
        url: 'Authentication/Register',
        data: {
          Email: User.Email,
          Username: User.Username,
          Password: User.Password
        }
      }).then((response) => {
        localStorage.setItem('token', response.data)
        CheckUserLogin()
        router.push('/')
      })
    } catch (error) {
      //alert(error)
      console.log(error)
    }
  }
  async function Login(User:UserLoginDTO) {

    console.log(User);

    try {
      await axios({
        method: 'post',
        url: 'Authentication/Login',
        data: User
        /*
        {
          Email: user.Email,
          Password: user.Password
        }*/
      }).then((response) => {
        localStorage.setItem('token', response.data)
        CheckUserLogin()
        GetUserData()
        router.replace('/')
      })
    } catch (error) {
      //alert(error)
      console.log(error)
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

  return { userLogIn, Register, Login, CheckUserLogin, LogOut, IsAdmin, UserData }
})
