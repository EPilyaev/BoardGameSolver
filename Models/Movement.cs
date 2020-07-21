using System;

namespace Models
{
    public enum Movement
    {
        Up, Down, Right, Left
    }

    public static class MovementExtensions
    {
        public static bool IsOppositeTo(this Movement movement, Movement otherMovement)
        {
            return movement switch
            {
                Movement.Up => otherMovement == Movement.Down,
                Movement.Down => otherMovement == Movement.Up,
                Movement.Right => otherMovement == Movement.Left,
                Movement.Left => otherMovement == Movement.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(movement), movement, null)
            };
        }
    }
}