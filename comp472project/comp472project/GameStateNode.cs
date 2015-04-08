using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace comp472project
{
    public enum Visited
    {
        VISITED, NOT_VISITED
    };
    class GameStateNode
    {
        public static Stopwatch timer;
        public static long CREATE_LIMIT = 2500;
        const int MAX_DEPTH = 2;
        Board boardState;
        int whiteMoves;
        int blackMoves;
        int score;
        int depth = 0;

        public Move move = new Move();
        GameState gameState;
        public List< GameStateNode> possibleMoves;

        public GameStateNode(Board gameBoard, int depth, Move change, GameState gameState)
        {
            //CONSTRUCTOR FOR NESTED NODES
            this.gameState = new GameState();
            this.gameState = (gameState == GameState.WhitePlay ? GameState.BlackPlay : GameState.WhitePlay);
            boardState = gameBoard;//copy the current board under consideration
            this.depth = depth+1; // number
            move = change;
            possibleMoves = new List<GameStateNode>();
            calculateScore();
        }
        public GameStateNode(Board gameBoard, GameState gameState)
        {
            //CONSTRUCTOR FOR ROOT NODE, OR CURRENT GAME PLAY
            this.gameState = gameState;
            boardState = gameBoard;
            depth = 0;
            possibleMoves = new List<GameStateNode>();
            calculateScore();
        }
        public GameStateNode()
        {
            score = 0;
        }

        //public static void resetTimer()
        //{
        //    timer = 0;
        //}

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
                        }
                        if ( boardState.getCell(i + 1, j) == 'E')
                        {
                            ++whiteMoves;
                        }
                    }
                }
            }
            score = whiteMoves - blackMoves;
        }
        public void GenerateSeachLevel(int level)
        {
            long elapsed = timer.ElapsedMilliseconds;
            if (level == 0 && elapsed < CREATE_LIMIT)
            {
                int boardSize = Board.getSize();
                for (int i = 0; i < boardSize; ++i)
                {
                    for (int j = 0; j < boardSize; ++j)
                    {
                        if (boardState.getCell(i, j) == 'E')
                        {
                            if (gameState == GameState.BlackPlay && boardState.getCell(i, j + 1) == 'E')
                            {
                                if (gameState == GameState.BlackPlay)
                                    generateNodeState('B', i, j, gameState);
                            }
                            if (boardState.getCell(i + 1, j) == 'E')
                            {
                                if (gameState == GameState.WhitePlay)
                                    generateNodeState('W', i, j, gameState);
                            }
                        }
                    }
                }
            }
            else if (level > 0 && elapsed < CREATE_LIMIT)
            {
                foreach(GameStateNode currentNode in possibleMoves)
                {
                    currentNode.GenerateSeachLevel(level - 1);
                }
            }
        }
        public GameStateNode getPlay(MinMax currentTest)
        {
            if (possibleMoves.Count == 0)
                return this;
            else
            {
                GameStateNode bestSoFar = new GameStateNode() ;

                foreach(GameStateNode currentNode in possibleMoves)
                {
                    if(currentTest == MinMax.MAX)
                    {
                        if (currentNode.getPlay(MinMax.MIN).score > bestSoFar.score)
                            bestSoFar = currentNode;
                    }
                    else
                    {
                        if (currentNode.getPlay(MinMax.MAX).score < bestSoFar.score)
                            bestSoFar = currentNode;
                    }
                }
                return bestSoFar;
            }
        }
        //void GenerateSeachSpace()
        //{ //BUILD STATE (single level) TREE BREATH FIRST
        //    int boardSize = Board.getSize();
        //    for (int i = 0; i < boardSize; ++i)
        //    {
        //        for (int j = 0; j < boardSize; ++j)
        //        {
        //            if (boardState.getCell(i, j) == 'E')
        //            {
        //                if (gameState == GameState.BlackPlay && boardState.getCell(i, j + 1) == 'E')
        //                {
        //                    if (gameState == GameState.BlackPlay)
        //                        generateNodeState('B', i, j, gameState);
        //                }
        //                if (boardState.getCell(i + 1, j) == 'E')
        //                {
        //                    if (gameState == GameState.WhitePlay)
        //                        generateNodeState('W', i, j, gameState);
        //                }
        //            }
        //        }
        //    }
        //}
        void generateNodeState(char color, int i, int j, GameState gameState)
        //GENERATES GAME STATE NODE AND SCORES USED FOR MIN MAX
        {
            GameStateNode newGameStateNode;
            Board newBoard = new Board(boardState);
            Move newMove;
            //MAKE MOVE FOR NEW BOARD
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

            newMove = new Move();
            newMove.setMove(i, j, color);
            //create a gameStateNode with the new board, the new boards depth, and the move added
            newGameStateNode = new GameStateNode(newBoard, depth, newMove, gameState);
            newGameStateNode.calculateScore();
            //Adjust move to be on 1-N scale so it uses same notation as human
            newMove.setMove(newMove.getX(), newMove.getY() + 1, color);
            possibleMoves.Add(newGameStateNode);
        }
        public Move getPossibleMove(char color)
        //GET MOVE FROM THE MIN MAX TREE FOR AI PLAYER
        {
        //    if (possibleMoves.Count != 0)
        //    {
        //        //if (color == 'W')
        //        //    return possibleMoves.Last().Value.move;
        //        //else
        //        //    return possibleMoves.First().Value.move;
        //        if (possibleMoves.Count == 1)
        //            return possibleMoves.ElementAt(0).Value.move;
        //        return findBestMove(color).move;
        //    }
        //    else
        //    {
        //        return null;
        //    }
            return null;
        }
        public GameStateNode findBestMove(char color, int level = 0)
        {
        //    GameStateNode bestMove = null;
        //    //int level = 1;

        //    //if (possibleMoves.Count != 0)
        //    //{
        //        if (level <= MAX_DEPTH)
        //        {
        //            int bestScore;
        //            if (color == 'W')
        //            {
        //                //return possibleMoves.Last().Value.move;
        //                bestScore = possibleMoves.Last().Key;
        //                int i = possibleMoves.Count - 1;
        //                //from the best score state, get the worst move min will play
        //                while (i >= 0 && possibleMoves.ElementAt(i).Key == bestScore)
        //                {
        //                    GameStateNode temp = null;
        //                    if (possibleMoves.ElementAt(i).Value.possibleMoves.Count != 0)
        //                    {

        //                        temp =  possibleMoves.ElementAt(i--).Value.findBestMove
        //                        ((gameState == GameState.WhitePlay ? 'B' : 'W'), level + 1);
                                
        //                        if (temp == null)
        //                        {
        //                            return possibleMoves.First().Value;
        //                        }
        //                        else
        //                        {
        //                            if (bestMove == null || bestMove.score > temp.score)
        //                                bestMove = temp;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //DEBUG
        //                        GameStateNode tester = possibleMoves.Last().Value;
        //                        //DEBUG
        //                        return possibleMoves.Last().Value;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                bestScore = possibleMoves.First().Key;
        //                int i = 0;
        //                while (i < possibleMoves.Count && possibleMoves.ElementAt(i).Key == bestScore)
        //                {
        //                    GameStateNode temp = null;
        //                    if (possibleMoves.ElementAt(i).Value.possibleMoves.Count != 0)
        //                    {
        //                        temp = possibleMoves.ElementAt(i++).Value.findBestMove
        //                             ((gameState == GameState.WhitePlay ? 'B' : 'W'), level + 1);

        //                        if (temp == null)
        //                        {
        //                            return possibleMoves.First().Value;
        //                        }
        //                        else
        //                        {
        //                            if (bestMove == null || bestMove.score < temp.score)
        //                                bestMove = temp;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //DEBUG
        //                        GameStateNode tester = possibleMoves.First().Value;
        //                        //DEBUG
        //                        return possibleMoves.First().Value;
        //                    }
    
        //                }
        //            }
        //    }
        //    return bestMove;
            return null;
        }

        public int getScore()
        {
            return score;
        }
    }
}
