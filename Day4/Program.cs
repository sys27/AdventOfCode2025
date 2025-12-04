var file = await File.ReadAllLinesAsync("input.txt");
var grid = file
    .Select(line => line.Select(cell => cell == '@').ToArray())
    .ToArray();

// Part 1
var answer = 0;
for (var i = 0; i < grid.Length; i++)
{
    var row = grid[i];
    for (var j = 0; j < row.Length; j++)
    {
        var cell = row[j];
        if (!cell)
            continue;

        if (CountAdjacent(grid, i, j) < 4)
            answer++;
    }
}

Console.WriteLine($"Answer for Part 1: {answer}");

// Part 2
answer = 0;
var changed = false;
do
{
    changed = false;

    for (var i = 0; i < grid.Length; i++)
    {
        var row = grid[i];
        for (var j = 0; j < row.Length; j++)
        {
            var cell = row[j];
            if (!cell)
                continue;

            if (CountAdjacent(grid, i, j) < 4)
            {
                answer++;
                row[j] = false;
                changed = true;
            }
        }
    }
}
while (changed);

Console.WriteLine($"Answer for Part 2: {answer}");

int CountAdjacent(bool[][] grid, int i, int j)
{
    var count = 0;
    var rowsLength = grid.Length;
    var columnsLength = grid[i].Length;

    if (i - 1 >= 0 && grid[i - 1][j])
        count++;

    if (i - 1 >= 0 && j + 1 < columnsLength && grid[i - 1][j + 1])
        count++;

    if (j + 1 < columnsLength && grid[i][j + 1])
        count++;

    if (i + 1 < rowsLength && j + 1 < columnsLength && grid[i + 1][j + 1])
        count++;

    if (i + 1 < rowsLength && grid[i + 1][j])
        count++;

    if (i + 1 < rowsLength && j - 1 >= 0 && grid[i + 1][j - 1])
        count++;

    if (j - 1 >= 0 && grid[i][j - 1])
        count++;

    if (i - 1 >= 0 && j - 1 >= 0 && grid[i - 1][j - 1])
        count++;

    return count;
}