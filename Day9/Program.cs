var file = await File.ReadAllLinesAsync("input.txt");
var points = file
    .Select(line =>
    {
        var span = line.AsSpan();
        Span<Range> ranges = stackalloc Range[2];
        span.Split(ranges, ',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        return new Point(
            int.Parse(span[ranges[0]]),
            int.Parse(span[ranges[1]])
        );
    })
    .ToArray();

// Part 1
var maxArea = long.MinValue;
for (var i = 0; i < points.Length; i++)
{
    for (var j = i + 1; j < points.Length; j++)
    {
        var p1 = points[i];
        var p2 = points[j];
        var area = (Math.Abs(p2.X - p1.X) + 1L) * (Math.Abs(p2.Y - p1.Y) + 1L);
        maxArea = Math.Max(maxArea, area);
    }
}

Console.WriteLine($"Answer for Part 1: {maxArea}");

readonly record struct Point(int X, int Y)
{
    public override string ToString()
        => $"({X}, {Y})";
}