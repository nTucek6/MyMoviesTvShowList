import { defineStore } from 'pinia'
import axios from "axios"
import { ref } from 'vue'

export const useMoviesAdminApi = defineStore('moviesadmin', () => {


  const Genres = ref();
  const MovieCrew = ref()

  const EditMovie = ref()

  function setEditMovie(data:any)
  {
    EditMovie.value = data
  }

  function setMovieCrew(data:any)
  {
    MovieCrew.value = data
  }

    async function GetGenres() {
        try {
            await axios({
                method: 'get',
                url: 'MoviesAdmin/GetGenres',
              })
              .then((response)=>{
                Genres.value = response.data
              }) 
          }
          catch (error) {
            //alert(error)
            console.log(error)
        }
    }

    async function GetCrew(Search:string):Promise<any> {
        try {
            await axios({
                method: 'get',
                url: 'MoviesAdmin/GetCrewSelectSearch',
                params:
                {
                    Search: Search
                }
              })
              .then((response)=>{
                MovieCrew.value = response.data
                //return response.data
              })
          }
          catch (error) {
            //alert(error)
            console.log(error)
            return null
        }
    }




    return { GetGenres, GetCrew, Genres, MovieCrew,EditMovie,setEditMovie, setMovieCrew }
 })