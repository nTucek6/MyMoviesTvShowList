
using Services.MoviesAdmin;
using Services.TVShowsAdmin;

namespace Services.MovieTVShowInfo
{
    public interface IMovieTVShowInfoService
    {
        public Task<MoviesDTO> GetMovieInfo(int Id);
        public Task<TVShowDTO> GetTVShowInfo(int Id);
        public Task ChangeMovieListStatus(ChangeWatchStatusDTO statusDTO);
        public Task ChangeTVShowListStatus(ChangeWatchStatusDTO statusDTO);
        public Task<List<WatchStatusSelectDTO>> GetMovieWatchStatus();
        public Task<WatchStatusSelectDTO> CheckUserMovieStatus(int UserId, int MovieId);
        public Task<WatchStatusSelectDTO> CheckUserTVShowStatus(int UserId, int TVShowId);

    }
}
