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

    public record CreateBandDto(
        string Genre,
        string BandName,
        string BandPicture        
    );
   
}
