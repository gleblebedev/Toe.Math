namespace Toe.Transformations
{
    public class IndentityTransformation : AbstractTransformation
    {
        public static readonly IndentityTransformation Instance = new IndentityTransformation();
        public static readonly Quaternion Quaternion = Quaternion.Identity;
        public static readonly Matrix4 Matrix = Matrix4.Identity;

        public override Matrix4 GetMatrix()
        {
            return Matrix4.Identity;
        }

        public override Quaternion GetQuaternion()
        {
            return Quaternion.Identity;
        }

        public override Vector3 Transform(Vector3 v)
        {
            return v;
        }

        public override void Transform(ref Vector3 v, out Vector3 res)
        {
            res = v;
        }

        public override Vector4 Transform(Vector4 v)
        {
            return v;
        }

        public override void Transform(ref Vector4 v, out Vector4 res)
        {
            res = v;
        }

        public override string ToString()
        {
            return "";
        }
    }
}