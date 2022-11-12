namespace Tasks;

public static class Extensions
{
    public static int ToInt32(this string value)
    {
        return int.Parse(value);
    }
}