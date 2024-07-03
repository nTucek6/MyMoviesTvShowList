using Microsoft.AspNetCore.Mvc;
using Services.Authentication;

namespace MyMoviesTvShowList.Controllers.Authentication
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDTO user)
        {
            var token = await authenticationService.Login(user);
            return Ok(token);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO user)
        {
            var token = await authenticationService.Register(user);
            return Ok(token);
          
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(AdminUserDTO user)
        {
            var token = await authenticationService.AdminLogin(user);
            return Ok(token);
        }
    }
}
