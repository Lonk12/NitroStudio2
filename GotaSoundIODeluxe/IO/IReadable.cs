using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO.IO {

    /// <summary>
    /// A readable item.
    /// </summary>
    public interface IReadable {

        /// <summary>
        /// Read the item.
        /// </summary>
        /// <param name="r">The file reader.</param>
        void Read(FileReader r);

    }

}
