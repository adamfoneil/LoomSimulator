using Abstractions;

static void Check(bool condition, string message)
{
    if (!condition) throw new Exception(message);
}

static void ExpectInvalid(Warp warp)
{
    try { warp.ResolvePattern(); }
    catch (InvalidOperationException) { return; }
    throw new Exception("Expected InvalidOperationException");
}

var example = Warp.Example.ResolvePattern();
Check(example.Length == 1000, "Example width");
for (int i = 0; i < example.Length; i++)
    Check(example[i] == (i, i % 300 < 100 ? "Red" : "Blue"), $"Example yarn {i}");

var truncated = new Warp { Width = 3, Pattern = [(5, "Green")] }.ResolvePattern();
Check(truncated.SequenceEqual(new[] { (0, "Green"), (1, "Green"), (2, "Green") }), "Truncated run");
Check(new Warp().ResolvePattern().Length == 0, "Zero width");
ExpectInvalid(new Warp { Width = -1 });
ExpectInvalid(new Warp { Width = 1 });
ExpectInvalid(new Warp { Width = 1, Pattern = [(0, "Red")] });
ExpectInvalid(new Warp { Width = 1, Pattern = [(-1, "Red")] });
Console.WriteLine("All warp checks passed.");
