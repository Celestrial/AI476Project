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
        GameState gameState;
        SortedList<int, GameStateNode> possibleMoves;

        public GameStateNode(Board gameBoard, int depth, Move change, GameState gameState)
        {
            //CONSTRUCTOR FOR NESTED NODES
            this.gameState = new GameState();
            this.gameState = (gameState == GameState.WhitePlay ? GameState.BlackPlay : GameState.WhitePlay);
            boardState = gameBoard;//copy the current board under consideration
            this.depth = depth+1; // number
            move = change;
            possibleMoves = new SortedList<int, GameStateNode>(new DuplicateKeyComparer<int>());
            calculateScore();
        }
        public GameStateNode(Board gameBoard, GameState gameState)
        {
            //CONSTRUCTOR FOR ROOT NODE, OR CURRENT GAME PLAY
            this.gameState = gameState;
            boardState = gameBoard;
            depth = 0;
            possibleMoves = new SortedList<int, GameStateNode>(new DuplicateKeyComparer<int>());
            calculateScore();
        }

        void calculateScore()
        //MAIN HEURISTIC FUNCTION
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
                        if (gameState == GameState.BlackPlay && boardState.getCell(i, j + 1) == 'E')
                        {
                            ++blackMoves;
                            if (gameState == GameState.BlackPlay)
                                generateNodeState('B', i, j, gameState);
                        }
                        if ( boardState.getCell(i + 1, j) == 'E')
                        {
                            ++whiteMoves;
                            if(gameState == GameState.WhitePlay)
                                generateNodeState('W', i, j, gameState);
                        }
                    }
                }
            }
            score = whiteMoves - blackMoves;
        }
        void generateNodeState(char color, int i, int j, GameState gameState)
        //GENERATES GAME STATE TREE AND STATE SCORES USED FOR MIN MAX
        {
            GameStateNode newGameStateNode;
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

            if (depth <= MAX_DEPTH /*&& blackMoves != 0 && whiteMoves != 0*/)
            {
                newMove = new Move();
                newMove.setMove(i, j, color);
                //create a gameStateNode with the new board, the new boards depth, and the move added
                newGameStateNode = new GameStateNode(newBoard, depth, newMove, gameState);
                newGameStateNode.calculateScore();
                //Adjust move to be on 1-N scale so it uses same notation as human
                newMove.setMove(newMove.getX(), newMove.getY() + 1, color);
                possibleMoves.Add(newGameStateNode.getScore(), newGameStateNode);
            }
        }
        public Move getPossibleMove(char color)
        //GET MOVE FROM THE MIN MAX TREE FOR AI PLAYER
        {
            if (possibleMoves.Count != 0)
            {
                //if (color == 'W')
                //    return possibleMoves.Last().Value.move;
                //else
                //    return possibleMoves.First().Value.move;
                return findBestMove(color).move;
            }
            else
            {
                return null;
            }
        }
        public GameStateNode findBestMove(char color, int level = 0)
        {
            GameStateNode bestMove = null;
            //int level = 1;

            //if (possibleMoves.Count != 0)
            //{
                if (level <= MAX_DEPTH)
                {
                    int bestScore;
                    if (color == 'W')
                    {
                        //return possibleMoves.Last().Value.move;
                        bestScore = possibleMoves.Last().Key;
                        int i = possibleMoves.Count - 1;
                        //from the best score state, get the worst move min will play
                        while (i >= 0 && possibleMoves.ElementAt(i).Key == bestScore)
                        {
                            GameStateNode temp = null;
                            if (possibleMoves.ElementAt(i).Value.possibleMoves.Count != 0)
                            {

                                temp =  possibleMoves.ElementAt(i--).Value.findBestMove
                                ((gameState == GameState.WhitePlay ? 'B' : 'W'), level + 1);
                                
                                if (temp == null)
                                {
                                    return possibleMoves.First().Value;
                                }
                                else
                                {
                                    if (bestMove == null || bestMove.score > temp.score)
                                        bestMove = temp;
                                }
                            }
                            else
                            {
                                //DEBUG
                                GameStateNode tester = possibleMoves.Last().Value;
                                //DEBUG
                                return possibleMoves.Last().Value;
                            }
                        }
                    }
                    else
                    {
                        bestScore = possibleMoves.First().Key;
                        int i = 0;
                        while (i < possibleMoves.Count && possibleMoves.ElementAt(i).Key == bestScore)
                        {
                            GameStateNode temp = null;
                            if (possibleMoves.ElementAt(i).Value.possibleMoves.Count != 0)
                            {
                                temp = possibleMoves.ElementAt(i++).Value.findBestMove
                                     ((gameState == GameState.WhitePlay ? 'B' : 'W'), level + 1);

                                if (temp == null)
                                {
                                    return possibleMoves.First().Value;
                                }
                                else
                                {
                                    if (bestMove == null || bestMove.score < temp.score)
                                        bestMove = temp;
                                }
                            }
                            else
                            {
                                //DEBUG
                                GameStateNode tester = possibleMoves.First().Value;
                                //DEBUG
                                return possibleMoves.First().Value;
                            }
    
                        }
                    }
            }
            return bestMove;
        }

        public int getScore()
        {
            return score;
        }
    }
}
