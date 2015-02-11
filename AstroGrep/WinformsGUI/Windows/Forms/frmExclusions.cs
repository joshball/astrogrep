using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using libAstroGrep;

namespace AstroGrep.Windows.Forms
{
   /// <summary>
   /// Used to manipulate the exclusion list.
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
   /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
   /// [Curtis_Beard]	   11/11/2014	CHG: use FilterItem
   /// </history>
   public partial class frmExclusions : Form
   {
      private List<FilterItem> filterItems = new List<FilterItem>();
      private bool inhibitAutoCheck;

      /// <summary>
      /// Gets the Exclusion list from this dialog.
      /// </summary>
      public List<FilterItem> FilterItems { get { return filterItems; } }

      /// <summary>
      /// Create the instance of this form.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		03/07/2012	ADD: 3131609, exclusions
      /// </history>
      public frmExclusions(List<FilterItem> items)
      {
         InitializeComponent();

         filterItems = items;

         API.ListViewExtensions.SetTheme(lstExclusions);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		03/07/2012	ADD: 3131609, exclusions
      /// [Curtis_Beard]	   11/11/2014	CHG: add category column
      /// </history>
      private void frmExclusions_Load(object sender, EventArgs e)
      {
         Language.ProcessForm(this);

         // set column text
         lstExclusions.Columns[0].Text = Language.GetGenericText("Exclusions.Enabled", "Enabled");
         lstExclusions.Columns[1].Text = Language.GetGenericText("Exclusions.Category", "Category");
         lstExclusions.Columns[2].Text = Language.GetGenericText("Exclusions.Type", "Type");
         lstExclusions.Columns[3].Text = Language.GetGenericText("Exclusions.Value", "Value");
         lstExclusions.Columns[4].Text = Language.GetGenericText("Exclusions.Option", "Option");

         LoadExclusions();

         SetButtonState();
      }

      /// <summary>
      /// Cancel the dialog.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private void btnCancel_Click(object sender, EventArgs e)
      {
         this.DialogResult = DialogResult.Cancel;
         this.Close();
      }

      /// <summary>
      /// Save the exclusion items.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		03/07/2012	ADD: 3131609, exclusions
      /// [Curtis_Beard]	   11/11/2014	CHG: use FilterItem
      /// </history>
      private void btnOK_Click(object sender, EventArgs e)
      {
         filterItems = GetCurrentFilterItems();

         this.DialogResult = DialogResult.OK;
         this.Close();
      }

      /// <summary>
      /// Delete the selected exclusion items.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private void btnDelete_Click(object sender, EventArgs e)
      {
         // remove
         if (lstExclusions.SelectedItems.Count > 0)
         {
            foreach (ListViewItem item in lstExclusions.SelectedItems)
            {
               lstExclusions.Items.Remove(item);
            }
            SetButtonState();
         }

         this.DialogResult = DialogResult.None;
      }

      /// <summary>
      /// Edit an exclusion item.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		03/07/2012	ADD: 3131609, exclusions
      /// [Curtis_Beard]	   11/11/2014	CHG: use FilterItem, pass all current FilterItems to add/edit form
      /// </history>
      private void btnEdit_Click(object sender, EventArgs e)
      {
         if (lstExclusions.SelectedItems.Count > 0)
         {
            // get currently selected exclusion
            var item = lstExclusions.SelectedItems[0].Tag as FilterItem;
            item.Enabled = lstExclusions.SelectedItems[0].Checked;

            var dlg = new frmAddEditExclusions(GetCurrentFilterItems(), item);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
               item = dlg.CurrentItem;
               var listItem = GetListViewItem(item);
               lstExclusions.SelectedItems[0].Checked = item.Enabled;
               lstExclusions.SelectedItems[0].Tag = item;

               lstExclusions.SelectedItems[0].SubItems[1].Text = listItem.SubItems[1].Text;
               lstExclusions.SelectedItems[0].SubItems[2].Text = listItem.SubItems[2].Text;
               lstExclusions.SelectedItems[0].SubItems[3].Text = listItem.SubItems[3].Text;
               lstExclusions.SelectedItems[0].SubItems[4].Text = listItem.SubItems[4].Text;

               SetButtonState();
            }
         }

         this.DialogResult = DialogResult.None;
      }

      /// <summary>
      /// Add a new exclusion item.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		03/07/2012	ADD: 3131609, exclusions
      /// [Curtis_Beard]	   11/11/2014	CHG: use FilterItem, pass all current FilterItems to add/edit form
      /// </history>
      private void btnAdd_Click(object sender, EventArgs e)
      {
         var dlg = new frmAddEditExclusions(GetCurrentFilterItems(), null);
         if (dlg.ShowDialog(this) == DialogResult.OK)
         {
            // create new entry
            lstExclusions.Items.Add(GetListViewItem(dlg.CurrentItem));

            SetButtonState();
         }

         this.DialogResult = DialogResult.None;
      }

      /// <summary>
      /// Update the button states.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private void lstExclusions_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         SetButtonState();
      }

      /// <summary>
      /// Edit the selected exclusion entry.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private void lstExclusions_DoubleClick(object sender, EventArgs e)
      {
         Point clientPoint = lstExclusions.PointToClient(Control.MousePosition);
         ListViewItem item = lstExclusions.GetItemAt(clientPoint.X, clientPoint.Y);

         if (item != null)
         {
            item.Selected = true;
            btnEdit_Click(null, null);
         }
      }

