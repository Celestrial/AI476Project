using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp472project
{
    class GameStateNode
    {
        Board boardState;
        int wScore;
        int bScore;
        List<GameStateNode> possibleMoves;


        void calculateScore()
        {
            int boardSize = boardState.getSize();
            for (int i = 0; i < boardSize; ++i)
            {
                for (int j = 0; j < boardSize; ++j)
                {
                    if (i == boardSize - 1)
                    {
                        
                    }
                    else if (j == boardSize - 1)
                    {

                    }
                    else
                    {

                    }
                }
            }
        }
        void generateNodes()
        {

        }
    }
}
