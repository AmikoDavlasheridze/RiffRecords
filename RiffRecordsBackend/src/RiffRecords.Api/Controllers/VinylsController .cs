using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RiffRecords.Application.Abstractions.serviceAbstractions;
using RiffRecords.Application.Dtos;

namespace RiffRecords.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VinylsController : ControllerBase
    {
        private readonly IVinylService _vinylService;

        public VinylsController(IVinylService vinylService)
        {
            _vinylService = vinylService;
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vinyl = await _vinylService.GetByIdAsync(id);

            if (vinyl == null) 
                return NotFound();

            return Ok(vinyl);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vinyls = await _vinylService.GetAllAsync();
            return Ok(vinyls);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVinylDto dto)
        {
            var vinyl = await _vinylService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = vinyl.VinylId }, vinyl);
        }

    }
}
