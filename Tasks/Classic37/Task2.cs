using NUnit.Framework;

namespace Tasks.Classic37;

public class Node
{
    public Node Left { get; set; }

    public Node Right { get; set; }

    public char? Winner { get; set; }

    public int Round { get; set; }
}

[TestFixture]
public class Task2
{
    [TestCase("level3_1")]
    [TestCase("level3_2")]
    [TestCase("level3_3")]
    [TestCase("level3_4")]
    [TestCase("level3_5")]
    public void METHOD(string fileName)
    {
        var input = Tools.ReadFromInput($"10\\{fileName}.in", "\\Classic37");

        var tournaments = new List<string>();
        foreach (var line in input.Skip(1))
            tournaments.Add(line);

        var result = new List<string>();
        foreach (var item in tournaments)
        {
            var node = new Node();
            BuildNode(node, item);
            SolveNode(node);
            var s = new string(GetRoundWinners(node, 2).ToArray());
            result.Add(s);

            Console.WriteLine(node.Winner);
        }

        Tools.WriteToOutput($"10\\{fileName}.out", result.ToArray(), "\\Classic37");
    }

    private void BuildNode(Node node, string participants)
    {
        if (participants.Length > 1)
        {
            var leftParticipants = participants.Substring(0, participants.Length / 2);
            var rightParticipants = participants.Substring(participants.Length / 2);
            node.Left = new Node();
            node.Right = new Node();
            BuildNode(node.Left, leftParticipants);
            BuildNode(node.Right, rightParticipants);
        }
        else
        {
            node.Winner = participants[0];
            node.Round = 0;
        }
    }

    private void SolveNode(Node node)
    {
        if (!node.Left.Winner.HasValue && !node.Right.Winner.HasValue)
        {
            SolveNode(node.Left);
            SolveNode(node.Right);
        }
        node.Winner = Winner(node.Left.Winner.Value, node.Right.Winner.Value);
        node.Round = node.Left.Round + 1;
    }

    private IEnumerable<char> GetRoundWinners(Node node, int round)
    {
        if (node.Round == round)
        {
            yield return node.Winner.Value;
        }
        else
        {
            foreach (var v in GetRoundWinners(node.Left, round).Concat(GetRoundWinners(node.Right, round)))
            {
                yield return v;
            }
        }
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