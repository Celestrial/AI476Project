using System.Collections;

namespace comp472project
{
    public class AIPlayer : PlayerManager
    {

        public AIPlayer(char color) : base(color) { }

        public override string getMove()
        {
            return null;
        }

        private char[,] getGameBoard()
        {
            return Board.gameBoard;
        }
    }
}