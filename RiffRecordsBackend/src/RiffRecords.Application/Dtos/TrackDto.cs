namespace RiffRecords.Application.Dtos
{
    // One row of an album's tracklist. DurationSeconds is null when the length is unknown.
    public record TrackDto(
        int TrackNumber,
        string Name,
        int? DurationSeconds,
        bool IsFamous
    );
}
