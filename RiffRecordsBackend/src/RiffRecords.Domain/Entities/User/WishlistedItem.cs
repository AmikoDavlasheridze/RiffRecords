using RiffRecords.Domain.Entities.Band;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Domain.Entities.User
{
    public class WishlistedItem
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int VinylId { get; set; }
        public Vinyl Vinyl { get; set; }
    }
}
