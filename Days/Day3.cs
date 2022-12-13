namespace AdventOfCode.Days;

public class Day3 : BaseDay
{
    public override int DayNumber => 3;

    protected override async Task<string> GetPartOne()
    {
        var compartment1 = new HashSet<char>();
        var compartment2 = new HashSet<char>();
        var sumOfPriorities = 0;

        await foreach (var line in Utilities.ReadFileLinesAsync(3))
        {
            var secondStartIndex = line!.Length / 2;

            for (var i = 0; i < line.Length; i++)
            {
                if (i < secondStartIndex)
                {
                    compartment1.Add(line[i]);
                }
                else
                {
                    compartment2.Add(line[i]);
                }
            }

            sumOfPriorities += compartment1.Intersect(compartment2).Select(GetCharPriority).Sum();
            compartment1.Clear();
            compartment2.Clear();
        }

        return sumOfPriorities.ToString();
    }

    protected override Task<string> GetPartTwo()
    {
        var badgePrioritySum = 0;
        var lines = Utilities.ReadFileLines(3).ToArray();

        for (var i = 0; i < lines.Length; i += 3)
        {
            var badge = lines[i].Intersect(lines[i + 1]).Intersect(lines[i + 2]).First();
            badgePrioritySum += GetCharPriority(badge);
        }

        return Task.FromResult(badgePrioritySum.ToString());
    }

    private static int GetCharPriority(char character)
    {
        const int capitalLetterAsciiDifference = 38;
        const int lowerLetterAsciiDifference = 96;
        return character switch
        {
            >= 'A' and <= 'Z' => character - capitalLetterAsciiDifference,
            >= 'a' and <= 'z' => character - lowerLetterAsciiDifference,
            _ => throw new Exception($"Invalid character to get priority: {character}")
        };
    }
}