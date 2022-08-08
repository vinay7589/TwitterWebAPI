using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterWebAPI.Contracts;

namespace TwitterWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService _userService)
        {
            this._userService = _userService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsers();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUserByName(string userName)
        {
            var response = await _userService.SearchUserByName(userName);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
