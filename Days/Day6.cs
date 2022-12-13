namespace AdventOfCode.Days;

public class Day6 : BaseDay
{
    public override int DayNumber => 6;

    protected override Task<string> GetPartOne()
    {
        const int bufferLength = 4;
        var datastream = Utilities.ReadAllText(6);
        return Task.FromResult(IndexOfMarker(datastream, bufferLength));
    }

    protected override Task<string> GetPartTwo()
    {
        const int bufferLength = 14;
        var datastream = Utilities.ReadAllText(6);
        return Task.FromResult(IndexOfMarker(datastream, bufferLength));
    }

    private static string IndexOfMarker(string datastream, int markerLength)
    {
        for (var start = 0; start + markerLength - 1 < datastream.Length; start++)
        {
            if (markerLength == datastream.Skip(start).Take(markerLength).Distinct().Count())
            {
                return (start + markerLength).ToString();
            }
        }

        return "No start-of-signal marker found";
    }
}