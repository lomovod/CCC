namespace Tasks;

public static class Tools
{
    public static string[] ReadFromInput(string fileName) => File.ReadAllLines($@"..\..\..\Input\{fileName}");

    public static void WriteToOutput(string fileName, string[] content) => File.WriteAllLines($@"..\..\..\Output\{fileName}", content);
}