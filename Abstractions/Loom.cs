namespace Abstractions;

public class Loom
{
    public int PedalCount { get; set; }
    public int HarnessCount { get; set; }
    public YarnConfiguration Weft { get; set; } = new();
    public YarnConfiguration Warp { get; set; } = new();
    public Threading Threading { get; set; } = new();
    public Tieup Tieup { get; set; } = new();
}
