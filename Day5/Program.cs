var file = await File.ReadAllLinesAsync("input.txt");
var emptyLineIndex = file.IndexOf(string.Empty);
if (emptyLineIndex == -1)
    throw new Exception("No empty line found");

var ranges = file
    .Take(emptyLineIndex)
    .Select(line =>
    {
        var parts = line.Split('-');

        return (Start: long.Parse(parts[0]), End: long.Parse(parts[1]));
    })
    .ToArray();

var products = file
    .Skip(emptyLineIndex + 1)
    .Select(long.Parse)
    .ToArray();

// Part 1
var freshCount = products.Count(x => ranges.Any(range => x >= range.Start && x <= range.End));

Console.WriteLine($"Answer for Part 1: {freshCount}");

// Part 2
var mergedRanges = new List<(long Start, long End)>();
foreach (var (start, end) in ranges.OrderBy(x => x.Start))
{
    var lastRange = mergedRanges.LastOrDefault();
    if (lastRange == default || start > lastRange.End + 1)
        mergedRanges.Add((start, end));
    else
        mergedRanges[^1] = (lastRange.Start, Math.Max(lastRange.End, end));
}

var totalFreshCount = mergedRanges.Sum(x => x.End - x.Start + 1);

Console.WriteLine($"Answer for Part 2: {totalFreshCount}");