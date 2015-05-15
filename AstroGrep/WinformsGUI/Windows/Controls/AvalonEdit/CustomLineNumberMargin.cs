using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;
using ICSharpCode.AvalonEdit.Utils;

using libAstroGrep;

namespace AstroGrep.Windows.Controls
{
   /// <summary>
   /// Handles custom line numbering in margin when given specific line numbers.
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
   /// 
   /// 
   /// 
   /// If List is equal to number of total lines, then use each specified, otherwise
   /// use only given LineNumbers that match to the given line number displayed.
   /// </remarks>
   /// <history>
   /// 
   /// </history>
   public class CustomLineNumberMargin : LineNumberMargin
   {
      /// <summary>
      /// 
      /// </summary>
      public List<LineNumber> LineNumbers { get; set; }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="availableSize"></param>
      /// <returns></returns>
      protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
      {
         //return base.MeasureOverride(availableSize);

         int numberLength = 2;
         if (LineNumbers != null && LineNumbers.Count == this.Document.LineCount)
         {
            numberLength = (from n in LineNumbers where n.Number > -1 select n.Number).Max().ToString().Length;
         }
         else
         {
            numberLength = maxLineNumberLength;
         }

         if (numberLength < 2)
         {
            numberLength = 2;
         }

         typeface = CreateTypeface(this);
         emSize = (double)GetValue(TextBlock.FontSizeProperty);

         FormattedText text = CreateFormattedText(
            this,
            new string('9', numberLength),
            typeface,
            emSize,
            (Brush)GetValue(Control.ForegroundProperty)
         );
         return new Size(text.Width, 0);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="drawingContext"></param>
      protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
      {
         //base.OnRender(drawingContext);

         TextView textView = this.TextView;
         Size renderSize = this.RenderSize;
         if (textView != null && textView.VisualLinesValid)
         {
            var foreground = (Brush)GetValue(Control.ForegroundProperty);  // non-match line
            var matchForeground = (Brush)GetValue(Control.BackgroundProperty); // match line

            foreach (VisualLine line in textView.VisualLines)
            {
               int lineNumber = line.FirstDocumentLine.LineNumber;
               bool isMatch = false;
               if (LineNumbers != null)
               {
                  // all line numbers are specified
                  if (LineNumbers.Count == this.Document.LineCount)
                  {
                     lineNumber = -1;

                     if (line.FirstDocumentLine.LineNumber - 1 < LineNumbers.Count)
                     {
                        lineNumber = LineNumbers[line.FirstDocumentLine.LineNumber - 1].Number;
                        isMatch = LineNumbers[line.FirstDocumentLine.LineNumber - 1].HasMatch;
                     }
                  }
                  else
                  {
                     // look for only a given line number since not all are specified
                     LineNumber lineNum = (from n in LineNumbers where n.Number == line.FirstDocumentLine.LineNumber select n).FirstOrDefault();
                     if (lineNum != null)
                     {
                        lineNumber = lineNum.Number;
                        isMatch = lineNum.HasMatch;
                     }
                  }
               }

               FormattedText text = CreateFormattedText(
                  this,
                  lineNumber > -1 
                     ? lineNumber.ToString(CultureInfo.CurrentCulture)
                     : string.Empty,
                     typeface, emSize, isMatch ? matchForeground : foreground
               );
               drawingContext.DrawText(text, new Point(renderSize.Width - text.Width,
                                                       line.VisualTop - textView.VerticalOffset));
            }
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="element"></param>
      /// <param name="text"></param>
      /// <param name="typeface"></param>
      /// <param name="emSize"></param>
      /// <param name="foreground"></param>
      /// <returns></returns>
      private FormattedText CreateFormattedText(FrameworkElement element, string text, Typeface typeface, double? emSize, Brush foreground)
      {
         return new FormattedText(
            text,
            CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight,
            typeface,
            emSize.Value,
            foreground,
            null,
            TextOptions.GetTextFormattingMode(element)
         );
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="fe"></param>
      /// <returns></returns>
      private Typeface CreateTypeface(FrameworkElement fe)
      {
         return new Typeface((FontFamily)fe.GetValue(TextBlock.FontFamilyProperty),
                             (FontStyle)fe.GetValue(TextBlock.FontStyleProperty),
                             (FontWeight)fe.GetValue(TextBlock.FontWeightProperty),
                             (FontStretch)fe.GetValue(TextBlock.FontStretchProperty));
      }
   }
}
