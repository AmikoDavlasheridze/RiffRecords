using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RiffRecords.Domain.Entities.Band
{
    public class Band
    {
        public int BandId { get; set; }
        public string BandName { get; private set; } = string.Empty;
        public string Slug { get; private set; } = string.Empty;
        public string Genre { get; private set; } = string.Empty;
        public string BandPicture { get; private set; } = string.Empty;

        public ICollection<Vinyl> Vinyls { get; set; }
        public ICollection<Track> Tracks { get; set; }
        public ICollection<Achievement> Achievements { get; set; }
        public Bio Bio { get; set; }

        public Band(string genre, string bandName, string bandPicture)
        {
            if (string.IsNullOrEmpty(bandName))
                throw new ArgumentException("Name Can Not Be Empty");

            if (string.IsNullOrEmpty(genre))
                throw new ArgumentException("Genre Can Not Be Empty");

            if (string.IsNullOrEmpty(bandPicture))
                throw new ArgumentException("Picture Cant be Empty");


            Genre = genre;
            BandName = bandName;
            BandPicture = bandPicture;
            Slug = GenerateSlug(bandName);        
        }

        private static string GenerateSlug(string bandName)
        {
            return bandName.ToLower().Replace(" ", "-");
        }

        public void UpdateDetails(string bandName, string genre, string bandPicture)
        {
            if (string.IsNullOrEmpty(bandName))
                throw new ArgumentException("Name Can Not Be Empty");

            if (string.IsNullOrEmpty(genre))
                throw new ArgumentException("Genre Can Not Be Empty");

            if (string.IsNullOrEmpty(bandPicture))
                throw new ArgumentException("Picture Cant be Empty");

            BandName = bandName;
            BandPicture = bandPicture;
            Genre = genre;
            Slug = GenerateSlug(bandName);
        }

    }
}
