﻿using System.Drawing;

namespace Algorithms.Primitives;

public enum NeighborDirection
{
    Top,
    Left,
    Right,
    Bottom
}

public class Matrix<TItem>
{
    private readonly TItem[,] _data;

    public Matrix(int width, int height)
    {
        Width = width;
        Height = height;

        _data = new TItem[Width, Height];
    }

    public int Height { get; }

    public int Width { get; }

    public TItem this[int x, int y]
    {
        get => _data[x - 1, y - 1];
        set => _data[x - 1, y - 1] = value;
    }

    public TItem this[Point point]
    {
        get => this[point.X, point.Y];
        set => this[point.X, point.Y] = value;
    }

    public Matrix<TDestination> Convert<TDestination>(Func<TItem, TDestination> converter)
    {
        var matrix = new Matrix<TDestination>(Width, Width);

        for (var x = 0; x < Width; x++)
        for (var y = 0; y < Height; y++)
        {
            var item = _data[x, y];
            var destination = converter.Invoke(item);
            matrix[x, y] = destination;
        }

        return matrix;
    }

    public bool TryGetNeighbor(Point point, NeighborDirection direction, out Point neighborPoint)
    {
        neighborPoint = GetNeighborPointCandidate(point, direction);
        return neighborPoint.X >= 1 && neighborPoint.X <= Width && neighborPoint.Y <= Height && neighborPoint.Y <= Height;
    }
    
    private static Point GetNeighborPointCandidate(Point point, NeighborDirection direction)
    {
        return direction switch
        {
            NeighborDirection.Top => point with { Y = point.Y - 1 },
            NeighborDirection.Left => point with { X = point.X - 1 },
            NeighborDirection.Right => point with { X = point.X + 1 },
            NeighborDirection.Bottom => point with { Y = point.Y + 1 },
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}