using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Board
    {
        private const int BoardSize = 3;

        private List<EntityOnTheBoard> _balls;
        private List<EntityOnTheBoard> _holes;

        private readonly Queue<Movement> _movementsMade = new Queue<Movement>();

        public static readonly Movement[] PossibleMovements;

        static Board()
        {
            PossibleMovements = Enum.GetValues(typeof(Movement)).Cast<Movement>().ToArray();
        }
        
        private Board(Board b)
        {
            _balls = b._balls.Select(ball => new EntityOnTheBoard(ball)).ToList();
            //TODO: Not efficient, clone holes only when ball matches a hole
            _holes = b._holes.Select(ball => new EntityOnTheBoard(ball)).ToList();

            _movementsMade = new Queue<Movement>(b._movementsMade);
        }
        
        public Board()
        {
            Init();
        }

        private void Init()
        {
            _balls = new List<EntityOnTheBoard>
            {
                new EntityOnTheBoard (0, 0, 0),
            };

            _holes = new List<EntityOnTheBoard>
            {
                new EntityOnTheBoard (0,1,1),
            };
        }

        public Board Go(Movement movement)
        {
            if (_movementsMade.Count > 0 )
            {
                var lastMove = _movementsMade.Last();
                
                if(lastMove.IsOppositeTo(movement))
                    return null;
            }
            
            var board = new Board(this);
            board.MoveBalls(movement);

            //check balls moved at all
            return BallsDidntMove(this, board) ? null : board;

            static bool BallsDidntMove(Board currentState, Board movedState)
            {
                return movedState._balls.All(b => 
                    currentState._balls.Any(thisBall => thisBall.Equals(b)));
            }
        }

        private void MoveBalls(Movement movement)
        {
            switch (movement)
            {
                case Movement.Up:
                    MoveBallsUp();
                    break;
                case Movement.Down:
                    MoveBallsDown();
                    break;
                case Movement.Right:
                    MoveBallsRight();
                    break;
                case Movement.Left:
                    MoveBallsLeft();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(movement), movement, null);
            }

            _movementsMade.Enqueue(movement);

            void MoveBallsUp()
            {
                foreach (var ball in _balls.OrderByDescending(b => b.YPos))
                {
                    if (ball.YPos < BoardSize - 1 && !_balls.Any(b => b.IsDirectlyAbove(ball)))
                        ball.YPos++;
                }
            }

            void MoveBallsDown()
            {
                foreach (var ball in _balls.OrderBy(b => b.YPos))
                {
                    if (ball.YPos > 0 && !_balls.Any(b => b.IsDirectlyUnder(ball)))
                        ball.YPos--;
                }
            }

            void MoveBallsRight()
            {
                foreach (var ball in _balls.OrderByDescending(b=>b.XPos))
                {
                    if (ball.XPos < BoardSize - 1 && !_balls.Any(b=>b.IsDirectlyOnTheRightTo(ball)))
                        ball.XPos++;
                }
            }

            void MoveBallsLeft()
            {
                foreach (var ball in _balls.OrderBy(b=>b.XPos))
                {
                    if (ball.XPos > 0 && !_balls.Any(b=>b.IsDirectlyOnTheLeftTo(ball)))
                        ball.XPos--;
                }
            }
        }


        public bool? CheckWinLose()
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