﻿using System.Collections;
using System;

namespace comp472project
{
    public enum GameState { WhitePlay = -1, BlackPlay = 1, GameOver = 0 };

    public class Game
    {
        int numberOfAIPlayers;
        Board board;
        Move move;
        PlayerManager p1, p2;
        GameState gameState;

        public Game()
        {
            Console.Out.Write("Enter number of AI players: ");
            string num = Console.ReadLine();
            move = new Move();
            numberOfAIPlayers = Convert.ToInt32(num);
            gameState = GameState.WhitePlay;


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

        public GameState getGameState()
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
            if (gameState == GameState.WhitePlay)
            {
                Console.Out.Write("Please enter white play position: ");
                move.setMove( Console.ReadLine().ToUpper());

                while (validPlay('W', move.getX(), move.getY()) == false)
                {
                    Console.Out.WriteLine("Move entered is invalid, please try again: ");
                    move.setMove(Console.ReadLine().ToUpper());
                }
                return move;
            }
            else
            {
                Console.Out.Write("Please enter black play position: ");
                move.setMove( Console.ReadLine().ToUpper());

                while ( validPlay('B', move.getX(), move.getY()) == false)
                {
                    Console.Out.WriteLine("Move entered is invalid, please try again: ");
                    move.setMove(Console.ReadLine().ToUpper());
                }
                return move;
            }
        }

        void placeTile()
        {
            if (gameState == GameState.WhitePlay)
            {
                board.changeTile('W', move.getX(), move.getY());
                board.changeTile('W', move.getX(), move.getY() + 1);
            }
            else
            {
                board.changeTile('B', move.getX(), move.getY());
                board.changeTile('B', move.getX() + 1, move.getY());
            }
        }

        public void check4Win()
        {
            if(gameState == GameState.BlackPlay)
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
            if (gameState == GameState.BlackPlay)
                Console.WriteLine("Black Wins!!!");
            else
                Console.Write("White Wins!!!");
            gameState = GameState.GameOver;
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
            if (gameState == GameState.WhitePlay)
                gameState = GameState.BlackPlay;
            else if (gameState == GameState.BlackPlay)
                gameState = GameState.WhitePlay;
        }
    }
}