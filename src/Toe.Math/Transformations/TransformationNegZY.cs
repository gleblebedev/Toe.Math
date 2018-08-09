namespace Toe.Transformations
{
    public class TransformationNegZY : AbstractTransformation
    {
        public static readonly TransformationNegZY Instance = new TransformationNegZY();
        public static readonly Quaternion Quaternion = new Quaternion(-0.5f, 0.5f, -0.5f, 0.5f);

        public static readonly Matrix4 Matrix = new Matrix4(0f, -1f, 0f, 0f, 0f, 0f, -1f, 0f, 1f, 0f, 0f, 1f, 0f, 0f, 0f,
            1f);

        public override Matrix4 GetMatrix()
        {
            return Matrix;
        }

        public override Quaternion GetQuaternion()
        {
            return Quaternion;
        }

        public override Vector3 Transform(Vector3 v)
        {
            return new Vector3(v.Z, -v.X, -v.Y);
        }

        public override void Transform(ref Vector3 v, out Vector3 res)
        {
            res = new Vector3(v.Z, -v.X, -v.Y);
        }

        public override Vector4 Transform(Vector4 v)
        {
            return new Vector4(v.Z, -v.X, -v.Y, v.W);
        }

        public override void Transform(ref Vector4 v, out Vector4 res)
        {
            res = new Vector4(v.Z, -v.X, -v.Y, v.W);
        }

        public override string ToString()
        {
            return "-ZY";
        }
    }
}