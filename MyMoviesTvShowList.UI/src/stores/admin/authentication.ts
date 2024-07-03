import { defineStore } from 'pinia'
import axios from 'axios'
import { ref } from 'vue'
import router from '@/router'
import { API_URLS, API_URLS_ADMIN } from '@/config'
import useJwt from 'jwt-decode'
import UserDTO from '@/app/shared/models/user.model'
import { UserRegisterDTO } from '@/app/shared/models/user-register.model'
import UserLoginDTO from '@/app/shared/models/user-login.model'
import AdminLoginDTO from '@/app/shared/models/admin-login.model'
import { ErrorResponse } from '@/app/shared/models/error_response.model'

export const useAuthentication = defineStore('Authentication', () => {
  const userLogIn = ref(false)

  const adminLogIn = ref(false)

  const UserData = ref<UserDTO>(new UserDTO())
  const AdminData = ref<UserDTO>(new UserDTO())

  const errorData = ref<ErrorResponse>(new ErrorResponse())

  async function Register(User: UserRegisterDTO) {
    try {
      await axios({
        method: 'post',
        url: API_URLS.REGISTER,
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
        url: API_URLS.LOGIN,
        data: User
      }).then((response) => {
        localStorage.setItem('token', response.data)
        CheckUserLogin()
        router.replace('/')
      })
    } catch (error: any) {
      errorData.value = error.response.data
    }
  }
  async function AdminLogin(User: AdminLoginDTO) {
    try {
      await axios({
        method: 'post',
        url: API_URLS_ADMIN.ADMINLOGIN,
        data: User
      }).then((response) => {
        sessionStorage.setItem('admintoken', response.data)
        CheckAdminLogin()
        router.replace('/admin')
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
    if (UserData.value.Role == 'Admin') {
      return true
    }
    return false
  }

  function GetUserData() {
    const token = localStorage.getItem('token')
    if (token != null) {
      const user: UserDTO = useJwt(token)
      UserData.value = user
    }
  }

  function GetAdminData() {
    const token = sessionStorage.getItem('admintoken')
    if (token != null) {
      const user: UserDTO = useJwt(token)
      AdminData.value = user
    }
  }
  
  function CheckAdminLogin() {
    const token = sessionStorage.getItem('admintoken')
    if (token != null) {
      adminLogIn.value = true
      GetAdminData()
    } else {
      userLogIn.value = false
    }
  }

  return { userLogIn, adminLogIn, Register, Login, CheckUserLogin, LogOut, IsAdmin, UserData, errorData, AdminLogin,CheckAdminLogin }
})
