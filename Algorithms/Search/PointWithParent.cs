using System.Drawing;

namespace Algorithms.Search;

internal class PointWithParent
{
    public PointWithParent(Point point, PointWithParent? parent)
    {
        Point = point;
        Parent = parent;
    }

    public Point Point { get; }
    public PointWithParent? Parent { get; }
}