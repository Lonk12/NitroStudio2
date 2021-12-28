using Kermalis.EndianBinaryIO;

namespace Kermalis.DLS2
{
    // TODO:

    public sealed class VersionChunk : DLSChunk
    {
        override internal void UpdateSize()
        {

        }

        internal VersionChunk(EndianBinaryReader reader) : base("vers", reader)
        {

        }
    }
}
