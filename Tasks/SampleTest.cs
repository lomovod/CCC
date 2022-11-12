using NUnit.Framework;

namespace Tasks;

public class SampleTest
{
    [Test]
    public void TestFiles()
    {
        var input = Tools.ReadFromInput("Test.txt");

        var charMatrix = new Matrix<char>(10, 9);

        var y = 0;
        foreach (var line in input)
        {
            y++;
            var x = 0;
            foreach (var @char in line)
            {
                x++;

                charMatrix[x, y] = @char;
            }
        }

        var intMatrix = charMatrix.Convert(c =>
        {
            return c switch
            {
                'W' => -1,
                _ => 0
            };
        });
    }
}