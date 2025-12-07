var file = await File.ReadAllLinesAsync("input.txt");
var grid = file
    .Select(line => line
        .Select(cell => cell switch
        {
            '.' => CellKind.Empty,
            'S' => CellKind.Beam,
            '^' => CellKind.Splitter,
            _ => throw new ArgumentOutOfRangeException(nameof(cell), cell, null),
        })
        .ToArray())
    .ToArray();

// Part 1
var answer = 0;
for (var i = 1; i < grid.Length; i++)
{
    var previous = grid[i - 1];
    var current = grid[i];

    for (var j = 0; j < current.Length; j++)
    {
        var previousCell = previous[j];
        var currentCell = current[j];

        if (previousCell != CellKind.Beam)
            continue;

        if (currentCell == CellKind.Empty)
        {
            current[j] = CellKind.Beam;
        }
        else if (currentCell == CellKind.Splitter)
        {
            if (j > 0)
                current[j - 1] = CellKind.Beam;

            if (j < current.Length - 1)
                current[j + 1] = CellKind.Beam;

            answer++;
        }
    }
}

Console.WriteLine($"Answer for Part 1: {answer}");

// Part 2
var lines = File.ReadAllLines("input.txt");
var startCol = -1;
for (var col = 0; col < lines[0].Length; col++)
{
    if (lines[0][col] == 'S')
    {
        startCol = col;
        break;
    }
}

var timelines = CountTimelinesRecursive(lines, 0, startCol, []);
Console.WriteLine($"Number of timelines: {timelines}");
return;

static long CountTimelinesRecursive(string[] grid, int i, int j, Dictionary<(int, int), long> memo)
{
    if (i >= grid.Length)
        return 1;

    if (memo.ContainsKey((i, j)))
        return memo[(i, j)];

    var result = 0L;

    if (grid[i][j] == '^')
    {
        var leftTimelines = 0L;
        var rightTimelines = 0L;

        var leftCol = j - 1;
        if (leftCol >= 0)
            leftTimelines = CountTimelinesRecursive(grid, i + 1, leftCol, memo);

        var rightCol = j + 1;
        if (rightCol < grid[i].Length)
            rightTimelines = CountTimelinesRecursive(grid, i + 1, rightCol, memo);

        result = leftTimelines + rightTimelines;
    }
    else
    {
        result = CountTimelinesRecursive(grid, i + 1, j, memo);
    }

    memo[(i, j)] = result;

    return result;
}

enum CellKind
{
    Empty,
    Beam,
    Splitter,
}