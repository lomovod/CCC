namespace Tasks;

public class Matrix<TItem>
{
    private readonly TItem[,] _data;
    private readonly int _height;
    private readonly int _width;

    public Matrix(int width, int height)
    {
        _width = width;
        _height = height;

        _data = new TItem[_width, _height];
    }

    public TItem this[int x, int y]
    {
        get => _data[x - 1, y - 1];
        set => _data[x - 1, y - 1] = value;
    }

    public Matrix<TDestination> Convert<TDestination>(Func<TItem, TDestination> converter)
    {
        var matrix = new Matrix<TDestination>(_width, _width);

        for (var x = 0; x < _width; x++)
        for (var y = 0; y < _height; y++)
        {
            var item = _data[x, y];
            var destination = converter.Invoke(item);
            matrix[x, y] = destination;
        }

        return matrix;
    }
}