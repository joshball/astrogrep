using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using AstroGrep.Common;
using AstroGrep.Core;
using libAstroGrep;
using libAstroGrep.EncodingDetection;
using libAstroGrep.Plugin;

namespace AstroGrep.Windows.Forms
{
   /// <summary>
   /// Used to display the options dialog.
   /// </summary>
   /// <remarks>
   /// AstroGrep File Searching Utility. Written by Theodore L. Ward
   /// Copyright (C) 2002 AstroComma Incorporated.
   /// 
   /// This program is free software; you can redistribute it and/or
   /// modify it under the terms of the GNU General public License
   /// as published by the Free Software Foundation; either version 2
   /// of the License, or (at your option) any later version.
   /// 
   /// This program is distributed in the hope that it will be useful,
   /// but WITHOUT ANY WARRANTY; without even the implied warranty of
   /// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   /// GNU General public License for more details.
   /// 
   /// You should have received a copy of the GNU General public License
   /// along with this program; if not, write to the Free Software
   /// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
   /// 
   /// The author may be contacted at:
   /// ted@astrocomma.com or curtismbeard@gmail.com
   /// </remarks>
   /// <history>
   /// [Curtis_Beard]		05/23/2007	Created
   /// [Curtis_Beard]		07/13/2007	ADD: system tray options
   /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions, remove file extension exclusion list
   /// </history>
   public partial class frmOptions : System.Windows.Forms.Form
   {
      private bool __LanguageChange = false;
      private bool __RightClickEnabled = false;
      private bool __RightClickUpdate = false;
      private bool __IsAdmin = API.UACHelper.HasAdminPrivileges();
      private Font __FileFont = Convertors.ConvertStringToFont(Core.GeneralSettings.FilePanelFont);
      private bool inhibitFileEncodingAutoCheck;

      /// <summary>
      /// Creates a new instance of the frmOptions class.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		05/23/2007	Created
      /// [Curtis_Beard]      10/09/2012	CHG: 3575507, handle UAC request for right click option
      /// </history>
      public frmOptions()
      {
         InitializeComponent();

         __RightClickEnabled = Shortcuts.IsSearchOption();

         ForeColorButton.ColorChange += new AstroGrep.Windows.Controls.ColorButton.ColorChangeHandler(NewColor);
         BackColorButton.ColorChange += new AstroGrep.Windows.Controls.ColorButton.ColorChangeHandler(NewColor);
         btnResultsWindowForeColor.ColorChange += new AstroGrep.Windows.Controls.ColorButton.ColorChangeHandler(NewColor);
         btnResultsWindowBackColor.ColorChange += new AstroGrep.Windows.Controls.ColorButton.ColorChangeHandler(NewColor);
         btnResultsContextForeColor.ColorChange += new AstroGrep.Windows.Controls.ColorButton.ColorChangeHandler(NewColor);
         chkRightClickOption.CheckedChanged += new EventHandler(chkRightClickOption_CheckedChanged);

         // get mainform's image list to use here for up/down buttons
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
         System.Windows.Forms.ImageList ListViewImageList = new ImageList();
         ListViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListViewImageList.ImageStream")));
         ListViewImageList.TransparentColor = System.Drawing.Color.Transparent;
         ListViewImageList.Images.SetKeyName(0, "");
         ListViewImageList.Images.SetKeyName(1, "");

         btnUp.ImageList = ListViewImageList;
         btnUp.ImageIndex = 0;
         btnDown.ImageList = ListViewImageList;
         btnDown.ImageIndex = 1;

         API.ListViewExtensions.SetTheme(lstFiles);
         API.ListViewExtensions.SetTheme(TextEditorsList);
         API.ListViewExtensions.SetTheme(PluginsList);
      }

      /// <summary>
      /// Checks to see if the language w changed.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/25/2006	Created
      /// </history>
      public bool IsLanguageChange
      {
         get { return __LanguageChange; }
      }

