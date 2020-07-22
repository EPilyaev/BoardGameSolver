using System;

namespace Models
{
    public class EntityOnTheBoard
    {
        public EntityOnTheBoard(int id, int xPos, int yPos)
        {
            Id = id;
            XPos = xPos;
            YPos = yPos;
        }

        public EntityOnTheBoard(EntityOnTheBoard entityToClone)
        {
            Id = entityToClone.Id;
            XPos = entityToClone.XPos;
            YPos = entityToClone.YPos;
        }
        
        public int Id { get; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        
        public bool IsDirectlyAbove(EntityOnTheBoard other)
        {
            return Id != other.Id && XPos == other.XPos && YPos == other.YPos + 1;
        }
        
        public bool IsDirectlyUnder(EntityOnTheBoard other)
        {
            return Id != other.Id && XPos == other.XPos && YPos == other.YPos - 1;
        }

        public bool IsDirectlyOnTheRightTo(EntityOnTheBoard other)
        {
            return Id != other.Id && YPos == other.YPos && XPos == other.XPos + 1;
        }
        
        public bool IsDirectlyOnTheLeftTo(EntityOnTheBoard other)
        {
            return Id != other.Id && YPos == other.YPos && XPos == other.XPos - 1;
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((EntityOnTheBoard) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        
        private bool Equals(EntityOnTheBoard other)
        {
            return Id == other.Id && XPos == other.XPos && YPos == other.YPos;
        }
    }
}