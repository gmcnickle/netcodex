// Sample code from "Equality Semantics in .NET" by Gary McNickle (gmcnickle@outlook.com)
// Licensed under CC BY 4.0: https://creativecommons.org/licenses/by/4.0/

using System;

namespace TupleEquality.Tests
{
    public class Point(float x, float y, float z) : IEquatable<Point>
    {
        public float X { get; } = x;

        public float Y { get; } = y;

        public float Z { get; } = z;

        private (float, float, float) AsTuple => (X, Y, Z);

        public override int GetHashCode() => AsTuple.GetHashCode();

        public static bool operator !=(Point left, Point right) => !Equals(left, right);

        public static bool operator ==(Point left, Point right) => Equals(left, right);

        public override bool Equals(object obj) => Equals(this, obj as Point);

        public bool Equals(Point other) => other is not null && Equals(this, other);

        protected static bool Equals(Point left, Point right) => ReferenceEquals(left, right) || left?.AsTuple == right?.AsTuple;
    }
}
