using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO.IO {

    /// <summary>
    /// Represents a <see cref="T:Syroot.BinaryData.ByteConverter" /> which handles little endianness.
    /// </summary>
    [SecuritySafeCritical]
    public sealed class ByteConverterLittleEndian : ByteConverter {
        /// <summary>
        /// Gets the <see cref="T:Syroot.BinaryData.ByteOrder" /> in which data is stored as converted by this instance.
        /// </summary>
        public override ByteOrder ByteOrder {
            get {
                return ByteOrder.LittleEndian;
            }
        }

        /// <summary>
        /// Stores the specified <see cref="T:System.Double" /> value as bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="buffer">The byte array to store the value in.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        [SecuritySafeCritical]
        public override unsafe void GetBytes(double value, byte[] buffer, int startIndex = 0) {
            ulong num = (ulong)*(long*)&value;
            buffer[startIndex] = (byte)num;
            buffer[startIndex + 1] = (byte)(num >> 8);
            buffer[startIndex + 2] = (byte)(num >> 16);
            buffer[startIndex + 3] = (byte)(num >> 24);
            buffer[startIndex + 4] = (byte)(num >> 32);
            buffer[startIndex + 5] = (byte)(num >> 40);
            buffer[startIndex + 6] = (byte)(num >> 48);
            buffer[startIndex + 7] = (byte)(num >> 56);
        }

        /// <summary>
        /// Stores the specified <see cref="T:System.Int16" /> value as bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="buffer">The byte array to store the value in.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        public override void GetBytes(short value, byte[] buffer, int startIndex = 0) {
            buffer[startIndex] = (byte)value;
            buffer[startIndex + 1] = (byte)((uint)value >> 8);
        }

        /// <summary>
        /// Stores the specified <see cref="T:System.Int32" /> value as bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="buffer">The byte array to store the value in.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        public override void GetBytes(int value, byte[] buffer, int startIndex = 0) {
            buffer[startIndex] = (byte)value;
            buffer[startIndex + 1] = (byte)(value >> 8);
            buffer[startIndex + 2] = (byte)(value >> 16);
            buffer[startIndex + 3] = (byte)(value >> 24);
        }

        /// <summary>
        /// Stores the specified <see cref="T:System.Int64" /> value as bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="buffer">The byte array to store the value in.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        public override void GetBytes(long value, byte[] buffer, int startIndex = 0) {
            buffer[startIndex] = (byte)value;
            buffer[startIndex + 1] = (byte)(value >> 8);
            buffer[startIndex + 2] = (byte)(value >> 16);
            buffer[startIndex + 3] = (byte)(value >> 24);
            buffer[startIndex + 4] = (byte)(value >> 32);
            buffer[startIndex + 5] = (byte)(value >> 40);
            buffer[startIndex + 6] = (byte)(value >> 48);
            buffer[startIndex + 7] = (byte)(value >> 56);
        }

        /// <summary>
        /// Stores the specified <see cref="T:System.Single" /> value as bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="buffer">The byte array to store the value in.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        [SecuritySafeCritical]
        public override unsafe void GetBytes(float value, byte[] buffer, int startIndex = 0) {
            uint num = *(uint*)&value;
            buffer[startIndex] = (byte)num;
            buffer[startIndex + 1] = (byte)(num >> 8);
            buffer[startIndex + 2] = (byte)(num >> 16);
            buffer[startIndex + 3] = (byte)(num >> 24);
        }

        /// <summary>
        /// Stores the specified <see cref="T:System.UInt16" /> value as bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="buffer">The byte array to store the value in.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        public override void GetBytes(ushort value, byte[] buffer, int startIndex = 0) {
            buffer[startIndex] = (byte)value;
            buffer[startIndex + 1] = (byte)((uint)value >> 8);
        }

        /// <summary>
        /// Stores the specified <see cref="T:System.UInt32" /> value as bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="buffer">The byte array to store the value in.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        public override void GetBytes(uint value, byte[] buffer, int startIndex = 0) {
            buffer[startIndex] = (byte)value;
            buffer[startIndex + 1] = (byte)(value >> 8);
            buffer[startIndex + 2] = (byte)(value >> 16);
            buffer[startIndex + 3] = (byte)(value >> 24);
        }

        /// <summary>
        /// Stores the specified <see cref="T:System.UInt64" /> value as bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="buffer">The byte array to store the value in.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        public override void GetBytes(ulong value, byte[] buffer, int startIndex = 0) {
            buffer[startIndex] = (byte)value;
            buffer[startIndex + 1] = (byte)(value >> 8);
            buffer[startIndex + 2] = (byte)(value >> 16);
            buffer[startIndex + 3] = (byte)(value >> 24);
            buffer[startIndex + 4] = (byte)(value >> 32);
            buffer[startIndex + 5] = (byte)(value >> 40);
            buffer[startIndex + 6] = (byte)(value >> 48);
            buffer[startIndex + 7] = (byte)(value >> 56);
        }

        /// <summary>
        /// Returns an <see cref="T:System.Double" /> instance converted from the bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="buffer">The byte array storing the raw data.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        /// <returns>The converted value.</returns>
        [SecuritySafeCritical]
        public override unsafe double ToDouble(byte[] buffer, int startIndex = 0) {
            long i = ((long)buffer[startIndex] | (long)buffer[startIndex + 1] << 8 | (long)buffer[startIndex + 2] << 16 | (long)buffer[startIndex + 3] << 24 | (long)buffer[startIndex + 4] << 32 | (long)buffer[startIndex + 5] << 40 | (long)buffer[startIndex + 6] << 48 | (long)buffer[startIndex + 7] << 56);
            return *(double*)&i;
        }

        /// <summary>
        /// Returns an <see cref="T:System.Int16" /> instance converted from the bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="buffer">The byte array storing the raw data.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        /// <returns>The converted value.</returns>
        public override short ToInt16(byte[] buffer, int startIndex = 0) {
            return (short)((int)buffer[startIndex] | (int)buffer[startIndex + 1] << 8);
        }

        /// <summary>
        /// Returns an <see cref="T:System.Int32" /> instance converted from the bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="buffer">The byte array storing the raw data.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        /// <returns>The converted value.</returns>
        public override int ToInt32(byte[] buffer, int startIndex = 0) {
            return (int)buffer[startIndex] | (int)buffer[startIndex + 1] << 8 | (int)buffer[startIndex + 2] << 16 | (int)buffer[startIndex + 3] << 24;
        }

        /// <summary>
        /// Returns an <see cref="T:System.Int64" /> instance converted from the bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="buffer">The byte array storing the raw data.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        /// <returns>The converted value.</returns>
        public override long ToInt64(byte[] buffer, int startIndex = 0) {
            return (long)buffer[startIndex] | (long)buffer[startIndex + 1] << 8 | (long)buffer[startIndex + 2] << 16 | (long)buffer[startIndex + 3] << 24 | (long)buffer[startIndex + 4] << 32 | (long)buffer[startIndex + 5] << 40 | (long)buffer[startIndex + 6] << 48 | (long)buffer[startIndex + 7] << 56;
        }

        /// <summary>
        /// Returns an <see cref="T:System.Single" /> instance converted from the bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="buffer">The byte array storing the raw data.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        /// <returns>The converted value.</returns>
        [SecuritySafeCritical]
        public override unsafe float ToSingle(byte[] buffer, int startIndex = 0) {
            int i = ((int)buffer[startIndex] | (int)buffer[startIndex + 1] << 8 | (int)buffer[startIndex + 2] << 16 | (int)buffer[startIndex + 3] << 24);
            return *(float*)&i;
        }

        /// <summary>
        /// Returns an <see cref="T:System.UInt16" /> instance converted from the bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="buffer">The byte array storing the raw data.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        /// <returns>The converted value.</returns>
        public override ushort ToUInt16(byte[] buffer, int startIndex = 0) {
            return (ushort)((uint)buffer[startIndex] | (uint)buffer[startIndex + 1] << 8);
        }

        /// <summary>
        /// Returns an <see cref="T:System.UInt32" /> instance converted from the bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="buffer">The byte array storing the raw data.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        /// <returns>The converted value.</returns>
        public override uint ToUInt32(byte[] buffer, int startIndex = 0) {
            return (uint)((int)buffer[startIndex] | (int)buffer[startIndex + 1] << 8 | (int)buffer[startIndex + 2] << 16 | (int)buffer[startIndex + 3] << 24);
        }

        /// <summary>
        /// Returns an <see cref="T:System.UInt64" /> instance converted from the bytes in the given <paramref name="buffer" />.
        /// </summary>
        /// <param name="buffer">The byte array storing the raw data.</param>
        /// <param name="startIndex">The index at which to start writing values into the buffer.</param>
        /// <returns>The converted value.</returns>
        public override ulong ToUInt64(byte[] buffer, int startIndex = 0) {
            return (ulong)((long)buffer[startIndex] | (long)buffer[startIndex + 1] << 8 | (long)buffer[startIndex + 2] << 16 | (long)buffer[startIndex + 3] << 24 | (long)buffer[startIndex + 4] << 32 | (long)buffer[startIndex + 5] << 40 | (long)buffer[startIndex + 6] << 48 | (long)buffer[startIndex + 7] << 56);
        }
    }

}
