namespace Abstractions;

public class Threading
{
    /// <summary>
    /// width of the repeating area
    /// </summary>
    public int Width { get; set; }
    /// <summary>
    /// height of repeating pattern
    /// </summary>
    public int HarnessCount { get; set; }
    public HashSet<(int Yarn, int Harness)> Setup { get; set; } = [];

    public static Threading One => new()
    {
        Width = 4,
        HarnessCount = 4,
        Setup =
        [
            (1, 1),
            (2, 2),
            (3, 3),
            (4, 4)
        ]
    };

    public static Threading Two => new()
    {
        Width = 4,
        HarnessCount = 4,
        Setup =
        [
            (1, 1),
            (4, 2),
            (3, 3),
            (2, 4)            
        ]
    };    
}
