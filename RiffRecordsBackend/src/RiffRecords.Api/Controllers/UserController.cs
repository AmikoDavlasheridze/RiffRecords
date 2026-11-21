using Microsoft.AspNetCore.Authorization;
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
        private readonly JwtTokenService _jwtTokenService;

        public UserController(IUserService userService, JwtTokenService jwtTokenService)
        {
            _userService = userService;
            this._jwtTokenService = jwtTokenService;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var user = await _userService.GetUserByIdAsync(User.GetUserId());
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            try
            {
                var user = await _userService.CreateNewUserAsync(dto);
                return CreatedAtAction(nameof(Me), user);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var user = await _userService.LoginUserAsync(dto);
            if (user == null)
            {
                return Unauthorized("password or email is incorrect");
            }
            var accessToken = _jwtTokenService.CreateToken(user);

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
