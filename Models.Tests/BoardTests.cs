using FluentAssertions;
using NUnit.Framework;

namespace Models.Tests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void StaticCtor_ShouldInitPossibleMovementsToAllAllowedMovements()
        {
            //Arrange
            var allPossibleMovements = new [] { Movement.Down, Movement.Left, Movement.Right, Movement.Up};
            
            //Act and Assert
            Board.PossibleMovements.Should().BeEquivalentTo(allPossibleMovements);
        }
    }
}