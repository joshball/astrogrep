using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libAstroGrep
{
   /// <summary>
   /// 
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
   /// [Curtis_Beard]      03/31/2015	ADD: rework Grep/Matches
   /// </history>
   public class MatchResultLine
   {
      /// <summary>Current line</summary>
      public string Line { get; set; }

      /// <summary>Current line number</summary>
      public int LineNumber { get; set; }

      /// <summary>Current column number</summary>
      public int ColumnNumber { get; set; }

      /// <summary>Determines if this line has a match within it</summary>
      public bool HasMatch { get; set; }

      /// <summary>List of line matches</summary>
      public List<MatchResultLineMatch> Matches { get; set; }

      /// <summary>
      /// Initializes this class.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      03/31/2015	ADD: rework Grep/Matches
      /// </history>
      public MatchResultLine()
      {
         Line = string.Empty;
         LineNumber = 1;
         ColumnNumber = 1;
         HasMatch = false;
         Matches = new List<MatchResultLineMatch>();
      }
   }
}
