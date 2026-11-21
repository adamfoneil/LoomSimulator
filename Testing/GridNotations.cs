namespace Testing;

[TestClass]
public sealed class GridNotations
{
    [TestMethod]
    [DataRow("[(1,1), (2,2), (3,3), (4,4)]x2", true, "4x8:1,1|2,2|3,3|4,4|5,1|6,2|7,3|8,4")]
    [DataRow("[(1,1), (2,2), (3,3), (4,4)]x2", false, "4x4:1,1|2,2|3,3|4,4")]
    [DataRow("[(1,1), (2,2), (3,3), (4,4), (5,4)]x2\r\n[(4,4), (3,3), (2,2), (1,1)]x2", false, "4x4:1,1|2,2|3,3|4,4")]
    public void SimpleCase(string input, bool expanded, string expected)
    {
        var grid = Abstractions.GridNotation.Parse(input, expanded);
        Assert.AreEqual(expected, grid.ToString());
    }
}
