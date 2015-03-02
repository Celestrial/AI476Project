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

            while(myGame.getGameState() != GameState.GameOver)
            {
                myGame.makeMove();
                myGame.check4Win();
                myGame.printBoard();
                myGame.switchPlayers();
            }
        }
    }
}
