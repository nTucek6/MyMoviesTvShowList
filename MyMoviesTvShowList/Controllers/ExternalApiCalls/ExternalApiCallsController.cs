using Microsoft.AspNetCore.Mvc;
using Services.ExternalApiCalls;

namespace MyMoviesTvShowList.Controllers.ExternalApiCalls
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExternalApiCallsController : Controller
    {
        private readonly IExternalApiCallsService externalApiCallsService;

        public ExternalApiCallsController(IExternalApiCallsService externalApiCallsService)
        {
            this.externalApiCallsService = externalApiCallsService;
        }


        [HttpGet]
        public async Task<IActionResult> GetMovieFromApi(string Title, string Type)
        {
            var response = await externalApiCallsService.GetMovieFromApi(Title, Type);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetCelebritie(string Fullname)
        {
            var reponse = await externalApiCallsService.GetCelebrity(Fullname);
            return Ok(reponse);
        }

    
    }
}
