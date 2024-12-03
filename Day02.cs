using System.Diagnostics;

namespace AdventOfCode_2024 {

  public class Day02 {

    private string path;
    private List<List<int>> input;
    private int partOne, partTwo;

    public Day02(string path) {
      this.path = path;
    }

    public void Solve() {
      Stopwatch sw = Stopwatch.StartNew();
      input = readInput(path);

      solve();
      sw.Stop();

      Console.WriteLine($"Day02: {sw.ElapsedMilliseconds} ms");
      Console.WriteLine("Part One: " + partOne);
      Console.WriteLine("Part Two: " + partTwo);
      Console.WriteLine("------------------------------------------------------------");
    }

    private List<List<int>> readInput(string path) {
      List<List<int>> result = new();
      string[] lines = File.ReadAllLines(path);

      foreach(string l in lines) {
        string[] split = l.Split(" ");

        List<int> li = new();
        foreach(string s in split) {
          li.Add(int.Parse(s));
        }

        result.Add(li);
      }

      return result;
    }

    private void solve() {
      foreach(List<int> l in input) {
        if (isSafe(l)) {
          partOne++;
        }
        else { // Nicht schön:(
          for(int i = 0; i < l.Count; i++) {
            List<int> tmp = new List<int>(l); tmp.RemoveAt(i);
            
            if(isSafe(tmp)) {
              partTwo++;
              break;
            }
          }
        }
      }

      partTwo += partOne;
    }

    private bool isSafe(List<int> li) {
      bool decrease = li[0] > li[1] ? true : false;

      for (int i = 0; i < li.Count - 1; i++) {
        if (decrease) {
          if (li[i + 1] >= li[i]) return false;
          if (li[i + 1] < li[i] - 3) return false;
        }
        else {
          if (li[i + 1] <= li[i]) return false;
          if (li[i + 1] > li[i] + 3) return false;
        }
      }

      return true;
    }
  }

}
