import { defineStore } from 'pinia'
import axios from "axios"
import { ref } from 'vue'
//import type { SaveMovieDTO } from '@/app/shared/models/save-movie.model';
import { MoviesDTO } from '@/app/shared/models/movies.model';

export const useMoviesAdminApi = defineStore('moviesadmin', () => {


  const Genres = ref();
  const MovieCrew = ref()
  const MovieData = ref<MoviesDTO[]>([])

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

    async function GetMovies(PostPerPage:number,Page:number,Search:string) {
      try {
        await axios({
          method: 'get',
          url: 'MoviesAdmin/GetMovies',
          params:
          {
            PostPerPage:PostPerPage,
            Page:Page,
            Search:Search
          }
          
        })
        .then((response)=>{
          
          MovieData.value = response.data
        }) 
          
        }
        catch (error) {
          //alert(error)
          console.log(error)
      }
    }


    async function SaveMovie(Movie:Object) {
      try {
        await axios({
          method: 'post',
          url: 'MoviesAdmin/SaveMovie',
          headers: {
            'Content-Type': 'multipart/form-data',
          },
          data: Movie
        })
        .then((response)=>{
          console.log(response.data)
        }) 
        }
        catch (error) {
          //alert(error)
          console.log(error)
      }
    }




    return { GetGenres, GetCrew, Genres, MovieCrew,EditMovie,setEditMovie, setMovieCrew,SaveMovie,GetMovies,MovieData }
 })