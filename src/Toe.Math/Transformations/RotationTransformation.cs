namespace Toe.Transformations
{
    public class RotationTransformation : AbstractTransformation
    {
        private Quaternion _quaternion;

        public RotationTransformation(Quaternion quaternion)
        {
            _quaternion = quaternion;
        }

        public Quaternion Quaternion
        {
            get { return _quaternion; }
        }

        public Matrix4 Matrix
        {
            get { return Matrix4.CreateFromQuaternion(_quaternion); }
        }

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
            return Vector3.Transform(v, _quaternion);
        }

        public override void Transform(ref Vector3 v, out Vector3 res)
        {
            Vector3.Transform(ref v, ref _quaternion, out res);
        }

        public override Vector4 Transform(Vector4 v)
        {
            return Vector4.Transform(v, _quaternion);
        }

        public override void Transform(ref Vector4 v, out Vector4 res)
        {
            Vector4.Transform(ref v, ref _quaternion, out res);
        }

        public override string ToString()
        {
            return string.Format("Q({0}, {1}, {2}, {3})", _quaternion.X, _quaternion.Y, _quaternion.Z, _quaternion.W);
        }
    }
}