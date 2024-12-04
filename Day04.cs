using System.Diagnostics;

namespace AdventOfCode_2024 {

  public class Day04 {

    private string path;
    private int[] dirX = new int[] { 1, 1, 1, 0, 0, -1, -1, -1 };
    private int[] dirY = new int[] { 1, 0, -1, 1, -1, 1, 0, -1 };

    public Day04(string path) {
      this.path = path;
    }

    public void Solve() {
      Stopwatch sw = Stopwatch.StartNew();
      string[] input = readInput(path);

      int p1 = partOne(input);
      int p2 = partTwo(input);
      sw.Stop();

      Console.WriteLine($"Day04: {sw.ElapsedMilliseconds} ms");
      Console.WriteLine("Part One: " + p1);
      Console.WriteLine("Part Two: " + p2);
      Console.WriteLine("------------------------------------------------------------");
    }

    private string[] readInput(string path) {
      return File.ReadAllLines(path);
    }

    private int partOne(string[] input) {
      int sum = 0;

      for (int x = 0; x < input.Length; x++) {
        for (int y = 0; y < input[0].Length; y++) {

          sum += findWord(input, x, y, "XMAS");
        }
      }

      return sum;
    }

    private int partTwo(string[] input) {
      int sum = 0;

      for (int x = 0; x < input.Length; x++) {
        for (int y = 0; y < input[0].Length; y++) {

          if (input[x][y] != 'A') continue;

          int x1 = x - 1, y1 = y - 1;
          int x2 = x - 1, y2 = y + 1;
          int x3 = x + 1, y3 = y - 1;
          int x4 = x + 1, y4 = y + 1;

          if (!checkBounds(x1, y1, input.Length, input[0].Length) ||
             !checkBounds(x2, y2, input.Length, input[0].Length) ||
             !checkBounds(x3, y3, input.Length, input[0].Length) ||
             !checkBounds(x4, y4, input.Length, input[0].Length)) {

            continue;
          }

          if (((input[x1][y1] == 'M' && input[x4][y4] == 'S') ||
                (input[x1][y1] == 'S' && input[x4][y4] == 'M')) &&
              ((input[x2][y2] == 'M' && input[x3][y3] == 'S') ||
                (input[x2][y2] == 'S' && input[x3][y3] == 'M'))) {

            sum++;
          }
        }
      }

      return sum;
    }

    private bool checkBounds(int x, int y, int h, int w) {
      return x >= 0 && y >= 0 && x < h && y < w;
    }

    private int findWord(string[] grid, int x, int y, string word) {
      int sum = 0;

      if (grid[x][y] != word[0]) {
        return sum;
      }

      int orgX = x, orgY = y;
      for (int i = 0; i < dirX.Length; i++) {

        x = orgX; y = orgY;
        bool match = true;

        for (int z = 1; z < word.Length; z++) {
          x += dirX[i];
          y += dirY[i];

          if (x < 0 || y < 0 || x >= grid.Length || y >= grid[0].Length) {
            match = false; break;
          }

          if (grid[x][y] != word[z]) {
            match = false; break;
          }
        }

        if (match) sum++;
      }

      return sum;
    }
  }
}
