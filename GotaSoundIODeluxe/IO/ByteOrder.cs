using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO.IO {

    /// <summary>
    /// Represents the possible endianness of binary data. From Syroot.BinaryData.
    /// </summary>
    public enum ByteOrder : ushort {
        /// <summary>
        /// Indicates the byte order of the system executing the assembly.
        /// </summary>
        System = 0,
        /// <summary>Indicates big endian byte order.</summary>
        BigEndian = 65279, // 0xFEFF
        /// <summary>Indicates little endian byte order.</summary>
        LittleEndian = 65534, // 0xFFFE
    }

}
