namespace AdventOfCode.Days;

public class Day9 : BaseDay
{
    public override int DayNumber => 9;

    protected override async Task<string> GetPartOne()
    {
        var visitedLocations = new HashSet<Point>();
        var map = new Map();

        await foreach (var line in Utilities.ReadFileLinesAsync(9))
        {
            map.MoveHead(line!);
            foreach (var point in map.TailVisitedPointsLastMove)
            {
                if (visitedLocations.Contains(point))
                    continue;

                visitedLocations.Add(point);
            }
        }

        return visitedLocations.Count.ToString();
    }

    protected override Task<string> GetPartTwo()
    {
        return Task.FromResult("not implemented");
    }

    private class Map
    {
        private readonly List<Point> _tailVisitedPointsLastMove = new();

        public Point Head { get; private set; } = new(0, 0);
        public Point Tail { get; private set; } = new(0, 0);

        public IReadOnlyList<Point> TailVisitedPointsLastMove => _tailVisitedPointsLastMove;

        public void MoveHead(string pattern)
        {
            _tailVisitedPointsLastMove.Clear();

            var direction = pattern[0];
            var spaces = int.Parse(pattern[2..]);

            for (var i = 0; i < spaces; i++)
            {
                Head = direction switch
                {
                    'U' => Head with { Y = Head.Y - 1 },
                    'D' => Head with { Y = Head.Y + 1 },
                    'L' => Head with { X = Head.X - 1 },
                    'R' => Head with { X = Head.X + 1 },
                    _ => throw new Exception($"Invalid direction: {direction}")
                };

                Tail = Head.GetFollowPointFrom(Tail);
                _tailVisitedPointsLastMove.Add(Tail);
            }
        }
    }

    private record Point(int X, int Y)
    {
        public Point GetFollowPointFrom(Point other)
        {
            var xDiff = X - other.X;
            var xDiffAbs = Math.Abs(xDiff);
            var yDiff = Y - other.Y;
            var yDiffAbs = Math.Abs(yDiff);
            var leftOrRight = xDiff > 0 ? 1 : -1;
            var upOrDown = yDiff > 0 ? 1 : -1;

            return (xDiffAbs, yDiffAbs) switch
            {
                (0, 0) => other,
                (1, 0) => other,
                (0, 1) => other,
                (1, 1) => other,
                (> 1, 0) => other with { X = other.X + leftOrRight },
                (0, > 1) => other with { Y = other.Y + upOrDown },
                (> 1, 1) => new Point(X: other.X + leftOrRight, Y: other.Y + upOrDown),
                (1, > 1) => new Point(X: other.X + leftOrRight, Y: other.Y + upOrDown),
                _ => throw new Exception("")
            };
        }
    }
}