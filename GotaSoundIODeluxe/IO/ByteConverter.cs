using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO.IO {

    /// <summary>
    /// Byte converter. From Syroot.BinaryDat.
    /// </summary>
    public abstract class ByteConverter {

        /// <summary>
        /// The exception thrown if a conversion buffer is too small or <c>null</c>.
        /// </summary>
        protected static readonly Exception BufferException = new Exception("Buffer null or too small.");

        /// <summary>
        /// Gets a <see cref="T:Syroot.BinaryData.ByteConverter" /> instance converting data stored in little endian byte order.
        /// </summary>
        public static ByteConverter LittleEndian { get; } = (ByteConverter)new ByteConverterLittleEndian();

        /// <summary>
        /// Gets a <see cref="T:Syroot.BinaryData.ByteConverter" /> instance converting data stored in big endian byte order.
        /// </summary>
        public static ByteConverter BigEndian { get; } = (ByteConverter)new ByteConverterBigEndian();

        /// <summary>
        /// Gets a <see cref="T:Syroot.BinaryData.ByteConverter" /> instance converting data stored in the byte order of the system
        /// executing the assembly.
        /// </summary>
        public static ByteConverter System { get; } = BitConverter.IsLittleEndian ? LittleEndian : BigEndian;

        /// <summary>
        /// Gets the <see cref="T:Syroot.BinaryData.ByteOrder" /> in which data is stored as converted by this instance.
        /// </summary>
        public abstract ByteOrder ByteOrder { get; }

        /// <summary>
        /// Returns a <see cref="T:Syroot.BinaryData.ByteConverter" /> for the given <paramref name="byteOrder" />.
        /// </summary>
        /// <param name="byteOrder">The <see cref="P:Syroot.BinaryData.ByteConverter.ByteOrder" /> to retrieve a converter for.</param>
        /// <returns>The corresponding <see cref="T:Syroot.BinaryData.ByteConverter" /> instance.</returns>
        public static ByteConverter GetConverter(ByteOrder byteOrder) {
            if (byteOrder == ByteOrder.System)
                return System;
            if (byteOrder == ByteOrder.BigEndian)
                return BigEndian;
            if (byteOrder == ByteOrder.LittleEndian)
                return LittleEndian;
            throw new ArgumentException(string.Format("Invalid {0}.", (object)"ByteOrder"), nameof(byteOrder));
        }

        /// <summary>
        /// Stores the specified <see cref="T:System.Decimal" /> value as bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="buffer">The byte array to store the value in.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        public void GetBytes(Decimal value, byte[] buffer, int startIndex = 0) {
            if (buffer != null && buffer.Length - startIndex < 16)
                throw BufferException;
            int[] bits = Decimal.GetBits(value);
            for (int index1 = 0; index1 < 4; ++index1) {
                int index2 = startIndex + index1 * 4;
                int num = bits[index1];
                buffer[index2] = (byte)num;
                buffer[index2 + 1] = (byte)(num >> 8);
                buffer[index2 + 2] = (byte)(num >> 16);
                buffer[index2 + 3] = (byte)(num >> 24);
            }
        }

        /// <summary>
        /// Stores the specified <see cref="T:System.Double" /> value as bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="buffer">The byte array to store the value in.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        public abstract void GetBytes(double value, byte[] buffer, int startIndex = 0);

        /// <summary>
        /// Stores the specified <see cref="T:System.Int16" /> value as bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="buffer">The byte array to store the value in.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        public abstract void GetBytes(short value, byte[] buffer, int startIndex = 0);

        /// <summary>
        /// Stores the specified <see cref="T:System.Int32" /> value as bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="buffer">The byte array to store the value in.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        public abstract void GetBytes(int value, byte[] buffer, int startIndex = 0);

        /// <summary>
        /// Stores the specified <see cref="T:System.Int64" /> value as bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="buffer">The byte array to store the value in.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        public abstract void GetBytes(long value, byte[] buffer, int startIndex = 0);

        /// <summary>
        /// Stores the specified <see cref="T:System.Single" /> value as bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="buffer">The byte array to store the value in.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        public abstract void GetBytes(float value, byte[] buffer, int startIndex = 0);

        /// <summary>
        /// Stores the specified <see cref="T:System.UInt16" /> value as bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="buffer">The byte array to store the value in.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        public abstract void GetBytes(ushort value, byte[] buffer, int startIndex = 0);

        /// <summary>
        /// Stores the specified <see cref="T:System.UInt32" /> value as bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="buffer">The byte array to store the value in.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        public abstract void GetBytes(uint value, byte[] buffer, int startIndex = 0);

        /// <summary>
        /// Stores the specified <see cref="T:System.UInt64" /> value as bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="buffer">The byte array to store the value in.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        public abstract void GetBytes(ulong value, byte[] buffer, int startIndex = 0);

        /// <summary>
        /// Returns an <see cref="T:System.Decimal" /> instance converted from the bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="buffer">The byte array storing the raw data.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        /// <returns>The converted value.</returns>
        public Decimal ToDecimal(byte[] buffer, int startIndex = 0) {
            if (buffer != null && buffer.Length - startIndex < 16)
                throw BufferException;
            int[] bits = new int[4];
            for (int index1 = 0; index1 < 4; ++index1) {
                int index2 = startIndex + index1 * 4;
                bits[index1] = (int)buffer[index2] | (int)buffer[index2 + 1] << 8 | (int)buffer[index2 + 2] << 16 | (int)buffer[index2 + 3] << 24;
            }
            return new Decimal(bits);
        }

        /// <summary>
        /// Returns an <see cref="T:System.Double" /> instance converted from the bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="buffer">The byte array storing the raw data.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        /// <returns>The converted value.</returns>
        public abstract double ToDouble(byte[] buffer, int startIndex = 0);

        /// <summary>
        /// Returns an <see cref="T:System.Int16" /> instance converted from the bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="buffer">The byte array storing the raw data.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        /// <returns>The converted value.</returns>
        public abstract short ToInt16(byte[] buffer, int startIndex = 0);

        /// <summary>
        /// Returns an <see cref="T:System.Int32" /> instance converted from the bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="buffer">The byte array storing the raw data.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        /// <returns>The converted value.</returns>
        public abstract int ToInt32(byte[] buffer, int startIndex = 0);

        /// <summary>
        /// Returns an <see cref="T:System.Int64" /> instance converted from the bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="buffer">The byte array storing the raw data.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        /// <returns>The converted value.</returns>
        public abstract long ToInt64(byte[] buffer, int startIndex = 0);

        /// <summary>
        /// Returns an <see cref="T:System.Single" /> instance converted from the bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="buffer">The byte array storing the raw data.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        /// <returns>The converted value.</returns>
        public abstract float ToSingle(byte[] buffer, int startIndex = 0);

        /// <summary>
        /// Returns an <see cref="T:System.UInt16" /> instance converted from the bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="buffer">The byte array storing the raw data.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        /// <returns>The converted value.</returns>
        public abstract ushort ToUInt16(byte[] buffer, int startIndex = 0);

        /// <summary>
        /// Returns an <see cref="T:System.UInt32" /> instance converted from the bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="buffer">The byte array storing the raw data.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        /// <returns>The converted value.</returns>
        public abstract uint ToUInt32(byte[] buffer, int startIndex = 0);

        /// <summary>
        /// Returns an <see cref="T:System.UInt64" /> instance converted from the bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="buffer">The byte array storing the raw data.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        /// <returns>The converted value.</returns>
        public abstract ulong ToUInt64(byte[] buffer, int startIndex = 0);

    }

}
