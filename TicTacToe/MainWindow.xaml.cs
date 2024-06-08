using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private char[] board;
        private int player;
        private bool gameActive;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            board = new char[] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
            player = 1;
            gameActive = true;
            UpdateStatus();
            ClearBoard();
        }

        private void ClearBoard()
        {
            btn1.Content = btn2.Content = btn3.Content = btn4.Content = btn5.Content = btn6.Content = btn7.Content = btn8.Content = btn9.Content = string.Empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!gameActive)
                return;

            Button button = sender as Button;
            int index = int.Parse(button.Name.Substring(3)) - 1;

            if (board[index] == ' ')
            {
                board[index] = player == 1 ? 'X' : 'O';
                button.Content = board[index].ToString();
                if (CheckWin())
                {
                    gameActive = false;
                    statusLabel.Content = $"Player {player} wins!";
                }
                else if (Array.TrueForAll(board, cell => cell != ' '))
                {
                    gameActive = false;
                    statusLabel.Content = "It's a draw!";
                }
                else
                {
                    player = player == 1 ? 2 : 1;
                    UpdateStatus();
                }
            }
        }

        private void UpdateStatus()
        {
            statusLabel.Content = $"Player {player}'s turn";
        }

        private bool CheckWin()
        {
            int[,] winConditions = new int[,]
            {
                {0, 1, 2}, {3, 4, 5}, {6, 7, 8},
                {0, 3, 6}, {1, 4, 7}, {2, 5, 8},
                {0, 4, 8}, {2, 4, 6}
            };

            for (int i = 0; i < winConditions.GetLength(0); i++)
            {
                int a = winConditions[i, 0];
                int b = winConditions[i, 1];
                int c = winConditions[i, 2];

                if (board[a] != ' ' && board[a] == board[b] && board[b] == board[c])
                {
                    return true;
                }
            }

            return false;
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
        }
    }
}