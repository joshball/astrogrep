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
   public class MatchResultLineMatch
   {
      /// <summary>Start position of found match in this line</summary>
      public int StartPosition { get; set; }

      /// <summary>Length of found match in this line</summary>
      public int Length { get; set; }

      /// <summary>
      /// Creates an instance of a MatchResultLineMatch.
      /// </summary>
      public MatchResultLineMatch()
      {
         StartPosition = -1;
         Length = 0;
      }

      /// <summary>
      /// Creates an instance of a MatchResultLineMatch with a given start position and length.
      /// </summary>
      /// <param name="startPosition">Start position of found match in this line</param>
      /// <param name="length">Length of found match in this line</param>
      public MatchResultLineMatch(int startPosition, int length)
      {
         StartPosition = startPosition;
         Length = length;
      }
   }
}
