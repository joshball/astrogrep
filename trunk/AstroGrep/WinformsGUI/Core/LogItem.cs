using System;
using System.Collections.Generic;
using System.Text;

namespace AstroGrep.Core
{
   /// <summary>
   /// Class to contain a log item entry (status, exclusion, error).
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
   /// [Curtis_Beard]	   12/06/2012	ADD: 1741735, initial class to help with log items
   /// </history>
   public class LogItem
   {
      /// <summary>
      /// Types of log item messages.
      /// </summary>
      public enum LogItemTypes
      {
         /// <summary>Status messages</summary>
         Status = 0,
         /// <summary>Exclusion messages</summary>
         Exclusion = 1,
         /// <summary>Error messages</summary>
         Error = 2
      }

      /// <summary>The item message type.</summary>
      public LogItemTypes ItemType { get; set; }

      /// <summary>The value of the message.</summary>
      public string Value { get; set; }

      /// <summary>Any extra details for the message.</summary>
      public string Details { get; set; }

      /// <summary>The message timestamp.</summary>
      public DateTime Date { get; private set; }

      /// <summary>
      /// Creates an instance of this class.
      /// </summary>
      /// <param name="type">Item Type</param>
      /// <param name="value">Value</param>
      public LogItem(LogItemTypes type, string value)
      {
         ItemType = type;
         Value = value;
         Details = string.Empty;
         Date = DateTime.Now;
      }

      /// <summary>
      /// Creates an instance of this class.
      /// </summary>
      /// <param name="type">Item Type</param>
      /// <param name="value">Value</param>
      /// <param name="details">Details</param>
      public LogItem(LogItemTypes type, string value, string details)
         : this(type, value)
      {
         Details = details;
      }

      /// <summary>
      /// String representation of this class (Type - Value).
      /// </summary>
      /// <returns>String showing Type - Value</returns>
      public override string ToString()
      {
         return string.Format("{0} - {1}", ItemType.ToString(), Value);
      }
   }
}
