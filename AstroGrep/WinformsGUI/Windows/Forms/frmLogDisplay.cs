using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AstroGrep.Core;
using libAstroGrep;

namespace AstroGrep.Windows.Forms
{
   /// <summary>
   /// Display a filterable view of the log items.
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
   /// [Curtis_Beard]	   12/06/2012	ADD: 1741735, initial dialog for filterable log items viewer.
   /// </history>
   public partial class frmLogDisplay : Form
   {
      /// <summary>
      /// Collection of messages to display
      /// </summary>
      public List<LogItem> LogItems 
      { 
         get; 
         set; 
      }

      /// <summary>
      /// Initial LogItemType to display (null to show all).
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   12/06/2012	ADD: 1741735, initial dialog for filterable log items viewer.
      /// [Curtis_Beard]	   11/11/2014	CHG: make nullable so that null means show all
      /// </history>
      public LogItem.LogItemTypes? DefaultFilterType 
      { 
         get;
         set;
      }

      /// <summary>
      /// Creates an instance of this class and sets up form.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   12/06/2012	ADD: 1741735, initial dialog for filterable log items viewer.
      /// [Curtis_Beard]	   03/19/2015	CHG: use system look for toolbar on Windows XP.
      /// </history>
      public frmLogDisplay()
      {
         InitializeComponent();
         API.ListViewExtensions.SetTheme(lstLog);
         DefaultFilterType = null;

         // use custom renderer to have background show selected state better than default (using default color though).
         if (API.IsWindowsVistaOrLater)
         {
            toolStrip1.Renderer = new MyRenderer();
         }
         else
         {
            // for xp show a more system look
            toolStrip1.RenderMode = ToolStripRenderMode.System;
         }
      }

      /// <summary>
      /// Setup the form.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   12/06/2012	ADD: 1741735, initial dialog for filterable log items viewer.
      /// [Curtis_Beard]	   11/11/2014	CHG: set width of columns, language calls for column names, support null for DefaultFilterType
      /// [Curtis_Beard]	   03/02/2015	CHG: add counts to tabs
      /// [Curtis_Beard]      03/03/2015	CHG: 93, load column widths from user settings
      /// </history>
      private void frmLogDisplay_Load(object sender, EventArgs e)
      {
         // safety check
         if (LogItems == null || LogItems.Count == 0)
         {
            this.Close();
         }

         Language.ProcessForm(this);

         // column names aren't supported in Language class
         lstLog.Columns[0].Text = Language.GetGenericText("LogDisplay.Column.Date", "Date");
         lstLog.Columns[1].Text = Language.GetGenericText("LogDisplay.Column.Type", "Type");
         lstLog.Columns[2].Text = Language.GetGenericText("LogDisplay.Column.Value", "Value");
         lstLog.Columns[3].Text = Language.GetGenericText("LogDisplay.Column.Details", "Details");

         // set column widths from user settings
         lstLog.Columns[0].Width = Core.GeneralSettings.LogDisplayColumnDateWidth;
         lstLog.Columns[1].Width = Core.GeneralSettings.LogDisplayColumnTypeWidth;
         lstLog.Columns[2].Width = Core.GeneralSettings.LogDisplayColumnValueWidth;
         lstLog.Columns[3].Width = Core.GeneralSettings.LogDisplayColumnDetailsWidth;

         // do counts after Language.ProcessForm to get language specific text.
         sbtnStatus.Text = string.Format("{0} ({1})", sbtnStatus.Text, LogItems.FindAll(l=>l.ItemType == LogItem.LogItemTypes.Status).Count);
         sbtnExclusions.Text = string.Format("{0} ({1})", sbtnExclusions.Text, LogItems.FindAll(l => l.ItemType == LogItem.LogItemTypes.Exclusion).Count);
         sbtnError.Text = string.Format("{0} ({1})", sbtnError.Text, LogItems.FindAll(l => l.ItemType == LogItem.LogItemTypes.Error).Count);

         if (DefaultFilterType.HasValue)
         {
            switch (DefaultFilterType)
            {
               case LogItem.LogItemTypes.Status:
                  sbtnExclusions.Checked = sbtnError.Checked = false;
                  sbtnStatus.Checked = true;
                  break;
               case LogItem.LogItemTypes.Exclusion:
                  sbtnError.Checked = sbtnStatus.Checked = false;
                  sbtnExclusions.Checked = true;
                  break;
               case LogItem.LogItemTypes.Error:
                  sbtnExclusions.Checked = sbtnStatus.Checked = false;
                  sbtnError.Checked = true;
                  break;
            }
         }
         else
         {
            // show all
            sbtnExclusions.Checked = sbtnError.Checked = sbtnStatus.Checked = true;
         }
      }

