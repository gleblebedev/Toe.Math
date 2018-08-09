using System;
using System.Runtime.InteropServices;

namespace Toe
{
    /// <summary>
    ///     Represents a 3D vector using three integer numbers.
    /// </summary>
    /// <remarks>
    ///     The Vector3i structure is suitable for interoperation with unmanaged code requiring three consecutive ints.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3i : IEquatable<Vector3i>
    {
        #region Fields

        /// <summary>
        ///     The X component of the Vector3.
        /// </summary>
        public int X;

        /// <summary>
        ///     The Y component of the Vector3.
        /// </summary>
        public int Y;

        /// <summary>
        ///     The Z component of the Vector3.
        /// </summary>
        public int Z;

        #endregion


        #region Constructors

        /// <summary>
        ///     Constructs a new instance.
        /// </summary>
        /// <param name="value">The value that will initialize this instance.</param>
        public Vector3i(int value)
        {
            X = value;
            Y = value;
            Z = value;
        }

        /// <summary>
        ///     Constructs a new Vector3i.
        /// </summary>
        /// <param name="x">The x component of the Vector3.</param>
        /// <param name="y">The y component of the Vector3.</param>
        /// <param name="z">The z component of the Vector3.</param>
        public Vector3i(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        ///     Constructs a new Vector3i.
        /// </summary>
        public Vector3i(Vector3 v)
        {
            X = (int)v.X;
            Y = (int)v.Y;
            Z = (int)v.Z;
        }

        #endregion

        public bool Equals(Vector3i other)
        {
            return (X == other.X) && (Y == other.Y) && (Z == other.Z);
        }

        /// <summary>
        ///     Adds the specified instances.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result of addition.</returns>
        public static Vector3i operator +(Vector3i left, Vector3i right)
        {
            left.X += right.X;
            left.Y += right.Y;
            left.Z += right.Z;
            return left;
        }

        public static Vector3i operator -(Vector3i left, Vector3i right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            left.Z -= right.Z;
            return left;
        }

        public static Vector3i operator *(Vector3i left, Vector3i right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            left.Z *= right.Z;
            return left;
        }

        public static Vector3i operator *(Vector3i left, int right)
        {
            left.X *= right;
            left.Y *= right;
            left.Z *= right;
            return left;
        }

        #region public float Length

        public int LengthSquared
        {
            get { return X * X + Y * Y + Z * Z; }
        }
        public int Volume
        {
            get { return X * Y * Z; }
        }
        #endregion
    }
}