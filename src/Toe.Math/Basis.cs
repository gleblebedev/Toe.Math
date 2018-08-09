namespace Toe
{
    public class Basis
    {
        public Vector3 Forward;
        public Vector3 Origin;
        public Vector3 Right;
        public Vector3 Up;

        public void FromViewMatrix(ref Matrix4 viewMatrix)
        {
            var i = viewMatrix.Inverted();
            var v = new Vector3(0, 0, 0);
            Vector3.Transform(ref v, ref i, out Origin);
            v = new Vector3(0, 0, -1);
            Vector3.Transform(ref v, ref i, out Forward);
            v = new Vector3(1, 0, 0);
            Vector3.Transform(ref v, ref i, out Right);
            v = new Vector3(0, 1, 0);
            Vector3.Transform(ref v, ref i, out Up);
            Forward -= Origin;
            Right -= Origin;
            Up -= Origin;

            Right.Normalize();
            Up.Normalize();
            Forward.Normalize();
        }
    }
}