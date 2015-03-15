using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp472project
{
    class Heuristic
    {
        //Board currentState;
        GameStateNode lowestGSLevel;

        public Heuristic()
        {
            //currentState = Game.getBoard();
            //generatePlayOptions();
        }

        public void generatePlayOptions(GameState gameState)
        {
            lowestGSLevel = new GameStateNode(Game.getBoard(), gameState);
        }

        public Move getPlay(char color)
        {
            if (lowestGSLevel.getPossibleMove(color) == null)
                Program.EndGame(Game.gameState);
            else
                return lowestGSLevel.getPossibleMove(color);
            return null;
        }
    }
}
