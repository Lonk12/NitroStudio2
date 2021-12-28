using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO.IO {

    /// <summary>
    /// Represents the set of formats of binary date and time encodings. From Syroot.BinaryData.
    /// </summary>
    public enum DateTimeDataFormat {
        /// <summary>
        /// The <see cref="T:System.DateTime" /> is stored as the ticks of a .NET <see cref="T:System.DateTime" /> instance.
        /// </summary>
        NetTicks,
        /// <summary>
        /// The <see cref="T:System.DateTime" /> has the 32-bit time_t format of the C library.
        /// This is a <see cref="T:System.UInt32" /> which can store the seconds from 1970-01-01 until approx. 2106-02-07.
        /// </summary>
        CTime,
        /// <summary>
        /// The <see cref="T:System.DateTime" /> has the 64-bit time_t format of the C library.
        /// This is an <see cref="T:System.Int64" /> which can store the seconds from 1970-01-01 until approx.
        /// 292277026596-12-04.
        /// </summary>
        CTime64,
    }

}
