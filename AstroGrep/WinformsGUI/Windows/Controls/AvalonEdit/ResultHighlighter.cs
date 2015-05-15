using System;
using System.Linq;
using System.Windows.Media;

using libAstroGrep;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;

namespace AstroGrep.Windows.Controls
{
   /// <summary>
   /// Handles highlight for a single file/MatchResult.
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
   public class ResultHighlighter : DocumentColorizingTransformer
   {
      private MatchResult match;
      private bool removeWhiteSpace = false;
      private bool showingFullFile = false;
      private SolidColorBrush matchForeground = new SolidColorBrush(Colors.White);
      private SolidColorBrush matchBackground = new SolidColorBrush(Color.FromRgb(251, 127, 6));
      private SolidColorBrush nonmatchForeground = new SolidColorBrush(Color.FromRgb(192, 192, 192));

      /// <summary>
      /// Creates an instance of this class.
      /// </summary>
      /// <param name="match">The current MatchResult</param>
      /// <param name="removeWhiteSpace">Determines if leading white space was removed</param>
      /// <history>
      /// [Curtis_Beard]	   04/08/2015	ADD: switch from Rich Text Box to AvalonEdit
      /// </history>
      public ResultHighlighter(MatchResult match, bool removeWhiteSpace)
      {
         this.match = match;
         this.removeWhiteSpace = removeWhiteSpace;
      }

      /// <summary>
      /// Creates an instance of this class.
      /// </summary>
      /// <param name="match">The current MatchResult</param>
      /// <param name="removeWhiteSpace">Determines if leading white space was removed</param>
      /// <param name="showingFullFile">Determines if showing full file contents or just matches</param>
      /// <history>
      /// [Curtis_Beard]	   04/08/2015	ADD: switch from Rich Text Box to AvalonEdit
      /// </history>
      public ResultHighlighter(MatchResult match, bool removeWhiteSpace, bool showingFullFile)
         : this(match, removeWhiteSpace)
      {
         this.showingFullFile = showingFullFile;
      }

      /// <summary>
      /// The match's foreground color.
      /// </summary>
      public SolidColorBrush MatchForeground
      {
         get { return matchForeground; }
         set { matchForeground = value; }
      }

      /// <summary>
      /// The match's background color.
      /// </summary>
      public SolidColorBrush MatchBackground
      {
         get { return matchBackground; }
         set { matchBackground = value; }
      }

      /// <summary>
      /// The non-match's foreground color.
      /// </summary>
      public SolidColorBrush NonMatchForeground
      {
         get { return nonmatchForeground; }
         set { nonmatchForeground = value; }
      }

      /// <summary>
      /// Applies the specified colors to the given line.
      /// </summary>
      /// <param name="line">Current DocumentLine from AvalonEdit</param>
      /// <history>
      /// [Curtis_Beard]	   04/08/2015	ADD: switch from Rich Text Box to AvalonEdit
      /// </history>
      protected override void ColorizeLine(DocumentLine line)
      {
         int lineStartOffset = line.Offset;
         string text = CurrentContext.Document.GetText(line);
         if (match == null || match.Matches == null || match.Matches.Count == 0 || string.IsNullOrEmpty(text))
            return;

         int lineNumber = line.LineNumber; // 1 based

         // lines in grep are 0 based array
         MatchResultLine matchLine = null;
         if (showingFullFile)
         {
            matchLine = (from m in match.Matches where m.LineNumber == lineNumber select m).FirstOrDefault();
         }
         else
         {
            matchLine = lineNumber - 1 < match.Matches.Count ? match.Matches[lineNumber - 1] : null;
         }

         string contents = matchLine != null ? matchLine.Line : string.Empty;
         bool isHit = matchLine != null ? matchLine.HasMatch : false;

         try
         {
            if (isHit && !string.IsNullOrEmpty(contents))
            {
               int trimOffset = 0;
               if (removeWhiteSpace)
               {
                  trimOffset = contents.Length - contents.TrimStart().Length;
               }

               for (int i = 0; i < matchLine.Matches.Count; i++)
               {
                  int startPosition = matchLine.Matches[i].StartPosition;
                  int length = matchLine.Matches[i].Length;

                  base.ChangeLinePart(
                            lineStartOffset + (startPosition - trimOffset), // startOffset
                            lineStartOffset + (startPosition - trimOffset) + length, // endOffset
                            (VisualLineElement element) =>
                            {
                               // highlight match
                               element.TextRunProperties.SetForegroundBrush(MatchForeground);
                               element.TextRunProperties.SetBackgroundBrush(MatchBackground);
                            });
               }
            }
            else if (!isHit && !showingFullFile)
            {
               base.ChangeLinePart(
                  lineStartOffset, // startOffset
                  lineStartOffset + line.Length, // endOffset
                  (VisualLineElement element) =>
                  {
                     // all non-matched lines are grayed out
                     element.TextRunProperties.SetForegroundBrush(NonMatchForeground);
                  });
            }
         }
         catch
         { }
      }
   }
}
