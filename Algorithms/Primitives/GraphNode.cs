namespace Algorithms.Primitives;

public class GraphNode<TNodeKey> where TNodeKey : notnull
{
    public TNodeKey Key { get; }

    public IList<GraphNodeRoute<TNodeKey>> Routes { get; } = new List<GraphNodeRoute<TNodeKey>>();

    public GraphNode(TNodeKey key)
    {
        Key = key;
    }
}

public class GraphNodeRoute<TNodeKey> where TNodeKey : notnull
{
    public GraphNode<TNodeKey> Node { get; }
    public int Distance { get; }

    public GraphNodeRoute(GraphNode<TNodeKey> node, int distance)
    {
        Node = node;
        Distance = distance;
    }
}