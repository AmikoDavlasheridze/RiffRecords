using Microsoft.AspNetCore.Mvc;
using RiffRecords.Application.Abstractions.serviceAbstractions;
using RiffRecords.Application.Dtos;

namespace RiffRecords.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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


    }
}
