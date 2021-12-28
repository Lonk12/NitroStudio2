using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO.IO {

    /// <summary>
    /// File header.
    /// </summary>
    public abstract class FileHeader : IReadable, IWriteable {

        /// <summary>
        /// Magic.
        /// </summary>
        public string Magic;

        /// <summary>
        /// Byte order.
        /// </summary>
        public ByteOrder ByteOrder;

        /// <summary>
        /// Version.
        /// </summary>
        public Version Version;

        /// <summary>
        /// Block types.
        /// </summary>
        public long[] BlockTypes;

        /// <summary>
        /// Block offsets.
        /// </summary>
        public long[] BlockOffsets;

        /// <summary>
        /// Block sizes.
        /// </summary>
        public long[] BlockSizes;

        /// <summary>
        /// File size.
        /// </summary>
        public long FileSize;

        /// <summary>
        /// Header size.
        /// </summary>
        public long HeaderSize;

        /// <summary>
        /// Read a header.
        /// </summary>
        /// <param name="r">The reader.</param>
        public abstract void Read(FileReader r);

        /// <summary>
        /// Write a header.
        /// </summary>
        /// <param name="w">The writer.</param>
        public abstract void Write(FileWriter w);

    }

}
