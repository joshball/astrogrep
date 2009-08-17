using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

using libAstroGrep;

namespace AstroGrep.Windows.Forms
{
   /// <summary>
   /// Main Form
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
   /// [Theodore_Ward]     ??/??/????  Initial
   /// [Curtis_Beard]      01/11/2005  .Net Conversion/Comments/Option Strict
   /// [Curtis_Beard]      10/15/2005	CHG: Replace search procedures
   /// [Andrew_Radford]    17/08/2008	CHG: Moved Winforms designer stuff to a .designer file
   /// </history>
	public partial class frmMain : Form
	{
      #region Declarations
      private bool __OptionsShow = true;
      private int __SortColumn = -1;
      private Grep __Grep = null;
      private string __SearchOptionsText = "Search Options {0}";
      private string __FilterOptionsText = "Filter Options {0}";
      private int __FileListHeight = Core.GeneralSettings.DEFAULT_FILE_PANEL_HEIGHT;
      private readonly System.Collections.Specialized.StringCollection __ErrorCollection = new System.Collections.Specialized.StringCollection();
      #endregion

      #region Delegate Declarations
      private delegate void UpdateHitCountCallBack(HitObject hit);
      private delegate void SetSearchStateCallBack(bool enable);
      private delegate void UpdateStatusMessageCallBack(string message);
      private delegate void ClearItemsCallBack();
      private delegate void AddToListCallBack(FileInfo file, int index);
      private delegate void DisplaySearchErrorsCallBack();
      #endregion

        
      private System.ComponentModel.IContainer components;

      /// <summary>
      /// Creates an instance of the frmMain class.
      /// </summary>
      /// /// <history>
      /// [Theodore_Ward]     ??/??/????  Created
      /// [Curtis_Beard]      11/02/2006	CHG: Conversion to C#, setup event handlers
      /// </history>
		public frmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         // Attach event handlers
         this.Resize += new EventHandler(frmMain_Resize);
         this.Closed += new EventHandler(frmMain_Closed);
		 pnlMainSearch.Paint += new PaintEventHandler(pnlMainSearch_Paint);
         pnlSearch.SizeChanged += new EventHandler(pnlSearch_SizeChanged);
         PanelOptionsContainer.Paint += new PaintEventHandler(PanelOptionsContainer_Paint);
         splitLeftRight.Paint += new PaintEventHandler(splitLeftRight_Paint);
         splitUpDown.Paint += new PaintEventHandler(splitUpDown_Paint);
         mnuFile.Select += new EventHandler(mnuFile_Select);
         mnuEdit.Select += new EventHandler(mnuEdit_Select);
         cboFilePath.DropDown += new EventHandler(cboFilePath_DropDown);
         cboFileName.DropDown += new EventHandler(cboFileName_DropDown);
         cboSearchForText.DropDown += new EventHandler(cboSearchForText_DropDown);
         chkNegation.CheckedChanged += new EventHandler(chkNegation_CheckedChanged);
         chkFileNamesOnly.CheckedChanged += new EventHandler(chkFileNamesOnly_CheckedChanged);
         txtHits.MouseDown += new MouseEventHandler(txtHits_MouseDown);
         lstFileNames.MouseDown += new MouseEventHandler(lstFileNames_MouseDown);
         lstFileNames.ColumnClick += new ColumnClickEventHandler(lstFileNames_ColumnClick);
         lstFileNames.HandleCreated += new EventHandler(lstFileNames_HandleCreated);
         lstFileNames.ItemDrag += new ItemDragEventHandler(lstFileNames_ItemDrag);
         
         try
         {
            // set font for printing and default display
            txtHits.Font = new Font("Courier New", 9.75F, FontStyle.Regular);
         }
         catch {} // todo: why is this here?
		}

