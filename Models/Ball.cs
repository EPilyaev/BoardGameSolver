using System;

namespace Models
{
    public class Ball : EntityOnTheBoard
    {
        public void Go(Movement movement)
        {
            switch (movement)
            {
                case Movement.Up:
                    YPos++;
                    break;
                case Movement.Down:
                    YPos--;
                    break;
                case Movement.Right:
                    XPos++;
                    break;
                case Movement.Left:
                    XPos--;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(movement), movement, null);
            }
        }
    }
}