namespace Abstractions;

public class ThreadingPattern
{
    /// <summary>
    /// overall loom width
    /// </summary>
    public int Width { get; set; }
    public (int Count, Threading Threading)[] Pattern { get; set; } = [];

    public (int Index, Threading Threading)[] ResolvePattern()
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
        var result = new (int Index, Threading Threading)[Width];
        var index = 0;
        while (index < result.Length)
        {
            foreach (var (count, threading) in Pattern)
            {
                for (var i = 0; i < count && index < result.Length; i++)
                {
                    result[index] = (index, threading);
                    index++;
                }
            }
        }
        return result;
    }

    public static ThreadingPattern Example => new()
    {
        Pattern = 
        [
            (5, Threading.One),
            (2, Threading.Two),
            (5, Threading.One)
        ]
    };
}
