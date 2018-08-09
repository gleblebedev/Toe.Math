using System;
using System.Runtime.InteropServices;

namespace Toe
{
    /// <summary>
    ///     Represents a 4D vector using four integer numbers.
    /// </summary>
    /// <remarks>
    ///     The Vector4i structure is suitable for interoperation with unmanaged code requiring three consecutive ints.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector4i : IEquatable<Vector4i>
    {
        #region Fields

        /// <summary>
        ///     The X component of the Vector4i.
        /// </summary>
        public int X;

        /// <summary>
        ///     The Y component of the Vector4i.
        /// </summary>
        public int Y;

        /// <summary>
        ///     The Z component of the Vector4i.
        /// </summary>
        public int Z;

        /// <summary>
        ///     The W component of the Vector4i.
        /// </summary>
        public int W;

        #endregion

        /// <summary>
        ///     Defines a zero-length Vector4.
        /// </summary>
        public static readonly Vector4i Zero = new Vector4i(0, 0, 0, 0);

        #region Constructors

        /// <summary>
        ///     Constructs a new instance.
        /// </summary>
        /// <param name="value">The value that will initialize this instance.</param>
        public Vector4i(int value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }

        /// <summary>
        ///     Constructs a new Vector3i.
        /// </summary>
        /// <param name="x">The x component of the Vector4i.</param>
        /// <param name="y">The y component of the Vector4i.</param>
        /// <param name="z">The z component of the Vector4i.</param>
        /// <param name="w">The w component of the Vector4i.</param>
        public Vector4i(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        ///     Constructs a new Vector3i.
        /// </summary>
        public Vector4i(Vector4 v)
        {
            X = (int)v.X;
            Y = (int)v.Y;
            Z = (int)v.Z;
            W = (int)v.W;
        }

        #endregion

        public bool Equals(Vector4i other)
        {
            return (X == other.X) && (Y == other.Y) && (Z == other.Z) && (W == other.W);
        }

        /// <summary>
        ///     Adds the specified instances.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result of addition.</returns>
        public static Vector4i operator +(Vector4i left, Vector4i right)
        {
            left.X += right.X;
            left.Y += right.Y;
            left.Z += right.Z;
            left.W += right.W;
            return left;
        }

        public static Vector4i operator -(Vector4i left, Vector4i right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            left.Z -= right.Z;
            left.W -= right.W;
            return left;
        }

        public static Vector4i operator *(Vector4i left, Vector4i right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            left.Z *= right.Z;
            left.W *= right.W;
            return left;
        }

        public static Vector4i operator *(Vector4i left, int right)
        {
            left.X *= right;
            left.Y *= right;
            left.Z *= right;
            left.W *= right;
            return left;
        }

        /// <summary>
        ///     Gets or sets the value at the index of the Vector.
        /// </summary>
        public int this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return X;
                }
                if (index == 1)
                {
                    return Y;
                }
                if (index == 2)
                {
                    return Z;
                }
                if (index == 3)
                {
                    return W;
                }
                throw new IndexOutOfRangeException("You tried to access this vector at index: " + index);
            }
            set
            {
                if (index == 0)
                {
                    X = value;
                }
                else if (index == 1)
                {
                    Y = value;
                }
                else if (index == 2)
                {
                    Z = value;
                }
                else if (index == 3)
                {
                    W = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("You tried to set this vector at index: " + index);
                }
            }
        }
        #region public float Length

        public int LengthSquared
        {
            get { return X * X + Y * Y + Z * Z + W * W; }
        }
        public int Volume
        {
            get { return X * Y * Z * W; }
        }
        #endregion
    }
}