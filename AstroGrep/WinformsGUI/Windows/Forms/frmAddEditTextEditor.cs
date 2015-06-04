using System;
using System.Collections.Generic;
using System.Windows.Forms;

using AstroGrep.Common;

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
      private string __OriginalFileType = string.Empty;
      private TextEditor __Editor = null;
      private List<string> __ExistingFileTypes = null;
      #endregion

      /// <summary>
      /// Creates an instance of the frmAddEditTextEditor class.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      public frmAddEditTextEditor()
      {
         InitializeComponent();
      }

      #region Properties

      /// <summary>
      /// Gets/Sets the current TextEditor.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		10/12/2012	Created
      /// </history>
      public TextEditor Editor
      {
         get { return __Editor; }
         set
         {
            __Editor = value;
            __OriginalFileType = value.FileType;
         }
      }

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
      /// Contains the current file types defined.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		04/20/2007	Created
      /// [Curtis_Beard]		08/13/2014	FIX: better detection of file types
      /// </history>
      public List<string> ExistingFileTypes
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
      /// [Justin_Dearing]    11/01/2007	REMOVE unneccessary code
      /// [Curtis_Beard]		03/06/2015	FIX: 65, add support for using quotes around file name
      /// </history>
      private void frmAddEditTextEditor_Load(object sender, System.EventArgs e)
      {
         if (!__Add)
         {
            // load values into text boxes
            txtFileType.Text = Editor.FileType;
            txtTextEditorLocation.Text = Editor.Editor;
            txtCmdLineArgs.Text = Editor.Arguments;
            chkUseQuotesAroundFileName.Checked = Editor.UseQuotesAroundFileName;
            updwnTabSize.Value = Editor.TabSize;
         }

         lblCmdOptions.Text = "Command Line Optons:\r\n" +
                              "  %1 - File\r\n" +
                              "  %2 - Line Number\r\n" +
                              "  %3 - Column";

         //Language.GenerateXml(this, Application.StartupPath + "\\" + this.Name + ".xml");
         Language.ProcessForm(this, HoverTips);

         UpdateCmdLinePreview();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="findFileType"></param>
      /// <param name="searchFileType"></param>
      /// <returns></returns>
      private bool FindType(string findFileType, string searchFileType)
      {
         bool foundType = false;

         if (searchFileType.Contains(Constants.TEXT_EDITOR_TYPE_SEPARATOR))
         {
            foreach (string val in searchFileType.Split(Constants.TEXT_EDITOR_TYPE_SEPARATOR.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
               if (IsFileTypeEqual(findFileType, val))
               {
                  foundType = true;
                  break;
               }
            }
         }
         else if (IsFileTypeEqual(findFileType, searchFileType))
         {
            foundType = true;
         }

         return foundType;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="type1"></param>
      /// <param name="type2"></param>
      /// <returns></returns>
      private bool IsFileTypeEqual(string type1, string type2)
      {
         if (type1.StartsWith(".") && !type2.StartsWith("."))
         {
            if (type1.ToLower().Equals("." + type2.ToLower()))
            {
               return true;
            }
         }
         else if (!type1.StartsWith(".") && type2.StartsWith("."))
         {
            if (("." + type1.ToLower()).Equals(type2.ToLower()))
            {
               return true;
            }
         }
         else if (type1.ToLower().Equals(type2.ToLower()))
         {
            return true;
         }

         return false;
      }

      /// <summary>
      /// Save the user selected items and close the form.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// [Curtis_Beard]		05/22/2007	FIX: 1646328, check when adding a new extension
      /// [Curtis_Beard]		08/13/2014	FIX: better detection of file types
      /// [Curtis_Beard]	   08/13/2014	ADD: 80, add ability to open default app when no editor is specified
      /// </history>
      private void btnOK_Click(object sender, System.EventArgs e)
      {
         // validate not an existing file type
         bool exists = false;
         if (ExistingFileTypes != null)
         {
            if (__Add || !__OriginalFileType.Equals(txtFileType.Text))
            {
               foreach (string fileType in ExistingFileTypes)
               {
                  if (fileType.Contains(Constants.TEXT_EDITOR_TYPE_SEPARATOR))
                  {
                     foreach (string val in fileType.Split(Constants.TEXT_EDITOR_TYPE_SEPARATOR.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                     {
                        if (FindType(val, txtFileType.Text))
                        {
                           exists = true;
                           break;
                        }
                     }
                  }
                  else if (FindType(fileType, txtFileType.Text))
                  {
                     exists = true;
                     break;
                  }
               }

               if (exists)
               {
                  this.DialogResult = DialogResult.None;
                  MessageBox.Show(this, Language.GetGenericText("TextEditorsErrorFileTypeExists"),
                     ProductInformation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                  return;
               }
            }
         }

         // validate cmdline has at least %1
         if (txtCmdLineArgs.Text.IndexOf("%1") == -1)
         {
            txtCmdLineArgs.Text = "%1";
         }

         // load values from text boxes
         __Editor = new TextEditor(txtFileType.Text, txtTextEditorLocation.Text, txtCmdLineArgs.Text, Convert.ToInt32(updwnTabSize.Value), chkUseQuotesAroundFileName.Checked);

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
      /// [Curtis_Beard]		03/06/2015	rename preview function
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
            UpdateCmdLinePreview();
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
      /// [Curtis_Beard]		03/06/2015	rename preview function
      /// </history>
      private void txtCmdLineArgs_TextChanged(object sender, System.EventArgs e)
      {
         UpdateCmdLinePreview();
      }

      /// <summary>
      /// Update the preview display.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Justin_Dearing]		11/01/2007	Created
      /// [Curtis_Beard]		03/06/2015	rename preview function
      /// </history>
      private void txtTextEditorLocation_TextChanged(object sender, EventArgs e)
      {
         UpdateCmdLinePreview();
      }

      /// <summary>
      /// Update the preview display.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		03/06/2015	FIX: 65, add support for using quotes around file name
      /// </history>
      private void chkUseQuotesAroundFileName_CheckedChanged(object sender, EventArgs e)
      {
         UpdateCmdLinePreview();
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
               MessageBox.Show(Language.GetGenericText("TextEditorsAllTypesDefined"), ProductInformation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
      /// [Curtis_Beard]		03/06/2015	FIX: 65, add support for using quotes around file name, rename function
      /// </history>
      private void UpdateCmdLinePreview()
      {
         const string previewText = "Preview: {0} {1}";
         string editor = string.Empty;
         string editorPath = txtTextEditorLocation.Text;
         string args = txtCmdLineArgs.Text;

         try
         {
            if (!string.IsNullOrEmpty(editorPath))
            {
               editor = System.IO.Path.GetFileName(editorPath);
            }
         }
         catch
         {
            editor = string.Empty;
         }

         string path = @"c:\file path\filename.txt";
         if (chkUseQuotesAroundFileName.Checked)
         {
            path = "\"" + path + "\"";
         }
         args = args.Replace("%1", path);
         args = args.Replace("%2", "450");
         args = args.Replace("%3", "11");

         lblCmdOptionsView.Text = string.Format(previewText, editor, args);
      }
      #endregion
   }
}
