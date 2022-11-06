using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Arise.FileSyncer.Serializer
{
    public static class BinaryWriter
    {
        #region Strings
        /// <summary>
        /// Writes a string at the end of the stream using an ushort for the length in UTF-8 encoding.
        /// </summary>
        public static void WriteAFS(this Stream stream, string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);

            if (bytes.Length > ushort.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(data), bytes.Length, "The string UTF-8 size must be smaller than 65535");
            }

            stream.WriteAFS(Convert.ToUInt16(bytes.Length));
            stream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Writes a string array at the end of the stream. Writes the length of it as well.
        /// </summary>
        public static void WriteAFS(this Stream stream, IList<string> data)
        {
            stream.WriteAFS(data.Count);
            for (int i = 0; i < data.Count; i++)
            {
                stream.WriteAFS(data[i]);
            }
        }
        #endregion

        #region Integers
        /// <summary>
        /// Writes a short at the end of the stream using 2 bytes.
        /// </summary>
        public static void WriteAFS(this Stream stream, short data)
        {
            stream.Write(BitConverter.GetBytes(data), 0, sizeof(short));
        }

        /// <summary>
        /// Writes a short array at the end of the stream. Writes the length of it as well.
        /// </summary>
        public static void WriteAFS(this Stream stream, IList<short> data)
        {
            stream.WriteAFS(data.Count);
            for (int i = 0; i < data.Count; i++)
            {
                stream.WriteAFS(data[i]);
            }
        }

        /// <summary>
        /// Writes an int at the end of the stream using 4 bytes.
        /// </summary>
        public static void WriteAFS(this Stream stream, int data)
        {
            stream.Write(BitConverter.GetBytes(data), 0, sizeof(int));
        }

        /// <summary>
        /// Writes a int array at the end of the stream. Writes the length of it as well.
        /// </summary>
        public static void WriteAFS(this Stream stream, IList<int> data)
        {
            stream.WriteAFS(data.Count);
            for (int i = 0; i < data.Count; i++)
            {
                stream.WriteAFS(data[i]);
            }
        }

        /// <summary>
        /// Writes a long at the end of the stream using 8 bytes.
        /// </summary>
        public static void WriteAFS(this Stream stream, long data)
        {
            stream.Write(BitConverter.GetBytes(data), 0, sizeof(long));
        }

        /// <summary>
        /// Writes a long array at the end of the stream. Writes the length of it as well.
        /// </summary>
        public static void WriteAFS(this Stream stream, IList<long> data)
        {
            stream.WriteAFS(data.Count);
            for (int i = 0; i < data.Count; i++)
            {
                stream.WriteAFS(data[i]);
            }
        }

        /// <summary>
        /// Writes an unsigned short at the end of the stream using 2 bytes.
        /// </summary>
        public static void WriteAFS(this Stream stream, ushort data)
        {
            stream.Write(BitConverter.GetBytes(data), 0, sizeof(ushort));
        }

        /// <summary>
        /// Writes an unsigned short array at the end of the stream. Writes the length of it as well.
        /// </summary>
        public static void WriteAFS(this Stream stream, IList<ushort> data)
        {
            stream.WriteAFS(data.Count);
            for (int i = 0; i < data.Count; i++)
            {
                stream.WriteAFS(data[i]);
            }
        }

        /// <summary>
        /// Writes an unsigned int at the end of the stream using 4 bytes.
        /// </summary>
        public static void WriteAFS(this Stream stream, uint data)
        {
            stream.Write(BitConverter.GetBytes(data), 0, sizeof(uint));
        }

        /// <summary>
        /// Writes an unsigned int array at the end of the stream. Writes the length of it as well.
        /// </summary>
        public static void WriteAFS(this Stream stream, IList<uint> data)
        {
            stream.WriteAFS(data.Count);
            for (int i = 0; i < data.Count; i++)
            {
                stream.WriteAFS(data[i]);
            }
        }

        /// <summary>
        /// Writes an unsigned long at the end of the stream using 8 bytes.
        /// </summary>
        public static void WriteAFS(this Stream stream, ulong data)
        {
            stream.Write(BitConverter.GetBytes(data), 0, sizeof(ulong));
        }

        /// <summary>
        /// Writes an unsigned long array at the end of the stream. Writes the length of it as well.
        /// </summary>
        public static void WriteAFS(this Stream stream, IList<ulong> data)
        {
            stream.WriteAFS(data.Count);
            for (int i = 0; i < data.Count; i++)
            {
                stream.WriteAFS(data[i]);
            }
        }
        #endregion

        #region Bytes
        /// <summary>
        /// Writes a bool at the end of the stream using a byte.
        /// </summary>
        public static void WriteAFS(this Stream stream, bool data)
        {
            stream.WriteByte(data ? (byte)1 : (byte)0);
        }

        /// <summary>
        /// Writes a bool array at the end of the stream. Writes the length of it as well.
        /// </summary>
        public static void WriteAFS(this Stream stream, IList<bool> data)
        {
            stream.WriteAFS(data.Count);
            for (int i = 0; i < data.Count; i++)
            {
                stream.WriteAFS(data[i]);
            }
        }

        /// <summary>
        /// Writes a byte at the end of the stream using a byte.
        /// </summary>
        public static void WriteAFS(this Stream stream, byte data)
        {
            stream.WriteByte(data);
        }

        /// <summary>
        /// Writes a byte array at the end of the stream. Writes the length of it as well.
        /// </summary>
        public static void WriteAFS(this Stream stream, byte[] data)
        {
            stream.WriteAFS(data.Length);
            stream.Write(data, 0, data.Length);
        }
        #endregion

        #region IBinarySerializable
        /// <summary>
        /// Writes an IBinarySerializable at the end of the stream using a byte.
        /// </summary>
        public static void WriteAFS(this Stream stream, IBinarySerializable data)
        {
            data.Serialize(stream);
        }

        /// <summary>
        /// Writes an IBinarySerializable array at the end of the stream. Writes the length of it as well.
        /// </summary>
        public static void WriteAFS<T>(this Stream stream, IList<T> data) where T : IBinarySerializable
        {
            stream.WriteAFS(data.Count);
            for (int i = 0; i < data.Count; i++)
            {
                stream.WriteAFS(data[i]);
            }
        }
        #endregion

        #region Miscellaneous
        /// <summary>
        /// Writes a System.DateTime (as UTC-Ticks) at the end of the stream using 8 bytes.
        /// </summary>
        public static void WriteAFS(this Stream stream, DateTime data)
        {
            Debug.Assert(data.Kind == DateTimeKind.Utc);
            stream.WriteAFS(data.Ticks);
        }

        /// <summary>
        /// Writes a System.DateTime array at the end of the stream. Writes the length of it as well.
        /// </summary>
        public static void WriteAFS(this Stream stream, IList<DateTime> data)
        {
            stream.WriteAFS(data.Count);
            for (int i = 0; i < data.Count; i++)
            {
                stream.WriteAFS(data[i]);
            }
        }

        /// <summary>
        /// Writes a Guid at the end of the stream using 16 bytes.
        /// </summary>
        public static void WriteAFS(this Stream stream, Guid data)
        {
            stream.Write(data.ToByteArray(), 0, 16);
        }

        /// <summary>
        /// Writes a Guid array at the end of the stream. Writes the length of it as well.
        /// </summary>
        public static void WriteAFS(this Stream stream, IList<Guid> data)
        {
            stream.WriteAFS(data.Count);
            for (int i = 0; i < data.Count; i++)
            {
                stream.WriteAFS(data[i]);
            }
        }
        #endregion
    }
}
