using Microsoft.AspNetCore.Mvc;
using Services.MovieTVShowList;

namespace MyMoviesTvShowList.Controllers.MovieTVShowList
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MovieTVShowListController : Controller
    {
      
        private readonly IMovieTVShowListService movieTVShowListService;

        public MovieTVShowListController(IMovieTVShowListService movieTVShowListService) 
        {
            this.movieTVShowListService = movieTVShowListService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserMovieList(string Username)
        {
            var list = await movieTVShowListService.GetUserMovieList(Username);
            return Ok(list);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserTVShowList(string Username)
        {
            var list = await movieTVShowListService.GetUserTVShowList(Username);
            return Ok(list);
        }


    }
}
