import { defineStore } from 'pinia'
import axios from "axios"
import { ref } from 'vue'
import { MoviesDTO } from '@/app/shared/models/movies.model';
import { API_URLS, API_URLS_ADMIN } from '@/config';

export const useMoviesAdminApi = defineStore('moviesadmin', () => {


  const Genres = ref();
  const MovieCrew = ref()
  const MovieData = ref<MoviesDTO[]>([])

  const EditMovie = ref()

  const isEdit = ref(false)

  function setEditMovie(data:any)
  {
    EditMovie.value = data
  }

  function setMovieCrew(data:any)
  {
    MovieCrew.value = data
  }

  function setIsEdit(data:boolean)
  {
    isEdit.value = data
  }

    async function GetGenres() {
        try {
            await axios({
                method: 'get',
                url: API_URLS.GETGENRES,
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
                url: API_URLS_ADMIN.GETCREWSELECTSEARCH,
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
          url: API_URLS_ADMIN.GETMOVIES,
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
          url: API_URLS_ADMIN.SAVEMOVIE,
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

    async function GetMovieFromAPI(Query:string) {
      try {
        await axios({
          method: 'get',
          url: API_URLS_ADMIN.GETMOVIEFROMAPI,
          params:
          {
            Title:Query,
            Type:"movie"
          }
          
        })
        .then((response)=>{
          EditMovie.value = response.data
        }) 
          
        }
        catch (error) {
          //alert(error)
          console.log(error)
      }
    }

    return { GetGenres, GetCrew, Genres, MovieCrew,EditMovie,setEditMovie, setMovieCrew,SaveMovie,GetMovies,MovieData, GetMovieFromAPI, isEdit, setIsEdit }
 })