      /// <summary>
      /// Handles setting the user specified options into the correct controls for display.
      /// </summary>
      /// <param name="sender">System parameter</param>
      /// <param name="e">System parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/19/2006	Created
      /// [Curtis_Beard]		07/21/2006	ADD: Custom colors for fore/back of results
      /// [Curtis_Beard]		07/28/2006	ADD: extension exclusion list
      /// [Curtis_Beard]		10/11/2007	CHG: use language culture ids
      /// [Curtis_Beard]		01/24/2012	CHG: allow back color use again since using .Net v2+
      /// [Curtis_Beard]	   02/24/2012	CHG: 3488321, ability to change results font
      /// [Curtis_Beard]	   10/16/2012	CHG: Save search settings on exit
      /// [Curtis_Beard]	   10/28/2012	ADD: 3575509, results word wrap
      /// [Curtis_Beard]	   10/28/2012	ADD: 3479503, ability to change file list font
      /// [Curtis_Beard]	   02/04/2014	ADD: 66, option to detect file encoding
      /// [Curtis_Beard]	   09/16/2014	ADD: installer check to hide desktop/start menu options
      /// [Curtis_Beard]      10/27/2014	CHG: 85, remove leading white space
      /// [Curtis_Beard]      03/03/2015	CHG: 93, option to save messages form position
      /// [Curtis_Beard]	   04/08/2015	CHG: 81, remove old word wrap and white space options. now in frmMain.
      /// [Curtis_Beard]	   04/15/2015	CHG: add content forecolor
      /// [Curtis_Beard]	   05/26/2015	FIX: 69, add performance setting for file detection
      /// </history>
      private void frmOptions_Load(object sender, System.EventArgs e)
      {
         cboPathMRUCount.SelectedIndex = Core.GeneralSettings.MaximumMRUPaths - 1;
         chkRightClickOption.Checked = Shortcuts.IsSearchOption();
         if (Registry.IsInstaller())
         {
            chkDesktopShortcut.Visible = false;
            chkStartMenuShortcut.Visible = false;
         }
         else
         {
            chkDesktopShortcut.Checked = Shortcuts.IsDesktopShortcut();
            chkStartMenuShortcut.Checked = Shortcuts.IsStartMenuShortcut();
         }
         chkShowExclusionErrorMessage.Checked = Core.GeneralSettings.ShowExclusionErrorMessage;
         chkSaveSearchOptions.Checked = Core.GeneralSettings.SaveSearchOptionsOnExit;
         chkDetectFileEncoding.Checked = Core.GeneralSettings.DetectFileEncoding;
         chkUseEncodingCache.Checked = Core.GeneralSettings.UseEncodingCache;
         chkSaveMessagesPosition.Checked = Core.GeneralSettings.LogDisplaySavePosition;

         // ColorButton init
         ForeColorButton.SelectedColor = Convertors.ConvertStringToColor(Core.GeneralSettings.HighlightForeColor);
         BackColorButton.SelectedColor = Convertors.ConvertStringToColor(Core.GeneralSettings.HighlightBackColor);
         btnResultsWindowForeColor.SelectedColor = Convertors.ConvertStringToColor(Core.GeneralSettings.ResultsForeColor);
         btnResultsWindowBackColor.SelectedColor = Convertors.ConvertStringToColor(Core.GeneralSettings.ResultsBackColor);
         btnResultsContextForeColor.SelectedColor = Convertors.ConvertStringToColor(Core.GeneralSettings.ResultsContextForeColor);

         // results font
         rtxtResultsPreview.Font = Convertors.ConvertStringToFont(Core.GeneralSettings.ResultsFont);
         DisplayFont(rtxtResultsPreview.Font, lblCurrentFont);

         // file list font
         DisplayFont(__FileFont, lblFileCurrentFont);

         tbcOptions.SelectedTab = tabGeneral;

         LoadEditors(TextEditors.GetAll());
         LoadPlugins();
         LoadFileEncodings();

         //Language.GenerateXml(this, Application.StartupPath + "\\" + this.Name + ".xml");
         Language.ProcessForm(this);
         Language.LegacyLoadComboBox(cboLanguage);

         // set the user selected language
         if (cboLanguage.Items.Count > 0)
         {
            foreach (object oItem in cboLanguage.Items)
            {
               LanguageItem item = (LanguageItem)oItem;
               if (item.Culture.Equals(Core.GeneralSettings.Language))
               {
                  cboLanguage.SelectedItem = item;
                  break;
               }
            }
         }

         // setup the performance drop down list
         List<EncodingPerformance> performanceValues = new List<EncodingPerformance>();
         Array values = Enum.GetValues(typeof(EncodingOptions.Performance));
         foreach (EncodingOptions.Performance val in values)
         {
            performanceValues.Add(new EncodingPerformance() { Name = Language.GetGenericText(string.Format("FileEncoding.Performance.{0}", Enum.GetName(typeof(EncodingOptions.Performance), val))), Value = (int)val });
         }
         cboPerformance.DisplayMember = "Name";
         cboPerformance.ValueMember = "Value";
         cboPerformance.DataSource = performanceValues;
         cboPerformance.SelectedValue = GeneralSettings.EncodingPerformance;
         chkDetectFileEncoding_CheckedChanged(null, null);

         // set column text
         TextEditorsList.Columns[0].Text = Language.GetGenericText("TextEditorsColumnFileType");
         TextEditorsList.Columns[1].Text = Language.GetGenericText("TextEditorsColumnLocation");
         TextEditorsList.Columns[2].Text = Language.GetGenericText("TextEditorsColumnCmdLine");
         TextEditorsList.Columns[3].Text = Language.GetGenericText("TextEditorsColumnTabSize");
         PluginsList.Columns[0].Text = Language.GetGenericText("PluginsColumnEnabled");
         PluginsList.Columns[1].Text = Language.GetGenericText("PluginsColumnName");
         PluginsList.Columns[2].Text = Language.GetGenericText("PluginsColumnExtensions");
         lstFiles.Columns[0].Text = Language.GetGenericText("FileEncoding.Enabled", "Enabled");
         lstFiles.Columns[1].Text = Language.GetGenericText("FileEncoding.FilePath", "File Path");
         lstFiles.Columns[2].Text = Language.GetGenericText("FileEncoding.Encoding", "Encoding");
      }

