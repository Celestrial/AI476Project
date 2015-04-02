using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp472project
{
    class Node
    {
        StateSpace currentBoard;
        List<Node> possibleMoves = new List<Node>();
        static int MAX_DEPTH = 3;
        int depth;

        public Node(int parentDepth, StateSpace currentState, bool whiteTurn)
        {
            currentBoard = currentState;
            depth = parentDepth + 1;
            genPosMoves(whiteTurn);
        }
        public Node(StateSpace currentState, bool whiteTurn)
        {
            currentBoard = currentState;
            depth = 0;
            genPosMoves(whiteTurn);
        }

        public void genPosMoves(bool whiteTurn)
        {
            if (depth < MAX_DEPTH)
            {
                if (whiteTurn)
                    genWhiteMoves(depth++);
                else
                    genBlackMoves(depth++);

                whiteTurn = !whiteTurn;

                foreach (Node state in possibleMoves)
                {
                    state.genPosMoves(whiteTurn);
                }
            }
        }

        public void genWhiteMoves(int currentDepth)
        {
            int boardSize = currentBoard.getBoard().getSize();
            Board tempBoard;
            StateSpace tempStateSpace;
            Node node;
            for (int i = 0; i < boardSize-1; ++i)
            {
                for(int j = 0; j < boardSize; ++j)
                {
                    if(currentBoard.getBoard().getCell(i,j) == 'E' && currentBoard.getBoard().getCell(i+1,j) == 'E')
                    {
                        tempBoard = new Board(currentBoard.getBoard());
                        tempBoard.changeTile('W', i, j);
                        tempBoard.changeTile('W', i + 1, j);
                        tempStateSpace = new StateSpace(tempBoard);
                        tempStateSpace.calcScores();
                        node = new Node(currentDepth, tempStateSpace, true);
                        possibleMoves.Add(node);
                    }
                }
            }
        }
        public void genBlackMoves(int currentDepth)
        {
            int boardSize = currentBoard.getBoard().getSize();
            Board tempBoard;
            StateSpace tempStateSpace;
            Node node;
            for (int i = 0; i < boardSize; ++i)
            {
                for (int j = 0; j < boardSize-1; ++j)
                {
                    if (currentBoard.getBoard().getCell(i, j) == 'E' && currentBoard.getBoard().getCell(i, j + 1) == 'E')
                    {
                        tempBoard = new Board(currentBoard.getBoard());
                        tempBoard.changeTile('B', i, j);
                        tempBoard.changeTile('B', i, j+1);
                        tempStateSpace = new StateSpace(tempBoard);
                        tempStateSpace.calcScores();
                        node = new Node(currentDepth, tempStateSpace, false);
                        possibleMoves.Add(node);
                    }
                }
            }
        }
    }
}
