using System.Collections;
using System.Collections.Generic;

namespace comp472project
{
    public class Board
    {
        static int SIZE = 7;
        char[,] gameBoard;


        public Board()
        {
            gameBoard = new char[SIZE, SIZE];
            makeBoard();
        }
        public Board(Board other)
        {
            gameBoard = new char[SIZE, SIZE];
            for (int i = 0; i < SIZE; ++i)
            {
                for (int j = 0; j < SIZE; ++j)
                {
                    gameBoard[i, j] = other.getCell(i, j);
                }
            }
        }

        public void makeBoard()
        {
            for (int j = 0; j < SIZE; ++j)
            {
                for (int i = 0; i < SIZE; ++i)
                {
                    gameBoard[j, i] = 'E';
                }
            }
        }

        public void changeTile(char color, char row, int col)
        {

            int iRow = ((int)char.ToUpper(row)) - 65;
            if (iRow >= SIZE || iRow < 0)
                return;
            if (char.ToUpper(color) == 'W')
            {
                gameBoard[iRow, col] = 'W';
            }
            else
            {
                gameBoard[iRow, col] = 'B';
            }
        }

        public void changeTile(char color, int row, int col)
        {
            if (row >= SIZE || row < 0)
                return;
            if (char.ToUpper(color) == 'W')
            {
                gameBoard[row, col] = 'W';
            }
            else
            {
                gameBoard[row, col] = 'B';
            }
        }

        public char getCell(int x, int y)
        {
            if (x >= SIZE || y >= SIZE)
                return '\0';
             return gameBoard[x, y];
        }

        public static int getSize()
        {
            return SIZE;
        }
    }
}