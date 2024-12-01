using System.Diagnostics;
using System.Runtime.Serialization;

namespace AdventOfCode_2024 {

  public class Day01 {

    private string path;
    private List<int> l1 = new(), l2 = new();

    public Day01(string path) {
      this.path = path;  
    }

    public void Solve() {
      Stopwatch sw = Stopwatch.StartNew();
      readInput(path);
      int p1 = partOne();
      long p2 = partTwo();
      sw.Stop();
      
      Console.WriteLine($"Day01: {sw.ElapsedMilliseconds} ms");
      Console.WriteLine("Part One: " + p1);
      Console.WriteLine("Part Two: " + p2);
      Console.WriteLine("------------------------------------------------------------");
    }

    private int partOne() {
      int distance = 0;
      l1.Sort(); l2.Sort();

      for (int i = 0; i < l1.Count; i++) {
        distance += Math.Abs(l1[i] - l2[i]);
      }

      return distance;
    }

    private long partTwo() {
      long score = 0;

      Dictionary<int, int> totals2 = new();
      foreach (int i in l2) {
        if (totals2.ContainsKey(i)) totals2[i]++;
        else totals2.Add(i, 1);
      }

      for (int i = 0; i < l1.Count; i++) {
        int x = l1[i];
        if (totals2.TryGetValue(x, out int val)) {
          score += x * val;
        }
      }

      return score;
    }

    private void readInput(string path) {
      string[] input = File.ReadAllLines(path);
      foreach (string s in input) {
        string[] split = s.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        l1.Add(int.Parse(split[0]));
        l2.Add(int.Parse(split[1]));
      }
    }
  }

}
