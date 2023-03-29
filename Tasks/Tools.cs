namespace Tasks;

public static class Tools
{
    public static string[] ReadFromInput(string fileName, string subFolder = "")
    {
        return File.ReadAllLines($@"..\..\..{subFolder}\Input\{fileName}");
    }

    public static void WriteToOutput(string fileName, string[] content, string subFolder = "")
    {
        File.WriteAllLines($@"..\..\..{subFolder}\Output\{fileName}", content);
    }
}