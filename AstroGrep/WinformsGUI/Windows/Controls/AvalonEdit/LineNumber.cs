using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroGrep.Windows.Controls
{
   /// <summary>
   /// 
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
   /// [Curtis_Beard]	   04/08/2015	ADD: switch from Rich Text Box to AvalonEdit
   /// </history>
   public class LineNumber
   {
      /// <summary>Line number</summary>
      public int Number { get; set; }

      /// <summary>Determines if current line has a match</summary>
      public bool HasMatch { get; set; }

      /// <summary>The full file path</summary>
      public string FileFullName { get; set; }

      /// <summary>Column number to first match</summary>
      public int ColumnNumber { get; set; }

      /// <summary>
      /// Creates an instance of this class.
      /// </summary>
      public LineNumber()
      {
         Number = -1;
         HasMatch = false;
         FileFullName = string.Empty;
         ColumnNumber = 1;
      }
   }
}
