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
            return null;
        }
    }
}