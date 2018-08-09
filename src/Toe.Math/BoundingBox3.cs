using System;
using System.Collections.Generic;
using System.Globalization;

namespace Toe
{
    public struct BoundingBox3
    {
        #region Constants and Fields

        public static readonly BoundingBox3 Empty = new BoundingBox3(AllMaxValues, AllMinValues);

        private static readonly Vector3 AllMaxValues = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

        private static readonly Vector3 AllMinValues = new Vector3(float.MinValue, float.MinValue, float.MinValue);

        private Vector3 max;

        private Vector3 min;

        #endregion

        #region Constructors and Destructors

        public BoundingBox3(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        public BoundingBox3(IEnumerable<Vector3> items)
        {
            min = AllMaxValues;
            max = AllMinValues;
            foreach (var Vector3 in items)
            {
                if (Vector3.X > max.X)
                {
                    max.X = Vector3.X;
                }
                if (Vector3.X < min.X)
                {
                    min.X = Vector3.X;
                }
                if (Vector3.Y > max.Y)
                {
                    max.Y = Vector3.Y;
                }
                if (Vector3.Y < min.Y)
                {
                    min.Y = Vector3.Y;
                }
                if (Vector3.Z > max.Z)
                {
                    max.Z = Vector3.Z;
                }
                if (Vector3.Z < min.Z)
                {
                    min.Z = Vector3.Z;
                }
            }
        }

        #endregion

        #region Public Properties

        public Vector3 Center
        {
            get { return (min + max)*0.5f; }
        }

        public bool IsEmpty
        {
            get { return min.X > max.X || min.Y > max.Y || min.Z > max.Z; }
        }

        public Vector3 Max
        {
            get { return max; }
        }

        public Vector3 Min
        {
            get { return min; }
        }

        public Vector3 Size
        {
            get
            {
                if (IsEmpty)
                {
                    return Vector3.Zero;
                }
                return max - min;
            }
        }

        public float Volume
        {
            get
            {
                return Size.Volume;
            }
        }

        #endregion

        #region Public Methods and Operators

        public BoundingBox3 Union(BoundingBox3 right)
        {
            if (IsEmpty)
                return right;
            if (right.IsEmpty)
                return this;
            return new BoundingBox3
            {
                min =
                    new Vector3(Math.Min(Min.X, right.Min.X), Math.Min(Min.Y, right.Min.Y), Math.Min(Min.Z, right.Min.Z)),
                max =
                    new Vector3(Math.Max(Max.X, right.Max.X), Math.Max(Max.Y, right.Max.Y), Math.Max(Max.Z, right.Max.Z))
            };
        }
        public void Union(ref BoundingBox3 right, out BoundingBox3 res)
        {
            if (IsEmpty)
            {
                res = right;
                return;
            }
            if (right.IsEmpty)
            {
                res = this;
                return;
            }
            res = new BoundingBox3
            {
                min =
                    new Vector3(Math.Min(Min.X, right.Min.X), Math.Min(Min.Y, right.Min.Y), Math.Min(Min.Z, right.Min.Z)),
                max =
                    new Vector3(Math.Max(Max.X, right.Max.X), Math.Max(Max.Y, right.Max.Y), Math.Max(Max.Z, right.Max.Z))
            };
        }
        public BoundingBox3 Union(Vector3 right)
        {
            if (IsEmpty)
                return new BoundingBox3 {min = right, max = right};
            return new BoundingBox3
            {
                min = new Vector3(Math.Min(Min.X, right.X), Math.Min(Min.Y, right.Y), Math.Min(Min.Z, right.Z)),
                max = new Vector3(Math.Max(Max.X, right.X), Math.Max(Max.Y, right.Y), Math.Max(Max.Z, right.Z))
            };
        }

        public static BoundingBox3 operator +(BoundingBox3 left, BoundingBox3 right)
        {
            return
                new BoundingBox3(
                    new Vector3(
                        Math.Min(left.min.X, right.min.X),
                        Math.Min(left.min.Y, right.min.Y),
                        Math.Min(left.min.Z, right.min.Z)),
                    new Vector3(
                        Math.Max(left.min.X, right.min.X),
                        Math.Max(left.min.Y, right.min.Y),
                        Math.Max(left.min.Z, right.min.Z)));
        }

        public static BoundingBox3 operator +(BoundingBox3 left, Vector3 right)
        {
            if (left.IsEmpty)
            {
                return left;
            }
            return new BoundingBox3(
                new Vector3(left.min.X + right.X, left.min.Y + right.Y, left.min.Z + right.Z),
                new Vector3(left.max.X + right.X, left.max.Y + right.Y, left.max.Z + right.Z));
        }

        public static bool operator ==(BoundingBox3 left, BoundingBox3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BoundingBox3 left, BoundingBox3 right)
        {
            return !left.Equals(right);
        }

        public bool Equals(BoundingBox3 other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is BoundingBox3 && Equals((BoundingBox3) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (min.GetHashCode()*397) ^ max.GetHashCode();
            }
        }

        public bool Contains(Vector3 v)
        {
            return (v.X >= min.X) && (v.X <= max.X)
                   && (v.Y >= min.Y) && (v.Y <= max.Y)
                   && (v.Z >= min.Z) && (v.Z <= max.Z);
        }

        public bool Contains(ref BoundingBox3 box)
        {
            return (box.min.X >= min.X) && (box.max.X <= max.X)
                   && (box.min.Y >= min.Y) && (box.max.Y <= max.Y)
                   && (box.min.Z >= min.Z) && (box.max.Z <= max.Z);
        }

        public bool Contains(ref Vector3 v)
        {
            return (v.X >= min.X) && (v.X <= max.X)
                   && (v.Y >= min.Y) && (v.Y <= max.Y)
                   && (v.Z >= min.Z) && (v.Z <= max.Z);
        }

        public bool Contains(ref Vector3 v, float eps)
        {
            return (v.X + eps >= min.X) && (v.X - eps <= max.X)
                   && (v.Y + eps >= min.Y) && (v.Y - eps <= max.Y)
                   && (v.Z + eps >= min.Z) && (v.Z - eps <= max.Z);
        }

        #endregion

        public BoundingBox3 ExpandBy(float step)
        {
            return ExpandBy(new Vector3(step, step, step));
        }

        public BoundingBox3 ExpandBy(Vector3 step)
        {
            return new BoundingBox3(min - step, max + step);
        }

        public static BoundingBox3 FromCenterAndSize(Vector3 center, Vector3 size)
        {
            var halfSize = size*0.5f;
            return new BoundingBox3(center- halfSize, center + halfSize);
        }

        public static BoundingBox3 FromCenterAndSize(Vector3 center, float size)
        {
            var halfSize = new Vector3(size, size, size) * 0.5f;
            return new BoundingBox3(center - halfSize, center + halfSize);
        }

        /// <summary>
        ///     Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="vec">The instance.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The result of the calculation.</returns>
        public static BoundingBox3 operator *(BoundingBox3 vec, float scale)
        {
            return new BoundingBox3(vec.Min * scale, vec.Max * scale);
        }

        /// <summary>
        ///     Multiplies an instance by a scale.
        /// </summary>
        /// <param name="vec">The instance.</param>
        /// <param name="scale">The scale vector.</param>
        /// <returns>The result of the calculation.</returns>
        public static BoundingBox3 operator *(BoundingBox3 vec, Vector3 scale)
        {
            return new BoundingBox3(vec.Min * scale, vec.Max * scale);
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
            return string.Format("{{{0} .. {1}}}", min, max);
        }

        #endregion

        #endregion

    }
}