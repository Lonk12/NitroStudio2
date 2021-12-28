using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO.IO {

    /// <summary>
    /// Reference.
    /// </summary>
    public abstract class Reference<T> : IReadable, IWriteable {

        /// <summary>
        /// Data contained by this reference.
        /// </summary>
        public T Data;

        /// <summary>
        /// Convert it to data.
        /// </summary>
        /// <param name="r">Reference.</param>
        public static implicit operator T(Reference<T> r) => r.Data;

        /// <summary>
        /// Data types. They must implement the main type T.
        /// </summary>
        public virtual List<Type> DataTypes => new List<Type>();

        /// <summary>
        /// Set current offset on jump.
        /// </summary>
        public abstract bool SetCurrentOffsetOnJump();

        /// <summary>
        /// If a null reference is 0.
        /// </summary>
        /// <returns>If the reference is 0.</returns>
        public abstract bool NullReferenceIs0();

        /// <summary>
        /// If the reference is absolute.
        /// </summary>
        public bool Absolute;

        /// <summary>
        /// Identifier.
        /// </summary>
        public int Identifier;

        /// <summary>
        /// Offset.
        /// </summary>
        public long Offset = -1;

        /// <summary>
        /// Size.
        /// </summary>
        public long Size = -1;

        /// <summary>
        /// Reference position.
        /// </summary>
        private long ReferencePosition;

        /// <summary>
        /// Read the reference.
        /// </summary>
        /// <param name="r">The reader.</param>
        public abstract void ReadRef(FileReader r);

        /// <summary>
        /// Write the reference.
        /// </summary>
        /// <param name="w">The writer.</param>
        /// <param name="ignoreNull">Ignore null data.</param>
        public abstract void WriteRef(FileWriter w, bool ignoreNullData = false);

        /// <summary>
        /// Read the reference.
        /// </summary>
        /// <param name="r"></param>
        public void Read(FileReader r) {

            //Read the reference info.
            ReadRef(r);

            //Read the data.
            ReadData(r);

        }

        /// <summary>
        /// Read the data.
        /// </summary>
        /// <param name="r">The reader.</param>
        public void ReadData(FileReader r) {

            //Set back position.
            long bak = r.Position;

            //Valid.
            if (Offset != 0 && Offset != -1) {

                //Jump to reference.
                r.Position = (Absolute ? 0 : r.CurrentOffset) + Offset;

                //Set current offset on jump.
                if (SetCurrentOffsetOnJump()) {
                    r.StructureOffsets.Push(r.CurrentOffset);
                    r.CurrentOffset = r.Position;
                }

                //Identifier.
                T obj;
                if (DataTypes.Count > 0) {
                    Type type = Identifier < DataTypes.Count ? DataTypes[Identifier] : null;
                    if (type != null) {
                        obj = (T)Activator.CreateInstance(type);
                    } else {
                        obj = default(T);
                    }
                } else {
                    obj = Activator.CreateInstance<T>();
                }

                //Data.
                if (obj != null) {

                    //Read data.
                    if (obj as IOFile != null) {
                        FileReader r2 = new FileReader(r.BaseStream);
                        r2.Position = r.Position;
                        ((IReadable)obj).Read(r2);
                        r.Position = r2.Position;
                    } else {
                        ((IReadable)obj).Read(r);
                    }

                }

                //Restore current offset.
                if (SetCurrentOffsetOnJump()) {
                    r.CurrentOffset = r.StructureOffsets.Pop();
                }

                //Set position.
                r.Position = bak;

                //Return the data.
                Data = obj;

            }

            //Invalid.
            else {
                Data = default(T);
            }

        }

        /// <summary>
        /// Initialize a write.
        /// </summary>
        /// <param name="w">The writer.</param>
        public void InitWrite(FileWriter w) {
            ReferencePosition = w.Position;
            Offset = NullReferenceIs0() ? 0 : -1;
            Size = -1;
            WriteRef(w);
        }


        /// <summary>
        /// Write the reference data.
        /// </summary>
        /// <param name="w">The writer.</param>
        public void WriteData(FileWriter w) {

            //Set backup.
            long bak = w.Position;

            //Write data.
            if (Data != null) {
                Offset = bak - w.CurrentOffset;
                if (SetCurrentOffsetOnJump()) {
                    w.StructureOffsets.Push(w.CurrentOffset);
                    w.CurrentOffset = w.Position;
                }
                if (Data as byte[] != null) {
                    w.Write(Data as byte[]);
                } else {
                    w.Write((IWriteable)Data);
                }
                Size = w.Position - bak;
                if (SetCurrentOffsetOnJump()) {
                    w.CurrentOffset = w.StructureOffsets.Pop();
                }
                if (DataTypes.Count > 0) {
                    for (int i = 0; i < DataTypes.Count; i++) {
                        try {
                            var h = Convert.ChangeType(Data, DataTypes[i]);
                            if (h != null) {
                                Identifier = i;
                            }
                        } catch { }
                    }
                }
            } else if (DataTypes.Count > 0) {
                Identifier = 0;
            }

            //Go to reference position.
            bak = w.Position;
            w.Position = ReferencePosition;
            WriteRef(w);
            w.Position = bak;

        }

        /// <summary>
        /// Close the reference.
        /// </summary>
        /// <param name="w">The writer.</param>
        public void CloseReference(FileWriter w) {

            //Go to reference position.
            long bak = w.Position;
            Offset = bak - w.CurrentOffset;
            w.Position = ReferencePosition;
            WriteRef(w, true);
            w.Position = bak;

        }

        /// <summary>
        /// Write the reference.
        /// </summary>
        /// <param name="w">The writer.</param>
        public void Write(FileWriter w) {
            InitWrite(w);
            WriteData(w);
        }

    }

}