      #region Form Events
      /// <summary>
      /// Form Load Event
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Theodore_Ward]		??/??/????	Initial
      /// [Curtis_Beard]		01/11/2005	.Net Conversion
      /// [Curtis_Beard]		04/12/2005	ADD: Command line additions
      /// [Son_Le]				08/08/2005	CHG: save listview header widths
      /// [Curtis_Beard]		10/15/2005	CHG: validate command line parameter as valid dir
      /// [Curtis_Beard]		07/12/2006	CHG: allow drives for a valid command line parameter
      /// [Curtis_Beard]		07/25/2006	CHG: Moved cmd line processing to ProcessCommandLine routine
      /// [Curtis_Beard]		10/10/2006	CHG: Remove call to load search settings, perform check only.
      /// [Curtis_Beard]		10/11/2007	ADD: convert language value if necessary
      /// </history>
      private void frmMain_Load(object sender, System.EventArgs e)
      {
         // Parse command line, must be before any use of config files
         CommandLineProcessing.CommandLineArguments args = CommandLineProcessing.Process(Environment.GetCommandLineArgs());

         // set defaults
         txtContextLines.Maximum = Constants.MAX_CONTEXT_LINES;
         lnkSearchOptions.Text = __SearchOptionsText;

         // Load language
         //Language.GenerateXml(this, Application.StartupPath + "\\" + this.Name + ".xml");
			Legacy.ConvertLanguageValue();
         Language.Load(AstroGrep.Core.GeneralSettings.Language);
         Language.ProcessForm(this, this.toolTip1);
         
         // set member to hold language specified text
         __SearchOptionsText = lnkSearchOptions.Text;

         // Hide the Search Options
         ShowSearchOptions();

         // Load the general settings
         Legacy.ConvertGeneralSettings();
         LoadSettings();

         // Load the search settings
         Legacy.ConvertSearchSettings();
         LoadSearchSettings();

         // Delete registry entry (if exist)
         Legacy.DeleteRegistry();

         // Load plugins
         Core.PluginManager.Load();

         // set view state of controls
         LoadViewStates();

         // Handle any command line arguments
         ProcessCommandLine(args);
      }

      /// <summary>
      /// Handles form resize event.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      private void frmMain_Resize(object sender, EventArgs e)
      {
         splitLeftRight.Invalidate();
         splitUpDown.Invalidate();
      }

      /// <summary>
      /// Closed Event - Save settings and exit
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// [Curtis_Beard]	   10/11/2006	CHG: Use common function to remove Browse..., call SaveSettings
      /// [Curtis_Beard]	   11/22/2006	CHG: Remove use of browse in combobox
      /// </history>
      private void frmMain_Closed(object sender, EventArgs e)
      {
         SaveSettings();
         Application.Exit();
      }
      #endregion

      #region Control Events
      /// <summary>
      /// Paint the border for the panel.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      11/02/2006  Created
      /// </history>
      private void pnlMainSearch_Paint(object sender, PaintEventArgs e)
      {
         Rectangle rect = pnlMainSearch.ClientRectangle;
         rect.Width -= 1;
         rect.Height -= 1;

         e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveCaption), rect);
      }

      /// <summary>
      /// Paint the border for the panel
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      11/02/2006  Created
      /// </history>
      private void PanelOptionsContainer_Paint(object sender, PaintEventArgs e)
      {
         Rectangle rect = PanelOptionsContainer.ClientRectangle;
         rect.Width -= 1;
         rect.Height -= 1;

         e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveCaption), rect);
      }

      /// <summary>
      /// Resize the comboboxes when the main search panel is resized.
      /// </summary>
      /// <param name="sender">system parm</param>
      /// <param name="e">system parm</param>
      /// <remarks>
      ///   This is a workaround for a bug in the .NET 2003 combobox control
      ///   when it is set to ComboBoxStyle.DropDown that will select the text
      ///   in the control when it is resized.
      ///   By temporarily changing the style to ComboBoxStyle.Simple and manually
      ///   setting the width, we can avoid this annoying feature.
      /// </remarks>
      /// <history>
      /// [Son_Le]            08/08/2005  FIX:1180742, remove highlight of combobox
      /// [Curtis_Beard]      11/03/2006  CHG: don't resize just change style
      /// </history>
      private void pnlSearch_SizeChanged(object sender, EventArgs e)
      {
         //int _width = btnSearch.Width + btnCancel.Width + 
         //             (btnCancel.Left - (btnSearch.Left + btnSearch.Width));

         cboFilePath.DropDownStyle = ComboBoxStyle.DropDownList;
         //cboFilePath.Width = _width;
         cboFilePath.DropDownStyle = ComboBoxStyle.DropDown;

         cboFileName.DropDownStyle = ComboBoxStyle.Simple;
         //cboFileName.Width = _width;
         cboFileName.DropDownStyle = ComboBoxStyle.DropDown;

         cboSearchForText.DropDownStyle = ComboBoxStyle.Simple;
         //cboSearchForText.Width = _width;
         cboSearchForText.DropDownStyle = ComboBoxStyle.DropDown;

         pnlMainSearch.Invalidate();
         PanelOptionsContainer.Invalidate();
      }

      /// <summary>
      /// Handles drawing a splitter gripper on the control.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      private void splitLeftRight_Paint(object sender, PaintEventArgs e)
      {
         const int RECT_SIZE = 2;
         const int MAX = 9;

         Graphics g = e.Graphics;
         int x1 = Convert.ToInt32(splitLeftRight.Width / 2) - 1;
         int x2 = x1 + 1;
         int y1 = Convert.ToInt32(((splitLeftRight.Height - (MAX * 2 * RECT_SIZE)) / 2) + 1);
         int y2 = y1 + 1;
         int index = 0;

         do
         {
            g.FillRectangle(SystemBrushes.ControlLightLight, new Rectangle(x2, y2, RECT_SIZE, RECT_SIZE));
            g.FillRectangle(SystemBrushes.ControlDark, new Rectangle(x1, y1, RECT_SIZE, RECT_SIZE));

            y1 = y1 + (2 * RECT_SIZE);
            y2 = y1 + 1;

            index += 1;
         }while (index < MAX);
      }

      /// <summary>
      /// Handles drawing a splitter gripper on the control.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      private void splitUpDown_Paint(object sender, PaintEventArgs e)
      {
         const int RECT_SIZE = 2;
         const int MAX = 9;

         Graphics g = e.Graphics;
         int x1 = Convert.ToInt32(((splitUpDown.Width - (MAX * 2 * RECT_SIZE)) / 2) + 1);
         int x2 = x1 + 1;
         int y1 = Convert.ToInt32(splitUpDown.Height / 2) - 1;
         int y2 = y1 + 1;
         int index = 0;

         do
         {
            g.FillRectangle(SystemBrushes.ControlLightLight, new Rectangle(x2, y2, RECT_SIZE, RECT_SIZE));
            g.FillRectangle(SystemBrushes.ControlDark, new Rectangle(x1, y1, RECT_SIZE, RECT_SIZE));

            x1 = x1 + (2 * RECT_SIZE);
            x2 = x1 + 1;

            index += 1;
         }while (index < MAX);
      }

      /// <summary>
      /// Hide/Show the Search Options
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Curtis_Beard]	   02/04/2005	Created
      /// </history>
      private void lnkSearchOptions_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
      {
         ShowSearchOptions();
      }

      /// <summary>
      /// Resize drop down list if necessary
      /// </summary>
      /// <param name="sender">system parm</param>
      /// <param name="e">system parm</param>
      /// <history>
      /// [Curtis_Beard]    11/21/2005	Created
      /// </history>
      private void cboSearchForText_DropDown(object sender, EventArgs e)
      {
         cboSearchForText.DropDownWidth = CalculateDropDownWidth(cboSearchForText);
      }

      /// <summary>
      /// Resize drop down list if necessary
      /// </summary>
      /// <param name="sender">system parm</param>
      /// <param name="e">system parm</param>
      /// <history>
      /// [Curtis_Beard]    11/21/2005	Created
      /// </history>
      private void cboFilePath_DropDown(object sender, EventArgs e)
      {
         cboFilePath.DropDownWidth = CalculateDropDownWidth(cboFilePath);
      }

      /// <summary>
      /// Resize drop down list if necessary
      /// </summary>
      /// <param name="sender">system parm</param>
      /// <param name="e">system parm</param>
      /// <history>
      /// [Curtis_Beard]    11/21/2005	Created
      /// </history>
      private void cboFileName_DropDown(object sender, EventArgs e)
      {
         cboFileName.DropDownWidth = CalculateDropDownWidth(cboFileName);
      }

      /// <summary>
      /// Negation check event
      /// </summary>
      /// <param name="sender">system parm</param>
      /// <param name="e">system parm</param>
      /// <history>
      /// [Curtis_Beard]	   01/28/2005	Created
      /// [Curtis_Beard]	   06/13/2005	CHG: Gray out file names only when checked
      /// </history>
      private void chkNegation_CheckedChanged(object sender, EventArgs e)
      {
         chkFileNamesOnly.Checked = chkNegation.Checked;
         chkFileNamesOnly.Enabled = !chkNegation.Checked;         
      }

      /// <summary>
      /// File Names Only Check Box Event
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Theodore_Ward]   ??/??/????  Initial
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// [Curtis_Beard]	   06/13/2005	CHG: Gray out context lines label
      /// </history>
      private void chkFileNamesOnly_CheckedChanged(object sender, EventArgs e)
      {
         if (chkFileNamesOnly.Checked)
         {
            chkLineNumbers.Enabled = false;
            txtContextLines.Enabled = false;
            lblContextLines.Enabled = false;
         }
         else
         {
            chkLineNumbers.Enabled = true;
            txtContextLines.Enabled = true;
            lblContextLines.Enabled = true;
         }
      }

      /// <summary>
      /// Cancel Button Event
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Theodore_Ward]     ??/??/????  Initial
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// </history>
      private void btnCancel_Click(object sender, System.EventArgs e)
      {
         if (__Grep != null)
            __Grep.Abort();
      }

      /// <summary>
      /// Search Button Event
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Theodore_Ward]     ??/??/????  Initial
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// </history>
      private void btnSearch_Click(object sender, System.EventArgs e)
      {
         if (!VerifyInterface())
            return;

         StartSearch();
      }

      /// <summary>
      /// txtHits Mouse Down Event - Used to detect a double click
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Theodore_Ward]     ??/??/????  Initial
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion/Replaced with mouse down
      /// [Curtis_Beard]	   12/07/2005	CHG: Use column constant
      /// [Curtis_Beard]	   07/03/2006	FIX: 1516777, stop right click to open text editor
      /// [Curtis_Beard]	   07/26/2006	ADD: 1512026, column position
      /// </history>
      private void txtHits_MouseDown(object sender, MouseEventArgs e)
      {
         if (e.Button == MouseButtons.Left && e.Clicks == 2)
         {
            int lineNumber;
            int hitLineNumber;
            int hitColumn;

            // Make sure there is something to click on.
            if (lstFileNames.SelectedItems.Count == 0)
               return;

            // retrieve the hit object
            HitObject hit = __Grep.RetrieveHitObject(int.Parse(lstFileNames.SelectedItems[0].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text));

            // Find out the line number the cursor is on.
            lineNumber = txtHits.GetLineFromCharIndex(txtHits.SelectionStart);

            // Use the cursor's linenumber to get the hit's line number.
            hitLineNumber = hit.RetrieveLineNumber(lineNumber);

            hitColumn = hit.RetrieveColumn(lineNumber);

            // Retrieve the filename
            string path = hit.FilePath;

            // Open the default editor.
            Common.EditFile(path, hitLineNumber, hitColumn);
         }
      }

      /// <summary>
      /// File Name List Double Click Event
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Theodore_Ward]     ??/??/????  Initial
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// [Curtis_Beard]	   12/07/2005	CHG: Use column constant
      /// [Curtis_Beard]	   07/03/2006	FIX: 1516777, stop right click to open text editor,
      ///                                 changed from DoubleClick event to MouseDown
      /// [Curtis_Beard]	   07/26/2006	ADD: 1512026, column position
      /// </history>
      private void lstFileNames_MouseDown(object sender, MouseEventArgs e)
      {
         if (e.Button == MouseButtons.Left && e.Clicks == 2)
         {
            // Make sure there is something to click on
            if (lstFileNames.SelectedItems.Count == 0)
               return;

            // retrieve the hit object
            HitObject hit = __Grep.RetrieveHitObject(int.Parse(lstFileNames.SelectedItems[0].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text));

            // Retrieve the filename
            string path = hit.FilePath;

            // Open the default editor.
            Common.EditFile(path, 1, 1);
         }
      }

      /// <summary>
      /// File Name List Select Index Change Event
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Theodore_Ward]     ??/??/????  Initial
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// [Curtis_Beard]	   12/07/2005	CHG: Use column constant
      /// </history>
      private void lstFileNames_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (lstFileNames.SelectedItems.Count > 0)
         {
            // retrieve hit object
            HitObject hit = __Grep.RetrieveHitObject(int.Parse(lstFileNames.SelectedItems[0].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text));
            
            HighlightText(hit);
            
            hit = null;
         }
      }

      /// <summary>
      /// Allow sorting of list view columns
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Curtis_Beard]	   02/06/2005	Created
      /// [Curtis_Beard]	   07/07/2006	CHG: add support for count column sorting
      /// [Curtis_Beard]	   10/06/2006	FIX: clear sort indicator propertly
      /// </history>
      private void lstFileNames_ColumnClick(object sender, ColumnClickEventArgs e)
      {
         // Determine whether the column is the same as the last column clicked.
         if (e.Column != __SortColumn)
         {
            // Remove sort indicator
            if (__SortColumn != -1)
               Windows.API.SetHeaderImage(lstFileNames, __SortColumn, SortOrder.Ascending, false);

            // Set the sort column to the new column.
            __SortColumn = e.Column;

            // Set the sort order to ascending by default.
            lstFileNames.Sorting = SortOrder.Ascending;            
         }
         else
         {
            // Determine what the last sort order was and change it.
            if (lstFileNames.Sorting == SortOrder.Ascending)
               lstFileNames.Sorting = SortOrder.Descending;
            else
               lstFileNames.Sorting = SortOrder.Ascending;
         }

         // set column sort image
         Windows.API.SetHeaderImage(lstFileNames, e.Column, lstFileNames.Sorting, true);

         // Set the ListViewItemSorter property to a new ListViewItemComparer object.
         ListViewItemComparer comparer;

         // set comparer for integer types if the count column, otherwise try date/string
         if (e.Column == Constants.COLUMN_INDEX_COUNT)
            comparer = new ListViewItemComparer(e.Column, lstFileNames.Sorting, true);
         else
            comparer = new ListViewItemComparer(e.Column, lstFileNames.Sorting);

         lstFileNames.ListViewItemSorter = comparer;

         // Call the sort method to manually sort.
         lstFileNames.Sort();
      }

      /// <summary>
      /// Handles setting the ImageList for the ListView control.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		09/08/2006	Created
      /// </history>
      private void lstFileNames_HandleCreated(object sender, EventArgs e)
      {
         Windows.API.SetHeaderImageList(lstFileNames.Handle, ListViewImageList.Handle);
      }

      /// <summary>
      /// Handles setting up the drag event for a selected file.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/25/2006	ADD: 1512028, Drag support
      /// </history>
      private void lstFileNames_ItemDrag(object sender, ItemDragEventArgs e)
      {
         ListViewItem item = (ListViewItem)e.Item;
         string path = item.SubItems[Constants.COLUMN_INDEX_DIRECTORY].Text + 
				System.IO.Path.DirectorySeparatorChar.ToString() + 
				item.SubItems[Constants.COLUMN_INDEX_FILE].Text;

         if (System.IO.File.Exists(path))
         {
            string[] paths = new string[1];
            paths[0] = path;
            DataObject data = new DataObject(DataFormats.FileDrop, paths);

            lstFileNames.DoDragDrop(data, DragDropEffects.Copy);
         }
      }

      /// <summary>
      /// Allows selection of the search path.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   11/22/2006	Created
      /// </history>
      private void picBrowse_Click(object sender, System.EventArgs e)
      {
         BrowseForFolder();
      }
      #endregion

      #region Private Methods
      /// <summary>
      /// Load the general settings values.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   10/11/2006	Created
      /// [Curtis_Beard]	   11/22/2006	CHG: Remove use of browse in combobox
      /// </history>
      private void LoadSettings()
      {
         //  Only load up to the desired number of paths.
         if (AstroGrep.Core.GeneralSettings.MaximumMRUPaths < 0 || AstroGrep.Core.GeneralSettings.MaximumMRUPaths > Constants.MAX_STORED_PATHS)
            AstroGrep.Core.GeneralSettings.MaximumMRUPaths = Constants.MAX_STORED_PATHS;

         LoadComboBoxEntry(cboFilePath, AstroGrep.Core.GeneralSettings.SearchStarts);
         LoadComboBoxEntry(cboFileName, AstroGrep.Core.GeneralSettings.SearchFilters);
         LoadComboBoxEntry(cboSearchForText, AstroGrep.Core.GeneralSettings.SearchTexts);

         // Path
         if (cboFilePath.Items.Count > 0 && cboFilePath.Items.Count != 1)
            cboFilePath.SelectedIndex = 0;
         
         // Filter
         if (cboFileName.Items.Count == 0)
         {
            // no entries so create defaults
            cboFileName.Items.AddRange(new object[] {"*.*", "*.txt", "*.java", "*.htm, *.html", "*.jsp, *.asp", "*.js, *.inc", "*.htm, *.html, *.jsp, *.asp", "*.sql", "*.bas, *.cls, *.vb", "*.cs", "*.cpp, *.c, *.h", "*.asm"});
         }
         cboFileName.SelectedIndex = 0;

         // Search
         if (cboSearchForText.Items.Count > 0)
            cboSearchForText.SelectedIndex = 0;

         // Result Window Colors
         txtHits.ForeColor = Common.ConvertStringToColor(Core.GeneralSettings.ResultsForeColor);
         txtHits.BackColor = Common.ConvertStringToColor(Core.GeneralSettings.ResultsBackColor);

         // File list columns
         SetColumnsText();

         LoadWindowSettings();

         // Load the text editors
         Common.LoadTextEditors();
      }

      /// <summary>
      /// Load the window settings.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   06/28/2007	Created
      /// </history>
      private void LoadWindowSettings()
      {
         int _state = Core.GeneralSettings.WindowState;

         // set the top/left
         if (Core.GeneralSettings.WindowTop != -1)
            Top = Core.GeneralSettings.WindowTop;
         if (Core.GeneralSettings.WindowLeft != -1)
            Left = Core.GeneralSettings.WindowLeft;

         // set the width/height
         if (Core.GeneralSettings.WindowWidth != -1)
            Width = Core.GeneralSettings.WindowWidth;
         if (Core.GeneralSettings.WindowHeight != -1)
            Height = Core.GeneralSettings.WindowHeight;

         if (_state != -1 && _state == (int)FormWindowState.Maximized)
         {
            WindowState = FormWindowState.Maximized;
         }

         // set the splitter positions
         if (Core.GeneralSettings.WindowSearchPanelWidth != -1)
            pnlSearch.Width = Core.GeneralSettings.WindowSearchPanelWidth;
         if (Core.GeneralSettings.WindowFilePanelHeight != -1)
            this.lstFileNames.Height = Core.GeneralSettings.WindowFilePanelHeight;
      }

      /// <summary>
      /// Save the general settings values.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   10/11/2006	Created
      /// </history>
      private void SaveSettings()
      {
         SaveWindowSettings();

         //save column widths
         Core.GeneralSettings.WindowFileColumnNameWidth = lstFileNames.Columns[Constants.COLUMN_INDEX_FILE].Width;
         Core.GeneralSettings.WindowFileColumnLocationWidth = lstFileNames.Columns[Constants.COLUMN_INDEX_DIRECTORY].Width;
         Core.GeneralSettings.WindowFileColumnDateWidth = lstFileNames.Columns[Constants.COLUMN_INDEX_DATE].Width;
         Core.GeneralSettings.WindowFileColumnCountWidth = lstFileNames.Columns[Constants.COLUMN_INDEX_COUNT].Width;

         //save divider panel positions
         Core.GeneralSettings.WindowSearchPanelWidth = pnlSearch.Width;
         Core.GeneralSettings.WindowFilePanelHeight = lstFileNames.Height;

         //save search comboboxes
         Core.GeneralSettings.SearchStarts = Common.GetComboBoxEntriesAsString(cboFilePath);
         Core.GeneralSettings.SearchFilters = Common.GetComboBoxEntriesAsString(cboFileName);
         Core.GeneralSettings.SearchTexts = Common.GetComboBoxEntriesAsString(cboSearchForText);

         Core.GeneralSettings.Save();
      }

      /// <summary>
      /// Save the window settings in the config.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   06/28/2007	Created
      /// </history>
      private void SaveWindowSettings()
      {
         if (WindowState == FormWindowState.Normal)
         {
            Core.GeneralSettings.WindowLeft = this.Left;
            Core.GeneralSettings.WindowTop = this.Top;
            Core.GeneralSettings.WindowWidth = this.Width;
            Core.GeneralSettings.WindowHeight = this.Height;
            Core.GeneralSettings.WindowState = (int)this.WindowState;
         }
         else
         {
            // just save the state, so that previous normal dimensions are valid
            Core.GeneralSettings.WindowState = (int)this.WindowState;
         }
      }

      /// <summary>
      /// Loads the given System.Windows.Forms.ComboBox with the values.
      /// </summary>
      /// <param name="combo">System.Windows.Forms.ComboBoxy</param>
      /// <param name="values">string of the values to load</param>
      /// <history>
      /// [Curtis_Beard]	   10/11/2006	Created
      /// [Curtis_Beard]	   11/22/2006	CHG: Remove use of browse in combobox
      /// </history>
      private void LoadComboBoxEntry(System.Windows.Forms.ComboBox combo, string values)
      {
         if (!values.Equals(string.Empty))
         {
            string[] items = Common.GetComboBoxEntriesFromString(values);
         
            if (items.Length > 0)
            {
               int start = items.Length;
               if (start > Core.GeneralSettings.MaximumMRUPaths)
                  start = Core.GeneralSettings.MaximumMRUPaths;

               combo.BeginUpdate();
               for (int i = start - 1; i > -1; i--)
               {
                  AddComboSelection(combo, items[i]);
               }
               combo.EndUpdate();
            }
         }
      }

      /// <summary>
      /// Set the Common Search Settings on the form
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   01/28/2005	Created
      /// [Curtis_Beard]	   10/10/2006	CHG: Use search settings implementation.
      /// </history>
      private void LoadSearchSettings()
      {
         chkRegularExpressions.Checked = Core.SearchSettings.UseRegularExpressions;
         chkCaseSensitive.Checked = Core.SearchSettings.UseCaseSensitivity;
         chkWholeWordOnly.Checked = Core.SearchSettings.UseWholeWordMatching;
         chkLineNumbers.Checked = Core.SearchSettings.IncludeLineNumbers;
         chkRecurse.Checked = Core.SearchSettings.UseRecursion;
         chkFileNamesOnly.Checked = Core.SearchSettings.ReturnOnlyFileNames;
         txtContextLines.Text = Core.SearchSettings.ContextLines.ToString();
         chkNegation.Checked = Core.SearchSettings.UseNegation;
      }

      /// <summary>
      /// Save the search options.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   10/11/2006	Created
      /// </history>
      private void SaveSearchSettings()
      {
         Core.SearchSettings.UseRegularExpressions = chkRegularExpressions.Checked;
         Core.SearchSettings.UseCaseSensitivity = chkCaseSensitive.Checked;
         Core.SearchSettings.UseWholeWordMatching = chkWholeWordOnly.Checked;
         Core.SearchSettings.IncludeLineNumbers = chkLineNumbers.Checked;
         Core.SearchSettings.UseRecursion = chkRecurse.Checked;
         Core.SearchSettings.ReturnOnlyFileNames = chkFileNamesOnly.Checked;
         Core.SearchSettings.ContextLines = int.Parse(txtContextLines.Text);
         Core.SearchSettings.UseNegation = chkNegation.Checked;

         Core.SearchSettings.Save();
      }

      /// <summary>
      /// Show/Hide the Search Options Panel
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   02/05/2005	Created
      /// </history>
      private void ShowSearchOptions()
      {
         if (__OptionsShow)
         {
            // hide and set text
            lnkSearchOptions.Text = String.Format(__SearchOptionsText, ">>");
            lnkSearchOptions.LinkBehavior = LinkBehavior.AlwaysUnderline;
            lnkSearchOptions.BackColor = SystemColors.Window;
            lnkSearchOptions.LinkColor = SystemColors.HotTrack;
            lnkSearchOptions.ActiveLinkColor = SystemColors.HotTrack;

            pnlSearchOptions.BackColor = SystemColors.Window;
            pnlSearchOptions.BorderStyle = BorderStyle.None;
            PanelOptionsContainer.Visible = false;
            pnlSearch.AutoScroll = false;

            __OptionsShow = false;
         }
         else
         {
            // set text
            lnkSearchOptions.Text = String.Format(__SearchOptionsText, "<<");
            lnkSearchOptions.LinkBehavior = LinkBehavior.NeverUnderline;
            lnkSearchOptions.BackColor = SystemColors.ActiveCaption;
            lnkSearchOptions.LinkColor = SystemColors.ActiveCaptionText;
            lnkSearchOptions.ActiveLinkColor = SystemColors.ActiveCaptionText;

            pnlSearchOptions.BackColor = SystemColors.Window;
            PanelOptionsContainer.Visible = true;
            pnlSearch.AutoScroll = true;
            pnlSearchOptions.BringToFront();

            __OptionsShow = true;
         }
      }

     


      /// <summary>
      /// Sets the file list's columns' text to the correct language.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/25/2006	Created
      /// </history>
      private void SetColumnsText()
      {
         if (lstFileNames.Columns.Count == 0)
         {
            lstFileNames.Columns.Add(Language.GetGenericText("ResultsColumnFile"), AstroGrep.Core.GeneralSettings.WindowFileColumnNameWidth, HorizontalAlignment.Left);
            lstFileNames.Columns.Add(Language.GetGenericText("ResultsColumnLocation"), AstroGrep.Core.GeneralSettings.WindowFileColumnLocationWidth, HorizontalAlignment.Left);
            lstFileNames.Columns.Add(Language.GetGenericText("ResultsColumnDate"), AstroGrep.Core.GeneralSettings.WindowFileColumnDateWidth, HorizontalAlignment.Left);
            lstFileNames.Columns.Add(Language.GetGenericText("ResultsColumnCount"), AstroGrep.Core.GeneralSettings.WindowFileColumnCountWidth, HorizontalAlignment.Left);
         }
         else
         {
            lstFileNames.Columns[Constants.COLUMN_INDEX_FILE].Text = Language.GetGenericText("ResultsColumnFile");
            lstFileNames.Columns[Constants.COLUMN_INDEX_DIRECTORY].Text = Language.GetGenericText("ResultsColumnLocation");
            lstFileNames.Columns[Constants.COLUMN_INDEX_DATE].Text = Language.GetGenericText("ResultsColumnDate");
            lstFileNames.Columns[Constants.COLUMN_INDEX_COUNT].Text = Language.GetGenericText("ResultsColumnCount");
         }
      }

      /// <summary>
      /// Verify user selected options
      /// </summary>
      /// <returns>True - Verified, False - Otherwise</returns>
      /// <history>
      /// [Theodore_Ward]   ??/??/????  Initial
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// [Curtis_Beard]	   10/14/2005	CHG: Use max context lines constant in message
      /// [Ed_Jakubowski]	   05/20/2009    Allow filename only searching
	  /// </history>
      private bool VerifyInterface()
      {
         try
         {
            try
            {
               int _lines = int.Parse(txtContextLines.Text);
               if (_lines < 0 || _lines > Constants.MAX_CONTEXT_LINES)
               {
                  MessageBox.Show(String.Format(Language.GetGenericText("VerifyErrorContextLines"), 0, Constants.MAX_CONTEXT_LINES.ToString()),
                     Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                  return false;
               }
            }
            catch
            {
               MessageBox.Show(String.Format(Language.GetGenericText("VerifyErrorContextLines"), 0, Constants.MAX_CONTEXT_LINES.ToString()),
                  Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               return false;
            }

            if (cboFileName.Text.Trim().Equals(string.Empty))
            {
               MessageBox.Show(Language.GetGenericText("VerifyErrorFileType"), 
                  Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               return false;
            }

            if (cboFilePath.Text.Trim().Equals(string.Empty))
            {
               MessageBox.Show(Language.GetGenericText("VerifyErrorNoStartPath"),
                  Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               return false;
            }

            if (!System.IO.Directory.Exists(cboFilePath.Text.Trim()))
            {
               MessageBox.Show(String.Format(Language.GetGenericText("VerifyErrorInvalidStartPath"), cboFilePath.Text),
                  Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               return false;
            }

            if (cboSearchForText.Text.Trim().Equals(string.Empty))
            {
               //MessageBox.Show(Language.GetGenericText("VerifyErrorNoSearchText"),
               //   Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               //return false;
            }
         }
         catch
         {
            MessageBox.Show(Language.GetGenericText("VerifyErrorGeneric"),
               Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
         }

         return true;
      }

      /// <summary>
      /// Add an item to a combo box
      /// </summary>
      /// <param name="combo">Combo Box</param>
      /// <param name="item">Item to add</param>
      /// <history>
      /// [Theodore_Ward]     ??/??/????  Initial
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// [Curtis_Beard]	   05/09/2007	CHG: check for a valid item
      /// [Ed_Jakubowski]	   05/26/2009	CHG: Added if Contains for testing combo item... this helps astrogrep run in mono 2.4
      /// </history>
      private static void AddComboSelection(ComboBox combo, string item)
      {
         if (item.Length > 0)
         {
            // If this path is already in the dropdown, remove it.
			if (combo.Items.Contains(item))
				combo.Items.Remove(item);
            // Add this path as the first item in the dropdown.
            combo.Items.Insert(0, item);

            // The combo text gets cleared by the AddItem.
            combo.Text = item;

            // Only store as many paths as has been set in options.
            //if (combo.Items.Count > Common.NUM_STORED_PATHS)
            if (combo.Items.Count > Core.GeneralSettings.MaximumMRUPaths)
            {
               // Remove the last item in the list.
               combo.Items.RemoveAt(combo.Items.Count - 1);
            }
         }
      }      

      /// <summary>
      /// Highlight the searched text in the results
      /// </summary>
      /// <param name="hit">Hit Object containing results</param>
      /// <history>
      /// [Curtis_Beard]	   01/27/2005	Created
      /// [Curtis_Beard]	   04/12/2005	FIX: 1180741, Don't capitalize hit line
      /// [Curtis_Beard]	   11/18/2005	ADD: custom highlight colors
      /// [Curtis_Beard] 	   12/06/2005	CHG: call WholeWordOnly from Grep class
      /// [Curtis_Beard] 	   04/21/2006	CHG: highlight regular expression searches
      /// [Curtis_Beard] 	   09/28/2006	FIX: use grep object for settings instead of gui items
      /// [Ed_Jakubowski]      05/20/2009   CHG: Skip highlight if hitCount = 0
      /// </history>
      private void HighlightText(HitObject hit)
      {
	     if (hit.HitCount == 0)
			 return;
          string _searchText = __Grep.SearchSpec.SearchText;
          string _tempLine;

          string _end;

          // Clear the contents
         txtHits.Text = string.Empty;
         txtHits.ForeColor = Common.ConvertStringToColor(Core.GeneralSettings.ResultsForeColor);
         txtHits.BackColor = Common.ConvertStringToColor(Core.GeneralSettings.ResultsBackColor);

         if (__Grep.SearchSpec.UseRegularExpressions)
            HighlightTextRegEx(hit);
         else
         {
             // Loop through hits and highlight search for text
             int _index = 0;
             for (_index = 0; _index < hit.LineCount; _index++)
            {
               // Retrieve hit text
               string _textToSearch = hit.RetrieveLine(_index);

               // Set default font
               txtHits.SelectionFont = new Font("Courier New", 9.75F, FontStyle.Regular);

               _tempLine = _textToSearch;

               // attempt to locate the text in the line
                int _pos = 0;
                if (__Grep.SearchSpec.UseCaseSensitivity)
                  _pos = _tempLine.IndexOf(_searchText);
               else
                  _pos = _tempLine.ToLower().IndexOf(_searchText.ToLower());

               if (_pos > -1)
               {
                  do
                  {
                      //
                     // retrieve parts of text
                     string _begin = _tempLine.Substring(0, _pos);
                     string _text = _tempLine.Substring(_pos, _searchText.Length);
                     _end = _tempLine.Substring(_pos + _searchText.Length);

                     // set default color for starting text
                     txtHits.SelectionColor = Common.ConvertStringToColor(Core.GeneralSettings.ResultsForeColor);
                     // txtHits.SelectionBackColor = Common.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsBackColor);
                     txtHits.SelectedText = _begin;

                     // do a check to see if begin and end are valid for wholeword searches
                      bool _highlight;
                      if (__Grep.SearchSpec.UseWholeWordMatching)
                        _highlight = Grep.WholeWordOnly(_begin, _end);
                     else
                        _highlight = true;

                     // set highlight color for searched text
                     if (_highlight)
                     {
                        txtHits.SelectionColor = Common.ConvertStringToColor(Core.GeneralSettings.HighlightForeColor);
                        // txtHits.SelectionBackColor = Common.ConvertStringToColor(AstroGrep.Core.GeneralSettings.HighlightBackColor);
                     }
                     txtHits.SelectedText = _text;

                     // Check remaining string for other hits in same line
                     if (__Grep.SearchSpec.UseCaseSensitivity)
                        _pos = _end.IndexOf(_searchText);
                     else
                        _pos = _end.ToLower().IndexOf(_searchText.ToLower());

                     // set default color for end, if no more hits in line
                     _tempLine = _end;
                     if (_pos < 0)
                     {
                        txtHits.SelectionColor = Common.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsForeColor);
                        // txtHits.SelectionBackColor = Common.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsBackColor);
                        txtHits.SelectedText = _end;
                     }

                  }while (_pos > -1);
               }
               else
               {
                  // set default color, no search text found
                  txtHits.SelectionColor = Common.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsForeColor);
                  // txtHits.SelectionBackColor = Common.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsBackColor);
                  txtHits.SelectedText = _textToSearch;
               }
            }
         }
      }

      /// <summary>
      /// Highlight the searched text in the results when using regular expressions
      /// </summary>
      /// <param name="hit">Hit Object containing results</param>
      /// <history>
      /// [Curtis_Beard]	   04/21/2006	Created
      /// [Curtis_Beard]	   05/11/2006	FIX: Include context lines if present and prevent system beep
      /// [Curtis_Beard]	   07/07/2006	FIX: 1512029, highlight whole word and case sensitive matches
      /// [Curtis_Beard] 	   09/28/2006	FIX: use grep object for settings instead of gui items, remove searchText parameter
      /// [Curtis_Beard]	   05/18/2006	FIX: 1723815, use correct whole word matching regex
      /// </history>
      private void HighlightTextRegEx(HitObject hit)
      {
         string _textToSearch = string.Empty;
         string _tempString = string.Empty;
         int _index = 0;
         int _lastPos = 0;
         int _counter = 0;
         Regex _regEx = new Regex(__Grep.SearchSpec.SearchText);
         MatchCollection _col;
         Match _item;

         // Loop through hits and highlight search for text
         for (_index = 0; _index < hit.LineCount; _index++)
         {
            // Retrieve hit text
            _textToSearch = hit.RetrieveLine(_index);

            // Set default font
            txtHits.SelectionFont = new Font("Courier New", 9.75F, FontStyle.Regular);

            // find all reg ex matches in line
            if (__Grep.SearchSpec.UseCaseSensitivity && __Grep.SearchSpec.UseWholeWordMatching)
            {
                _regEx = new Regex("\\b" + __Grep.SearchSpec.SearchText + "\\b");
               _col = _regEx.Matches(_textToSearch);
            }
            else if (__Grep.SearchSpec.UseCaseSensitivity)
            {
                _regEx = new Regex(__Grep.SearchSpec.SearchText);
               _col = _regEx.Matches(_textToSearch);
            }
            else if (__Grep.SearchSpec.UseWholeWordMatching)
            {
                _regEx = new Regex("\\b" + __Grep.SearchSpec.SearchText + "\\b", RegexOptions.IgnoreCase);
               _col = _regEx.Matches(_textToSearch);
            }
            else
            {
                _regEx = new Regex(__Grep.SearchSpec.SearchText, RegexOptions.IgnoreCase);
               _col = _regEx.Matches(_textToSearch);
            }

            // loop through the matches
            _lastPos = 0;
            for (_counter = 0; _counter < _col.Count; _counter++)
            {
               _item = _col[_counter];

               // set the start text
               txtHits.SelectionColor = Common.ConvertStringToColor(Core.GeneralSettings.ResultsForeColor);
               // txtHits.SelectionBackColor = Common.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsBackColor);

               // check for empty string to prevent assigning nothing to selection text preventing
               //  a system beep
               _tempString = _textToSearch.Substring(_lastPos, _item.Index - _lastPos);
               if (!_tempString.Equals(string.Empty))
                  txtHits.SelectedText = _tempString;

               // set the hit text
               txtHits.SelectionColor = Common.ConvertStringToColor(Core.GeneralSettings.HighlightForeColor);
               // txtHits.SelectionBackColor = Common.ConvertStringToColor(AstroGrep.Core.GeneralSettings.HighlightBackColor);
               txtHits.SelectedText = _textToSearch.Substring(_item.Index, _item.Length);

               // set the end text
               txtHits.SelectionColor = Common.ConvertStringToColor(Core.GeneralSettings.ResultsForeColor);
               // txtHits.SelectionBackColor = Common.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsBackColor);
               if (_counter + 1 >= _col.Count)
               {
                  //  no more hits so just set the rest
                  txtHits.SelectedText = _textToSearch.Substring(_item.Index + _item.Length);
                  _lastPos = _item.Index + _item.Length;
               }
               else
               {
                  // another hit so just set inbetween
                  txtHits.SelectedText = _textToSearch.Substring(_item.Index + _item.Length, _col[_counter + 1].Index - (_item.Index + _item.Length));
                  _lastPos = _col[_counter + 1].Index;
               }
            }

            if (_col.Count == 0)
            {
               //  no match, just a context line
               txtHits.SelectionColor = Common.ConvertStringToColor(Core.GeneralSettings.ResultsForeColor);
               // txtHits.SelectionBackColor = Common.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsBackColor);
               txtHits.SelectedText = _textToSearch;
            }
         }
      }

      /// <summary>
      /// Enable/Disable menu items (Thread safe)
      /// </summary>
      /// <param name="enable">True - enable menu items, False - disable</param>
      /// <history>
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// [Curtis_Beard]	   07/10/2006	CHG: Disable combo boxes during search
      /// [Curtis_Beard]	   07/12/2006	CHG: make thread safe
      /// [Curtis_Beard]	   07/25/2006	ADD: enable/disable context lines label
      /// </history>
      private void SetSearchState(bool enable)
      {
         if (this.InvokeRequired)
         {
            SetSearchStateCallBack _delegate = new SetSearchStateCallBack(SetSearchState);
            this.Invoke(_delegate, new Object[1] {enable});
            return;
         }

         mnuFile.Enabled = enable;
         mnuEdit.Enabled = enable;
         mnuTools.Enabled = enable;
         mnuHelp.Enabled = enable;

         btnSearch.Enabled = enable;
         btnCancel.Enabled = !enable;
         picBrowse.Enabled = enable;
         pnlSearchOptions.Enabled = enable;
         lblContextLines.Enabled = enable;

         cboFileName.Enabled = enable;
         cboFilePath.Enabled = enable;
         cboSearchForText.Enabled = enable;

         if (enable)
            btnSearch.Focus();
         else
            btnCancel.Focus();

      }

      /// <summary>
      /// Open Browser for Folder dialog
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		10/13/2005	ADD: Initial
      /// [Curtis_Beard]		10/02/2006	FIX: Clear ComboBox when only Browse... is present
      /// [Curtis_Beard]		11/22/2006	CHG: Remove use of browse in combobox
      /// [Justin_Dearing]		05/06/2007	CHG: Dialog defaults to the folder selected in the combobox.
      /// [Curtis_Beard]		05/23/2007	CHG: Remove cancel highlight
      /// </history>
      private void BrowseForFolder()
      {
         FolderBrowserDialog dlg = new FolderBrowserDialog();
         dlg.Description = Language.GetGenericText("OpenFolderDescription");
         dlg.ShowNewFolderButton = false;

         // set initial directory if valid
         if (System.IO.Directory.Exists(cboFilePath.Text)) 
         {
            dlg.SelectedPath = cboFilePath.Text;
         }
         
         // display dialog and setup path if selected
         if (dlg.ShowDialog(this) == DialogResult.OK)
         {
            AddComboSelection(cboFilePath, dlg.SelectedPath);
         }
      }

      /// <summary>
      /// Calculates the width of the drop down list of the given combo box
      /// </summary>
      /// <param name="combo">Combo box to base calculate from</param>
      /// <returns>Width of longest string in combo box items</returns>
      /// <history>
      /// [Curtis_Beard]    11/21/2005	Created
      /// </history>
      private int CalculateDropDownWidth(ComboBox combo)
      {
         const int EXTRA = 10;

         Graphics g = combo.CreateGraphics();
         int _max = combo.Width;
         string _itemValue = string.Empty;
         SizeF _size;

         foreach (object _item in combo.Items)
         {
            _itemValue = _item.ToString();
            _size = g.MeasureString(_itemValue, combo.Font);

            if (_size.Width > _max)
               _max = Convert.ToInt32(_size.Width);
         }

         // keep original width if no item longer
         if (_max != combo.Width)
            _max += EXTRA;

         return _max;
      }

      /// <summary>
      /// Truncate the given file's name if it is to long to fit in the given status bar
      /// </summary>
      /// <param name="file">FileInfo object to measure</param>
      /// <param name="status">StatusBar to measure against</param>
      /// <returns>file name or truncated file name if to long</returns>
      /// <history>
      /// [Curtis_Beard]	   04/21/2006	Created, fixes bug 1367852
      /// </history>
      private string TruncateFileName(System.IO.FileInfo file, StatusBar status)
      {
         const int EXTRA = 20;     //used for spacing of the sizer
         Graphics g = status.CreateGraphics();
         int _strLen = 0;
         string _name = file.FullName;

         _strLen = Convert.ToInt32(g.MeasureString(_name, status.Font).Width);
         if (_strLen >= (status.Width - EXTRA))
         {
            // truncate to just the root name and the file name (for now)
            _name = file.Directory.Root.Name + @"...\" + file.Name;
         }

         g.Dispose();

         return _name;
      }

      /// <summary>
      /// Processes any command line arguments
      /// </summary>
      /// <param name="args">CommandLineProcessing.CommandLineArguments</param>
      /// <history>
      /// [Curtis_Beard]		07/25/2006	ADD: 1492221, command line parameters
      /// [Curtis_Beard]		05/18/2007	CHG: adapt to new processing
      /// </history>
      private void ProcessCommandLine(CommandLineProcessing.CommandLineArguments args)
      {
         if (args.AnyArguments)
         {
            //if (args.IsProjectFile)
               //LoadProjectFile(args.ProjectFile);

            if (args.IsValidStartPath)
               AddComboSelection(cboFilePath, args.StartPath);

            if (args.IsValidFileTypes)
               AddComboSelection(cboFileName, args.FileTypes);

            if (args.IsValidSearchText)
               AddComboSelection(cboSearchForText, args.SearchText);

            // turn on option if specified (options default to last saved otherwise)
            if (args.UseRegularExpressions)
               chkRegularExpressions.Checked = true;
            if (args.IsCaseSensitive)
               chkCaseSensitive.Checked = true;
            if (args.IsWholeWord)
               chkWholeWordOnly.Checked = true;
            if (args.UseRecursion)
               chkRecurse.Checked = true;
            if (args.IsFileNamesOnly)
               chkFileNamesOnly.Checked = true;
            if (args.IsNegation)
               chkNegation.Checked = true;
            if (args.UseLineNumbers)
               chkLineNumbers.Checked = true;
            if (args.ContextLines > -1)
               txtContextLines.Value = args.ContextLines;
            //if (args.SkipHidden)
            //   chkSkipHidden.Checked = true;
            //if (args.SkipSystem)
            //   chkSkipSystem.Checked = true;

            // keep last to ensure all options are set before a search begins
            if (args.StartSearch)
            {
               btnSearch_Click(null, null);
               this.Show();
               this.Refresh();
            }
         }
      }

      /// <summary>
      /// Save results to a text file
      /// </summary>
      /// <param name="path">Fully qualified file path</param>
      /// <history>
      /// [Curtis_Beard]		09/06/2006	Created
      /// </history>
      private void SaveResultsAsText(string path)
      {
         System.IO.StreamWriter writer = null;

         try
         {
            // Open the file
            writer = new System.IO.StreamWriter(path, false, System.Text.Encoding.Default);

            SetStatusBarMessage(String.Format(Language.GetGenericText("SaveSaving"), path));

            // loop through File Names list
            for (int _index = 0; _index < lstFileNames.Items.Count; _index++)
            {
               HitObject _hit = __Grep.RetrieveHitObject(int.Parse(lstFileNames.Items[_index].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text));

               // write info to a file
               writer.WriteLine("-------------------------------------------------------------------------------");
               writer.WriteLine(_hit.FilePath);
               writer.WriteLine("-------------------------------------------------------------------------------");
               writer.Write(_hit.Lines);
               writer.WriteLine("");

               // clear hit object
               _hit = null;
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(String.Format(Language.GetGenericText("SaveError"), ex.ToString()), Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
         finally
         {
            // Close file
            if (writer != null)
            {
               writer.Flush();
               writer.Close();
            }

            SetStatusBarMessage(Language.GetGenericText("SaveSaved"));
         }
      }

      /// <summary>
      /// Save results to a html file
      /// </summary>
      /// <param name="path">Fully qualified file path</param>
      /// <history>
      /// [Curtis_Beard]		09/06/2006	Created
      /// </history>
      private void SaveResultsAsHTML(string path)
      {
         StreamWriter writer = null;

         try
         {
            SetStatusBarMessage(string.Format(Language.GetGenericText("SaveSaving"), path));

            // Open the file
            writer = new StreamWriter(path, false, System.Text.Encoding.Default);

            string repeat = string.Empty;
            string repeatSection;
            var allSections = new System.Text.StringBuilder();
            string repeater;
            var lines = new System.Text.StringBuilder();
            string template = HTMLHelper.GetContents("Output.html");
            string css = HTMLHelper.GetContents("Output.css");
            int totalHits = 0;

            if (__Grep.SearchSpec.ReturnOnlyFileNames)
               template = HTMLHelper.GetContents("Output-fileNameOnly.html");

            css = HTMLHelper.ReplaceCssHolders(css);
            template = template.Replace("%%style%%", css);
            template = template.Replace("%%title%%", "AstroGrep Results");

            int rStart = template.IndexOf("[repeat]");
            int rStop = template.IndexOf("[/repeat]") + "[/repeat]".Length;
            repeat = template.Substring(rStart, rStop - rStart);

            repeatSection = repeat;
            repeatSection = repeatSection.Replace("[repeat]", string.Empty);
            repeatSection = repeatSection.Replace("[/repeat]", string.Empty);

            // loop through File Names list
            for (int _index = 0; _index < lstFileNames.Items.Count; _index++)
            {
               HitObject _hit = __Grep.RetrieveHitObject(int.Parse(lstFileNames.Items[_index].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text));

               lines = new System.Text.StringBuilder();
               repeater = repeatSection;
               repeater = repeater.Replace("%%file%%", _hit.FilePath);
               totalHits += _hit.HitCount;

               for (int _jIndex = 0; _jIndex < _hit.LineCount; _jIndex++)
                  lines.Append(HTMLHelper.GetHighlightLine(_hit.RetrieveLine(_jIndex), __Grep));

               repeater = repeater.Replace("%%lines%%", lines.ToString());

               // clear hit object
               _hit = null;

               allSections.Append(repeater);
            }

            template = template.Replace(repeat, allSections.ToString());
            template = template.Replace("%%totalhits%%", totalHits.ToString());
            template = HTMLHelper.ReplaceSearchOptions(template, __Grep);

            // write out template to the file
            writer.WriteLine(template);
         }
         catch (Exception ex)
         {
            MessageBox.Show(string.Format(Language.GetGenericText("SaveError"), ex), Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
         finally
         {
            // Close file
            if (writer != null)
            {
               writer.Flush();
               writer.Close();
            }

            SetStatusBarMessage(Language.GetGenericText("SaveSaved"));
         }
      }

      /// <summary>
      /// Save results to a xml file
      /// </summary>
      /// <param name="path">Fully qualified file path</param>
      /// <history>
      /// [Curtis_Beard]		09/06/2006	Created
      /// </history>
      private void SaveResultsAsXML(string path)
      {
         System.Xml.XmlTextWriter writer = null;

         try
         {
            SetStatusBarMessage(string.Format(Language.GetGenericText("SaveSaving"), path));

            // Open the file
            writer = new System.Xml.XmlTextWriter(path, System.Text.Encoding.UTF8);
            writer.Formatting = System.Xml.Formatting.Indented;

            writer.WriteStartDocument(true);
            writer.WriteStartElement("astrogrep");
            writer.WriteAttributeString("version", "1.0");

            // write out search options
            writer.WriteStartElement("options");
            writer.WriteElementString("searchPath", __Grep.StartDirectory);
            writer.WriteElementString("fileTypes", __Grep.FileFilterSpec.FileFilter);
            writer.WriteElementString("searchText", __Grep.SearchSpec.SearchText);
            writer.WriteElementString("regularExpressions", __Grep.SearchSpec.UseRegularExpressions.ToString());
            writer.WriteElementString("caseSensitive", __Grep.SearchSpec.UseCaseSensitivity.ToString());
            writer.WriteElementString("wholeWord", __Grep.SearchSpec.UseWholeWordMatching.ToString());
            writer.WriteElementString("recurse", __Grep.SearchSpec.SearchInSubfolders.ToString());
            writer.WriteElementString("showFileNamesOnly", __Grep.SearchSpec.ReturnOnlyFileNames.ToString());
            writer.WriteElementString("negation", __Grep.SearchSpec.UseNegation.ToString());
            writer.WriteElementString("lineNumbers", __Grep.SearchSpec.IncludeLineNumbers.ToString());
            writer.WriteElementString("contextLines", __Grep.SearchSpec.ContextLines.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("search");
            writer.WriteAttributeString("totalfiles", __Grep.Greps.Count.ToString());

            // get total hits
            int totalHits = 0;
            for (int _index = 0; _index < lstFileNames.Items.Count; _index++)
            {
               HitObject _hit = __Grep.RetrieveHitObject(int.Parse(lstFileNames.Items[_index].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text));

               // add to total
               totalHits += _hit.HitCount;

               // clear hit object
               _hit = null;
            }
            writer.WriteAttributeString("totalfound", totalHits.ToString());

            for (int _index = 0; _index < lstFileNames.Items.Count; _index++)
            {
               writer.WriteStartElement("item");
               HitObject _hit = __Grep.RetrieveHitObject(int.Parse(lstFileNames.Items[_index].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text));

               writer.WriteAttributeString("file", _hit.FilePath);
               writer.WriteAttributeString("total", _hit.HitCount.ToString());

               // write out lines
               for (int _jIndex = 0; _jIndex < _hit.LineCount; _jIndex++)
                  writer.WriteElementString("line", _hit.RetrieveLine(_jIndex));

               // clear hit object
               _hit = null;

               writer.WriteEndElement();
            }

            writer.WriteEndElement();   //search
            writer.WriteEndElement();   //astrogrep
         }
         catch (Exception ex)
         {
            MessageBox.Show(string.Format(Language.GetGenericText("SaveError"), ex.ToString()), Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
         finally
         {
            // Close file
            if (writer != null)
            {
               writer.Flush();
               writer.Close();
            }

            SetStatusBarMessage(Language.GetGenericText("SaveSaved"));
         }
      }

      /// <summary>
      /// Set the view states of the controls.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      05/16/2007  ADD: created
      /// </history>
      private void LoadViewStates()
      {
         // set view state of results preview
         //if (Core.GeneralSettings.ShowResultsPreview)
         //{
            __FileListHeight = Core.GeneralSettings.WindowFilePanelHeight;
         //}
         //txtHits.Visible = !Core.GeneralSettings.ShowResultsPreview;
         //mnuViewPreview_Click(null, null);
         //if (!Core.GeneralSettings.ShowResultsPreview)
         //{
         //  __FileListHeight = Core.GeneralSettings.DEFAULT_FILE_PANEL_HEIGHT;
         //}

         // Set view state of status bar
         //mnuViewStatusBar.Checked = Core.GeneralSettings.ShowStatusBar;
         //stbStatus.Visible = Core.GeneralSettings.ShowStatusBar;
      }

      #endregion

      #region Menu Events
      /// <summary>
      /// Enable/Disable menu items if listview contains items
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Curtis_Beard]      11/03/2005	Created
      /// </history>
      private void mnuFile_Select(object sender, EventArgs e)
      {
         if (lstFileNames.Items.Count == 0)
         {
            mnuSaveResults.Enabled = false;
            mnuPrintResults.Enabled = false;
         }
         else
         {
            mnuSaveResults.Enabled = true;
            mnuPrintResults.Enabled = true;
         }
      }

      /// <summary>
      /// Select a folder for the search path.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   11/22/2006	Created
      /// </history>
      private void mnuBrowse_Click(object sender, System.EventArgs e)
      {
         BrowseForFolder();
      }

      /// <summary>
      /// Save the results to a file
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Curtis_Beard]	   01/11/2005	Initial
      /// [Curtis_Beard]	   10/14/2005	CHG: use No Results for message box title 
      /// [Curtis_Beard]	   12/07/2005	CHG: Use column constant
      /// [Curtis_Beard]	   09/06/2006	CHG: Update to support html and xml output
      /// </history>
      private void mnuSaveResults_Click(object sender, System.EventArgs e)
      {
         SaveFileDialog dlg = new SaveFileDialog();
         dlg.CheckPathExists = true;
         dlg.AddExtension = true;
         dlg.Title = Language.GetGenericText("SaveDialogTitle");
         dlg.Filter = "Text (*.txt)|*.txt|HTML (*.html)|*.html|XML (*.xml)|*.xml";

         // only show dialog if information to save
         if (lstFileNames.Items.Count > 0)
         {
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
               switch (dlg.FilterIndex)
               {
                  case 1:
                     // Save to text
                     SaveResultsAsText(dlg.FileName);
                     break;
                  case 2:
                     // Save to html
                     SaveResultsAsHTML(dlg.FileName);
                     break;
                  case 3:
                     // Save to xml
                     SaveResultsAsXML(dlg.FileName);
                     break;
               }
            }
         }
         else
            MessageBox.Show(Language.GetGenericText("SaveNoResults"), Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
      }

      /// <summary>
      /// Show Print Dialog
      /// </summary>
      /// <param name="sender">system parm</param>
      /// <param name="e">system parm</param>
      /// <history>
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// [Curtis_Beard]	   09/10/2005	CHG: pass in listView and grep hashtable
      /// [Curtis_Beard]	   10/14/2005	CHG: use No Results for message box title
      /// [Curtis_Beard]	   12/07/2005	CHG: Pass in font name and size to print dialog
      /// [Curtis_Beard]	   10/11/2006	CHG: Pass in font and icon
      /// </history>
      private void mnuPrintResults_Click(object sender, EventArgs e)
      {
         if (lstFileNames.Items.Count > 0)
         {
            var _form = new frmPrint(lstFileNames, __Grep.Greps, txtHits.Font, Icon);
            _form.ShowDialog(this);
            _form = null;
         }
         else
            MessageBox.Show(Language.GetGenericText("PrintNoResults"), Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
      }

      /// <summary>
      /// Menu Exit
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Theodore_Ward]     ??/??/????  Initial
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// </history>
      private void mnuExit_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      /// <summary>
      /// Enable/Disable menu items if listview contains items
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Curtis_Beard]      11/03/2005	Created
      /// </history>
      private void mnuEdit_Select(object sender, EventArgs e)
      {
         if (lstFileNames.Items.Count == 0)
         {
            mnuSelectAll.Enabled = false;
            mnuOpenSelected.Enabled = false;
         }
         else
         {
            mnuSelectAll.Enabled = true;
            mnuOpenSelected.Enabled = true;
         }
      }

      /// <summary>
      /// Menu Select All Event
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Theodore_Ward]     ??/??/????  Initial
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// </history>
      private void mnuSelectAll_Click(object sender, System.EventArgs e)
      {
         for (int i = 0; i < lstFileNames.Items.Count; i++)
            lstFileNames.Items[i].Selected = true;
      }

      /// <summary>
      /// Open Selected Files Event
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Theodore_Ward]     ??/??/????  Initial
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// [Curtis_Beard]	   12/07/2005	CHG: Use column constant
      /// [Curtis_Beard]	   07/26/2006	ADD: 1512026, column position
      /// </history>
      private void mnuOpenSelected_Click(object sender, System.EventArgs e)
      {
         string path;
         HitObject hit;

         for (int i = 0; i < lstFileNames.SelectedItems.Count; i++)
         {
            // retrieve hit object
            hit = __Grep.RetrieveHitObject(int.Parse(lstFileNames.SelectedItems[i].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text));

            // retrieve the filename
            path = hit.FilePath;

            // open the default editor
            Common.EditFile(path, 1, 1);
         }
      }

      /// <summary>
      /// Clear MRU Event
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Theodore_Ward]     ??/??/????  Initial
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// [Curtis_Beard]	   11/22/2006	CHG: Remove use of browse in combobox
      /// </history>
      private void mnuClearMRU_Click(object sender, System.EventArgs e)
      {
         cboFilePath.Items.Clear();
         cboFileName.Items.Clear();
         cboSearchForText.Items.Clear();

         AstroGrep.Core.GeneralSettings.SearchStarts = string.Empty;
         AstroGrep.Core.GeneralSettings.SearchFilters = string.Empty;
         AstroGrep.Core.GeneralSettings.SearchTexts = string.Empty;
         AstroGrep.Core.GeneralSettings.Save();
      }

      /// <summary>
      /// Save Search Settings Event
      /// </summary>
      /// <param name="sender">system parm</param>
      /// <param name="e">system parm</param>
      /// <history>
      /// [Curtis_Beard]	   01/28/2005	Created
      /// </history>
      private void mnuSaveSearchSettings_Click(object sender, System.EventArgs e)
      {
         if (VerifyInterface())
         {
            SaveSearchSettings();
         }
      }      

      /// <summary>
      /// Menu Options Event
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Theodore_Ward]		??/??/????  Initial
      /// [Curtis_Beard]		01/11/2005	.Net Conversion
      /// [Curtis_Beard]		11/10/2006	ADD: Update combo boxes and language changes
      /// [Curtis_Beard]		11/22/2006	CHG: Remove use of browse in combobox
      /// [Curtis_Beard]		05/22/2007	FIX: 1723814, rehighlight the selected result
      /// </history>
      private void mnuOptions_Click(object sender, System.EventArgs e)
      {
         frmOptions _form = new frmOptions();

         if (_form.ShowDialog(this) == DialogResult.OK)
         {
            //update combobox lengths
            while (cboFilePath.Items.Count > AstroGrep.Core.GeneralSettings.MaximumMRUPaths)
               cboFilePath.Items.RemoveAt(cboFilePath.Items.Count - 1);
            while (cboFileName.Items.Count > AstroGrep.Core.GeneralSettings.MaximumMRUPaths)
               cboFileName.Items.RemoveAt(cboFileName.Items.Count - 1);
            while (cboSearchForText.Items.Count > AstroGrep.Core.GeneralSettings.MaximumMRUPaths)
               cboSearchForText.Items.RemoveAt(cboSearchForText.Items.Count - 1);

            // load new language if necessary
            if (_form.IsLanguageChange)
            {
               Language.Load(Core.GeneralSettings.Language);
               Language.ProcessForm(this, this.toolTip1);

               SetColumnsText();

               // reload label
               __SearchOptionsText = lnkSearchOptions.Text;
               if (!__OptionsShow)
                  lnkSearchOptions.Text = String.Format(__SearchOptionsText, ">>");
               else
                  lnkSearchOptions.Text = String.Format(__SearchOptionsText, "<<");

               // clear statusbar text
               stbStatus.Text = string.Empty;
            }

            // change results display and rehighlight
            txtHits.ForeColor = Common.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsForeColor);
            txtHits.BackColor = Common.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsBackColor);
            lstFileNames_SelectedIndexChanged(null, null);
         }
         _form = null;
      }

      /// <summary>
      /// Menu About Event
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Theodore_Ward]		??/??/????  Initial
      /// [Curtis_Beard]		01/11/2005	.Net Conversion
      /// </history>
      private void mnuAbout_Click(object sender, System.EventArgs e)
      {
         frmAbout _form = new frmAbout();

         _form.ShowDialog(this);
         _form = null;
      }

		/// <summary>
		/// Sends all selected items from the file list to the clipboard
		/// </summary>
		/// /// <history>
		/// [Ed_Jakbuowski]     05/20/2009  Created
		/// </history>
		private void CopyMenuItem_Click(object sender, System.EventArgs e)
		{
			if (lstFileNames.SelectedItems.Count <= 0)
				return;
			string data = "";
			try
			{
				foreach(ListViewItem lvi in lstFileNames.SelectedItems)
					data += lvi.Text+ ", " + lvi.SubItems[1].Text + ", " + lvi.SubItems[2].Text + ", " + lvi.SubItems[3].Text + Environment.NewLine;
				Clipboard.SetDataObject(data);
			}
			catch(Exception ex)
			{
				MessageBox.Show("Exception: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Need certain keyboard events on the lstFileNames.
		/// </summary>
		/// /// <history>
		/// [Ed_Jakbuowski]     05/20/2009  Created
		/// </history>
		private void lstFileNames_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.C && e.Control) //ctrl+c  Copy to clipboard
				CopyMenuItem_Click(sender, EventArgs.Empty);
			if (e.KeyCode == Keys.A && e.Control) //ctrl+a  Select All
			{
				foreach (ListViewItem lvi in lstFileNames.Items)
					lvi.Selected = true;
			}
			// I think the delete key is a bad idea, because its too easy to modify the results by mistake.
			//if (e.KeyCode == Keys.Delete) //delete
			//	DeleteMenuItem_Click(sender, EventArgs.Empty);

		}

		/// <summary>
		/// Context Menu item for opening selected files
		/// </summary>
		/// /// <history>
		/// [Ed_Jakbuowski]     05/20/2009  Created
		/// </history>
		private void OpenMenuItem_Click(object sender, System.EventArgs e)
		{
			mnuOpenSelected_Click(sender, e);
		}

		/// <summary>
		/// Context Menu item for opening selected file's Directory
		/// </summary>
		/// /// <history>
		/// [Ed_Jakbuowski]     05/20/2009  Created
		/// </history>
		private void OpenFolderMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				foreach(ListViewItem lvi in lstFileNames.SelectedItems)
				{
					string folder = lvi.SubItems[1].Text;
					if (Directory.Exists(folder))
					{
						System.Diagnostics.Process.Start("Explorer.exe" , folder);
					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show("Exception: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Context Menu Item for deleting items from the list.
		/// </summary>
		/// /// <history>
		/// [Ed_Jakbuowski]     05/20/2009  Created
		/// </history>
		private void DeleteMenuItem_Click(object sender, System.EventArgs e)
		{
			if (lstFileNames.SelectedItems.Count <= 0)
				return;
		
			try
			{
				while(lstFileNames.SelectedItems.Count > 0)
					lstFileNames.SelectedItems[0].Remove();
			}
			catch(Exception ex)
			{
				MessageBox.Show("Exception: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

      #endregion

      #region Grep Events
      /// <summary>
      /// Handles the Grep object's SearchingFile event
      /// </summary>
      /// <param name="file">FileInfo object containg currently being search file</param>
      /// <history>
      /// [Curtis_Beard]		10/17/2005	Created
      /// [Curtis_Beard]		12/02/2005	CHG: handle SearchingFile event instead of StatusMessage
      /// [Curtis_Beard]		04/21/2006	CHG: truncate the file name if necessary
      /// </history>
      private void ReceiveSearchingFile(System.IO.FileInfo file)
      {
         string message = string.Format(Language.GetGenericText("SearchSearching"), TruncateFileName(file, stbStatus));

         SetStatusBarMessage(message);
      }

      /// <summary>
      /// A file has been detected to contain the searching text
      /// </summary>
      /// <param name="file">File detected to contain searching text</param>
      /// <param name="index">Position in GrepCollection</param>
      /// <history>
      /// [Curtis_Beard]		10/17/2005	Created
      /// </history>
      private void ReceiveFileHit(System.IO.FileInfo file, int index)
      {
         AddHitToList(file, index);
      }

      /// <summary>
      /// A line has been detected to contain the searching text
      /// </summary>
      /// <param name="hit">The HitObject that contains the line</param>
      /// <param name="index">The position in the HitObject's line collection</param>
      /// <history>
      /// [Curtis_Beard]		11/04/2005   Created
      /// </history>
      private void ReceiveLineHit(HitObject hit, int index)
      {
         UpdateHitCount(hit);
      }

      /// <summary>
      /// Receives the search error event when a file search causes an uncatchable error
      /// </summary>
      /// <param name="file">FileInfo object of error file</param>
      /// <param name="ex">Exception message</param>
      /// <history>
      /// [Curtis_Beard]		03/14/2006	Created
      /// [Curtis_Beard]		05/28/2007  CHG: use Exception and display error
      /// [Curtis_Beard]		08/07/2007  ADD: 1741735, better search error handling
      /// </history>
      private void ReceiveSearchError(System.IO.FileInfo file, Exception ex)
      {
         string message = string.Empty;
         if (file == null)
            message = string.Format(Language.GetGenericText("SearchGenericError"), ex.Message);
         else
            message = string.Format(Language.GetGenericText("SearchFileError"), file.FullName, ex.Message);

         __ErrorCollection.Add(message);
      }

      /// <summary>
      /// Receives the search cancel event when the grep has been cancelled.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/12/2006	Created
      /// [Curtis_Beard]		08/07/2007  ADD: 1741735, display any search errors
      /// </history>
      private void ReceiveSearchCancel()
      {
         string message = Language.GetGenericText("SearchCancelled");

         SetStatusBarMessage(message);
         SetSearchState(true);

         DisplaySearchErrors();
      }

      /// <summary>
      /// Receives the search complete event when the grep has completed.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/12/2006	Created
      /// [Curtis_Beard]		06/27/2007  CHG: removed message parameter
      /// [Curtis_Beard]		08/07/2007  ADD: 1741735, display any search errors
      /// [Ed_Jakubowski]		05/20/2009  ADD: Display the Count
      /// </history>
      private void ReceiveSearchComplete()
      {
         string message = Language.GetGenericText("SearchFinished");
         
         SetStatusBarMessage(message + "    Count: " + this.lstFileNames.Items.Count.ToString());
         SetSearchState(true);

         DisplaySearchErrors();
      }

      /// <summary>
      /// Display any search errors that were logged.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		08/07/2007  ADD: 1741735, better search error handling
      /// </history>
      private void DisplaySearchErrors()
      {
         if (this.InvokeRequired)
         {
            DisplaySearchErrorsCallBack del = new DisplaySearchErrorsCallBack(DisplaySearchErrors);
            this.Invoke(del);
            return;
         }

         string[] msgs = new string[__ErrorCollection.Count];
         __ErrorCollection.CopyTo(msgs, 0);

         if (msgs.Length > 0)
         {
            // create a simple form to display the errors
            Form frmError = new Form();
            frmError.Name = "frmError";
            frmError.StartPosition =  FormStartPosition.CenterParent;
            frmError.Size = new Size(this.Width - 50, this.Height / 2);
            frmError.Text = Constants.ProductName;
            frmError.ShowInTaskbar = false;
            frmError.MinimizeBox = false;
            frmError.Icon = null;
            frmError.FormBorderStyle = FormBorderStyle.SizableToolWindow;

            TextBox txtMsg = new TextBox();
            txtMsg.Dock = DockStyle.Fill;
            txtMsg.ReadOnly = true;
            txtMsg.BackColor = System.Drawing.SystemColors.Window;
            txtMsg.Lines = msgs;
            txtMsg.Name = "txtMsg";
            txtMsg.Multiline = true;
            txtMsg.ScrollBars = ScrollBars.Both;
            txtMsg.Select(0,0);
            txtMsg.WordWrap = false;

            frmError.Controls.Add(txtMsg);
            frmError.ShowDialog(this);
            frmError.Dispose();
         }
      }

      /// <summary>
      /// Updates the count column (Thread safe)
      /// </summary>
      /// <param name="hit">HitObject that contains updated information</param>
      /// <history>
      /// [Curtis_Beard]		11/21/2005  Created
      /// </history>
      private void UpdateHitCount(HitObject hit)
      {
         // Makes this a thread safe operation
         if (lstFileNames.InvokeRequired)
         {
            UpdateHitCountCallBack _delegate = new UpdateHitCountCallBack(UpdateHitCount);
            lstFileNames.Invoke(_delegate, new object[1] {hit});
            return;
         }

         // find correct item to update
         foreach (ListViewItem _item in lstFileNames.Items)
         {
            if (int.Parse(_item.SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text) == hit.Index)
            {
               _item.SubItems[Constants.COLUMN_INDEX_COUNT].Text = hit.HitCount.ToString();
               break;
            }
         }
      }

      /// <summary>
      /// Set the status bar message text. (Thread Safe)
      /// </summary>
      /// <param name="message">Text to display</param>
      /// <history>
      /// [Curtis_Beard]		01/27/2007	Created
      /// </history>
      private void SetStatusBarMessage(string message)
      {
         if (stbStatus.InvokeRequired)
         {
            UpdateStatusMessageCallBack _delegate = new UpdateStatusMessageCallBack(SetStatusBarMessage);
            stbStatus.Invoke(_delegate, new object[1] {message});
            return;
         }

         stbStatus.Text = message;
      }


      /// <summary>
      /// Start the searching
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		10/17/2005	Created
      /// [Curtis_Beard]		07/03/2006	FIX: 1516775, Remove trim on the search expression
      /// [Curtis_Beard]		07/12/2006	CHG: moved thread actions to grep class
      /// [Curtis_Beard]		11/22/2006	CHG: Remove use of browse in combobox
      /// [Curtis_Beard]		08/07/2007  ADD: 1741735, better search error handling
      /// [Curtis_Beard]		08/21/2007  FIX: 1778467, make sure file pattern is correct if a '\' is present
      /// </history>
      private void StartSearch()
      {
         string _path;
         string _fileName;

          try
         {
            _fileName = cboFileName.Text;
            _path = cboFilePath.Text.Trim();
            string _expression = cboSearchForText.Text;

            // update combo selections
            AddComboSelection(cboSearchForText, _expression);
            AddComboSelection(cboFileName, _fileName);
            AddComboSelection(cboFilePath, _path);

            // Ensure that there is a backslash.
            if (!_path.EndsWith(Path.DirectorySeparatorChar.ToString()))
               _path += Path.DirectorySeparatorChar.ToString();

            // update path and fileName if fileName has a path in it
            int slashPos = _fileName.LastIndexOf(Path.DirectorySeparatorChar.ToString());
				if (slashPos > -1)
				{
					// fileName has a slash, so append the directory and get the file filter
					_path += _fileName.Substring(0, slashPos);
					_fileName = _fileName.Substring(slashPos + 1);
				}

            // disable gui
            SetSearchState(false);

            // reset display
            SetStatusBarMessage(string.Empty);
            ClearItems();
            txtHits.Clear();

            // Clear search errors
            __ErrorCollection.Clear();

            __Grep = new Grep(GetSearchSpecFromUI(),GetFilterSpecFromUI());

            string[] extensions = Core.GeneralSettings.ExtensionExcludeList.Split(';');
            foreach (string ext in extensions)
                __Grep.AddExclusionExtension(ext.ToLower());

            __Grep.Plugins = Core.PluginManager.Items;

            __Grep.StartDirectory = _path;

            // attach events
            __Grep.FileHit += ReceiveFileHit;
            __Grep.LineHit += ReceiveLineHit;
            __Grep.SearchCancel += ReceiveSearchCancel;
            __Grep.SearchComplete += ReceiveSearchComplete;
            __Grep.SearchError += ReceiveSearchError;
            __Grep.SearchingFile += ReceiveSearchingFile;

            __Grep.BeginExecute();
         }
         catch (Exception ex)
         {
            __ErrorCollection.Add(ex.Message);
            DisplaySearchErrors();
         }
      }

      /// <summary>
      /// Clears the file list (Thread safe).
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/10/2006	Created
      /// </history>
      private void ClearItems()
      {
         if (lstFileNames.InvokeRequired)
         {
            ClearItemsCallBack _delegate = ClearItems;
            lstFileNames.Invoke(_delegate);
            return;
         }

         lstFileNames.Items.Clear();
      }

    // todo: move or replace me
    struct SearchSpec : ISearchSpec
    {
        public bool SearchInSubfolders { get;  set; }
        public bool UseRegularExpressions { get;  set; }
        public bool UseCaseSensitivity { get;  set; }
        public bool UseWholeWordMatching { get;  set; }
        public bool UseNegation { get;  set; }
        public int ContextLines { get;  set; }
        public string SearchText { get;  set; }
        public bool ReturnOnlyFileNames { get;  set; }
        public bool IncludeLineNumbers { get;  set; }
    }


    // todo: move or replace me
    struct FileFilterSpec : IFileFilterSpec
    {
        public string FileFilter { get;  set; }
        public bool SkipHiddenFiles { get;  set; }
        public bool SkipSystemFiles { get;  set; }
        public DateTime DateModifiedStare { get;  set; }
        public DateTime DateModifiedEnd { get;  set; }
        public int FileSizeMin { get;  set; }
        public int FileSizeMax { get;  set; }
        public string FileNameRegex { get;  set; }
    }


         private IFileFilterSpec GetFilterSpecFromUI()
         {
             return new FileFilterSpec
                        {
                            FileFilter = null,
                            SkipHiddenFiles = chkHiddenFiles.Checked,
                            SkipSystemFiles = chkSystemFiles.Checked,
                            DateModifiedStare = DateTime.MinValue,
                            DateModifiedEnd = DateTime.MaxValue,
                            FileSizeMin = 0,
                            FileSizeMax = int.MaxValue,
                            FileNameRegex = null
                        };
         }


       /// <summary>
      /// Sets the grep options
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		10/17/2005	Created
      /// [Curtis_Beard]		07/28/2006  ADD: extension exclusion list
      /// [Andrew_Radford]		13/08/2009  CHG: Now retruns ISearchSpec rather than altering global state
      /// </history>
      private ISearchSpec GetSearchSpecFromUI()
      {
          return new SearchSpec
                        {
                            UseCaseSensitivity = chkCaseSensitive.Checked,
                            ContextLines = Convert.ToInt32(txtContextLines.Value),
                            IncludeLineNumbers = chkLineNumbers.Checked,
                            UseNegation = chkNegation.Checked,
                            ReturnOnlyFileNames = chkFileNamesOnly.Checked,
                            SearchInSubfolders = chkRecurse.Checked,
                            UseRegularExpressions = chkRegularExpressions.Checked,
                            UseWholeWordMatching = chkWholeWordOnly.Checked,
                            SearchText = cboSearchForText.Text
                        };  
      }

      /// <summary>
      /// Add a file hit to the listview (Thread safe).
      /// </summary>
      /// <param name="file">File to add</param>
      /// <param name="index">Position in GrepCollection</param>
      /// <history>
      /// [Curtis_Beard]		10/17/2005	Created
      /// [Curtis_Beard]		12/02/2005	CHG: Add the count column
      /// [Curtis_Beard]		07/07/2006	CHG: Make thread safe
      /// [Curtis_Beard]		09/14/2006	CHG: Update to use date's ToString method
      /// </history>
      private void AddHitToList(FileInfo file, int index)
      {
         if (lstFileNames.InvokeRequired)
         {
            AddToListCallBack _delegate = AddHitToList;
            lstFileNames.Invoke(_delegate, new object[2] {file, index});
            return;
         }

          // Create the list item
         var _listItem = new ListViewItem(file.Name);
         _listItem.SubItems.Add(file.DirectoryName);
         _listItem.SubItems.Add(file.LastWriteTime.ToString());
         _listItem.SubItems.Add("0");
         // must be last
         _listItem.SubItems.Add(index.ToString());

         // Add list item to listview
         lstFileNames.Items.Add(_listItem);

         // clear it out
         _listItem = null;
      }
      #endregion

      private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {

      }

      private void panel2_Paint(object sender, PaintEventArgs e)
      {

      }

      private void label4_Click(object sender, EventArgs e)
      {

      }



    }
}
