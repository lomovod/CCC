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
            foreach (var neighborDirection in Enum.GetValues<NeighborDirection>())
            {
                if (TryPoint(visitedPoints, point.Point, neighborDirection, out var neighborPoint))
                {
                    adjacentPoints.Add(neighborPoint);
                }
            }

            foreach (var adjacentPoint in adjacentPoints)
            {
                visitedPoints[adjacentPoint] = true;
                var adjacentPointWithParent = new PointWithParent(adjacentPoint, point);
                pointsQueue.Enqueue(adjacentPointWithParent);
            }
        }

        return Array.Empty<Point>();
    }

    private bool TryPoint(Matrix<bool> visitedPoints, Point current, NeighborDirection neighborDirection,
        out Point result)
    {
    }
    
    private bool IsPointAvailable(Matrix<bool> visitedPoints, Point candidate)
    {
        if (!FreeWayFunc(Matrix[candidate]))
            return false;

        return !visitedPoints[candidate];
    }
}