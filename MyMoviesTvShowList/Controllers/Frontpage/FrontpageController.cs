using Microsoft.AspNetCore.Mvc;
using Services.Frontpage;

namespace MyMoviesTvShowList.Controllers.Frontpage
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FrontpageController : Controller
    {

        private readonly IFrontpageService frontpageService;
        public FrontpageController(IFrontpageService frontpageService) 
        {
            this.frontpageService = frontpageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMoviesList(int PostPerPage, int Page, string? Search)
        {
            var movies = await frontpageService.GetMoviesList(PostPerPage, Page, Search);
            return Ok(movies);
        }

        [HttpGet]
        public async Task<IActionResult> GetTVShowList(int PostPerPage, int Page, string? Search)
        {
            var tvshow = await frontpageService.GetTVShowList(PostPerPage, Page, Search);
            return Ok(tvshow);
        }


    }
}
