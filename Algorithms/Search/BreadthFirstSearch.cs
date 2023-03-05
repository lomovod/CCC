using System.Drawing;
using Algorithms.Primitives;

namespace Algorithms.Search;

public class BreadthFirstSearch<T>
{
    public Matrix<T> Matrix { get; set; }

    public Func<T, bool> FreeWayFunc { get; set; }

    public Point[] FindShortestRoute(Point start, Point end)
    {
        var visitedPoints = new Matrix<bool>(Matrix.Width, Matrix.Height);
        var pointsQueue = new Queue<PointWithParent>();
        visitedPoints[start] = true;
        pointsQueue.Enqueue(new PointWithParent(start, null));
        while (pointsQueue.TryDequeue(out var point))
        {
            if (point.Point == end)
            {
                var result = new List<Point>();
                while (point != null)
                {
                    result.Add(point.Point);
                    point = point.Parent;
                }

                return result.ToArray().Reverse().ToArray();
            }

            var adjacentPoints = new List<Point>();
            if (TryTopPoint(visitedPoints, point.Point, out var topPoint))
                adjacentPoints.Add(topPoint);
            if (TryBottomPoint(visitedPoints, point.Point, out var bottomPoint))
                adjacentPoints.Add(bottomPoint);
            if (TryLeftPoint(visitedPoints, point.Point, out var leftPoint))
                adjacentPoints.Add(leftPoint);
            if (TryRightPoint(visitedPoints, point.Point, out var rightPoint))
                adjacentPoints.Add(rightPoint);

            foreach (var adjacentPoint in adjacentPoints)
            {
                visitedPoints[adjacentPoint] = true;
                var adjacentPointWithParent = new PointWithParent(adjacentPoint, point);
                pointsQueue.Enqueue(adjacentPointWithParent);
            }
        }

        return Array.Empty<Point>();
    }

    private bool TryTopPoint(Matrix<bool> visitedPoints, Point current, out Point result)
    {
        result = Point.Empty;
        if (current.Y <= 1)
            return false;

        result = current with { Y = current.Y - 1 };
        return IsPointAvailable(visitedPoints, result);
    }

    private bool TryBottomPoint(Matrix<bool> visitedPoints, Point current, out Point result)
    {
        result = Point.Empty;
        if (current.Y > visitedPoints.Height)
            return false;

        result = current with { Y = current.Y + 1 };
        return IsPointAvailable(visitedPoints, result);
    }

    private bool TryLeftPoint(Matrix<bool> visitedPoints, Point current, out Point result)
    {
        result = Point.Empty;
        if (current.X <= 1)
            return false;

        result = current with { X = current.X - 1 };
        return IsPointAvailable(visitedPoints, result);
    }

    private bool TryRightPoint(Matrix<bool> visitedPoints, Point current, out Point result)
    {
        result = Point.Empty;
        if (current.X > visitedPoints.Width)
            return false;

        result = current with { X = current.X + 1 };
        return IsPointAvailable(visitedPoints, result);
    }

    private bool IsPointAvailable(Matrix<bool> visitedPoints, Point candidate)
    {
        if (!FreeWayFunc(Matrix[candidate]))
            return false;

        return !visitedPoints[candidate];
    }
}