using System.Text.RegularExpressions;

namespace AdventOfCode.Days;

public class Day5 : BaseDay
{
    public override string DayNumberDisplay => "05";

    protected override async Task<string> GetPartOne()
    {
        var (stacks, movements) = await ParseFile();

        foreach (var movement in movements)
        {
            var numberToMove = movement[0];
            var source = stacks[movement[1] - 1];
            var destination = stacks[movement[2] - 1];

            foreach (var _ in Enumerable.Range(0, numberToMove))
            {
                var crate = source.Pop();
                destination.Push(crate);
            }
        }

        return string.Join(string.Empty, stacks.Select(stack => stack.Peek().ToString()));
    }

    protected override async Task<string> GetPartTwo()
    {
        var (stacks, movements) = await ParseFile();

        foreach (var movement in movements)
        {
            var numberToMove = movement[0];
            var source = stacks[movement[1] - 1];
            var destination = stacks[movement[2] - 1];
            var groupStack = new Stack<char>();

            foreach (var _ in Enumerable.Range(0, numberToMove)) groupStack.Push(source.Pop());

            while (groupStack.Count > 0) destination.Push(groupStack.Pop());
        }

        return string.Join(string.Empty, stacks.Select(stack => stack.Peek().ToString()));
    }

    private static async Task<RearrangePlan> ParseFile()
    {
        var rows = new List<char[]>();
        var movements = new List<int[]>();

        await foreach (var line in Utilities.ReadFileLinesAsync(5))
        {
            if (line is not { Length: > 0 })
                continue;

            if (line.TrimStart().StartsWith('['))
                rows.Add(ParseStackRow(line).ToArray());
            else if (line.StartsWith("move")) movements.Add(ParseMovementRow(line).ToArray());
        }

        return new RearrangePlan(MapCrateRowsToStacks(rows), movements);
    }

    private static IEnumerable<char> ParseStackRow(string line)
    {
        // The 'diagram' of the stacks is formatted such that the second index
        // of each 4-char group is the 'crate' char.
        return line.Where((_, i) => i % 4 == 1);
    }

    private static IReadOnlyList<Stack<char>> MapCrateRowsToStacks(List<char[]> rows)
    {
        var rowCount = rows.Count;
        var stackCount = rows[0].Length;
        var stacks = new List<Stack<char>>(stackCount);

        // Fill a stack (vertical) with all row (horizontal) values before starting the next stack.
        for (var colIndex = 0; colIndex < stackCount; colIndex++)
        {
            stacks.Add(new Stack<char>());

            for (var rowIndex = rowCount - 1; rowIndex >= 0; rowIndex--)
            {
                var crate = rows[rowIndex][colIndex];
                if (crate == ' ') // Don't need empty spaces on the stack anymore.
                    continue;

                stacks[colIndex].Push(crate);
            }
        }

        return stacks;
    }

    private static IEnumerable<int> ParseMovementRow(string line)
    {
        var regex = new Regex(@"(\d+)");
        return regex.Matches(line).Select(m => int.Parse(m.Value));
    }

    private record RearrangePlan(IReadOnlyList<Stack<char>> Stacks, IReadOnlyList<int[]> Movements);
}