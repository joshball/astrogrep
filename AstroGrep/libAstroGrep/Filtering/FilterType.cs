using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libAstroGrep
{
   /// <summary>
   /// Used to define each FilterItem's type is and what it can do.
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
   public class FilterType
   {
      private const char DELIMETER = '^';

      /// <summary>
      /// The main types for a FilterItem.
      /// </summary>
      public enum Categories
      {
         File,
         Directory
      }

      /// <summary>
      /// The types of categories for a FilterItem's main type.
      /// </summary>
      public enum SubCategories
      {
         Name,
         Path,
         Hidden,
         System,
         DateModified,
         DateCreated,
         Extension,
         Size,
         Binary,
         MinimumHitCount,
         ReadOnly
      }

      /// <summary>
      /// The object types that are supported for a value.
      /// </summary>
      public enum ValueTypes
      {
         Null,
         String,
         DateTime,
         Long,
         Size
      }

      /// <summary>
      /// The options that can be applied against a value.
      /// </summary>
      public enum ValueOptions
      {
         None,
         Equals,
         NotEquals,
         Contains,
         StartsWith,
         EndsWith,
         GreaterThan,
         GreaterThanEquals,
         LessThan,
         LessThanEquals
      }

      private Categories cat = Categories.File;
      private SubCategories subcat = SubCategories.Name;
      private ValueTypes valuetype = ValueTypes.Null;
      private List<ValueOptions> supportedValueOptions = null;
      private bool supportsIgnoreCase = false;
      private bool supportsMulitpleItems = true;

      /// <summary>
      /// Gets the category.
      /// </summary>
      public Categories Category { get { return cat; } }

      /// <summary>
      /// Gets the sub category.
      /// </summary>
      public SubCategories SubCategory { get { return subcat; } }

      /// <summary>
      /// Gets the selected ValueType (type of object the value can be).
      /// </summary>
      public ValueTypes ValueType { get { return valuetype; } }

      /// <summary>
      /// Gets the list of supported ValueOptions (the options that can be applied against the value).
      /// </summary>
      public List<ValueOptions> SupportedValueOptions { get { return supportedValueOptions; } }

      /// <summary>
      /// Gets whether this object can support the ignore case directive.
      /// </summary>
      public bool SupportsIgnoreCase { get { return supportsIgnoreCase; } }

      /// <summary>
      /// Gets whether this object can support having more than 1 instance of this object defined system wide.
      /// </summary>
      public bool SupportsMulitpleItems { get { return supportsMulitpleItems; } }

      /// <summary>
      /// Creates an instance of this class with the required category and sub category.
      /// </summary>
      /// <param name="category">Selected category</param>
      /// <param name="subCategory">Selected sub category</param>
      /// <history>
      /// [Curtis_Beard]	   10/31/2014	ADD: exclusions update
      /// </history>
      public FilterType(Categories category, SubCategories subCategory)
      {
         cat = category;
         subcat = subCategory;

         switch (subcat)
         {
            case SubCategories.Name:
            case SubCategories.Path:
               valuetype = ValueTypes.String;
               supportedValueOptions = new List<ValueOptions>() { ValueOptions.Equals, ValueOptions.Contains, ValueOptions.StartsWith, ValueOptions.EndsWith };
               supportsIgnoreCase = true;
               supportsMulitpleItems = true;
               break;

            case SubCategories.Hidden:
            case SubCategories.System:
            case SubCategories.ReadOnly:
            case SubCategories.Binary:
               valuetype = ValueTypes.Null;
               supportedValueOptions = new List<ValueOptions>() { ValueOptions.None };
               supportsIgnoreCase = false;
               supportsMulitpleItems = false;
               break;

            case SubCategories.DateModified:
            case SubCategories.DateCreated:
               valuetype = ValueTypes.DateTime;
               supportedValueOptions = new List<ValueOptions>() { ValueOptions.Equals, ValueOptions.NotEquals, ValueOptions.GreaterThan, ValueOptions.GreaterThanEquals, ValueOptions.LessThan, ValueOptions.LessThanEquals };
               supportsIgnoreCase = false;
               supportsMulitpleItems = true;
               break;

            case SubCategories.Extension:
               valuetype = ValueTypes.String;
               supportedValueOptions = new List<ValueOptions>() { ValueOptions.None };
               supportsIgnoreCase = false;
               supportsMulitpleItems = true;
               break;

            case SubCategories.Size:
               valuetype = ValueTypes.Size;
               supportedValueOptions = new List<ValueOptions>() { ValueOptions.Equals, ValueOptions.NotEquals, ValueOptions.GreaterThan, ValueOptions.GreaterThanEquals, ValueOptions.LessThan, ValueOptions.LessThanEquals };
               supportsIgnoreCase = false;
               supportsMulitpleItems = true;
               break;

            case SubCategories.MinimumHitCount:
               valuetype = ValueTypes.Long;
               supportedValueOptions = new List<ValueOptions>() { ValueOptions.None };
               supportsIgnoreCase = false;
               supportsMulitpleItems = false;
               break;
         }
      }

      /// <summary>
      /// Converts this class to its string form.
      /// </summary>
      /// <returns>string representation of this class</returns>
      /// <history>
      /// [Curtis_Beard]	   10/31/2014	ADD: exclusions update
      /// </history>
      public override string ToString()
      {
         return string.Format("{1}{0}{2}", DELIMETER, Category.ToString(), SubCategory.ToString());
      }

      /// <summary>
      /// Converts string form of this class to an actual FilterType.
      /// </summary>
      /// <param name="value">string form of a FilterType</param>
      /// <returns>FilterType object</returns>
      /// <history>
      /// [Curtis_Beard]	   10/31/2014	ADD: exclusions update
      /// </history>
      public static FilterType FromString(string value)
      {
         var values = value.Split(DELIMETER);
         Categories cat = (Categories)Enum.Parse(typeof(Categories), values[0]);
         SubCategories subcat = (SubCategories)Enum.Parse(typeof(SubCategories), values[1]);

         return new FilterType(cat, subcat);
      }

      /// <summary>
      /// Creates the default FilterTypes that are available for user selection.
      /// </summary>
      /// <returns>List of FilterTypes that are available for selection</returns>
      /// <history>
      /// [Curtis_Beard]	   10/31/2014	ADD: exclusions update
      /// </history>
      public static List<FilterType> GetDefaultFilterTypes()
      {
         List<FilterType> defaultTypes = new List<FilterType>();

         // File
         defaultTypes.Add(new FilterType(Categories.File, SubCategories.Extension));
         defaultTypes.Add(new FilterType(Categories.File, SubCategories.Name));
         defaultTypes.Add(new FilterType(Categories.File, SubCategories.Path));
         defaultTypes.Add(new FilterType(Categories.File, SubCategories.System));
         defaultTypes.Add(new FilterType(Categories.File, SubCategories.Hidden));
         defaultTypes.Add(new FilterType(Categories.File, SubCategories.Binary));
         defaultTypes.Add(new FilterType(Categories.File, SubCategories.ReadOnly));
         defaultTypes.Add(new FilterType(Categories.File, SubCategories.DateModified));
         defaultTypes.Add(new FilterType(Categories.File, SubCategories.DateCreated));
         defaultTypes.Add(new FilterType(Categories.File, SubCategories.Size));
         defaultTypes.Add(new FilterType(Categories.File, SubCategories.MinimumHitCount));

         // Directory
         defaultTypes.Add(new FilterType(Categories.Directory, SubCategories.Name));
         defaultTypes.Add(new FilterType(Categories.Directory, SubCategories.Path));
         defaultTypes.Add(new FilterType(Categories.Directory, SubCategories.System));
         defaultTypes.Add(new FilterType(Categories.Directory, SubCategories.Hidden));
         defaultTypes.Add(new FilterType(Categories.Directory, SubCategories.DateModified));
         defaultTypes.Add(new FilterType(Categories.Directory, SubCategories.DateCreated));

         return defaultTypes;
      }
   }
}
