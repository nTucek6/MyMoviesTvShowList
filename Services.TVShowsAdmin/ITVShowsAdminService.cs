
namespace Services.TVShowsAdmin
{
    public interface ITVShowsAdminService
    {

        public Task<List<TVShowDTO>> GetTVShows(int PostPerPage, int Page, string? Search);

        public Task SaveTVShow(SaveTVShowDTO saveTVShow);
        public Task<int> GetTVShowCount();

    }
}
