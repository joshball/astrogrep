using System;
using System.Collections.Generic;
using System.Text;

namespace AstroGrep.Output
{
    /// <summary>
    /// Helper routines for building JSON objects.
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
    /// [Curtis_Beard]      10/30/2012	Created
    /// </history>
    public class JSONHelper
    {
        /// <summary>
        /// Evaluates all characters in a string and returns a new string,
        /// properly formatted for JSON compliance and bounded by double-quotes.
        /// </summary>
        /// <param name="text">string to be evaluated</param>
        /// <returns>new string, in JSON-compliant form</returns>
        public static string ToJSONString(string text)
        {
            char[] charArray = text.ToCharArray();
            List<string> output = new List<string>();
            foreach (char c in charArray)
            {
                if (((int)c) == 8)              //Backspace
                    output.Add("\\b");
                else if (((int)c) == 9)         //Horizontal tab
                    output.Add("\\t");
                else if (((int)c) == 10)        //Newline
                    output.Add("\\n");
                else if (((int)c) == 12)        //Formfeed
                    output.Add("\\f");
                else if (((int)c) == 13)        //Carriage return
                    output.Add("\\n");
                else if (((int)c) == 34)        //Double-quotes (")
                    output.Add("\\" + c.ToString());
                //else if (((int)c) == 44)        //Comma (,)
                //    output.Add("\\" + c.ToString());
                else if (((int)c) == 47)        //Solidus   (/)
                    output.Add("\\" + c.ToString());
                else if (((int)c) == 92)        //Reverse solidus   (\)
                    output.Add("\\" + c.ToString());
                else if (((int)c) > 31)
                    output.Add(c.ToString());
                //TODO: add support for hexadecimal
            }
            return "\"" + string.Join("", output.ToArray()) + "\"";
        }

        /// <summary>
        /// Evaluates the boolean value and returns a new string,
        /// properly formatted for JSON compliance.
        /// </summary>
        /// <param name="value">boolean to be evaluated</param>
        /// <returns>new string, in JSON-compliant form</returns>
        public static string ToJSONString(bool value)
        {
            return value.ToString().ToLower();
        }

        /// <summary>
        /// Evaluates the double value and returns a new string,
        /// properly formatted for JSON compliance.
        /// </summary>
        /// <param name="value">boolean to be evaluated</param>
        /// <returns>new string, in JSON-compliant form</returns>
        public static string ToJSONString(double value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Evaluates the decimal value and returns a new string,
        /// properly formatted for JSON compliance.
        /// </summary>
        /// <param name="value">boolean to be evaluated</param>
        /// <returns>new string, in JSON-compliant form</returns>
        public static string ToJSONString(decimal value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Evaluates the long value and returns a new string,
        /// properly formatted for JSON compliance.
        /// </summary>
        /// <param name="value">boolean to be evaluated</param>
        /// <returns>new string, in JSON-compliant form</returns>
        public static string ToJSONString(long value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Evaluates the int value and returns a new string,
        /// properly formatted for JSON compliance.
        /// </summary>
        /// <param name="value">boolean to be evaluated</param>
        /// <returns>new string, in JSON-compliant form</returns>
        public static string ToJSONString(int value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Evaluates the single value and returns a new string,
        /// properly formatted for JSON compliance.
        /// </summary>
        /// <param name="value">boolean to be evaluated</param>
        /// <returns>new string, in JSON-compliant form</returns>
        public static string ToJSONString(Single value)
        {
            return value.ToString("E");
        }

        /// <summary>
        /// Evaluates the byte value and returns a new string,
        /// properly formatted for JSON compliance.
        /// </summary>
        /// <param name="value">boolean to be evaluated</param>
        /// <returns>new string, in JSON-compliant form</returns>
        public static string ToJSONString(byte value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Evaluates the DateTime value and returns a new string,
        /// properly formatted for JSON compliance.
        /// </summary>
        /// <param name="value">boolean to be evaluated</param>
        /// <returns>new string, in JSON-compliant form</returns>
        public static string ToJSONString(DateTime value)
        {
            //create Timespan by subtracting the value provided from the Unix Epoch
            TimeSpan span = (value.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime());

            //the total seconds (which is a Unix timestamp)
            double seconds = (double)span.TotalSeconds;

            return string.Format("\\/Date({0})\\/", seconds);
        }

        /// <summary>
        /// Evaluates the array's values and returns a new string,
        /// properly formatted for JSON compliance.
        /// </summary>
        /// <param name="arr">Array to be evaluated</param>
        /// <returns>new string, in JSON-compliant form</returns>
        public static string ToJSONString(Array arr)
        {
            List<string> values = new List<string>();

            if (arr != null)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    object value = arr.GetValue(i);
                    values.Add(GetValueAsJSON(value));
                }
            }

            return string.Format("[{0}]", string.Join(",", values.ToArray()));
        }

        /// <summary>
        /// Determines the type of the object passed in and 
        /// converts it to a JSON compliant string.
        /// </summary>
        /// <param name="value">object to evaluate</param>
        /// <returns>new string, in JSON-compliant form</returns>
        private static string GetValueAsJSON(object value)
        {
            string output = string.Empty;
            Type valueType = value.GetType();

            if (valueType == typeof(System.String))
            {
                output = ToJSONString(Convert.ToString(value));
            }
            else if (valueType == typeof(System.Boolean))
            {
                output = ToJSONString(Convert.ToBoolean(value));
            }
            else if (valueType == typeof(System.Enum))
            {
                output = ToJSONString(Enum.GetName(valueType, value));
            }
            else if (valueType == typeof(System.Single))
            {
                output = ToJSONString(Convert.ToSingle(value));
            }
            else if (valueType == typeof(System.Decimal))
            {
                output = ToJSONString(Convert.ToDecimal(value));
            }
            else if (valueType == typeof(System.Double))
            {
                output = ToJSONString(Convert.ToDouble(value));
            }
            else if (valueType == typeof(System.Int32))
            {
                output = ToJSONString(Convert.ToInt32(value));
            }
            else if (valueType == typeof(System.Byte))
            {
                output = ToJSONString(Convert.ToByte(value));
            }
            else if (valueType.IsArray)
            {
                output = ToJSONString((Array)value);
            }
            else
            {
                output = value.ToString();
            }

            return output;
        }
    }
}
