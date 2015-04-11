using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp472project
{
    public enum MinMax
    {
        MIN, MAX
    };
    class Heuristic
    {
        //Board currentState;
        GameStateNode lowestGSLevel;
        MinMax searchState = MinMax.MAX;
        static int hightestSoFar = 0;
        public static bool first = true;
        int lowestSoFar = 0;

        public Heuristic()
        {
            //currentState = Game.getBoard();
            //generatePlayOptions();
        }

        public void generatePlayOptions(GameState gameState)
        {
            GameStateNode.timer = System.Diagnostics.Stopwatch.StartNew();

            if (gameState == GameState.WhitePlay)
                searchState = MinMax.MAX;
            else
                searchState = MinMax.MIN;

            lowestGSLevel = new GameStateNode(Game.getBoard(), gameState);
            int i = 0;
            while(GameStateNode.timer.ElapsedMilliseconds < GameStateNode.CREATE_LIMIT)
            {
                lowestGSLevel.GenerateSeachLevel(i++);
            }
            //Move temp = lowestGSLevel.getPlay(searchState).move;
            //return temp;
        }

        //public void traverse(GameStateNode currentNode)
        //{
        //    Stack<GameStateNode> queued = new Stack<GameStateNode>();
        //    foreach(GameStateNode node in lowestGSLevel.possibleMoves)
        //    {
        //        traverse(node);
        //    }

        //}

        public Move getPlay(char color)
        {
            //if (lowestGSLevel.getPossibleMove(color) == null)
            //    Program.EndGame(Game.gameState);
            //else
            //    return lowestGSLevel.getPossibleMove(color);
            //return null;
            Move temp = lowestGSLevel.getPlay(color == 'W' ? MinMax.MAX : MinMax.MIN).move;
            Console.WriteLine("Board State Score = " + lowestGSLevel.getScore());
            first = true;
            return temp;
        }
    }
}
