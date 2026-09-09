using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Domain.Entities.Band
{
    public class Band
    {
        public int BandId { get; set; }
        public string BandName { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string BandPicture { get; set; } = string.Empty;

        public ICollection<Vinyl> Vinyls { get; set; }
        public ICollection<Track> Tracks { get; set; }
        public ICollection<Achievement> Achievements { get; set; }
        public Bio Bio { get; set; }
    }
}
