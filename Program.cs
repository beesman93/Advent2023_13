using System.Reflection;
using System.Reflection.PortableExecutable;

List<List<string>> input_lines = new();
input_lines.Add(new());

using (StreamReader reader = new(args[0]))
{
    while(!reader.EndOfStream)
    {
        string a = reader.ReadLine();
        if(a=="")
            input_lines.Add(new());
        else
            input_lines.Last().Add(a);
    }
}

solve(false);
solve(true);

void solve(bool part2)
{
    int total = 0;
    foreach (List<string> lines in input_lines)
    {
        total += solveReflectionsOffByX(lines,part2 ? 1 : 0);
    }
    Console.WriteLine(total);
}

int compareLinesDiff(string a, string b)
{
    int diff = 0;
    for (int i = 0; i < a.Length; i++)
        if (a[i] != b[i])
            diff++;
    return diff;
}
int solveReflectionsOffByX(List<string> lines,int smudgeCount)
{
    int offByX = 0;
    for (int i = 0; i < lines.Count - 1; i++)
    {
        offByX = compareLinesDiff(lines[i], lines[i + 1]);
        if (offByX <= smudgeCount)
        {
            int steps = 1;
            while (i - steps >= 0 && (i + 1 + steps) < lines.Count)
            {
                offByX += compareLinesDiff(lines[i - steps], lines[i + 1 + steps]);
                if (offByX> smudgeCount)
                    break;
                steps++;
            }
            if (offByX== smudgeCount)
                return (i + 1) * 100;
        }
    }

    List<string> linesPivot = new();
    {
        for (int j = 0; j < lines[0].Length; j++)
        {
            linesPivot.Add("");
            foreach (string line in lines)
            {
                linesPivot[linesPivot.Count - 1] += line[j];
            }
        }
    }

    for (int i = 0; i < linesPivot.Count - 1; i++)
    {
        offByX = compareLinesDiff(linesPivot[i], linesPivot[i + 1]);
        if (offByX<= smudgeCount)
        {
            int steps = 1;
            while (i - steps >= 0 && i + 1 + steps < linesPivot.Count)
            {
                offByX += compareLinesDiff(linesPivot[i - steps], linesPivot[i + 1 + steps]);
                if (offByX> smudgeCount)
                    break;
                steps++;
            }
            if (offByX== smudgeCount)
            {
                return (i + 1);
            }
        }
    }
    return -1;
}