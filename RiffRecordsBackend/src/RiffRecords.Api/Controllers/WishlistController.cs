using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiffRecords.Api.Security;
using RiffRecords.Application.Abstractions.serviceAbstractions;
using RiffRecords.Application.Dtos;

namespace RiffRecords.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistedItemService _wishlistService;

        public WishlistController(IWishlistedItemService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMine()
        {
            var items = await _wishlistService.GetAllItemsAsync(User.GetUserId());
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddWishlistItemDto dto)
        {
            try
            {
                var item = await _wishlistService.AddAnItemAsync(User.GetUserId(), dto);
                return CreatedAtAction(nameof(GetMine), item);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                await _wishlistService.RemoveItemAsync(User.GetUserId(), id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
        }
    }
}