      #region Private Methods
      /// <summary>
      /// Update the results preview.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// [Curtis_Beard]		01/24/2012	CHG: allow back color use again since using .Net v2+
      /// [Curtis_Beard]	   02/24/2012	CHG: 3488321, ability to change results font
      /// [Curtis_Beard]	   09/11/2014	FIX, don't play windows chime if matching text is at end of line
      /// [Curtis_Beard]	   04/08/2015	CHG: 81, remove old word wrap and white space options. now in frmMain.
      /// </history>
      private void UpdateResultsPreview()
      {
         string PREVIEW_TEXT = Language.GetGenericText("ResultsPreviewText");
         string PREVIEW_SPACER_TEXT = Language.GetGenericText("ResultsPreviewSpacerText");
         string PREVIEW_MATCH_TEXT = Language.GetGenericText("ResultsPreviewTextMatch");

         string _textToSearch = string.Empty;
         string _searchText = PREVIEW_MATCH_TEXT;
         string _tempLine = string.Empty;

         string _begin = string.Empty;
         string _text = string.Empty;
         string _end = string.Empty;
         int _pos = 0;
         bool _highlight = false;

         if (tbcOptions.SelectedTab == tabResults)
         {
            // Clear the contents
            rtxtResultsPreview.Text = string.Empty;
            rtxtResultsPreview.ForeColor = btnResultsWindowForeColor.SelectedColor;
            rtxtResultsPreview.BackColor = btnResultsWindowBackColor.SelectedColor;

            // Retrieve hit text
            _textToSearch = PREVIEW_TEXT;

            _textToSearch = string.Format("{0}{1}", PREVIEW_SPACER_TEXT, _textToSearch);

            // Set default font
            rtxtResultsPreview.SelectionFont = rtxtResultsPreview.Font;

            _tempLine = _textToSearch;

            // attempt to locate the text in the line
            _pos = _tempLine.IndexOf(_searchText);

            if (_pos > -1)
            {
               do
               {
                  _highlight = false;

                  //
                  // retrieve parts of text
                  _begin = _tempLine.Substring(0, _pos);
                  _text = _tempLine.Substring(_pos, _searchText.Length);
                  _end = _tempLine.Substring(_pos + _searchText.Length);

                  // set default color for starting text
                  rtxtResultsPreview.SelectionColor = btnResultsWindowForeColor.SelectedColor;
                  rtxtResultsPreview.SelectionBackColor = btnResultsWindowBackColor.SelectedColor;
                  rtxtResultsPreview.SelectedText = _begin;

                  // do a check to see if begin and end are valid for wholeword searches
                  _highlight = true;

                  // set highlight color for searched text
                  if (_highlight)
                  {
                     rtxtResultsPreview.SelectionColor = ForeColorButton.SelectedColor;
                     rtxtResultsPreview.SelectionBackColor = BackColorButton.SelectedColor;
                  }
                  rtxtResultsPreview.SelectedText = _text;

                  // Check remaining string for other hits in same line
                  _pos = _end.IndexOf(_searchText);

                  // set default color for end, if no more hits in line
                  _tempLine = _end;
                  if (_pos < 0)
                  {
                     rtxtResultsPreview.SelectionColor = btnResultsWindowForeColor.SelectedColor;
                     rtxtResultsPreview.SelectionBackColor = btnResultsWindowBackColor.SelectedColor;
                     if (_end.Length > 0)
                     {
                        rtxtResultsPreview.SelectedText = _end;
                     }
                  }

               } while (_pos > -1);
            }
            else
            {
               // set default color, no search text found
               rtxtResultsPreview.SelectionColor = btnResultsWindowForeColor.SelectedColor;
               rtxtResultsPreview.SelectionBackColor = btnResultsWindowBackColor.SelectedColor;
               rtxtResultsPreview.SelectedText = _textToSearch;
            }
         }
      }

      /// <summary>
      /// Handle when a new color has been selected.
      /// </summary>
      /// <param name="newColor">new Color</param>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      private void NewColor(Color newColor)
      {
         UpdateResultsPreview();
      }

      /// <summary>
      /// Loads the given text editors.
      /// </summary>
      /// <param name="editors">TextEditor array, can be nothing</param>
      /// <history>
      /// [Curtis_Beard]	   07/10/2006	Created
      /// [Curtis_Beard]		03/06/2015	FIX: 65, support use quotes around file name
      /// </history>
      private void LoadEditors(TextEditor[] editors)
      {
         if (editors != null)
         {
            TextEditorsList.BeginUpdate();
            foreach (TextEditor editor in editors)
            {
               ListViewItem item = new ListViewItem();
               item.Text = editor.FileType;
               item.SubItems.Add(editor.Editor);
               item.SubItems.Add(editor.Arguments);
               item.SubItems.Add(editor.TabSize.ToString());
               item.Selected = true;
               item.Tag = editor;
               TextEditorsList.Items.Add(item);
            }
            TextEditorsList.EndUpdate();
         }
      }

