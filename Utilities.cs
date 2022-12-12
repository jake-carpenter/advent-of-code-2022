namespace AdventOfCode;

public static class Utilities
{
    public static async IAsyncEnumerable<string?> ReadFileLinesAsync(int day)
    {
        using var reader = new StreamReader(File.OpenRead($"Inputs/day{day}.txt"));
        while (!reader.EndOfStream)
        {
            yield return await reader.ReadLineAsync();
        }
    }

    public static IEnumerable<string> ReadFileLines(int day)
    {
        return File.ReadLines($"Inputs/day{day}.txt");
    }

    public static string ReadAllText(int day)
    {
        return File.ReadAllText($"Inputs/day{day}.txt");
    }
}