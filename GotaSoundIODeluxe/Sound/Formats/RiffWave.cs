using GotaSoundIO.IO;
using GotaSoundIO.IO.RIFF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO.Sound {

    /// <summary>
    /// A standard RIFF wave.
    /// </summary>
    public class RiffWave : SoundFile {

        /// <summary>
        /// Get the supported encodings.
        /// </summary>
        /// <returns>The supported encodings.</returns>
        public override Type[] SupportedEncodings() => new Type[] { typeof(PCM16), typeof(PCM8) };

        /// <summary>
        /// Name.
        /// </summary>
        /// <returns>The name.</returns>
        public override string Name() => "WAV";

        /// <summary>
        /// Extensions.
        /// </summary>
        /// <returns>The extensions.</returns>
        public override string[] Extensions() => new string[] { "WAV" };

        /// <summary>
        /// Description.
        /// </summary>
        /// <returns>The description.</returns>
        public override string Description() => "A standard PCM wav file.";

        /// <summary>
        /// If the file supports tracks.
        /// </summary>
        /// <returns>No, it doesn't.</returns>
        public override bool SupportsTracks() => false;

        /// <summary>
        /// The preferred encoding.
        /// </summary>
        /// <returns>Preferred encoding for the file.</returns>
        public override Type PreferredEncoding() => null;

        /// <summary>
        /// Blank constructor.
        /// </summary>
        public RiffWave() {}

        /// <summary>
        /// Create a new wave from a file.
        /// </summary>
        /// <param name="filePath">The file.</param>
        public RiffWave(string filePath) : base(filePath) {}

        /// <summary>
        /// Read the RIFF.
        /// </summary>
        /// <param name="r">The reader.</param>
        public override void Read(FileReader r) {

            //Init.
            using (RiffReader rr = new RiffReader(r.BaseStream)) {

                //Format.
                rr.OpenChunk(rr.Chunks.Where(x => x.Magic.Equals("fmt ")).FirstOrDefault());
                if (rr.ReadUInt16() != 1) { throw new Exception("Unexpected standard WAV data format."); }
                int numChannels = rr.ReadUInt16();
                SampleRate = rr.ReadUInt32();
                rr.ReadUInt32(); //Byte rate.
                rr.ReadUInt16(); //Blocks.
                ushort bitsPerSample = rr.ReadUInt16();
                LoopStart = 0;
                LoopEnd = 0;
                Loops = false;
                if (bitsPerSample != 8 && bitsPerSample != 16) { throw new Exception("This tool only accepts 8-bit or 16-bit WAV files."); }

                //Sample.
                var smpl = rr.Chunks.Where(x => x.Magic.Equals("smpl")).FirstOrDefault();
                if (smpl != null) {
                    rr.OpenChunk(smpl);
                    rr.ReadUInt32s(7);
                    Loops = rr.ReadUInt32() > 0;
                    if (Loops) {
                        rr.ReadUInt32s(3);
                        LoopStart = r.ReadUInt32(); //(uint)(r.ReadUInt32() / (bitsPerSample / 8));
                        LoopEnd = r.ReadUInt32(); //(uint)(r.ReadUInt32() / (bitsPerSample / 8));
                    }
                }

                //Data.
                rr.OpenChunk(rr.Chunks.Where(x => x.Magic.Equals("data")).FirstOrDefault());
                uint dataSize = rr.Chunks.Where(x => x.Magic.Equals("data")).FirstOrDefault().Size;
                uint numBlocks = (uint)(dataSize / numChannels / (bitsPerSample / 8));
                r.Position = rr.Position;
                Audio.Read(r, bitsPerSample == 16 ? typeof(PCM16) : typeof(PCM8), numChannels, numBlocks, (uint)bitsPerSample / 8, 1, (uint)bitsPerSample / 8, 1, 0);
                Audio.ChangeBlockSize(-1);

            }

        }

        /// <summary>
        /// Write the RIFF.
        /// </summary>
        /// <param name="w">The writer.</param>
        public override void Write(FileWriter w) {

            //Init.
            using (RiffWriter rw = new RiffWriter(w.BaseStream)) {

                //Init file.
                rw.InitFile("WAVE");

                //Format block.
                rw.StartChunk("fmt ");
                rw.Write((ushort)1);
                rw.Write((ushort)Audio.Channels.Count);
                rw.Write(SampleRate);
                uint bitsPerSample = Audio.EncodingType.Equals(typeof(PCM16)) ? 16u : 8u;
                rw.Write((uint)(SampleRate * Audio.Channels.Count * (bitsPerSample / 8)));
                rw.Write((ushort)(bitsPerSample / 8 * Audio.Channels.Count));
                rw.Write((ushort)bitsPerSample);
                rw.EndChunk();

                //Sample block.
                if (Loops) {
                    rw.StartChunk("smpl");
                    rw.Write(new uint[2]);
                    rw.Write((uint)(1d / SampleRate * 1000000000));
                    rw.Write((uint)60);
                    rw.Write(new uint[3]);
                    rw.Write((uint)1);
                    rw.Write(new uint[3]);
                    rw.Write(LoopStart/* * bitsPerSample / 8*/);
                    rw.Write(LoopEnd/* * bitsPerSample / 8*/);
                    rw.Write((ulong)0);
                    rw.EndChunk();
                }

                //Data block.
                Audio.ChangeBlockSize((int)bitsPerSample / 8);
                rw.StartChunk("data");
                w.Position = rw.Position;
                Audio.Write(w);
                rw.Position = w.Position;
                while (rw.Position % 2 != 0) {
                    rw.Write((byte)0);
                }
                rw.EndChunk();

                //Close file.
                rw.CloseFile();
                Audio.ChangeBlockSize(-1);

            }

        }

    }

}
