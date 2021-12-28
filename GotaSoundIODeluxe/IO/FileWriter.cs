using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO.IO {

    /// <summary>
    /// File writer.
    /// </summary>
    public class FileWriter : BinaryWriter {

        /// <summary>
        /// Header.
        /// </summary>
        private FileHeader Header;

        /// <summary>
        /// Current offset.
        /// </summary>
        public long CurrentOffset;

        /// <summary>
        /// The start of the file.
        /// </summary>
        public long FileOffset;

        /// <summary>
        /// Structure offsets.
        /// </summary>
        public Stack<long> StructureOffsets = new Stack<long>();

        /// <summary>
        /// Offsets.
        /// </summary>
        public Dictionary<string, long> Offsets = new Dictionary<string, long>();

        /// <summary>
        /// Block offsets.
        /// </summary>
        public List<long> BlockOffsets = new List<long>();

        /// <summary>
        /// Block sizes.
        /// </summary>
        public List<long> BlockSizes = new List<long>();
        
        /// <summary>
        /// Block types.
        /// </summary>
        public List<long> BlockTypes = new List<long>();

        /// <summary>
        /// References.
        /// </summary>
        public Dictionary<string, Reference<object>> References = new Dictionary<string, Reference<object>>();

        //Contructors.
        #region

        /// <summary>
        /// Create a new file writer.
        /// </summary>
        /// <param name="output">The output stream.</param>
        public FileWriter(Stream output) : base(output) {
            ByteOrder = ByteOrder.LittleEndian;
        }

        #endregion

        //Binary data writer. From Syroot.BinaryData.
        #region BinaryDataWriter

        /// <summary>
        /// Gets or sets the <see cref="P:Syroot.BinaryData.BinaryDataWriter.ByteConverter" /> instance used to parse multibyte binary data with.
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
        /// way the underlying <see cref="T:System.IO.BinaryWriter" /> is instantiated, it can only be specified at creation time.
        /// </summary>
        public Encoding Encoding { get; }

        /// <summary>
        /// Gets a value indicating whether the end of the stream has been reached.
        /// </summary>
        public bool EndOfStream {
            get {
                return this.BaseStream.IsEndOfStream();
            }
        }

        /// <summary>
        /// Gets or sets the length in bytes of the stream in bytes.
        /// </summary>
        public long Length {
            get {
                return this.BaseStream.Length;
            }
            set {
                this.BaseStream.SetLength(value);
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

        /// <summary>Aligns the writer to the next given byte multiple.</summary>
        /// <param name="alignment">The byte multiple.</param>
        /// <param name="grow"><c>true</c> to enlarge the stream size to include the final position in case it is larger
        /// than the current stream length.</param>
        /// <returns>The new position within the current stream.</returns>
        public long Align(int alignment, bool grow = true) {
            return this.BaseStream.Align(alignment, grow);
        }

        /// <summary>
        /// Writes a <see cref="T:System.Boolean" /> value in the given format to the current stream, with 0 representing
        /// <c>false</c> and 1 representing <c>true</c>.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Boolean" /> value to write.</param>
        /// <param name="format">The binary format in which the <see cref="T:System.Boolean" /> will be written.</param>
        public void Write(bool value, BooleanDataFormat format) {
            this.BaseStream.Write(value, format, this.ByteConverter);
        }

        /// <summary>
        /// Writes an enumeration of <see cref="T:System.Boolean" /> values to the current stream, with 0 representing
        /// <c>false</c> and 1 representing <c>true</c>.
        /// </summary>
        /// <param name="values">The <see cref="T:System.Boolean" /> values to write.</param>
        /// <param name="format">The binary format in which the <see cref="T:System.Boolean" /> will be written.</param>
        public void Write(IEnumerable<bool> values, BooleanDataFormat format = BooleanDataFormat.Byte) {
            this.BaseStream.Write(values, format, this.ByteConverter);
        }

        /// <summary>
        /// Writes a <see cref="T:System.DateTime" /> value to this stream. The <see cref="T:System.DateTime" /> will be available in the
        /// specified binary format.
        /// </summary>
        /// <param name="value">The <see cref="T:System.DateTime" /> value to write.</param>
        /// <param name="format">The binary format in which the <see cref="T:System.DateTime" /> will be written.</param>
        public void Write(DateTime value, DateTimeDataFormat format = DateTimeDataFormat.NetTicks) {
            this.BaseStream.Write(value, format, this.ByteConverter);
        }

        /// <summary>
        /// Writes an enumeration of <see cref="T:System.DateTime" /> values to this stream. The <see cref="T:System.DateTime" /> values
        /// will be available in the specified binary format.
        /// </summary>
        /// <param name="values">The <see cref="T:System.DateTime" /> values to write.</param>
        /// <param name="format">The binary format in which the <see cref="T:System.DateTime" /> values will be written.</param>
        public void Write(IEnumerable<DateTime> values, DateTimeDataFormat format = DateTimeDataFormat.NetTicks) {
            this.BaseStream.Write(values, format, this.ByteConverter);
        }

        /// <summary>
        /// Writes an 16-byte floating point value to this stream and advances the current position of the stream by
        /// sixteen bytes.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Decimal" /> value to write.</param>
        public override void Write(Decimal value) {
            this.BaseStream.Write(value, this.ByteConverter);
        }

        /// <summary>
        /// Writes an enumeration of <see cref="T:System.Decimal" /> values to the current stream and advances the current
        /// position by that number of <see cref="T:System.Decimal" /> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="T:System.Decimal" /> values to write.</param>
        public void Write(IEnumerable<Decimal> values) {
            this.BaseStream.Write(values, this.ByteConverter);
        }

        /// <summary>
        /// Writes an 8-byte floating point value to this stream and advances the current position of the stream by
        /// eight bytes.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Double" /> value to write.</param>
        public override void Write(double value) {
            this.BaseStream.Write(value, this.ByteConverter);
        }

        /// <summary>
        /// Writes an enumeration of <see cref="T:System.Double" /> values to the current stream and advances the current position
        /// by that number of <see cref="T:System.Double" /> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="T:System.Double" /> values to write.</param>
        public void Write(IEnumerable<double> values) {
            this.BaseStream.Write(values, this.ByteConverter);
        }

        /// <summary>
        /// Writes an enum value to this stream and advances the current position of the stream by the size of the
        /// underlying enum type size. Optionally validates the value to be defined in the enum type.
        /// </summary>
        /// <param name="value">The enum value to write.</param>
        /// <param name="strict"><c>true</c> to raise an <see cref="T:System.ArgumentOutOfRangeException" /> if the value is not
        /// defined in the enum type.</param>
        public void WriteEnum<T>(T value, bool strict = false) where T : struct, IComparable, IFormattable {
            this.BaseStream.WriteEnum(value, strict, this.ByteConverter);
        }

        /// <summary>
        /// Writes an enumeration of enum values to this stream and advances the current position of the stream by the
        /// size of the underlying enum type size multiplied by the number of values. Optionally validates the values to
        /// be defined in the enum type.
        /// </summary>
        /// <param name="values">The enum values to write.</param>
        /// <param name="strict"><c>true</c> to raise an <see cref="T:System.ArgumentOutOfRangeException" /> if a value is not
        /// defined in the enum type.</param>
        public void WriteEnums<T>(IEnumerable<T> values, bool strict = false) where T : struct, IComparable, IFormattable {
            this.BaseStream.WriteEnums(values, strict, this.ByteConverter);
        }

        /// <summary>
        /// Writes an 2-byte signed integer to this stream and advances the current position of the stream by two bytes.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Int16" /> value to write.</param>
        public override void Write(short value) {
            this.BaseStream.Write(value, this.ByteConverter);
        }

        /// <summary>
        /// Writes an enumeration of <see cref="T:System.Int16" /> values to the current stream and advances the current position
        /// by that number of <see cref="T:System.Int16" /> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="T:System.Int16" /> values to write.</param>
        public void Write(IEnumerable<short> values) {
            this.BaseStream.Write(values, this.ByteConverter);
        }

        /// <summary>
        /// Writes an 4-byte signed integer to this stream and advances the current position of the stream by four
        /// bytes.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Int32" /> value to write.</param>
        public override void Write(int value) {
            this.BaseStream.Write(value, this.ByteConverter);
        }

        /// <summary>
        /// Writes an enumeration of <see cref="T:System.Int32" /> values to the current stream and advances the current position
        /// by that number of <see cref="T:System.Int32" /> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="T:System.Int32" /> values to write.</param>
        public void Write(IEnumerable<int> values) {
            this.BaseStream.Write(values, this.ByteConverter);
        }

        /// <summary>
        /// Writes an 8-byte signed integer to this stream and advances the current position of the stream by eight
        /// bytes.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Int64" /> value to write.</param>
        public override void Write(long value) {
            this.BaseStream.Write(value, this.ByteConverter);
        }

        /// <summary>
        /// Writes an enumeration of <see cref="T:System.Int64" /> values to the current stream and advances the current position
        /// by that number of <see cref="T:System.Int64" /> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="T:System.Int64" /> values to write.</param>
        public void Write(IEnumerable<long> values) {
            this.BaseStream.Write(values, this.ByteConverter);
        }

        /// <summary>
        /// Writes an 4-byte floating point value to this stream and advances the current position of the stream by four
        /// bytes.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Single" /> value to write.</param>
        public override void Write(float value) {
            this.BaseStream.Write(value, this.ByteConverter);
        }

        /// <summary>
        /// Writes an enumeration of <see cref="T:System.Single" /> values to the current stream and advances the current position
        /// by that number of <see cref="T:System.Single" /> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="T:System.Single" /> values to write.</param>
        public void Write(IEnumerable<float> values) {
            this.BaseStream.Write(values, this.ByteConverter);
        }

        /// <summary>
        /// Writes a string to this stream with the given encoding and advances the current position of the stream in
        /// accordance with the encoding used and the specific characters being written to the stream. The string will
        /// be available in the specified binary format.
        /// </summary>
        /// <param name="value">The <see cref="T:System.String" /> value to write.</param>
        /// <param name="format">The binary format in which the string will be written.</param>
        /// <param name="encoding">The encoding used for converting the string.</param>
        public void Write(string value, StringDataFormat format, Encoding encoding = null) {
            this.BaseStream.Write(value, format, encoding, this.ByteConverter);
        }

        /// <summary>
        /// Writes an enumeration of <see cref="T:System.String" /> values to this stream with the given encoding. The strings
        /// will be available in the specified binary format.
        /// </summary>
        /// <param name="values">The <see cref="T:System.String" /> values to write.</param>
        /// <param name="format">The binary format in which the strings will be written.</param>
        /// <param name="encoding">The encoding used for converting the strings.</param>
        public void Write(IEnumerable<string> values, StringDataFormat format = StringDataFormat.DynamicByteCount, Encoding encoding = null) {
            this.BaseStream.Write(values, format, encoding, (ByteConverter)null);
        }

        /// <summary>
        /// Writes an 2-byte unsigned integer value to this stream and advances the current position of the stream by
        /// two bytes.
        /// </summary>
        /// <param name="value">The <see cref="T:System.UInt16" /> value to write.</param>
        public override void Write(ushort value) {
            this.BaseStream.Write(value, this.ByteConverter);
        }

        /// <summary>
        /// Writes an enumeration of <see cref="T:System.UInt16" /> values to the current stream and advances the current position
        /// by that number of <see cref="T:System.UInt16" /> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="T:System.UInt16" /> values to write.</param>
        public void Write(IEnumerable<ushort> values) {
            this.BaseStream.Write(values, this.ByteConverter);
        }

        /// <summary>
        /// Writes an 4-byte unsigned integer value to this stream and advances the current position of the stream by
        /// four bytes.
        /// </summary>
        /// <param name="value">The <see cref="T:System.UInt32" /> value to write.</param>
        public override void Write(uint value) {
            this.BaseStream.Write(value, this.ByteConverter);
        }

        /// <summary>
        /// Writes an enumeration of <see cref="T:System.UInt32" /> values to the current stream and advances the current position
        /// by that number of <see cref="T:System.UInt32" /> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="T:System.UInt32" /> values to write.</param>
        public void Write(IEnumerable<uint> values) {
            this.BaseStream.Write(values, this.ByteConverter);
        }

        /// <summary>
        /// Writes an 8-byte unsigned integer value to this stream and advances the current position of the stream by
        /// eight bytes.
        /// </summary>
        /// <param name="value">The <see cref="T:System.UInt64" /> value to write.</param>
        public override void Write(ulong value) {
            this.BaseStream.Write(value, this.ByteConverter);
        }

        /// <summary>
        /// Writes an enumeration of <see cref="T:System.UInt64" /> values to the current stream and advances the current position
        /// by that number of <see cref="T:System.UInt64" /> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="T:System.UInt64" /> values to write.</param>
        public void Write(IEnumerable<ulong> values) {
            this.BaseStream.Write(values, this.ByteConverter);
        }

        #endregion

        /// <summary>
        /// Write an item.
        /// </summary>
        ///<param name="w">Item to write.</param>
        public void Write(IWriteable w) {

            //Write.
            w.Write(this);

        }

        /// <summary>
        /// Write a file.
        /// </summary>
        /// <param name="f">The file to write.</param>
        public void WriteFile(IOFile f) {
            Write(f.Write());
        }

        /// <summary>
        /// Initialize a file.
        /// </summary>
        /// <typeparam name="T">Type of header.</typeparam>
        /// <param name="magic">The magic.</param>
        /// <param name="byteOrder">Byte order.</param>
        /// <param name="version">File version.</param>
        /// <param name="numBlocks">Number of blocks.</param>
        public void InitFile<T>(string magic, ByteOrder byteOrder, Version version, int numBlocks) {

            //Set properties.
            FileOffset = Position;
            Header = (FileHeader)Activator.CreateInstance(typeof(T));
            Header.ByteOrder = ByteOrder = byteOrder;
            Header.Version = version;
            Header.Magic = magic;
            Header.BlockOffsets = new long[numBlocks];
            Header.BlockSizes = new long[numBlocks];
            Header.BlockTypes = new long[numBlocks];
            Write(Header);
            Header.HeaderSize = Position - FileOffset;

        }

        /// <summary>
        /// Close the file.
        /// </summary>
        public void CloseFile() {

            //Set Write properties.
            Header.BlockOffsets = BlockOffsets.ToArray();
            Header.BlockSizes = BlockSizes.ToArray();
            Header.BlockTypes = BlockTypes.ToArray();
            Header.FileSize = Position - FileOffset;
            long bak = Position;
            Position = FileOffset;
            Write(Header);
            Position = bak;

        }

        /// <summary>
        /// Init a block.
        /// </summary>
        /// <param name="magic">The magic.</param>
        /// <param name="writeMagicAndSize">Whether or not to write the magic.</param>
        /// <param name="setOffset">If the offset should be set or not.</param>
        /// <param name="blockType">Block type.</param>
        public void InitBlock(string magic, bool writeMagicAndSize = true, bool setOffset = true, long blockType = 0) {

            //Add the block offset.
            BlockOffsets.Add(Position);
            BlockTypes.Add(blockType);

            //Write magic and size.
            if (writeMagicAndSize) {
                Write(magic.ToCharArray());
                Write((uint)0);
            }

            //Set offset.
            if (setOffset) {
                StartStructure();
            }

        }

        /// <summary>
        /// Close a block.
        /// </summary>
        /// <param name="writeBlockSize">If the block size should be written.</param>
        public void CloseBlock(bool writeBlockSize = true) {

            //Add block size.
            long bak = Position;
            BlockSizes.Add(Position - BlockOffsets[BlockOffsets.Count - 1]);

            //Write the block size.
            if (writeBlockSize) {
                Position = BlockOffsets[BlockOffsets.Count - 1] + 4;
                Write((uint)BlockSizes[BlockSizes.Count - 1]);
                EndStructure();
            }

            //Reset position.  
            Position = bak;

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
        /// Initialize an offset.
        /// </summary>
        /// <param name="name">Name of the offset.</param>
        public void InitOffset(string name) {
            Offsets.Add(name, Position);
            Write((uint)0);
        }

        /// <summary>
        /// Close and offset.
        /// </summary>
        /// <param name="name">Offset name.</param>
        /// <param name="absolute">If the offset is absolute.</param>
        /// <param name="offsetOverride">If to overide the offset.</param>
        public void CloseOffset(string name, bool absolute = false, long offsetOverride = -2) {
            long bak = Position;
            Position = Offsets[name];
            Offsets.Remove(name);
            if (offsetOverride != -2) {
                Write((uint)offsetOverride);
            } else if (absolute) {
                Write((uint)(bak - FileOffset));
            } else {
                Write((uint)(bak - CurrentOffset));
            }
            Position = bak;
        }

        /// <summary>
        /// Initialize a reference.
        /// </summary>
        /// <typeparam name="T">Type of reference to init.</typeparam>
        /// <param name="name">Name of the reference.</param>
        public void InitReference<T>(string name) {
            long posBak = Position;
            Reference<object> r = (Reference<object>)Activator.CreateInstance(typeof(T));
            r.InitWrite(this);
            r.Absolute = false;
            r.Identifier = 0;
            r.Offset = posBak;
            r.Size = 0;
            References.Add(name, r);
        }

        /// <summary>
        /// Close a reference.
        /// </summary>
        /// <param name="name">The name of the reference to close.</param>
        /// <param name="identifier">The identifier of the reference.</param>
        /// <param name="absolute">If the reference is absolute.</param>
        /// <param name="offsetOverride">Offset to override the reference's.</param>
        /// <param name="sizeOverride">Size to override the reference's.</param>
        public void CloseReference(string name, int identifier = 0, bool absolute = false, long offsetOverride = -2, long sizeOverride = -2) {
            long bak = Position;
            Position = References[name].Offset;
            References[name].Absolute = absolute;
            References[name].Identifier = identifier;
            References[name].Size = bak - Position;
            if (sizeOverride != -2) { References[name].Size = sizeOverride; }
            if (absolute) {
                References[name].Offset = bak - FileOffset;
            } else {
                References[name].Offset = bak - CurrentOffset;
            }
            if (offsetOverride != -2) { References[name].Offset = offsetOverride; }
            References[name].WriteRef(this, true);
            Position = bak;
            References.Remove(name);
        }

        /// <summary>
        /// Write bit flags.
        /// </summary>
        /// <param name="flags">Flags.</param>
        /// <param name="numBytes">Number of bytes to write.</param>
        public void WriteBitFlags(bool[] flags, int numBytes) {

            //Flags.
            ulong u = 0;
            for (int i = 0; i < flags.Length; i++) {
                if (flags[i]) { u |= (ulong)(0b1 << i); }
            }

            //Write.
            switch (numBytes) {
                case 1:
                    Write((byte)u);
                    break;
                case 2:
                    Write((short)u);
                    break;
                case 4:
                    Write((uint)u);
                    break;
                case 8:
                    Write(u);
                    break;
            }

        }

        /// <summary>
        /// Pad the stream to be even by a certain amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public void Pad(int amount) {
            while ((Position - FileOffset) % amount != 0) {
                Write((byte)0);
            }
        }

        /// <summary>
        /// Write a null terminated string.
        /// </summary>
        /// <param name="s">The string.</param>
        public void WriteNullTerminated(string s) {
            Write(s.ToCharArray());
            Write((byte)0);
        }

        /// <summary>
        /// Write a fixed string.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="amount">The size.</param>
        public void WriteFixedString(string s, int amount) {
            string str = s.Substring(0, Math.Min(amount, s.Length));
            Write(str, StringDataFormat.Raw);
            Write(new byte[amount - str.Length]);
        }

    }

}
