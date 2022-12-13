using System.Diagnostics;

namespace AdventOfCode.Days;

public abstract class BaseDay
{
    public abstract int DayNumber { get; }

    protected abstract Task<string> GetPartOne();
    protected abstract Task<string> GetPartTwo();

    public async Task Execute()
    {
        Console.WriteLine($"\n--------------- Day {DayNumber:00} ---------------");

        var stopwatch = Stopwatch.StartNew();
        var partOne = await GetPartOne();
        stopwatch.Stop();
        Console.WriteLine($"Part One {stopwatch.ElapsedMilliseconds,3}ms:  {partOne}");

        stopwatch.Restart();
        var partTwo = await GetPartTwo();
        stopwatch.Stop();
        Console.WriteLine($"Part Two {stopwatch.Elapsed.Milliseconds,3}ms:  {partTwo}");
    }
}