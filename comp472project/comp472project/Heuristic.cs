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
            GameStateNode.timer = System.Diagnostics.Stopwatch.StartNew();
            lowestGSLevel = new GameStateNode(Game.getBoard(), gameState);
            int i = 0;
            while(GameStateNode.timer.ElapsedMilliseconds < GameStateNode.CREATE_LIMIT)
            {
                lowestGSLevel.GenerateSeachLevel(i++);
            }
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
