using System.Collections.Generic;

namespace Models
{
    public class GameSolver
    {
        private readonly Board _initialState;

        public GameSolver()
        {
            _initialState = new Board();
        }

        public Board SearchForSolution(int movesLeft = 10_000)
        {
            var boardStatesQueue = new Queue<Board>();
            boardStatesQueue.Enqueue(_initialState);

            while (movesLeft > 0 && boardStatesQueue.Count > 0)
            {
                var boardState = boardStatesQueue.Dequeue();

                var winLose = boardState.CheckWinLose();

                switch (winLose)
                {
                    case true:
                        return boardState;
                    case false:
                        continue;
                    default:
                        EnqueueMovements(boardState, boardStatesQueue);
                        movesLeft--;
                        break;
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