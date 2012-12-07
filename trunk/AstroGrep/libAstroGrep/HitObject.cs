using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace libAstroGrep
{
   /// <summary>
   /// A HitObject contains the information of one instance of a file that contains
   /// the search text in a Grep search.  It contains every instance of the search text
   /// in a file.  This is done by creating an object and then calling Add passing in the
   /// line and line number in which the search text was found.  Other properties include 
   /// file name, full path, holding directory, file last write time, total count, and all 
   /// lines.  It is also possible to retrieve a specific line or line number based on 
   /// the index into the contained collection.
   /// </summary>
   /// <remarks>
   ///   AstroGrep File Searching Utility. Written by Theodore L. Ward
   ///   Copyright (C) 2002 AstroComma Incorporated.
   ///   
   ///   This program is free software; you can redistribute it and/or
   ///   modify it under the terms of the GNU General Public License
   ///   as published by the Free Software Foundation; either version 2
   ///   of the License, or (at your option) any later version.
   ///   
   ///   This program is distributed in the hope that it will be useful,
   ///   but WITHOUT ANY WARRANTY; without even the implied warranty of
   ///   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   ///   GNU General Public License for more details.
   ///   
   ///   You should have received a copy of the GNU General Public License
   ///   along with this program; if not, write to the Free Software
   ///   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
   /// 
   ///   The author may be contacted at:
   ///   ted@astrocomma.com or curtismbeard@gmail.com
   /// </remarks>
   /// <history>
   /// [Curtis_Beard]      09/08/2005	Created
   /// [Curtis_Beard]	   11/21/2005	ADD: support for total hit count
   /// [Curtis_Beard]	   07/26/2006	ADD: 1512026, column position
   /// [Curtis_Beard]      09/12/2006  CHG: Converted to C#
   /// [Andrew_Radford]    05/08/2009  CHG: Update to C# 3.5, Generic Collections
   /// [Curtis_Beard]      10/13/2012	CHG: use class instead of 3 lists
   /// </history>
   public class HitObject
   {
      #region Declarations

      private FileInfo __File = null;
      private readonly List<HitObjectStorage> __storage = new List<HitObjectStorage>();

      #endregion

      #region Constructors

      /// <summary>
      /// Initializes a new instance of the HitObject class.
      /// </summary>
      /// <param name="file">FileInfo object</param>
      public HitObject(FileInfo file)
      {
         HitCount = 0;
         __File = file;
      }

      /// <summary>
      /// Initializes a new instance of the HitObject class.
      /// </summary>
      /// <param name="path">File path</param>
      public HitObject(string path)
      {
         HitCount = 0;
         __File = new FileInfo(path);
      }
      #endregion

      #region Public Properties

      /// <summary>
      /// Retrieve the name of the file
      /// </summary>
      /// <value>Name of file</value>
      /// <history>
      /// [Curtis_Beard]    09/09/2005	Created
      /// </history>
      public string FileName
      {
         get
         {
            return __File.Name;
         }
      }

      /// <summary>
      /// Retrieve the full path to the file
      /// </summary>
      /// <value>Full path to file</value>
      /// <history>
      /// [Curtis_Beard]    09/09/2005	Created
      /// </history>
      public string FilePath
      {
         get
         {
            return __File.FullName;
         }
      }

      /// <summary>
      /// Retrieve the holding directory of the file
      /// </summary>
      /// <value>Holding directory of file</value>
      /// <history>
      /// [Curtis_Beard]    09/09/2005	Created
      /// </history>
      public string FileDirectory
      {
         get
         {
            return __File.DirectoryName;
         }
      }

      /// <summary>
      /// Retrieve the last write time of the file
      /// </summary>
      /// <value>Last Write Time of file</value>
      /// <history>
      /// [Curtis_Beard]    09/09/2005	Created
      /// </history>
      public DateTime FileModifiedDate
      {
         get
         {
            return __File.LastWriteTime;
         }
      }

      /// <summary>
      /// Retrieve all lines containing the search text
      /// </summary>
      /// <value>All lines</value>
      /// <history>
      /// [Curtis_Beard]      09/09/2005	Created
      /// [Curtis_Beard]      10/13/2012	CHG: use class instead of 3 lists
      /// </history>
      public string Lines
      {
         get
         {
            var lines = new StringBuilder(__storage.Count);
            foreach (var item in __storage)
            {
               lines.Append(item.Line);
            }

            return lines.ToString();
         }
      }

      /// <summary>
      /// Retrieve the total number of lines
      /// </summary>
      /// <value>Total number of lines</value>
      /// <history>
      /// [Curtis_Beard]      09/09/2005	Created
      /// [Curtis_Beard]      10/13/2012	CHG: use class instead of 3 lists
      /// </history>
      public int LineCount
      {
         get
         {
            return __storage.Count;
         }
      }

      /// <summary>
      /// Gets/Sets the Index of the hit in the collection
      /// </summary>
      /// <value>Index position in collection</value>
      /// <returns>Index position in collection</returns>
      /// <history>
      /// [Curtis_Beard]      09/09/2005	Created
      /// </history>
      public int Index { get; set; }

      /// <summary>
      /// Gets the total hit count in the object
      /// </summary>
      /// <value>Total Hit Count</value>
      /// <returns>Total Hit Count</returns>
      /// <history>
      /// [Curtis_Beard]	   11/21/2005	Created
      /// </history>
      public int HitCount { get; private set; }

      #endregion

      #region Public Methods
      /// <summary>
      /// Retrieve a specified line
      /// </summary>
      /// <param name="index">line to retrieve</param>
      /// <returns>String.Empty if index is greater than total, line of text otherwise</returns>
      /// <history>
      /// [Curtis_Beard]      09/09/2005	Created
      /// [Curtis_Beard]      10/13/2012	CHG: use class instead of 3 lists
      /// </history>
      public string RetrieveLine(int index)
      {
         if (index >= __storage.Count)
            return string.Empty;

         return __storage[index].Line;
      }

      /// <summary>
      /// Retrieve a specified line number
      /// </summary>
      /// <param name="index">line number to retrieve</param>
      /// <returns>0 if index is greater than total, line number otherwise</returns>
      /// <history>
      /// [Curtis_Beard]      09/09/2005	Created
      /// [Curtis_Beard]      10/13/2012	CHG: use class instead of 3 lists
      /// </history>
      public int RetrieveLineNumber(int index)
      {
         if (index >= __storage.Count)
            return 0;

         return __storage[index].LineNumber;
      }

      /// <summary>
      /// Retrieve the first hit's column position.
      /// </summary>
      /// <param name="index">line number to retrieve</param>
      /// <returns>0 if index is greater than total, column position otherwise</returns>
      /// <history>
      /// [Curtis_Beard]		07/26/2006	ADD: 1512026, save column position
      /// [Curtis_Beard]      10/13/2012	CHG: use class instead of 3 lists
      /// </history>
      public int RetrieveColumn(int index)
      {
         if (index >= __storage.Count)
            return 0;

         return __storage[index].ColumnNumber;
      }

      /// <summary>
      /// Retrieve the first actual hit's index.
      /// </summary>
      /// <returns>Index position or 0 if not found</returns>
      /// <history>
      /// [Curtis_Beard]      10/13/2012	ADD: use class instead of 3 lists
      /// </history>
      public int RetrieveFirstHitIndex()
      {
         for (int i = 0; i < __storage.Count; i++)
         {
            if (__storage[i].IsHit)
            {
               return i;
            }
         }

         return 0;
      }

      /// <summary>
      /// Add a hit to the collection.
      /// </summary>
      /// <param name="line">line of text containing search text</param>
      /// <param name="lineNumber">line number of line in file</param>
      /// <returns>position of added item in collection</returns>
      /// <history>
      /// [Curtis_Beard]      09/09/2005	Created
      /// [Curtis_Beard]      07/26/2006	ADD: 1512026, save column position
      /// [Curtis_Beard]      10/13/2012	CHG: use class instead of 3 lists
      /// </history>
      public int Add(string line, int lineNumber)
      {
         var item = new HitObjectStorage();
         item.Line = line;
         item.LineNumber = lineNumber;
         item.ColumnNumber = 1;
         item.IsHit = false;
         __storage.Add(item);

         return __storage.Count - 1;
      }

      /// <summary>
      /// Add a hit to the collection.
      /// </summary>
      /// <param name="line">line of text containing search text</param>
      /// <param name="lineNumber">line number of line in file</param>
      /// <param name="column">column position of first hit in line</param>
      /// <returns>position of added item in collection</returns>
      /// <history>
      /// [Curtis_Beard]		07/26/2006	ADD: 1512026, save column position
      /// [Curtis_Beard]      10/13/2012	CHG: use class instead of 3 lists
      /// </history>
      public int Add(string line, int lineNumber, int column)
      {
         var item = new HitObjectStorage();
         item.Line = line;
         item.LineNumber = lineNumber;
         item.ColumnNumber = column;
         item.IsHit = true;
         __storage.Add(item);

         return __storage.Count - 1;
      }

      /// <summary>
      /// Updates the total hit count
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   11/21/2005	Created
      /// </history>
      public void SetHitCount()
      {
         HitCount += 1;
      }

      /// <summary>
      /// Updates the total hit count
      /// </summary>
      /// <param name="count">Value to add count to total</param>
      /// <history>
      /// [Curtis_Beard]	   11/21/2005	Created
      /// </history>
      public void SetHitCount(int count)
      {
         HitCount += count;
      }

      #endregion

      /// <summary>
      /// Storage class for each hit.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      10/13/2012	ADD: use class instead of 3 lists
      /// </history>
      internal class HitObjectStorage
      {
         /// <summary>Current line</summary>
         public string Line { get; set; }

         /// <summary>Current line number</summary>
         public int LineNumber { get; set; }

         /// <summary>Current column number</summary>
         public int ColumnNumber { get; set; }

         /// <summary>Determines if this is a hit or context line</summary>
         public bool IsHit { get; set; }

         /// <summary>
         /// Initializes this class.
         /// </summary>
         /// <history>
         /// [Curtis_Beard]      10/13/2012	ADD: use class instead of 3 lists
         /// </history>
         public HitObjectStorage()
         {
            Line = string.Empty;
            LineNumber = 1;
            ColumnNumber = 1;
            IsHit = false;
         }
      }
   }
}