      /// <summary>
      /// Handle the form's key up event.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   12/06/2012	ADD: 1741735, initial dialog for filterable log items viewer.
      /// </history>
      private void frmLogDisplay_KeyUp(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Escape)
         {
            this.Close();
         }
      }

      /// <summary>
      /// Handle the listview's key up event.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   12/06/2012	ADD: 1741735, initial dialog for filterable log items viewer.
      /// </history>
      private void lstLog_KeyUp(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Escape)
         {
            this.Close();
         }
      }

      /// <summary>
      /// Handle the listview's key down event (item selection and item copy).
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   12/06/2012	ADD: 1741735, initial dialog for filterable log items viewer.
      /// </history>
      private void lstLog_KeyDown(object sender, KeyEventArgs e)
      {
         //ctrl+c  Copy to clipboard
         if (e.KeyCode == Keys.C && e.Control)
         {
            if (lstLog.SelectedItems.Count <= 0)
               return;

            System.Text.StringBuilder data = new System.Text.StringBuilder();
            try
            {
               foreach (ListViewItem lvi in lstLog.SelectedItems)
               {
                  data.Append(lvi.Text);
                  data.Append(", ");
                  data.Append(lvi.SubItems[1].Text);
                  data.Append(", ");
                  data.Append(lvi.SubItems[2].Text);
                  data.Append(", ");
                  data.Append(lvi.SubItems[3].Text);

                  data.Append(Environment.NewLine);
               }
               Clipboard.SetDataObject(data.ToString());
            }
            catch (Exception ex)
            {
               MessageBox.Show(this, "Exception: " + ex.Message, Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }

         //ctrl+a  Select All
         if (e.KeyCode == Keys.A && e.Control)
         {
            foreach (ListViewItem lvi in lstLog.Items)
            {
               lvi.Selected = true;
            }
         }
      }

      /// <summary>
      /// Handle better showing button selected state.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   11/11/2014	Initial
      /// </history>
      private class MyRenderer : ToolStripProfessionalRenderer
      {
         /// <summary>
         /// Adjust button background display to better show selected item.
         /// </summary>
         /// <param name="e">render event argument</param>
         /// <history>
         /// [Curtis_Beard]	   11/11/2014	Initial
         /// </history>
         protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
         {
            var btn = e.Item as ToolStripButton;
            if (btn != null && btn.CheckOnClick && btn.Checked)
            {
               Rectangle bounds = new Rectangle(Point.Empty, e.Item.Size);

               // fill button background
               e.Graphics.FillRectangle(new SolidBrush(ProfessionalColors.ButtonCheckedHighlight), bounds);

               // draw border around button
               bounds.Inflate(-1, -1);
               e.Graphics.DrawRectangle(new Pen(ProfessionalColors.ButtonCheckedHighlightBorder), bounds);
            }
            else
            {
               base.OnRenderButtonBackground(e);
            }
         }
      }

      /// <summary>
      /// Adds the specified LogItemType to the display.
      /// </summary>
      /// <param name="type">LogItemType to add</param>
      /// <history>
      /// [Curtis_Beard]	   12/06/2012	ADD: 1741735, initial dialog for filterable log items viewer.
      /// [Curtis_Beard]	   11/11/2014	CHG: format details display for exclusions
      /// [Curtis_Beard]	   03/02/2015	CHG: set sort to null before add
      /// </history>
      private void AddLogItemType(LogItem.LogItemTypes type)
      {
         lstLog.BeginUpdate();
         lstLog.ListViewItemSorter = null;

         foreach (LogItem item in LogItems)
         {
            if (item.ItemType == type)
            {
               string typeText = Language.GetGenericText(string.Format("LogDisplay.{0}", type), type.ToString());
               string valueText = item.Value;
               string detailsText = item.Details;
               if (item.ItemType == LogItem.LogItemTypes.Exclusion)
               {
                  // convert details from format FilterItem~~FilterValue
                  string[] values = Core.Common.SplitByString(detailsText, "~~");
                  var filterItem = FilterItem.FromString(values[0]);
                  detailsText = string.Format("{0} -> {1}{2}{3}{4}", 
                     Language.GetGenericText(string.Format("Exclusions.{0}", filterItem.FilterType.Category), filterItem.FilterType.Category.ToString()), 
                     Language.GetGenericText(string.Format("Exclusions.{0}", filterItem.FilterType.SubCategory), filterItem.FilterType.SubCategory.ToString()),
                     !string.IsNullOrEmpty(filterItem.Value) && filterItem.FilterType.SubCategory != FilterType.SubCategories.Extension ? ", " + values[1] : string.Empty,
                     filterItem.ValueOption != FilterType.ValueOptions.None ? " " + Language.GetGenericText(string.Format("Exclusions.{0}", filterItem.ValueOption), filterItem.ValueOption.ToString()) : string.Empty,
                     !string.IsNullOrEmpty(filterItem.Value) && filterItem.FilterType.SubCategory != FilterType.SubCategories.Extension ? " " + filterItem.Value : string.Empty
                     );
               }
               else if (item.ItemType == LogItem.LogItemTypes.Status)
               {
                  // Value = Language lookup text (e.g. SearchFinished,SearchCancelled)
                  // Details = 0||1 replacement arguments where 0 is file name or error message, 1 is details
                  string[] values = Core.Common.SplitByString(detailsText, "||");
                  valueText = string.Format(Language.GetGenericText(valueText), values);
                  detailsText = string.Empty;
                  if (values.Length > 1)
                  {
                     detailsText = values[1];
                  }
               }
               else if (item.ItemType == LogItem.LogItemTypes.Error)
               {
                  // Value = Language lookup text (e.g. SearchGenericError, SearchFileError)
                  // Details = 0||1 replacement arguments where 0 = file or empty string, 1 = details (error text)
                  string[] values = Core.Common.SplitByString(detailsText, "||");
                  valueText = string.Format(Language.GetGenericText(valueText), values[0]);
                  detailsText = values[1];
               }

               ListViewItem lstItem = new ListViewItem(new string[4] { item.Date.ToShortDateString() + " " + item.Date.ToString("hh:mm:ss.ff tt"), typeText, valueText, detailsText });
               lstItem.Tag = item;
               lstLog.Items.Add(lstItem);
            }
         }

         lstLog.ListViewItemSorter = new LogItemComparer();
         lstLog.Sort();

         lstLog.EndUpdate();
      }

      /// <summary>
      /// Removes the specified LogItemType from the display.
      /// </summary>
      /// <param name="type">LogItemType to remove</param>
      /// <history>
      /// [Curtis_Beard]	   12/06/2012	ADD: 1741735, initial dialog for filterable log items viewer.
      /// [Curtis_Beard]	   03/02/2015	CHG: set sort to null before remove
      /// </history>
      private void RemoveLogItemType(LogItem.LogItemTypes type)
      {
         lstLog.BeginUpdate();
         lstLog.ListViewItemSorter = null;

         foreach (ListViewItem lstItem in lstLog.Items)
         {
            LogItem item = lstItem.Tag as LogItem;
            if (item.ItemType == type)
            {
               lstLog.Items.Remove(lstItem);
            }
         }

         lstLog.ListViewItemSorter = new LogItemComparer();
         lstLog.Sort();

         lstLog.EndUpdate();
      }

      /// <summary>
      /// Handle the check changed event for the Status button.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   12/06/2012	ADD: 1741735, initial dialog for filterable log items viewer.
      /// </history>
      private void sbtnStatus_CheckedChanged(object sender, EventArgs e)
      {
         if (sbtnStatus.Checked)
         {
            // include status
            sbtnStatus.Checked = true;
            AddLogItemType(LogItem.LogItemTypes.Status);
         }
         else
         {
            // remove status
            RemoveLogItemType(LogItem.LogItemTypes.Status);
         }
      }

      /// <summary>
      /// Handle the check changed event for the Exclusions button.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   12/06/2012	ADD: 1741735, initial dialog for filterable log items viewer.
      /// </history>
      private void sbtnExclusions_CheckedChanged(object sender, EventArgs e)
      {
         if (sbtnExclusions.Checked)
         {
            // include exclusions
            sbtnExclusions.Checked = true;
            AddLogItemType(LogItem.LogItemTypes.Exclusion);
         }
         else
         {
            // remove exclusions
            RemoveLogItemType(LogItem.LogItemTypes.Exclusion);
         }
      }

      /// <summary>
      /// Handle the check changed event for the Error button.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   12/06/2012	ADD: 1741735, initial dialog for filterable log items viewer.
      /// </history>
      private void sbtnError_CheckedChanged(object sender, EventArgs e)
      {
         if (sbtnError.Checked)
         {
            // include error
            sbtnError.Checked = true;
            AddLogItemType(LogItem.LogItemTypes.Error);
         }
         else
         {
            // remove error
            sbtnError.Checked = false;
            RemoveLogItemType(LogItem.LogItemTypes.Error);
         }
      }

      /// <summary>
      /// Class used to compare the LogItem's stored in the ListView.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   12/06/2012	ADD: 1741735, initial dialog for filterable log items viewer.
      /// </history>
      internal class LogItemComparer : System.Collections.IComparer
      {
         /// <summary>
         /// Creates an instance of this class.
         /// </summary>
         /// <history>
         /// [Curtis_Beard]	   12/06/2012	ADD: 1741735, initial dialog for filterable log items viewer.
         /// </history>
         public LogItemComparer()
         {

         }

         /// <summary>
         /// Compares the given ListViewItems (uses Tag property to get LogItem).
         /// </summary>
         /// <param name="x">First ListViewItem</param>
         /// <param name="y">Second ListViewItem</param>
         /// <returns>int for comparison of LogItem dates</returns>
         /// <history>
         /// [Curtis_Beard]	   12/06/2012	ADD: 1741735, initial dialog for filterable log items viewer.
         /// </history>
         public int Compare(object x, object y)
         {
            LogItem xItem = (x as ListViewItem).Tag as LogItem;
            LogItem yItem = (y as ListViewItem).Tag as LogItem;

            return xItem.Date.CompareTo(yItem.Date);
         }
      }

      /// <summary>
      /// Save column widths and window position (if enabled).
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      03/03/2015	CHG: 93, save form position and column widths
      /// </history>
      private void frmLogDisplay_FormClosing(object sender, FormClosingEventArgs e)
      {
         Core.GeneralSettings.LogDisplayColumnDateWidth = lstLog.Columns[0].Width;
         Core.GeneralSettings.LogDisplayColumnTypeWidth = lstLog.Columns[1].Width;
         Core.GeneralSettings.LogDisplayColumnValueWidth = lstLog.Columns[2].Width;
         Core.GeneralSettings.LogDisplayColumnDetailsWidth = lstLog.Columns[3].Width;

         if (Core.GeneralSettings.LogDisplaySavePosition)
         {
            Core.GeneralSettings.LogDisplayTop = this.Top;
            Core.GeneralSettings.LogDisplayLeft = this.Left;
            Core.GeneralSettings.LogDisplayWidth = this.Width;
            Core.GeneralSettings.LogDisplayHeight = this.Height;
         }
      }
   }
}
