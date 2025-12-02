var file = File.ReadAllText("input.txt");
var ranges = file
    .Split(',')
    .Select(x =>
    {
        var range = x.Split('-').Select(long.Parse).ToArray();

        return (range[0], range[1]);
    })
    .ToArray();

// Part 1
var invalid = 0L;
foreach (var (start, end) in ranges)
{
    for (var i = start; i <= end; i++)
    {
        var digits = (int)Math.Log10(i) + 1;
        if (digits % 2 == 1)
            continue;

        var half = digits / 2;
        var divisor = (int)Math.Pow(10, half);
        var left = i / divisor;
        var right = i % divisor;

        if (left == right)
            invalid += i;
    }
}

Console.WriteLine($"Answer for Part 1: {invalid}");

// Part 2
invalid = 0L;
foreach (var (start, end) in ranges)
{
    for (var i = start; i <= end; i++)
    {
        var digits = (int)Math.Log10(i) + 1;
        for (var j = 2;; j++)
        {
            var part = digits / j;
            if (part == 0)
                break;

            if (digits % j != 0)
                continue;

            var parts = new long[j];
            for (var k = 0; k < parts.Length; k++)
                parts[k] = i % (long)Math.Pow(10, digits - part * k) / (long)Math.Pow(10, digits - part * (k + 1));

            if (parts.All(x => x == parts[0]))
            {
                invalid += i;
                break;
            }
        }
    }
}

Console.WriteLine($"Answer for Part 2: {invalid}");