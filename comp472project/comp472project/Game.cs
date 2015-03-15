using System.Collections;
using System;

namespace comp472project
{
    public enum GameState { WhitePlay = -1, BlackPlay = 1, GameOver = 0 };

    public class Game
    {
        int numberOfAIPlayers;
        static Board board;
        Move move;
        PlayerManager p1, p2;
        GameState gameState;

        public Game()
        {
            board = new Board();

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
   

            gameState = GameState.WhitePlay;


            if (numberOfAIPlayers == 0)
            {
                p2 = new HPlayer('B');
                p1 = new HPlayer('W');
            }
            else if (numberOfAIPlayers == 1)
            {
                p2 = new HPlayer('B');
                p1 = new AIPlayer('W');
            }
            else
            {
                p2 = new AIPlayer('B');
                p1 = new AIPlayer('W');
            }
        }

        bool validPlay(char color, int x, int y)
        {
            if (x < 0 || x >= Board.getSize() || y < 0 || y >= Board.getSize())
                return false;

            if (board.getCell(x, y) == 'E')
            {
                if (color == 'W' && (y >= 0 && y < Board.getSize() - 1) && board.getCell(x, y + 1) == 'E')
                {
                    return true;
                }
                else if (color == 'B' && (x >= 0 && x < Board.getSize() - 1) && board.getCell(x + 1, y) == 'E')
                {
                    return true;
                }
            }
            return false;
        }

        //return game state : {whiteplay, blackplay or gameover
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
            if (gameState == GameState.BlackPlay)
            {
                board.changeTile('B', move.getX(), move.getY());
                board.changeTile('B', move.getX(), move.getY() + 1);
            }
            else
            {
                board.changeTile('W', move.getX(), move.getY());
                board.changeTile('W', move.getX() + 1, move.getY());
            }
        }

        public void check4Win()
        {
            if(gameState == GameState.BlackPlay)
            {
                for(int i = 0; i < Board.getSize(); ++i)
                {
                    for(int j = 0; j < Board.getSize()-1; ++j)
                    {
                        if (board.getCell(i, j) == 'E')
                            if (board.getCell(i, j + 1) == 'E')
                                return;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Board.getSize() - 1; ++i)
                {
                    for (int j = 0; j < Board.getSize(); ++j)
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
            for(int i = 0; i < Board.getSize(); ++i)
            {
                for(int j = 0; j < Board.getSize(); ++j)
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

        public static Board getBoard()
        {
            return board;
        }
    }
}