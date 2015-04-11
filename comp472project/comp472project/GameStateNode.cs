using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace comp472project
{
    class GameStateNode
    {
        public static Stopwatch timer;
        public static long CREATE_LIMIT = 2500;
        const int MAX_DEPTH = 2;
        Board boardState;
        float whiteMoves;
        float blackMoves;
        float score;
        int depth = 0;

        public Move move = new Move();
        GameState gameState;
        public List< GameStateNode> possibleMoves;

        public GameStateNode(Board gameBoard, int depth, Move change, GameState gameState)
        {
            //CONSTRUCTOR FOR NESTED NODES
            this.gameState = new GameState();
            //this.gameState = ((gameState == GameState.WhitePlay && depth != 0) ? GameState.BlackPlay : GameState.WhitePlay);
            if(!Heuristic.first)//if (depth != 0)
            {
                if (gameState == GameState.WhitePlay)
                    this.gameState = GameState.BlackPlay;
                if (gameState == GameState.BlackPlay)
                    this.gameState = GameState.WhitePlay;
            }
            Heuristic.first = false;
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
                            ++blackMoves;//GET A POINT FOR EACH AVAILABLE BLACK MOVE
                            if ((i % 2) == 1 )
                                ++blackMoves;//GET POINT IF MOVE IS ON EVERY SECOND COLUMN
                            if (i != boardSize - 1 && i != 0)
                                blackMoves += .5f;//HALF POINT IF NOT FIRST OR LAST COLUMN
                            //if (boardState.getCell(i + 1, j) == 'E' && boardState.getCell(i + 1, j + 1) == 'E')
                            //    blackMoves += 0.5f;
                            //if (boardState.getCell(i - 1, j) == 'E' && boardState.getCell(i - 1, j + 1) == 'E')
                            //    blackMoves += 0.5f;    
                        }
                        if ( boardState.getCell(i + 1, j) == 'E')
                        {
                            ++whiteMoves;//GET A POINT FOR EACH AVAILABLE WHITE MOVE
                            if ((j % 2) == 1)
                                ++whiteMoves;//GET POINT IF MOVE IS ON EVERY SECOND COLUMN
                            if (j != boardSize - 1 && j != 0)
                                whiteMoves += .5f;//HALF POINT IF NOT FIRST OR LAST ROW
                            //if (boardState.getCell(i, j+1) == 'E' && boardState.getCell(i + 1, j + 1) == 'E')
                            //    whiteMoves += 0.5f;
                            //if (boardState.getCell(i, j-1) == 'E' && boardState.getCell(i + 1, j - 1) == 'E')
                            //    whiteMoves += 0.5f;    
                        }
                    }
                }
            }
            score = whiteMoves - blackMoves;
        }
        public void GenerateSeachLevel(int level)//GENERATE SEARCH SPACE ONE LEVEL AT A TIME
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
                            if (gameState == GameState.BlackPlay && j+1 != boardSize && boardState.getCell(i, j + 1) == 'E')
                            {
                                if (gameState == GameState.BlackPlay)
                                    generateNodeState('B', i, j, gameState);
                            }
                            if (gameState == GameState.WhitePlay && i + 1 != boardSize && boardState.getCell(i + 1, j) == 'E')
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
            if (possibleMoves.Count == 0)//check for children
                return this;
            else if (this.depth == 0)
            {
                GameStateNode bestSoFar = new GameStateNode();
                //GO THROUGH CHILDREN CHANGE CHECK FOR BEST ONE
                foreach (GameStateNode currentNode in possibleMoves)
                {
                    if (currentTest == MinMax.MAX)
                    {
                        if (bestSoFar.boardState == null || currentNode.getPlay(MinMax.MAX).score > bestSoFar.score)
                            bestSoFar = currentNode;
                    }
                    else
                    {
                        if (bestSoFar.boardState == null || currentNode.getPlay(MinMax.MIN).score < bestSoFar.score)
                            bestSoFar = currentNode;
                    }
                }
                return bestSoFar;
            }
            else
            {
                GameStateNode bestSoFar = new GameStateNode();
                //GO THROUGH CHILDREN CHANGE CHECK FOR BEST ONE
                foreach (GameStateNode currentNode in possibleMoves)
                {
                    if (currentTest == MinMax.MAX)
                    {
                        if (bestSoFar.boardState == null || currentNode.getPlay(MinMax.MIN).score > bestSoFar.score)
                            bestSoFar = currentNode;
                    }
                    else
                    {
                        if (bestSoFar.boardState == null || currentNode.getPlay(MinMax.MAX).score < bestSoFar.score)
                            bestSoFar = currentNode;
                    }
                }
                return bestSoFar;
            }
        }

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
            //Adjust move to be on 1-N scale so it uses same notation as human
            newMove.setMove(newMove.getX(), newMove.getY(), color);
            possibleMoves.Add(newGameStateNode);
        }

        public float getScore()
        {
            return score;
        }
    }
}
