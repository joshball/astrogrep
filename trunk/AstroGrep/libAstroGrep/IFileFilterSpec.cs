using System;

namespace libAstroGrep
{
    public interface IFileFilterSpec
    {
        /// <summary>The FileFilter</summary>
        string FileFilter { get; }

        /// <summary>Whether to skip hidden files and directories.</summary>
        bool SkipHiddenFiles { get; }

        /// <summary>Whether to skip system files and directories.</summary>
        bool SkipSystemFiles { get; }

        DateTime DateModifiedStare { get; }

        DateTime DateModifiedEnd { get; }

        int FileSizeMin { get; }

        int FileSizeMax { get; }

        string FileNameRegex { get; }
    }
}