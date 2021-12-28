using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO.Sound.Playback {

    /// <summary>
    /// Null wave provider.
    /// </summary>
    public class NullWavePlayer : IWavePlayer {

        /// <summary>
        /// Volume.
        /// </summary>
        public float Volume { get => m_Volume; set => m_Volume = value; }
        private float m_Volume = 1f;

        /// <summary>
        /// Playback state.
        /// </summary>
        public PlaybackState PlaybackState => throw new NotImplementedException();
        private PlaybackState m_PlaybackState;

        /// <summary>
        /// Playback stopped.
        /// </summary>
        public event EventHandler<StoppedEventArgs> PlaybackStopped;

        /// <summary>
        /// Play.
        /// </summary>
        public void Play() {
            m_PlaybackState = PlaybackState.Playing;
        }

        /// <summary>
        /// Stop.
        /// </summary>
        public void Stop() {
            m_PlaybackState = PlaybackState.Stopped;
        }

        /// <summary>
        /// Pause.
        /// </summary>
        public void Pause() {
            if (m_PlaybackState == PlaybackState.Paused) {
                m_PlaybackState = PlaybackState.Playing;
            } else {
                m_PlaybackState = PlaybackState.Paused;
            }
        }

        /// <summary>
        /// Init.
        /// </summary>
        public void Init(IWaveProvider waveProvider) {}

        /// <summary>
        /// There's nothing to dispose.
        /// </summary>
        public void Dispose() {}

    }

}
