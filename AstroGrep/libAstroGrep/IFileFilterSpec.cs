using System;
using System.Collections.Generic;

namespace libAstroGrep
{
   /// <summary>
   /// Interface to grep that allow filter of files.
   /// </summary>
   /// <history>
   /// [Curtis_Beard]	   11/11/2014	CHG: move all file/dir filters to FilterItems list.
   /// </history>
   public interface IFileFilterSpec
   {
      /// <summary>The FileFilter</summary>
      string FileFilter { get; }

      /// <summary>
      /// List of FilterItems that will filter out files/directories based on user inputted options.
      /// </summary>
      /// <remarks>
      /// Examples are Files that are readonly, binary, or the name contains certain text.
      /// Directories that are created after a certain date, or marked as system.
      /// </remarks>
      List<FilterItem> FilterItems { get; }
   }
}