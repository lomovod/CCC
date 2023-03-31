using NUnit.Framework;

namespace Tasks.Classic37;

[TestFixture]
public class Test4
{
    [TestCase("level4_1")]
    [TestCase("level4_2")]
    [TestCase("level4_3")]
    [TestCase("level4_4")]
    [TestCase("level4_5")]
    [TestCase("level4_6")]
    public void METHOD(string fileName)
    {
        var input = Tools.ReadFromInput($"4\\{fileName}.in", "\\Classic37");

        var list = new List<string>();
        foreach (var line in input.Skip(1))
        {
            var dictionary = line.Split(' ')
                .ToDictionary(s => s[s.Length - 1], s => int.Parse(s.Substring(0, s.Length - 1)));

            var result = string.Empty;

            var max = dictionary.Values.Sum() / 4;

            while (true)
            {
                var half = GetHalf(dictionary);
                result += half;
                if (string.IsNullOrEmpty(half))
                    break;
            }

            //var max = max / 4;
            while (dictionary['R'] > 0)
            {
                var rs = dictionary['R'] >= max ? max : dictionary['R'];
                dictionary['R'] -= rs;
                var ps = dictionary['P'] >= max - rs ? max - rs : dictionary['P'];
                dictionary['P'] -= ps;
                var ss = max  - ps - rs;
                dictionary['S'] -= ss;

                var a = new string(Enumerable.Repeat('R', rs)
                    .Concat(Enumerable.Repeat('P', ps))
                    .Concat(Enumerable.Repeat('S', ss)).ToArray());
                // if (a == "RRPS")
                //     a = "RPRS";

                //a = a.Replace("RRPS", "RPRS");

                var strings = Split(a, 2).ToList();
                var indexOf = strings.ToList().IndexOf("PS");
                if (indexOf > -1)
                {
                    strings[0] = "S" + strings[0][1];
                    strings[indexOf] = "PR";
                    a = string.Join("", strings);
                }


                result += a; //GetHalf2(dictionary);
            }


            var s = new string(Enumerable.Repeat('P', dictionary['P']).Concat(Enumerable.Repeat('S', dictionary['S'])).ToArray());
            result += s;



            list.Add(result);
        }

        Tools.WriteToOutput($"4\\{fileName}.out", list.ToArray(), "\\Classic37");
    }

    static IEnumerable<string> Split(string str, int chunkSize)
    {
        return Enumerable.Range(0, str.Length / chunkSize)
            .Select(i => str.Substring(i * chunkSize, chunkSize));
    }

    private string GetHalf(Dictionary<char, int> dictionary)
    {
        var max = dictionary.Values.Sum() / 2;
        if (dictionary['R'] < max)
        {
            return string.Empty;
        }

        if (max < 4)
        {
            max = dictionary.Values.Sum();
        }

        var rs = dictionary['R'] >= max - 1 ? max - 1 : dictionary['R'];
        dictionary['R'] -= rs;
        var ps = dictionary['P'] >= max - rs ? max - rs : dictionary['P'];
        dictionary['P'] -= ps;
        var ss = max - ps - rs;
        dictionary['S'] -= ss;

        var s = new string(Enumerable.Repeat('R', rs)
            .Concat(Enumerable.Repeat('P', ps))
            .Concat(Enumerable.Repeat('S', ss)).ToArray());
        // if (s == "RRPS")
        //     s = "RPRS";
        //s = s.Replace("RRPS", "RPRS");

        var strings = Split(s, 2).ToList();
        var indexOf = strings.ToList().IndexOf("PS");
        if (indexOf > -1)
        {
            strings[0] = "S" + strings[0][1];
            strings[indexOf] = "PR";
            s = string.Join("", strings);
        }


        return s;
    }

    private string GetHalf2(Dictionary<char, int> dictionary)
    {
        var max = dictionary.Values.Sum() / 2;
        if (max < 4)
        {
            max = dictionary.Values.Sum();
        }

        var rs = dictionary['R'] >= max  ? max  : dictionary['R'];
        dictionary['R'] -= rs;
        var ps = dictionary['P'] >= max - rs ? max - rs : dictionary['P'];
        dictionary['P'] -= ps;
        var ss = max - ps - rs;
        dictionary['S'] -= ss;

        var s = new string(Enumerable.Repeat('R', rs)
            .Concat(Enumerable.Repeat('P', ps))
            .Concat(Enumerable.Repeat('S', ss)).ToArray());
        // if (s == "RRPS")
        //     s = "RPRS";
        s = s.Replace("RRPS", "RPRS");

        return s;
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