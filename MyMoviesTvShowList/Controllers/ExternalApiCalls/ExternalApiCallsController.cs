﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ExternalApiCalls;

namespace MyMoviesTvShowList.Controllers.ExternalApiCalls
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class ExternalApiCallsController : Controller
    {
        private readonly IExternalApiCallsService externalApiCallsService;

        public ExternalApiCallsController(IExternalApiCallsService externalApiCallsService)
        {
            this.externalApiCallsService = externalApiCallsService;
        }


        [HttpGet]
        public async Task<IActionResult> GetMovieFromApi(string Title)
        {
            var response = await externalApiCallsService.GetMovieFromApi(Title);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetTVShowFromApi(string Title)
        {
            var response = await externalApiCallsService.GetTVShowFromApi(Title);
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetCelebritie(string Fullname)
        {
            var reponse = await externalApiCallsService.GetCelebrity(Fullname);
            return Ok(reponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonFromWiki(string personName)
        {
            var response = await externalApiCallsService.GetPersonFromWiki(personName);
            return Ok(response);
        }



    }
}
