namespace Toe.Transformations
{
    public abstract class AbstractTransformation
    {
        public static IndentityTransformation Indentity
        {
            get { return IndentityTransformation.Instance; }
        }

        public virtual Matrix4 GetMatrix()
        {
            return Matrix4.CreateFromQuaternion(GetQuaternion());
        }

        public abstract Quaternion GetQuaternion();

        public virtual Vector3 Transform(Vector3 v)
        {
            return Vector3.Transform(v, GetMatrix());
        }

        public virtual void Transform(ref Vector3 v, out Vector3 res)
        {
            var m = GetMatrix();
            Vector3.Transform(ref v, ref m, out res);
        }

        public virtual Vector4 Transform(Vector4 v)
        {
            return Vector4.Transform(v, GetMatrix());
        }

        public virtual void Transform(ref Vector4 v, out Vector4 res)
        {
            var m = GetMatrix();
            Vector4.Transform(ref v, ref m, out res);
        }

        public static bool TryParse(string transformation, out AbstractTransformation res)
        {
            switch (transformation)
            {
                case "":
                    res = IndentityTransformation.Instance;
                    return true;
                case "-X":
                    res = IndentityTransformation.Instance;
                    return true;
                case "-X-X":
                    res = IndentityTransformation.Instance;
                    return true;
                case "-X-X-Z":
                    res = IndentityTransformation.Instance;
                    return true;
                case "-X-Z":
                    res = IndentityTransformation.Instance;
                    return true;
                case "-X-Z-Z":
                    res = IndentityTransformation.Instance;
                    return true;
                case "-XZ":
                    res = IndentityTransformation.Instance;
                    return true;
                case "-Y":
                    res = IndentityTransformation.Instance;
                    return true;
                case "-Y-Y":
                    res = IndentityTransformation.Instance;
                    return true;
                case "-Y-Y-Z":
                    res = IndentityTransformation.Instance;
                    return true;
                case "-Y-Z":
                    res = IndentityTransformation.Instance;
                    return true;
                case "-Y-Z-Z":
                    res = IndentityTransformation.Instance;
                    return true;
                case "-YZ":
                    res = IndentityTransformation.Instance;
                    return true;
                case "-Z":
                    res = IndentityTransformation.Instance;
                    return true;
                case "-Z-Z":
                    res = IndentityTransformation.Instance;
                    return true;
                case "X":
                    res = IndentityTransformation.Instance;
                    return true;
                case "X-Z":
                    res = IndentityTransformation.Instance;
                    return true;
                case "X-Z-Z":
                    res = IndentityTransformation.Instance;
                    return true;
                case "XZ":
                    res = IndentityTransformation.Instance;
                    return true;
                case "Y":
                    res = IndentityTransformation.Instance;
                    return true;
                case "Y-Z":
                    res = IndentityTransformation.Instance;
                    return true;
                case "Y-Z-Z":
                    res = IndentityTransformation.Instance;
                    return true;
                case "YZ":
                    res = IndentityTransformation.Instance;
                    return true;
                case "Z":
                    res = IndentityTransformation.Instance;
                    return true;
                case "Z-Z":
                    res = IndentityTransformation.Instance;
                    return true;
                default:
                    res = IndentityTransformation.Instance;
                    return false;
            }
        }
    }
}