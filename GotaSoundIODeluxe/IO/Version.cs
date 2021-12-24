using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotaSoundIO.IO {

    /// <summary>
    /// Version.
    /// </summary>
    public abstract class Version : IReadable, IWriteable {

        /// <summary>
        /// Major version.
        /// </summary>
        public byte Major;

        /// <summary>
        /// Minor version.
        /// </summary>
        public byte Minor;

        /// <summary>
        /// Revision.
        /// </summary>
        public byte Revision;

        /// <summary>
        /// Read the version.
        /// </summary>
        /// <param name="r">The reader.</param>
        public abstract void Read(FileReader r);

        /// <summary>
        /// Write the version.
        /// </summary>
        /// <param name="w">The writer.</param>
        public abstract void Write(FileWriter w);

        /// <summary>
        /// If the version is greater.
        /// </summary>
        /// <param name="v1">Version 1.</param>
        /// <param name="v2">Version 2.</param>
        /// <returns>If the left version is greater.</returns>
        public static bool operator >(Version v1, Version v2) {
            if (v1.Major > v2.Major) { return true; }
            if (v1.Major < v2.Major) { return false; }
            if (v1.Minor > v2.Minor) { return true; }
            if (v1.Minor < v2.Minor) { return false; }
            if (v1.Revision > v2.Revision) { return true; }
            return false;
        }

        /// <summary>
        /// If the version is lesser.
        /// </summary>
        /// <param name="v1">Version 1.</param>
        /// <param name="v2">Version 2.</param>
        /// <returns>If the left version is lesser.</returns>
        public static bool operator <(Version v1, Version v2) {
            if (v1.Major < v2.Major) { return true; }
            if (v1.Major > v2.Major) { return false; }
            if (v1.Minor < v2.Minor) { return true; }
            if (v1.Minor > v2.Minor) { return false; }
            if (v1.Revision < v2.Revision) { return true; }
            return false;
        }

        /// <summary>
        /// If the version is greater or equal to.
        /// </summary>
        /// <param name="v1">Version 1.</param>
        /// <param name="v2">Version 2.</param>
        /// <returns>If the left version is greater or equal to.</returns>
        public static bool operator <=(Version v1, Version v2) {
            return v1 < v2 || v1 == v2;
        }

        /// <summary>
        /// If the version is lesser or equal to.
        /// </summary>
        /// <param name="v1">Version 1.</param>
        /// <param name="v2">Version 2.</param>
        /// <returns>If the left version is lesser or equal to.</returns>
        public static bool operator >=(Version v1, Version v2) {
            return v1 > v2 || v1 == v2;
        }

        /// <summary>
        /// If the versions are equal.
        /// </summary>
        /// <param name="v1">The first version.</param>
        /// <param name="v2">The second version.</param>
        /// <returns>If the versions are equal.</returns>
        public static bool operator ==(Version v1, Version v2) {
            return v1.Major == v2.Major && v1.Minor == v2.Minor && v1.Revision == v2.Revision;
        }

        /// <summary>
        /// If the versions are not equal.
        /// </summary>
        /// <param name="v1">The first version.</param>
        /// <param name="v2">The second version.</param>
        /// <returns>If the versions are not equal.</returns>
        public static bool operator !=(Version v1, Version v2) {
            return !(v1 == v2);
        }

        /// <summary>
        /// If this object equals another one.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>If the versions are equal.</returns>
        public override bool Equals(object obj) {
            if (obj as Version != null) {
                return (obj as Version) == this;
            } else {
                return false;
            }
        }

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode() {
            return Major.GetHashCode() * Minor.GetHashCode() * Revision.GetHashCode();
        }

    }

}
