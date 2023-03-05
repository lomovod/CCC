using NUnit.Framework;

namespace Tasks.Classic36;

[TestFixture]
public class Task1
{
    [TestCase("level1_1")]
    [TestCase("level1_2")]
    [TestCase("level1_3")]
    [TestCase("level1_4")]
    [TestCase("level1_5")]
    public void METHOD(string fileName)
    {
        var input = Tools.ReadFromInput($"1\\{fileName}.in");

        var length = int.Parse(input[0]);

        long count = 0;
        foreach (var line in input.Skip(1))
            count += line.Count(x => x == 'C');

        Tools.WriteToOutput($"1\\{fileName}.out", new[] { count.ToString() });
    }
}