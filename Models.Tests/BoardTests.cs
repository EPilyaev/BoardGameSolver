using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Models.Tests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void StaticCtor_ShouldInitPossibleMovementsToAllMovementsEnumValues()
        {
            //Arrange
            var allPossibleMovements = Enum.GetValues(typeof(Movement)).Cast<Movement>().ToArray();
            
            //Act and Assert
            Board.PossibleMovements.Should().BeEquivalentTo(allPossibleMovements);
        }
    }
}