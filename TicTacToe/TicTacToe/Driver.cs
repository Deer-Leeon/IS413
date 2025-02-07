using System;

namespace TicTacToeGame
{
    public class Driver
    {
        public void MainGame(object[,] tictactoeboard)
        {
            // Get the number of rows and columns
            int rows = tictactoeboard.GetLength(0);
            int cols = tictactoeboard.GetLength(1);

            // Iterate through the array and print elements
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(tictactoeboard[i, j] + " "); // Print each element with a space
                }
                Console.WriteLine(); // Move to the next line after printing a row
            }
        }

        private bool CheckForWinner(object[,] tictactoeboard)
        {
            // Check rows and columns
            for (int i = 0; i < 3; i++)
            {
                if (tictactoeboard[i, 0].Equals(tictactoeboard[i, 1]) &&
                    tictactoeboard[i, 1].Equals(tictactoeboard[i, 2]))
                    return true;
                if (tictactoeboard[0, i].Equals(tictactoeboard[1, i]) &&
                    tictactoeboard[1, i].Equals(tictactoeboard[2, i]))
                    return true;
            }
            // Check diagonals
            if (tictactoeboard[0, 0].Equals(tictactoeboard[1, 1]) &&
                tictactoeboard[1, 1].Equals(tictactoeboard[2, 2]))
                return true;
            if (tictactoeboard[0, 2].Equals(tictactoeboard[1, 1]) &&
                tictactoeboard[1, 1].Equals(tictactoeboard[2, 0]))
                return true;
            return false; // No winner
        }

        private bool CheckIfBoardIsFull(object[,] tictactoeboard)
        {
            // go through each cell
            foreach (var cell in tictactoeboard)
            {
                if (cell is int)
                    return false;
            }
            return true;
        }

        private void PlayerOneMove(object[,] tictactoeboard)
        {
            // add a move where X is printed
            
            Console.WriteLine("Player one move. Where do you want to place your X?");
            string input = Console.ReadLine();
            if (input.Any(c => "123456789".Contains(c)))
            {
                if (input == "1")
                {
                    PlayMoveX(tictactoeboard, 0, 0);
                }
                else if (input == "2")
                {
                    PlayMoveX(tictactoeboard, 0, 1);
                }
                else if (input == "3")
                {
                    PlayMoveX(tictactoeboard, 0, 2);
                }
                else if (input == "4")
                {
                    PlayMoveX(tictactoeboard, 1, 0);
                }
                else if (input == "5")
                {
                    PlayMoveX(tictactoeboard, 1, 1);
                }
                else if (input == "6")
                {
                    PlayMoveX(tictactoeboard, 1, 2);
                }
                else if (input == "7")
                {
                    PlayMoveX(tictactoeboard, 2, 0);
                }
                else if (input == "8")
                {
                    PlayMoveX(tictactoeboard, 2, 1);
                }
                else if (input == "9")
                {
                    PlayMoveX(tictactoeboard, 2, 2);
                }
            }
        }

        private void PlayMoveX(object[,] tictactoeboard, int row, int col)
        {
            if (tictactoeboard[row, col] != "X" && tictactoeboard[row, col] != "O")
            {
                tictactoeboard[row, col] = "X";
            }
            else Console.WriteLine("Field has already been played in");
        }
        
        private void PlayerTwoMove(object[,] tictactoeboard)
        {
            Console.WriteLine("Player one move. Where do you want to place your X?");
            string input = Console.ReadLine();
            if (input.Any(c => "123456789".Contains(c)))
            {
                if (input == "1")
                {
                    PlayMoveO(tictactoeboard, 0, 0);
                }
                else if (input == "2")
                {
                    PlayMoveO(tictactoeboard, 0, 1);
                }
                else if (input == "3")
                {
                    PlayMoveO(tictactoeboard, 0, 2);
                }
                else if (input == "4")
                {
                    PlayMoveO(tictactoeboard, 1, 0);
                }
                else if (input == "5")
                {
                    PlayMoveO(tictactoeboard, 1, 1);
                }
                else if (input == "6")
                {
                    PlayMoveO(tictactoeboard, 1, 2);
                }
                else if (input == "7")
                {
                    PlayMoveO(tictactoeboard, 2, 0);
                }
                else if (input == "8")
                {
                    PlayMoveO(tictactoeboard, 2, 1);
                }
                else if (input == "9")
                {
                    PlayMoveO(tictactoeboard, 2, 2);
                }
            }
        }
        
        private void PlayMoveO(object[,] tictactoeboard, int row, int col)
        {
            if (tictactoeboard[row, col] != "X" && tictactoeboard[row, col] != "O")
            {
                tictactoeboard[row, col] = "O";
            }
            else Console.WriteLine("Field has already been played in");
        }
    }
}