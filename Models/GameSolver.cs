using System;
using System.Collections.Generic;

namespace Models
{
    public class GameSolver
    {
        public Board InitialState { get; }

        private int _overallMoves;
        public int OverallMoves => _overallMoves;

        public GameSolver()
        {
            InitialState = new Board(10);
            InitialState.AddBallAndHole(0,0, 2,3);
            InitialState.AddBallAndHole(7,5, 1,1);
            InitialState.AddBallAndHole(3,1, 8,9);
        }

        public Board SearchForSolution(int movesLeft = 10_000_000)
        {
            var boardStatesQueue = new Queue<Board>();
            boardStatesQueue.Enqueue(InitialState);
            
            while (movesLeft > 0 && boardStatesQueue.Count > 0)
            {
                var boardState = boardStatesQueue.Dequeue();

                var state = boardState.CheckBoardState();

                switch (state)
                {
                    case GameState.Win:
                        return boardState;
                    case GameState.Lose:
                        continue;
                    case GameState.Neutral:
                        EnqueueMovements(boardState, boardStatesQueue);
                        movesLeft--;
                        _overallMoves++;
                        break;
                    case GameState.CloserToWin:
                        boardStatesQueue.Clear();
                        EnqueueMovements(boardState, boardStatesQueue);
                        movesLeft--;
                        _overallMoves++;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return null;
        }

        private void EnqueueMovements(Board state, Queue<Board> movementsQueue)
        {
            foreach (var movement in Board.PossibleMovements)
            {
                var newState = state.Go(movement);

                if (newState != null)
                    movementsQueue.Enqueue(newState);
            }
        }
    }
}