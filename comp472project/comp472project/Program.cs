using System;
using System.Collections.Generic;

/*
 *SEAN CELESTIN
 *4718593
 *DELIVERABLE 1 FOR COMP 472 PROJECT
 *MARCH 01, 2015
 *“I certify that this submission is my original work and meets the Faculty's Expectations of Originality”
 */
namespace comp472project
{
    static class Program
    {
        static void Main()
        {
            Game myGame = new Game();

            while (myGame.getGameState() != PlayState.GameOver)
            {
                myGame.makeMove();
                myGame.check4Win();
                myGame.printBoard();
                myGame.switchPlayers();
            }
        }

        public static void EndGame(GameState gameState)
        {
            if(gameState == GameState.WhitePlay)
                Console.Write("White has no more plays, Black Wins!!!");
            else
                Console.Write("Black has no more plays, White Wins!!!");
                
            Environment.Exit(0);
        }
    }
}
