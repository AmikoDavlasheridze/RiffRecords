using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Application.Dtos
{
    namespace RiffRecords.Application.Dtos
    {
        // Returned to the client
        public record VinylDto(
            int VinylId,
            string AlbumTitle,
            decimal Price,
            int ReleaseYear,
            string Image,
            int Stock,
            int BandId,
            string BandName
        );

        // Accepted from the client when creating
        public record CreateVinylDto(
            string AlbumTitle,
            decimal Price,
            int ReleaseYear,
            string Image,
            int BandId,
            int InitialStock
        );
    }
}
