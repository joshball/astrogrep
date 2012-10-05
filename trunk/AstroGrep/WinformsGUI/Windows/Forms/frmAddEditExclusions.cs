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
   /// Used to edit a single exclusion item.
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
   public partial class frmAddEditExclusions : Form
   {
      private ExclusionItem _item = null;

      /// <summary>
      /// Gets the current exclusion item for this dialog.
      /// </summary>
      public ExclusionItem CurrentItem { get { return _item; } }

      /// <summary>
      /// Create an instance of this form.
      /// </summary>
      /// <param name="item">ExclusionItem object to edit, null if adding a new one.</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      public frmAddEditExclusions(ExclusionItem item)
      {
         InitializeComponent();

         _item = item;
      }

      /// <summary>
      /// Setup the form.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private void frmExclusions_Load(object sender, EventArgs e)
      {
         Language.ProcessForm(this);

         // load combo boxes with language specific values
         foreach (string name in Enum.GetNames(typeof(ExclusionItem.ExclusionTypes)))
         {
            cboTypes.Items.Add(Language.GetGenericText("Exclusions." + name));
         }
         foreach (string name in Enum.GetNames(typeof(ExclusionItem.OptionsTypes)))
         {
            cboOptions.Items.Add(Language.GetGenericText("Exclusions." + name));
         }

         // load fields with values if in edit mode.
         if (_item != null)
         {
            cboTypes.SelectedIndex = (int)_item.Type;
            txtValue.Text = _item.Value;
            cboOptions.SelectedIndex = (int)_item.Option;
            chkIgnoreCase.Checked = _item.IgnoreCase;
         }
      }

      /// <summary>
      /// Save the new/edit item.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private void btnOK_Click(object sender, EventArgs e)
      {
         this.DialogResult = DialogResult.None;
         ExclusionItem item = VerifyInterface();

         if (item != null)
         {
            _item = item;

            this.DialogResult = DialogResult.OK;
            this.Close();
         }
      }

      /// <summary>
      /// Close the dialog.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private void btnCancel_Click(object sender, EventArgs e)
      {
         this.DialogResult = DialogResult.Cancel;
         this.Close();
      }

      /// <summary>
      /// Setup controls based on selected type.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private void cboTypes_SelectedIndexChanged(object sender, EventArgs e)
      {
         switch (cboTypes.SelectedIndex)
         {
            case 0:
               cboOptions.SelectedIndex = 0;
               cboOptions.Enabled = false;
               chkIgnoreCase.Enabled = false;
               break;

            default:
               cboOptions.SelectedIndex = 1;
               cboOptions.Enabled = true;
               chkIgnoreCase.Enabled = true;
               break;
         }
      }

      /// <summary>
      /// Setup controls based on selected options.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private void cboOptions_SelectedIndexChanged(object sender, EventArgs e)
      {
         chkIgnoreCase.Enabled = cboOptions.SelectedIndex != 0;
      }

      /// <summary>
      /// Verify that the values selected are valid. [MessageBox displayed based on error]
      /// </summary>
      /// <returns>ExclusionItem object if valid, null if not.</returns>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private ExclusionItem VerifyInterface()
      {
         // needs to be a type
         if (cboTypes.SelectedIndex == -1)
         {
            MessageBox.Show(this, Language.GetGenericText("Exclusions.Error.NoType"), Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return null;
         }

         // needs to be a value
         if (string.IsNullOrEmpty(txtValue.Text.Trim()))
         {
            MessageBox.Show(this, Language.GetGenericText("Exclusions.Error.NoValue"), Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return null;
         }

         // create item and set type, value
         var item = new ExclusionItem();
         item.Type = (ExclusionItem.ExclusionTypes)Enum.Parse(typeof(ExclusionItem.ExclusionTypes), cboTypes.SelectedIndex.ToString());
         item.Value = txtValue.Text;

         // file extension needs to be in format .xxxx
         //if (item.Type == ExclusionItem.ExclusionTypes.FileExtension && (!item.Value.StartsWith(".") || item.Value.Length == 1))
         //{
         //   MessageBox.Show(this, Language.GetGenericText("Exclusions.Error.InvalidExtensionFormat"), Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         //   return null;
         //}

         // set option type
         if (cboOptions.SelectedIndex == -1)
            item.Option = ExclusionItem.OptionsTypes.None;
         else
            item.Option = (ExclusionItem.OptionsTypes)Enum.Parse(typeof(ExclusionItem.OptionsTypes), cboOptions.SelectedIndex.ToString());

         // if not an extension, then options must be something other than None
         if (item.Type != ExclusionItem.ExclusionTypes.FileExtension && item.Option == ExclusionItem.OptionsTypes.None)
         {
            MessageBox.Show(this, Language.GetGenericText("Exclusions.Error.InvalidOptions"), Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return null;
         }

         // set ignore case
         if (item.Type != ExclusionItem.ExclusionTypes.FileExtension)
            item.IgnoreCase = chkIgnoreCase.Checked;
         else
            item.IgnoreCase = false;

         return item;
      }
   }
}
