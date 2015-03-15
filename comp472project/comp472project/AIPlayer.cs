using System.Collections;

namespace comp472project
{
    public class AIPlayer : PlayerManager
    {
        Heuristic skyNet;
        string play;

        public AIPlayer(char color) : base(color) 
        {
            skyNet = new Heuristic();
        }

        public override string getMove()
        {
            skyNet.generatePlayOptions(Game.gameState);
            Move temp = skyNet.getPlay(base.getColor());
            if (temp == null)
                return null;
            char convert = (char)(temp.getX()+65);
            play = convert + temp.getY().ToString();

            if (base.getColor() == 'W')
            {
                //USED TO LIMIT TREE DEPTH FOR FIRST 3 PAIRS OF MOVES
                ++Game.playCount;
            }
            return play;
        }
    }
}