using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Application.Dtos
{
    public record BandDto(
        int BandId,
        string BandName,
        string BandPicture,
        string Genre,
        string Slug
    );

    // A vinyl as shown inside its band's details: no BandId/BandName, plus its tracks.
    public record BandVinylDto(
        int VinylId,
        string AlbumTitle,
        decimal Price,
        int ReleaseYear,
        string Image,
        int Stock,
        List<string> Tracks
    );

    // Everything a single band page needs, returned by GET api/band/{id}.
    public record BandDetailsDto(
        int BandId,
        string BandName,
        string BandPicture,
        string Genre,
        string Slug,
        string? Bio,
        string? History,
        List<string> Achievements,
        List<BandVinylDto> Vinyls
    );

    public record CreateBandDto(
        string Genre,
        string BandName,
        string BandPicture        
    );
   
}
