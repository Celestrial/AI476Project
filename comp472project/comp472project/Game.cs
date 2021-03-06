﻿using System.Collections;
using System;

namespace comp472project
{
    public enum PlayState { WhitePlay = -1, BlackPlay = 1, GameOver = 0 };

    public class Game
    {
        int numberOfAIPlayers;
        Board board;
        Move move;
        PlayerManager p1, p2;
        PlayState gameState;

        public Game()
        {
            Console.Out.Write("Enter number of AI players: ");
            string num = Console.ReadLine();
            move = new Move();
            try
            {
                numberOfAIPlayers = Convert.ToInt32(num);
            }
            catch(FormatException e)
            {
                Console.WriteLine("Invalid number of players, default NO AI selected.");
                numberOfAIPlayers = 0;
            }
            finally
            {
                if(numberOfAIPlayers < 0 || numberOfAIPlayers > 2)
                {
                    Console.WriteLine("Invalid number of players, default NO AI selected.");
                    numberOfAIPlayers = 0;
                }
            }


            gameState = PlayState.WhitePlay;


            if (numberOfAIPlayers == 0)
            {
                p1 = new HPlayer('W');
                p2 = new HPlayer('B');
            }
            else if (numberOfAIPlayers == 1)
            {
                p1 = new HPlayer('W');
                p2 = new AIPlayer('B');
            }
            else
            {
                p1 = new AIPlayer('W');
                p2 = new AIPlayer('B');
            }
            board = new Board();
        }

        bool validPlay(char color, int x, int y)
        {
            if (x < 0 || x >= board.getSize() || y < 0 || y >= board.getSize())
                return false;

            if (board.getCell(x, y) == 'E')
            {
                if (color == 'W' && (y >= 0 && y < board.getSize() - 1) && board.getCell(x, y + 1) == 'E')
                {
                    return true;
                }
                else if (color == 'B' && (x >= 0 && x < board.getSize() - 1) && board.getCell(x + 1, y) == 'E')
                {
                    return true;
                }
            }
            return false;
        }

        public PlayState getGameState()
        {
            return gameState;
        }

        public void makeMove()
        {
            getPlay();//place current play in move variable
            placeTile();//make visual changes to board
        }

        Move getPlay()
        {
            if (gameState == PlayState.WhitePlay)
            {
               
                move.setMove( p1.getMove());

                while (validPlay(p1.getColor(), move.getX(), move.getY()) == false)
                {
                    Console.Out.WriteLine("Move entered is invalid, please try again: ");
                    move.setMove(p1.getMove());
                }
            }
            else
            {
                move.setMove(p2.getMove());

                while ( validPlay(p2.getColor(), move.getX(), move.getY()) == false)
                {
                    Console.Out.WriteLine("Move entered is invalid, please try again: ");
                    move.setMove(p2.getMove());
                }
            }
            return move;
        }

        void placeTile()
        {
            if (gameState == PlayState.WhitePlay)
            {
                board.placeTile('W', move);
            }
            else
            {
                board.placeTile('B', move);
            }
            //if (gameState == PlayState.WhitePlay)
            //{
            //    board.changeTile('W', move.getX(), move.getY());
            //    board.changeTile('W', move.getX(), move.getY() + 1);
            //}
            //else
            //{
            //    board.changeTile('B', move.getX(), move.getY());
            //    board.changeTile('B', move.getX() + 1, move.getY());
            //}
        }

        public void check4Win()
        {
            if (gameState == PlayState.BlackPlay)
            {
                for(int i = 0; i < board.getSize(); ++i)
                {
                    for(int j = 0; j < board.getSize()-1; ++j)
                    {
                        if (board.getCell(i, j) == 'E')
                            if (board.getCell(i, j + 1) == 'E')
                                return;
                    }
                }
            }
            else
            {
                for (int i = 0; i < board.getSize() - 1; ++i)
                {
                    for (int j = 0; j < board.getSize(); ++j)
                    {
                        if (board.getCell(i, j) == 'E')
                            if (board.getCell(i + 1, j) == 'E')
                                return;
                    }
                }
            }
            printBoard();
            if (gameState == PlayState.BlackPlay)
                Console.WriteLine("Black Wins!!!");
            else
                Console.Write("White Wins!!!");
            gameState = PlayState.GameOver;
        }

        internal void printBoard()
        {
            for(int i = 0; i < board.getSize(); ++i)
            {
                for(int j = 0; j < board.getSize(); ++j)
                {
                    Console.Write(board.getCell(i, j)+" ,");
                }
                Console.WriteLine();
            }
        }

        internal void switchPlayers()
        {
            if (gameState == PlayState.WhitePlay)
                gameState = PlayState.BlackPlay;
            else if (gameState == PlayState.BlackPlay)
                gameState = PlayState.WhitePlay;
        }
    }
}