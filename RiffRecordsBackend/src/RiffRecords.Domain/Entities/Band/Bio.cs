using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Domain.Entities.Band
{
    public class Bio
    {
        public int Id { get; set; }
        public string BioText { get; set; } = string.Empty;
        public string HistoryText { get; set; } = string.Empty;

        public int BandId { get; set; }
        public Band Band { get; set; }
    }
}
