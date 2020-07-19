using System;

namespace Models
{
    public class Ball : EntityOnTheBoard
    {
        public Ball(int boardSize) : base(boardSize)
        {
        }
        
        public void Go(Movement movement)
        {
            switch (movement)
            {
                case Movement.Up:
                    if(YPos<BoardSize-1)
                        YPos++;
                    break;
                case Movement.Down:
                    if(YPos>0)
                        YPos--;
                    break;
                case Movement.Right:
                    if(XPos<BoardSize-1)
                        XPos++;
                    break;
                case Movement.Left:
                    if(XPos>0)
                        XPos--;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(movement), movement, null);
            }
        }
    }
}