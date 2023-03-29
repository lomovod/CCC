using System.Drawing;
using NUnit.Framework;

namespace Tasks.Classic36;

[TestFixture]
public class Test4
{
    //[TestCase("level4_example")]
    //[TestCase("level4_1")]
    //[TestCase("level4_2")]
    [TestCase("level4_3")]
    [TestCase("level4_4")]
    [TestCase("level4_5")]
    public void METHOD(string fileName)
    {
        var input = Tools.ReadFromInput($"4\\{fileName}.in", "\\Classic36");

        var length = int.Parse(input[0]);

        var map = new char[length, length];

        for (var i = 1; i <= length; i++)
        {
            var line = input[i];

            for (var j = 0; j < length; j++)
                map[j, i - 1] = line[j];
        }

        //for (int i = 0; i < length; i++)
        //{
        //    for (int j = 0; j < length; j++)
        //    {
        //        Console.Write(map[i, j]);
        //    }

        //    Console.WriteLine();
        //}

        //Console.WriteLine(map[5,4]);
        //return;

        var totalCoins = 0;
        var coins = 0;

        foreach (var line in input.Skip(1).Take(length))
            totalCoins += line.Count(x => x == 'C');
        var path = "";

        var playerCoorinates = input[length + 1];
        var maxSteps = int.Parse(input[length + 2]);

        var y = int.Parse(playerCoorinates.Split(' ')[0]) - 1;
        var x = int.Parse(playerCoorinates.Split(' ')[1]) - 1;

        for (var step = 0; step < maxSteps; step++)
        {
            var closestCoin = FindPathToClosestCoin(map, length, length, x, y);
            path += closestCoin.Item1;
            x = closestCoin.Item2.X;
            y = closestCoin.Item2.Y;

            coins++;
            if (coins == totalCoins)
                break;
        }

        Console.WriteLine(path);
        Tools.WriteToOutput($"4\\{fileName}.out", new[] { path }, "\\Classic36");
    }

    private (string, Point) FindPathToClosestCoin(char[,] map, int width, int height, int playerX, int playerY)
    {
        var tempMap = new int[width, height];

        var currentX = playerX;
        var currentY = playerY;
        var currentStep = 1;

        tempMap[currentX, currentY] = currentStep;

        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
            if (map[x, y] == 'W' || map[x, y] == 'G')
                tempMap[x, y] = -1;


        var point = FindClosestCoin(map, tempMap, playerX, playerY, currentStep);
        var routeToCoin = BuildRouteToCoin(tempMap, point.Value);

        return (routeToCoin, point.Value);
        // while (true)
        // {
        //     currentStep++;
        //
        //     if (tempMap[currentX + 1, currentY] != -1) 
        //         tempMap[currentX + 1, currentY] = currentStep;
        //
        //     //if (map[currentX + 1, currentY] != 'W' || map[currentX + 1, currentY] != 'G')
        //     //    tempMap[currentX + 1, currentY] = -1;
        //     //else
        //     //    tempMap[currentX + 1, currentY] = currentStep;
        //
        //     //if (map[currentX + 1, currentY] != 'W' || map[currentX + 1, currentY] != 'G')
        //     //    tempMap[currentX + 1, currentY] = -1;
        //     //else
        //     //    tempMap[currentX + 1, currentY] = currentStep;
        // }
    }

    private string BuildRouteToCoin(int[,] tempMap, Point point)
    {
        var path = "";
        while (true)
        {
            var weight = tempMap[point.X, point.Y] - 1;
            if (tempMap[point.X + 1, point.Y] == weight)
            {
                point = new Point(point.X + 1, point.Y);
                path = "L" + path;
                //path = "U" + path;
            }
            else if (tempMap[point.X - 1, point.Y] == weight)
            {
                point = new Point(point.X - 1, point.Y);
                path = "R" + path;
                //path = "D" + path;
            }
            else if (tempMap[point.X, point.Y + 1] == weight)
            {
                point = new Point(point.X, point.Y + 1);
                path = "U" + path;
                //path = "L" + path;
            }
            else if (tempMap[point.X, point.Y - 1] == weight)
            {
                point = new Point(point.X, point.Y - 1);
                path = "D" + path;
                //path = "R" + path;
            }

            if (weight == 1)
                return path;
        }
    }

    private Point? FindClosestCoin(char[,] map, int[,] tempMap, int playerX, int playerY, int currentStep)
    {
        if (map[playerX, playerY] == 'C')
        {
            map[playerX, playerY] = '\0';
            return new Point(playerX, playerY);
        }

        if (map[playerX + 1, playerY] == 'C')
        {
            tempMap[playerX + 1, playerY] = currentStep + 1;
            map[playerX + 1, playerY] = '\0';
            return new Point(playerX + 1, playerY);
        }

        if (map[playerX - 1, playerY] == 'C')
        {
            tempMap[playerX - 1, playerY] = currentStep + 1;
            map[playerX - 1, playerY] = '\0';
            return new Point(playerX - 1, playerY);
        }

        if (map[playerX, playerY + 1] == 'C')
        {
            tempMap[playerX, playerY + 1] = currentStep + 1;
            map[playerX, playerY + 1] = '\0';
            return new Point(playerX, playerY + 1);
        }

        if (map[playerX, playerY - 1] == 'C')
        {
            tempMap[playerX, playerY - 1] = currentStep + 1;
            map[playerX, playerY - 1] = '\0';
            return new Point(playerX, playerY - 1);
        }


        if (tempMap[playerX + 1, playerY] == 0)
        {
            tempMap[playerX + 1, playerY] = currentStep + 1;
            // if (map[playerX+1, playerY] == 'C')
            // {
            //     map[playerX+1, playerY] = '\0';
            //     return new Point(playerX+1, playerY);
            // }


            var findClosestCoin = FindClosestCoin(map, tempMap, playerX + 1, playerY, currentStep + 1);
            if (findClosestCoin.HasValue)
                return findClosestCoin;
        }

        if (tempMap[playerX - 1, playerY] == 0)
        {
            tempMap[playerX - 1, playerY] = currentStep + 1;
            // if (map[playerX-1, playerY] == 'C')
            // {
            //     map[playerX-1, playerY] = '\0';
            //     return new Point(playerX-1, playerY);
            // }


            var findClosestCoin = FindClosestCoin(map, tempMap, playerX - 1, playerY, currentStep + 1);
            if (findClosestCoin.HasValue)
                return findClosestCoin;
        }

        if (tempMap[playerX, playerY + 1] == 0)
        {
            tempMap[playerX, playerY + 1] = currentStep + 1;
            // if (map[playerX, playerY+1] == 'C')
            // {
            //     map[playerX, playerY+1] = '\0';
            //     return new Point(playerX, playerY+1);
            // }


            var findClosestCoin = FindClosestCoin(map, tempMap, playerX, playerY + 1, currentStep + 1);
            if (findClosestCoin.HasValue)
                return findClosestCoin;
        }

        if (tempMap[playerX, playerY - 1] == 0)
        {
            tempMap[playerX, playerY - 1] = currentStep + 1;
            // if (map[playerX, playerY-1] == 'C')
            // {
            //     map[playerX, playerY-1] = '\0';
            //     return new Point(playerX, playerY-1);
            // }


            var findClosestCoin = FindClosestCoin(map, tempMap, playerX, playerY - 1, currentStep + 1);
            if (findClosestCoin.HasValue)
                return findClosestCoin;
        }

        return null;
    }

    private Point GetRandomCoinCoordinates(char[,] map, int width, int height)
    {
        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
            if (map[x, y] == 'C')
                return new Point(x, y);

        return new Point(-1, -1);
    }
}