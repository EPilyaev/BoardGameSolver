using System;

namespace Models
{
    public class EntityOnTheBoard
    {
        protected readonly int BoardSize;

        public EntityOnTheBoard(int boardSize)
        {
            BoardSize = boardSize;
        }
        
        public int Id { get; set; }
        
        public int XPos { get; set; }
        public int YPos { get; set; }
    }
}