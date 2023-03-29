using Algorithms.Search;

namespace Algorithms.Primitives;

public class Graph<TNodeKey> where TNodeKey : notnull
{
    public IDictionary<TNodeKey, GraphNode<TNodeKey>> Items { get; } = new Dictionary<TNodeKey, GraphNode<TNodeKey>>();

    public GraphNode<TNodeKey> AddNode(TNodeKey key)
    {
        var node = new GraphNode<TNodeKey>(key);
        Items.Add(key, node);
        return node;
    }
}