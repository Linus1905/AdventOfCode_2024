using System.Diagnostics;

namespace AdventOfCode_2024 {

  public class Day06 {

    private string path;
    private int[] dirX = new int[] { -1, 0, 1, 0 };
    private int[] dirY = new int[] { 0, 1, 0, -1 };
    private int curDir = 0; // 0 -> up, 1 -> right, 2 -> down, 3 -> left
    private int posX = 0, posY = 0;

    public Day06(string path) {
      this.path = path;
    }

    public void Solve() {
      Stopwatch sw = Stopwatch.StartNew();
      char[,] grid = readInput(path);

      int p1 = partOne(grid);
      int p2 = partTwo(grid);
      sw.Stop();

      Console.WriteLine($"Day06: {sw.ElapsedMilliseconds} ms");
      Console.WriteLine("Part One: " + p1);
      Console.WriteLine("Part Two: " + p2);
      Console.WriteLine("------------------------------------------------------------");
    }

    private char[,] readInput(string path) {
      string[] input = File.ReadAllLines(path);
      char[,] grid = new char[input.Length, input[0].Length];

      for (int i = 0; i < input.Length; i++) {
        for (int j = 0; j < input[i].Length; j++) {
          grid[i, j] = input[i][j];
        }
      }

      return grid;
    }

    private int partOne(char[,] grid) {
      findStartPos(grid);

      HashSet<string> visitedDistinct = new();

      int h = grid.GetLength(0), w = grid.GetLength(1);
      while (true) {
        int nx = posX + dirX[curDir], ny = posY + dirY[curDir];
        if (!checkBounds(nx, ny, h, w)) {
          break;
        }

        if (grid[nx, ny] == '#') {
          curDir = (curDir + 1) % 4;
          continue;
        }

        posX = nx; posY = ny;
        visitedDistinct.Add(posX + "," + posY);
      }

      return visitedDistinct.Count();
    }

    private void findStartPos(char[,] grid) {
      for (int i = 0; i < grid.Length; i++) {

        bool b = false;

        for (int j = 0; j < grid.GetLength(0); j++) {

          if (grid[i, j] == '^' || grid[i, j] == 'v' ||
              grid[i, j] == '<' || grid[i, j] == '>') {

            posX = i; posY = j;
            curDir = grid[i, j] switch {
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
    }

    private bool checkBounds(int x, int y, int h, int w) {
      return x >= 0 && y >= 0 && x < h && y < w;
    }

    private int partTwo(char[,] grid) {
      findStartPos(grid);
      int startX = posX, startY = posY, startDir = curDir;

      int sum = 0;
      for (int i = 0; i < grid.GetLength(0); i++) {
        for (int j = 0; j < grid.GetLength(1); j++) {

          if (grid[i, j] == '.') {
            grid[i, j] = '#';

            if (hasLoop(grid)) sum++;

            grid[i, j] = '.';
            resetToStartPos(grid, startX, startY, startDir);
          }
        }
      }

      return sum;
    }

    private void resetToStartPos(char[,] grid, int startX, int startY, int startDir) {
      posX = startX; posY = startY; curDir = startDir;
      grid[posX, posY] = curDir switch {
        0 => '^',
        1 => 'v',
        2 => '<',
        3 => '>'
      };
    }

    private bool hasLoop(char[,] grid) {
      // Punkte, an welchen die Richtung geändert wird + Richtung vor Änderung
      HashSet<string> turningPoints = new();

      int h = grid.GetLength(0), w = grid.GetLength(1);
      while (true) {
        int nx = posX + dirX[curDir], ny = posY + dirY[curDir];
        if (!checkBounds(nx, ny, h, w)) {
          return false;
        }

        if (grid[nx, ny] == '#') {
          string key = posX + "," + posY + "," + curDir;
          if (turningPoints.Contains(key)) {
            // Wendepunkt mit gleicher Ausrichtung wie schonmal erreicht -> Loop angenommen.
            return true;
          }

          turningPoints.Add(key);

          curDir = (curDir + 1) % 4;
          continue;
        }

        posX = nx; posY = ny;
      }
    }
  }

}
