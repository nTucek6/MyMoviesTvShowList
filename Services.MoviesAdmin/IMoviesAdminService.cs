namespace Services.MoviesAdmin
{
    public interface IMoviesAdminService
    {
        Task<List<GenresSelectDTO>> GetGenres();
        Task UpdateMoviesScore();
        Task<List<CrewsSelectDTO>> GetCrewSelectSearch(string search);

    }
}