using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KInLineCore
{
    public class Game
    {
        Cells[,] cells;
        Cells currentPlayer;
        int[] counts;
        private bool gameOver = false;

        public Game(int rows, int cols, int K)
        {
            this.cells = new Cells[rows, cols];
            this.currentPlayer = Cells.Red;
            this.counts = new int[cols];
            this.K = K;
        }

        public Game() : this(8, 8, 4)
        {
        }

        public Cells this[int row, int col]
        {
            get { return cells[row, col]; }
        }

        public int Rows
        {
            get
            {
                return cells.GetLength(0);
            }
        }

        public int Cols
        {
            get
            {
                return cells.GetLength(1);
            }
        }

        public int K { get; private set; }

        public Cells CurrentPlayer {  get { return currentPlayer; } }

        public bool IsGameOver { get { return gameOver; } }

        public void Play(int col)
        {
            if (!CanPlay(col))
                throw new InvalidOperationException();

            cells[counts[col], col] = currentPlayer;
            counts[col]++;

            CheckWinCondition();

            if (!gameOver)
            {
                currentPlayer = currentPlayer == Cells.Red ? Cells.Yellow: Cells.Red;
            }
        }

        private void CheckWinCondition()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (this[i, j] != Cells.Empty)
                    {
                        for (int d = 0; d < 4; d++)
                            if (ThereIsKInLine(i, j, d))
                            {
                                gameOver = true;
                                break;
                            }
                    }
                }
            }

            if (counts.All(c => c == K))
            {
                if (!gameOver)
                {
                    gameOver = true;
                    currentPlayer = Cells.Empty;
                }
            }
        }

        private bool ThereIsKInLine(int row, int col, int dir)
        {
            int[] dr = { 0, 1, 1, 1 };
            int[] dc = { 1, 1, 0, -1 };

            for (int k = 1; k < this.K; k++)
            {
                int nextR = row + dr[dir] * k;
                int nextC = col + dc[dir] * k;
                if (!IsIn(nextR, nextC) || cells[nextR, nextC] != cells[row, col])
                    return false;
            }

            return true;
        }

        private bool IsIn(int nextR, int nextC)
        {
            return nextR >= 0 && nextR < Rows && nextC >= 0 && nextC < Cols;
        }

        public bool CanPlay(int col)
        {
            return !gameOver && counts[col] < Rows;
        }
    }

    public enum Cells
    {
        Empty,
        Red,
        Yellow
    }
}
