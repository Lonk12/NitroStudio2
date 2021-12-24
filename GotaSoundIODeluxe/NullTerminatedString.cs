using GotaSoundIO.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO {

    /// <summary>
    /// A null terminated string.
    /// </summary>
    public class NullTerminatedString : IReadable, IWriteable, IComparable<NullTerminatedString> {

        /// <summary>
        /// Actual data.
        /// </summary>
        public string Data;

        /// <summary>
        /// Byte data.
        /// </summary>
        public byte[] ByteData => Encoding.UTF8.GetBytes(Data);

        /// <summary>
        /// Create a null terminated string.
        /// </summary>
        public NullTerminatedString() {}

        /// <summary>
        /// Create a null terminated string.
        /// </summary>
        /// <param name="str">The string data.</param>
        public NullTerminatedString(string str) {
            Data = str;
        }

        /// <summary>
        /// Read data.
        /// </summary>
        /// <param name="r">The reader.</param>
        public void Read(FileReader r) {
            Data = r.ReadNullTerminated();
        }

        /// <summary>
        /// Write data.
        /// </summary>
        /// <param name="w">The writer.</param>
        public void Write(FileWriter w) {
            w.WriteNullTerminated(Data);
        }

        /// <summary>
        /// Get a list of bits from a byte array.
        /// </summary>
        /// <param name="byteData">Byte data.</param>
        /// <returns>A list of bits.</returns>
        public static bool[] GetBits(byte[] byteData) {

            //Bits.
            bool[] bits = new bool[byteData.Length * 8];

            //For each byte.
            for (int i = 0; i < byteData.Length; i++) {
                bits[i * 8 + 7] = (byteData[i] & 0b1) > 0;
                bits[i * 8 + 6] = (byteData[i] & 0b10) > 0;
                bits[i * 8 + 5] = (byteData[i] & 0b100) > 0;
                bits[i * 8 + 4] = (byteData[i] & 0b1000) > 0;
                bits[i * 8 + 3] = (byteData[i] & 0b10000) > 0;
                bits[i * 8 + 2] = (byteData[i] & 0b100000) > 0;
                bits[i * 8 + 1] = (byteData[i] & 0b1000000) > 0;
                bits[i * 8 + 0] = (byteData[i] & 0b10000000) > 0;
            }

            //Return bits.
            return bits;

        }

        /// <summary>
        /// Compare this to another null terminated string.
        /// </summary>
        /// <param name="other">The other string.</param>
        /// <returns>Its rank relative to the other string.</returns>
        public int CompareTo(NullTerminatedString other) {

            //Get bit arrays.
            var bits = GetBits(ByteData);
            var bitsOther = GetBits(other.ByteData);

            //Compare each bit.
            for (int i = 0; i < bits.Length; i++) {

                //Other is too short.
                if (i >= bitsOther.Length) {
                    return 1;
                }

                //Compare.
                if (bits[i] && !bitsOther[i]) {
                    return 1;
                } else if (!bits[i] && bitsOther[i]) {
                    return -1;
                }

            }

            //They are equal.
            if (bits.Length == bitsOther.Length) {
                return 0;
            }

            //Other is greater.
            return -1;

        }

    }

}
