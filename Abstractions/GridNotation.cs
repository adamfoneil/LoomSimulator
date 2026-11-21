using System.Text.RegularExpressions;

namespace Abstractions;

public record GridDefinition(
    int Width,
    int Height,
    HashSet<(int X, int Y, bool Enabled)> Cells
)
{
    public override string ToString() => $"{Width}x{Height}:{string.Join('|', Cells
        .OrderBy(c => c.X)
        .ThenBy(c => c.Y)
        .Select(c => $"{c.X},{c.Y}"))}";
}


public static class GridNotation
{
    private static readonly Regex NotationRegex = new(
        "^\\[(?<cells>[^\\]]*)\\](?:\\s*x\\s*(?<repeat>\\d+)\\s*)?$",
        RegexOptions.CultureInvariant);

    private static readonly Regex CellRegex = new(
        "\\(\\s*(?<x>\\d+)\\s*,\\s*(?<y>\\d+)\\s*\\)",
        RegexOptions.CultureInvariant);

    public static GridDefinition Parse(string input, bool expanded)
    {
        ArgumentNullException.ThrowIfNull(input);

        var notation = NotationRegex.Match(input.Trim());
        if (!notation.Success)
            throw new FormatException("Grid notation must be a bracketed coordinate list followed by an optional x repetition, for example [(1,1), (2,2)]x2.");

        var repeat = 1;
        var repeatText = notation.Groups["repeat"].Value;
        if (!string.IsNullOrEmpty(repeatText) && (!int.TryParse(repeatText, out repeat) || repeat <= 0))
            throw new FormatException("Grid repetition must be a positive integer.");

        var cellText = notation.Groups["cells"].Value;
        var matches = CellRegex.Matches(cellText);
        if (matches.Count == 0)
            throw new FormatException("Grid notation must contain at least one (X,Y) coordinate.");

        var remainder = CellRegex.Replace(cellText, string.Empty);
        if (remainder.Any(c => c != ',' && !char.IsWhiteSpace(c)) ||
            remainder.Count(c => c == ',') != matches.Count - 1)
            throw new FormatException("Grid notation contains invalid coordinate syntax.");

        var baseCells = new HashSet<(int X, int Y, bool Enabled)>();
        foreach (Match match in matches)
        {
            if (!int.TryParse(match.Groups["x"].Value, out var x) ||
                !int.TryParse(match.Groups["y"].Value, out var y) ||
                x <= 0 || y <= 0)
                throw new FormatException("Grid coordinates must be positive integers.");

            baseCells.Add((x, y, true));
        }

        var width = baseCells.Max(c => c.X);
        var height = baseCells.Max(c => c.Y);

        if (!expanded)
            return new GridDefinition(width, height, baseCells);

        var cells = new HashSet<(int X, int Y, bool Enabled)>();
        for (var repetition = 0; repetition < repeat; repetition++)
        {
            foreach (var (x, y, enabled) in baseCells)
                cells.Add((x + (repetition * width), y, enabled));
        }

        return new GridDefinition(width, height * repeat, cells);
    }
}
