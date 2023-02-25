using System.Drawing;
using NUnit.Framework;

namespace Tasks;

public class NumericSampleTests
{
    [Test]
    public void MatrixNavigation()
    {
        var matrix = new Matrix<char>(10, 9);
        var fileContent = Tools.ReadFromInput("Test.txt");
        
        matrix.FillFromArrayOfStrings(fileContent, s => s.ToArray());

        var bfs = new Bfs<char>
        {
            Matrix = matrix,
            FreeWayFunc = c => c != 'W'
        };
        var result = bfs.FindShortestRoute(new Point(5, 2), new Point(9, 7));
        
        Assert.AreEqual(20, result.Length);
    }
}