using FluentAssertions;
using System;
using Xunit;

namespace VATrainer.Test.ViewModels
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            bool test = false;

            // Act
            test = true;

            // Assert 
            test.Should().BeTrue();
        }
    }
}
