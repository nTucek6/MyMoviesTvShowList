using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.CrewsAdmin;

namespace MyMoviesTvShowList.Controllers.CrewsAdmin
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CrewsAdminController : Controller
    {
        private readonly ICrewsAdminService crewsAdminService;

        public CrewsAdminController(ICrewsAdminService crewsAdminService)
        {
            this.crewsAdminService = crewsAdminService;
        }


       [HttpGet]
       public async Task<IActionResult> GetPeople(int PostPerPage, int Page, string? Search)
        {
            var people = await crewsAdminService.GetPeople(PostPerPage,Page,Search);
            return Ok(people);
        }

        [HttpPost]
        public async Task<IActionResult> SavePerson([FromForm]PersonDTO person)
        {
            await crewsAdminService.SavePerson(person);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPeopleCount()
        {
            var count = await crewsAdminService.GetPeopleCount();

            return Ok(count);
        }


    }
}
