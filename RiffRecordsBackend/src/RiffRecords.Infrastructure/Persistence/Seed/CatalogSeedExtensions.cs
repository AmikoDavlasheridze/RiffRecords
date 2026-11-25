using System.Reflection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RiffRecords.Domain.Entities.Band;

namespace RiffRecords.Infrastructure.Persistence.Seed
{
    public static class CatalogSeedExtensions
    {
        private const string ResourceName = "RiffRecords.Infrastructure.Persistence.Seed.catalog.json";

        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

        // Imports the bands from the embedded catalog.json. Idempotent: bands whose slug
        // already exists are skipped, so it is safe to run on every startup.
        public static async Task SeedCatalogAsync(this IServiceProvider services, CancellationToken ct = default)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("CatalogSeed");

            await using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceName)
                ?? throw new InvalidOperationException($"Embedded resource '{ResourceName}' was not found.");

            var catalog = await JsonSerializer.DeserializeAsync<CatalogFile>(stream, JsonOptions, ct)
                ?? throw new InvalidOperationException("Catalog seed file is empty.");

            var existingSlugs = (await db.Bands.Select(b => b.Slug).ToListAsync(ct)).ToHashSet();
            var added = 0;

            foreach (var item in catalog.Bands)
            {
                var band = new Band(item.Genre, item.BandName, item.BandImage);
                if (existingSlugs.Contains(band.Slug))
                    continue;

                band.Bio = new Bio { BioText = item.Bio, HistoryText = item.History };

                foreach (var achievement in item.Achievements)
                    band.Achievements.Add(new Achievement { Description = achievement });

                foreach (var v in item.Vinyls)
                {
                    var vinyl = new Vinyl(v.AlbumTitle, v.Price, v.ReleaseYear, v.Image, 0, v.Stock)
                    {
                        Description = v.Description,
                        Label = v.Label,
                        RuntimeSeconds = v.RuntimeSeconds
                    };

                    // AddTrack enforces the one-famous-track-per-album rule.
                    foreach (var t in (v.Tracks ?? []).OrderBy(t => t.Number))
                        vinyl.AddTrack(t.Number, t.Name, t.DurationSeconds, t.IsFamous);

                    band.Vinyls.Add(vinyl);
                }

                db.Bands.Add(band);
                added++;
            }

            if (added > 0)
                await db.SaveChangesAsync(ct);

            logger.LogInformation("Catalog seed: {Added} band(s) added, {Skipped} already present.",
                added, catalog.Bands.Count - added);
        }

        private sealed record CatalogFile(List<CatalogBand> Bands);

        private sealed record CatalogBand(
            string BandName,
            string Genre,
            string BandImage,
            string Bio,
            string History,
            List<string> Achievements,
            List<CatalogVinyl> Vinyls);

        private sealed record CatalogVinyl(
            string AlbumTitle,
            decimal Price,
            int ReleaseYear,
            string Image,
            int Stock,
            string? Description,
            string? Label,
            int? RuntimeSeconds,
            List<CatalogTrack>? Tracks);

        private sealed record CatalogTrack(
            int Number,
            string Name,
            int? DurationSeconds,
            bool IsFamous);
    }
}
