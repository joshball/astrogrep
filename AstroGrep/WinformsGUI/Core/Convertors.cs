using System;
using System.Collections.Generic;
using System.Text;

namespace AstroGrep.Core
{
   /// <summary>
   /// Contains common methods to convert to string/from string.
   /// </summary>
   /// <remarks>
   ///   AstroGrep File Searching Utility. Written by Theodore L. Ward
   ///   Copyright (C) 2002 AstroComma Incorporated.
   ///   
   ///   This program is free software; you can redistribute it and/or
   ///   modify it under the terms of the GNU General public License
   ///   as published by the Free Software Foundation; either version 2
   ///   of the License, or (at your option) any later version.
   ///   
   ///   This program is distributed in the hope that it will be useful,
   ///   but WITHOUT ANY WARRANTY; without even the implied warranty of
   ///   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   ///   GNU General public License for more details.
   ///   
   ///   You should have received a copy of the GNU General public License
   ///   along with this program; if not, write to the Free Software
   ///   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
   /// 
   ///   The author may be contacted at:
   ///   ted@astrocomma.com or curtismbeard@gmail.com
   /// </remarks>
   /// <history>
   /// [Curtis_Beard]      11/07/2012  Initial
   /// </history>
   public class Convertors
   {
      /// <summary>
      /// Retrieves all the ComboBox entries as a string.
      /// </summary>
      /// <param name="combo">ComboBox</param>
      /// <returns>string of entries</returns>
      /// <history>
      /// [Curtis_Beard]		11/03/2006	Created
      /// </history>
      public static string GetComboBoxEntriesAsString(System.Windows.Forms.ComboBox combo)
      {
         string[] entries = new string[combo.Items.Count];

         for (int i = 0; i < combo.Items.Count; i++)
         {
            entries[i] = combo.Items[i].ToString();
         }

         return string.Join(Constants.SEARCH_ENTRIES_SEPARATOR, entries);
      }

      /// <summary>
      /// Retrieves the values as an array of strings.
      /// </summary>
      /// <param name="values">ComboBox values as a string</param>
      /// <returns>Array of strings</returns>
      /// <history>
      /// [Curtis_Beard]		11/03/2006	Created
      /// </history>
      public static string[] GetComboBoxEntriesFromString(string values)
      {
         string[] entries = Core.Common.SplitByString(values, Constants.SEARCH_ENTRIES_SEPARATOR);

         return entries;
      }

      /// <summary>
      /// Converts a Color to a string.
      /// </summary>
      /// <param name="color">Color</param>
      /// <returns>color values as a string</returns>
      /// <history>
      /// [Curtis_Beard]		11/03/2006	Created
      /// </history>
      public static string ConvertColorToString(System.Drawing.Color color)
      {
         return string.Format("{0}{4}{1}{4}{2}{4}{3}", color.R.ToString(), color.G.ToString(), color.B.ToString(), color.A.ToString(), Constants.COLOR_SEPARATOR);
      }

      /// <summary>
      /// Converts a string to a Color.
      /// </summary>
      /// <param name="color">color values as a string</param>
      /// <returns>Color</returns>
      /// <history>
      /// [Curtis_Beard]		11/03/2006	Created
      /// </history>
      public static System.Drawing.Color ConvertStringToColor(string color)
      {
         string[] rgba = color.Split(char.Parse(Constants.COLOR_SEPARATOR));

         return System.Drawing.Color.FromArgb(byte.Parse(rgba[3]), byte.Parse(rgba[0]), byte.Parse(rgba[1]), byte.Parse(rgba[2]));
      }

      /// <summary>
      /// Converts a string to a SolidColorBrush.
      /// </summary>
      /// <param name="color">color values as a string</param>
      /// <returns>System.Windows.Media.SolidColorBrush</returns>
      /// <history>
      /// [Curtis_Beard]		04/15/2015	Created
      /// </history>
      public static System.Windows.Media.SolidColorBrush ConvertStringToSolidColorBrush(string color)
      {
         System.Drawing.Color dColor = ConvertStringToColor(color);

         return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(dColor.R, dColor.G, dColor.B));
      }

      /// <summary>
      /// Converts a font to a string.
      /// </summary>
      /// <param name="font">Font</param>
      /// <returns>font values as a string</returns>
      /// <history>
      /// [Curtis_Beard]	   02/24/2012	CHG: 3488321, ability to change results font
      /// [Curtis_Beard]		10/22/2012	FIX: 36, use invariant culture to always have same float decimal separator
      /// </history>
      public static string ConvertFontToString(System.Drawing.Font font)
      {
         return string.Format("{0}{3}{1}{3}{2}", font.Name, font.Size.ToString(System.Globalization.CultureInfo.InvariantCulture), font.Style.ToString(), Constants.FONT_SEPARATOR);
      }

      /// <summary>
      /// Converts a string to a Font.
      /// </summary>
      /// <param name="font">font values as a string</param>
      /// <returns>Font</returns>
      /// <history>
      /// [Curtis_Beard]	   02/24/2012	CHG: 3488321, ability to change results font
      /// [Curtis_Beard]		10/22/2012	FIX: 36, use invariant culture to always have same float decimal separator
      /// </history>
      public static System.Drawing.Font ConvertStringToFont(string font)
      {
         string[] fontValues = Core.Common.SplitByString(font, Constants.FONT_SEPARATOR);

         return new System.Drawing.Font(fontValues[0], float.Parse(fontValues[1], System.Globalization.CultureInfo.InvariantCulture), (System.Drawing.FontStyle)Enum.Parse(typeof(System.Drawing.FontStyle), fontValues[2], true));
      }

      /// <summary>
      /// Converts given file size in bytes as string to the display type as a string.
      /// </summary>
      /// <param name="bytes">File size in bytes</param>
      /// <param name="displayType">byte,kb,mb,gb</param>
      /// <returns>file size in given display type</returns>
      /// <history>
      /// [Curtis_Beard]        11/17/2014  Initial
      /// </history>
      public static string ConvertFileSizeForDisplay(string bytes, string displayType)
      {
         // convert bytes value to selected display size
         long value = long.Parse(bytes);
         switch (displayType.ToLower())
         {
            case "byte":
               break;
            case "kb":
               value = value / 1024;
               break;
            case "mb":
               value = value / (1024 * 1024);
               break;
            case "gb":
               value = value / (1024 * 1024 * 1024);
               break;
         }

         return value.ToString();
      }

      /// <summary>
      /// Converts given file size to long for use in comparison of file sizes.
      /// </summary>
      /// <param name="textValue">TextBox value entered by user</param>
      /// <param name="sizeType">The selected size type (byte,kb,mb,gb)</param>
      /// <param name="defaultValue">The default value</param>
      /// <returns>long representing number of bytes user selected</returns>
      /// <history>
      /// [Curtis_Beard]        02/09/2012  ADD: 3424156, size drop down selection
      /// </history>
      public static long ConvertFileSizeFromDisplay(string textValue, string sizeType, long defaultValue)
      {
         long retVal = defaultValue;

         double size;
         if (double.TryParse(textValue, out size))
         {
            switch (sizeType.ToLower())
            {
               case "byte":
                  break;
               case "kb":
                  size = size * 1024;
                  break;
               case "mb":
                  size = size * 1024 * 1024;
                  break;
               case "gb":
                  size = size * 1024 * 1024 * 1024;
                  break;
            }

            retVal = (long)size;
         }

         return retVal;
      }
   }
}
