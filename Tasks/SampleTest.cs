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
        foreach (var line in input.Skip(1)) count += line.Count(x => x == 'C');

        Tools.WriteToOutput($"1\\{fileName}.out", new[] { count.ToString() });
    }

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

            for (var j = 0; j < length; j++) map[i - 1, j] = line[j];
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
            for (var j = 0; j < length; j++) Console.Write(map[i, j]);

            Console.WriteLine();
        }

        Console.WriteLine(coints);

        Tools.WriteToOutput($"2\\{fileName}.out", new[] { coints.ToString() });
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
        var input = Tools.ReadFromInput($"3\\{fileName}.in");

        var length = int.Parse(input[0]);

        var map = new char[length, length];

        for (var i = 1; i <= length; i++)
        {
            var line = input[i];

            for (var j = 0; j < length; j++) map[i - 1, j] = line[j];
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

            if (isDead) break;

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
        Tools.WriteToOutput($"3\\{fileName}.out", new[] { $"{coins} {isAliveString}" });
    }

    [TestCase("level4_example")]
    // [TestCase("level2_1")]
    // [TestCase("level2_2")]
    // [TestCase("level2_3")]
    // [TestCase("level2_4")]
    // [TestCase("level2_5")]
    public void METHOD4(string fileName)
    {
        var input = Tools.ReadFromInput($"4\\{fileName}.in");

        var length = int.Parse(input[0]);

        var map = new char[length, length];

        for (var i = 1; i <= length; i++)
        {
            var line = input[i];

            for (var j = 0; j < length; j++) map[i - 1, j] = line[j];
        }

        var coorinates = input[length + 1];
        var maxSteps = int.Parse(input[length + 2]);

        var y = int.Parse(coorinates.Split(' ')[0]) - 1;
        var x = int.Parse(coorinates.Split(' ')[1]) - 1;

        var path = "";
        var stack = new Stack<Point>();
        var coins = 0;

        long count = 0;
        foreach (var line in input.Skip(1).Take(length)) count += line.Count(x => x == 'C');


        for (var step = 0; step < maxSteps; step++)
        {
            var ways = 0;
            var currentWay = 'X';
            if (map[x + 1, y] == 'C')
            {
                ways++;
                currentWay = 'R';
            }

            if (map[x, y + 1] == 'C')
            {
                ways++;
                if (currentWay == 'X')
                    currentWay = 'D';
            }

            if (map[x - 1, y] == 'C')
            {
                ways++;
                if (currentWay == 'X')
                    currentWay = 'L';
            }

            if (map[x, y-1] == 'C')
            {
                ways++;
                if (currentWay == 'X')
                    currentWay = 'U';
            }

            if (ways > 1)
            {
                stack.Push(new Point { x = x, y = y });
            }

            if (ways == 0)
            {
                var point = stack.Pop();
                var lastRoute = path.Reverse().ToString();
                var tx = x;
                var ty = y;
                for (int j = 0; j < lastRoute.Length; j++)
                {

                    switch (lastRoute[j])
                    {
                        case 'D':
                            ty -= 1;
                            path += 'U';
                            break;
                        case 'U':
                            ty += 1;
                            path += 'D';
                            break;
                        case 'L':
                            tx += 1;
                            path += 'R';
                            break;
                        case 'R':
                            tx -= 1;
                            path += 'L';
                            break;
                    }

                    if (tx == point.x && ty == point.y)
                    {
                        break;
                    }
                }
                continue;
            }

            switch (currentWay)
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

            path += currentWay;
            map[x, y] = ' ';
            coins++;

            if (coins == count)
            {
                break;
            }
        }


        Console.WriteLine(path);

        //Tools.WriteToOutput($"2\\{fileName}.out", new[] { coints.ToString() });
    }

    private class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public string Path { get; set; }
    }
}