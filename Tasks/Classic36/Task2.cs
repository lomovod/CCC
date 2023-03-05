using NUnit.Framework;

namespace Tasks.Classic36;

[TestFixture]
public class Task2
{
    //[TestCase("level2_example")]
    [TestCase("level2_1")]
    [TestCase("level2_2")]
    [TestCase("level2_3")]
    [TestCase("level2_4")]
    [TestCase("level2_5")]
    public void METHOD2(string fileName)
    {
        var input = Tools.ReadFromInput($"2\\{fileName}.in");

        var length = int.Parse(input[0]);

        var map = new char[length, length];

        for (var i = 1; i <= length; i++)
        {
            var line = input[i];

            for (var j = 0; j < length; j++)
                map[i - 1, j] = line[j];
        }

        var coorinates = input[length + 1];
        var path = input[length + 3];

        var y = int.Parse(coorinates.Split(' ')[0]) - 1;
        var x = int.Parse(coorinates.Split(' ')[1]) - 1;

        var coints = 0;

        foreach (var direction in path)
        {
            switch (direction)
            {
                case 'D':
                    y += 1;
                    break;
                case 'U':
                    y -= 1;
                    break;
                case 'L':
                    x -= 1;
                    break;
                case 'R':
                    x += 1;
                    break;
            }

            if (map[y, x] == 'C')
            {
                coints++;
                map[y, x] = ' ';
            }
        }

        for (var i = 0; i < length; i++)
        {
            for (var j = 0; j < length; j++)
                Console.Write(map[i, j]);

            Console.WriteLine();
        }

        Console.WriteLine(coints);

        Tools.WriteToOutput($"2\\{fileName}.out", new[] { coints.ToString() });
    }
}