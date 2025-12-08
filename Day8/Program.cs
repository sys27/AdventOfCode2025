var file = await File.ReadAllLinesAsync("input.txt");
var boxes = file
    .Select(line =>
    {
        var span = line.AsSpan();

        Span<Range> ranges = stackalloc Range[3];
        span.Split(ranges, ',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        return new Box(
            int.Parse(span[ranges[0]]),
            int.Parse(span[ranges[1]]),
            int.Parse(span[ranges[2]])
        );
    })
    .ToArray();

var distances = new List<Pair>();
for (var i = 0; i < boxes.Length; i++)
{
    for (var j = i + 1; j < boxes.Length; j++)
    {
        var box1 = boxes[i];
        var box2 = boxes[j];
        distances.Add(new Pair(box1, box2, box1.DistanceTo(box2)));
    }
}

distances.Sort((pair1, pair2) => pair1.Distance.CompareTo(pair2.Distance));

// Part 1
var circuits = new List<Circuit>(boxes.Length);
foreach (var box in boxes)
    circuits.Add(new Circuit([box]));

for (int i = 0, connectionsCreated = 0; i < distances.Count && connectionsCreated < 1000; i++, connectionsCreated++)
{
    var (box1, box2, _) = distances[i];
    var box1Connection = circuits.FirstOrDefault(x => x.Boxes.Contains(box1));
    var box2Connection = circuits.FirstOrDefault(x => x.Boxes.Contains(box2));

    if (ReferenceEquals(box1Connection, box2Connection))
        continue;

    box1Connection!.Boxes.UnionWith(box2Connection!.Boxes);
    circuits.Remove(box2Connection);
}

circuits.Sort((circuit1, circuit2) => circuit2.Size.CompareTo(circuit1.Size));

var answer = circuits
    .Take(3)
    .Select(x => x.Size)
    .Aggregate(1L, (acc, c) => acc * c);

Console.WriteLine($"Answer for Part 1: {answer}");

// Part 2
circuits = new List<Circuit>(boxes.Length);
foreach (var box in boxes)
    circuits.Add(new Circuit([box]));

foreach (var (box1, box2, _) in distances)
{
    var box1Connection = circuits.FirstOrDefault(x => x.Boxes.Contains(box1));
    var box2Connection = circuits.FirstOrDefault(x => x.Boxes.Contains(box2));

    if (ReferenceEquals(box1Connection, box2Connection))
        continue;

    box1Connection!.Boxes.UnionWith(box2Connection!.Boxes);
    circuits.Remove(box2Connection);

    if (circuits.Count == 1)
    {
        Console.WriteLine($"Answer for Part 2: {(long)box1.X * box2.X}");
        return;
    }
}

record Box(int X, int Y, int Z)
{
    public override string ToString()
        => $"({X}, {Y}, {Z})";

    public double DistanceTo(Box other)
        => Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2) + Math.Pow(Z - other.Z, 2));
}

record struct Pair(Box Box1, Box Box2, double Distance)
{
    public override string ToString()
        => $"{Box1} <-> {Box2}: {Distance}";
}

record Circuit(HashSet<Box> Boxes)
{
    public int Size
        => Boxes.Count;
}