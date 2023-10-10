import { defineStore } from 'pinia'
import axios from "axios"
import { ref } from 'vue'
import router from '@/router'
import  useJwt  from 'jwt-decode'

export const useAuthentication = defineStore('Authentication', () => {

  const userLogIn = ref(false);

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
            localStorage.setItem("token",response.data);
            CheckUserLogin();
            router.push('/');
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
            localStorage.setItem("token",response.data);
            CheckUserLogin();
            router.replace('/');
          })
        
      }
      catch (error) {
        //alert(error)
        console.log(error)
    }
  }

  function LogOut()
  {
    localStorage.removeItem("token");
    CheckUserLogin();
  }


  function CheckUserLogin()
  {
    const token = localStorage.getItem("token");
    if(token != null)
    {
      userLogIn.value = true;
    }
    else
    {
      userLogIn.value = false;
    }
  }

  function IsAdmin()
  {
      const token = localStorage.getItem("token");
      if(token != null)
      {
        const role:{Role:string} = useJwt(token)
        if(role.Role == "Admin")
        {
         return true
        }
      }
      return false;
  }



  return { userLogIn,Register,Login,CheckUserLogin,LogOut,IsAdmin }
})
