using Microsoft.AspNetCore.Mvc;
using Services.MoviesAdmin;

namespace MyMoviesTvShowList.Controllers.MoviesAdmin
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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

    }
}