      /// <summary>
      /// Saves the defined text editors.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   07/10/2006	Created
      /// [Curtis_Beard]		03/06/2015	FIX: 65, support use quotes around file name
      /// </history>
      private void SaveEditors()
      {
         if (TextEditorsList.Items.Count == 0)
         {
            TextEditors.Save(null);
            return;
         }

         TextEditor[] editors = new TextEditor[TextEditorsList.Items.Count];
         int index = 0;
         foreach (ListViewItem item in TextEditorsList.Items)
         {
            editors[index] = item.Tag as TextEditor;
            index += 1;
         }

         TextEditors.Save(editors);
      }

      /// <summary>
      /// Sets the TextEditor's button states depending on if one is selected.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/20/2006	Created
      /// </history>
      private void SetTextEditorsButtonState()
      {
         if (TextEditorsList.SelectedItems.Count > 0)
         {
            btnRemove.Enabled = true;
            btnEdit.Enabled = true;
         }
         else
         {
            btnRemove.Enabled = false;
            btnEdit.Enabled = false;
         }
      }

      /// <summary>
      /// Determines if a text editor is defined for all file types.
      /// </summary>
      /// <returns>Returns true if an all file types text editor is defined, false otherwise</returns>
      /// <history>
      /// [Curtis_Beard]		07/20/2006	Created
      /// </history>
      private bool IsAllTypesDefined()
      {
         foreach (ListViewItem item in TextEditorsList.Items)
         {
            if (item.Text.Equals("*"))
               return true;
         }

         return false;
      }

