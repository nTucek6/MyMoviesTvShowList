namespace Services.MoviesAdmin
{
    public interface IMoviesAdminService
    {
        Task<List<GenresSelectDTO>> GetGenres();
        Task UpdateMoviesScore();
        Task<List<CrewsSelectDTO>> GetCrewSelectSearch(string search);
        Task SaveMovie(SaveMovieDTO movie);
        Task<List<MoviesDTO>> GetMovies(int PostPerPage, int Page, string? Search);

    }
}