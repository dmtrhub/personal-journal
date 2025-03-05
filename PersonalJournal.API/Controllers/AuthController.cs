using Microsoft.AspNetCore.Mvc;
using PersonalJournal.Application.DTOs;
using PersonalJournal.Application.Interfaces;

namespace PersonalJournal.API.Controllers
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

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
        {
            var user = await _authService.RegisterAsync(dto);
            if (user is null)
                return BadRequest("User already exists.");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto dto)
        {
            var tokens = await _authService.LoginAsync(dto);
            if (tokens is null)
                return BadRequest("Invalid email or password.");

            return Ok(tokens);
        }       
    }
}
