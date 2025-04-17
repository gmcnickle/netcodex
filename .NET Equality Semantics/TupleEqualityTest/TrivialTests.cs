// Sample code from "Equality Semantics in .NET" by Gary McNickle (gmcnickle@outlook.com)
// Licensed under CC BY 4.0: https://creativecommons.org/licenses/by/4.0/

using Xunit;

namespace TupleEquality.Tests
{
    public class TrivialTests
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
            var left = new Trivial(xa, ya, za);
            var right = new Trivial(xb, yb, zb);

            Assert.Equal(expectedResult, left.Equals(right));
            Assert.NotSame(left, right);
        }

        [Theory]
        [InlineData(0.0, 0.0, 0.0, 0.0, 0.0, 0.0, ShouldBeEqual)]
        [InlineData(0.1, 0.0, 0.0, 0.1, 0.0, 0.0, ShouldBeEqual)]
        [InlineData(0.00001, 0.0, 0.0, 0.0, 0.0, 0.0, ShouldBeDifferent)]
        [InlineData(0.1, 0.0, 0.0, 0.0, 0.0, 0.0, ShouldBeDifferent)]
        [InlineData(0.0, 1.0, 0.0, 0.0, 1.0, 0.0, ShouldBeEqual)]
        [InlineData(0.0, 1.0, 0.0, 0.0, 0.0, 0.0, ShouldBeDifferent)]
        [InlineData(0.0, 0.0, 1.0, 0.0, 0.0, 1.0, ShouldBeEqual)]
        [InlineData(0.0, 0.0, 1.0, 0.0, 0.0, 0.0, ShouldBeDifferent)]
        public void ValueEquality_DoesNotImplyReferenceEquality(float xa, float ya, float za, float xb, float yb, float zb, bool expectedResult)
        {
            var left = new Trivial(xa, ya, za);
            var right = new Trivial(xb, yb, zb);

            Assert.Equal(expectedResult, left.Equals(right));
            Assert.Equal(expectedResult, left == right);
            Assert.Equal(!expectedResult, left != right);
            Assert.False(object.ReferenceEquals(left, right), "Instances are not the same reference");
        }

        [Fact]
        public void SameInstance_ShouldBeReferenceEqualAndValueEqual()
        {
            var instance = new Trivial(1f, 2f, 3f);

#pragma warning disable CS1718 // Comparison made to same variable
            Assert.True(instance == instance);
#pragma warning restore CS1718 
            Assert.True(instance.Equals(instance));
            Assert.True(object.ReferenceEquals(instance, instance));
        }

        [Fact]
        public void Equality_WithNull_ReturnsFalse()
        {
            var trivial = new Trivial(1f, 2f, 3f);
            Trivial other = null;

            Assert.False(trivial.Equals(other));
            Assert.False(trivial == other);
            Assert.True(trivial != other);
        }
    }
}
