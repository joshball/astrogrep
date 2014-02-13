namespace libAstroGrep
{
    public interface ISearchSpec
    {
        /// <summary>Array of start directories</summary>
        string[] StartDirectories { get; }

        /// <summary>Full file paths to files that will be searched (if defined, StartDirectories ignored, can be used for Search within Results)</summary>
        string[] StartFilePaths { get; set; }

        /// <summary>Use of directory recursion for grep</summary>
        bool SearchInSubfolders { get; }

        /// <summary>Use of regular expressions for grep</summary>
        bool UseRegularExpressions { get;  }

        /// <summary>Use of a case sensitive grep</summary>
        bool UseCaseSensitivity { get; }

        /// <summary>Use of a whole word match grep</summary>
        bool UseWholeWordMatching { get; }

        /// <summary>Use of negation of the grep results</summary>
        bool UseNegation { get; }

        /// <summary>The number of context lines included in grep results</summary>
        int ContextLines { get; }

        /// <summary>The search text</summary>
        string SearchText { get; }

        /// <summary>Whether to return only file names for grep results</summary>
        bool ReturnOnlyFileNames { get; }

        /// <summary>Sets including line numbers as part of a line</summary>
        bool IncludeLineNumbers { get; }

        /// <summary>Sets whether to detect file encoding or use default encoding (previous versions)</summary>
        bool DetectFileEncoding { get; }
    }
}