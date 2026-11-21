namespace Abstractions;

/// <summary>
/// color of each yarn, number of threads overall
/// </summary>
public class YarnConfiguration
{
    public int Width { get; set; }
    public (int Count, string Color)[] Pattern { get; set; } = [];

    /// <summary>
    /// Repeats the pattern to fill the width, returning a zero-based index and color for each yarn.
    /// </summary>
    public (int Index, string Color)[] ResolvePattern()
    {
        if (Width < 0)
            throw new InvalidOperationException("Width cannot be negative.");

        if (Width == 0)
            return [];

        if (Pattern.Length == 0)
            throw new InvalidOperationException("A pattern is required for a nonzero width.");

        foreach (var (count, _) in Pattern)
        {
            if (count <= 0)
                throw new InvalidOperationException("Pattern counts must be positive.");
        }

        var result = new (int Index, string Color)[Width];
        var index = 0;
        while (index < result.Length)
        {
            foreach (var (count, color) in Pattern)
            {
                for (var i = 0; i < count && index < result.Length; i++)
                {
                    result[index] = (index, color);
                    index++;
                }
            }
        }

        return result;
    }

    public static YarnConfiguration Example => new()
    {
        Width = 1000,
        Pattern =
        [
            (100, "Red"),
            (200, "Blue")
        ]
    };
}
