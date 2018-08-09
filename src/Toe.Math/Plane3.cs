using System;
using System.Globalization;

namespace Toe
{
    public struct Plane3 : IEquatable<Plane3>
    {
        public static bool operator ==(Plane3 left, Plane3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Plane3 left, Plane3 right)
        {
            return !left.Equals(right);
        }

        public bool Equals(Plane3 other)
        {
            return Distance.Equals(other.Distance) && Normal.Equals(other.Normal);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Plane3 && Equals((Plane3) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Distance.GetHashCode()*397) ^ Normal.GetHashCode();
            }
        }

        #region Constants and Fields

        public static readonly Plane3 UnitX = new Plane3(Vector3.UnitX, 0.0f);

        public static readonly Plane3 UnitY = new Plane3(Vector3.UnitY, 0.0f);

        public static readonly Plane3 UnitZ = new Plane3(Vector3.UnitZ, 0.0f);

        #endregion

        #region Constructors and Destructors

        public Plane3(Vector3 normal, float distance)
        {
            Normal = normal;
            Distance = distance;
        }
        public Plane3(Vector3 a, Vector3 b, Vector3 c)
        {
            Normal = Vector3.Cross(b - a, c - a).Normalized();
            if (float.IsNaN(Normal.X) || float.IsNaN(Normal.Y) || float.IsNaN(Normal.Z))
                Normal = Vector3.UnitZ;
            Distance = Vector3.Dot(a,Normal);
        }
        #endregion

        #region Public Properties

        public float Distance { get; }

        public Vector3 Normal { get; }

        #endregion

        #region Public Methods and Operators

        public float GetDistance(Vector3 float3)
        {
            return Vector3.Dot(float3, Normal) - Distance;
        }


        public bool? GetSide(Vector3 float3)
        {
            var d = GetDistance(float3);
            if (d > 0)
                return true;
            if (d < 0)
                return false;
            return null;
        }

        public bool? GetSide(BoundingBox3 box)
        {
            var max = box.Max;
            var min = box.Min;
            var isXpos = Normal.X > 0;
            var isYpos = Normal.Y > 0;
            var isZpos = Normal.Z > 0;
            var farmost = new Vector3(isXpos ? max.X : min.X, isYpos ? max.Y : min.Y, isZpos ? max.Z : min.Z);
            var nearest = new Vector3(isXpos ? min.X : max.X, isYpos ? min.Y : max.Y, isZpos ? min.Z : max.Z);
            var farSide = GetSide(farmost);
            var nearSide = GetSide(nearest);

            if (farSide != nearSide)
                return null;
            return farSide;
        }

        #endregion
        #region Overrides

        #region public override string ToString()

        private static readonly string listSeparator = CultureInfo.CurrentCulture.TextInfo.ListSeparator;

        /// <summary>
        ///     Returns a System.String that represents the current Plane3.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("({0}{2} {1})", Normal, Distance);
        }

        #endregion

        #endregion
    }
}