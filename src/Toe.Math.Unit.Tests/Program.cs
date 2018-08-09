using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Toe.Transformations;

namespace Toe.Math.Unit.Tests
{
    internal class ConfigurationTransformation
    {
        private readonly string _name;
        private readonly Quaternion _rotation;
        private readonly Matrix4 _matrix;

        public Matrix4 Matrix 
        {
            get { return _matrix; }
        }
        public string Name
        {
            get { return _name; }
        }
        public Quaternion Quaternion
        {
            get { return _rotation; }
        }

        public ConfigurationTransformation(string name, Quaternion rotation)
        {
            
            _name = name;

            _rotation = rotation;

            _matrix = Matrix4.CreateFromQuaternion(rotation);
            _matrix = new Matrix4(
                Round(_matrix.M11),
                Round(_matrix.M12),
                Round(_matrix.M13),
                Round(_matrix.M14),

                Round(_matrix.M21),
                Round(_matrix.M22),
                Round(_matrix.M23),
                Round(_matrix.M24),

                Round(_matrix.M31),
                Round(_matrix.M32),
                Round(_matrix.M33),
                Round(_matrix.M34),

                Round(_matrix.M41),
                Round(_matrix.M42),
                Round(_matrix.M43),
                Round(_matrix.M44));
        }

        float Round(float v)
        {
            return (int) v;
        }
    }

    public class Program
	{
        private static ConfigurationTransformation[] _transformations = new[]
        {
            new ConfigurationTransformation("NegZ", Quaternion.FromAxisAngle(-Vector3.UnitZ, MathHelper.PiOver2)), 
            new ConfigurationTransformation("Z",    Quaternion.FromAxisAngle( Vector3.UnitZ, MathHelper.PiOver2)), 
            new ConfigurationTransformation("NegY", Quaternion.FromAxisAngle(-Vector3.UnitY, MathHelper.PiOver2)), 
            new ConfigurationTransformation("Y",    Quaternion.FromAxisAngle( Vector3.UnitY, MathHelper.PiOver2)), 
            new ConfigurationTransformation("NegX", Quaternion.FromAxisAngle(-Vector3.UnitX, MathHelper.PiOver2)), 
            new ConfigurationTransformation("X",    Quaternion.FromAxisAngle( Vector3.UnitX, MathHelper.PiOver2)), 
        };

