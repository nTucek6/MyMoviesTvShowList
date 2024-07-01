import { defineStore } from 'pinia'
import { ref } from 'vue'
import axios from 'axios'
import { API_URLS_ADMIN } from '@/config'
import type { PersonDTO } from '@/app/shared/models/person.model'

export const useCrewsAdmin = defineStore('CrewsAdmin', () => {
  const PeopleData = ref<PersonDTO[]>([])
  const PeopleCount = ref<number>(0)

  const EditPerson = ref()

  const isEdit = ref(false)

  function setEditPerson(data: any) {
    EditPerson.value = data
  }

  function setIsEdit(data: boolean) {
    isEdit.value = data
  }

  async function GetPeople(PostPerPage: number, Page: number, Search: string) {
    try {
      await axios({
        method: 'get',
        url: API_URLS_ADMIN.GETPEOPLE,
        params: {
          PostPerPage: PostPerPage,
          Page: Page,
          Search: Search
        }
      }).then((response) => {
        const newItems = response.data
        if (Page > 1) {
          PeopleData.value = [...PeopleData.value, ...newItems]
        } else {
          PeopleData.value = newItems
        }
      })
    } catch (error) {
      //alert(error)
      console.log(error)
    }
  }

  async function GetPeopleCount() {
    try {
      await axios({
        method: 'get',
        url: API_URLS_ADMIN.GETPEOPLECOUNT
      }).then((response) => {
        PeopleCount.value = response.data
      })
    } catch (error) {
      console.log(error)
    }
  }

  async function SavePerson(Person: Object) {
    try {
      await axios({
        method: 'post',
        url: API_URLS_ADMIN.SAVEPERSON,
        headers: {
          'Content-Type': 'multipart/form-data'
        },
        data: Person
      }).then((response) => {
        console.log(response.data)
      })
    } catch (error) {
      console.log(error)
    }
  }

  async function GetPersonFromAPI(Query: string) {
    try {
      await axios({
        method: 'get',
        url: API_URLS_ADMIN.GETPERSONFROMAPI,
        params: {
          Fullname: Query
        }
      }).then((response) => {
        EditPerson.value = response.data
      })
    } catch (error) {
      //alert(error)
      console.log(error)
    }
  }

  return {
    SavePerson,
    GetPeople,
    GetPeopleCount,
    PeopleData,
    EditPerson,
    setEditPerson,
    GetPersonFromAPI,
    isEdit,
    setIsEdit,
    PeopleCount
  }
})
