using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
   /// [Curtis_Beard]	   11/11/2014	Initial
   /// </history>
   public partial class FilterValueType : UserControl
   {
      private ViewTypes currentViewType = ViewTypes.String;

      /// <summary>
      /// Available view types for this control.
      /// </summary>
      public enum ViewTypes
      {
         /// <summary>Display the DateTime picker</summary>
         DateTime,
         /// <summary>Display a TextBox</summary>
         String,
         /// <summary>Display the numeric up/down control</summary>
         Numeric,
         /// <summary>Display a numeric up/down control with a drop down for size type selection</summary>
         Size
      }

      /// <summary>
      /// Initialize the control.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   11/11/2014	Initial
      /// </history>
      public FilterValueType()
      {
         InitializeComponent();

         numValue.Maximum = numSize.Maximum = decimal.MaxValue;

         // setup default view type
         SetViewType(currentViewType);
      }

      /// <summary>
      /// Handle resetting the value and controls.
      /// </summary>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   11/11/2014	Initial
      /// </history>
      protected override void OnLoad(EventArgs e)
      {
         base.OnLoad(e);

         ResetValue();
         ResetControls();
      }

      /// <summary>
      /// Reset the control's position and size to fall within the overall size of the control.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   11/11/2014	Initial
      /// </history>
      private void ResetControls()
      {
         dtpValue.Left = txtValue.Left = numValue.Left = pnlSize.Left = 0;
         dtpValue.Top = txtValue.Top = numValue.Top = pnlSize.Top = 0;
         dtpValue.Width = txtValue.Width = numValue.Width = pnlSize.Width = this.Width;
         dtpValue.Height = txtValue.Height = numValue.Height = pnlSize.Height = this.Width;
      }

      /// <summary>
      /// Sets the view type.
      /// </summary>
      /// <param name="viewType">The view type to be displayed</param>
      /// <history>
      /// [Curtis_Beard]	   11/11/2014	Initial
      /// </history>
      public void SetViewType(ViewTypes viewType)
      {
         currentViewType = viewType;

         switch (currentViewType)
         {
            case ViewTypes.DateTime:
               dtpValue.Visible = true;
               txtValue.Visible = false;
               numValue.Visible = false;
               pnlSize.Visible = false;
               break;

            case ViewTypes.String:
               dtpValue.Visible = false;
               txtValue.Visible = true;
               numValue.Visible = false;
               pnlSize.Visible = false;
               break;

            case ViewTypes.Numeric:
               dtpValue.Visible = false;
               txtValue.Visible = false;
               numValue.Visible = true;
               pnlSize.Visible = false;
               break;

            case ViewTypes.Size:
               dtpValue.Visible = false;
               txtValue.Visible = false;
               numValue.Visible = false;
               pnlSize.Visible = true;
               break;
         }
      }

      /// <summary>
      /// Resets all control values to the default.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   11/11/2014	Initial
      /// </history>
      public void ResetValue()
      {
         dtpValue.Value = DateTime.Now;
         txtValue.Text = string.Empty;
         numValue.Value = 0;
         numSize.Value = 0;
         cboSize.SelectedIndex = 0;
      }

      /// <summary>
      /// Sets the size drop down to the give text value.
      /// </summary>
      /// <param name="itemText">byte,kb,mb,gb</param>
      /// <history>
      /// [Curtis_Beard]	   11/11/2014	Initial
      /// </history>
      public void SetSizeDropDown(string itemText)
      {
         cboSize.SelectedItem = itemText;
      }

      /// <summary>
      /// Retrieves the currently selected size type.
      /// </summary>
      /// <returns>string of byte,kb,mb,gb</returns>
      /// <history>
      /// [Curtis_Beard]	   11/11/2014	Initial
      /// </history>
      public string GetSizeDropDown()
      {
         return cboSize.SelectedItem.ToString();
      }

      /// <summary>
      /// Gets/Sets the value for the currently displayed view type.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   11/11/2014	Initial
      /// </history>
      public string Value
      {
         get
         {
            switch (currentViewType)
            {
               case ViewTypes.DateTime:
                  return dtpValue.Value.ToString();

               case ViewTypes.Numeric:
                  return numValue.Value.ToString();

               case ViewTypes.Size:
                  return Core.Convertors.ConvertFileSizeFromDisplay(numSize.Value.ToString(), cboSize.SelectedItem.ToString(), 0).ToString();

               case ViewTypes.String:
               default:
                  return txtValue.Text;
            }
         }

         set
         {
            switch (currentViewType)
            {
               case ViewTypes.DateTime:
                  dtpValue.Value = DateTime.Parse(value);
                  break;

               case ViewTypes.Numeric:
                  numValue.Value = decimal.Parse(value);
                  break;

               case ViewTypes.Size:
                  numSize.Value = decimal.Parse(value);
                  break;

               case ViewTypes.String:
               default:
                  txtValue.Text = value;
                  break;
            }
         }
      }
   }
}
