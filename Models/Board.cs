using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Board
    {
        private const int BoardSize = 3;

        private List<Ball> _balls;
        private List<EntityOnTheBoard> _holes;

        private readonly Queue<Movement> _movementsMade = new Queue<Movement>();

        private static readonly Movement[] PossibleMovements;

        static Board()
        {
            PossibleMovements = Enum.GetValues(typeof(Movement)).Cast<Movement>().ToArray();
        }
        
        private Board(Board b)
        {
            _balls = b._balls.Select(ball => new Ball(BoardSize) {XPos = ball.XPos, YPos = ball.YPos, Id = ball.Id})
                .ToList();
            
            _holes = new List<EntityOnTheBoard>(b._holes);

            _movementsMade = new Queue<Movement>(b._movementsMade);
            
        }
        
        public Board()
        {
            Init();
        }

        //TODO: move to another class
        public Board SearchForSolution(int recursionDepth)
        {
            var result = CheckWinLose();

            switch (result)
            {
                case true:
                    return this;
                case false:
                    return null;
            }

            var movementsQueue = new Queue<Board>();

            EnqueueMovements(this, movementsQueue);

            while (recursionDepth>0 || movementsQueue.Count>0)
            {
                var move = movementsQueue.Dequeue();

                var winLose = move.CheckWinLose();

                if (winLose == true)
                {
                    return move;
                }

                if (winLose == false)
                {
                    //this move leads to loss, not need to search further
                    continue;
                }
                
                //Analyze board movement states and search for solution inside states
                EnqueueMovements(move, movementsQueue);

                recursionDepth--;
            }

            return null;
        }

        private void EnqueueMovements(Board state, Queue<Board> movementsQueue)
        {
            foreach (var movement in PossibleMovements)
            {
                var newState = state.Go(movement);
                
                if(newState != null)
                    movementsQueue.Enqueue(newState);
            }
        }

        private void Init()
        {
            _balls = new List<Ball>
            {
                new Ball (BoardSize) {Id = 0, XPos = 0, YPos = 0},
            };

            _holes = new List<EntityOnTheBoard>
            {
                new EntityOnTheBoard (BoardSize) {Id = 0, XPos = 1, YPos = 1},
            };
        }

        private Board Go(Movement movement)
        {
            if (_movementsMade.Count > 0 )
            {
                var lastMove = _movementsMade.Last();
                
                if(lastMove.IsOppositeTo(movement))
                    return null;
            }
            
            var board = new Board(this);
            
            board._movementsMade.Enqueue(movement);

            foreach (var ball in board._balls)
            {
                //Todo: make balls not to go through each other
                ball.Go(movement);
            }

            //check balls moved at all
            return BallsDidntMove(this, board) ? null : board;

            static bool BallsDidntMove(Board currentState, Board movedState)
            {
                return movedState._balls.All(b => 
                    currentState._balls.Any(thisBall => thisBall.Equals(b)));
            }
        }

        private bool? CheckWinLose()
        {
            //Check win
            List<int> matchedIds = new List<int>(_balls.Count);
            foreach (var ball in _balls)
            {
                var matchingHole = _holes.FirstOrDefault(h => h.XPos == ball.XPos && h.YPos == ball.YPos);
                if (matchingHole != null)
                {
                    if (matchingHole.Id == ball.Id)
                    {
                        //remove ball and hole from list
                        matchedIds.Add(matchingHole.Id);
                    }
                    else
                    {
                        //lose
                        return false;
                    }
                }
            }

            foreach (var id in matchedIds)
            {
                _balls.Remove(_balls.Find(b=>b.Id==id));
                _holes.Remove(_holes.Find(h=>h.Id==id));
            }

            if (_balls.Count == 0) return true;

            return null;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Movements:");

            foreach (var move in _movementsMade)
            {
                sb.AppendLine(move.ToString());
            }

            return sb.ToString();
        }
    }
}