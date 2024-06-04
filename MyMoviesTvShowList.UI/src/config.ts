const API_BASE_URL = import.meta.env.VITE_API_URL

export const API_URLS = {
  BASE: API_BASE_URL,
  LOGIN: `${API_BASE_URL}Authentication/Login`,
  REGISTER: `${API_BASE_URL}Authentication/Register`,
  GETGENRES: `${API_BASE_URL}MoviesAdmin/GetGenres`  
}

export const API_URLS_ADMIN = {
  GETMOVIES : `${API_BASE_URL}MoviesAdmin/GetMovies`,
  GETCREWSELECTSEARCH : `${API_BASE_URL}MoviesAdmin/GetCrewSelectSearch`,
  SAVEMOVIE : `${API_BASE_URL}MoviesAdmin/SaveMovie`,

  GETPEOPLE : `${API_BASE_URL}CrewsAdmin/GetPeople`,
  GETPEOPLECOUNT : `${API_BASE_URL}CrewsAdmin/GetPeopleCount`,
  SAVEPERSON : `${API_BASE_URL}CrewsAdmin/SavePerson`,

  SAVETVSHOW : `${API_BASE_URL}TVShowsAdmin/SaveTVShow`

}
