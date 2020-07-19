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

        private Queue<Movement> _movements = new Queue<Movement>();
        private static bool _solutionFound = false;

        private Board(Board b)
        {
            _balls = b._balls.Select(ball => new Ball(BoardSize) {XPos = ball.XPos, YPos = ball.YPos, Id = ball.Id})
                .ToList();
            
            _holes = new List<EntityOnTheBoard>(b._holes);

            _movements = new Queue<Movement>(b._movements);
        }
        
        public Board()
        {
            Init();
        }

        public Board SearchForSolution(int recursionDepth)
        {
            if (_solutionFound || recursionDepth <= 0)
                return null;
            
            recursionDepth--;
            
            var result = CheckWinLose();

            switch (result)
            {
                case true:
                    _solutionFound = true;
                    return this;
                case false:
                    return null;
            }

            var movements = Enum.GetValues(typeof(Movement)).Cast<Movement>();
            
            List<Board> boardMovementStates = new List<Board>();
            
            foreach (var movement in movements)
            {
                boardMovementStates.Add(Go(movement));
            }
            
            //Analyze board movement states and search for solution inside states
            foreach (var movementState in boardMovementStates)
            {
                var res  = movementState.SearchForSolution(recursionDepth);
                if (res != null) return res;
            }

            return null;
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
            var board = new Board(this);
            
            board._movements.Enqueue(movement);

            foreach (var ball in board._balls)
            {
                ball.Go(movement);
            }

            return board;
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

            foreach (var move in _movements)
            {
                sb.AppendLine(move.ToString());
            }

            return sb.ToString();
        }
    }
}