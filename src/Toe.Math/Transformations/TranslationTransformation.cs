namespace Toe.Transformations
{
    public class TranslationTransformation : AbstractTransformation
    {
        private Vector3 _offset;

        public TranslationTransformation(Vector3 offset)
        {
            _offset = offset;
        }

        public Quaternion Quaternion
        {
            get { return Quaternion.Identity; }
        }

        public Matrix4 Matrix
        {
            get { return Matrix4.CreateTranslation(_offset); }
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
            return v + _offset;
        }

        public override void Transform(ref Vector3 v, out Vector3 res)
        {
            Vector3.Add(ref v, ref _offset, out res);
        }

        public override Vector4 Transform(Vector4 v)
        {
            return new Vector4(_offset, 0) + v;
        }

        public override void Transform(ref Vector4 v, out Vector4 res)
        {
            var _o = new Vector4(_offset, 0);
            Vector4.Add(ref v, ref _o, out res);
        }

        public override string ToString()
        {
            return string.Format("V({0}, {1}, {2})", _offset.X, _offset.Y, _offset.Z);
        }
    }
}