namespace Abstractions;

/// <summary>
/// describes the pedal-harness configuration
/// </summary>
public class Tieup
{
    /// <summary>
    /// imagine as actual pedals identifying columns in a 2D grid (X axis)
    /// </summary>
    public int PedalCount { get; set; }
    /// <summary>
    /// imagine as horizontal pieces of wood (aka Shafts)
    /// </summary>
    public int HarnessCount { get; set; }
    /// <summary>
    /// indicate which harnesses go with which pedals
    /// </summary>
    public Dictionary<int, int[]> PedalHarnessStates { get; set; } = [];


    public static Tieup BookSampleOne => new()
    {
        PedalCount = 4,
        HarnessCount = 4,
        PedalHarnessStates = new()
        {
            [1] = [1, 2],
            [2] = [2, 3],
            [3] = [3, 4],
            [4] = [1, 4]
        }
    };
}
