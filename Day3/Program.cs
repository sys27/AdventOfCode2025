var file = await File.ReadAllLinesAsync("input.txt");
var batteries = file
    .Select(line => line.Select(c => (byte)(c - '0')).ToArray())
    .ToArray();

// Part 1
var total = 0L;
foreach (var battery in batteries)
{
    const int maxDigits = 2;

    var firstMax = FindMax(battery, 1, -1, maxDigits);
    var secondMax = FindMax(battery, 2, firstMax.Index, maxDigits);

    total += firstMax.Item * 10 + secondMax.Item;
}

Console.WriteLine($"Answer for Part 1: {total}");

// Part 2
total = 0;
foreach (var battery in batteries)
{
    const int maxDigits = 12;

    var previous = FindMax(battery, 1, -1, maxDigits);
    var number = previous.Item * (long)Math.Pow(10, maxDigits - 1);

    for (var i = 2; i <= 12; i++)
    {
        previous = FindMax(battery, i, previous.Index, maxDigits);
        number += previous.Item * (long)Math.Pow(10, maxDigits - i);
    }

    total += number;
}

Console.WriteLine($"Answer for Part 2: {total}");

(int Index, byte Item) FindMax(byte[] battery, int position, int lastIndex, int maxDigits)
    => battery
        .Index()
        .Skip(lastIndex + 1)
        .Take(battery.Length - (lastIndex + 1) - (maxDigits - position))
        .MaxBy(x => x.Item);