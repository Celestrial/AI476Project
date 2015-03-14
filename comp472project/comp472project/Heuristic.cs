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
            generatePlayOptions();
        }

        public void generatePlayOptions()
        {
            lowestGSLevel = new GameStateNode(Game.getBoard());
        }

        public Move getPlay(char color)
        {
            return lowestGSLevel.getPossibleMove(color);
        }
    }
}
