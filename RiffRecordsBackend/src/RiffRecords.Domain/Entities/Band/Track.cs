using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Domain.Entities.Band
{
    public class Track
    {
        public int Id { get; set; }
        public string TrackName { get; set; } = string.Empty;
        public int TrackNumber { get; set; }
        public int? DurationSeconds { get; set; }
        public bool IsFamous { get; set; }


        public int VinylId { get; set; }
        public Vinyl Vinyl { get; set; }
    }
}
