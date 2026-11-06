using Microsoft.AspNetCore.Mvc;
using RiffRecords.Application.Dtos;
using RiffRecords.Application.Abstractions.serviceAbstractions;

namespace RiffRecords.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BandController : ControllerBase
    {
        private readonly IBandService _bandService;

        public BandController(IBandService bandService)
        {
            _bandService = bandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bands = await _bandService.GetAllBandsAsync();
            return Ok(bands);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var band = await _bandService.GetBandByIdAsync(id);
            if (band == null)
            {
                return NotFound();
            }
            return Ok(band);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateBandDto dto)
        {
            var band = await _bandService.CreateBandAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = band.BandId },
                band);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBandAsync(int id, [FromBody] CreateBandDto dto)
        {
            try
            {
                await _bandService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch(KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBandAsync(int id)
        {
            try
            {
                await _bandService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
