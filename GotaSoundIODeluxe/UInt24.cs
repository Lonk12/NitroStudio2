using GotaSoundIO.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO {

    /// <summary>
    /// Unsigned 24-bit integer.
    /// </summary>
    public struct UInt24 : IReadable, IWriteable {

        /// <summary>
        /// Max value.
        /// </summary>
        public const int MaxValue = 16777215;

        /// <summary>
        /// Min value.
        /// </summary>
        public const int MinValue = 0;

        /// <summary>
        /// Data.
        /// </summary>
        private byte[] Data;

        /// <summary>
        /// Check for null.
        /// </summary>
        private void NullCheck() {
            if (Data == null) {
                Data = new byte[3];
            }
        }

        /// <summary>
        /// Get this as an int.
        /// </summary>
        /// <returns>This as an int.</returns>
        private int GetInt() {
            NullCheck();
            int ret = 0;
            ret |= Data[2];
            ret |= (Data[1] << 8);
            ret |= (Data[0] << 16);
            return ret;
        }

        /// <summary>
        /// Get data from an int.
        /// </summary>
        /// <param name="val">Int to get data from.</param>
        private static UInt24 FromInt(int val) {
            UInt24 ret = new UInt24();
            ret.NullCheck();
            ret.Data[2] = (byte)((val >> 0) & 0xFF);
            ret.Data[1] = (byte)((val >> 8) & 0xFF);
            ret.Data[0] = (byte)((val >> 16) & 0xFF);
            return ret;
        }

        /// <summary>
        /// Get this as a uint.
        /// </summary>
        /// <returns>This as a uint.</returns>
        private uint GetUInt() {
            return (uint)GetInt();
        }

        /// <summary>
        /// Get this as an int.
        /// </summary>
        /// <param name="val">Value.</param>

        public static implicit operator int(UInt24 val) => val.GetInt();

        /// <summary>
        /// Get this as a uint.
        /// </summary>
        /// <param name="val">Value.</param>

        public static implicit operator uint(UInt24 val) => val.GetUInt();

        /// <summary>
        /// Convert from an int.
        /// </summary>
        /// <param name="val">Value.</param>
        public static explicit operator UInt24(int val) => FromInt(val);

        /// <summary>
        /// Convert from a uint.
        /// </summary>
        /// <param name="val">Value.</param>
        public static explicit operator UInt24(uint val) => FromInt((int)val);

        /// <summary>
        /// Convert from a float.
        /// </summary>
        /// <param name="val">Value.</param>
        public static explicit operator UInt24(float val) => FromInt((int)val);

        /// <summary>
        /// Read the data.
        /// </summary>
        /// <param name="r">The reader.</param>
        public void Read(FileReader r) {
            NullCheck();
            switch (r.ByteOrder) {
                case ByteOrder.LittleEndian:
                case ByteOrder.System:
                    Data[2] = r.ReadByte();
                    Data[1] = r.ReadByte();
                    Data[0] = r.ReadByte();
                    break;
                case ByteOrder.BigEndian:
                    Data = r.ReadBytes(3);
                    break;
            }
        }

        /// <summary>
        /// Write the data.
        /// </summary>
        /// <param name="w">The writer.</param>
        public void Write(FileWriter w) {
            NullCheck();
            switch (w.ByteOrder) {
                case ByteOrder.LittleEndian:
                case ByteOrder.System:
                    w.Write(Data[2]);
                    w.Write(Data[1]);
                    w.Write(Data[0]);
                    break;
                case ByteOrder.BigEndian:
                    w.Write(Data);
                    break;
            }
        }

    }

}
