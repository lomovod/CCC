using System.Drawing;
using Algorithms.Primitives;

namespace Algorithms.Extensions;

public static class GraphExtensions
{
    public static Graph<Point> ToGraph<TNodeKey>(this Matrix<TNodeKey> matrix, Func<TNodeKey, bool> validNodeFunc)
        where TNodeKey : notnull
    {
        var graph = new Graph<Point>();
        for (var y = 1; y <= matrix.Height; y++)
        for (var x = 1; x <= matrix.Width; x++)
        {
            var point = new Point(x, y);
            var item = matrix[point];
            if (validNodeFunc(item)) graph.AddNode(point);
        }

        for (var y = 1; y <= matrix.Height; y++)
        for (var x = 1; x <= matrix.Width; x++)
        {
            var point = new Point(x, y);
            if (!graph.Items.TryGetValue(point, out var graphItem)) continue;
            
            foreach (var direction in Enum.GetValues<NeighborDirection>())
                if (matrix.TryGetNeighbor(point, direction, out var neighborPoint) &&
                    graph.Items.TryGetValue(neighborPoint, out var neighborGraphItem))
                    graphItem.Routes.Add(new GraphNodeRoute<Point>(neighborGraphItem, 1));
        }

        return graph;
    }
}