using Algorithms.Primitives;

namespace Algorithms.Extensions;

public static class MatrixExtensions
{
    public static void FillFrom<T>(this Matrix<T> matrix, T[,] source)
    {
        for (var y = 0; y < source.GetLength(0); y++)
        for (var x = 0; x < source.GetLength(1); x++)
            matrix[x + 1, y + 1] = source[x, y];
    }

    public static void FillFromArrayOfStrings<T>(this Matrix<T> matrix, string[] strings,
        Func<string, T[]> stringToRowConverter)
    {
        for (var y = 1; y <= strings.Length; y++)
        {
            var line = strings[y - 1];
            var row = stringToRowConverter.Invoke(line);
            for (var x = 1; x <= row.Length; x++)
                matrix[x, y] = row[x - 1];
        }
    }

    public static void FillWith<T>(this Matrix<T> matrix, T value)
    {
        for (var y = 1; y <= matrix.Height; y++)
        for (var x = 1; x <= matrix.Width; x++)
            matrix[x, y] = value;
    }
}