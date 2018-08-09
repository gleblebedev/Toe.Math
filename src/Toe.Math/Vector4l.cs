using System;
using System.Runtime.InteropServices;

namespace Toe
{
    /// <summary>
    ///     Represents a 4D vector using four integer numbers.
    /// </summary>
    /// <remarks>
    ///     The Vector4l structure is suitable for interoperation with unmanaged code requiring three consecutive ints.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector4l : IEquatable<Vector4l>
    {
        #region Fields

        /// <summary>
        ///     The X component of the Vector4l.
        /// </summary>
        public long X;

        /// <summary>
        ///     The Y component of the Vector4l.
        /// </summary>
        public long Y;

        /// <summary>
        ///     The Z component of the Vector4l.
        /// </summary>
        public long Z;

        /// <summary>
        ///     The W component of the Vector4l.
        /// </summary>
        public long W;

        #endregion


        #region Constructors

        /// <summary>
        ///     Constructs a new instance.
        /// </summary>
        /// <param name="value">The value that will initialize this instance.</param>
        public Vector4l(long value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }

        /// <summary>
        ///     Constructs a new Vector3i.
        /// </summary>
        /// <param name="x">The x component of the Vector4l.</param>
        /// <param name="y">The y component of the Vector4l.</param>
        /// <param name="z">The z component of the Vector4l.</param>
        /// <param name="w">The w component of the Vector4l.</param>
        public Vector4l(long x, long y, long z, long w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        ///     Constructs a new Vector3i.
        /// </summary>
        public Vector4l(Vector4 v)
        {
            X = (int)v.X;
            Y = (int)v.Y;
            Z = (int)v.Z;
            W = (int)v.W;
        }

        #endregion

        public bool Equals(Vector4l other)
        {
            return (X == other.X) && (Y == other.Y) && (Z == other.Z) && (W == other.W);
        }

        /// <summary>
        ///     Adds the specified instances.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result of addition.</returns>
        public static Vector4l operator +(Vector4l left, Vector4l right)
        {
            left.X += right.X;
            left.Y += right.Y;
            left.Z += right.Z;
            left.W += right.W;
            return left;
        }

        public static Vector4l operator -(Vector4l left, Vector4l right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            left.Z -= right.Z;
            left.W -= right.W;
            return left;
        }

        public static Vector4l operator *(Vector4l left, Vector4l right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            left.Z *= right.Z;
            left.W *= right.W;
            return left;
        }

        public static Vector4l operator *(Vector4l left, int right)
        {
            left.X *= right;
            left.Y *= right;
            left.Z *= right;
            left.W *= right;
            return left;
        }
        #region public float Length

        public long LengthSquared
        {
            get { return X * X + Y * Y + Z * Z + W * W; }
        }
        public long Volume
        {
            get { return X * Y * Z * W; }
        }
        #endregion
    }
}