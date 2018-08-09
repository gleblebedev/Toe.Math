using System;
using System.Globalization;
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
    public struct Vector2i : IEquatable<Vector2i>
    {
        #region Fields

        public static readonly Vector2i Zero = new Vector2i(0, 0);

        /// <summary>
        ///     The X component of the Vector3.
        /// </summary>
        public int X;

        /// <summary>
        ///     The Y component of the Vector3.
        /// </summary>
        public int Y;

        #endregion


        #region Constructors

        /// <summary>
        ///     Constructs a new instance.
        /// </summary>
        /// <param name="value">The value that will initialize this instance.</param>
        public Vector2i(int value)
        {
            X = value;
            Y = value;
        }

        /// <summary>
        ///     Constructs a new Vector3.
        /// </summary>
        /// <param name="x">The x component of the Vector3.</param>
        /// <param name="y">The y component of the Vector3.</param>
        /// <param name="z">The z component of the Vector3.</param>
        public Vector2i(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

        public bool Equals(Vector2i other)
        {
            return (X == other.X) && (Y == other.Y);
        }

        #region public float Length

        public int LengthSquared
        {
            get { return X*X + Y*Y; }
        }
        public int Area
        {
            get { return X * Y; }
        }
        #endregion

        /// <summary>
        ///     Adds the specified instances.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result of addition.</returns>
        public static Vector2i operator +(Vector2i left, Vector2i right)
        {
            left.X += right.X;
            left.Y += right.Y;
            return left;
        }

        public static Vector2i operator -(Vector2i left, Vector2i right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            return left;
        }

        public static Vector2i operator *(Vector2i left, Vector2i right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            return left;
        }

        public static Vector2i operator *(Vector2i left, int right)
        {
            left.X *= right;
            left.Y *= right;
            return left;
        }
        #region Overrides

        #region public override string ToString()

        private static readonly string listSeparator = CultureInfo.CurrentCulture.TextInfo.ListSeparator;

        /// <summary>
        ///     Returns a System.String that represents the current Vector2.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("({0}{2} {1})", X, Y, listSeparator);
        }

        #endregion

        #endregion
    }
}