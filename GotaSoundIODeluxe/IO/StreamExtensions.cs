using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO.IO {

    /// <summary>
    /// Represents static extension methods for read and write operations on <see cref="T:System.IO.Stream" /> instances.
    /// </summary>
    public static class StreamExtensions {
        private static readonly DateTime _cTimeBase = new DateTime(1970, 1, 1);
        [ThreadStatic]
        private static byte[] _buffer;
        [ThreadStatic]
        private static char[] _charBuffer;

        private static byte[] Buffer {
            get {
                if (_buffer == null)
                    _buffer = new byte[16];
                return _buffer;
            }
        }

        private static char[] CharBuffer {
            get {
                if (_charBuffer == null)
                    _charBuffer = new char[16];
                return _charBuffer;
            }
        }

        /// <summary>Aligns the reader to the next given byte multiple.</summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="alignment">The byte multiple.</param>
        /// <param name="grow"><c>true</c> to enlarge the stream size to include the final position in case it is larger
        /// than the current stream length.</param>
        /// <returns>The new position within the current stream.</returns>
        public static long Align(this Stream stream, int alignment, bool grow = false) {
            if (alignment <= 0)
                throw new ArgumentOutOfRangeException("Alignment must be bigger than 0.");
            long num = stream.Seek((-stream.Position % (long)alignment + (long)alignment) % (long)alignment, SeekOrigin.Current);
            if (grow && num > stream.Length)
                stream.SetLength(num);
            return num;
        }

        /// <summary>
        /// Gets a value indicating whether the end of the <paramref name="stream" /> has been reached and no more data
        /// can be read.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <returns>A value indicating whether the end of the stream has been reached.</returns>
        public static bool IsEndOfStream(this Stream stream) {
            return stream.Position >= stream.Length;
        }

        private static void ValidateEnumValue(Type enumType, object value) {
            if (!EnumExtensions.IsValid(enumType, value))
                throw new InvalidDataException(string.Format("Read value {0} is not defined in the enum type {1}.", value, (object)enumType));
        }

        /// <summary>
        /// Returns a <see cref="T:System.Boolean" /> instance read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="format">The <see cref="T:Syroot.BinaryData.BooleanDataFormat" /> format in which the data is stored.</param>
        /// <returns>The value read from the current stream.</returns>
        public static bool ReadBoolean(this Stream stream, BooleanDataFormat format = BooleanDataFormat.Byte) {
            switch (format) {
                case BooleanDataFormat.Byte:
                    return (uint)stream.ReadByte() > 0U;
                case BooleanDataFormat.Word:
                    return (uint)stream.ReadInt16((ByteConverter)null) > 0U;
                case BooleanDataFormat.Dword:
                    return (uint)stream.ReadInt32((ByteConverter)null) > 0U;
                default:
                    throw new ArgumentException(string.Format("Invalid {0}.", (object)"BooleanDataFormat"), nameof(format));
            }
        }

        /// <summary>
        /// Returns an array of <see cref="T:System.Boolean" /> instances read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="count">The number of values to read.</param>
        /// <param name="format">The <see cref="T:Syroot.BinaryData.BooleanDataFormat" /> format in which the data is stored.</param>
        /// <returns>The array of values read from the current stream.</returns>
        public static bool[] ReadBooleans(this Stream stream, int count, BooleanDataFormat format = BooleanDataFormat.Byte) {
            bool[] flagArray = new bool[count];
            lock (stream) {
                switch (format) {
                    case BooleanDataFormat.Byte:
                        for (int index = 0; index < count; ++index)
                            flagArray[index] = (uint)stream.ReadByte() > 0U;
                        break;
                    case BooleanDataFormat.Word:
                        for (int index = 0; index < count; ++index)
                            flagArray[index] = (uint)stream.ReadInt16((ByteConverter)null) > 0U;
                        break;
                    case BooleanDataFormat.Dword:
                        for (int index = 0; index < count; ++index)
                            flagArray[index] = (uint)stream.ReadInt32((ByteConverter)null) > 0U;
                        break;
                    default:
                        throw new ArgumentException(string.Format("Invalid {0}.", (object)"BooleanDataFormat"), nameof(format));
                }
            }
            return flagArray;
        }

        /// <summary>
        /// Returns a <see cref="T:System.Byte" /> instance read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <returns>The value read from the current stream.</returns>
        public static byte Read1Byte(this Stream stream) {
            return (byte)stream.ReadByte();
        }

        /// <summary>
        /// Returns an array of <see cref="T:System.Byte" /> instances read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="count">The number of values to read.</param>
        /// <returns>The array of values read from the current stream.</returns>
        public static byte[] ReadBytes(this Stream stream, int count) {
            byte[] buffer = new byte[count];
            stream.Read(buffer, 0, count);
            return buffer;
        }

        /// <summary>
        /// Returns a <see cref="T:System.DateTime" /> instance read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="format">The <see cref="T:Syroot.BinaryData.DateTimeDataFormat" /> format in which the data is stored.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The value read from the current stream.</returns>
        public static DateTime ReadDateTime(
          this Stream stream,
          DateTimeDataFormat format = DateTimeDataFormat.NetTicks,
          ByteConverter converter = null) {
            switch (format) {
                case DateTimeDataFormat.NetTicks:
                    return new DateTime(stream.ReadInt64(converter));
                case DateTimeDataFormat.CTime:
                    return _cTimeBase.AddSeconds((double)stream.ReadUInt32(converter));
                case DateTimeDataFormat.CTime64:
                    return _cTimeBase.AddSeconds((double)stream.ReadInt64(converter));
                default:
                    throw new ArgumentException(string.Format("Invalid {0}.", (object)"DateTimeDataFormat"), nameof(format));
            }
        }

        /// <summary>
        /// Returns an array of <see cref="T:System.DateTime" /> instances read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="count">The number of values to read.</param>
        /// <param name="format">The <see cref="T:Syroot.BinaryData.DateTimeDataFormat" /> format in which the data is stored.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The array of values read from the current stream.</returns>
        public static DateTime[] ReadDateTimes(
          this Stream stream,
          int count,
          DateTimeDataFormat format = DateTimeDataFormat.NetTicks,
          ByteConverter converter = null) {
            DateTime[] dateTimeArray = new DateTime[count];
            lock (stream) {
                switch (format) {
                    case DateTimeDataFormat.NetTicks:
                        for (int index = 0; index < count; ++index)
                            dateTimeArray[index] = new DateTime(stream.ReadInt64(converter));
                        break;
                    case DateTimeDataFormat.CTime:
                        for (int index = 0; index < count; ++index)
                            dateTimeArray[index] = _cTimeBase.AddSeconds((double)stream.ReadUInt32(converter));
                        break;
                    case DateTimeDataFormat.CTime64:
                        for (int index = 0; index < count; ++index)
                            dateTimeArray[index] = _cTimeBase.AddSeconds((double)stream.ReadInt64(converter));
                        break;
                    default:
                        throw new ArgumentException(string.Format("Invalid {0}.", (object)"BooleanDataFormat"), nameof(format));
                }
            }
            return dateTimeArray;
        }

        /// <summary>
        /// Returns a <see cref="T:System.Decimal" /> instance read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The value read from the current stream.</returns>
        public static Decimal ReadDecimal(this Stream stream, ByteConverter converter = null) {
            FillBuffer(stream, 16);
            return (converter ?? ByteConverter.System).ToDecimal(Buffer, 0);
        }

        /// <summary>
        /// Returns an array of <see cref="T:System.Decimal" /> instances read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="count">The number of values to read.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The array of values read from the current stream.</returns>
        public static Decimal[] ReadDecimals(
          this Stream stream,
          int count,
          ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            Decimal[] numArray = new Decimal[count];
            lock (stream) {
                byte[] buffer = Buffer;
                for (int index = 0; index < count; ++index) {
                    FillBuffer(stream, 16);
                    numArray[index] = converter.ToDecimal(buffer, 0);
                }
            }
            return numArray;
        }

        /// <summary>
        /// Returns a <see cref="T:System.Double" /> instance read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The value read from the current stream.</returns>
        public static double ReadDouble(this Stream stream, ByteConverter converter = null) {
            FillBuffer(stream, 8);
            return (converter ?? ByteConverter.System).ToDouble(Buffer, 0);
        }

        /// <summary>
        /// Returns an array of <see cref="T:System.Double" /> instances read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="count">The number of values to read.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The array of values read from the current stream.</returns>
        public static double[] ReadDoubles(this Stream stream, int count, ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            double[] numArray = new double[count];
            lock (stream) {
                byte[] buffer = Buffer;
                for (int index = 0; index < count; ++index) {
                    FillBuffer(stream, 8);
                    numArray[index] = converter.ToDouble(buffer, 0);
                }
            }
            return numArray;
        }

        /// <summary>
        /// Returns an <see cref="T:System.Enum" /> instance of type <typeparamref name="T" /> from the <paramref name="stream" />.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="strict"><c>true</c> to raise an <see cref="T:System.ArgumentOutOfRangeException" /> if a value is not
        /// defined in the enum type.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The value read from the current stream.</returns>
        public static T ReadEnum<T>(this Stream stream, bool strict = false, ByteConverter converter = null) where T : struct, IComparable, IFormattable {
            return (T)ReadEnum(stream, typeof(T), strict, converter);
        }

        /// <summary>
        /// Returns an array of <see cref="T:System.Enum" /> instances of type <typeparamref name="T" /> read from the
        /// <paramref name="stream" />.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="count">The number of values to read.</param>
        /// <param name="strict"><c>true</c> to raise an <see cref="T:System.ArgumentOutOfRangeException" /> if a value is not
        /// defined in the enum type.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The array of values read from the current stream.</returns>
        public static T[] ReadEnums<T>(
          this Stream stream,
          int count,
          bool strict = false,
          ByteConverter converter = null)
          where T : struct, IComparable, IFormattable {
            converter = converter ?? ByteConverter.System;
            T[] objArray = new T[count];
            Type enumType = typeof(T);
            lock (stream) {
                for (int index = 0; index < count; ++index)
                    objArray[index] = (T)ReadEnum(stream, enumType, strict, converter);
            }
            return objArray;
        }

        /// <summary>
        /// Returns an <see cref="T:System.Int16" /> instance read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The value read from the current stream.</returns>
        public static short ReadInt16(this Stream stream, ByteConverter converter = null) {
            FillBuffer(stream, 2);
            return (converter ?? ByteConverter.System).ToInt16(Buffer, 0);
        }

        /// <summary>
        /// Returns an array of <see cref="T:System.Int16" /> instances read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="count">The number of values to read.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The array of values read from the current stream.</returns>
        public static short[] ReadInt16s(this Stream stream, int count, ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            short[] numArray = new short[count];
            lock (stream) {
                byte[] buffer = Buffer;
                for (int index = 0; index < count; ++index) {
                    FillBuffer(stream, 2);
                    numArray[index] = converter.ToInt16(buffer, 0);
                }
            }
            return numArray;
        }

        /// <summary>
        /// Returns an <see cref="T:System.Int32" /> instance read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The value read from the current stream.</returns>
        public static int ReadInt32(this Stream stream, ByteConverter converter = null) {
            FillBuffer(stream, 4);
            return (converter ?? ByteConverter.System).ToInt32(Buffer, 0);
        }

        /// <summary>
        /// Returns an array of <see cref="T:System.Int32" /> instances read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="count">The number of values to read.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The array of values read from the current stream.</returns>
        public static int[] ReadInt32s(this Stream stream, int count, ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            int[] numArray = new int[count];
            lock (stream) {
                byte[] buffer = Buffer;
                for (int index = 0; index < count; ++index) {
                    FillBuffer(stream, 4);
                    numArray[index] = converter.ToInt32(buffer, 0);
                }
            }
            return numArray;
        }

        /// <summary>
        /// Returns an <see cref="T:System.Int64" /> instance read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The value read from the current stream.</returns>
        public static long ReadInt64(this Stream stream, ByteConverter converter = null) {
            FillBuffer(stream, 8);
            return (converter ?? ByteConverter.System).ToInt64(Buffer, 0);
        }

        /// <summary>
        /// Returns an array of <see cref="T:System.Int64" /> instances read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="count">The number of values to read.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The array of values read from the current stream.</returns>
        public static long[] ReadInt64s(this Stream stream, int count, ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            long[] numArray = new long[count];
            lock (stream) {
                byte[] buffer = Buffer;
                for (int index = 0; index < count; ++index) {
                    FillBuffer(stream, 8);
                    numArray[index] = converter.ToInt64(buffer, 0);
                }
            }
            return numArray;
        }

        /// <summary>
        /// Returns an <see cref="T:System.SByte" /> instance read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <returns>The value read from the current stream.</returns>
        public static sbyte ReadSByte(this Stream stream) {
            return (sbyte)stream.ReadByte();
        }

        /// <summary>
        /// Returns an array of <see cref="T:System.SByte" /> instances read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="count">The number of values to read.</param>
        /// <returns>The array of values read from the current stream.</returns>
        public static sbyte[] ReadSBytes(this Stream stream, int count) {
            sbyte[] numArray = new sbyte[count];
            lock (stream) {
                byte[] buffer = Buffer;
                for (int index = 0; index < count; ++index) {
                    FillBuffer(stream, 1);
                    numArray[index] = (sbyte)buffer[0];
                }
            }
            return numArray;
        }

        /// <summary>
        /// Returns a <see cref="T:System.Single" /> instance read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The value read from the current stream.</returns>
        public static float ReadSingle(this Stream stream, ByteConverter converter = null) {
            FillBuffer(stream, 4);
            return (converter ?? ByteConverter.System).ToSingle(Buffer, 0);
        }

        /// <summary>
        /// Returns an array of <see cref="T:System.Single" /> instances read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="count">The number of values to read.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The array of values read from the current stream.</returns>
        public static float[] ReadSingles(this Stream stream, int count, ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            float[] numArray = new float[count];
            lock (stream) {
                byte[] buffer = Buffer;
                for (int index = 0; index < count; ++index) {
                    FillBuffer(stream, 4);
                    numArray[index] = converter.ToSingle(buffer, 0);
                }
            }
            return numArray;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String" /> instance read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="format">The <see cref="T:Syroot.BinaryData.StringDataFormat" /> format determining how the length of the string is
        /// stored.</param>
        /// <param name="encoding">The <see cref="T:System.Text.Encoding" /> to parse the bytes with, or <c>null</c> to use
        /// <see cref="P:System.Text.Encoding.UTF8" />.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The value read from the current stream.</returns>
        public static string ReadString(
          this Stream stream,
          StringDataFormat format = StringDataFormat.DynamicByteCount,
          Encoding encoding = null,
          ByteConverter converter = null) {
            encoding = encoding ?? Encoding.UTF8;
            converter = converter ?? ByteConverter.System;
            switch (format) {
                case StringDataFormat.DynamicByteCount:
                    return ReadStringWithLength(stream, Read7BitEncodedInt32(stream), false, encoding);
                case StringDataFormat.ByteCharCount:
                    return ReadStringWithLength(stream, stream.ReadByte(), true, encoding);
                case StringDataFormat.Int16CharCount:
                    return ReadStringWithLength(stream, (int)stream.ReadInt16(converter), true, encoding);
                case StringDataFormat.Int32CharCount:
                    return ReadStringWithLength(stream, stream.ReadInt32(converter), true, encoding);
                case StringDataFormat.ZeroTerminated:
                    return ReadStringZeroPostfix(stream, encoding);
                default:
                    throw new ArgumentException(string.Format("Invalid {0}.", (object)"StringDataFormat"), nameof(format));
            }
        }

        /// <summary>
        /// Returns an array of <see cref="T:System.String" /> instances read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="count">The number of values to read.</param>
        /// <param name="format">The <see cref="T:Syroot.BinaryData.StringDataFormat" /> format determining how the length of the strings is
        /// stored.</param>
        /// <param name="encoding">The <see cref="T:System.Text.Encoding" /> to parse the bytes with, or <c>null</c> to use
        /// <see cref="P:System.Text.Encoding.UTF8" />.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The array of values read from the current stream.</returns>
        public static string[] ReadStrings(
          this Stream stream,
          int count,
          StringDataFormat format = StringDataFormat.DynamicByteCount,
          Encoding encoding = null,
          ByteConverter converter = null) {
            encoding = encoding ?? Encoding.UTF8;
            converter = converter ?? ByteConverter.System;
            string[] strArray = new string[count];
            lock (stream) {
                switch (format) {
                    case StringDataFormat.DynamicByteCount:
                        for (int index = 0; index < count; ++index)
                            strArray[index] = ReadStringWithLength(stream, Read7BitEncodedInt32(stream), false, encoding);
                        break;
                    case StringDataFormat.ByteCharCount:
                        for (int index = 0; index < count; ++index)
                            strArray[index] = ReadStringWithLength(stream, stream.ReadByte(), true, encoding);
                        break;
                    case StringDataFormat.Int16CharCount:
                        for (int index = 0; index < count; ++index)
                            strArray[index] = ReadStringWithLength(stream, (int)stream.ReadInt16(converter), true, encoding);
                        break;
                    case StringDataFormat.Int32CharCount:
                        for (int index = 0; index < count; ++index)
                            strArray[index] = ReadStringWithLength(stream, stream.ReadInt32(converter), true, encoding);
                        break;
                    case StringDataFormat.ZeroTerminated:
                        for (int index = 0; index < count; ++index)
                            strArray[index] = ReadStringZeroPostfix(stream, encoding);
                        break;
                    default:
                        throw new ArgumentException(string.Format("Invalid {0}.", (object)"StringDataFormat"), nameof(format));
                }
            }
            return strArray;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String" /> instance read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="length">The length of the string.</param>
        /// <param name="encoding">The <see cref="T:System.Text.Encoding" /> to parse the decode the chars with, or <c>null</c> to use
        /// <see cref="P:System.Text.Encoding.UTF8" />.</param>
        /// <returns>The value read from the current stream.</returns>
        public static string ReadString(this Stream stream, int length, Encoding encoding = null) {
            return ReadStringWithLength(stream, length, true, encoding ?? Encoding.UTF8);
        }

        /// <summary>
        /// Returns an array of <see cref="T:System.String" /> instances read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="count">The number of values to read.</param>
        /// <param name="length">The length of the string.</param>
        /// <param name="encoding">The <see cref="T:System.Text.Encoding" /> to parse the bytes with, or <c>null</c> to use
        /// <see cref="P:System.Text.Encoding.UTF8" />.</param>
        /// <returns>The array of values read from the current stream.</returns>
        public static string[] ReadStrings(
          this Stream stream,
          int count,
          int length,
          Encoding encoding = null) {
            encoding = encoding ?? Encoding.UTF8;
            string[] strArray = new string[count];
            lock (stream) {
                for (int index = 0; index < count; ++index)
                    strArray[index] = ReadStringWithLength(stream, length, true, encoding);
            }
            return strArray;
        }

        /// <summary>
        /// Returns a <see cref="T:System.UInt16" /> instance read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The value read from the current stream.</returns>
        public static ushort ReadUInt16(this Stream stream, ByteConverter converter = null) {
            FillBuffer(stream, 2);
            return (converter ?? ByteConverter.System).ToUInt16(Buffer, 0);
        }

        /// <summary>
        /// Returns an array of <see cref="T:System.UInt16" /> instances read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="count">The number of values to read.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The array of values read from the current stream.</returns>
        public static ushort[] ReadUInt16s(this Stream stream, int count, ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            ushort[] numArray = new ushort[count];
            lock (stream) {
                byte[] buffer = Buffer;
                for (int index = 0; index < count; ++index) {
                    FillBuffer(stream, 2);
                    numArray[index] = converter.ToUInt16(buffer, 0);
                }
            }
            return numArray;
        }

        /// <summary>
        /// Returns a <see cref="T:System.UInt32" /> instance read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The value read from the current stream.</returns>
        public static uint ReadUInt32(this Stream stream, ByteConverter converter = null) {
            FillBuffer(stream, 4);
            return (converter ?? ByteConverter.System).ToUInt32(Buffer, 0);
        }

        /// <summary>
        /// Returns an array of <see cref="T:System.UInt32" /> instances read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="count">The number of values to read.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The array of values read from the current stream.</returns>
        public static uint[] ReadUInt32s(this Stream stream, int count, ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            uint[] numArray = new uint[count];
            lock (stream) {
                byte[] buffer = Buffer;
                for (int index = 0; index < count; ++index) {
                    FillBuffer(stream, 4);
                    numArray[index] = converter.ToUInt32(buffer, 0);
                }
            }
            return numArray;
        }

        /// <summary>
        /// Returns a <see cref="T:System.UInt64" /> instance read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The value read from the current stream.</returns>
        public static ulong ReadUInt64(this Stream stream, ByteConverter converter = null) {
            FillBuffer(stream, 8);
            return (converter ?? ByteConverter.System).ToUInt64(Buffer, 0);
        }

        /// <summary>
        /// Returns an array of <see cref="T:System.UInt64" /> instances read from the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="count">The number of values to read.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        /// <returns>The array of values read from the current stream.</returns>
        public static ulong[] ReadUInt64s(this Stream stream, int count, ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            ulong[] numArray = new ulong[count];
            lock (stream) {
                byte[] buffer = Buffer;
                for (int index = 0; index < count; ++index) {
                    FillBuffer(stream, 8);
                    numArray[index] = converter.ToUInt64(buffer, 0);
                }
            }
            return numArray;
        }

        private static void FillBuffer(Stream stream, int length) {
            if (stream.Read(Buffer, 0, length) < length)
                throw new EndOfStreamException(string.Format("Could not read {0} bytes.", (object)length));
        }

        private static int Read7BitEncodedInt32(Stream stream) {
            int num1 = 0;
            for (int index = 0; index < 5; ++index) {
                int num2 = stream.ReadByte();
                if (num2 == -1)
                    throw new EndOfStreamException("Incomplete 7-bit encoded integer.");
                num1 |= (num2 & (int)sbyte.MaxValue) << index * 7;
                if ((num2 & 128) == 0)
                    return num1;
            }
            throw new InvalidDataException("Invalid 7-bit encoded integer.");
        }

        private static object ReadEnum(
          Stream stream,
          Type enumType,
          bool strict,
          ByteConverter converter) {
            converter = converter ?? ByteConverter.System;
            Type underlyingType = Enum.GetUnderlyingType(enumType);
            int length = Marshal.SizeOf(underlyingType);
            FillBuffer(stream, length);
            object obj;
            if (underlyingType == typeof(byte))
                obj = (object)Buffer[0];
            else if (underlyingType == typeof(sbyte))
                obj = (object)(sbyte)Buffer[0];
            else if (underlyingType == typeof(short))
                obj = (object)converter.ToInt16(Buffer, 0);
            else if (underlyingType == typeof(int))
                obj = (object)converter.ToInt32(Buffer, 0);
            else if (underlyingType == typeof(long))
                obj = (object)converter.ToInt64(Buffer, 0);
            else if (underlyingType == typeof(ushort))
                obj = (object)converter.ToUInt16(Buffer, 0);
            else if (underlyingType == typeof(uint)) {
                obj = (object)converter.ToUInt32(Buffer, 0);
            }
            else {
                if (!(underlyingType == typeof(ulong)))
                    throw new NotImplementedException(string.Format("Unsupported enum type {0}.", (object)underlyingType));
                obj = (object)converter.ToUInt64(Buffer, 0);
            }
            if (strict)
                ValidateEnumValue(enumType, obj);
            return obj;
        }

        private static string ReadStringWithLength(
          Stream stream,
          int length,
          bool lengthInChars,
          Encoding encoding) {
            if (length == 0)
                return string.Empty;
            Decoder decoder = encoding.GetDecoder();
            StringBuilder stringBuilder = new StringBuilder(length);
            int num1 = 0;
            lock (stream) {
                byte[] buffer = Buffer;
                char[] charBuffer = CharBuffer;
                do {
                    do {
                        int num2 = 0;
                        int charCount = 0;
                        while (charCount == 0) {
                            int num3 = stream.Read(buffer, num2++, 1);
                            if (num3 == 0)
                                throw new EndOfStreamException("Incomplete string data, missing requested length.");
                            num1 += num3;
                            charCount = decoder.GetCharCount(buffer, 0, num2);
                            if (charCount > 0) {
                                decoder.GetChars(buffer, 0, num2, charBuffer, 0);
                                stringBuilder.Append(charBuffer, 0, charCount);
                            }
                        }
                    }
                    while (lengthInChars && stringBuilder.Length < length);
                    if (lengthInChars)
                        break;
                }
                while (num1 < length);
            }
            return stringBuilder.ToString();
        }

        private static string ReadStringZeroPostfix(Stream stream, Encoding encoding) {
            List<byte> byteList = new List<byte>();
            bool flag = true;
            byte[] buffer = Buffer;
            lock (stream) {
                switch (encoding.GetByteCount("A")) {
                    case 1:
                        while (flag) {
                            FillBuffer(stream, 1);
                            if (flag = buffer[0] > (byte)0)
                                byteList.Add(buffer[0]);
                        }
                        break;
                    case 2:
                        while (flag) {
                            FillBuffer(stream, 2);
                            if (flag = buffer[0] != (byte)0 || buffer[1] > (byte)0) {
                                byteList.Add(buffer[0]);
                                byteList.Add(buffer[1]);
                            }
                        }
                        break;
                    default:
                        throw new NotImplementedException("Unhandled character byte count. Only 1- or 2-byte encodings are support at the moment.");
                }
            }
            return encoding.GetString(byteList.ToArray());
        }

        /// <summary>
        /// Writes a <see cref="T:System.Boolean" /> value to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="format">The <see cref="T:Syroot.BinaryData.BooleanDataFormat" /> format in which the data is stored.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(
          this Stream stream,
          bool value,
          BooleanDataFormat format = BooleanDataFormat.Byte,
          ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            switch (format) {
                case BooleanDataFormat.Byte:
                    stream.WriteByte(value ? (byte)1 : (byte)0);
                    break;
                case BooleanDataFormat.Word:
                    byte[] buffer1 = Buffer;
                    converter.GetBytes(value ? (short)1 : (short)0, buffer1, 0);
                    stream.Write(Buffer, 0, 2);
                    break;
                case BooleanDataFormat.Dword:
                    byte[] buffer2 = Buffer;
                    converter.GetBytes(value ? 1 : 0, buffer2, 0);
                    stream.Write(Buffer, 0, 4);
                    break;
                default:
                    throw new ArgumentException(string.Format("Invalid {0}.", (object)"BooleanDataFormat"), nameof(format));
            }
        }

        /// <summary>
        /// Writes an array of <see cref="T:System.Boolean" /> values to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="values">The values to write.</param>
        /// <param name="format">The <see cref="T:Syroot.BinaryData.BooleanDataFormat" /> format in which the data is stored.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(
          this Stream stream,
          IEnumerable<bool> values,
          BooleanDataFormat format = BooleanDataFormat.Byte,
          ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            lock (stream) {
                switch (format) {
                    case BooleanDataFormat.Byte:
                        using (IEnumerator<bool> enumerator = values.GetEnumerator()) {
                            while (enumerator.MoveNext()) {
                                bool current = enumerator.Current;
                                stream.WriteByte(current ? (byte)1 : (byte)0);
                            }
                            break;
                        }
                    case BooleanDataFormat.Word:
                        byte[] buffer1 = Buffer;
                        using (IEnumerator<bool> enumerator = values.GetEnumerator()) {
                            while (enumerator.MoveNext()) {
                                bool current = enumerator.Current;
                                converter.GetBytes(current ? (short)1 : (short)0, buffer1, 0);
                                stream.Write(Buffer, 0, 2);
                            }
                            break;
                        }
                    case BooleanDataFormat.Dword:
                        byte[] buffer2 = Buffer;
                        using (IEnumerator<bool> enumerator = values.GetEnumerator()) {
                            while (enumerator.MoveNext()) {
                                bool current = enumerator.Current;
                                converter.GetBytes(current ? 1 : 0, buffer2, 0);
                                stream.Write(Buffer, 0, 4);
                            }
                            break;
                        }
                    default:
                        throw new ArgumentException(string.Format("Invalid {0}.", (object)"BooleanDataFormat"), nameof(format));
                }
            }
        }

        /// <summary>
        /// Writes a <see cref="T:System.Byte" /> value to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="value">The value to write.</param>
        public static void Write(this Stream stream, byte value) {
            stream.WriteByte(value);
        }

        /// <summary>
        /// Writes an array of <see cref="T:System.Byte" /> values to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="values">The values to write.</param>
        public static void Write(this Stream stream, IEnumerable<byte> values) {
            lock (stream) {
                foreach (byte num in values)
                    stream.WriteByte(num);
            }
        }

        /// <summary>
        /// Writes a <see cref="T:System.DateTime" /> value to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="format">The <see cref="T:Syroot.BinaryData.DateTimeDataFormat" /> format in which the data is stored.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(
          this Stream stream,
          DateTime value,
          DateTimeDataFormat format = DateTimeDataFormat.NetTicks,
          ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            switch (format) {
                case DateTimeDataFormat.NetTicks:
                    stream.Write(value.Ticks, converter);
                    break;
                case DateTimeDataFormat.CTime:
                    stream.Write((uint)(new DateTime(1970, 1, 1) - value).TotalSeconds, converter);
                    break;
                case DateTimeDataFormat.CTime64:
                    stream.Write((ulong)(new DateTime(1970, 1, 1) - value).TotalSeconds, converter);
                    break;
                default:
                    throw new ArgumentException(string.Format("Invalid {0}.", (object)"DateTimeDataFormat"), nameof(format));
            }
        }

        /// <summary>
        /// Writes an array of <see cref="T:System.DateTime" /> values to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="values">The values to write.</param>
        /// <param name="format">The <see cref="T:Syroot.BinaryData.DateTimeDataFormat" /> format in which the data is stored.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(
          this Stream stream,
          IEnumerable<DateTime> values,
          DateTimeDataFormat format = DateTimeDataFormat.NetTicks,
          ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
        }

        /// <summary>
        /// Writes a <see cref="T:System.Decimal" /> value to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(this Stream stream, Decimal value, ByteConverter converter = null) {
            byte[] buffer = Buffer;
            (converter ?? ByteConverter.System).GetBytes(value, buffer, 0);
            stream.Write(buffer, 0, 16);
        }

        /// <summary>
        /// Writes an array of <see cref="T:System.Decimal" /> values to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="values">The values to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(
          this Stream stream,
          IEnumerable<Decimal> values,
          ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            lock (stream) {
                byte[] buffer = Buffer;
                foreach (Decimal num in values) {
                    converter.GetBytes(num, buffer, 0);
                    stream.Write(buffer, 0, 16);
                }
            }
        }

        /// <summary>
        /// Writes a <see cref="T:System.Double" /> value to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(this Stream stream, double value, ByteConverter converter = null) {
            byte[] buffer = Buffer;
            (converter ?? ByteConverter.System).GetBytes(value, buffer, 0);
            stream.Write(buffer, 0, 8);
        }

        /// <summary>
        /// Writes an array of <see cref="T:System.Double" /> values to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="values">The values to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(
          this Stream stream,
          IEnumerable<double> values,
          ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            lock (stream) {
                byte[] buffer = Buffer;
                foreach (double num in values) {
                    converter.GetBytes(num, buffer, 0);
                    stream.Write(buffer, 0, 8);
                }
            }
        }

        /// <summary>
        /// Writes an <see cref="T:System.Enum" /> value of type <typeparamref name="T" /> to the <paramref name="stream" />.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="strict"><c>true</c> to raise an <see cref="T:System.ArgumentOutOfRangeException" /> if the value is not
        /// defined in the enum type.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void WriteEnum<T>(
          this Stream stream,
          T value,
          bool strict = false,
          ByteConverter converter = null)
          where T : struct, IComparable, IFormattable {
            WriteEnum(stream, typeof(T), (object)value, strict, converter);
        }

        /// <summary>
        /// Writes an array of <see cref="T:System.Enum" /> values of type <typeparamref name="T" /> to the
        /// <paramref name="stream" />.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="values">The values to write.</param>
        /// <param name="strict"><c>true</c> to raise an <see cref="T:System.ArgumentOutOfRangeException" /> if the value is not
        /// defined in the enum type.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void WriteEnums<T>(
          this Stream stream,
          IEnumerable<T> values,
          bool strict = false,
          ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            Type enumType = typeof(T);
            lock (stream) {
                foreach (T obj in values)
                    WriteEnum(stream, enumType, (object)obj, strict, converter);
            }
        }

        /// <summary>
        /// Writes an <see cref="T:System.Int16" /> value to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(this Stream stream, short value, ByteConverter converter = null) {
            byte[] buffer = Buffer;
            (converter ?? ByteConverter.System).GetBytes(value, buffer, 0);
            stream.Write(buffer, 0, 2);
        }

        /// <summary>
        /// Writes an array of <see cref="T:System.Int16" /> values to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="values">The values to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(
          this Stream stream,
          IEnumerable<short> values,
          ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            lock (stream) {
                byte[] buffer = Buffer;
                foreach (short num in values) {
                    converter.GetBytes(num, buffer, 0);
                    stream.Write(buffer, 0, 2);
                }
            }
        }

        /// <summary>
        /// Writes an <see cref="T:System.Int32" /> value to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(this Stream stream, int value, ByteConverter converter = null) {
            byte[] buffer = Buffer;
            (converter ?? ByteConverter.System).GetBytes(value, buffer, 0);
            stream.Write(buffer, 0, 4);
        }

        /// <summary>
        /// Writes an array of <see cref="T:System.Int32" /> values to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="values">The values to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(this Stream stream, IEnumerable<int> values, ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            lock (stream) {
                byte[] buffer = Buffer;
                foreach (int num in values) {
                    converter.GetBytes(num, buffer, 0);
                    stream.Write(buffer, 0, 4);
                }
            }
        }

        /// <summary>
        /// Writes an <see cref="T:System.Int64" /> value to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(this Stream stream, long value, ByteConverter converter = null) {
            byte[] buffer = Buffer;
            (converter ?? ByteConverter.System).GetBytes(value, buffer, 0);
            stream.Write(buffer, 0, 8);
        }

        /// <summary>
        /// Writes an array of <see cref="T:System.Int64" /> values to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="values">The values to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(this Stream stream, IEnumerable<long> values, ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            lock (stream) {
                byte[] buffer = Buffer;
                foreach (long num in values) {
                    converter.GetBytes(num, buffer, 0);
                    stream.Write(buffer, 0, 8);
                }
            }
        }

        /// <summary>
        /// Writes an <see cref="T:System.SByte" /> value to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="value">The value to write.</param>
        public static void Write(this Stream stream, sbyte value) {
            byte[] buffer = Buffer;
            buffer[0] = (byte)value;
            stream.Write(buffer, 0, 1);
        }

        /// <summary>
        /// Writes an array of <see cref="T:System.SByte" /> values to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="values">The values to write.</param>
        public static void Write(this Stream stream, IEnumerable<sbyte> values) {
            lock (stream) {
                byte[] buffer = Buffer;
                foreach (sbyte num in values) {
                    buffer[0] = (byte)num;
                    stream.Write(buffer, 0, 1);
                }
            }
        }

        /// <summary>
        /// Writes a <see cref="T:System.Single" /> value to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(this Stream stream, float value, ByteConverter converter = null) {
            byte[] buffer = Buffer;
            (converter ?? ByteConverter.System).GetBytes(value, buffer, 0);
            stream.Write(buffer, 0, 4);
        }

        /// <summary>
        /// Writes an array of <see cref="T:System.Single" /> values to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="values">The values to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(
          this Stream stream,
          IEnumerable<float> values,
          ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            lock (stream) {
                byte[] buffer = Buffer;
                foreach (float num in values) {
                    converter.GetBytes(num, buffer, 0);
                    stream.Write(buffer, 0, 4);
                }
            }
        }

        /// <summary>
        /// Writes a <see cref="T:System.String" /> value to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="format">The <see cref="T:Syroot.BinaryData.StringDataFormat" /> format determining how the length of the string is
        /// stored.</param>
        /// <param name="encoding">The <see cref="T:System.Text.Encoding" /> to parse the bytes with, or <c>null</c> to use
        /// <see cref="P:System.Text.Encoding.UTF8" />.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(
          this Stream stream,
          string value,
          StringDataFormat format = StringDataFormat.DynamicByteCount,
          Encoding encoding = null,
          ByteConverter converter = null) {
            encoding = encoding ?? Encoding.UTF8;
            converter = converter ?? ByteConverter.System;
            byte[] bytes = encoding.GetBytes(value);
            lock (stream) {
                switch (format) {
                    case StringDataFormat.DynamicByteCount:
                        Write7BitEncodedInt(stream, bytes.Length);
                        stream.Write(bytes, 0, bytes.Length);
                        break;
                    case StringDataFormat.ByteCharCount:
                        stream.WriteByte((byte)value.Length);
                        stream.Write(bytes, 0, bytes.Length);
                        break;
                    case StringDataFormat.Int16CharCount:
                        converter.GetBytes((short)value.Length, Buffer, 0);
                        stream.Write(Buffer, 0, 2);
                        stream.Write(bytes, 0, bytes.Length);
                        break;
                    case StringDataFormat.Int32CharCount:
                        converter.GetBytes(value.Length, Buffer, 0);
                        stream.Write(Buffer, 0, 4);
                        stream.Write(bytes, 0, bytes.Length);
                        break;
                    case StringDataFormat.ZeroTerminated:
                        stream.Write(bytes, 0, bytes.Length);
                        switch (encoding.GetByteCount("A")) {
                            case 1:
                                stream.WriteByte((byte)0);
                                return;
                            case 2:
                                stream.WriteByte((byte)0);
                                stream.WriteByte((byte)0);
                                return;
                            default:
                                return;
                        }
                    case StringDataFormat.Raw:
                        stream.Write(bytes, 0, bytes.Length);
                        break;
                    default:
                        throw new ArgumentException(string.Format("Invalid {0}.", (object)"StringDataFormat"), nameof(format));
                }
            }
        }

        /// <summary>
        /// Writes an array of <see cref="T:System.String" /> values to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="values">The values to write.</param>
        /// <param name="format">The <see cref="T:Syroot.BinaryData.StringDataFormat" /> format determining how the length of the strings is
        /// stored.</param>
        /// <param name="encoding">The <see cref="T:System.Text.Encoding" /> to parse the bytes with, or <c>null</c> to use
        /// <see cref="P:System.Text.Encoding.UTF8" />.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(
          this Stream stream,
          IEnumerable<string> values,
          StringDataFormat format = StringDataFormat.DynamicByteCount,
          Encoding encoding = null,
          ByteConverter converter = null) {
            encoding = encoding ?? Encoding.UTF8;
            converter = converter ?? ByteConverter.System;
            lock (stream) {
                foreach (string str in values)
                    stream.Write(str, format, encoding, converter);
            }
        }

        /// <summary>
        /// Writes an <see cref="T:System.UInt16" /> value to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(this Stream stream, ushort value, ByteConverter converter = null) {
            byte[] buffer = Buffer;
            (converter ?? ByteConverter.System).GetBytes(value, buffer, 0);
            stream.Write(buffer, 0, 2);
        }

        /// <summary>
        /// Writes an array of <see cref="T:System.UInt16" /> values to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="values">The values to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(
          this Stream stream,
          IEnumerable<ushort> values,
          ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            lock (stream) {
                byte[] buffer = Buffer;
                foreach (ushort num in values) {
                    converter.GetBytes(num, buffer, 0);
                    stream.Write(buffer, 0, 2);
                }
            }
        }

        /// <summary>
        /// Writes a <see cref="T:System.UInt32" /> value to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(this Stream stream, uint value, ByteConverter converter = null) {
            byte[] buffer = Buffer;
            (converter ?? ByteConverter.System).GetBytes(value, buffer, 0);
            stream.Write(buffer, 0, 4);
        }

        /// <summary>
        /// Writes an array of <see cref="T:System.UInt32" /> values to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="values">The values to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(this Stream stream, IEnumerable<uint> values, ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            lock (stream) {
                byte[] buffer = Buffer;
                foreach (uint num in values) {
                    converter.GetBytes(num, buffer, 0);
                    stream.Write(buffer, 0, 4);
                }
            }
        }

        /// <summary>
        /// Writes a <see cref="T:System.UInt64" /> value to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(this Stream stream, ulong value, ByteConverter converter = null) {
            byte[] buffer = Buffer;
            (converter ?? ByteConverter.System).GetBytes(value, buffer, 0);
            stream.Write(buffer, 0, 8);
        }

        /// <summary>
        /// Writes an array of <see cref="T:System.UInt64" /> values to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The extended <see cref="T:System.IO.Stream" /> instance.</param>
        /// <param name="values">The values to write.</param>
        /// <param name="converter">The <see cref="T:Syroot.BinaryData.ByteConverter" /> to use for converting multibyte data.</param>
        public static void Write(
          this Stream stream,
          IEnumerable<ulong> values,
          ByteConverter converter = null) {
            converter = converter ?? ByteConverter.System;
            lock (stream) {
                byte[] buffer = Buffer;
                foreach (ulong num in values) {
                    converter.GetBytes(num, buffer, 0);
                    stream.Write(buffer, 0, 8);
                }
            }
        }

        private static void Write7BitEncodedInt(Stream stream, int value) {
            for (; value >= 128; value >>= 7)
                stream.WriteByte((byte)(value | 128));
            stream.WriteByte((byte)value);
        }

        private static void WriteEnum(
          Stream stream,
          Type enumType,
          object value,
          bool strict,
          ByteConverter converter) {
            converter = converter ?? ByteConverter.System;
            Type underlyingType = Enum.GetUnderlyingType(enumType);
            byte[] buffer = Buffer;
            if (underlyingType == typeof(byte))
                Buffer[0] = (byte)value;
            else if (underlyingType == typeof(sbyte))
                Buffer[0] = (byte)(sbyte)value;
            else if (underlyingType == typeof(short))
                converter.GetBytes((short)value, buffer, 0);
            else if (underlyingType == typeof(int))
                converter.GetBytes((int)value, buffer, 0);
            else if (underlyingType == typeof(long))
                converter.GetBytes((long)value, buffer, 0);
            else if (underlyingType == typeof(ushort))
                converter.GetBytes((ushort)value, buffer, 0);
            else if (underlyingType == typeof(uint)) {
                converter.GetBytes((uint)value, buffer, 0);
            }
            else {
                if (!(underlyingType == typeof(ulong)))
                    throw new NotImplementedException(string.Format("Unsupported enum type {0}.", (object)underlyingType));
                converter.GetBytes((ulong)value, buffer, 0);
            }
            if (strict)
                ValidateEnumValue(enumType, value);
            stream.Write(buffer, 0, Marshal.SizeOf(underlyingType));
        }
    }
}
