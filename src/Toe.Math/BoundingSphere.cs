using System.Globalization;

namespace Toe
{
    public struct BoundingSphere
    {
        private readonly Vector3 _center;

        private readonly float _radius;

        public BoundingSphere(Vector3 center, float radius)
        {
            _center = center;
            _radius = radius;
        }

        public Vector3 Center { get { return _center; } }

        public float Radius { get { return _radius; } }

        public static bool operator ==(BoundingSphere left, BoundingSphere right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BoundingSphere left, BoundingSphere right)
        {
            return !left.Equals(right);
        }

        public bool Equals(BoundingSphere other)
        {
            return _center.Equals(other._center) && _radius.Equals(other._radius);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is BoundingSphere && Equals((BoundingSphere)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_center.GetHashCode() * 397) ^ _radius.GetHashCode();
            }
        }

        /// <summary>
        ///     Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="sphere">The instance.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The result of the calculation.</returns>
        public static BoundingSphere operator *(BoundingSphere sphere, float scale)
        {
            return new BoundingSphere(sphere._center * scale, sphere._radius * scale);
        }

        /// <summary>
        ///     Multiplies an instance by a scale.
        /// </summary>
        /// <param name="sphere">The instance.</param>
        /// <param name="scale">The scale vector.</param>
        /// <returns>The result of the calculation.</returns>
        public static BoundingSphere operator *(BoundingSphere sphere, Vector3 scale)
        {
            var s = scale.X;
            if (scale.Y > s)
                s = scale.Y;
            if (scale.Z > s)
                s = scale.Z;
            return new BoundingSphere(sphere._center * scale, sphere._radius * s);
        }

        public static BoundingSphere operator +(BoundingSphere sphere, Vector3 scale)
        {
            return new BoundingSphere(sphere._center + scale, sphere._radius);
        }

        public static BoundingSphere operator -(BoundingSphere sphere, Vector3 scale)
        {
            return new BoundingSphere(sphere._center - scale, sphere._radius);
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
            return string.Format("({0}{1} {2}{1} {3}{1} radius:{4})", _center.X, listSeparator, _center.Y, _center.Z, _radius);
        }

        #endregion

        #endregion
    }
}