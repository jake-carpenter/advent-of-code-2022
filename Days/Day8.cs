namespace AdventOfCode.Days;

public class Day8 : BaseDay
{
    public override int DayNumber => 8;

    protected override Task<string> GetPartOne()
    {
        var trees = ParseTrees();
        var count = 0;

        for (var row = 0; row < trees.GetLength(0); row++)
        {
            for (var col = 0; col < trees.GetLength(1); col++)
            {
                count += IsTreeVisible(trees, row, col) ? 1 : 0;
            }
        }

        return Task.FromResult(count.ToString());
    }

    protected override Task<string> GetPartTwo()
    {
        var trees = ParseTrees();
        var totalRows = trees.GetLength(0);
        var totalCols = trees.GetLength(1);
        var scenicScores = new List<int>();

        for (var row = 0; row < totalRows; row++)
        {
            for (var col = 0; col < totalCols; col++)
            {
                var treeHeight = trees[row, col];
                var above = GetCountOfVisibleTrees(GetTreesAbove(trees, row, col), treeHeight);
                var below = GetCountOfVisibleTrees(GetTreesBelow(trees, row, col), treeHeight);
                var left = GetCountOfVisibleTrees(GetTreesLeft(trees, row, col), treeHeight);
                var right = GetCountOfVisibleTrees(GetTreesRight(trees, row, col), treeHeight);

                scenicScores.Add(above * below * left * right);
            }
        }

        return Task.FromResult(scenicScores.Max().ToString());
    }

    private static int[,] ParseTrees()
    {
        var lines = Utilities.ReadFileLines(8).ToArray();
        var columnCount = lines[0].Length;
        var treeRows = new int[lines.Length, columnCount];

        for (var row = 0; row < lines.Length; row++)
        {
            for (var col = 0; col < lines[row].Length; col++)
            {
                treeRows[row, col] = int.Parse(lines[row][col].ToString());
            }
        }

        return treeRows;
    }

    private static bool IsTreeVisible(int[,] trees, int row, int col)
    {
        var treeHeight = trees[row, col];

        var treesAbove = GetTreesAbove(trees, row, col);
        if (treesAbove.All(x => x < treeHeight))
            return true;

        var treesBelow = GetTreesBelow(trees, row, col);
        if (treesBelow.All(x => x < treeHeight))
            return true;

        var treesLeft = GetTreesLeft(trees, row, col);
        if (treesLeft.All(x => x < treeHeight))
            return true;

        var treesRight = GetTreesRight(trees, row, col);
        if (treesRight.All(x => x < treeHeight))
            return true;

        return false;
    }

    private static IEnumerable<int> GetTreesAbove(int[,] trees, int row, int col)
    {
        for (var currentRow = row - 1; currentRow >= 0; currentRow--)
        {
            yield return trees[currentRow, col];
        }
    }

    private static IEnumerable<int> GetTreesBelow(int[,] trees, int row, int col)
    {
        for (var currentRow = row + 1; currentRow < trees.GetLength(0); currentRow++)
        {
            yield return trees[currentRow, col];
        }
    }

    private static IEnumerable<int> GetTreesLeft(int[,] trees, int row, int col)
    {
        for (var currentCol = col - 1; currentCol >= 0; currentCol--)
        {
            yield return trees[row, currentCol];
        }
    }

    private static IEnumerable<int> GetTreesRight(int[,] trees, int row, int col)
    {
        for (var currentCol = col + 1; currentCol < trees.GetLength(1); currentCol++)
        {
            yield return trees[row, currentCol];
        }
    }

    private static int GetCountOfVisibleTrees(IEnumerable<int> treeline, int height)
    {
        var count = 0;
        var reachedEnd = true;
        using var enumerator = treeline.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (enumerator.Current >= height)
            {
                reachedEnd = false;
                break;
            }

            count++;
        }

        return !reachedEnd ? count + 1 : count;
    }
}