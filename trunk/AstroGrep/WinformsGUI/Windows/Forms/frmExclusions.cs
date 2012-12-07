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
   /// </history>
   public partial class frmExclusions : Form
   {
      private List<ExclusionItem> exclusionItems = new List<ExclusionItem>();

      /// <summary>
      /// Gets the Exclusion list from this dialog.
      /// </summary>
      public List<ExclusionItem> ExclusionItems { get { return exclusionItems; } }

      /// <summary>
      /// Create the instance of this form.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		03/07/2012	ADD: 3131609, exclusions
      /// </history>
      public frmExclusions(List<ExclusionItem> items)
      {
         InitializeComponent();

         exclusionItems = items;

         API.ListViewExtensions.SetTheme(lstExclusions);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private void frmExclusions_Load(object sender, EventArgs e)
      {
         Language.ProcessForm(this);

         // set column text
         lstExclusions.Columns[0].Text = Language.GetGenericText("Exclusions.Type", "Type");
         lstExclusions.Columns[1].Text = Language.GetGenericText("Exclusions.Value", "Value");
         lstExclusions.Columns[2].Text = Language.GetGenericText("Exclusions.Option", "Option");

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
      /// </history>
      private void btnOK_Click(object sender, EventArgs e)
      {
         // save current list
         if (exclusionItems == null)
         {
            exclusionItems = new List<ExclusionItem>();
         }
         exclusionItems.Clear();
         foreach (ListViewItem listItem in lstExclusions.Items)
         {
            exclusionItems.Add(listItem.Tag as ExclusionItem);
         }

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
      /// </history>
      private void btnEdit_Click(object sender, EventArgs e)
      {
         if (lstExclusions.SelectedItems.Count > 0)
         {
            // get currently selected exclusion
            var item = lstExclusions.SelectedItems[0].Tag as ExclusionItem;

            var dlg = new frmAddEditExclusions(item);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
               item = dlg.CurrentItem;
               var listItem = GetListViewItem(item);
               lstExclusions.SelectedItems[0].Tag = item;

               lstExclusions.SelectedItems[0].Text = listItem.Text;
               lstExclusions.SelectedItems[0].SubItems[1].Text = listItem.SubItems[1].Text;
               lstExclusions.SelectedItems[0].SubItems[2].Text = listItem.SubItems[2].Text;

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
      /// </history>
      private void btnAdd_Click(object sender, EventArgs e)
      {
         var dlg = new frmAddEditExclusions(null);
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
      /// </history>
      private void LoadExclusions()
      {
         if (exclusionItems != null && exclusionItems.Count > 0)
         {
            lstExclusions.BeginUpdate();
            foreach (ExclusionItem item in exclusionItems)
            {
               lstExclusions.Items.Add(GetListViewItem(item));
            }
            lstExclusions.EndUpdate();
         }
      }

      /// <summary>
      /// Get the list view item from the given ExclusionItem object.
      /// </summary>
      /// <param name="item">ExclusionItem object</param>
      /// <returns>ListViewItem object</returns>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private ListViewItem GetListViewItem(ExclusionItem item)
      {
         ListViewItem listItem = new ListViewItem();
         listItem.Tag = item;
         listItem.Text = Language.GetGenericText("Exclusions." + item.Type.ToString());
         listItem.SubItems.Add(item.Value);
         listItem.SubItems.Add(string.Format("{0}{1}", Language.GetGenericText("Exclusions." + item.Option.ToString()), item.IgnoreCase ? Language.GetGenericText("Exclusions.IgnoreCase") : ""));

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

         exclusionItems = ExclusionItem.ConvertStringToExclusions(Constants.DefaultExclusions);
         LoadExclusions();
      }
   }
}
