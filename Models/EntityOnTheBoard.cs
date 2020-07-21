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

        protected bool Equals(EntityOnTheBoard other)
        {
            return Id == other.Id && XPos == other.XPos && YPos == other.YPos;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EntityOnTheBoard) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, XPos, YPos);
        }
    }
}