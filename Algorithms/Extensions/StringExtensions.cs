namespace Algorithms.Extensions;

public static class StringExtensions
{
    public static int ToInt32(this string value)
    {
        return int.Parse(value);
    }
}