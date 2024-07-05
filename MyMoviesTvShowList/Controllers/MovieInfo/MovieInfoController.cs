using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangeMovieListStatus(ChangeWatchStatusDTO statusDTO)
        {
            await movieInfoService.ChangeMovieListStatus(statusDTO);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetMovieWatchStatus()
        {
            var list = await movieInfoService.GetMovieWatchStatus();
            return Ok(list);
        }
        [HttpGet]
        public async Task<IActionResult> CheckUserMovieStatus(int UserId, int MovieId)
        {
            var status = await movieInfoService.CheckUserMovieStatus(UserId, MovieId);
            return Ok(status);
        }



    }
}
