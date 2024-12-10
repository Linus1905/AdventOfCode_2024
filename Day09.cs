using System.Diagnostics;

namespace AdventOfCode_2024 {

  public class Day09 {

    private string path;

    public Day09(string path) {
      this.path = path;
    }

    private string readInput(string path) {
      return File.ReadAllText(path).TrimEnd();
    }

    public void Solve() {
      Stopwatch sw = Stopwatch.StartNew();
      string input = readInput(path);
      long p1 = partOne(input);
      long p2 = partTwo(input);
      sw.Stop();

      Console.WriteLine($"Day09: {sw.ElapsedMilliseconds} ms");
      Console.WriteLine("Part One: " + p1);
      Console.WriteLine("Part Two: " + p2);
      Console.WriteLine("------------------------------------------------------------");
    }

    private long partOne(string input) {
      int[] disk = getDisk(input);
      disk = moveBlocks(disk);
      long checksum = getChecksum(disk);

      return checksum;
    }

    private int[] getDisk(string input) {
      int l = 0;
      for (int i = 0; i < input.Length; i++) {
        l += input[i] - '0';
      }

      int[] disk = new int[l]; int ind = 0, id = 0;
      for (int i = 0; i < input.Length; i++) {
        int c = input[i] - '0';

        if (i % 2 == 1) {
          for (int x = 0; x < c; x++) {
            disk[ind] = -1;
            ind++;
          }
        } else {
          for (int x = 0; x < c; x++) {
            disk[ind] = id;
            ind++;
          }
          id++;
        }
      }

      return disk;
    }

    private List<int> getFreeBlocks(int[] disk) {
      List<int> freeBlocks = new();

      for (int i = 0; i < disk.Length; i++) {
        if (disk[i] == -1) freeBlocks.Add(i);
      }

      return freeBlocks;
    }

    private int[] moveBlocks(int[] disk) {
      List<int> freeBlocks = getFreeBlocks(disk);

      int ind = disk.Length - 1;
      while (freeBlocks.Count > 0 && ind > 0) {

        if (disk[ind] == -1) {
          freeBlocks.RemoveAt(freeBlocks.Count - 1);
        } else {
          int fIndex = freeBlocks[0];
          disk[fIndex] = disk[ind];
          disk[ind] = -1;
          freeBlocks.RemoveAt(0);
        }

        ind--;
      }

      return disk;
    }

    private class Pair {
      public int x { get; set; }
      public int y { get; set; }

      public Pair(int x, int y) {
        this.x = x;
        this.y = y;
      }

      public int getAbs() {
        return y - x;
      }
    }

    private long partTwo(string input) {
      int[] disk = getDisk(input);
      disk = moveBlocks2(disk);
      long checksum = getChecksum(disk);

      return checksum;
    }

    private List<Pair> getFreeBlocks2(int[] disk) {
      List<Pair> freeBlocks = new();

      int startInd = -1;
      for (int i = 0; i < disk.Length; i++) {
        if (disk[i] == -1 && startInd == -1) startInd = i;
        else if (disk[i] != -1 && startInd != -1) {
          freeBlocks.Add(new Pair(startInd, i - 1));
          startInd = -1;
        }
      }

      return freeBlocks;
    }

    private int[] moveBlocks2(int[] disk) {
      List<Pair> freeBlocks = getFreeBlocks2(disk);
      HashSet<int> triedMove = new();

      int ind = disk.Length - 1;
      int endInd = -1, curId = -1;
      while (true) {
        if (freeBlocks.Count == 0 || ind < 0) break;

        if (disk[ind] != -1 && endInd == -1) {
          curId = disk[ind];
          endInd = ind;
        } else if (disk[ind] != curId && endInd != -1) {

          if (!triedMove.Contains(curId)) {
            for (int i = 0; i < freeBlocks.Count; i++) {
              int startInd = ind + 1;
              Pair block = freeBlocks[i];
              if (startInd < block.x) break;

              int d = endInd - startInd + 1;

              if (block.getAbs() + 1 >= d) {
                // Kompletten Block verschieben 
                for (int x = block.x; x < block.x + d; x++) {
                  disk[x] = disk[startInd];
                }
                for (int x = startInd; x <= endInd; x++) {
                  disk[x] = -1;
                }

                block.x += d;

                if (block.x > block.y) freeBlocks.RemoveAt(i);
                break;
              }
            }
            // Block mit ID wurde versuchct zu verschieben.
            triedMove.Add(curId);
          }

          if (disk[ind] != -1 && disk[ind] != curId) {
            curId = disk[ind];
            endInd = ind;
          } 
          else {
            endInd = -1;
          }
        }

        ind--;
      }

      return disk;
    }

    private long getChecksum(int[] disk) {
      long sum = 0;

      for (int i = 0; i < disk.Length; i++) {
        if (disk[i] == -1) continue;
        sum += disk[i] * i;
      }

      return sum;
    }

    private void printDisk(int[] disk) {
      Console.WriteLine();
      for (int i = 0; i < disk.Length; i++) {
        string s = disk[i] < 0 ? "." : disk[i].ToString();
        Console.Write(s);
      }
    }

  }
}
