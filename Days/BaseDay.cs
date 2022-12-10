namespace AdventOfCode.Days;

public abstract class BaseDay
{
    public abstract string DayNumberDisplay { get; }

    protected abstract Task<string> GetPartOne();
    protected abstract Task<string> GetPartTwo();

    public async Task Execute()
    {
        var partOne = GetPartOne();
        var partTwo = GetPartTwo();

        await Task.WhenAll(partOne, partTwo);

        Console.WriteLine($"\n--------------- Day {DayNumberDisplay} ---------------");
        Console.WriteLine($"Part One: {partOne.Result}");
        Console.WriteLine($"Part Two: {partTwo.Result}\n");
    }
}