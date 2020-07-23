using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Models.Tests
{
    [TestFixture]
    public class EntityOnTheBoardTests 
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(23)]
        [TestCase(100025)]
        public void Ctor_ShouldInitWithCorrectId(int id)
        {
            //Arrange and Act
            var entity = new EntityOnTheBoard(id, 0,0);

            //Assert
            entity.Id.Should().Be(id);
        }
        
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(23)]
        [TestCase(100025)]
        public void Ctor_ShouldInitWithCorrectXPos(int xPos)
        {
            //Arrange and Act
            var entity = new EntityOnTheBoard(0, xPos,0);

            //Assert
            entity.XPos.Should().Be(xPos);
        }
        
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(23)]
        [TestCase(100025)]
        public void Ctor_ShouldInitWithCorrectYPos(int yPos)
        {
            //Arrange and Act
            var entity = new EntityOnTheBoard(0, 0,yPos);

            //Assert
            entity.YPos.Should().Be(yPos);
        }

        [Test]
        public void Ctor_ShouldInitEqualToEntityToClone()
        {
            //Arrange
            var entityToClone = new EntityOnTheBoard(1,2,3);
            
            //Act
            var cloned = new EntityOnTheBoard(entityToClone);
            
            //Assert
            cloned.Should().Be(entityToClone);
        }

        [Test]
        public void IsDirectlyAbove_WhenOtherHasSameId_ShouldReturnFalse()
        {
            //Arrange
            var sameId = 0;
            var entityOne = new EntityOnTheBoard(sameId, 1, 0);
            var entityAbove = new EntityOnTheBoard(sameId,1,1);
            
            //Act
            var isAbove = entityAbove.IsDirectlyAbove(entityOne);

            //Assert
            isAbove.Should().BeFalse();
        }

        [Test]
        public void IsDirectlyAbove_WhenOtherHasDifferentIdAndIsAbove_ShouldReturnTrue()
        {
            //Arrange
            var entityOne = new EntityOnTheBoard(0, 1, 0);
            var entityAbove = new EntityOnTheBoard(1,1,1);
            
            //Act
            var isAbove = entityAbove.IsDirectlyAbove(entityOne);

            //Assert
            isAbove.Should().BeTrue();
        }
        
        [Test]
        public void IsDirectlyAbove_WhenOtherHasDifferentIdAndIsNotAbove_ShouldReturnFalse()
        {
            //Arrange
            var entityOne = new EntityOnTheBoard(0, 1, 0);
            var entityAbove = new EntityOnTheBoard(1,2,3);
            
            //Act
            var isAbove = entityAbove.IsDirectlyAbove(entityOne);

            //Assert
            isAbove.Should().BeFalse();
        }

        [Test]
        public void IsDirectlyUnder_WhenOtherHasSameId_ShouldReturnFalse()
        {
            //Arrange
            var sameId = 0;
            var entityOne = new EntityOnTheBoard(sameId, 1, 1);
            var entityUnder = new EntityOnTheBoard(sameId,1,0);
            
            //Act
            var isUnder = entityUnder.IsDirectlyUnder(entityOne);

            //Assert
            isUnder.Should().BeFalse();
        }

        [Test]
        public void IsDirectlyOnTheRightTo_WhenOtherHasSameId_ShouldReturnFalse()
        {
            //Arrange
            var sameId = 0;
            var entityOne = new EntityOnTheBoard(sameId, 1, 0);
            var entityOnTheRight = new EntityOnTheBoard(sameId,2,0);
            
            //Act
            var isOnTheRight = entityOnTheRight.IsDirectlyOnTheRightTo(entityOne);

            //Assert
            isOnTheRight.Should().BeFalse();
        }

        [Test]
        public void IsDirectlyOnTheLeftTo_WhenOtherHasSameId_ShouldReturnFalse()
        {
            //Arrange
            var sameId = 0;
            var entityOne = new EntityOnTheBoard(sameId, 1, 0);
            var entityOnTheLeft = new EntityOnTheBoard(sameId,0,0);
            
            //Act
            var isOnTheLeft = entityOnTheLeft.IsDirectlyAbove(entityOne);

            //Assert
            isOnTheLeft.Should().BeFalse();
        }
    }
}