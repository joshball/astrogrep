using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libAstroGrep
{
   /// <summary>
   /// Used to force a file to load with a certain Encoding.
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
   /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
   /// </history>
   public class FileEncoding
   {
      private const char DELIMETER = '|';
      private const char LIST_DELIMETER = '<';

      /// <summary>
      /// Determines if file encoding is enabled or not.
      /// </summary>
      public bool Enabled { get; set; }

      /// <summary>
      /// Full file path.
      /// </summary>
      public string FilePath { get; set; }
      
      /// <summary>
      /// File Encoding object.
      /// </summary>
      public Encoding Encoding { get; set; }

      /// <summary>
      /// Outputs this object to a string using the delimeter.
      /// </summary>
      /// <returns>string representation of this object</returns>
      /// <history>
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// </history>
      public override string ToString()
      {
         return string.Format("{1}{0}{2}{0}{3}", DELIMETER, Enabled, FilePath, Encoding.CodePage);
      }

      #region Public Static Methods

      /// <summary>
      /// Creates an instance of an FileEncoding object from a string.
      /// </summary>
      /// <param name="value">string to convert to object</param>
      /// <returns>FileEncoding object</returns>
      /// <history>
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// </history>
      public static FileEncoding FromString(string value)
      {
         string[] values = value.Split(DELIMETER);

         var item = new FileEncoding();
         item.Enabled = Convert.ToBoolean(values[0]);
         item.FilePath = values[1];
         item.Encoding = Encoding.GetEncoding(Convert.ToInt32(values[2]));

         return item;
      }

      /// <summary>
      /// Converts a List of FileEncodings to a string.
      /// </summary>
      /// <param name="list">List of FileEncodings</param>
      /// <returns>string of FileEncodings</returns>
      /// <history>
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// </history>
      public static string ConvertFileEncodingsToString(List<FileEncoding> list)
      {
         var builder = new System.Text.StringBuilder();

         if (list != null)
         {
            foreach (var item in list)
            {
               if (builder.Length > 0)
               {
                  builder.Append(LIST_DELIMETER);
               }

               builder.Append(item.ToString());
            }
         }

         return builder.ToString();
      }

      /// <summary>
      /// Converts the given string to a list of FileEncodings.
      /// </summary>
      /// <param name="value">string to convert</param>
      /// <returns>List of FileEncodings</returns>
      /// <history>
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// </history>
      public static List<FileEncoding> ConvertStringToFileEncodings(string value)
      {
         var list = new List<FileEncoding>();

         if (!string.IsNullOrEmpty(value))
         {
            var values = value.Split(LIST_DELIMETER);

            foreach (string val in values)
            {
               list.Add(FileEncoding.FromString(val));
            }
         }

         return list;
      }

      #endregion
   }
}
