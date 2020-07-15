using System.Collections.Generic;

namespace Models
{
    public class Board
    {
        private const int BoardSize = 3;

        private List<EntityOnTheBoard> _balls;
        private List<EntityOnTheBoard> _holes;

        //Todo: implement movements
        
        public Board(int size)
        {
            
        }

        private void InitHoles()
        {
            _balls = new List<EntityOnTheBoard>
            {
                new EntityOnTheBoard {Id = 0, XPos = 0, YPos = 0},
            };
            
            _holes = new List<EntityOnTheBoard>
            {
                new EntityOnTheBoard {Id = 0, XPos = 1, YPos = 1},
            };
        }
    }
}