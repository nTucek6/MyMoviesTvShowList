const API_BASE_URL = import.meta.env.VITE_API_URL

export const API_URLS = {
  BASE: API_BASE_URL,
  LOGIN: `${API_BASE_URL}Authentication/Login`,
  REGISTER: `${API_BASE_URL}Authentication/Register`,
  GETGENRES: `${API_BASE_URL}MoviesAdmin/GetGenres`,

  GETMOVIESLIST: `${API_BASE_URL}Frontpage/GetMoviesList`,
  GETMOVIEINFO: `${API_BASE_URL}MovieInfo/GetMovieInfo`,

  GETUSERMOVIESLIST: `${API_BASE_URL}MovieTVShowList/GetUserMovieList`,
  GETUSERTVSHOWLIST: `${API_BASE_URL}MovieTVShowList/GetUserTVShowList`,
  CHANGEMOVIELISTSTATUS: `${API_BASE_URL}MovieInfo/ChangeMovieListStatus`,
  GETMOVIEWATCHSTATUS: `${API_BASE_URL}MovieInfo/GetMovieWatchStatus`,
  CHECKUSERMOVIESTATUS: `${API_BASE_URL}MovieInfo/CheckUserMovieStatus`,
}

export const API_URLS_ADMIN = {
  ADMINLOGIN: `${API_BASE_URL}Authentication/AdminLogin`,

  GETMOVIES: `${API_BASE_URL}MoviesAdmin/GetMovies`,
  GETCREWSELECTSEARCH: `${API_BASE_URL}MoviesAdmin/GetCrewSelectSearch`,
  SAVEMOVIE: `${API_BASE_URL}MoviesAdmin/SaveMovie`,
  GETMOVIEFROMAPI: `${API_BASE_URL}ExternalApiCalls/GetMovieFromApi`,
  GETMOVIECOUNT: `${API_BASE_URL}MoviesAdmin/GetMoviesCount`,

  GETPEOPLE: `${API_BASE_URL}CrewsAdmin/GetPeople`,
  GETPEOPLECOUNT: `${API_BASE_URL}CrewsAdmin/GetPeopleCount`,
  SAVEPERSON: `${API_BASE_URL}CrewsAdmin/SavePerson`,
  GETPERSONFROMAPI: `${API_BASE_URL}ExternalApiCalls/GetCelebritie`,

  SAVETVSHOW: `${API_BASE_URL}TVShowsAdmin/SaveTVShow`,
  GETTVSHOW: `${API_BASE_URL}TVShowsAdmin/GetTVShow`,
  GETTVSHOWFROMAPI: `${API_BASE_URL}ExternalApiCalls/GetTVShowFromApi`,
  GETTVSHOWCOUNT: `${API_BASE_URL}TVShowsAdmin/GetTVShowCount`
}
