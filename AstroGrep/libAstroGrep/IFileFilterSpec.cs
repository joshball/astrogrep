using System;

namespace libAstroGrep
{
   /// <summary>
   /// Interface to grep that allow filter of files.
   /// </summary>
   public interface IFileFilterSpec
   {
      /// <summary>The FileFilter</summary>
      string FileFilter { get; }

      /// <summary>Whether to skip hidden files and directories.</summary>
      bool SkipHiddenFiles { get; }

      /// <summary>Whether to skip system files and directories.</summary>
      bool SkipSystemFiles { get; }

      /// <summary>
      /// Modified start date
      /// </summary>
      DateTime DateModifiedStart { get; }

      /// <summary>
      /// Modified end date
      /// </summary>
      DateTime DateModifiedEnd { get; }

      /// <summary>
      /// Minimum file size
      /// </summary>
      long FileSizeMin { get; }

      /// <summary>
      /// Maximum file size
      /// </summary>
      long FileSizeMax { get; }

      /// <summary>
      /// List of ExclusionItem objects to exclude certain files/folders.
      /// </summary>
      System.Collections.Generic.List<ExclusionItem> ExclusionItems { get; }
   }
}