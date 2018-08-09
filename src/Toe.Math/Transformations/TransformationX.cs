namespace Toe.Transformations
{
    public class TransformationX : AbstractTransformation
    {
        public static readonly TransformationX Instance = new TransformationX();

        public static readonly Quaternion Quaternion = new Quaternion(0.70710678118654752440084436210485f, 0f, 0f,
            0.70710678118654752440084436210485f);

        public static readonly Matrix4 Matrix = new Matrix4(1f, 0f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, -1f, 0f, 1f, 0f, 0f, 0f,
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
            return new Vector3(v.X, -v.Z, v.Y);
        }

        public override void Transform(ref Vector3 v, out Vector3 res)
        {
            res = new Vector3(v.X, -v.Z, v.Y);
        }

        public override Vector4 Transform(Vector4 v)
        {
            return new Vector4(v.X, -v.Z, v.Y, v.W);
        }

        public override void Transform(ref Vector4 v, out Vector4 res)
        {
            res = new Vector4(v.X, -v.Z, v.Y, v.W);
        }

        public override string ToString()
        {
            return "X";
        }
    }
}