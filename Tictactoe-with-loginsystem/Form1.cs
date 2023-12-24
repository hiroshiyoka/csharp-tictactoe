using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tictactoe_with_loginsystem
{
    public partial class Form1 : Form
    {
        private enum Player { X, O }

        private Player currentPlayer;
        private int moveCount = 0;
        private bool gameEnded = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonClick(object sender, EventArgs e)
        {
            if (!gameEnded)
            {
                var button = (Button)sender;
                currentPlayer = Player.X;
                MakeMove(button);
                timer1.Start();
            }
        }

        private void resetGame(object sender, EventArgs e)
        {
            label1.Text = "Result: ";
            moveCount = 0;
            gameEnded = false;
            currentPlayer = Player.X;
            timer1.Stop();

            foreach (var button in Controls.OfType<Button>().Where(b => b.Tag?.ToString() == "Play"))
            {
                button.Enabled = true;
                button.Text = "?";
                button.BackColor = SystemColors.Control;
            }
        }

        private void playAI(object sender, EventArgs e)
        {
            if (!gameEnded)
            {
                var availableButtons = Controls.OfType<Button>().Where(b => b.Text == "?" && b.Enabled).ToList();
                if (availableButtons.Any())
                {
                    currentPlayer = Player.O;
                    MakeMove(availableButtons.First());
                    timer1.Stop();
                }
                else
                {
                    timer1.Stop();
                }
            }
        }

        private void MakeMove(Button button)
        {
            button.Text = currentPlayer.ToString();
            button.Enabled = false;
            button.BackColor = currentPlayer == Player.X ? Color.Gold : Color.Lime;
            moveCount++;
            CheckForWinner();
        }

        private void CheckForWinner()
        {
            if (CheckWin("X"))
                EndGame("X");
            else if (CheckWin("O"))
                EndGame("O");
            else if (moveCount == 9)
                EndGame("Draw");
        }

        private bool CheckWin(string player)
        {
            Func<int, int, int, bool> check = (a, b, c) => button(a).Text == player && button(b).Text == player && button(c).Text == player;

            return
                check(1, 2, 3) || check(4, 5, 6) || check(7, 8, 9) ||
                check(1, 4, 7) || check(2, 5, 8) || check(3, 6, 9) ||
                check(1, 5, 9) || check(3, 5, 7);

            Button button(int index) => Controls.Find($"button{index}", true).FirstOrDefault() as Button;
        }

        private void EndGame(string result)
        {
            MENANG();
            gameEnded = true;

            label1.Text = result == "Draw" ? "Result: It's a Draw!" : $"Result: {result} Win!";
        }

        private void MENANG()
        {
            foreach (var button in Controls.OfType<Button>().Where(b => b.Tag?.ToString() == "Play"))
            {
                button.Enabled = false;
            }
        }
    }
}
