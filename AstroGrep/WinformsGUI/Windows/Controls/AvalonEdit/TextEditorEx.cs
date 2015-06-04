using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

using ICSharpCode.AvalonEdit;

namespace AstroGrep.Windows.Controls
{
   using TextEditor = ICSharpCode.AvalonEdit.TextEditor;

   /// <summary>
   /// A custom implemenation of the TextEditor control for use with AstroGrep providing a custom line numbering margin.
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
   /// [Curtis_Beard]		04/08/2015	ADD: update RichTextBox to AvalonEdit
   /// [Curtis_Beard]	   05/11/2015	CHG: zoom for TextEditor control
   /// </history>
   public class TextEditorEx : TextEditor
   {
      #region Declarations

      private bool showLineNumbers = true;
      private CustomLineNumberMargin customMargin = new CustomLineNumberMargin();
      private static readonly object dottedLineTag = new Object();
      private bool hasCustomMargin = false;
      private System.Windows.Point rightClickPosition = new System.Windows.Point();
      private Brush lineNumbersMatchForeground = new SolidColorBrush(Colors.Black);

      private bool zoomIsCtrlKey = false;
      private double zoomDefault = 0;
      private double zoomMaximum = 0;
      private double zoomMinimum = 0;

      #endregion

      /// <summary>
      /// Setup a custom TextEditor control with custom line numbering in the left margin.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		04/08/2015	ADD: update RichTextBox to AvalonEdit
      /// [Curtis_Beard]	   05/11/2015	CHG: zoom for TextEditor control
      /// </history>
      public TextEditorEx()
         : base()
      {
         base.IsReadOnly = true;
         base.Options.EnableHyperlinks = false;
         base.Options.EnableEmailHyperlinks = false;
         base.ShowLineNumbers = false;
         base.PreviewMouseRightButtonDown += TextEditorEx_PreviewMouseRightButtonDown;
         base.PreviewMouseWheel += TextEditorEx_PreviewMouseWheel;
         base.PreviewKeyDown += TextEditorEx_PreviewKeyDown;
         base.PreviewKeyUp += TextEditorEx_PreviewKeyUp;

         InitCustomMargin();

         // set initial font size to calculate zoom values
         FontSize = base.FontSize;
      }

      #region Public Properties

      /// <summary>
      /// Specifies whether line numbers are shown on the left to the text view.
      /// </summary>
      public new bool ShowLineNumbers
      {
         get
         {
            return showLineNumbers;
         }
         set
         {
            showLineNumbers = value;
            if (value)
            {
               InitCustomMargin();
            }
            else
            {
               ClearCustomMargin();
            }
         }
      }

      /// <summary>
      /// Gets/Sets the Brush used for displaying the foreground color of line numbers.
      /// </summary>
      public new Brush LineNumbersForeground
      {
         get
         {
            return base.LineNumbersForeground;
         }
         set
         {
            base.LineNumbersForeground = value;

            customMargin.SetBinding(System.Windows.Controls.Control.ForegroundProperty,
                                new System.Windows.Data.Binding("LineNumbersForeground")
                                {
                                   Source = this
                                });
         }
      }

      /// <summary>
      /// Gets/Sets/ the Brush used for displaying the foreground color of matching line numbers.
      /// </summary>
      public Brush LineNumbersMatchForeground
      {
         get
         {
            return lineNumbersMatchForeground;
         }
         set
         {
            lineNumbersMatchForeground = value;

            customMargin.SetBinding(System.Windows.Controls.Control.BackgroundProperty,
                                new System.Windows.Data.Binding("LineNumbersMatchForeground")
                                {
                                   Source = this
                                });
         }
      }

      /// <summary>
      /// Specifies the displayed line numbers in the left margin.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	  05/28/2015	FIX: 74, invalidate measure when numbers are updated
      /// </history>
      public List<LineNumber> LineNumbers
      {
         get { return customMargin.LineNumbers; }
         set 
         { 
            customMargin.LineNumbers = value; 
            customMargin.InvalidateMeasure(); 
         }
      }

      /// <summary>
      /// Gets or sets the font size.
      /// </summary>
      public new double FontSize
      {
         get { return base.FontSize; }
         set 
         {
            base.FontSize = value;

            // setup zoom levels
            zoomDefault = value;
            zoomMaximum = value * 2;
            zoomMinimum = value / 2;
         }
      }

      /// <summary>
      /// Zoom in view by 1.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   05/11/2015	CHG: zoom for TextEditor control
      /// </history>
      public void ZoomIn()
      {
         if (base.FontSize + 1 <= zoomMaximum)
         {
            base.FontSize += 1;
         }
      }