      /// <summary>
      /// Retrieves an array of file types currently defined.
      /// </summary>
      /// <returns>String array of file types</returns>
      /// <history>
      /// [Curtis_Beard]		08/13/2014	FIX: better detection of file types
      /// </history>
      private List<string> GetExistingFileTypes()
      {
         List<string> types = new List<string>();

         for (int i = 0; i < TextEditorsList.Items.Count; i++)
         {
            string value = TextEditorsList.Items[i].Text;
            if (value.Contains(Constants.TEXT_EDITOR_TYPE_SEPARATOR))
            {
               foreach (string val in value.Split(Constants.TEXT_EDITOR_TYPE_SEPARATOR.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
               {
                  types.Add(val.ToLower());
               }
            }
            else
            {
               types.Add(value.ToLower());
            }
         }

         return types;
      }

      /// <summary>
      /// Displays the given font as a string on the given label.
      /// </summary>
      /// <param name="fnt">Font to display</param>
      /// <param name="lbl">Label to show font</param>
      /// <history>
      /// [Curtis_Beard]	   10/10/2012	ADD: 3479503, ability to change file list font
      /// [Curtis_Beard]	   10/26/2012	CHG: use / to separate values instead of comma which could be used in SizeInPoints
      /// </history>
      private void DisplayFont(Font fnt, Label lbl)
      {
         lbl.Text = string.Format("{0} / {1} / {2}", fnt.Name, fnt.SizeInPoints, fnt.Style.ToString());
      }
      #endregion

      #region Control Events
      /// <summary>
      /// Handles saving the user specified options.
      /// </summary>
      /// <param name="sender">System parameter</param>
      /// <param name="e">System parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/19/2006	Created
      /// [Curtis_Beard]		07/21/2006	ADD: Custom colors for fore/back of results
      /// [Curtis_Beard]		07/25/2006	FIX: Add back Browse... if it was removed
      /// [Curtis_Beard]      07/28/2006  ADD: extension exclusion list
      /// [Curtis_Beard]      11/10/2006  FIX: Don't load new language, just set that it changed
      /// [Curtis_Beard]      11/13/2006  CHG: Only try and save the search option if enabled
      /// [Curtis_Beard]		10/11/2007	CHG: use language culture ids
      /// [Curtis_Beard]		01/24/2012	CHG: allow back color use again since using .Net v2+
      /// [Curtis_Beard]	   02/24/2012	CHG: 3488321, ability to change results font
      /// [Curtis_Beard]	   10/16/2012	CHG: Save search settings on exit
      /// [Curtis_Beard]	   10/28/2012	ADD: 3575509, results word wrap
      /// [Curtis_Beard]	   10/28/2012	CHG: 3575507, handle UAC request for right click option
      /// [Curtis_Beard]	   10/28/2012	ADD: 3479503, ability to change file list font
      /// [Curtis_Beard]	   02/04/2014	ADD: 66, option to detect file encoding
      /// [Curtis_Beard]	   09/16/2014	ADD: installer check for desktop/start menu options processing
      /// [Curtis_Beard]      10/27/2014	CHG: 85, remove leading white space
      /// [Curtis_Beard]      03/03/2015	CHG: 93, option to save messages form position
      /// [Curtis_Beard]	   04/08/2015	CHG: 81, remove old word wrap and white space options. now in frmMain.
      /// [Curtis_Beard]	   04/15/2015	CHG: add content forecolor
      /// [Curtis_Beard]	   05/26/2015	FIX: 69, add performance setting for file detection
      /// </history>
      private void btnOK_Click(object sender, System.EventArgs e)
      {
         // Store the values in the globals
         Core.GeneralSettings.MaximumMRUPaths = cboPathMRUCount.SelectedIndex + 1;
         Core.GeneralSettings.HighlightForeColor = Convertors.ConvertColorToString(ForeColorButton.SelectedColor);
         Core.GeneralSettings.HighlightBackColor = Convertors.ConvertColorToString(BackColorButton.SelectedColor);
         Core.GeneralSettings.ResultsForeColor = Convertors.ConvertColorToString(btnResultsWindowForeColor.SelectedColor);
         Core.GeneralSettings.ResultsBackColor = Convertors.ConvertColorToString(btnResultsWindowBackColor.SelectedColor);
         Core.GeneralSettings.ResultsContextForeColor = Convertors.ConvertColorToString(btnResultsContextForeColor.SelectedColor);
         Core.GeneralSettings.ResultsFont = Convertors.ConvertFontToString(rtxtResultsPreview.Font);
         Core.GeneralSettings.ShowExclusionErrorMessage = chkShowExclusionErrorMessage.Checked;
         Core.GeneralSettings.SaveSearchOptionsOnExit = chkSaveSearchOptions.Checked;
         Core.GeneralSettings.FilePanelFont = Convertors.ConvertFontToString(__FileFont);
         Core.GeneralSettings.DetectFileEncoding = chkDetectFileEncoding.Checked;
         Core.GeneralSettings.EncodingPerformance = (int)cboPerformance.SelectedValue;
         Core.GeneralSettings.UseEncodingCache = chkUseEncodingCache.Checked;
         Core.GeneralSettings.LogDisplaySavePosition = chkSaveMessagesPosition.Checked;

         // Only load new language on a change
         LanguageItem item = (LanguageItem)cboLanguage.SelectedItem;
         if (!Core.GeneralSettings.Language.Equals(item.Culture))
         {
            Core.GeneralSettings.Language = item.Culture;
            Language.Load(Core.GeneralSettings.Language);
            __LanguageChange = true;
         }

         // set shortcuts
         if (!Registry.IsInstaller())
         {
            Shortcuts.SetDesktopShortcut(chkDesktopShortcut.Checked);
            Shortcuts.SetStartMenuShortcut(chkStartMenuShortcut.Checked);
         }

         SaveEditors();

         SaveFileEncodings();

         Core.PluginManager.Save();

         // handle right click search change
         if (__RightClickUpdate)
         {
            try
            {
               string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "AstroGrep.AdminProcess.exe");
               string agPath = string.Format("\"{0}\"", Application.ExecutablePath);
               string explorerText = string.Format("\"{0}\"", Language.GetGenericText("SearchExplorerItem"));
               string args = string.Format("\"{0}\" {1} {2}", chkRightClickOption.Checked.ToString(), agPath, explorerText);

               API.UACHelper.AttemptPrivilegeEscalation(path, args, false);
            }
            catch (Exception ex)
            {
               // TODO
               System.Diagnostics.Debug.WriteLine(ex.Message);
            }
         }

         this.Close();
      }

      /// <summary>
      /// Closes the form
      /// </summary>
      /// <param name="sender">System parameter</param>
      /// <param name="e">System parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/19/2006	Created
      /// </history>
      private void btnCancel_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      /// <summary>
      /// Add a new text editor.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/20/2006	Created
      /// [Curtis_Beard]		03/06/2015	FIX: 65, support use quotes around file name
      /// </history>
      private void btnAdd_Click(object sender, System.EventArgs e)
      {
         if (tbcOptions.SelectedTab == tabTextEditors)
         {
            frmAddEditTextEditor dlg = new frmAddEditTextEditor();
            dlg.IsAdd = true;
            dlg.IsAllTypesDefined = IsAllTypesDefined();
            dlg.ExistingFileTypes = GetExistingFileTypes();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
               // create new entry
               ListViewItem item = new ListViewItem();
               item.Tag = dlg.Editor;
               item.Text = dlg.Editor.FileType;
               item.SubItems.Add(dlg.Editor.Editor);
               item.SubItems.Add(dlg.Editor.Arguments);
               item.SubItems.Add(dlg.Editor.TabSize.ToString());
               TextEditorsList.Items.Add(item);

               SetTextEditorsButtonState();
            }
         }

         this.DialogResult = DialogResult.None;
      }

