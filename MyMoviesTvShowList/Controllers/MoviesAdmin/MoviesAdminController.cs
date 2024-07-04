using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.MoviesAdmin;

namespace MyMoviesTvShowList.Controllers.MoviesAdmin
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MoviesAdminController : Controller
    {
        private readonly IMoviesAdminService moviesAdminService;

        public MoviesAdminController(IMoviesAdminService moviesAdminService)
        {
            this.moviesAdminService = moviesAdminService;
        }

       [HttpPost]
       public async Task<IActionResult> UpdateMoviesScore()
        {
            await moviesAdminService.UpdateMoviesScore();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await moviesAdminService.GetGenres();

            return Ok(genres);
        }

        [HttpGet]
        public async Task<IActionResult> GetCrewSelectSearch(string Search)
        {
            var crew = await moviesAdminService.GetCrewSelectSearch(Search);

            return Ok(crew);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMovie([FromForm] SaveMovieDTO movie)
        {
            await moviesAdminService.SaveMovie(movie);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMovies(int PostPerPage, int Page, string? Search)
        {
            var movies = await moviesAdminService.GetMovies(PostPerPage, Page, Search);

            return Ok(movies);
        }

        [HttpGet]
        public async Task<IActionResult> GetMoviesCount()
        {
            int count = await moviesAdminService.GetMoviesCount();
            return Ok(count);
        }

    }
}
