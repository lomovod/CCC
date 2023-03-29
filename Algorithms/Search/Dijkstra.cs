using Algorithms.Primitives;

namespace Algorithms.Search;

public class Dijkstra<TNodeKey> where TNodeKey : notnull
{
    private readonly Graph<TNodeKey> _graph;

    public Dijkstra(Graph<TNodeKey> graph)
    {
        _graph = graph;
    }

    public DijkstraRouteMap<TNodeKey> GetRouteMap(TNodeKey startingPoint)
    {
        var visitedPoints = new Dictionary<TNodeKey, int>();
        var discovery = new Dictionary<TNodeKey, int> { [startingPoint] = 0 };
        while (discovery.Any())
        {
            var sourceNode = discovery.MinBy(pair => pair.Value);
            visitedPoints.Add(sourceNode.Key, sourceNode.Value);
            discovery.Remove(sourceNode.Key);

            foreach (var targetNode in _graph.Items[sourceNode.Key].Routes.OrderBy(route => route.Distance))
            {
                if (visitedPoints.ContainsKey(targetNode.Node.Key))
                {
                    continue;
                }
                
                if (!discovery.TryGetValue(targetNode.Node.Key, out var nodeLabel))
                {
                    nodeLabel = int.MaxValue;
                    discovery[targetNode.Node.Key] = nodeLabel;
                }

                var newNodeDistanceCandidate = sourceNode.Value + targetNode.Distance;
                if (newNodeDistanceCandidate < nodeLabel)
                {
                    discovery[targetNode.Node.Key] = newNodeDistanceCandidate;
                }
            }
        }

        return new DijkstraRouteMap<TNodeKey>(_graph, visitedPoints);
    }
}