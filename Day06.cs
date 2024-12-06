using System.Diagnostics;

namespace AdventOfCode_2024 {

  public class Day06 {

    private string path;
    private int[] dirX = new int[] { -1, 0, 1, 0 };
    private int[] dirY = new int[] { 0, 1, 0, -1 };

    public Day06(string path) {
      this.path = path;
    }

    public void Solve() {
      Stopwatch sw = Stopwatch.StartNew();
      string[] input = readInput(path);

      int p1 = partOne(input);
      int p2 = partTwo();
      sw.Stop();

      Console.WriteLine($"Day06: {sw.ElapsedMilliseconds} ms");
      Console.WriteLine("Part One: " + p1);
      Console.WriteLine("Part Two: " + p2);
      Console.WriteLine("------------------------------------------------------------");
    }

    private string[] readInput(string path) {
      return File.ReadAllLines(path);
    }

    private int partOne(string[] grid) {
      int curDir = 0; // 0 -> up, 1 -> right, 2 -> down, 3 -> left
      int posX = 0, posY = 0;

      for (int i = 0; i < grid.Length; i++) {

        bool b = false;

        for (int j = 0; j < grid[0].Length; j++) {

          if (grid[i][j] == '^' || grid[i][j] == 'v' ||
              grid[i][j] == '<' || grid[i][j] == '>') {

            posX = i; posY = j;
            curDir = grid[i][j] switch {
              '^' => 0,
              'v' => 1,
              '<' => 2,
              '>' => 3
            };
            b = true;
            break;
          }
        }

        if (b) break;
      }

      HashSet<string> visitedDistinct = new();

      int h = grid.Length, w = grid[0].Length;
      while (true) {
        int nx = posX + dirX[curDir], ny = posY + dirY[curDir];
        if (!checkBounds(nx, ny, h, w)) {
          break;
        }

        if (grid[nx][ny] == '#') {
          curDir = (curDir + 1) % 4;
          continue;
        }

        posX = nx; posY = ny;
        visitedDistinct.Add(posX + "," + posY);
      }

      return visitedDistinct.Count();
    }

    private bool checkBounds(int x, int y, int h, int w) {
      return x >= 0 && y >= 0 && x < h && y < w;
    }

    private int partTwo() {
      int sum = 0;

      return sum;
    }
  }
}
