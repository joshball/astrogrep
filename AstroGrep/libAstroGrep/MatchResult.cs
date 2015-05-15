using System;
using System.Collections.Generic;
using System.IO;
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
   public class MatchResult
   {
      private FileInfo file = null;
      private Encoding detectedEncoding = null;
      private List<MatchResultLine> matches = new List<MatchResultLine>();
      private bool fromPlugin = false;

      /// <summary>
      /// Gets/Sets the current FileInfo for this MatchResult.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   03/31/2015	Created
      /// </history>
      public FileInfo File
      {
         get { return file; }
         set { file = value; }
      }

      /// <summary>
      /// Gets/Sets the Index of the hit in the collection
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      09/09/2005	Created
      /// </history>
      public int Index { get; set; }

      /// <summary>
      /// Gets the total hit count in the object
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   11/21/2005	Created
      /// </history>
      public int HitCount { get; private set; }

      /// <summary>
      /// Gets/Sets the detected file encoding.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   12/01/2014	Created
      /// </history>
      public Encoding DetectedEncoding
      {
         get { return detectedEncoding; }
         set { detectedEncoding = value; }
      }

      /// <summary>
      /// Gets/Sets all the MatchResultLines for this MatchResult.
      /// </summary>
      public List<MatchResultLine> Matches
      {
         get { return matches; }
         set { matches = value; }
      }

      /// <summary>
      /// Gets/Sets whether this MatchResult is from a plugin.
      /// </summary>
      public bool FromPlugin
      {
         get { return fromPlugin; }
         set { fromPlugin = value; }
      }

      /// <summary>
      /// Initializes this MatchResult with the current FileInfo.
      /// </summary>
      /// <param name="file">Current FileInfo</param>
      /// <history>
      /// [Curtis_Beard]	   03/31/2015	Created
      /// </history>
      public MatchResult(FileInfo file)
      {
         this.File = file;
         HitCount = 0;
      }

      /// <summary>
      /// Updates the total hit count
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   11/21/2005	Created
      /// </history>
      public void SetHitCount()
      {
         SetHitCount(1);
      }

      /// <summary>
      /// Updates the total hit count
      /// </summary>
      /// <param name="count">Value to add count to total</param>
      /// <history>
      /// [Curtis_Beard]	   11/21/2005	Created
      /// </history>
      public void SetHitCount(int count)
      {
         HitCount += count;
      }

      /// <summary>
      /// Retrieves the first MatchResultLine from the MatchResult list.
      /// </summary>
      /// <returns>First MatchResultLine that contains a match, otherwise null</returns>
      /// <history>
      /// [Curtis_Beard]	   03/31/2015	Created
      /// </history>
      public MatchResultLine GetFirstMatch()
      {
         return (from m in matches where m.HasMatch select m).FirstOrDefault();
      }
   }
}
