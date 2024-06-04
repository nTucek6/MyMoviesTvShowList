import { defineStore } from 'pinia'
import {ref} from 'vue'
import axios from "axios"
import { API_URLS_ADMIN } from '@/config'

export const useCrewsAdmin = defineStore('CrewsAdmin', () => {
  
  const PeopleData = ref()
  const PeopleCount = ref()

  const EditPerson = ref()

  function setEditPerson(data:any)
  {
    EditPerson.value = data;
  }

  async function GetPeople(PostPerPage:number,Page:number,Search:string) {
    try {
      await axios({
        method: 'get',
        url: API_URLS_ADMIN.GETPEOPLE,
        params:
        {
          PostPerPage:PostPerPage,
          Page:Page,
          Search:Search
        }
        
      })
      .then((response)=>{
        //console.log(response.data)
        PeopleData.value = response.data
      }) 
        
      }
      catch (error) {
        //alert(error)
        console.log(error)
    }
  }

  async function GetPeopleCount() {
    try {
      await axios({
        method: 'get',
        url: API_URLS_ADMIN.GETPEOPLECOUNT, 
      })
      .then((response)=>{
        console.log(response.data)
        PeopleCount.value = response.data
      })
      }
      catch (error) {
        console.log(error)
    }
  }

  async function SavePerson(Person:Object) {
    try {
      await axios({
        method: 'post',
        url: API_URLS_ADMIN.SAVEPERSON,
        headers: {
          'Content-Type': 'multipart/form-data',
        },
        data: Person
      })
      .then((response)=>{
        console.log(response.data)
      }) 
        
      }
      catch (error) {
        console.log(error)
    }
  }

  return {SavePerson,GetPeople, GetPeopleCount,PeopleData, EditPerson,setEditPerson }
})
