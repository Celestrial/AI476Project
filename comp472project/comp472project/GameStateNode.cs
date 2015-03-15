using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp472project
{
    class GameStateNode
    {
        const int MAX_DEPTH = 2;
        Board boardState;
        int whiteMoves;
        int blackMoves;
        int score;
        int depth = 0;
        Move move = new Move();
        List<GameStateNode> possibleMoves;

        public GameStateNode(Board gameBoard, int depth, Move change)
        {
            //CONSTRUCTOR FOR NESTED NODES
            boardState = gameBoard;//copy the current board under consideration
            this.depth = depth; // number
            move = change;
            possibleMoves = new List<GameStateNode>();
            calculateScore();
        }
        public GameStateNode(Board gameBoard)
        {
            //CONSTRUCTOR FOR ROOT NODE, OR CURRENT GAME PLAY
            boardState = gameBoard;
            depth = 0;
            possibleMoves = new List<GameStateNode>();
            calculateScore();
        }

        void calculateScore()//MAIN HEURISTIC FUNCTION
        {
            whiteMoves = blackMoves = 0;
            //CALCULATE SCORE OF CURRENT GAME STATE
            int boardSize = Board.getSize();
            for (int i = 0; i < boardSize; ++i)
            {
                for (int j = 0; j < boardSize; ++j)
                {
                    if (boardState.getCell(i, j) == 'E')
                    {
                        if (boardState.getCell(i, j + 1) == 'E')
                        {
                            ++blackMoves;
                            generateNodeState('B', i, j);
                        }
                        if (boardState.getCell(i + 1, j) == 'E')
                        {
                            ++whiteMoves;
                            generateNodeState('W', i, j);
                        }
                    }
                }
            }
            score = whiteMoves - blackMoves;
        }
        void generateNodeState(char color, int i, int j)
        {
            //GENERATES GAME STATES AND STATE SCORES
            GameStateNode newGameState;
            Board newBoard = new Board(boardState);
            Move newMove;
            if(color == 'B')
            {
                newBoard.changeTile(color, i, j);
                newBoard.changeTile(color, i, j + 1);
            }
            else
            {
                newBoard.changeTile(color, i, j);
                newBoard.changeTile(color, i + 1, j);
            }

            if (depth != MAX_DEPTH /*&& blackMoves != 0 && whiteMoves != 0*/)
            {
                newMove = new Move();
                newMove.setMove(i, j+1, color);
                //create a gameStateNode with the new board, the new boards depth, and the move added
                newGameState = new GameStateNode(newBoard, depth, newMove);
                newGameState.calculateScore();
                possibleMoves.Add(newGameState);
            }
        }
        public Move getPossibleMove(char color)
        {
            if (color == 'W')
                return possibleMoves.Last().move;
            else
                return possibleMoves.First().move;
        }
    }
}
