using System.Collections;
using System.Collections.Generic;

namespace comp472project
{
    public class Board
    {
        const int SIZE = 3;
        char[,] gameBoard;


        public void setBoard()
        {
            //gameBoard = new char[(int) System.Math.Pow(SIZE,2)];
            gameBoard = new char[SIZE, SIZE];
            makeBoard();
        }

        public void makeBoard()
        {
            for (int j = 0; j < SIZE; ++j)
            {
                for (int i = 0; i < SIZE; ++i)
                {
                    gameBoard[j, i] = 'E';

                    //if (j == SIZE - 1 || i == SIZE -1)
                    //    gameBoard[j, i] = 'E';
                    //else
                    //    gameBoard[j, i] = 'W';
                }
            }
        }

        public Board()
        {
            setBoard();
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

        public int getSize()
        {
            return SIZE;
        }
    }
}