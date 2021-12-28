using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO.Sound.Playback {

    /// <summary>
    /// Stream for looping playback
    /// </summary>
    public class LoopStream : WaveStream {

        /// <summary>
        /// Source stream.
        /// </summary>
        WaveStream sourceStream;

        /// <summary>
        /// Loop start.
        /// </summary>
        public uint LoopStart;

        /// <summary>
        /// Loop end.
        /// </summary>
        public uint LoopEnd;

        /// <summary>
        /// Player.
        /// </summary>
        private StreamPlayer player;

        /// <summary>
        /// Creates a new Loop stream.
        /// </summary>
        /// <param name="player">Player.</param>
        /// <param name="sourceStream">The stream to read from. Note: the Read method of this stream should return 0 when it reaches the end or else we will not loop to the start again.</param>
        /// <param name="loops">If to loop the playback.</param>
        /// <param name="loopStart">Loop start sample.</param>
        /// <param name="loopEnd">Loop end sample.</param>
        public LoopStream(StreamPlayer player, WaveStream sourceStream, bool loops, uint loopStart, uint loopEnd) {
            this.player = player;
            this.sourceStream = sourceStream;
            Loops = loops;
            LoopStart = loopStart;
            LoopEnd = loopEnd;
        }

        /// <summary>
        /// Use this to turn looping on or off
        /// </summary>
        public bool Loops { get; set; }

        /// <summary>
        /// Return source stream's wave format
        /// </summary>
        public override WaveFormat WaveFormat {
            get { return sourceStream.WaveFormat; }
        }

        /// <summary>
        /// LoopStream simply returns
        /// </summary>
        public override long Length {
            get { return sourceStream.Length; }
        }

        /// <summary>
        /// LoopStream simply passes on positioning to source stream
        /// </summary>
        public override long Position {
            get { return sourceStream.Position; }
            set { sourceStream.Position = value; }
        }

        /// <summary>
        /// Current sample.
        /// </summary>
        public uint CurrentSample {
            get { return (uint)(sourceStream.Position / WaveFormat.Channels / (WaveFormat.BitsPerSample / 8)); }
            set { sourceStream.Position = value * WaveFormat.Channels * (WaveFormat.BitsPerSample / 8); }
        }

        /// <summary>
        /// The length in samples.
        /// </summary>
        public uint GetLengthInSamples { 
            get => (uint)(sourceStream.Length / WaveFormat.Channels / (WaveFormat.BitsPerSample / 8));
        }

        /// <summary>
        /// Read data.
        /// </summary>
        public override int Read(byte[] buffer, int offset, int count) {             
            int totalBytesRead = 0;
            while (totalBytesRead < count) {
                int bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                if (bytesRead == 0 || sourceStream.Position > LoopEnd * WaveFormat.Channels * WaveFormat.BitsPerSample / 8 || sourceStream.Position > sourceStream.Length) {

                    //Break.
                    if (sourceStream.Position == 0 || !(Loops && player.Loop)) {
                        break;
                    }

                    //Loop.
                    if (Loops && player.Loop) {
                        if (CurrentSample >= LoopEnd) {
                            CurrentSample = LoopStart;
                        }
                    }

                }
                totalBytesRead += bytesRead;
            }
            return totalBytesRead;
        }
    }

}
