using System.Collections;
using System.Collections.Generic;
using System;

namespace comp472project
{
    public class Board
    {
        static int SIZE = 8;
        char[,] gameBoard;
        const char BLACK_CHAR = (char)((byte)254);
        const char WHITE_CHAR = (char)((byte)255);

        public Board()
        {
            Console.Write("Enter game board size: ");
            try
            {
                SIZE = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                SIZE = 8;
            }
            Console.WriteLine("\n");
            gameBoard = new char[SIZE, SIZE];
            makeBoard();
        }
        public void printBoard()
        {
            Console.Write("  ");
            for (int i = 0; i < Board.getSize(); ++i)
            {
                Console.Write("  [" + (i + 1) + "]");
            }
            Console.WriteLine();

            for (int i = 0; i < Board.getSize(); ++i)
            {
                Console.Write("[" + (char)(65 + i) + "]");
                for (int j = 0; j < Board.getSize(); ++j)
                {
                    if (j != Board.getSize() - 1)
                    {
                        if(getCell(i,j) == 'E')
                            Console.Write(" [ ],");
                        else if (getCell(i, j) == 'B')
                            Console.Write(" [" + getCell(i, j) + "],");
                        else if (getCell(i, j) == 'W')
                            Console.Write(" [" + getCell(i, j) + "],");
                        else
                            Console.Write("Problem");
                    }
                    else if(getCell(i,j) == 'E')
                            Console.Write(" [ ]");
                        else if (getCell(i, j) == 'B')
                            Console.Write(" [" + getCell(i, j) + "]");
                        else if (getCell(i, j) == 'W')
                            Console.Write(" [" + getCell(i, j) + "]");
                        else
                            Console.Write("Problem");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
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
            if (x >= SIZE || y >= SIZE || x < 0 || y < 0)
                return '\0';
             return gameBoard[x, y];
        }

        public static int getSize()
        {
            return SIZE;
        }
    }
}