using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using libAstroGrep;

namespace AstroGrep.Windows.Forms
{
   /// <summary>
   /// Used to edit a single file encoding item.
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
   /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
   /// </history>
   public partial class frmAddEditFileEncoding : Form
   {
      /// <summary>
      /// The currently selected FileEncoding.
      /// </summary>
      public FileEncoding SelectedFileEncoding
      {
         get;
         set;
      }

      /// <summary>
      /// Initialize the form.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// </history>
      public frmAddEditFileEncoding()
      {
         InitializeComponent();

         // load combo box with available encodings
         cboEncodings.DisplayMember = "DisplayName";
         cboEncodings.ValueMember = "CodePage";
         cboEncodings.DataSource = Encoding.GetEncodings();
      }

      /// <summary>
      /// Setup the form with language and selected values.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// </history>
      private void frmAddEditForceEncodingFile_Load(object sender, EventArgs e)
      {
         Language.ProcessForm(this, toolTip1);

         if (SelectedFileEncoding != null)
         {
            txtFile.Text = SelectedFileEncoding.FilePath;
            cboEncodings.SelectedValue = SelectedFileEncoding.Encoding.CodePage;
         }
      }

      /// <summary>
      /// Handle the browse file click event.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// </history>
      private void picBrowse_Click(object sender, EventArgs e)
      {
         var dlg = new OpenFileDialog();
         dlg.Multiselect = false;

         // set initial directory if valid
         if (System.IO.File.Exists(txtFile.Text))
         {
            dlg.FileName = txtFile.Text;
         }

         // display dialog and setup path if selected
         if (dlg.ShowDialog(this) == DialogResult.OK)
         {
            txtFile.Text = dlg.FileName;
         }
      }

      /// <summary>
      /// Handle the OK click event (verify and set entry).
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// </history>
      private void btnOK_Click(object sender, EventArgs e)
      {
         this.DialogResult = System.Windows.Forms.DialogResult.None;

         var file = VerifyInterface();
         if (file != null)
         {
            SelectedFileEncoding = file;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
         }
      }

      /// <summary>
      /// Handle the Cancel click event.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// </history>
      private void btnCancel_Click(object sender, EventArgs e)
      {
         this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.Close();
      }

      /// <summary>
      /// Verify the interface values are valid.
      /// </summary>
      /// <returns>FileEncoding object based on inputs, null if not valid (displays message to user)</returns>
      /// <history>
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// </history>
      private FileEncoding VerifyInterface()
      {
         if (string.IsNullOrEmpty(txtFile.Text) || !System.IO.File.Exists(txtFile.Text))
         {
            MessageBox.Show(this, "Please select a valid file.", Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return null;
         }

         if (cboEncodings.SelectedItem == null)
         {
            MessageBox.Show(this, "Please select an encoding.", Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return null;
         }

         FileEncoding file = new FileEncoding();
         file.Enabled = true;
         file.FilePath = txtFile.Text;
         file.Encoding = Encoding.GetEncoding((int)cboEncodings.SelectedValue);

         return file;
      }
   }
}