      /// <summary>
      /// Zoom out view by 1.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   05/11/2015	CHG: zoom for TextEditor control
      /// </history>
      public void ZoomOut()
      {
         if (base.FontSize - 1 >= zoomMinimum)
         {
            base.FontSize -= 1;
         }
      }

      /// <summary>
      /// Reset zoom level to initial value.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   05/11/2015	CHG: zoom for TextEditor control
      /// </history>
      public void ZoomReset()
      {
         base.FontSize = zoomDefault;
      }

      #endregion

      /// <summary>
      /// Retrieves the position from the current right click point.
      /// </summary>
      /// <returns>TextViewPosition? value</returns>
      /// <history>
      /// [Curtis_Beard]		04/08/2015	ADD: update RichTextBox to AvalonEdit
      /// </history>
      public TextViewPosition? GetPositionFromRightClickPoint()
      {
         return this.GetPositionFromPoint(rightClickPosition);
      }

      #region Private Methods

      /// <summary>
      /// Handles setting up the custom numbering margin.
      /// </summary>
      /// <remarks>Handles being called more than once as it will only add margin if it doesn't exist yet.</remarks>
      /// <history>
      /// [Curtis_Beard]		04/08/2015	ADD: update RichTextBox to AvalonEdit
      /// </history>
      private void InitCustomMargin()
      {
         if (hasCustomMargin)
         {
            return;
         }

         var leftMargins = this.TextArea.LeftMargins;
         var dottedLine = new System.Windows.Shapes.Line
         {
            X1 = 0,
            Y1 = 0,
            X2 = 0,
            Y2 = 1,
            StrokeDashArray = { 0, 2 },
            Stretch = System.Windows.Media.Stretch.Fill,
            StrokeThickness = 1,
            StrokeDashCap = System.Windows.Media.PenLineCap.Round,
            Margin = new System.Windows.Thickness(2, 0, 2, 0),
            Tag = dottedLineTag
         };

         dottedLine.SetBinding(
            System.Windows.Shapes.Line.StrokeProperty,
            new System.Windows.Data.Binding("LineNumbersForeground") { Source = this }
         );

         leftMargins.Insert(0, customMargin);
         leftMargins.Insert(1, dottedLine);

         customMargin.SetBinding(System.Windows.Controls.Control.ForegroundProperty,
                                new System.Windows.Data.Binding("LineNumbersForeground")
                                {
                                   Source = this
                                });

         customMargin.SetBinding(System.Windows.Controls.Control.BackgroundProperty,
                                new System.Windows.Data.Binding("LineNumbersMatchForeground")
                                {
                                   Source = this
                                });

         hasCustomMargin = true;
      }

      /// <summary>
      /// Handles removing the custom numbering margin.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		04/08/2015	ADD: update RichTextBox to AvalonEdit
      /// </history>
      private void ClearCustomMargin()
      {
         TextEditor editor = this;
         var leftMargins = editor.TextArea.LeftMargins;

         for (int i = 0; i < leftMargins.Count; i++)
         {
            if (leftMargins[i] is CustomLineNumberMargin)
            {
               leftMargins.RemoveAt(i);
               if (i < leftMargins.Count && leftMargins[i] is System.Windows.Shapes.Line && (leftMargins[i] as System.Windows.Shapes.Line).Tag == dottedLineTag)
               {
                  leftMargins.RemoveAt(i);
               }

               hasCustomMargin = false;
               break;
            }
         }
      }

      /// <summary>
      /// Handles getting the mouse position when right clicked in CustomTextEditor.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		04/08/2015	ADD: update RichTextBox to AvalonEdit
      /// </history>
      private void TextEditorEx_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
      {
         rightClickPosition = e.GetPosition(this);
      }

      /// <summary>
      /// Handle mouse wheel zooming.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   05/11/2015	CHG: zoom for TextEditor control
      /// </history>
      private void TextEditorEx_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
      {
         zoomIsCtrlKey = false;
      }

      /// <summary>
      /// Handle mouse wheel zooming.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   05/11/2015	CHG: zoom for TextEditor control
      /// </history>
      private void TextEditorEx_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
      {
         if (e.Key == System.Windows.Input.Key.LeftCtrl || e.Key == System.Windows.Input.Key.RightCtrl)
         {
            zoomIsCtrlKey = true;
         }
      }

      /// <summary>
      /// Handle mouse wheel zooming.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   05/11/2015	CHG: zoom for TextEditor control
      /// </history>
      private void TextEditorEx_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
      {
         if (zoomIsCtrlKey && e.Delta > 0)
         {
            ZoomIn();
            e.Handled = true;
         }
         else if (zoomIsCtrlKey && e.Delta < 0)
         {
            ZoomOut();
            e.Handled = true;
         }
      }

      #endregion
   }
}
