namespace AdventOfCode.Days;

public class Day2 : BaseDay
{
    protected override string DayNumberDisplay => "02";

    protected override async Task<string> GetPartOne()
    {
        var totalScore = 0;

        await foreach (var line in Utilities.ReadFileLinesAsync(2))
        {
            var hands = line!.Split(' ');
            var handScore = CalcScoreOfHand(hands[1]);
            var roundScore = hands switch
            {
                ["A", "X"] => 3,
                ["A", "Y"] => 6,
                ["A", "Z"] => 0,
                ["B", "X"] => 0,
                ["B", "Y"] => 3,
                ["B", "Z"] => 6,
                ["C", "X"] => 6,
                ["C", "Y"] => 0,
                ["C", "Z"] => 3,
                _ => throw new Exception($"Invalid hands for calculating round score {hands[0]},{hands[1]}")
            };

            totalScore += handScore + roundScore;
        }

        return totalScore.ToString();
    }

    protected override async Task<string> GetPartTwo()
    {
        var totalScore = 0;

        await foreach (var line in Utilities.ReadFileLinesAsync(2))
        {
            var hands = line!.Split(' ');
            var handScore = hands switch
            {
                ["A", "X"] => 3, // pick Scissors to lose
                ["A", "Y"] => 1, // pick Rock to draw
                ["A", "Z"] => 2, // pick Paper to win
                ["B", "X"] => 1, // pick Rock to lose
                ["B", "Y"] => 2, // pick Paper to draw
                ["B", "Z"] => 3, // pick Scissors to win
                ["C", "X"] => 2, // pick Paper to lose
                ["C", "Y"] => 3, // pick Scissors to draw
                ["C", "Z"] => 1, // pick Rock to win
                _ => throw new Exception($"Invalid input for calculating play {hands[0]},{hands[1]}")
            };

            var roundScore = hands[1] switch
            {
                "X" => 0, // lose
                "Y" => 3, // draw
                "Z" => 6, // win
                _ => throw new Exception($"Invalid hand for calculating round score {hands[1]}")
            };

            totalScore += handScore + roundScore;
        }

        return totalScore.ToString();
    }

    private int CalcScoreOfHand(string hand)
    {
        return hand switch
        {
            "A" => 1,
            "B" => 2,
            "C" => 3,
            "X" => 1,
            "Y" => 2,
            "Z" => 3,
            _ => throw new Exception($"Invalid hand: {hand}")
        };
    }
}