      /// <summary>
      /// Edit the selected text editor.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/20/2006	Created
      /// [Curtis_Beard]		08/13/2014	FIX: better detection of file types
      /// [Curtis_Beard]		03/06/2015	FIX: 65, support use quotes around file name
      /// </history>
      private void btnEdit_Click(object sender, System.EventArgs e)
      {
         if (tbcOptions.SelectedTab == tabTextEditors)
         {
            if (TextEditorsList.SelectedItems.Count > 0)
            {
               ListViewItem item = TextEditorsList.SelectedItems[0];
               frmAddEditTextEditor dlg = new frmAddEditTextEditor();

               // set values
               dlg.IsAdd = false;
               dlg.IsAllTypesDefined = IsAllTypesDefined();
               dlg.Editor = item.Tag as TextEditor;
               dlg.ExistingFileTypes = GetExistingFileTypes();

               if (dlg.ShowDialog(this) == DialogResult.OK)
               {
                  // get values
                  TextEditorsList.SelectedItems[0].Tag = dlg.Editor;
                  TextEditorsList.SelectedItems[0].Text = dlg.Editor.FileType;
                  TextEditorsList.SelectedItems[0].SubItems[1].Text = dlg.Editor.Editor;
                  TextEditorsList.SelectedItems[0].SubItems[2].Text = dlg.Editor.Arguments;
                  TextEditorsList.SelectedItems[0].SubItems[3].Text = dlg.Editor.TabSize.ToString();
               }

               SetTextEditorsButtonState();
            }
         }

         this.DialogResult = DialogResult.None;
      }

      /// <summary>
      /// Delete the selected text editor.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/20/2006	Created
      /// </history>
      private void btnRemove_Click(object sender, System.EventArgs e)
      {
         if (tbcOptions.SelectedTab == tabTextEditors)
         {
            // remove
            if (TextEditorsList.SelectedItems.Count > 0)
            {
               TextEditorsList.Items.Remove(TextEditorsList.SelectedItems[0]);
               SetTextEditorsButtonState();
            }
         }

         this.DialogResult = DialogResult.None;
      }

      /// <summary>
      /// Update the text editor button states.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/20/2006	Created
      /// </history>
      private void TextEditorsList_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         SetTextEditorsButtonState();
      }

      /// <summary>
      /// Edit the selected text editor entry.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/20/2006	Created
      /// </history>
      private void TextEditorsList_DoubleClick(object sender, EventArgs e)
      {
         Point clientPoint = TextEditorsList.PointToClient(Control.MousePosition);
         ListViewItem item = TextEditorsList.GetItemAt(clientPoint.X, clientPoint.Y);

         if (item != null)
         {
            item.Selected = true;
            btnEdit_Click(null, null);
         }
      }

