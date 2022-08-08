using Microsoft.AspNetCore.Mvc;
using TwitterWebAPI.Contracts;
using TwitterWebAPI.Dtos;
using TwitterWebAPI.Model;

namespace TwitterWebAPI.Controllers
{
    [Route("tweets")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            this._authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto user)
        {
            var response = await _authRepository.Register(
                new User { Username = user.Username }, user.Password);
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto user)
        {
            var response = await _authRepository.Login(
                user.Username, user.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
