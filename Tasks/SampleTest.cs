using NUnit.Framework;

namespace Tasks;

public class SampleTest
{
    [Test]
    public void TestFiles()
    {
        var input = Tools.ReadFromInput("Test.txt");
        Tools.WriteToOutput("Test2.txt", input);
    }
}