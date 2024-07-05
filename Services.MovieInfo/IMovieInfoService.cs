
using Services.MoviesAdmin;

namespace Services.MovieInfo
{
    public interface IMovieInfoService
    {
        public Task<MoviesDTO> GetMovieInfo(int Id);
        public Task ChangeMovieListStatus(ChangeWatchStatusDTO statusDTO);
        Task<List<WatchStatusSelectDTO>> GetMovieWatchStatus();
        Task<WatchStatusSelectDTO> CheckUserMovieStatus(int UserId, int MovieId);

    }
}
