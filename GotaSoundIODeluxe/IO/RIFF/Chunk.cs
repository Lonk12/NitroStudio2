using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO.IO.RIFF {

    /// <summary>
    /// Chunk.
    /// </summary>
    public class Chunk {

        /// <summary>
        /// Magic.
        /// </summary>
        public string Magic;

        /// <summary>
        /// Position.
        /// </summary>
        public long Pos;

        /// <summary>
        /// Size.
        /// </summary>
        public uint Size;

    }

}
