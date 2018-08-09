using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Toe
{
    public static class ExtensionMethods
    {
        #region Constants and Fields

        private static readonly byte[] SkippingBuf = new byte[32];

        #endregion

        #region Public Methods and Operators

        public static T ElementAt<T>(this T[,,] array, Vector3i indices)
        {
            return array[indices.X, indices.Y, indices.Z];
        }

        public static T ElementAtOrDefault<T>(this T[,,] array, Vector3i indices)
        {
            if (indices.X < 0)
                return default(T);
            if (indices.Y < 0)
                return default(T);
            if (indices.Z < 0)
                return default(T);
            if (indices.X >= array.GetLength(0))
                return default(T);
            if (indices.Y >= array.GetLength(1))
                return default(T);
            if (indices.Z >= array.GetLength(2))
                return default(T);
            return array[indices.X, indices.Y, indices.Z];
        }

        public static void Write(this BinaryWriter writer, ref Vector2 value)
        {
            writer.Write(value.X);
            writer.Write(value.Y);
        }

        public static void Write(this BinaryWriter writer, ref Vector3 value)
        {
            writer.Write(value.X);
            writer.Write(value.Y);
            writer.Write(value.Z);
        }

        public static void Write(this BinaryWriter writer, ref Vector4 value)
        {
            writer.Write(value.X);
            writer.Write(value.Y);
            writer.Write(value.Z);
            writer.Write(value.W);
        }

        public static void Write(this BinaryWriter writer, Vector2 value)
        {
            writer.Write(value.X);
            writer.Write(value.Y);
        }

        public static void Write(this BinaryWriter writer, Vector3 value)
        {
            writer.Write(value.X);
            writer.Write(value.Y);
            writer.Write(value.Z);
        }

        public static void Write(this BinaryWriter writer, Vector4 value)
        {
            writer.Write(value.X);
            writer.Write(value.Y);
            writer.Write(value.Z);
            writer.Write(value.W);
        }

        public static void Write(this BinaryWriter writer, Quaternion value)
        {
            writer.Write(value.X);
            writer.Write(value.Y);
            writer.Write(value.Z);
            writer.Write(value.W);
        }

        public static void Write(this BinaryWriter writer, Matrix4x3 value)
        {
            writer.Write(value.M11);
            writer.Write(value.M12);
            writer.Write(value.M13);

            writer.Write(value.M21);
            writer.Write(value.M22);
            writer.Write(value.M23);

            writer.Write(value.M31);
            writer.Write(value.M32);
            writer.Write(value.M33);

            writer.Write(value.M41);
            writer.Write(value.M42);
            writer.Write(value.M43);
        }
        public static Vector2 ReadVector2(this BinaryReader reader)
        {
            var x = reader.ReadSingle();
            var y = reader.ReadSingle();
            return new Vector2(x, y);
        }

        public static Vector3 ReadVector3(this BinaryReader reader)
        {
            var x = reader.ReadSingle();
            var y = reader.ReadSingle();
            var z = reader.ReadSingle();
            return new Vector3(x, y, z);
        }
        public static Vector3i ReadVector3i(this BinaryReader reader)
        {
            var x = reader.ReadInt32();
            var y = reader.ReadInt32();
            var z = reader.ReadInt32();
            return new Vector3i(x, y, z);
        }
        public static Vector4 ReadVector4(this BinaryReader reader)
        {
            var x = reader.ReadSingle();
            var y = reader.ReadSingle();
            var z = reader.ReadSingle();
            var w = reader.ReadSingle();
            return new Vector4(x, y, z, w);
        }

        public static Quaternion ReadQuaternion(this BinaryReader reader)
        {
            var x = reader.ReadSingle();
            var y = reader.ReadSingle();
            var z = reader.ReadSingle();
            var w = reader.ReadSingle();
            return new Quaternion(x, y, z, w);
        }

        public static string ReadStringSZ(this BinaryReader reader)
        {
            return reader.ReadStringSZ(Encoding.UTF8);
        }

        public static string ReadStringSZ(this BinaryReader reader, Encoding encoding)
        {
            var list = new List<byte>(16);
            for (;;)
            {
                var next = reader.Read();
                if (next < 0 || next == 0)
                    return encoding.GetString(list.ToArray(), 0, list.Count);
                list.Add((byte) next);
            }
        }

        public static string ReadFixedStringSZ(this BinaryReader reader, int size)
        {
            var data = reader.ReadBytes(size);
            int len = 0;
            var sb = new StringBuilder();
            while (len < size && data[len] != 0)
            {
                sb.Append((char) data[len]);
                ++len;
            }
            return sb.ToString();
        }

        public static string ReadFixedStringSZ(this BinaryReader reader, Encoding encoding, int size)
        {
            var data = reader.ReadBytes(size);
            int len = 0;
            while (len < size && data[len] != 0) ++len;
            return encoding.GetString(data, 0, len);
        }

        public static void WriteAllText(this BinaryWriter writer, string text)
        {
            writer.WriteAllText(text, Encoding.UTF8);
        }

        public static void WriteAllText(this BinaryWriter writer, string text, Encoding encoding)
        {
            writer.Write(encoding.GetBytes(text));
        }

        public static void WriteStringSZ(this BinaryWriter writer, string text)
        {
            writer.WriteStringSZ(text, Encoding.UTF8);
        }

        public static void WriteStringSZ(this BinaryWriter writer, string text, Encoding encoding)
        {
            writer.Write(encoding.GetBytes(text));
            writer.Write((byte) 0);
        }

        public static Vector3[] ReadVector3Array(this BinaryReader reader, int size)
        {
            var res = new Vector3[size];
            for (var index = 0; index < res.Length; index++)
            {
                res[index] = reader.ReadVector3();
            }
            return res;
        }
        public static Vector2[] ReadVector2Array(this BinaryReader reader, int size)
        {
            var res = new Vector2[size];
            for (var index = 0; index < res.Length; index++)
            {
                res[index] = reader.ReadVector2();
            }
            return res;
        }
        public static Vector4[] ReadVector4Array(this BinaryReader reader, int size)
        {
            var res = new Vector4[size];
            for (var index = 0; index < res.Length; index++)
            {
                res[index] = reader.ReadVector4();
            }
            return res;
        }

        public static float[] ReadSingleArray(this BinaryReader reader, int size)
        {
            var res = new float[size];
            for (var index = 0; index < res.Length; index++)
            {
                res[index] = reader.ReadSingle();
            }
            return res;
        }

        public static ushort[] ReadUInt16Array(this BinaryReader reader, int size)
        {
            var res = new ushort[size];
            for (var index = 0; index < res.Length; index++)
            {
                res[index] = reader.ReadUInt16();
            }
            return res;
        }

        public static uint[] ReadUInt32Array(this BinaryReader reader, int size)
        {
            var res = new uint[size];
            for (var index = 0; index < res.Length; index++)
            {
                res[index] = reader.ReadUInt32();
            }
            return res;
        }

        public static int[] ReadInt32Array(this BinaryReader reader, int size)
        {
            var res = new int[size];
            for (var index = 0; index < res.Length; index++)
            {
                res[index] = reader.ReadInt32();
            }
            return res;
        }

        public static void SkipBytes(this BinaryReader reader, int bytes)
        {
            while (bytes > 0)
            {
                var stepSize = Math.Min(SkippingBuf.Length, bytes);
                if (reader.Read(SkippingBuf, 0, stepSize) != stepSize)
                {
                    throw new FormatException();
                }
                bytes -= stepSize;
            }
        }

        #endregion
    }
}