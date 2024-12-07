using System.Diagnostics;

namespace AdventOfCode_2024 {

  public class Day07 {

    private string path;
    private List<char> operators = new List<char>() { '*', '+'};
    private List<Equation> equations = new();

    public Day07(string path) {
      this.path = path;
    }

    private void readInput(string path) {
      string[] input = File.ReadAllLines(path);
      foreach (string s in input) {
        string[] split = s.Split(":");

        long targetVal = long.Parse(split[0]);
        List<int> numbers = split[1].Trim().Split(" ").Select(x => int.Parse(x)).ToList();

        equations.Add(new Equation(targetVal, numbers));
      }
    }

    public void Solve() {
      Stopwatch sw = Stopwatch.StartNew();
      readInput(path);
      long p1 = partOne();
      long p2 = partTwo();
      sw.Stop();

      Console.WriteLine($"Day07: {sw.ElapsedMilliseconds} ms");
      Console.WriteLine("Part One: " + p1);
      Console.WriteLine("Part Two: " + p2);
      Console.WriteLine("------------------------------------------------------------");
    }

    private long partOne() {
      long sum = 0;

      foreach (Equation e in equations) {
        bool isTrue = false;
        isEquationTrue(e.TargetVal, e.Numbers, e.Numbers[0], 1, ref isTrue);

        if (isTrue) {
          sum += e.TargetVal;
        }
      }

      return sum;
    }

    private long partTwo() {
      long sum = 0;
      operators.Add('x'); // Zusätzlicher concat Operator

      foreach (Equation e in equations) {
        bool isTrue = false;
        isEquationTrue(e.TargetVal, e.Numbers, e.Numbers[0], 1, ref isTrue);

        if (isTrue) {
          sum += e.TargetVal;
        }
      }

      return sum;
    }


    private bool isEquationTrue(long targetVal, List<int> numbers, long curVal, int index, ref bool isTrue) {
      if (curVal == targetVal) {
        isTrue = true;
        return true;
      }

      if (index >= numbers.Count) {
        return false;
      }

      foreach (char c in operators) {
        long tmp = curVal;

        if (c == '*') curVal *= numbers[index];
        else if(c == '+') curVal += numbers[index];
        else curVal = concatNums(curVal, numbers[index]);

        isEquationTrue(targetVal, numbers, curVal, index + 1, ref isTrue);
        curVal = tmp;
      }

      return false;
    }

    // 1000 concat 42 = 1000 * 100 + 42 = 100042 (performanter als string concat + int.parse)
    private long concatNums(long a, long b) {
      //if (b < 10L) return 10L * a + b;
      //if (b < 100L) return 100L * a + b;
      //if (b < 1000L) return 1000L * a + b;
      //if (b < 10000L) return 10000L * a + b;
      //if (b < 100000L) return 100000L * a + b;
      //if (b < 1000000L) return 1000000L * a + b;
      //if (b < 10000000L) return 10000000L * a + b;
      //if (b < 100000000L) return 100000000L * a + b;
      //if (b < 1000000000L) return 1000000000L * a + b;
      //if (b < 10000000000L) return 10000000000L * a + b;
      //if (b < 100000000000L) return 100000000000L * a + b;
      //if (b < 1000000000000L) return 1000000000000L * a + b;
      //if (b < 10000000000000L) return 10000000000000L * a + b;
      //if (b < 100000000000000L) return 100000000000000L * a + b;
      //if (b < 1000000000000000L) return 1000000000000000L * a + b;
      //if (b < 10000000000000000L) return 10000000000000000L * a + b;
      //if (b < 100000000000000000L) return 100000000000000000L * a + b;
      //return 1000000000000000000L * a + b;
      return long.Parse(a + "" + b);
    }

    private class Equation {
      public long TargetVal { get; set; }

      public List<int> Numbers { get; set; }

      public Equation(long targetVal, List<int> numbers) {
        TargetVal = targetVal;
        Numbers = numbers;
      }
    }
  }

}
