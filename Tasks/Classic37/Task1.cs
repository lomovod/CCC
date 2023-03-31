using NUnit.Framework;

namespace Tasks.Classic37;

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
        var input = Tools.ReadFromInput($"1\\{fileName}.in", "\\Classic37");

        var length = int.Parse(input[0]);

        var list = new List<string>();
        foreach (var line in input.Skip(1))
            list.Add(line);

        var result = new List<string>();
        foreach (var item in list)
            result.Add(Winner(item[0], item[1]).ToString());


        Tools.WriteToOutput($"1\\{fileName}.out", result.ToArray(), "\\Classic37");
    }

    private char Winner(char left, char right)
    {
        if ((left == 'P' && right == 'R') || (left == 'R' && right == 'P') || (left == 'P' && right == 'P'))
            return 'P';

        if ((left == 'S' && right == 'R') || (left == 'R' && right == 'S') || (left == 'R' && right == 'R'))
            return 'R';

        if ((left == 'P' && right == 'S') || (left == 'S' && right == 'P') || (left == 'S' && right == 'S'))
            return 'S';

        throw new NotImplementedException();
    }
}