      /// <summary>
      /// Setup tab pages when selected.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		05/22/2007	Created
      /// </history>
      private void tbcOptions_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (tbcOptions.SelectedTab == tabResults)
         {
            UpdateResultsPreview();
         }
         else if (tbcOptions.SelectedTab == tabPlugins)
         {
            if (PluginsList.Items.Count > 0)
            {
               PluginsList.Items[0].Selected = true;
            }
         }
      }


      /// <summary>
      /// Show font selection dialog.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   02/24/2012	CHG: 3488321, ability to change results font
      /// </history>
      private void btnFindFont_Click(object sender, EventArgs e)
      {
         var dlg = new FontDialog()
         {
            ShowColor = false,
            ShowEffects = false,
            ShowHelp = false,
            Font = rtxtResultsPreview.Font
         };

         var result = dlg.ShowDialog(this);
         if (result == DialogResult.OK)
         {
            DisplayFont(dlg.Font, lblCurrentFont);
            rtxtResultsPreview.Font = dlg.Font;
         }
      }

      /// <summary>
      /// Show font selection dialog.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   10/10/2012	ADD: 3479503, ability to change file list font
      /// </history>
      private void btnFileFindFont_Click(object sender, EventArgs e)
      {
         var dlg = new FontDialog()
         {
            ShowColor = false,
            ShowEffects = false,
            ShowHelp = false,
            Font = __FileFont
         };

         var result = dlg.ShowDialog(this);
         if (result == DialogResult.OK)
         {
            DisplayFont(dlg.Font, lblFileCurrentFont);
            __FileFont = dlg.Font;
         }
      }

      /// <summary>
      /// Handle change to the right click checkbox.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      10/09/2012	Initial: 3575507, handle UAC request for right click option
      /// </history>
      private void chkRightClickOption_CheckedChanged(object sender, EventArgs e)
      {
         if (chkRightClickOption.Checked != __RightClickEnabled)
         {
            if (!__IsAdmin)
            {
               API.UACHelper.AddShieldToButton(btnOK);
            }
            __RightClickUpdate = true;
         }
         else
         {
            if (!__IsAdmin)
            {
               API.UACHelper.RemoveShieldFromButton(btnOK);
            }
            __RightClickUpdate = false;
         }
      }

      /// <summary>
      /// Update results preview display when remove leading white space checkbox changes.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      10/27/2014	CHG: 85, remove leading white space
      /// </history>
      private void chkRemoveLeadingWhiteSpace_CheckedChanged(object sender, EventArgs e)
      {
         UpdateResultsPreview();
      }

      #endregion

      #region Plugin Methods
      /// <summary>
      /// Load the plugins from the manager to the listview.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/28/2006	Created
      /// </history>
      private void LoadPlugins()
      {
         PluginsList.Items.Clear();
         ListViewItem item;

         for (int i = 0; i < Core.PluginManager.Items.Count; i++)
         {
            item = new ListViewItem();
            item.Checked = Core.PluginManager.Items[i].Enabled;
            item.SubItems.Add(Core.PluginManager.Items[i].Plugin.Name);
            item.SubItems.Add(Core.PluginManager.Items[i].Plugin.Extensions);
            PluginsList.Items.Add(item);
         }
      }

      /// <summary>
      /// Display the selected plugin details.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		09/05/2006	Created
      /// </history>
      private void PluginsList_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (PluginsList.SelectedItems.Count > 0)
            LoadPluginDetails(Core.PluginManager.Items[PluginsList.SelectedItems[0].Index].Plugin);
         else
            ClearPluginDetails();
      }

      /// <summary>
      /// Enable or disable the selected plugin.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		09/05/2006	Created
      /// </history>
      private void PluginsList_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
      {
         if (e.Index > -1 && e.Index < PluginsList.Items.Count)
         {
            PluginsList.Items[e.Index].Selected = true;
            if (e.NewValue == CheckState.Checked)
               Core.PluginManager.Items[e.Index].Enabled = true;
            else
               Core.PluginManager.Items[e.Index].Enabled = false;
         }
      }

      /// <summary>
      /// Clear the plugin details.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		09/05/2006	Created
      /// </history>
      private void ClearPluginDetails()
      {
         lblPluginName.Text = string.Empty;
         lblPluginVersion.Text = string.Empty;
         lblPluginAuthor.Text = string.Empty;
         lblPluginDescription.Text = string.Empty;
      }

      /// <summary>
      /// Display the plugin details.
      /// </summary>
      /// <param name="plugin">IAstroGrepPlugin to load</param>
      /// <history>
      /// [Curtis_Beard]		09/05/2006	Created
      /// </history>
      private void LoadPluginDetails(IAstroGrepPlugin plugin)
      {
         lblPluginName.Text = plugin.Name;
         lblPluginVersion.Text = plugin.Version;
         lblPluginAuthor.Text = plugin.Author;
         lblPluginDescription.Text = plugin.Description;
      }

      private void btnUp_Click(object sender, EventArgs e)
      {
         // move selected plugin up in list
         if (PluginsList.SelectedItems.Count > 0 && PluginsList.SelectedItems[0].Index != 0)
         {
            Core.PluginManager.Items.Reverse(PluginsList.SelectedItems[0].Index - 1, 2);
            LoadPlugins();
         }
      }

      private void btnDown_Click(object sender, EventArgs e)
      {
         // move selected plugin down in list
         if (PluginsList.SelectedItems.Count > 0 && PluginsList.SelectedItems[0].Index != (PluginsList.Items.Count - 1))
         {
            Core.PluginManager.Items.Reverse(PluginsList.SelectedItems[0].Index, 2);
            LoadPlugins();
         }
      }
      #endregion

      #region File Encoding Methods

      private void LoadFileEncodings()
      {
         var fileEncodings = FileEncoding.ConvertStringToFileEncodings(GeneralSettings.FileEncodings);
         if (fileEncodings != null && fileEncodings.Count > 0)
         {
            lstFiles.BeginUpdate();

            foreach (var file in fileEncodings)
            {
               var item = GetFileEncodingListViewItem(file);
               lstFiles.Items.Add(item);
            }

            lstFiles.EndUpdate();
         }

         SetFileEncodingButtonState();
      }

      private void SaveFileEncodings()
      {
         string encodings = string.Empty;

         if (lstFiles.Items.Count > 0)
         {
            var fileEncodings = new List<FileEncoding>();
            foreach (ListViewItem item in lstFiles.Items)
            {
               var encoding = item.Tag as FileEncoding;
               encoding.Enabled = item.Checked;
               fileEncodings.Add(encoding);
            }

            encodings = FileEncoding.ConvertFileEncodingsToString(fileEncodings);
         }

         GeneralSettings.FileEncodings = encodings;
      }

      private void btnFileEncodingAdd_Click(object sender, EventArgs e)
      {
         var dialog = new frmAddEditFileEncoding();
         if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
         {
            lstFiles.Items.Add(GetFileEncodingListViewItem(dialog.SelectedFileEncoding));

            SetFileEncodingButtonState();
         }

         this.DialogResult = System.Windows.Forms.DialogResult.None;
      }

      private void btnFileEncodingEdit_Click(object sender, EventArgs e)
      {
         if (lstFiles.SelectedItems.Count > 0)
         {
            // get currently selected exclusion
            var item = lstFiles.SelectedItems[0].Tag as FileEncoding;
            item.Enabled = lstFiles.SelectedItems[0].Checked;

            var dialog = new frmAddEditFileEncoding();
            dialog.SelectedFileEncoding = item;
            if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
               item = dialog.SelectedFileEncoding;
               var listItem = GetFileEncodingListViewItem(item);
               lstFiles.SelectedItems[0].Checked = item.Enabled;
               lstFiles.SelectedItems[0].Tag = item;

               lstFiles.SelectedItems[0].SubItems[1].Text = listItem.SubItems[1].Text;
               lstFiles.SelectedItems[0].SubItems[2].Text = listItem.SubItems[2].Text;

               SetFileEncodingButtonState();
            }
         }

         this.DialogResult = System.Windows.Forms.DialogResult.None;
      }

      private void btnFileEncodingDelete_Click(object sender, EventArgs e)
      {
         // remove
         if (lstFiles.SelectedItems.Count > 0)
         {
            foreach (ListViewItem item in lstFiles.SelectedItems)
            {
               lstFiles.Items.Remove(item);
            }
            SetFileEncodingButtonState();
         }

         this.DialogResult = DialogResult.None;
      }

      /// <summary>
      /// Sets the FileEncoding's button states depending on if one is selected.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// </history>
      private void SetFileEncodingButtonState()
      {
         if (lstFiles.SelectedItems.Count > 0)
         {
            btnFileEncodingDelete.Enabled = true;
            btnFileEncodingEdit.Enabled = true;
         }
         else
         {
            btnFileEncodingDelete.Enabled = false;
            btnFileEncodingEdit.Enabled = false;
         }
      }

      /// <summary>
      /// Get the list view item from the given FileEncoding object.
      /// </summary>
      /// <param name="item">FileEncoding object</param>
      /// <returns>ListViewItem object</returns>
      /// <history>
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// </history>
      private ListViewItem GetFileEncodingListViewItem(FileEncoding item)
      {
         ListViewItem listItem = new ListViewItem();
         listItem.Tag = item;
         listItem.Checked = item.Enabled;
         listItem.SubItems.Add(item.FilePath);
         listItem.SubItems.Add(item.Encoding.EncodingName);

         return listItem;
      }

      /// <summary>
      /// Update the button states.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// </history>
      private void lstFiles_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         SetFileEncodingButtonState();
      }

      /// <summary>
      /// Edit the selected file entry.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// </history>
      private void lstFiles_DoubleClick(object sender, EventArgs e)
      {
         Point clientPoint = lstFiles.PointToClient(Control.MousePosition);
         ListViewItem item = lstFiles.GetItemAt(clientPoint.X, clientPoint.Y);

         if (item != null)
         {
            item.Selected = true;
            btnFileEncodingEdit_Click(null, null);
         }
      }

      /// <summary>
      /// Handles the key down event (supports ctrl-a, del).
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// </history>
      private void lstFiles_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyCode == Keys.A && e.Control) //ctrl+a  Select All
         {
            foreach (ListViewItem item in lstFiles.Items)
            {
               item.Selected = true;
            }
         }

         if (e.KeyCode == Keys.Delete) //delete
         {
            btnFileEncodingDelete_Click(sender, EventArgs.Empty);
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
      private void lstFiles_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         inhibitFileEncodingAutoCheck = true;
      }

      /// <summary>
      /// Handle not changing checked state of item when double clicking to edit it.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   03/04/2014	FIX: 52, don't change check state when double clicking to edit
      /// </history>
      private void lstFiles_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
      {
         if (inhibitFileEncodingAutoCheck)
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
      private void lstFiles_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         inhibitFileEncodingAutoCheck = false;
      }

      /// <summary>
      /// Handles ListView Column Click event to allow Enabled column to toggle all checkboxes.
      /// </summary>
      /// <param name="sender">lstFiles listview</param>
      /// <param name="e">column click arguments</param>
      /// <history>
      /// [Curtis_Beard]	   08/13/2014	ADD: 79, allow Enabled column to toggle all checkboxes
      /// </history>
      private void lstFiles_ColumnClick(object sender, ColumnClickEventArgs e)
      {
         // enabled column
         if (e.Column == 0)
         {
            bool allChecked = (lstFiles.CheckedItems.Count == lstFiles.Items.Count);
            foreach (ListViewItem item in lstFiles.Items)
            {
               item.Checked = !allChecked;
            }
         }
      }

      /// <summary>
      /// Enable/Disable settings based on if encoding detection is enabled.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   05/26/2015	FIX: 69, add performance setting for file detection
      /// </history>
      private void chkDetectFileEncoding_CheckedChanged(object sender, EventArgs e)
      {
         lblPerformance.Enabled = cboPerformance.Enabled = chkUseEncodingCache.Enabled = btnCacheClear.Enabled = chkDetectFileEncoding.Checked;
      }

      /// <summary>
      /// Clears the encoding cache.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   05/26/2015	FIX: 69, add performance setting for file detection
      /// </history>
      private void btnCacheClear_Click(object sender, EventArgs e)
      {
         libAstroGrep.EncodingDetection.Caching.EncodingCache.Instance.Clear(true);

         MessageBox.Show(this, Language.GetGenericText("FileEncoding.CacheCleared", "Cache cleared successfully."), ProductInformation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
      }

      /// <summary>
      /// Used to display encoding performance enum values.
      /// </summary>
      internal class EncodingPerformance
      {
         /// <summary>Display name of performance value</summary>
         public string Name { get; set; }

         /// <summary>Performance enum value</summary>
         public int Value { get; set; }
      }

      #endregion
   }
}