      /// <summary>
      /// Handles the key down event (supports ctrl-a, del).
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private void lstExclusions_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyCode == Keys.A && e.Control) //ctrl+a  Select All
         {
            foreach (ListViewItem item in lstExclusions.Items)
            {
               item.Selected = true;
            }
         }

         if (e.KeyCode == Keys.Delete) //delete
         {
            btnDelete_Click(sender, EventArgs.Empty);
         }
      }

      /// <summary>
      /// Sets the TextEditor's button states depending on if one is selected.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private void SetButtonState()
      {
         if (lstExclusions.SelectedItems.Count > 0)
         {
            btnDelete.Enabled = true;
            btnEdit.Enabled = true;
         }
         else
         {
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
         }
      }

      /// <summary>
      /// Loads the exclusion list.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// [Curtis_Beard]	   11/11/2014	CHG: use FilterItem
      /// </history>
      private void LoadExclusions()
      {
         if (filterItems != null && filterItems.Count > 0)
         {
            lstExclusions.BeginUpdate();
            foreach (FilterItem item in filterItems)
            {
               lstExclusions.Items.Add(GetListViewItem(item));
            }
            lstExclusions.EndUpdate();
         }
      }

      /// <summary>
      /// Get the list view item from the given FilterItem object.
      /// </summary>
      /// <param name="item">FilterItem object</param>
      /// <returns>ListViewItem object</returns>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// [Curtis_Beard]	   11/11/2014	CHG: use FilterItem
      /// </history>
      private ListViewItem GetListViewItem(FilterItem item)
      {
         ListViewItem listItem = new ListViewItem();
         listItem.Tag = item;
         listItem.Checked = item.Enabled;
         listItem.SubItems.Add(Language.GetGenericText("Exclusions." + item.FilterType.Category.ToString(), item.FilterType.Category.ToString()));
         listItem.SubItems.Add(Language.GetGenericText("Exclusions." + item.FilterType.SubCategory.ToString(), item.FilterType.SubCategory.ToString()));
         
         string valueText = item.Value;         
         string optionText = Language.GetGenericText("Exclusions." + item.ValueOption.ToString());
         string additionalInfo = string.Empty;
         if (item.ValueIgnoreCase)
         {
            additionalInfo = Language.GetGenericText("Exclusions.IgnoreCase");
         }
         else if (item.FilterType.Category == FilterType.Categories.File && item.FilterType.SubCategory == FilterType.SubCategories.Size && !string.IsNullOrEmpty(item.ValueSizeOption))
         {
            valueText = string.Format("{0} {1}", AstroGrep.Core.Convertors.ConvertFileSizeForDisplay(item.Value, item.ValueSizeOption), item.ValueSizeOption);
         }

         listItem.SubItems.Add(valueText);
         listItem.SubItems.Add(string.Format("{0}{1}", optionText, additionalInfo));

         return listItem;
      }

      /// <summary>
      /// Restore default exclusion list.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private void btnRestoreDefaults_Click(object sender, EventArgs e)
      {
         lstExclusions.Items.Clear();

         filterItems = FilterItem.ConvertStringToFilterItems(Constants.DefaultFilterItems);
         LoadExclusions();
      }
      
      /// <summary>
      /// Handle not changing checked state of item when double clicking to edit it.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   03/04/2014	FIX: 52, don't change check state when double clicking to edit
      /// </history>
      private void lstExclusions_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         inhibitAutoCheck = true;
      }

      /// <summary>
      /// Handle not changing checked state of item when double clicking to edit it.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   03/04/2014	FIX: 52, don't change check state when double clicking to edit
      /// </history>
      private void lstExclusions_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
      {
         if (inhibitAutoCheck)
         {
            e.NewValue = e.CurrentValue;
         }
      }

      /// <summary>
      /// Handle not changing checked state of item when double clicking to edit it.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   03/04/2014	FIX: 52, don't change check state when double clicking to edit
      /// </history>
      private void lstExclusions_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         inhibitAutoCheck = false;
      }

      /// <summary>
      /// Handles ListView Column Click event to allow Enabled column to toggle all checkboxes.
      /// </summary>
      /// <param name="sender">lstExclusions listview</param>
      /// <param name="e">column click arguments</param>
      /// <history>
      /// [Curtis_Beard]	   08/13/2014	ADD: 79, allow Enabled column to toggle all checkboxes
      /// </history>
      private void lstExclusions_ColumnClick(object sender, ColumnClickEventArgs e)
      {
         // enabled column
         if (e.Column == 0)
         {
            bool allChecked = (lstExclusions.CheckedItems.Count == lstExclusions.Items.Count);
            foreach (ListViewItem item in lstExclusions.Items)
            {
               item.Checked = !allChecked;
            }
         }
      }

      /// <summary>
      /// Get a list of FilterItems that match the currently displayed FilterItems in this screen.
      /// </summary>
      /// <returns>List of FilterItems</returns>
      /// <history>
      /// [Curtis_Beard]	   11/06/2014	CHG: use FilterItem
      /// </history>
      private List<FilterItem> GetCurrentFilterItems()
      {
         var list = new List<FilterItem>(lstExclusions.Items.Count);
         foreach (ListViewItem listItem in lstExclusions.Items)
         {
            var item = listItem.Tag as FilterItem;
            item.Enabled = listItem.Checked;
            list.Add(item);
         }

         return list;
      }
   }
}
