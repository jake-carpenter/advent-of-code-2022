using System.Text;

namespace AdventOfCode.Days;

public class Day7 : BaseDay
{
    protected override string DayNumberDisplay => "07";

    protected override async Task<string> GetPartOne()
    {
        const int maxDirectorySize = 100_000;
        var pathSizes = await GetPathSizes();

        return pathSizes.Values.Where(v => v <= maxDirectorySize).Sum().ToString();
    }

    protected override async Task<string> GetPartTwo()
    {
        const int totalDiskSpace = 70_000_000;
        const int updateDiskSpace = 30_000_000;
        const int neededSpace = totalDiskSpace - updateDiskSpace;

        var pathSizes = await GetPathSizes();
        var usedSpace = pathSizes["/"];
        var spaceToFree = usedSpace - neededSpace;

        var sizeToDelete = int.MaxValue;
        foreach (var (_, size) in pathSizes)
        {
            if (size < spaceToFree)
                continue;

            if (size < sizeToDelete)
            {
                sizeToDelete = size;
            }
        }

        return sizeToDelete.ToString();
    }

    private static async Task<Dictionary<string, int>> GetPathSizes()
    {
        var pathSizes = new Dictionary<string, int>();
        var pathStack = new Stack<string>();

        await foreach (var line in Utilities.ReadFileLinesAsync(7))
        {
            if (line is not { Length: > 0 })
                continue;

            var isCommand = line.StartsWith("$");
            var isCdOutCommand = line.StartsWith("$ cd ..");
            var isCdInCommand = !isCdOutCommand && line.StartsWith("$ cd");
            var isDirectory = line.StartsWith("dir");
            var isFile = !isCommand && !isDirectory;

            if (isCdInCommand)
            {
                var directory = line[5..];
                pathStack.Push(directory);

                var fullPath = GetFullPath(pathStack);
                pathSizes.Add(fullPath, 0);
            }
            else if (isCdOutCommand)
            {
                pathStack.Pop();
            }
            else if (isFile)
            {
                var filesize = int.Parse(line.Split(' ')[0]);

                foreach (var fullPath in GetAllAncestors(pathStack))
                {
                    pathSizes[fullPath] += filesize;
                }
            }
        }

        return pathSizes;
    }

    private static string GetFullPath(Stack<string> pathStack)
    {
        var stringBuilder = new StringBuilder();

        for (var i = pathStack.Count - 1; i >= 0; i--)
        {
            var directory = pathStack.ElementAt(i);
            if (directory != "/")
            {
                stringBuilder.Append('/');
            }

            stringBuilder.Append(directory);
        }

        return stringBuilder.Replace("//", "/").ToString();
    }

    private static IEnumerable<string> GetAllAncestors(Stack<string> pathStack)
    {
        var stringBuilder = new StringBuilder();
        for (var targetIndex = pathStack.Count - 1; targetIndex >= 0; targetIndex--)
        {
            stringBuilder.Clear();

            for (var pathIndex = pathStack.Count - 1; pathIndex >= targetIndex; pathIndex--)
            {
                var directory = pathStack.ElementAt(pathIndex);
                if (directory != "/")
                {
                    stringBuilder.Append('/');
                }

                stringBuilder.Append(directory);
            }

            yield return stringBuilder.Replace("//", "/").ToString();
        }
    }
}