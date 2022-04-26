using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KInLineCore;

namespace KInLineGame
{
    public partial class KInLineGame : Form
    {
        public KInLineGame()
        {
            InitializeComponent();
        }

        Game game = new Game();

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game = new Game(game.Rows, game.Cols, game.K);
            board.Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Do you want to leave the game?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                Application.Exit();
        }

        private void board_Paint(object sender, PaintEventArgs e)
        {
            var gr = e.Graphics;
            SizeF cellSize = new SizeF(board.ClientSize.Width / (float)game.Cols,
                board.ClientSize.Height / (float)game.Rows);

            for (int i = 0; i < game.Rows; i++)
            {
                for (int j = 0; j < game.Cols; j++)
                {
                    switch (game[i, j])
                    {
                        case Cells.Empty:
                            gr.DrawEllipse(Pens.Black, j * cellSize.Width, board.ClientSize.Height - (i + 1) * cellSize.Height, cellSize.Width,
                                cellSize.Height);
                            break;
                        case Cells.Red:
                            gr.FillEllipse(Brushes.Red, j * cellSize.Width, board.ClientSize.Height - (i + 1) * cellSize.Height, cellSize.Width,
                                cellSize.Height);
                            gr.DrawEllipse(Pens.Black, j * cellSize.Width, board.ClientSize.Height - (i + 1) * cellSize.Height, cellSize.Width,
                                cellSize.Height);
                            break;
                        case Cells.Yellow:
                            gr.FillEllipse(Brushes.Yellow, j * cellSize.Width, board.ClientSize.Height - (i + 1) * cellSize.Height, cellSize.Width,
                                cellSize.Height);
                            gr.DrawEllipse(Pens.Black, j * cellSize.Width, board.ClientSize.Height - (i + 1) * cellSize.Height, cellSize.Width,
                                cellSize.Height);
                            break;
                    }
                }
            }
        }

        private void board_Resize(object sender, EventArgs e)
        {
            board.Invalidate();
        }

        private void board_MouseClick(object sender, MouseEventArgs e)
        {
            SizeF cellSize = new SizeF(board.ClientSize.Width / (float)game.Cols,
                board.ClientSize.Height / (float)game.Rows);
            int col = (int)(e.X / cellSize.Width);

            if (game.CanPlay(col))
            {
                game.Play(col);
                board.Invalidate();

                if (game.IsGameOver)
                    MessageBox.Show("Game Over", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Invalid Column", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game = new Game(8, 8, 4);
            board.Invalidate();
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game = new Game(10, 10, 5);
            board.Invalidate();
        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string information = "App : K in Line v2.0 \n";
            information += "Author : Ariel Plasencia Díaz \n";

            MessageBox.Show(information, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string help = "Posición inicial y objetivo del juego:\n";
            help += "El K en Línea pertenece a la familia de juegos populares ya que sus sencillas reglas se pueden aprender rápidamente ofreciendo un gran entretenimiento durante un largo tiempo. El objetivo de este juego consiste en colocar cuatro fichas en una fila contínua vertical horizontal o diagonalmente. Se juega sobre un tablero que al empezar está vacío.\n";

            help += "\n¿Cómo colocar las fichas?\n";
            help += "Ambos jugadores sitúan sus fichas(una por movimiento) en el tablero.La regla para colocarlas consiste en que la estas siempre caen hasta abajo. Es decir una ficha puede ser colocada bien en la parte inferior de una columna o bien sobre otra de alguna otra columna. La siguiente imagen muestra un ejemplo de la posición de una partida en curso donde las cruces verdes señalan las casillas donde el jugador puede colocar una nueva ficha.\n";

            help += "\n¿Cómo finalizar el juego?\n";
            help += "La partida termina si una de las siguientes condiciones se cumple:\n";
            help += "1. Uno de los jugadores coloca cuatro o más fichas en una línea contínua vertical, horizontal o diagonalmente. Este jugador gana la partida.\n";
            help += "2. Todas las casillas del tablero están ocupadas y ningún jugador cumple la condición anterior para ganar. En este caso la partida finaliza en empate.\n";

            MessageBox.Show(help, "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
