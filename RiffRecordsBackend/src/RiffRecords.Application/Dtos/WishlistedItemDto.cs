using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Application.Dtos
{
    public record WishlistedItemDto(int Id, int UserId, int VinylId);
    public record AddWishlistItemDto(int UserId, int VinylId);
}
