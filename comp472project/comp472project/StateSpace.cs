using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp472project
{
    public class StateSpace
    {
        Board gameState;
        int boardSize;
        //SCORE IS 0 WHEN PLAYER HAS NO MOVES
        float whiteScore;
        float blackScore;
        //const float winState = 0;
        Byte[,] boardID;

        public StateSpace(Board currentState)
        {
            boardSize = currentState.getSize();
            this.gameState = currentState;
            boardID = new Byte[boardSize, boardSize];
        }

        public void calcScores()
        {
            //CALCULATE SCORES AND GENERATE ID
            char cellValue;
            for(int i = 0; i < boardSize; ++i)
            {
                for(int j = 0; j < boardSize; ++j)
                {
                    cellValue = gameState.getCell(i,j);
                    if (cellValue == 'E')
                    {
                        boardID[i, j] = 0;
                        if (j+1 != boardSize && gameState.getCell(i, j + 1) == 'E')
                            ++whiteScore;
                        if (i+1 != boardSize && gameState.getCell(i + 1, j) == 'E')
                            ++blackScore;
                    }
                    else if (cellValue == 'W')
                        boardID[i, j] = 1;
                    else
                        boardID[i, j] = 2;
                }
            }
        }
        public Board getBoard()
        {
            return gameState;
        }
    }
}
