using System;
using System.Windows.Forms;

namespace AstroGrep.Windows.Forms
{
   /// <summary>
   /// Used to Add/Edit a Text Editor for a specified file type.
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
   /// [Curtis_Beard]		07/21/2006	Created
   /// [Justin_Dearing]    11/01/2007	REMOVE: Unneccessary global variable
   /// [Andrew_Radford]    17/08/2008	CHG: Moved Winforms designer stuff to a .designer file
   /// [Curtis_Beard]      09/28/2012  CHG: 3553474, support multiple file types per editor (display message)
   /// </history>
	public partial class frmAddEditTextEditor : Form
	{
      #region Declarations
      private bool __Add = true;
      private bool __AllTypesDefined = false;
      private string __FileType;
      private string __OriginalFileType = string.Empty;
      private string __Location;
      private string __CmdArgs;
      private string[] __ExistingFileTypes = null;
      #endregion


      private System.ComponentModel.IContainer components;

      /// <summary>
      /// Creates an instance of the frmAddEditTextEditor class.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
		public frmAddEditTextEditor()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      #region Properties
      /// <summary>
      /// Determines whether the control is in Addition mode.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      public bool IsAdd
      {
         set { __Add = value; }
      }

      /// <summary>
      /// Determines whether the All File Types has already been used.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      public bool IsAllTypesDefined
      {
         set
         {
            __AllTypesDefined = value;
            if (__AllTypesDefined)
               // one is defined so don't display
               lblAllTypesMessage.Visible = false;
            else
               // not defined so display message
               lblAllTypesMessage.Visible = true;
         }
      }

      /// <summary>
      /// Contains the file type.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      public string TextEditorFileType
      {
         get { return __FileType; }
         set
         {
            __OriginalFileType = value;
            __FileType = value;
         }
      }

      /// <summary>
      /// Contains the location.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      public string TextEditorLocation
      {
         get { return __Location; }
         set { __Location = value; }
      }

      /// <summary>
      /// Contains the command line arguments.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      public string TextEditorCommandLine
      {
         get { return __CmdArgs; }
         set { __CmdArgs = value; }
      }

      /// <summary>
      /// Contains the current file types defined.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		04/20/2007	Created
      /// </history>
      public string[] ExistingFileTypes
      {
         get { return __ExistingFileTypes; }
         set { __ExistingFileTypes = value; }
      }
      #endregion

      #region Events
      /// <summary>
      /// Setup the form for display.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// [Justin_Dearing]      11/01/2007	REMOVE unneccessary code
      /// </history>
      private void frmAddEditTextEditor_Load(object sender, System.EventArgs e)
      {
         if (!__Add)
         {
            // load values into text boxes
            txtFileType.Text = __FileType;
            txtTextEditorLocation.Text = __Location;
            txtCmdLineArgs.Text = __CmdArgs;
         }

         lblCmdOptions.Text = "Command Line Optons:\r\n" +
                              "  %1 - File\r\n" +
                              "  %2 - Line Number\r\n" +
                              "  %3 - Column";

         //Language.GenerateXml(this, Application.StartupPath + "\\" + this.Name + ".xml");
         Language.ProcessForm(this, HoverTips);

         lblCmdOptionsView.Text = RetrieveCmdLineViewText();
      }

      /// <summary>
      /// Save the user selected items and close the form.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// [Curtis_Beard]		05/22/2007	FIX: 1646328, check when adding a new extension
      /// </history>
      private void btnOK_Click(object sender, System.EventArgs e)
      {
         // validate not an existing file type
         bool exists = false;
         if (ExistingFileTypes != null)
         {
            foreach (string fileType in ExistingFileTypes)
            {
               if (fileType.ToLower().Equals(txtFileType.Text.ToLower()))
               {
                  exists = true;
                  break;
               }
            }
            if (exists)
            {
               this.DialogResult = DialogResult.None;
               MessageBox.Show(this, Language.GetGenericText("TextEditorsErrorFileTypeExists"), 
                  Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
               return;
            }
         }

         // validate that an editor exists
         if (txtTextEditorLocation.Text.Length < 1)
         {
            this.DialogResult = DialogResult.None;
            MessageBox.Show(this, Language.GetGenericText("TextEditorsErrorNoEditor"), 
               Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
         }

         // validate cmdline has at least %1
         if (txtCmdLineArgs.Text.IndexOf("%1") == -1)
         {
            txtCmdLineArgs.Text = "%1";
         }

         // load values from text boxes
         __FileType = txtFileType.Text;
         __Location = txtTextEditorLocation.Text;
         __CmdArgs = txtCmdLineArgs.Text;

         this.DialogResult = DialogResult.OK;
         this.Close();
      }

      /// <summary>
      /// Close the form.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      private void btnCancel_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      /// <summary>
      /// Allow selection of an executable file.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      private void btnBrowse_Click(object sender, System.EventArgs e)
      {
         OpenFileDialog dlg = new OpenFileDialog();
         dlg.Filter = "Executables (*.exe)|*.exe|All Files (*.*)|*.*";
         dlg.Title = Language.GetGenericText("TextEditorsBrowseTitle");
         dlg.Multiselect = false;

         if (dlg.ShowDialog(this) == DialogResult.OK)
         {
            txtTextEditorLocation.Text = dlg.FileName;
            lblCmdOptionsView.Text = RetrieveCmdLineViewText();
         }

         dlg.Dispose();
      }

      /// <summary>
      /// Update the preview display.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      private void txtCmdLineArgs_TextChanged(object sender, System.EventArgs e)
      {
         lblCmdOptionsView.Text = RetrieveCmdLineViewText();
      }
      
      /// <summary>
      /// Update the preview display.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Justin_Dearing]		11/01/2007	Created
      /// </history>
      void txtTextEditorLocation_TextChanged(object sender, EventArgs e)
      {
         lblCmdOptionsView.Text = RetrieveCmdLineViewText();	
      }

      /// <summary>
      /// Checks to make sure the all file types (*) is used only once
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      private void txtFileType_TextChanged(object sender, System.EventArgs e)
      {
         if (__AllTypesDefined && !__OriginalFileType.Equals(Constants.ALL_FILE_TYPES))
         {
            if (txtFileType.Text.Equals(Constants.ALL_FILE_TYPES))
            {
               MessageBox.Show(Language.GetGenericText("TextEditorsAllTypesDefined"), Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
               txtFileType.Text = string.Empty;
            }
         }
      }
      #endregion

      #region Private Methods
      /// <summary>
      /// Returns a preview of what the command line will look like to open a file
      /// </summary>
      /// <returns>Preview of command line</returns>
      /// <history>
      /// [Curtis_Beard]      06/13/2005	ADD: Better cmd line arg support
      /// [Curtis_Beard]      07/26/2006	ADD: 1512026, column
      /// [Justin_Dearing]    11/01/2007	ADD: Preview will show editor name.
      /// </history>
      private string RetrieveCmdLineViewText()
      {
      	
         const string _previewText = "Preview: \"{0} {1}\"";
      	 string _editor = "editor.exe";
      	 string _editorPath = txtTextEditorLocation.Text;
         string _text = txtCmdLineArgs.Text;
         
         try {
            if (!string.IsNullOrEmpty(_editorPath))
            {
               _editor = System.IO.Path.GetFileName(_editorPath);
            }
         }
         catch {
            _editor = "editor.exe";
         }

         _text = _text.Replace("%1", "file.txt");
         _text = _text.Replace("%2", "450");
         _text = _text.Replace("%3", "11");

         return string.Format(_previewText, _editor, _text);
      }
      #endregion
	}
}
