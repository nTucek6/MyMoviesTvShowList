using Newtonsoft.Json.Linq;
using Services.CrewsAdmin;
using Services.MoviesAdmin;
using Services.TVShowsAdmin;

namespace Services.ExternalApiCalls
{
    public interface IExternalApiCallsService
    {
        public Task<MoviesDTO> GetMovieFromApi(string Title);
        public Task<TVShowDTO> GetTVShowFromApi(string Title);
        public Task<PersonDTO> GetCelebrity(string Fullname);
        public GenresSelectDTO SearchGenre(string Genre);
        Task<string> GetPersonFromWiki(string personName);

    }
}
