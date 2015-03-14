using System.Collections;
using System.Collections.Generic;

namespace comp472project
{
    public class Board
    {
        const int SIZE = 3;
        public static char[,] gameBoard;


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
                    //    gameBoard[j, i] = 'B';
                }
            }
        }

        public Board()
        {
            setBoard();
        }
        public Board(Board otherBoard)
        {
            gameBoard = otherBoard.getBoard();
        }

        public void changeTile(char color, char row, int col)
        {

            int iRow = ((int)char.ToUpper(row)) - 65;
            if (iRow >= SIZE || iRow < 0)
                return;
            if (char.ToUpper(color) == 'B')
            {
                gameBoard[iRow, col] = 'B';
            }
            else
            {
                gameBoard[iRow, col] = 'W';
            }
        }

        public void changeTile(char color, int row, int col)
        {
            if (row >= SIZE || row < 0)
                return;
            if (char.ToUpper(color) == 'B')
            {
                gameBoard[row, col] = 'B';
            }
            else
            {
                gameBoard[row, col] = 'W';
            }
        }

        public char getCell(int x, int y)
        {
            return gameBoard[x, y];
        }
        public void placeTile(char player, Move curMove)
        {
            if (player == 'W')
            {
                changeTile('W', curMove.getX(), curMove.getY());
                changeTile('W', curMove.getX(), curMove.getY() + 1);
            }
            else
            {
                changeTile('B', curMove.getX(), curMove.getY());
                changeTile('B', curMove.getX() + 1, curMove.getY());
            }
            StateSpace newBoard = new StateSpace(this);
            newBoard.calcScores();
            Node newNode = new Node(newBoard, player == 'W' ? true : false);
        }
        public int getSize()
        {
            return SIZE;
        }

        public char[,] getBoard()
        {
            return gameBoard;
        }
    }
}