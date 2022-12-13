namespace AdventOfCode.Days;

public class Day1 : BaseDay
{
    public override int DayNumber => 1;

    protected override async Task<string> GetPartOne()
    {
        var calories = await BuildElvCaloriesCatalog();
        return calories.Max().ToString();
    }

    protected override async Task<string> GetPartTwo()
    {
        var calories = await BuildElvCaloriesCatalog();
        return calories.OrderByDescending(x => x).Take(3).Sum().ToString();
    }

    private static async Task<List<int>> BuildElvCaloriesCatalog()
    {
        var calories = new List<int>();
        var currentElfCalories = 0;

        await foreach (var line in Utilities.ReadFileLinesAsync(1))
        {
            if (line is not { Length: > 0 })
            {
                calories.Add(currentElfCalories);
                currentElfCalories = 0;
                continue;
            }

            currentElfCalories += int.Parse(line);
        }

        return calories;
    }
}