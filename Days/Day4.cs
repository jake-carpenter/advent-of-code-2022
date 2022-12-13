namespace AdventOfCode.Days;

public class Day4 : BaseDay
{
    public override int DayNumber => 4;

    protected override async Task<string> GetPartOne()
    {
        var assignments = await MapAssignments();
        var count = 0;

        foreach (var (a, b) in assignments)
        {
            var hashA = a.ToHashSet();
            var hashB = b.ToHashSet();

            if (hashA.All(x => hashB.Contains(x)) || hashB.All(x => hashA.Contains(x)))
            {
                count++;
            }
        }

        return count.ToString();
    }

    protected override async Task<string> GetPartTwo()
    {
        var assignments = await MapAssignments();
        var count = 0;

        foreach (var (a, b) in assignments)
        {
            var hashA = a.ToHashSet();

            if (b.Any(x => hashA.Contains(x)))
            {
                count++;
            }
        }

        return count.ToString();
    }

    private async Task<IEnumerable<ElfPairAssignments>> MapAssignments()
    {
        var assignments = new List<ElfPairAssignments>();

        await foreach (var line in Utilities.ReadFileLinesAsync(4))
        {
            var pairs = line!.Split(',');
            var a = MapRangeToAssignments(pairs[0]);
            var b = MapRangeToAssignments(pairs[1]);
            assignments.Add(new ElfPairAssignments(a.ToList(), b.ToList()));
        }

        return assignments;
    }

    private static IEnumerable<int> MapRangeToAssignments(string input)
    {
        var range = input.Split('-');
        var start = int.Parse(range[0]);
        var end = int.Parse(range[1]);

        for (var i = start; i <= end; i++)
        {
            yield return i;
        }
    }

    private record ElfPairAssignments(List<int> A, List<int> B);
}