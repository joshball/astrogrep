using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace libAstroGrep
{
   /// <summary>
   /// Container for an exclusion item. [DEPRECATED, use FilterItem]
   /// Should only be used to convert exclusionitem to filteritem
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
   /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
   /// [Curtis_Beard]	   11/06/2014	CHG: Deprecate class, use FilterItem
   /// </history>
   public class ExclusionItem
   {
      #region Declarations
      
      private const char DELIMETER = '|';
      private const char LIST_DELIMETER = '<';

      #endregion

      #region Enumerations

      /// <summary>
      /// Exclusion type enumeration.
      /// </summary>
      /// <history>
      /// </history>
      public enum ExclusionTypes
      {
         /// <summary>File Extension</summary>
         FileExtension,
         /// <summary>File Name</summary>
         FileName,
         /// <summary>File Path</summary>
         FilePath,
         /// <summary>Directory Name</summary>
         DirectoryName,
         /// <summary>Directory Path</summary>
         DirectoryPath
      }

      /// <summary>
      /// Option type enumeration.
      /// </summary>
      public enum OptionsTypes
      {
         /// <summary>No option</summary>
         None,
         /// <summary>Value equals</summary>
         Equals,
         /// <summary>Value contains</summary>
         Contains,
         /// <summary>Value starts with</summary>
         StartsWith,
         /// <summary>Value ends with</summary>
         EndsWith
      }

      #endregion

      #region Properties

      /// <summary>
      /// Determines whether the exclusion is enabled.
      /// </summary>
      public bool Enabled { get; set; }
      
      /// <summary>
      /// Exclusion type.
      /// </summary>
      public ExclusionTypes Type { get; set; }

      /// <summary>
      /// Option type.
      /// </summary>
      public OptionsTypes Option { get; set; }

      /// <summary>
      /// Value of exclusion.
      /// </summary>
      public string Value { get; set; }

      /// <summary>
      /// Determines whether to ignore case.
      /// </summary>
      public bool IgnoreCase { get; set; }

      #endregion

      /// <summary>
      /// Creates an instance of this object.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      public ExclusionItem()
      {
         Enabled = true;
         Type = ExclusionTypes.FilePath;
         Value = string.Empty;
         Option = OptionsTypes.None;
         IgnoreCase = false;
      }

      /// <summary>
      /// Creates an instance of this object.
      /// </summary>
      /// <param name="enabled">Enabled</param>
      /// <param name="type">Exclusion type</param>
      /// <param name="value">Exclusion value</param>
      /// <param name="option">Exclusion option</param>
      /// <param name="ignoreCase">true/false to ignore case</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      public ExclusionItem(bool enabled, ExclusionTypes type, string value, OptionsTypes option, bool ignoreCase)
      {
         Enabled = enabled;
         Type = type;
         Value = value;
         Option = option;
         IgnoreCase = ignoreCase;
      }

      /// <summary>
      /// Outputs this object to a string using the delimeter.
      /// </summary>
      /// <returns>string representation of this object</returns>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      public override string ToString()
      {
         return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}", DELIMETER, Type.ToString(), Value, Option.ToString(), IgnoreCase.ToString(), Enabled);
      }

      #region Public Static Methods

      /// <summary>
      /// Creates an instance of an ExclusionItem object from a string.
      /// </summary>
      /// <param name="value">string to convert to object</param>
      /// <returns>ExclusionItem object</returns>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      public static ExclusionItem FromString(string value)
      {
         var item = new ExclusionItem();

         string[] values = value.Split(DELIMETER);
         item.Type = (ExclusionTypes)Enum.Parse(typeof(ExclusionTypes), values[0]);
         item.Value = values[1];
         item.Option = (OptionsTypes)Enum.Parse(typeof(OptionsTypes), values[2]);
         item.IgnoreCase = Convert.ToBoolean(values[3]);
         if (values.Length > 4)
            item.Enabled = Convert.ToBoolean(values[4]);

         return item;
      }

      /// <summary>
      /// Converts the given string to a list of ExclusionItems.
      /// </summary>
      /// <param name="value">string to convert</param>
      /// <returns>List of ExclusionItems</returns>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      public static List<ExclusionItem> ConvertStringToExclusions(string value)
      {
         var list = new List<ExclusionItem>();

         if (!string.IsNullOrEmpty(value))
         {
            var values = value.Split(LIST_DELIMETER);

            foreach (string val in values)
            {
               var item = ExclusionItem.FromString(val);
               list.Add(item);
            }
         }

         return list;
      }
      #endregion
   }
}
