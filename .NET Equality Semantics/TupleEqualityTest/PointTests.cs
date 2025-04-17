// Sample code from "Equality Semantics in .NET" by Gary McNickle (gmcnickle@outlook.com)
// Licensed under CC BY 4.0: https://creativecommons.org/licenses/by/4.0/

using Xunit;

namespace TupleEquality.Tests
{
    public class PointTests
    {
        public const bool ShouldBeEqual = true;
        public const bool ShouldBeDifferent = false;

        [Theory]
        [InlineData(0.0, 0.0, 0.0, 0.0, 0.0, 0.0, ShouldBeEqual)]
        [InlineData(0.1, 0.0, 0.0, 0.1, 0.0, 0.0, ShouldBeEqual)]
        [InlineData(0.00001, 0.0, 0.0, 0.0, 0.0, 0.0, ShouldBeDifferent)]
        [InlineData(0.1, 0.0, 0.0, 0.0, 0.0, 0.0, ShouldBeDifferent)]
        [InlineData(0.0, 1.0, 0.0, 0.0, 1.0, 0.0, ShouldBeEqual)]
        [InlineData(0.0, 1.0, 0.0, 0.0, 0.0, 0.0, ShouldBeDifferent)]
        [InlineData(0.0, 0.0, 1.0, 0.0, 0.0, 1.0, ShouldBeEqual)]
        [InlineData(0.0, 0.0, 1.0, 0.0, 0.0, 0.0, ShouldBeDifferent)]
        public void EqualityTest(float xa, float ya, float za, float xb, float yb, float zb, bool expectedResult)
        {
            var left = new Point(xa, ya, za);
            var right = new Point(xb, yb, zb);

            Assert.Equal(expectedResult, left.Equals(right));
            Assert.Equal(expectedResult, left == right);
            Assert.Equal(!expectedResult, left != right);
        }

        [Fact]
        public void Equality_WithNull_ReturnsFalse()
        {
            var point = new Point(1f, 2f, 3f);
            Point other = null;

            Assert.False(point.Equals(other));
            Assert.False(point == other);
            Assert.True(point != other);
        }
    }
}
