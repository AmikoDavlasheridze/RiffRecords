using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RiffRecords.Api.Security;
using RiffRecords.Application.Abstractions.serviceAbstractions;
using RiffRecords.Application.Dtos;

namespace RiffRecords.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtTokenService jwtTokenService;

        public UserController(IUserService userService, JwtTokenService jwtTokenService)
        {
            _userService = userService;
            this.jwtTokenService = jwtTokenService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var user = await _userService.CreateNewUserAsync(dto);

            return CreatedAtAction(
                nameof(GetUserById),
                new { id = user.Id},
                user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var user = await _userService.LoginUserAsync(dto);
            if (user == null)
            {
                return Unauthorized("password or username is incorrect");
            }
            var accessToken = jwtTokenService.CreateToken(user);

            return Ok(new
            {
                user.Id,
                user.Email,
                user.Username,
                user.Role,
                accessToken
            });
        }

    }
}
