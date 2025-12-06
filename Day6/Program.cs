var lines = await File.ReadAllLinesAsync("input.txt");
var operators = lines[^1]
    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
    .Select(x => x switch
    {
        "+" => Operator.Add,
        "*" => Operator.Mul,
        _ => throw new ArgumentOutOfRangeException(),
    })
    .ToArray();

var numbers = new long[operators.Length];
for (var i = 0; i < numbers.Length; i++)
    if (operators[i] == Operator.Mul)
        numbers[i] = 1;

// Part 1
Span<Range> current = stackalloc Range[operators.Length];
for (var i = 0; i < lines.Length - 1; i++)
{
    var line = lines[i];
    line.Split(current, ' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    for (var j = 0; j < current.Length; j++)
    {
        var op = operators[j];
        var parsedNumber = long.Parse(line[current[j]]);

        numbers[j] = op switch
        {
            Operator.Add => numbers[j] + parsedNumber,
            Operator.Mul => numbers[j] * parsedNumber,
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}

Console.WriteLine($"Answer for Part 1: {numbers.Sum()}");

// Part 2
var columns = lines[^1].Length;
var numberIndex = 0;
for (var j = 0; j < columns;)
{
    var opLine = lines[^1];
    var opChar = opLine[j];
    var op = opChar switch
    {
        '+' => Operator.Add,
        '*' => Operator.Mul,
        _ => throw new ArgumentOutOfRangeException(),
    };

    numbers[numberIndex] = op == Operator.Mul ? 1 : 0;

    while (true)
    {
        var numberString = string.Empty;
        for (var i = 0; i <= lines.Length - 2; i++)
            numberString += lines[i][j];

        if (!string.IsNullOrWhiteSpace(numberString))
        {
            var number = long.Parse(numberString);
            numbers[numberIndex] = op switch
            {
                Operator.Add => numbers[numberIndex] + number,
                Operator.Mul => numbers[numberIndex] * number,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        j++;

        if (j == columns)
            break;

        opChar = opLine[j];
        if (opChar != ' ')
            break;
    }

    numberIndex++;
}

Console.WriteLine($"Answer for Part 2: {numbers.Sum()}");

enum Operator
{
    Add,
    Mul,
}