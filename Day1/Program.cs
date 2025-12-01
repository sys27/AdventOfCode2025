var lines = await File.ReadAllLinesAsync("input.txt");

// Part 1
var count = 0;
var dial = 50;

foreach (var line in lines)
{
    var number = int.Parse(line.AsSpan(1));

    dial += line[0] == 'R' ? number : -number;
    dial = (dial % 100 + 100) % 100;

    if (dial == 0)
        count++;
}

Console.WriteLine($"Answer for Part 1: {count}");

// Part 2
count = 0;
dial = 50;

foreach (var line in lines)
{
    var number = int.Parse(line.AsSpan(1));
    var direction = line[0] == 'R';

    for (var i = 0; i < number; i++)
    {
        dial += direction ? 1 : -1;

        dial = dial switch
        {
            < 0 => 99,
            > 99 => 0,
            _ => dial
        };

        if (dial == 0)
            count++;
    }
}

Console.WriteLine($"Answer for Part 2: {count}");