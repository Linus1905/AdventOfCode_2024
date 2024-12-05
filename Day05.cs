using System.Diagnostics;

namespace AdventOfCode_2024 {

  public class Day05 {

    private string path;
    private Dictionary<int, HashSet<int>> orderingRules = new();
    private List<List<int>> updateNumbers = new();
    private List<List<int>> invalidOrderings = new();

    public Day05(string path) {
      this.path = path;
    }

    public void Solve() {
      Stopwatch sw = Stopwatch.StartNew();
      readInput(path);

      int p1 = partOne();
      int p2 = partTwo();
      sw.Stop();

      Console.WriteLine($"Day05: {sw.ElapsedMilliseconds} ms");
      Console.WriteLine("Part One: " + p1);
      Console.WriteLine("Part Two: " + p2);
      Console.WriteLine("------------------------------------------------------------");
    }

    private void readInput(string path) {
      using (StreamReader sr = File.OpenText(path)) {
        string s = string.Empty;
        bool flag = false;

        while ((s = sr.ReadLine()) != null) {
          if (string.IsNullOrWhiteSpace(s)) {
            flag = true;
            continue;
          }

          if (flag) {
            updateNumbers.Add(s.Split(",").Select(e => int.Parse(e)).ToList<int>());
          } else {
            string[] split = s.Split("|");
            int x = int.Parse(split[0]), y = int.Parse(split[1]);

            if (orderingRules.ContainsKey(x)) {
              orderingRules[x].Add(y);
            } else {
              orderingRules.Add(x, new HashSet<int>() { y });
            }
          }
        }
      }
    }

    private int partOne() {
      int sum = 0;

      foreach(List<int> l in updateNumbers) {
        if (isValidOrdering(l, false)) {
          sum += l[l.Count / 2];
        }
        else {
          invalidOrderings.Add(l);
        }
      }

      return sum;
    }

    private bool isValidOrdering(List<int> list, int index, bool swapWrongOrdered) {
      if(!orderingRules.ContainsKey(list[index])) {
        return true;
      }

      HashSet<int> ordering = orderingRules[list[index]];
      bool valid = true;

      for (int i = 0; i < list.Count; i++) {
        if (ordering.Contains(list[i]) && index > i) {
          
          if(swapWrongOrdered) {
            int tmp = list[i];
            list[i] = list[index];
            list[index] = tmp;
          }
          valid = false; break;
        }
      }

      return valid;
    }

    private bool isValidOrdering(List<int> list, bool swapWrongOrdered) {
      bool valid = true;
      for (int i = 0; i < list.Count; i++) {
        valid = isValidOrdering(list, i, swapWrongOrdered);
       
        if (!valid) {
          return valid;
        }
      }

      return valid;
    }

    private int partTwo() {
      int sum = 0;

      foreach(List<int> l in invalidOrderings) {

        int c = 0;
        while(true) { // Nicht schön :(, vorallem mit dem bool Parameter
          c++;

          bool valid = isValidOrdering(l, true);

          if(valid) {
            sum += l[l.Count / 2];
            break;
          }

          if (c > 10000) { // sollte theoretisch nicht passieren...
            Console.WriteLine("Error Ordering: " + l.ToString());
            break;
          }
        }
      }

      return sum;
    }
  }
}
