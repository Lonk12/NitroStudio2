using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO.IO {

    /// <summary>
    /// File reader.
    /// </summary>
    public class FileReader : BinaryReader {

        /// <summary>
        /// The start of the file.
        /// </summary>
        public long FileOffset;

        /// <summary>
        /// Current offset position.
        /// </summary>
        public long CurrentOffset;

        /// <summary>
        /// Structure offsets.
        /// </summary>
        public Stack<long> StructureOffsets = new Stack<long>();

        /// <summary>
        /// Block offsets.
        /// </summary>
        public long[] BlockOffsets;

        /// <summary>
        /// Block sizes.
        /// </summary>
        public long[] BlockSizes;

        /// <summary>
        /// Offsets.
        /// </summary>
        public Dictionary<string, uint> Offsets = new Dictionary<string, uint>();

        /// <summary>
        /// References.
        /// </summary>
        public Dictionary<string, Reference<object>> References = new Dictionary<string, Reference<object>>();

        //Constructors.
        #region Constructors

        /// <summary>
        /// A new file reader.
        /// </summary>
        /// <param name="input">The base stream.</param>
        public FileReader(Stream input) : base(input) {
            ByteOrder = ByteOrder.LittleEndian;
        }

        #endregion

        //Binary data reader. From Syroot.BinaryData.
        #region BinaryDataReader

        /// <summary>
        /// Gets or sets the <see cref="P:Syroot.BinaryData.BinaryDataReader.ByteConverter" /> instance used to parse multibyte binary data with.
        /// </summary>
        public ByteConverter ByteConverter { get; set; }

        /// <summary>
        /// Gets or sets the byte order used to parse multibyte binary data with.
        /// </summary>
        public ByteOrder ByteOrder {
            get {
                return this.ByteConverter.ByteOrder;
            }
            set {
                this.ByteConverter = ByteConverter.GetConverter(value);
            }
        }

        /// <summary>
        /// Gets the encoding used for string related operations where no other encoding has been provided. Due to the
        /// way the underlying <see cref="T:System.IO.BinaryReader" /> is instantiated, it can only be specified at creation time.
        /// </summary>
        public Encoding Encoding { get; }

        /// <summary>
        /// Gets a value indicating whether the end of the stream has been reached and no more data can be read.
        /// </summary>
        public bool EndOfStream {
            get {
                return this.BaseStream.IsEndOfStream();
            }
        }

        /// <summary>Gets the length of the stream in bytes.</summary>
        public long Length {
            get {
                return this.BaseStream.Length;
            }
        }

        /// <summary>
        /// Gets or sets the position within the current stream. This is a shortcut to the base stream Position
        /// property.
        /// </summary>
        public long Position {
            get {
                return this.BaseStream.Position;
            }
            set {
                this.BaseStream.Position = value;
            }
        }

        /// <summary>Aligns the reader to the next given byte multiple.</summary>
        /// <param name="alignment">The byte multiple.</param>
        /// <returns>The new position within the current stream.</returns>
        public long Align(int alignment) {
            return this.BaseStream.Align(alignment, true);
        }

        /// <summary>
        /// Reads a <see cref="T:System.Boolean" /> value from the current stream. The <see cref="T:System.Boolean" /> is available in the
        /// specified binary format.
        /// </summary>
        /// <param name="format">The binary format, in which the <see cref="T:System.Boolean" /> will be read.</param>
        /// <returns>The <see cref="T:System.Boolean" /> read from the current stream.</returns>
        public bool ReadBoolean(BooleanDataFormat format) {
            return this.BaseStream.ReadBoolean(format);
        }

        /// <summary>
        /// Reads the specified number of <see cref="T:System.Boolean" /> values from the current stream into a
        /// <see cref="T:System.Boolean" /> array. The <see cref="T:System.Boolean" /> values are available in the specified binary format.
        /// </summary>
        /// <param name="count">The number of <see cref="T:System.Boolean" /> values to read.</param>
        /// <param name="format">The binary format, in which the <see cref="T:System.Boolean" /> values will be read.</param>
        /// <returns>The <see cref="T:System.Boolean" /> array read from the current stream.</returns>
        public bool[] ReadBooleans(int count, BooleanDataFormat format = BooleanDataFormat.Byte) {
            return this.BaseStream.ReadBooleans(count, format);
        }

        /// <summary>
        /// Reads a <see cref="T:System.DateTime" /> from the current stream. The <see cref="T:System.DateTime" /> is available in the
        /// specified binary format.
        /// </summary>
        /// <param name="format">The binary format, in which the <see cref="T:System.DateTime" /> will be read.</param>
        /// <returns>The <see cref="T:System.DateTime" /> read from the current stream.</returns>
        public DateTime ReadDateTime(DateTimeDataFormat format = DateTimeDataFormat.NetTicks) {
            return this.BaseStream.ReadDateTime(format, this.ByteConverter);
        }

        /// <summary>
        /// Reads the specified number of <see cref="T:System.DateTime" /> values from the current stream into a
        /// <see cref="T:System.DateTime" /> array. The <see cref="T:System.DateTime" /> values are available in the specified binary
        /// format.
        /// </summary>
        /// <param name="count">The number of <see cref="T:System.DateTime" /> values to read.</param>
        /// <param name="format">The binary format, in which the <see cref="T:System.DateTime" /> values will be read.</param>
        /// <returns>The <see cref="T:System.DateTime" /> array read from the current stream.</returns>
        public DateTime[] ReadDateTimes(int count, DateTimeDataFormat format = DateTimeDataFormat.NetTicks) {
            return this.BaseStream.ReadDateTimes(count, format, this.ByteConverter);
        }

        /// <summary>
        /// Reads an 16-byte floating point value from the current stream and advances the current position of the
        /// stream by sixteen bytes.
        /// </summary>
        /// <returns>The 16-byte floating point value read from the current stream.</returns>
        public override Decimal ReadDecimal() {
            return this.BaseStream.ReadDecimal(this.ByteConverter);
        }

        /// <summary>
        /// Reads the specified number of <see cref="T:System.Decimal" /> values from the current stream into a
        /// <see cref="T:System.Decimal" /> array and advances the current position by that number of <see cref="T:System.Decimal" /> values
        /// multiplied with the size of a single value.
        /// </summary>
        /// <param name="count">The number of <see cref="T:System.Decimal" /> values to read.</param>
        /// <returns>The <see cref="T:System.Decimal" /> array read from the current stream.</returns>
        public Decimal[] ReadDecimals(int count) {
            return this.BaseStream.ReadDecimals(count, this.ByteConverter);
        }

        /// <summary>
        /// Reads an 8-byte floating point value from the current stream and advances the current position of the stream
        /// by eight bytes.
        /// </summary>
        /// <returns>The 8-byte floating point value read from the current stream.</returns>
        public override double ReadDouble() {
            return this.BaseStream.ReadDouble(this.ByteConverter);
        }

        /// <summary>
        /// Reads the specified number of <see cref="T:System.Double" /> values from the current stream into a
        /// <see cref="T:System.Double" /> array and advances the current position by that number of <see cref="T:System.Double" /> values
        /// multiplied with the size of a single value.
        /// </summary>
        /// <param name="count">The number of <see cref="T:System.Double" /> values to read.</param>
        /// <returns>The <see cref="T:System.Double" /> array read from the current stream.</returns>
        public double[] ReadDoubles(int count) {
            return this.BaseStream.ReadDoubles(count, this.ByteConverter);
        }

        /// <summary>
        /// Reads the specified enum value from the current stream and advances the current position by the size of the
        /// underlying enum type. Optionally validates the value to be defined in the enum type.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="strict"><c>true</c> to raise an <see cref="T:System.ArgumentOutOfRangeException" /> if the value is not
        /// defined in the enum type.</param>
        /// <returns>The enum value read from the current stream.</returns>
        public T ReadEnum<T>(bool strict = false) where T : struct, IComparable, IFormattable {
            return this.BaseStream.ReadEnum<T>(strict, this.ByteConverter);
        }

        /// <summary>
        /// Reads the specified number of enum values from the current stream into an array of the enum type. Optionally
        /// validates values to be defined in the enum type.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="count">The number of enum values to read.</param>
        /// <param name="strict"><c>true</c> to raise an <see cref="T:System.ArgumentOutOfRangeException" /> if a value is not
        /// defined in the enum type.</param>
        /// <returns>The enum values array read from the current stream.</returns>
        public T[] ReadEnums<T>(int count, bool strict = false) where T : struct, IComparable, IFormattable {
            return this.BaseStream.ReadEnums<T>(count, strict, this.ByteConverter);
        }

        /// <summary>
        /// Reads a 2-byte signed integer from the current stream and advances the current position of the stream by two
        /// bytes.
        /// </summary>
        /// <returns>The 2-byte signed integer read from the current stream.</returns>
        public override short ReadInt16() {
            return this.BaseStream.ReadInt16(this.ByteConverter);
        }

        /// <summary>
        /// Reads the specified number of <see cref="T:System.Int16" /> values from the current stream into a <see cref="T:System.Int16" />
        /// array and advances the current position by that number of <see cref="T:System.Int16" /> values multiplied with the
        /// size of a single value.
        /// </summary>
        /// <param name="count">The number of <see cref="T:System.Int16" /> values to read.</param>
        /// <returns>The <see cref="T:System.Int16" /> array read from the current stream.</returns>
        public short[] ReadInt16s(int count) {
            return this.BaseStream.ReadInt16s(count, this.ByteConverter);
        }

        /// <summary>
        /// Reads a 4-byte signed integer from the current stream and advances the current position of the stream by
        /// four bytes.
        /// </summary>
        /// <returns>The 4-byte signed integer read from the current stream.</returns>
        public override int ReadInt32() {
            return this.BaseStream.ReadInt32(this.ByteConverter);
        }

        /// <summary>
        /// Reads the specified number of <see cref="T:System.Int32" /> values from the current stream into a <see cref="T:System.Int32" />
        /// array and advances the current position by that number of <see cref="T:System.Int32" /> values multiplied with the
        /// size of a single value.
        /// </summary>
        /// <param name="count">The number of <see cref="T:System.Int32" /> values to read.</param>
        /// <returns>The <see cref="T:System.Int32" /> array read from the current stream.</returns>
        public int[] ReadInt32s(int count) {
            return this.BaseStream.ReadInt32s(count, this.ByteConverter);
        }

        /// <summary>
        /// Reads an 8-byte signed integer from the current stream and advances the current position of the stream by
        /// eight bytes.
        /// </summary>
        /// <returns>The 8-byte signed integer read from the current stream.</returns>
        public override long ReadInt64() {
            return this.BaseStream.ReadInt64(this.ByteConverter);
        }

        /// <summary>
        /// Reads the specified number of <see cref="T:System.Int64" /> values from the current stream into a <see cref="T:System.Int64" />
        /// array and advances the current position by that number of <see cref="T:System.Int64" /> values multiplied with the
        /// size of a single value.
        /// </summary>
        /// <param name="count">The number of <see cref="T:System.Int64" /> values to read.</param>
        /// <returns>The <see cref="T:System.Int64" /> array read from the current stream.</returns>
        public long[] ReadInt64s(int count) {
            return this.BaseStream.ReadInt64s(count, this.ByteConverter);
        }

        /// <summary>
        /// Reads the specified number of <see cref="T:System.SByte" /> values from the current stream into a <see cref="T:System.SByte" />
        /// array and advances the current position by that number of <see cref="T:System.SByte" /> values multiplied with the
        /// size of a single value.
        /// </summary>
        /// <param name="count">The number of <see cref="T:System.SByte" /> values to read.</param>
        /// <returns>The <see cref="T:System.SByte" /> array read from the current stream.</returns>
        public sbyte[] ReadSBytes(int count) {
            return this.BaseStream.ReadSBytes(count);
        }

        /// <summary>
        /// Reads a 4-byte floating point value from the current stream and advances the current position of the stream
        /// by four bytes.
        /// </summary>
        /// <returns>The 4-byte floating point value read from the current stream.</returns>
        public override float ReadSingle() {
            return this.BaseStream.ReadSingle(this.ByteConverter);
        }

        /// <summary>
        /// Reads the specified number of <see cref="T:System.Single" /> values from the current stream into a
        /// <see cref="T:System.Single" /> array and advances the current position by that number of <see cref="T:System.Single" /> values
        /// multiplied with the size of a single value.
        /// </summary>
        /// <param name="count">The number of <see cref="T:System.Single" /> values to read.</param>
        /// <returns>The <see cref="T:System.Single" /> array read from the current stream.</returns>
        public float[] ReadSingles(int count) {
            return this.BaseStream.ReadSingles(count, this.ByteConverter);
        }

        /// <summary>
        /// Reads a string from the current stream. The string is available in the specified binary format and encoding.
        /// </summary>
        /// <param name="format">The binary format, in which the string will be read.</param>
        /// <param name="encoding">The encoding used for converting the string or <c>null</c> to use the encoding
        /// configured for this instance.</param>
        /// <returns>The string read from the current stream.</returns>
        public string ReadString(StringDataFormat format, Encoding encoding = null) {
            return this.BaseStream.ReadString(format, encoding ?? this.Encoding, this.ByteConverter);
        }

        /// <summary>
        /// Reads a string from the current stream. The string has neither a prefix or postfix, the length has to be
        /// specified manually. The string is available in the specified encoding.
        /// </summary>
        /// <param name="length">The length of the string.</param>
        /// <param name="encoding">The encoding used for converting the string or <c>null</c> to use the encoding
        /// configured for this instance.</param>
        /// <returns>The <see cref="T:System.String" /> read from the current stream.</returns>
        public string ReadString(int length, Encoding encoding = null) {
            return this.BaseStream.ReadString(length, encoding ?? this.Encoding);
        }

        /// <summary>
        /// Reads the specified number of <see cref="T:System.String" /> values from the current stream into a
        /// <see cref="T:System.String" /> array.
        /// </summary>
        /// <param name="count">The number of <see cref="T:System.String" /> values to read.</param>
        /// <returns>The <see cref="T:System.String" /> array read from the current stream.</returns>
        public string[] ReadStrings(int count) {
            return this.BaseStream.ReadStrings(count, StringDataFormat.DynamicByteCount, (Encoding)null, (ByteConverter)null);
        }

        /// <summary>
        /// Reads the specified number of <see cref="T:System.String" /> values from the current stream into a
        /// <see cref="T:System.String" /> array. The strings are available in the specified binary format and encoding.
        /// </summary>
        /// <param name="count">The number of <see cref="T:System.String" /> values to read.</param>
        /// <param name="format">The binary format, in which the string will be read.</param>
        /// <param name="encoding">The encoding used for converting the string or <c>null</c> to use the encoding
        /// configured for this instance.</param>
        /// <returns>The <see cref="T:System.String" /> array read from the current stream.</returns>
        public string[] ReadStrings(int count, StringDataFormat format, Encoding encoding = null) {
            return this.BaseStream.ReadStrings(count, format, encoding ?? this.Encoding, this.ByteConverter);
        }

        /// <summary>
        /// Reads the specified number of <see cref="T:System.String" /> values from the current stream into a
        /// <see cref="T:System.String" /> array. The strings have neither a prefix or postfix, the length has to be specified
        /// manually. The strings are available in the specified encoding.
        /// </summary>
        /// <param name="count">The number of <see cref="T:System.String" /> values to read.</param>
        /// <param name="length">The length of the string.</param>
        /// <param name="encoding">The encoding used for converting the string or <c>null</c> to use the encoding
        /// configured for this instance.</param>
        /// <returns>The <see cref="T:System.String" /> array read from the current stream.</returns>
        public string[] ReadStrings(int count, int length, Encoding encoding = null) {
            return this.BaseStream.ReadStrings(count, length, encoding ?? this.Encoding);
        }

        /// <summary>
        /// Reads a 2-byte unsigned integer from the current stream using little-endian encoding and advances the
        /// position of the stream by two bytes.
        /// </summary>
        /// <returns>The 2-byte unsigned integer read from the current stream.</returns>
        public override ushort ReadUInt16() {
            return this.BaseStream.ReadUInt16(this.ByteConverter);
        }

        /// <summary>
        /// Reads the specified number of <see cref="T:System.UInt16" /> values from the current stream into a
        /// <see cref="T:System.UInt16" /> array and advances the current position by that number of <see cref="T:System.UInt16" /> values
        /// multiplied with the size of a single value.
        /// </summary>
        /// <param name="count">The number of <see cref="T:System.UInt16" /> values to read.</param>
        /// <returns>The <see cref="T:System.UInt16" /> array read from the current stream.</returns>
        public ushort[] ReadUInt16s(int count) {
            return this.BaseStream.ReadUInt16s(count, this.ByteConverter);
        }

        /// <summary>
        /// Reads an 8-byte unsigned integer from the current stream and advances the position of the stream by eight
        /// bytes.
        /// </summary>
        /// <returns>The 8-byte unsigned integer read from the current stream.</returns>
        public override uint ReadUInt32() {
            return this.BaseStream.ReadUInt32(this.ByteConverter);
        }

        /// <summary>
        /// Reads the specified number of <see cref="T:System.UInt32" /> values from the current stream into a
        /// <see cref="T:System.UInt32" /> array and advances the current position by that number of <see cref="T:System.UInt32" /> values
        /// multiplied with the size of a single value.
        /// </summary>
        /// <param name="count">The number of <see cref="T:System.UInt32" /> values to read.</param>
        /// <returns>The <see cref="T:System.UInt32" /> array read from the current stream.</returns>
        public uint[] ReadUInt32s(int count) {
            return this.BaseStream.ReadUInt32s(count, this.ByteConverter);
        }

        /// <summary>
        /// Reads an 8-byte unsigned integer from the current stream and advances the position of the stream by eight
        /// bytes.
        /// </summary>
        /// <returns>The 8-byte unsigned integer read from the current stream.</returns>
        public override ulong ReadUInt64() {
            return this.BaseStream.ReadUInt64(this.ByteConverter);
        }

        /// <summary>
        /// Reads the specified number of <see cref="T:System.UInt64" /> values from the current stream into a
        /// <see cref="T:System.UInt64" /> array and advances the current position by that number of <see cref="T:System.UInt64" /> values
        /// multiplied with the size of a single value.
        /// </summary>
        /// <param name="count">The number of <see cref="T:System.UInt64" /> values to read.</param>
        /// <returns>The <see cref="T:System.UInt64" /> array read from the current stream.</returns>
        public ulong[] ReadUInt64s(int count) {
            return this.BaseStream.ReadUInt64s(count, this.ByteConverter);
        }

        #endregion

        /// <summary>
        /// Read an item.
        /// </summary>
        /// <typeparam name="T">IReadable type to read.</typeparam>
        /// <returns>The item.</returns>
        public T Read<T>() {

            //Invalid type.
            if (!typeof(IReadable).IsAssignableFrom(typeof(T))) {
                throw new Exception("Type \"" + typeof(T).ToString() + "\" does not implement IReadable.");
            }

            //Read.
            T r = (T)Activator.CreateInstance(typeof(T));
            (r as IReadable).Read(this);
            return r;

        }

        /// <summary>
        /// Read a null terminated string.
        /// </summary>
        /// <returns>The string.</returns>
        public string ReadNullTerminated() {
            string s = "";
            char c = ReadChar();
            while (c != 0) {
                s += c;
                c = ReadChar();
            }
            return s;
        }

        /// <summary>
        /// Open a file.
        /// </summary>
        /// <param name="fileHeader">The output file header.</param>
        /// <param name="setOffset">Whether or not to set the current offset.</param>
        public void OpenFile<T>(out FileHeader fileHeader, bool setOffset = true) where T : FileHeader {
            FileOffset = Position;
            if (setOffset) CurrentOffset = Position;
            fileHeader = Read<T>();
            BlockOffsets = fileHeader.BlockOffsets;
            BlockSizes = fileHeader.BlockSizes;
        }

        /// <summary>
        /// Open a file.
        /// </summary>
        /// <param name="setOffset">If to set the offset.</param>
        public void OpenFile(bool setOffset = true) {
            FileOffset = Position;
            if (setOffset) CurrentOffset = Position;
        }

        /// <summary>
        /// Open a block.
        /// </summary>
        /// <param name="blockNum">The block number.</param>
        /// <param name="magic">Block magic.</param>
        /// <param name="size">Block size.</param>
        /// <param name="readMagicAndSize">Whether or not to read the block magic and size.</param>
        /// <param name="setOffset">Whether or not to set the current offset.</param>
        public void OpenBlock(int blockNum, out string magic, out uint size, bool readMagicAndSize = true, bool setOffset = true) {
            Position = FileOffset + BlockOffsets[blockNum];
            if (setOffset) { CurrentOffset = Position; }
            magic = "";
            size = 0;
            if (readMagicAndSize) {
                magic = new string(ReadChars(4));
                size = ReadUInt32();
            }
            CurrentOffset = Position;
        }

        /// <summary>
        /// Read a sound file.
        /// </summary>
        /// <typeparam name="T">Soundfile type.</typeparam>
        /// <returns>The file.</returns>
        public T ReadFile<T>() where T : IOFile {
            FileReader r = new FileReader(BaseStream);
            r.Position = Position;
            r.CurrentOffset = r.Position;
            r.FileOffset = r.Position;
            T f = r.Read<T>();
            Position = r.Position;
            return f;
        }

        /// <summary>
        /// Start a structure.
        /// </summary>
        public void StartStructure() {
            StructureOffsets.Push(CurrentOffset);
            CurrentOffset = Position;
        }

        /// <summary>
        /// End a structure.
        /// </summary>
        public void EndStructure() {
            CurrentOffset = StructureOffsets.Pop();
        }

        /// <summary>
        /// Jump to an offset.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="absolute">If the offset is absolute.</param>
        public void Jump(long offset, bool absolute = false) {
            if (absolute) {
                Position = FileOffset + offset;
            } else {
                Position = CurrentOffset + offset;
            }
        }

        /// <summary>
        /// Open an offset.
        /// </summary>
        /// <param name="name">Offset name.</param>
        public void OpenOffset(string name) {
            Offsets.Add(name, ReadUInt32());
        }

        /// <summary>
        /// Jump to an offset.
        /// </summary>
        /// <param name="name">Offset name.</param>
        /// <param name="remove">Remove the offset after jumping.</param>
        /// <param name="absolute">If the offset is absolute.</param>
        public void JumpToOffset(string name, bool remove = true, bool absolute = false) {
            if (absolute) {
                Position = FileOffset + Offsets[name];
            } else {
                Position = CurrentOffset + Offsets[name];
            }
            if (remove) { CloseOffset(name); }
        }

        /// <summary>
        /// If an offset is null.
        /// </summary>
        /// <param name="name">The offset.</param>
        /// <returns>If the offset is null.</returns>
        public bool OffsetNull(string name) {
            if (!Offsets[name].Equals(0xFFFFFFFF) && !Offsets[name].Equals(0)) {
                return false;
            }
            Offsets.Remove(name);
            return true;
        }

        /// <summary>
        /// Close an offset.
        /// </summary>
        /// <param name="name">Offset to close.</param>
        public void CloseOffset(string name) {
            Offsets.Remove(name);
        }

        /// <summary>
        /// Open a reference.
        /// </summary>
        /// <param name="name">Name of the reference.</param>
        public void OpenReference<T>(string name) {
            Reference<object> r = Activator.CreateInstance<T>() as Reference<object>;
            r.ReadRef(this);
            References.Add(name, r);
        }

        /// <summary>
        /// Jump to a reference.
        /// </summary>
        /// <param name="name">Name of the reference to jump to.</param>
        /// <param name="remove">If to remove the reference.</param>
        public void JumpToReference(string name, bool remove = true) {
            if (References[name].Absolute) {
                Position = FileOffset + References[name].Offset;
            } else {
                Position = CurrentOffset + References[name].Offset;
            }
            if (remove) { CloseReference(name); }
        }

        /// <summary>
        /// If a reference is null. Removes it if it is.
        /// </summary>
        /// <param name="name">Reference.</param>
        /// <returns>If the reference is null.</returns>
        public bool ReferenceNull(string name) {
            if (!References[name].Offset.Equals(-1) && !References[name].Offset.Equals(0)) {
                return false;
            }
            References.Remove(name);
            return true;
        }

        /// <summary>
        /// Get the data type of a reference.
        /// </summary>
        /// <param name="name">The name of the reference.</param>
        /// <returns>The data type of the reference.</returns>
        public int ReferenceIdentifier(string name) {
            return References[name].Identifier;
        }

        /// <summary>
        /// Close a reference.
        /// </summary>
        /// <param name="name">Reference to close.</param>
        public void CloseReference(string name) {
            References.Remove(name);
        }

        /// <summary>
        /// Read bit flags.
        /// </summary>
        /// <param name="numBytes">Number of bytes.</param>
        /// <param name="maxArraySize">Max array size.</param>
        /// <returns>The bit flags.</returns>
        public bool[] ReadBitFlags(int numBytes, int maxArraySize = 0xFFFF) {

            //Read flags.
            ulong flags = 0;
            switch (numBytes) {
                case 1:
                    flags = ReadByte();
                    break;
                case 2:
                    flags = ReadUInt16();
                    break;
                case 4:
                    flags = ReadUInt32();
                    break;
                case 8:
                    flags = ReadUInt64();
                    break;
            }

            //Flags.
            List<bool> b = new List<bool>();
            for (int i = 0; i < Math.Min(numBytes * 8, maxArraySize); i++) {
                b.Add((flags & (ulong)(0b1 << i)) > 0);
            }

            //Return flags.
            return b.ToArray();

        }

        /// <summary>
        /// Read fixed size string.
        /// </summary>
        /// <param name="size">String size.</param>
        /// <returns>The string.</returns>
        public string ReadFixedString(int size) {
            return new string(ReadChars(size).Where(x => x != 0).ToArray());
        }

    }

}
