using Services.CrewsAdmin;
using Services.MoviesAdmin;

namespace Services.ExternalApiCalls
{
    public interface IExternalApiCallsService
    {
        public Task<MoviesDTO> GetMovieFromApi(string Title, string Type);
        public Task<PersonDTO> GetCelebrity(string Fullname);

    }
}
