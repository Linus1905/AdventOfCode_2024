using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode_2024 {

  public class Day03 {

    private string path;
    private MatchCollection matchesMul;
    private Dictionary<int, int> mulResults = new();

    public Day03(string path) {
      this.path = path;
    }

    public void Solve() {
      Stopwatch sw = Stopwatch.StartNew();
      string input = readInput(path);

      long p1 = partOne(input);
      long p2 = partTwo(input);
      sw.Stop();

      Console.WriteLine($"Day03: {sw.ElapsedMilliseconds} ms");
      Console.WriteLine("Part One: " + p1);
      Console.WriteLine("Part Two: " + p2);
      Console.WriteLine("------------------------------------------------------------");
    }

    private string readInput(string path) {
      return File.ReadAllText(path);
    }

    private long partOne(string input) {
      long sum1 = 0;
      matchesMul = Regex.Matches(input, @"mul[(][0-9]{1,3},[0-9]{1,3}[)]");

      foreach (Match match in matchesMul) {
        string str = match.Value.Substring(4, match.Value.IndexOf(')') - 4);
        string[] split = str.Split(",");
        int x = int.Parse(split[0]); int y = int.Parse(split[1]);
        int mul = x * y; mulResults.Add(match.Index, mul);

        sum1 += mul;
      }

      return sum1;
    }

    private long partTwo(string input) {
      long sum2 = 0;
      MatchCollection doMatches = Regex.Matches(input, @"do[(][)]");
      MatchCollection dontMatches = Regex.Matches(input, @"don't[(][)]");

      foreach (Match match in matchesMul) {
        Match closestDo = getClosestMatch(match, doMatches);
        Match closestDont = getClosestMatch(match, dontMatches);

        int doInd = closestDo is null ? int.MaxValue : closestDo.Index;
        int dontInd = closestDont is null ? int.MaxValue : closestDont.Index;

        if ((closestDo is null && closestDont is null) ||
            Math.Abs(match.Index - doInd) < Math.Abs(match.Index - dontInd)) {
          sum2 += mulResults[match.Index];
        }
      }

      return sum2;
    }

    private Match getClosestMatch(Match match, MatchCollection matches) {
      Match closest = null;

      for(int i = 0; i < matches.Count; i++) {
        if (matches[i].Index < match.Index) {
          closest = matches[i];
        }
        else {
          break;
        }
      }

      return closest;
    }
  }

}
