using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.MovieTVShowInfo;

namespace MyMoviesTvShowList.Controllers.MovieInfo
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MovieTVShowInfoController : Controller
    {
        private readonly IMovieTVShowInfoService movieInfoService;

        public MovieTVShowInfoController(IMovieTVShowInfoService movieInfoService)
        {
            this.movieInfoService = movieInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovieInfo(int Id)
        {
            var movie = await movieInfoService.GetMovieInfo(Id);
            return Ok(movie);
        }
        [HttpGet]
        public async Task<IActionResult> GetTVShowInfo(int Id)
        {
            var tvshow = await movieInfoService.GetTVShowInfo(Id);
            return Ok(tvshow);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangeMovieListStatus(ChangeWatchStatusDTO statusDTO)
        {
            await movieInfoService.ChangeMovieListStatus(statusDTO);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangeTVShowListStatus(ChangeWatchStatusDTO statusDTO)
        {
            await movieInfoService.ChangeTVShowListStatus(statusDTO);
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

        [HttpGet]
        public async Task<IActionResult> CheckUserTVShowStatus(int UserId, int TVShowId)
        {
            var status = await movieInfoService.CheckUserTVShowStatus(UserId, TVShowId);
            return Ok(status);
        }



    }
}
