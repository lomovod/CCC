using Algorithms.Primitives;

namespace Algorithms.Search;

public class DijkstraRouteMap<TNodeKey> where TNodeKey : notnull
{
    private readonly IDictionary<TNodeKey, int> _distances;
    private readonly Graph<TNodeKey> _graph;

    public DijkstraRouteMap(Graph<TNodeKey> graph, IDictionary<TNodeKey, int> distances)
    {
        _distances = distances;
        _graph = BuildReverseRouteGraph(graph);
    }

    public GraphNodeRoute<TNodeKey>[] FindShortestRoute(TNodeKey destinationPoint)
    {
        var distance = _distances[destinationPoint];
        var targetNode = _graph.Items[destinationPoint];
        
        var route = new List<GraphNodeRoute<TNodeKey>>();

        while (distance > 0)
        {
            foreach (var nodeRoute in targetNode.Routes)
            {
                var nodeDistance = _distances[nodeRoute.Node.Key];
                if (nodeDistance + nodeRoute.Distance == distance)
                {
                    route.Add(new GraphNodeRoute<TNodeKey>(targetNode, nodeRoute.Distance));
                    
                    distance -= nodeRoute.Distance;
                    targetNode = nodeRoute.Node;
                    break;
                }
            }
        }

        route.Add(new GraphNodeRoute<TNodeKey>(targetNode, 0));

        return route.ToArray().Reverse().ToArray();
    }

    private static Graph<TNodeKey> BuildReverseRouteGraph(Graph<TNodeKey> sourceGraph)
    {
        var targetGraph = new Graph<TNodeKey>();
        foreach (var sourceGraphItem in sourceGraph.Items)
        {
            targetGraph.Items.Add(sourceGraphItem.Key, new GraphNode<TNodeKey>(sourceGraphItem.Key));
        }

        foreach (var sourceGraphItem in sourceGraph.Items)
        {
            foreach (var sourceGraphItemRoute in sourceGraphItem.Value.Routes)
            {
                var targetNode = targetGraph.Items[sourceGraphItemRoute.Node.Key];
                var sourceNode = targetGraph.Items[sourceGraphItem.Key];
                
                targetNode.Routes.Add(new GraphNodeRoute<TNodeKey>(sourceNode, sourceGraphItemRoute.Distance));
            }
        }

        return targetGraph;
    }
}