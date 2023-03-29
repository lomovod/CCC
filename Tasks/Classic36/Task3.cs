using NUnit.Framework;

namespace Tasks.Classic36;

[TestFixture]
public class Task3
{
    private class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public string Path { get; set; }
    }

    //[TestCase("level3_example")]
    [TestCase("level3_1")]
    [TestCase("level3_2")]
    [TestCase("level3_3")]
    [TestCase("level3_4")]
    [TestCase("level3_5")]
    [TestCase("level3_6")]
    [TestCase("level3_7")]
    public void METHOD3(string fileName)
    {
        var input = Tools.ReadFromInput($"3\\{fileName}.in", "\\Classic36");

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

        var ghosts = int.Parse(input[length + 4]);
        var ghostPoints = new List<Point>();
        for (var i = 0; i < ghosts; i++)
        {
            var point = new Point();
            coorinates = input[length + 5 + i * 3];
            point.Path = input[length + 7 + i * 3];
            point.y = int.Parse(coorinates.Split(' ')[0]) - 1;
            point.x = int.Parse(coorinates.Split(' ')[1]) - 1;
            ghostPoints.Add(point);
        }

        var coins = 0;
        var isDead = false;

        for (var step = 0; step < path.Length; step++)
        {
            var direction = path[step];
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

            foreach (var ghostPoint in ghostPoints)
            {
                var direction2 = ghostPoint.Path[step];
                switch (direction2)
                {
                    case 'D':
                        ghostPoint.y += 1;
                        break;
                    case 'U':
                        ghostPoint.y -= 1;
                        break;
                    case 'L':
                        ghostPoint.x -= 1;
                        break;
                    case 'R':
                        ghostPoint.x += 1;
                        break;
                }

                if (x == ghostPoint.x && y == ghostPoint.y)
                {
                    isDead = true;
                    break;
                }
            }

            if (isDead)
                break;

            if (map[y, x] == 'W')
            {
                isDead = true;
                break;
            }


            if (map[y, x] == 'C')
            {
                coins++;
                map[y, x] = ' ';
            }
        }


        // for (int i = 0; i < length; i++)
        // {
        //     for (int j = 0; j < length; j++)
        //     {
        //         Console.Write(map[i, j]);
        //     }
        //
        //     Console.WriteLine();
        // }


        var isAliveString = isDead ? "NO" : "YES";
        Tools.WriteToOutput($"3\\{fileName}.out", new[] { $"{coins} {isAliveString}" }, "\\Classic36");
    }
}