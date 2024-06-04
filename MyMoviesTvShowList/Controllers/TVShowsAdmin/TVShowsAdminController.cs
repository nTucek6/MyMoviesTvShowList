using Microsoft.AspNetCore.Mvc;
using Services.TVShowsAdmin;

namespace MyMoviesTvShowList.Controllers.TVShowsAdmin
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TVShowsAdminController : Controller
    {
        private readonly ITVShowsAdminService tVShowsAdminService;

        public TVShowsAdminController(ITVShowsAdminService tVShowsAdminService) {
            this.tVShowsAdminService = tVShowsAdminService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveTVShow([FromForm]SaveTVShowDTO saveTVShow)
        {
            await tVShowsAdminService.SaveTVShow(saveTVShow);
            return Ok();
        }



    }
}
