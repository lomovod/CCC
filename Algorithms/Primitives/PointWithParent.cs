using System.Drawing;

namespace Algorithms.Primitives;

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