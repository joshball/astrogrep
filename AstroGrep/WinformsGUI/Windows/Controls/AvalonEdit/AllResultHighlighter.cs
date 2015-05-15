using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

using libAstroGrep;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;

namespace AstroGrep.Windows.Controls
{
   /// <summary>
   /// Handles highlighting of all results when displayed.
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
   public class AllResultHighlighter : DocumentColorizingTransformer
   {
      private IList<MatchResult> matches;
      private bool removeWhiteSpace = false;
      private SolidColorBrush matchForeground = new SolidColorBrush(Colors.White);
      private SolidColorBrush matchBackground = new SolidColorBrush(Color.FromRgb(251, 127, 6));
      private SolidColorBrush nonmatchForeground = new SolidColorBrush(Color.FromRgb(192, 192, 192));

      /// <summary>
      /// Creates an instance of this class.
      /// </summary>
      /// <param name="matches">List of all matches</param>
      /// <param name="removeWhiteSpace">Determines if leading white space was removed</param>
      /// <history>
      /// [Curtis_Beard]	   04/08/2015	ADD: switch from Rich Text Box to AvalonEdit
      /// </history>
      public AllResultHighlighter(IList<MatchResult> matches, bool removeWhiteSpace)
      {
         this.matches = matches;
         this.removeWhiteSpace = removeWhiteSpace;
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

         if (matches == null || matches.Count == 0  || string.IsNullOrEmpty(text))
            return;

         // find what type of line this is, either the file path or a result line
         bool isFileName = false;
         MatchResultLine matchLine = null;
         foreach (MatchResult result in matches)
         {
            if (result.File.FullName.Equals(text, StringComparison.OrdinalIgnoreCase))
            {
               isFileName = true;
               break;
            }
            else
            {
               foreach (var matchResultLine in result.Matches)
               {
                  string lineText = matchResultLine.Line;
                  if(removeWhiteSpace)
                  {
                     lineText = lineText.TrimStart();
                  }

                  if (lineText.Equals(text))
                  {
                     matchLine = matchResultLine;
                     break;
                  }
               }
            }
         }

         try
         {
            if (isFileName)
            {
               base.ChangeLinePart(
                  lineStartOffset, // startOffset
                  lineStartOffset + line.Length, // endOffset
                  (VisualLineElement element) =>
                  {
                     // bold current typeface for file name display
                     Typeface tf = element.TextRunProperties.Typeface;
                     var tfNew = new Typeface(tf.FontFamily, tf.Style, System.Windows.FontWeights.Bold, tf.Stretch);
                     element.TextRunProperties.SetTypeface(tfNew);
                  });
            }
            else
            {
               if (matchLine != null && matchLine.HasMatch)
               {
                  int trimOffset = 0;
                  if (removeWhiteSpace)
                  {
                     trimOffset = matchLine.Line.Length - matchLine.Line.TrimStart().Length;
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
               else
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
         }
         catch
         { }
      }
   }
}
