using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace GotaSoundIO.Sound.Playback {

    /// <summary>
    /// A stream player.
    /// </summary>
    public class StreamPlayer : IDisposable {

        /// <summary>
        /// Out data.
        /// </summary>
        public IWavePlayer SoundOut;

        /// <summary>
        /// Loop.
        /// </summary>
        public bool Loop;

        /// <summary>
        /// Wave.
        /// </summary>
        private RiffWave Riff;

        /// <summary>
        /// A new stream player.
        /// </summary>
        public StreamPlayer() {
            SoundOut = new WaveOut();
        }

        /// <summary>
        /// Loop stream.
        /// </summary>
        public LoopStream LoopStream;

        /// <summary>
        /// Wave file reader.
        /// </summary>
        public WaveFileReader WaveFileReader;

        /// <summary>
        /// Memory stream.
        /// </summary>
        public MemoryStream MemoryStream;

        /// <summary>
        /// Load a stream.
        /// </summary>
        /// <param name="s">The sound file.</param>
        public void LoadStream(SoundFile s) {
            Riff = new RiffWave();
            Riff.FromOtherStreamFile(s);
            MemoryStream = new MemoryStream(Riff.Write());
            WaveFileReader = new WaveFileReader(MemoryStream);
            SoundOut.Dispose();
            SoundOut = new WaveOut();
            LoopStream = new LoopStream(this, WaveFileReader, Riff.Loops && Loop, s.LoopStart, (Riff.Loops && Loop) ? s.LoopEnd : (uint)s.Audio.NumSamples);
            try {
                SoundOut.Init(LoopStream);
            } catch (NAudio.MmException e) { SoundOut = new NullWavePlayer(); }
        }

        /// <summary>
        /// Get the position.
        /// </summary>
        /// <returns>The position.</returns>
        public uint GetPosition() {
            return LoopStream == null ? 0 : LoopStream.CurrentSample;
        }

        /// <summary>
        /// Set the position.
        /// </summary>
        /// <param name="pos">The position.</param>
        public void SetPosition(uint pos) {
            if (LoopStream != null) {
                LoopStream.CurrentSample = pos;
            }
        }

        /// <summary>
        /// Get the length.
        /// </summary>
        /// <returns>The length.</returns>
        public uint GetLength() {
            return LoopStream == null ? 0 : LoopStream.GetLengthInSamples;
        }

        /// <summary>
        /// Play the stream.
        /// </summary>
        public void Play() {
            SoundOut.Stop();
            SoundOut.Play();
        }

        /// <summary>
        /// Pause.
        /// </summary>
        public void Pause() {
            if (SoundOut.PlaybackState == PlaybackState.Paused) {
                if (SoundOut as WaveOut != null) {
                    (SoundOut as WaveOut).Resume();
                }
            } else if (SoundOut.PlaybackState == PlaybackState.Playing) {
                SoundOut.Pause();
            }
        }

        /// <summary>
        /// Stop the stream.
        /// </summary>
        public void Stop() {
            SoundOut.Stop();
        }

        /// <summary>
        /// Dispose of the player.
        /// </summary>
        public void Dispose() {
            SoundOut.Stop();
            SoundOut.Dispose();
        }

    }

}
