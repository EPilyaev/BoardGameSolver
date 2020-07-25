using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Board
    {
        private readonly int _boardSize;

        private readonly List<EntityOnTheBoard> _balls = new List<EntityOnTheBoard>();
        private readonly List<EntityOnTheBoard> _holes = new List<EntityOnTheBoard>();

        public Queue<Movement> MovementsMade { get; } = new Queue<Movement>();

        public static readonly Movement[] PossibleMovements;

        static Board()
        {
            PossibleMovements = Enum.GetValues(typeof(Movement)).Cast<Movement>().ToArray();
        }

        private Board(Board b)
        {
            _boardSize = b._boardSize;
            _balls = b._balls.Select(ball => new EntityOnTheBoard(ball)).ToList();
            _holes = new List<EntityOnTheBoard>(b._holes);

            MovementsMade = new Queue<Movement>(b.MovementsMade);
        }

        public Board(int size)
        {
            _boardSize = size;
        }

        public void AddBallAndHole(int xBallPos, int yBallPos, int xHolePos, int yHolePos)
        {
            int id = _balls.Count;

            if (xBallPos < 0 || xBallPos > _boardSize - 1 || yBallPos < 0 || yBallPos > _boardSize - 1)
                throw new InvalidPositionException(
                    $"Ball position is invalid. Both X and Y positions must be between 0 and {_boardSize}");
            if (xHolePos < 0 || xHolePos > _boardSize - 1 || yHolePos < 0 || yHolePos > _boardSize - 1)
                throw new InvalidPositionException(
                    $"Hole position is invalid. Both X and Y positions must be between 0 and {_boardSize}");

            var ball = new EntityOnTheBoard(id, xBallPos, yBallPos);
            var hole = new EntityOnTheBoard(id, xHolePos, yHolePos);

            if (_balls.Any(b => b.OccupiesSamePositionAs(ball)))
                throw new InvalidPositionException("There is already a ball on the same field!");

            if (_holes.Any(h => h.OccupiesSamePositionAs(hole)))
                throw new InvalidPositionException("There is already a hole on the same field");

            _balls.Add(ball);
            _holes.Add(hole);
        }

        public GameState CheckBoardState()
        {
            List<int> matchedIds = new List<int>(_balls.Count);
            foreach (var ball in _balls)
            {
                var matchingHole = _holes.FirstOrDefault(hole => hole.OccupiesSamePositionAs(ball));
                if (matchingHole != null)
                {
                    if (matchingHole.Id == ball.Id)
                    {
                        matchedIds.Add(matchingHole.Id);
                    }
                    else
                    {
                        return GameState.Lose;
                    }
                }
            }

            foreach (var id in matchedIds)
            {
                _balls.Remove(_balls.Find(b => b.Id == id));
                _holes.Remove(_holes.Find(h => h.Id == id));
            }

            if (_balls.Count == 0) return GameState.Win;

            var closerToWin = matchedIds.Count > 0;
            return closerToWin ? GameState.CloserToWin : GameState.Neutral;
        }

        public Board Go(Movement movement)
        {
            if (MovementsMade.Count > 0)
            {
                var lastMove = MovementsMade.Last();

                if (lastMove.IsOppositeTo(movement))
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

        public string DrawState()
        {
            StringBuilder sb = new StringBuilder();

            for (int y = _boardSize - 1; y >= 0; y--)
            {
                sb.Append("|");
                for (int x= 0; x<=_boardSize-1; x++)
                {
                    var ball = _balls.FirstOrDefault(b => b.XPos == x && b.YPos == y);
                    if (ball != null)
                    {
                        sb.Append($"b{ball.Id}|");
                    }
                    else
                    {
                        var hole = _holes.FirstOrDefault(h => h.XPos == x && h.YPos == y);
                        sb.Append(hole != null ? $"h{hole.Id}|" : "  |");
                    }
                }
                
                sb.Append($"{Environment.NewLine}");
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Movements:");

            foreach (var move in MovementsMade)
            {
                sb.AppendLine(move.ToString());
            }

            return sb.ToString();
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

            MovementsMade.Enqueue(movement);

            void MoveBallsUp()
            {
                foreach (var ball in _balls.OrderByDescending(b => b.YPos))
                {
                    if (ball.YPos < _boardSize - 1 && !_balls.Any(b => b.IsDirectlyAbove(ball)))
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
                foreach (var ball in _balls.OrderByDescending(b => b.XPos))
                {
                    if (ball.XPos < _boardSize - 1 && !_balls.Any(b => b.IsDirectlyOnTheRightTo(ball)))
                        ball.XPos++;
                }
            }

            void MoveBallsLeft()
            {
                foreach (var ball in _balls.OrderBy(b => b.XPos))
                {
                    if (ball.XPos > 0 && !_balls.Any(b => b.IsDirectlyOnTheLeftTo(ball)))
                        ball.XPos--;
                }
            }
        }
    }
}