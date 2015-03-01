using System;
using System.Collections.Generic;

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
