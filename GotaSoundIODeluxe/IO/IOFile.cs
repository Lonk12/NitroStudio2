using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO.IO {

    /// <summary>
    /// A file.
    /// </summary>
    public abstract class IOFile : IReadable, IWriteable {

        /// <summary>
        /// File version.
        /// </summary>
        public Version Version;

        /// <summary>
        /// Byte order.
        /// </summary>
        public ByteOrder ByteOrder;

        /// <summary>
        /// Create a new sound file.
        /// </summary>
        public IOFile() { }

        /// <summary>
        /// Create a new sound file from a stream.
        /// </summary>
        /// <param name="s">The stream to read from.</param>
        public IOFile(Stream s) { Read(s); }

        /// <summary>
        /// Create a new sound file from a file.
        /// </summary>
        /// <param name="file">The file to read.</param>
        public IOFile(byte[] file) { Read(file); }

        /// <summary>
        /// Create a new sound file from a file path.
        /// </summary>
        /// <param name="filePath">The path to read the file from.</param>
        public IOFile(string filePath) { Read(filePath); }

        /// <summary>
        /// Read a file from a path.
        /// </summary>
        /// <param name="filePath">Path to read the file from.</param>
        public void Read(string filePath) {
            using (var f = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                Read(f);
            }
        }

        /// <summary>
        /// Read a file from a byte array.
        /// </summary>
        /// <param name="file">File to read.</param>
        public void Read(byte[] file) {
            using (var m = new MemoryStream(file)) {
                Read(m);
            }
        }

        /// <summary>
        /// Read a file from a stream.
        /// </summary>
        /// <param name="s">Stream to read file from.</param>
        public void Read(Stream s) {
            FileReader r = new FileReader(s);
            Read(r);
            r.Dispose();
        }

        /// <summary>
        /// Read the file.
        /// </summary>
        /// <param name="r">The reader.</param>
        public abstract void Read(FileReader r);

        /// <summary>
        /// Write the file.
        /// </summary>
        /// <param name="w">The writer.</param>
        public abstract void Write(FileWriter w);

        /// <summary>
        /// Write to a stream.
        /// </summary>
        /// <param name="s">The stream to write to.</param>
        public void Write(Stream s) {
            using (FileWriter w = new FileWriter(s)) {
                Write(w);
            }
        }

        /// <summary>
        /// Write the file to a byte array.
        /// </summary>
        /// <returns>The file as a byte array.</returns>
        public byte[] Write() {
            using (var f = new MemoryStream()) {
                Write(f);
                return f.ToArray();
            }
        }

        /// <summary>
        /// Write the file to a filepath.
        /// </summary>
        /// <param name="filePath">The path to write to.</param>
        public void Write(string filePath) {
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
            using (FileStream f = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
                Write(f);
            }
        }

        /// <summary>
        /// Duplicate this file.
        /// </summary>
        /// <returns>A duplicate of this file.</returns>
        public T Duplicate<T>() where T : IOFile {
            T ret = Activator.CreateInstance<T>();
            ret.Read(Write());
            return ret;
        }

        /// <summary>
        /// Md5Sum hash.
        /// </summary>
        public string Md5Sum {
            get {
                using (var md5 = MD5.Create()) {
                    var hash = md5.ComputeHash(Write());
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

    }

}
