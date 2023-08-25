using AspNetCoreApi6.Models;
using AspNetCoreApi6.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApi6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
               _authService = authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(LoginUser user)
        {
            if(await _authService.RegisterUser(user))
            {
                return Ok("Successfully Done");
            }
            return BadRequest("Smthng went wrong");
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUser user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(await _authService.Login(user))
            {
                var tokenString = _authService.GenerateTokenString(user);
                return Ok(tokenString);
            }
            return BadRequest();

        }
    }
}
