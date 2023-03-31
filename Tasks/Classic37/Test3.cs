using NUnit.Framework;

namespace Tasks.Classic37;

[TestFixture]
public class Task3
{
    [TestCase("level3_1")]
    [TestCase("level3_2")]
    [TestCase("level3_3")]
    [TestCase("level3_4")]
    [TestCase("level3_5")]
    [TestCase("level3_6")]
    public void METHOD(string fileName)
    {
        var input = Tools.ReadFromInput($"3\\{fileName}.in", "\\Classic37");

        var list = new List<string>();
        foreach (var line in input.Skip(1))
        {
            var dictionary = line.Split(' ')
                .ToDictionary(s => s[s.Length - 1], s => int.Parse(s.Substring(0, s.Length - 1)));

            var result = string.Empty;

            while (dictionary['R'] > 0)
            {
                var rs = dictionary['R'] >= 3 ? 3 : dictionary['R'];
                dictionary['R'] -= rs;
                //var ps = 4 - rs;
                var ps = dictionary['P'] >= 4 - rs ? 4 - rs : dictionary['P'];
                dictionary['P'] -= ps;
                var ss = 4 - ps - rs;
                dictionary['S'] -= ss;

                var s = new string(Enumerable.Repeat('R', rs)
                    .Concat(Enumerable.Repeat('P', ps))
                    .Concat(Enumerable.Repeat('S', ss)).ToArray());
                if (s == "RRPS")
                    s = "RPRS";
                result += s;
            }

            while (dictionary['P'] > 0 || dictionary['S'] > 0)
            {
                var ps = dictionary['P'] >= 4 ? 4 : dictionary['P'];
                dictionary['P'] -= ps;
                var ss = 4 - ps;
                dictionary['S'] -= ss;
                var s = new string(Enumerable.Repeat('P', ps).Concat(Enumerable.Repeat('S', ss)).ToArray());
                result += s;
            }

            list.Add(result);
        }

        Tools.WriteToOutput($"3\\{fileName}.out", list.ToArray(), "\\Classic37");
    }


    private char Winner(char left, char right)
    {
        if ((left == 'P' && right == 'R') || (left == 'R' && right == 'P') || (left == 'P' && right == 'P'))
            return 'P';

        if ((left == 'S' && right == 'R') || (left == 'R' && right == 'S') || (left == 'R' && right == 'R'))
            return 'R';

        if ((left == 'P' && right == 'S') || (left == 'S' && right == 'P') || (left == 'S' && right == 'S'))
            return 'S';

        throw new NotImplementedException();
    }
}