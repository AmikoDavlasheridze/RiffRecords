using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Domain.Entities.Band
{
    public class Achievement
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;

        public int BandId { get; set; }
        public Band Band { get; set; }
    }
}
