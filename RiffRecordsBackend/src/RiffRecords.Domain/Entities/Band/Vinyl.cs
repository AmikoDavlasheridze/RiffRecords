using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Domain.Entities.Band
{
    public class Vinyl
    {
        public int VinylId { get; set; }
        public string AlbumTitle { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int ReleaseYear { get; set; }
        public string Image { get; set; } = string.Empty;
        public int Stock { get; private set; }

        public int BandId { get; set; }
        public Band Band { get; set; }
        public ICollection<Track> Tracks { get; set; }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity Must Be Positive");
            if (quantity > Stock)
                throw new ArgumentException("Not Enough Stock");

            Stock -= quantity;
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive");
            Stock += quantity;
        }

        public Vinyl(string albumTitle, decimal price, int releaseYear, string image, int bandId, int initialStock)
        {
            if (initialStock < 0)
                throw new ArgumentException("Quantity must be positive");

            if (price < 0)
                throw new ArgumentException("Price must be positive.");

            Stock = initialStock;
            ReleaseYear = releaseYear;
            Image = image;
            BandId = bandId;
            Price = price;
            AlbumTitle = albumTitle;
        }
    }
}
