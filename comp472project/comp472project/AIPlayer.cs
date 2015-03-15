using System.Collections;

namespace comp472project
{
    public class AIPlayer : PlayerManager
    {
        Heuristic skyNet;

        public AIPlayer(char color) : base(color) 
        {
            skyNet = new Heuristic();
        }

        public override string getMove()
        {
            skyNet.generatePlayOptions(Game.gameState);
            Move temp = skyNet.getPlay(base.getColor());
            char convert = (char)(temp.getX()+65);
            return convert + temp.getY().ToString();
        }
    }
}