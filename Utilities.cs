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

    public static string ReadAllText(int day)
    {
        return File.ReadAllText($"Inputs/day{day}.txt");
    }
}