		static void Main(string[] args)
		{
		    var m = new Dictionary<Matrix4, ConfigurationTransformation>();
		    foreach (var transformation in _transformations)
		    {
		        m[transformation.Matrix] = transformation;
		    }
            var queue = new Queue<ConfigurationTransformation>(_transformations);

            while (queue.Count > 0)
            {
                var t = queue.Dequeue();
		        foreach (var transformation in _transformations)
		        {
                    var q = transformation.Quaternion * t.Quaternion;
                    var mm = t.Matrix * transformation.Matrix;
                    var res = new ConfigurationTransformation(t.Name + transformation.Name, q);
                    Assert.AreEqual(mm,res.Matrix);
		            var v1 = new Vector3(1, 2, 3);
		            var v2 = Vector3.Transform(v1, t.Quaternion);
                    var v3 = Vector3.Transform(v2, transformation.Quaternion);
                    var v3_ = Vector3.Transform(v1, q);
                    if (System.Math.Abs(v3.X - v3.X) > 1e-6 || System.Math.Abs(v3.Y - v3.Y) > 1e-6 || System.Math.Abs(v3.Y - v3.Y) > 1e-6)
                        Assert.Fail(v3 +" != "+ v3_);
                    if (!m.ContainsKey(res.Matrix))
		            {
		                m.Add(res.Matrix, res);
                        queue.Enqueue(res);
		            }
		        }
		    }

            Matrix4 unknownTr = _transformations.First(_ => _.Name == "Z").Matrix * _transformations.First(_ => _.Name == "X").Matrix;
            Matrix4 unknownTr2 = _transformations.First(_ => _.Name == "X").Matrix * _transformations.First(_ => _.Name == "Z").Matrix;
            var name = m[unknownTr].Name;
            var name2 = m[unknownTr2].Name;


            foreach (var transformation in m.Values.OrderBy(_ => _.Name))
		    {
                Debug.WriteLine(string.Format("case \"{0}\": res = IndentityTransformation.Instance; return true;", transformation.Name.Replace("Neg", "-")));

		    }
            var dir = @"D:\MyWork\toe.math\csharp\common\Toe.Math\Transformations";
            foreach (var transformation in m.Values.OrderBy(_ => _.Name))
            {
                var sb = new StringBuilder();
                sb.AppendLine("namespace Toe.Transformations");
sb.AppendLine("{");
sb.AppendLine("    public class Transformation" + transformation.Name + ": AbstractTransformation");
sb.AppendLine("    {");
sb.AppendLine(string.Format("        public readonly static Transformation{0} Instance = new Transformation{0}();", transformation.Name));
                
sb.AppendLine(string.Format("        public readonly static Quaternion Quaternion = new Quaternion({0}f,{1}f,{2}f,{3}f);",
    ToStr(transformation.Quaternion.X), ToStr(transformation.Quaternion.Y), ToStr(transformation.Quaternion.Z), ToStr(transformation.Quaternion.W)));
sb.AppendLine(string.Format("        public readonly static Matrix4 Matrix = new Matrix4({0}f,{1}f,{2}f,{3}f,{4}f,{5}f,{6}f,{7}f,{8}f,{9}f,{10}f,{11}f,{12}f,{13}f,{14}f,{15}f);"
    , ToStr(transformation.Matrix.M11)
    , ToStr(transformation.Matrix.M12)
    , ToStr(transformation.Matrix.M13)
    , ToStr(transformation.Matrix.M14)
    , ToStr(transformation.Matrix.M21)
    , ToStr(transformation.Matrix.M22)
    , ToStr(transformation.Matrix.M23)
    , ToStr(transformation.Matrix.M24)
    , ToStr(transformation.Matrix.M31)
    , ToStr(transformation.Matrix.M32)
    , ToStr(transformation.Matrix.M33)
    , ToStr(transformation.Matrix.M44)
    , ToStr(transformation.Matrix.M41)
    , ToStr(transformation.Matrix.M42)
    , ToStr(transformation.Matrix.M43)
    , ToStr(transformation.Matrix.M44)
    ));
                var v = Vector3.Transform(new Vector3(1, 2, 3), transformation.Matrix);
                var tstr = string.Format("{0}, {1}, {2}", GetLetter("v",v.X), GetLetter("v",v.Y), GetLetter("v",v.Z));
                sb.AppendLine("        public override  Matrix4 GetMatrix() { return Matrix; }");
                sb.AppendLine("        public override  Quaternion GetQuaternion() { return Quaternion; }");
                sb.AppendLine("        public override Vector3 Transform(Vector3 v) { return new Vector3("+tstr+");}");
                sb.AppendLine("        public override void Transform(ref Vector3 v, out Vector3 res) { res = new Vector3(" + tstr + "); }");
                sb.AppendLine("        public override Vector4 Transform(Vector4 v) { return new Vector4(" + tstr + ", v.W); }");
                sb.AppendLine("        public override void Transform(ref Vector4 v, out Vector4 res) { res = new Vector4(" + tstr + ", v.W); }");
                sb.AppendLine("        public override string ToString() {return \"" + transformation.Name.Replace("Neg","-") + "\";}");
                sb.AppendLine("    }");
                sb.AppendLine("}");
                File.WriteAllText(Path.Combine(dir,"Transformation"+transformation.Name+".cs"), sb.ToString(), new UTF8Encoding(false));
            }
        }
        static string[] _lll = new string[] { "", "X", "Y", "Z" };
        public static string GetLetter(string p, float v)
        {
            string s = "";
            if (v < 0)
            {
                s = "-";
                v = -v;
            }
            return s+(p +"."+ _lll[(int) v]);
        }
        public static string ToStr(float v)
        {
            if (System.Math.Abs(v - (0)) < 1e-5)
            {
                return "0";
            }
            if (System.Math.Abs(v - (1)) < 1e-5)
            {
                return "1";
            }
            if (System.Math.Abs(v - (-1)) < 1e-5)
            {
                return "-1";
            }
            if (System.Math.Abs(v - (0.5)) < 1e-5)
            {
                return "0.5";
            }
            if (System.Math.Abs(v - (-0.5)) < 1e-5)
            {
                return "-0.5";
            }
            if (System.Math.Abs(v - (0.70710678118654752440084436210485)) < 1e-5)
            {
                return "0.70710678118654752440084436210485";
            }
            if (System.Math.Abs(v - (-0.70710678118654752440084436210485)) < 1e-5)
            {
                return "-0.70710678118654752440084436210485";
            }
            
            return v.ToString("N10");
        }
	}
}
