using Microsoft.AspNetCore.Mvc;
using Services.MovieInfo;

namespace MyMoviesTvShowList.Controllers.MovieInfo
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MovieInfoController : Controller
    {
        private readonly IMovieInfoService movieInfoService;

        public MovieInfoController(IMovieInfoService movieInfoService)
        {
            this.movieInfoService = movieInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovieInfo(int Id)
        {
            var movie = await movieInfoService.GetMovieInfo(Id);
            return Ok(movie);
        }

    }
}
