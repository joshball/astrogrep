using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace libAstroGrep
{
   /// <summary>
   /// Used to store a user's exclusion input.
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
   /// </history>
   public class FilterItem
   {
      private const char DELIMETER = '|';
      private const char LIST_DELIMETER = '<';

      /// <summary>
      /// Determines whether the item is enabled or not.
      /// </summary>
      public bool Enabled { get; set; }

      /// <summary>
      /// Gets/Sets the item's directive type.
      /// </summary>
      public FilterType FilterType { get; set; }

      /// <summary>
      /// Gets/Sets the user inputted value.
      /// </summary>
      public string Value { get; set; }

      /// <summary>
      /// Gets/Sets the user inputted option.
      /// </summary>
      public FilterType.ValueOptions ValueOption { get; set; }

      /// <summary>
      /// Gets/Sets the user inputted option to ignore case (string type).
      /// </summary>
      public bool ValueIgnoreCase { get; set; }

      /// <summary>
      /// Gets/Sets the user inputted option for size display (Size type).
      /// </summary>
      public string ValueSizeOption { get; set; }

      /// <summary>
      /// Creates an instance of this class.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   10/31/2014	ADD: exclusions update
      /// </history>
      public FilterItem()
      {
         Enabled = false;
         FilterType = null;
         Value = string.Empty;
         ValueOption = FilterType.ValueOptions.None;
         ValueIgnoreCase = false;
         ValueSizeOption = string.Empty;
      }

      /// <summary>
      /// Creates an instance of this class.
      /// </summary>
      /// <param name="filterType">The type of filter to be used for this item.</param>
      /// <param name="value">The current user value</param>
      /// <param name="valueOption">The current user option for this value</param>
      /// <param name="ignoreCase">Whether the ignore case option was selected</param>
      /// <history>
      /// [Curtis_Beard]	   10/31/2014	ADD: exclusions update
      /// </history>
      public FilterItem(FilterType filterType, string value, FilterType.ValueOptions valueOption, bool ignoreCase)
         : this()
      {
         Enabled = true;
         FilterType = filterType;
         Value = value;
         ValueOption = valueOption;
         ValueIgnoreCase = ignoreCase;
      }

      /// <summary>
      /// Creates an instance of this class.
      /// </summary>
      /// <param name="filterType">The type of filter to be used for this item.</param>
      /// <param name="value">The current user value</param>
      /// <param name="valueOption">The current user option for this value</param>
      /// <param name="ignoreCase">Whether the ignore case option was selected</param>
      /// <param name="valueSizeOption">The size display option for this value</param>
      /// <param name="enabled">Whether this item is enabled or not</param>
      public FilterItem(FilterType filterType, string value, FilterType.ValueOptions valueOption, bool ignoreCase, string valueSizeOption, bool enabled)
         : this(filterType, value, valueOption, ignoreCase, enabled)
      {
         ValueSizeOption = valueSizeOption;
      }

      /// <summary>
      /// Creates an instance of this class.
      /// </summary>
      /// <param name="filterType">The type of filter to be used for this item.</param>
      /// <param name="value">The current user value</param>
      /// <param name="valueOption">The current user option for this value</param>
      /// <param name="ignoreCase">Whether the ignore case option was selected</param>
      /// <param name="enabled">Whether this item is enabled or not</param>
      /// <history>
      /// [Curtis_Beard]	   10/31/2014	ADD: exclusions update
      /// </history>
      public FilterItem(FilterType filterType, string value, FilterType.ValueOptions valueOption, bool ignoreCase, bool enabled)
         : this(filterType, value, valueOption, ignoreCase)
      {
         Enabled = enabled;
      }

      /// <summary>
      /// Outputs this object to a string using the delimeter.
      /// </summary>
      /// <returns>string representation of this object</returns>
      /// <history>
      /// [Curtis_Beard]	   10/31/2014	ADD: exclusions update
      /// </history>
      public override string ToString()
      {
         return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}", DELIMETER, FilterType.ToString(), Value, ValueOption.ToString(), ValueIgnoreCase.ToString(), ValueSizeOption, Enabled);
      }

      /// <summary>
      /// Deteremines whether the given FileInfo object should be excluded based on the exclusion settings.
      /// </summary>
      /// <param name="file">FileInfo object</param>
      /// <param name="value">output value for causing filtering</param>
      /// <returns>true if matches a setting, false if not</returns>
      /// <history>
      /// [Curtis_Beard]	   10/31/2014	ADD: exclusions update
      /// </history>
      public bool ShouldExcludeFile(FileInfo file, out string value)
      {
         value = string.Empty;

         if (Enabled && FilterType.Category == FilterType.Categories.File)
         {
            switch (FilterType.SubCategory)
            {
               case FilterType.SubCategories.Extension:
                  string temp = Value.ToLower();
                  if (file.Extension.StartsWith(".") && !temp.StartsWith("."))
                  {
                     temp = string.Format(".{0}", temp);
                  }
                  value = temp;
                  return file.Extension.ToLower().Equals(temp);

               case FilterType.SubCategories.Name:
                  value = file.Name;
                  return CheckStringAgainstOption(file.Name);

               case FilterType.SubCategories.Path:
                  value = file.FullName;
                  return CheckStringAgainstOption(file.FullName);

               case FilterType.SubCategories.Hidden:
                  value = "Hidden";
                  return (file.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;

               case FilterType.SubCategories.System:
                  value = "System";
                  return (file.Attributes & FileAttributes.System) == FileAttributes.System;

               case FilterType.SubCategories.ReadOnly:
                  value = "ReadOnly";
                  return (file.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;

               case FilterType.SubCategories.DateModified:
                  value = file.LastWriteTime.ToString();
                  return CheckDateTimeAgainstOption(file.LastWriteTime);

               case FilterType.SubCategories.DateCreated:
                  value = file.CreationTime.ToString();
                  return CheckDateTimeAgainstOption(file.CreationTime);

               case FilterType.SubCategories.Size:
                  value = file.Length.ToString();
                  return CheckLongAgainstOption(file.Length);

               case FilterType.SubCategories.Binary:
                  value = "Binary";
                  return IsBinaryFile(file);

               default:
                  value = string.Empty;
                  return false;
            }
         }

         return false;
      }

      /// <summary>
      /// Deteremines whether the given DirectoryInfo object should be excluded based on the exclusion settings.
      /// </summary>
      /// <param name="dir">DirectoryInfo object</param>
      /// <param name="value">output value for causing filtering</param>
      /// <returns>true if matches a setting, false if not</returns>
      /// <history>
      /// [Curtis_Beard]	   10/31/2014	ADD: exclusions update
      /// </history>
      public bool ShouldExcludeDirectory(DirectoryInfo dir, out string value)
      {
         value = string.Empty;

         if (Enabled && FilterType.Category == FilterType.Categories.Directory)
         {
            switch (FilterType.SubCategory)
            {
               case FilterType.SubCategories.Name:
                  value = dir.Name;
                  return CheckStringAgainstOption(dir.Name);

               case FilterType.SubCategories.Path:
                  value = dir.FullName;
                  return CheckStringAgainstOption(dir.FullName);

               case FilterType.SubCategories.Hidden:
                  value = "Hidden";
                  return (dir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;

               case FilterType.SubCategories.System:
                  value = "System";
                  return (dir.Attributes & FileAttributes.System) == FileAttributes.System;

               case FilterType.SubCategories.DateModified:
                  value = dir.LastWriteTime.ToString();
                  return CheckDateTimeAgainstOption(dir.LastWriteTime);

               case FilterType.SubCategories.DateCreated:
                  value = dir.CreationTime.ToString();
                  return CheckDateTimeAgainstOption(dir.CreationTime);

               default:
                  value = string.Empty;
                  return false;
            }
         }

         return false;
      }

      /// <summary>
      /// Checks whether the given string matches against the value based on exclusion settings.
      /// </summary>
      /// <param name="checkValue">Value to check</param>
      /// <returns>true if matches, false otherwise</returns>
      /// <history>
      /// [Curtis_Beard]	   10/31/2014	ADD: exclusions update
      /// </history>
      private bool CheckStringAgainstOption(string checkValue)
      {
         if (Enabled && FilterType.ValueType == FilterType.ValueTypes.String)
         {
            switch (ValueOption)
            {
               case FilterType.ValueOptions.Equals:
                  return checkValue.Equals(Value, ValueIgnoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture);

               case FilterType.ValueOptions.Contains:
                  return checkValue.IndexOf(Value, ValueIgnoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture) > -1;

               case FilterType.ValueOptions.StartsWith:
                  return checkValue.StartsWith(Value, ValueIgnoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture);

               case FilterType.ValueOptions.EndsWith:
                  return checkValue.EndsWith(Value, ValueIgnoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture);

               case FilterType.ValueOptions.None:
               default:
                  return false;
            }
         }

         return false;
      }

      /// <summary>
      /// Checks whether the given value matches against the value based on exclusion settings.
      /// </summary>
      /// <param name="checkValue">Value to check</param>
      /// <returns>true if matches, false otherwise</returns>
      /// <history>
      /// [Curtis_Beard]	   10/31/2014	ADD: exclusions update
      /// </history>
      private bool CheckLongAgainstOption(long checkValue)
      {
         if (Enabled && (FilterType.ValueType == FilterType.ValueTypes.Long || FilterType.ValueType == libAstroGrep.FilterType.ValueTypes.Size))
         {
            long itemValue = Convert.ToInt64(Value);

            switch (ValueOption)
            {
               case FilterType.ValueOptions.Equals:
                  return checkValue == itemValue;

               case FilterType.ValueOptions.NotEquals:
                  return checkValue != itemValue;

               case FilterType.ValueOptions.GreaterThan:
                  return checkValue > itemValue;

               case FilterType.ValueOptions.GreaterThanEquals:
                  return checkValue >= itemValue;

               case FilterType.ValueOptions.LessThan:
                  return checkValue < itemValue;

               case FilterType.ValueOptions.LessThanEquals:
                  return checkValue <= itemValue;

               default:
                  return false;
            }
         }

         return false;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="checkValue"></param>
      /// <returns></returns>
      /// <history>
      /// [Curtis_Beard]	   10/31/2014	ADD: exclusions update
      /// </history>
      private bool CheckDateTimeAgainstOption(DateTime checkValue)
      {
         if (Enabled && FilterType.ValueType == FilterType.ValueTypes.DateTime)
         {
            DateTime itemValue = Convert.ToDateTime(Value);

            switch (ValueOption)
            {
               case FilterType.ValueOptions.Equals:
                  return checkValue == itemValue;

               case FilterType.ValueOptions.NotEquals:
                  return checkValue != itemValue;

               case FilterType.ValueOptions.GreaterThan:
                  return checkValue > itemValue;

               case FilterType.ValueOptions.GreaterThanEquals:
                  return checkValue >= itemValue;

               case FilterType.ValueOptions.LessThan:
                  return checkValue < itemValue;

               case FilterType.ValueOptions.LessThanEquals:
                  return checkValue <= itemValue;

               default:
                  return false;
            }
         }

         return false;
      }

      /// <summary>
      /// Returns true if file is binary. Algorithm taken from winGrep.
      /// The function scans first 10KB for 0x0000 sequence
      /// and if found, assumes the file to be binary
      /// </summary>
      /// <param name="filePath">Path to a file</param>
      /// <returns>True is file is binary otherwise false</returns>
      /// <history>
      /// [Curtis_Beard]	   10/31/2014	ADD: exclusions update
      /// </history>
      private bool IsBinaryFile(FileInfo file)
      {
         try
         {
            const int MAX_NULL_COUNT = 2;
            byte[] buffer = new byte[1024];
            int count = 0;
            using (FileStream readStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
               count = readStream.Read(buffer, 0, buffer.Length);
            }

            int nullCount = 0;
            for (int i = 0; i < count - 1; i = i + 2)
            {
               if (buffer[i] == 0 && buffer[i + 1] == 0)
               {
                  nullCount++;

                  if (nullCount >= MAX_NULL_COUNT)
                  {
                     return true;
                  }
               }
            }

            return false;
         }
         catch
         {
            return false;
         }
      }

      #region Public Static Methods

      /// <summary>
      /// Creates an instance of an FilterItem object from a string.
      /// </summary>
      /// <param name="value">string to convert to object</param>
      /// <returns>FilterItem object</returns>
      /// <history>
      /// [Curtis_Beard]	   10/31/2014	ADD: exclusions update
      /// </history>
      public static FilterItem FromString(string value)
      {
         string[] values = value.Split(DELIMETER);

         var item = new FilterItem();
         item.FilterType = FilterType.FromString(values[0]);
         item.Value = values[1];
         item.ValueOption = (FilterType.ValueOptions)Enum.Parse(typeof(FilterType.ValueOptions), values[2]);
         item.ValueIgnoreCase = Convert.ToBoolean(values[3]);
         item.ValueSizeOption = values[4];
         item.Enabled = Convert.ToBoolean(values[5]);

         return item;
      }

      /// <summary>
      /// Converts a List of FilterItems to a string.
      /// </summary>
      /// <param name="list">List of FilterItems</param>
      /// <returns>string of filter items</returns>
      /// <history>
      /// [Curtis_Beard]	   10/31/2014	ADD: exclusions update
      /// </history>
      public static string ConvertFilterItemsToString(List<FilterItem> list)
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
      /// Converts the given string to a list of FilterItems.
      /// </summary>
      /// <param name="value">string to convert</param>
      /// <returns>List of FilterItems</returns>
      /// <history>
      /// [Curtis_Beard]	   10/31/2014	ADD: exclusions update
      /// </history>
      public static List<FilterItem> ConvertStringToFilterItems(string value)
      {
         var list = new List<FilterItem>();

         if (!string.IsNullOrEmpty(value))
         {
            var values = value.Split(LIST_DELIMETER);

            foreach (string val in values)
            {
               list.Add(FilterItem.FromString(val));
            }
         }

         return list;
      }
      #endregion
   }
}
