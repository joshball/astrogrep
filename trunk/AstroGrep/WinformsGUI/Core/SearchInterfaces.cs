using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using libAstroGrep;

namespace AstroGrep.Core
{
   /// <summary>
   /// Implements the libAstroGrep interfaces.
   /// </summary>
   /// <remarks>
   /// AstroGrep File Searching Utility. Written by Theodore L. Ward
   /// Copyright (C) 2002 AstroComma Incorporated.
   /// 
   /// This program is free software; you can redistribute it and/or
   /// modify it under the terms of the GNU General Public License
   /// as published by the Free Software Foundation; either version 2
   /// of the License, or (at your option) any later version.
   /// 
   /// This program is distributed in the hope that it will be useful,
   /// but WITHOUT ANY WARRANTY; without even the implied warranty of
   /// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   /// GNU General Public License for more details.
   /// 
   /// You should have received a copy of the GNU General Public License
   /// along with this program; if not, write to the Free Software
   /// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
   /// 
   /// The author may be contacted at:
   /// ted@astrocomma.com or curtismbeard@gmail.com
   /// </remarks>
   /// <history>
   /// [Curtis_Beard]		12/01/2014	Created
   /// </history>
   public class SearchInterfaces
   {
      /// <summary>
      /// Implement ISearchSpec interface.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		12/01/2014	Moved from frmMain.cs
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// [Curtis_Beard]      04/07/2015	CHG: remove line numbers
      /// </history>
      public struct SearchSpec : ISearchSpec
      {
         /// <summary>starting directories</summary>
         public string[] StartDirectories { get; set; }

         /// <summary>starting full file paths which if defined will ignore StartDirectories</summary>
         public string[] StartFilePaths { get; set; }

         /// <summary>search in sub folders</summary>
         public bool SearchInSubfolders { get; set; }

         /// <summary>search text will be used as a regular expression</summary>
         public bool UseRegularExpressions { get; set; }

         /// <summary>enable case sensitive searching</summary>
         public bool UseCaseSensitivity { get; set; }

         /// <summary>enable whole word matching</summary>
         public bool UseWholeWordMatching { get; set; }

         /// <summary>enable detecting files that don't match the search text</summary>
         public bool UseNegation { get; set; }

         /// <summary>number of context lines (0 is default)</summary>
         public int ContextLines { get; set; }

         /// <summary>the text to find</summary>
         public string SearchText { get; set; }

         /// <summary>enable only processing the file up until one match is found</summary>
         public bool ReturnOnlyFileNames { get; set; }

         /// <summary>enable detecting the file encoding (false uses Encoding.Default)</summary>
         public bool DetectFileEncoding { get; set; }

         /// <summary>list of FileEncodings objects to force encoding of certain files selected by user</summary>
         public List<FileEncoding> FileEncodings { get; set; }
      }

      /// <summary>
      /// Implement IFileFilterSpec interface.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		12/01/2014	Moved from frmMain.cs, adjusted to match new interface
      /// </history>
      public struct FileFilterSpec : IFileFilterSpec
      {
         /// <summary>List of file filters used when retrieving files from a directory (multiples separated by , or ; )</summary>
         public string FileFilter { get; set; }

         /// <summary>List of FilterItems that can be used to filter out files/directories based on certain features of said file/directory.</summary>
         public List<FilterItem> FilterItems { get; set; }
      }
   }
}
