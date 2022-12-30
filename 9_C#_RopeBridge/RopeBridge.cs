var commands = File.ReadAllLines(@"C:\Users\stefan.stingl\source\repos\AOC9\AOC9\input.txt")
                .Select(x => new { Direction = x[0], Distance = int.Parse(x.Substring(2)) });

int visitedPart1 = GetVisitedPositions(2);
Console.WriteLine($"Visited Positions in Part 1: {visitedPart1} ");

int visitedPart2 = GetVisitedPositions(10);
Console.WriteLine($"Visited Positions in Part 2: {visitedPart2} ");


//Thanks to Nawill81 for the Idea. 
int GetVisitedPositions(int numberOfKnots)
{
    HashSet<(int, int)> visited = new();
    var knots = new (int X, int Y)[numberOfKnots];

    foreach (var command in commands)
    {
        for (int i = 0; i < command.Distance; i++)
        {
            knots[0] = command.Direction switch
            {
                'L' => (--knots[0].X, knots[0].Y),
                'R' => (++knots[0].X, knots[0].Y),
                'U' => (knots[0].X, ++knots[0].Y),
                'D' => (knots[0].X, --knots[0].Y),
                _ => throw new InvalidOperationException("we broke"),
            };

            for (int j = 1; j < numberOfKnots; j++)
            {
                var xDist = knots[j - 1].X - knots[j].X;
                var yDist = knots[j - 1].Y - knots[j].Y;

                if (Math.Abs(xDist) > 1 || Math.Abs(yDist) > 1)
                {
                    knots[j].X += Math.Sign(xDist);
                    knots[j].Y += Math.Sign(yDist);
                }
            }

            visited.Add(knots.Last());
        }
    }

    return visited.Count;
}