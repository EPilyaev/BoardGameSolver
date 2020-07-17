using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class Board
    {
        private const int BoardSize = 3;

        private List<Ball> _balls;
        private List<EntityOnTheBoard> _holes;

        private Queue<Movement> _movements = new Queue<Movement>();

        public Board(int size)
        {
            var movements = Enum.GetValues(typeof(Movement)).Cast<Movement>();

            var result = CheckWinLose();

            if (result == true)
            {
                //win, return
            }

            if (result == false)
            {
                //lose, return
            }
            
            foreach (var movement in movements)
            {
                
            }
        }

        private void InitHoles()
        {
            _balls = new List<Ball>
            {
                new Ball {Id = 0, XPos = 0, YPos = 0},
            };

            _holes = new List<EntityOnTheBoard>
            {
                new EntityOnTheBoard {Id = 0, XPos = 1, YPos = 1},
            };
        }

        private bool? Go(Movement movement)
        {
           
            
            _movements.Enqueue(movement);

            foreach (var ball in _balls)
            {
                ball.Go(movement);
            }

            return null;
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
    }
}