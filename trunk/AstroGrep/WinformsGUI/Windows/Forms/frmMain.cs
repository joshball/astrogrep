using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

using AstroGrep.Core;
using AstroGrep.Output;
using AstroGrep.Windows.Controls;
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
   /// [Theodore_Ward]      ??/??/????  Initial
   /// [Curtis_Beard]       01/11/2005  .Net Conversion/Comments/Option Strict
   /// [Curtis_Beard]       10/15/2005	CHG: Replace search procedures
   /// [Andrew_Radford]     17/08/2008	CHG: Moved Winforms designer stuff to a .designer file
   /// [Curtis_Beard]	    03/07/2012	ADD: 3131609, exclusions
   /// [Curtis_Beard]		09/26/2012	CHG: 3572487, move command line logic to program.cs and use property for value
   /// </history>
   public partial class frmMain : Form
   {
      #region Declarations

      private bool __OptionsShow = true;
      private int __SortColumn = -1;
      private Grep __Grep = null;
      private string __SearchOptionsText = "Search Options {0}";
      private int __FileListHeight = Core.GeneralSettings.DEFAULT_FILE_PANEL_HEIGHT;
      private readonly List<LogItem> LogItems = new List<LogItem>();
      private List<FilterItem> __FilterItems = new List<FilterItem>();
      private CommandLineProcessing.CommandLineArguments __CommandLineArgs = new CommandLineProcessing.CommandLineArguments();
      private bool isSearching = false;

      private System.ComponentModel.IContainer components;

      #endregion

      #region Delegate Declarations

      private delegate void UpdateHitCountCallBack(HitObject hit);
      private delegate void SetSearchStateCallBack(bool enable);
      private delegate void UpdateStatusMessageCallBack(string message);
      private delegate void UpdateStatusCountCallBack(int count);
      private delegate void CalculateTotalCountCallBack();
      private delegate void ClearItemsCallBack();
      private delegate void AddToListCallBack(FileInfo file, int index);
      private delegate void DisplaySearchMessagesCallBack(LogItem.LogItemTypes? displayType);
      private delegate void DisplayExclusionErrorMessagesCallBack();
      private delegate void RestoreTaskBarProgressCallBack();

      #endregion

      /// <summary>
      /// Gets/Sets the command line arguments
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		09/26/2012	Initial: 3572487
      /// </history>
      public CommandLineProcessing.CommandLineArguments CommandLineArgs
      {
         get { return __CommandLineArgs; }
         set { __CommandLineArgs = value; }
      }

      /// <summary>
      /// Creates an instance of the frmMain class.
      /// </summary>
      /// /// <history>
      /// [Theodore_Ward]     ??/??/????  Created
      /// [Curtis_Beard]      11/02/2006	CHG: Conversion to C#, setup event handlers
      /// [Curtis_Beard]      02/09/2012	FIX: 3486074, set modification date end to max value
      /// [Curtis_Beard]	   02/24/2012	CHG: 3488321, ability to change results font
      /// [Curtis_Beard]	   09/18/2013	ADD: 58, add drag/drop support for path
      /// </history>
      public frmMain()
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         API.ListViewExtensions.SetTheme(lstFileNames);

         // Attach event handlers
         Resize += frmMain_Resize;
         Closed += frmMain_Closed;
         pnlMainSearch.Paint += pnlMainSearch_Paint;
         pnlSearch.SizeChanged += pnlSearch_SizeChanged;
         PanelOptionsContainer.Paint += PanelOptionsContainer_Paint;
         splitLeftRight.Paint += splitLeftRight_Paint;
         splitUpDown.Paint += splitUpDown_Paint;
         mnuFile.Select += mnuFile_Select;
         mnuEdit.Select += mnuEdit_Select;
         mnuView.Select += mnuView_Select;
         cboFilePath.DropDown += cboFilePath_DropDown;
         cboFilePath.DragDrop += cboFilePath_DragDrop;
         cboFilePath.DragEnter += cboFilePath_DragEnter;
         cboFilePath.AllowDrop = true;
         cboFileName.DropDown += cboFileName_DropDown;
         cboSearchForText.DropDown += cboSearchForText_DropDown;
         chkNegation.CheckedChanged += chkNegation_CheckedChanged;
         chkFileNamesOnly.CheckedChanged += chkFileNamesOnly_CheckedChanged;
         txtHits.MouseDown += txtHits_MouseDown;
         txtHits.Enter += txtHits_Enter;
         txtHits.Leave += txtHits_Leave;
         txtHits.KeyDown += txtHits_KeyDown;
         lstFileNames.Enter += lstFileNames_Enter;
         lstFileNames.Leave += lstFileNames_Leave;
         lstFileNames.MouseDown += lstFileNames_MouseDown;
         lstFileNames.ColumnClick += lstFileNames_ColumnClick;
         lstFileNames.ItemDrag += lstFileNames_ItemDrag;
         //sbStatusPanel.DoubleClick += new EventHandler(sbStatusPanel_DoubleClick);
         sbErrorCountPanel.DoubleClick += new EventHandler(sbErrorCountPanel_DoubleClick);
         sbFilterCountPanel.DoubleClick += new EventHandler(sbFilterCountPanel_DoubleClick);
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
      /// [Ed_Jakubowski]		10/29/2009	CHG: Fix for Startup Path when using Mono 2.4
      /// [Curtis_Beard]		01/30/2012	CHG: 1955653, set focus to search text on startup
      /// [Curtis_Beard]	   02/24/2012	CHG: 3489693, save state of search options
      /// [Curtis_Beard]		09/26/2012	CHG: 3572487, remove init of command line options (now in program.cs)
      /// </history>
      private void frmMain_Load(object sender, System.EventArgs e)
      {
         // set defaults
         txtContextLines.Maximum = Constants.MAX_CONTEXT_LINES;
         lnkSearchOptions.Text = __SearchOptionsText;

         // Load language
         //Language.GenerateXml(this, Application.StartupPath + "\\" + this.Name + ".xml");
         Language.ProcessForm(this, this.toolTip1);

         // set member to hold language specified text
         __SearchOptionsText = lnkSearchOptions.Text;

         // Hide the Search Options
         __OptionsShow = !Core.GeneralSettings.ShowSearchOptions;
         ShowSearchOptions();

         // Load the general settings
         Legacy.ConvertGeneralSettings();
         LoadSettings();

         // Load the search settings
         Legacy.ConvertSearchSettings();
         int line = Core.SearchSettings.ContextLines;
         LoadSearchSettings();

         // Make sure in Mono to set the command-line path
         if (CommandLineArgs.AnyArguments && CommandLineArgs.IsValidStartPath)
         {
            cboFilePath.Text = CommandLineArgs.StartPath;
         }

         // Delete registry entry (if exist)
         Legacy.DeleteRegistry();

         // Load plugins
         Core.PluginManager.Load();

         // set view state of controls
         LoadViewStates();

         // Handle any command line arguments
         ProcessCommandLine();

         // set focus to search text combobox
         cboSearchForText.Select();
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
      /// [Curtis_Beard]	   10/16/2012	CHG: Save search settings on exit
      /// </history>
      private void frmMain_Closed(object sender, EventArgs e)
      {
         SaveSettings();

         if (Core.GeneralSettings.SaveSearchOptionsOnExit)
         {
            SaveSearchSettings();
         }

         Application.Exit();
      }

      /// <summary>
      /// Allows Ctrl-F keyboard event to set focus to search text field.
      /// </summary>
      /// <param name="msg">system parameter for system message</param>
      /// <param name="keyData">system parameter for keys pressed</param>
      /// <returns>true if processed, false otherwise</returns>
      /// <history>
      /// [Curtis_Beard]	   10/27/2014	CHG: 87, set focus to search text field for ctrl-f
      /// </history>
      protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
      {
         if (keyData == (Keys.Control | Keys.F))
         {
            cboSearchForText.Focus();
            return true;
         }

         return base.ProcessCmdKey(ref msg, keyData);
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
      /// [Curtis_Beard]      02/21/2014  FIX: 50, set back text after change so user entered text isn't cleared
      /// </history>
      private void pnlSearch_SizeChanged(object sender, EventArgs e)
      {
         string temp = cboFilePath.Text;
         cboFilePath.DropDownStyle = ComboBoxStyle.DropDownList;
         cboFilePath.DropDownStyle = ComboBoxStyle.DropDown;
         cboFilePath.Text = temp;

         temp = cboFileName.Text;
         cboFileName.DropDownStyle = ComboBoxStyle.Simple;
         cboFileName.DropDownStyle = ComboBoxStyle.DropDown;
         cboFileName.Text = temp;

         temp = cboSearchForText.Text;
         cboSearchForText.DropDownStyle = ComboBoxStyle.Simple;
         cboSearchForText.DropDownStyle = ComboBoxStyle.DropDown;
         cboSearchForText.Text = temp;

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
         } while (index < MAX);
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
         } while (index < MAX);
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
      /// Show the Search Exclusions dialog
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private void lnkExclusions_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
      {
         var dlg = new frmExclusions(__FilterItems);
         if (dlg.ShowDialog(this) == DialogResult.OK)
         {
            __FilterItems = dlg.FilterItems;
         }
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
      /// [Theodore_Ward]       ??/??/????  Initial
      /// [Curtis_Beard]	    01/11/2005	.Net Conversion
      /// [Curtis_Beard]	    10/30/2012	ADD: 28, search within results
      /// </history>
      private void btnSearch_Click(object sender, System.EventArgs e)
      {
         if (!VerifyInterface())
            return;

         StartSearch(false);
      }

      /// <summary>
      /// Search in Results Event
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	    10/30/2012	ADD: 28, search within results
      /// </history>
      private void mnuSearchInResults_Click(object sender, EventArgs e)
      {
         if (!VerifyInterface())
            return;

         StartSearch(true);
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
            string hitLine = string.Empty;

            // Make sure there is something to click on.
            if (lstFileNames.SelectedItems.Count == 0)
               return;

            // retrieve the hit object
            HitObject hit = __Grep.RetrieveHitObject(int.Parse(lstFileNames.SelectedItems[0].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text));

            // Find out the line number the cursor is on.
            lineNumber = txtHits.GetLineFromCharIndex(txtHits.SelectionStart);
            int orgLineNumber = lineNumber;

            // might need to adjust line number when using word wrap
            /*if (txtHits.WordWrap)
            {
               int wrappedCount = 0;

               System.Diagnostics.Debug.WriteLine(string.Format("Starting line number: {0}", lineNumber));

               for (int i = lineNumber; i >= 0; i--)
               {
                  int charIndex = txtHits.GetFirstCharIndexFromLine(i);
                  Point pos = txtHits.GetPositionFromCharIndex(charIndex);
                  char firstChar = txtHits.GetCharFromPosition(pos);
                  char secondChar = txtHits.GetCharFromPosition(txtHits.GetPositionFromCharIndex(charIndex + 1));
                  
                  string line = hit.RetrieveLine(i);
                  char lineFirstChar = (!string.IsNullOrEmpty(line) && line.Length > 0) ? line[0] : Char.Parse(" ");
                  char lineSecondChar = (!string.IsNullOrEmpty(line) && line.Length > 0 && line.Length > 1) ? line[1] : Char.Parse(" ");

                  System.Diagnostics.Debug.WriteLine(string.Format("Character 1:{0}, character 2:{1} at line {2}.  Hit Character 1:{3}, hit character 2:{4}", 
                     firstChar, secondChar, i, lineFirstChar, lineSecondChar));

                  //if (firstChar != '\n' && firstChar != lineFirstChar)
                  if (firstChar != '\n' && (firstChar != lineFirstChar || (firstChar == lineFirstChar && secondChar != lineSecondChar)))
                  {
                     wrappedCount++;
                  }
               }

               lineNumber = lineNumber - wrappedCount;
               System.Diagnostics.Debug.WriteLine(string.Format("Current line number: {0}, wrapped count: {1}", lineNumber, wrappedCount));
            }

            // safety check
            if (lineNumber < 0)
            {
               lineNumber = orgLineNumber;
            }*/

            // Use the cursor's linenumber to get the hit's line number.
            hitLineNumber = hit.RetrieveLineNumber(lineNumber);

            hitColumn = hit.RetrieveColumn(lineNumber);

            // might need 
            hitLine = hit.RetrieveLine(lineNumber);

            // Retrieve the filename
            string path = hit.FilePath;

            // Open the default editor.
            TextEditors.EditFile(path, hitLineNumber, hitColumn, hitLine);
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		03/20/2014	CHG: 73, enter key opens editor of file list
      /// </history>
      private void txtHits_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            e.SuppressKeyPress = true;
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		03/20/2014	CHG: 73, enter key opens editor of file list
      /// </history>
      private void txtHits_Leave(object sender, EventArgs e)
      {
         AcceptButton = btnSearch;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		03/20/2014	CHG: 73, enter key opens editor of file list
      /// </history>
      private void txtHits_Enter(object sender, EventArgs e)
      {
         AcceptButton = null;
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
      /// [Curtis_Beard]	   02/24/2012	CHG: 3488322, use first hit line/column position instead of line 1, column 1
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

            // open the default editor at first hit
            int index = hit.RetrieveFirstHitIndex();
            TextEditors.EditFile(path, hit.RetrieveLineNumber(index), hit.RetrieveColumn(index), hit.RetrieveLine(index));
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
      /// [Curtis_Beard]	   02/24/2012	CHG: 3488322, use hand cursor for results view to signal click
      /// [Curtis_Beard]	   09/28/2012	CHG: only attempt file show when 1 item is selected (prevents flickering and loading on all deselect)
      /// </history>
      private void lstFileNames_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (lstFileNames.SelectedItems.Count == 1 && !isSearching)
         {
            // set to hand cursor so users get a hint that they can double click
            if (txtHits.Cursor != Cursors.Hand)
            {
               txtHits.Cursor = Cursors.Hand;
            }

            // retrieve hit object
            HitObject hit = __Grep.RetrieveHitObject(int.Parse(lstFileNames.SelectedItems[0].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text));

            SetStatusBarEncoding(hit.DetectedEncoding.EncodingName);

            HighlightText(hit);

            hit = null;
         }

         if (lstFileNames.SelectedItems.Count == 0)
         {
            SetStatusBarEncoding(string.Empty);
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
      /// [Curtis_Beard]		02/17/2012	CHG: update listview sorting
      /// </history>
      private void lstFileNames_ColumnClick(object sender, ColumnClickEventArgs e)
      {
         int clearColIndex = -1;

         // set to wait cursor
         lstFileNames.Cursor = Cursors.WaitCursor;

         // Determine whether the column is the same as the last column clicked.
         if (e.Column != __SortColumn)
         {
            // Remove sort indicator
            if (__SortColumn != -1)
            {
               clearColIndex = __SortColumn;
            }

            // Set the sort column to the new column.
            __SortColumn = e.Column;

            // Set the sort order to ascending by default.
            if (e.Column == Constants.COLUMN_INDEX_COUNT ||
               e.Column == Constants.COLUMN_INDEX_SIZE ||
               e.Column == Constants.COLUMN_INDEX_DATE)
            {
               lstFileNames.Sorting = SortOrder.Descending;
            }
            else
            {
               lstFileNames.Sorting = SortOrder.Ascending;
            }
         }
         else
         {
            // Determine what the last sort order was and change it.
            if (lstFileNames.Sorting == SortOrder.Ascending)
               lstFileNames.Sorting = SortOrder.Descending;
            else
               lstFileNames.Sorting = SortOrder.Ascending;
         }

         // Set the ListViewItemSorter property to a new ListViewItemComparer object.
         ListViewItemComparer comparer = new ListViewItemComparer(e.Column, lstFileNames.Sorting);
         lstFileNames.ListViewItemSorter = comparer;

         // Call the sort method to manually sort.
         lstFileNames.Sort();

         // Display sort image and highlight sort column
         Windows.API.ListViewExtensions.SetSortIcon(lstFileNames, e.Column, lstFileNames.Sorting);

         // Apply theming since sorting removes it.
         Windows.API.ListViewExtensions.SetTheme(lstFileNames);

         // restore to default cursor
         lstFileNames.Cursor = Cursors.Default;
      }

      /// <summary>
      /// Handles setting up the drag event for a selected file.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/25/2006	ADD: 1512028, Drag support
      /// [Andrew_Radford]		26/09/2009	FIX: 2864409, Drag and drop to editor open only 1 file
      /// </history>
      private void lstFileNames_ItemDrag(object sender, ItemDragEventArgs e)
      {
         var lst = sender as ListView;
         var paths = new List<string>();

         foreach (ListViewItem item in lst.SelectedItems)
         {
            var path = item.SubItems[Constants.COLUMN_INDEX_DIRECTORY].Text +
             Path.DirectorySeparatorChar +
             item.SubItems[Constants.COLUMN_INDEX_FILE].Text;

            if (File.Exists(path))
            {
               paths.Add(path);
            }
         }

         var dataObject = new DataObject(DataFormats.FileDrop, paths.ToArray());
         lstFileNames.DoDragDrop(dataObject, DragDropEffects.Copy);
      }

      /// <summary>
      /// Handles setting the form's AcceptButton to btnSearch so that enter key defaults to btnSearch for rest of form.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		03/20/2014	CHG: 73, enter key opens editor of file list
      /// </history>
      private void lstFileNames_Leave(object sender, EventArgs e)
      {
         AcceptButton = btnSearch;
      }

      /// <summary>
      /// Handles setting the form's AcceptButton to null so that enter key can be processed by control.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// /// <history>
      /// [Curtis_Beard]		03/20/2014	CHG: 73, enter key opens editor of file list
      /// </history>
      private void lstFileNames_Enter(object sender, EventArgs e)
      {
         AcceptButton = null;
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

      /// <summary>
      /// Displays the exclusions messages.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   09/27/2012	ADD: 1741735, better error handling display
      /// </history>
      private void sbFilterCountPanel_DoubleClick(object sender, EventArgs e)
      {
         DisplaySearchMessages(LogItem.LogItemTypes.Exclusion);
      }

      /// <summary>
      /// Displays the error messages.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   09/27/2012	ADD: 1741735, better error handling display
      /// </history>
      private void sbErrorCountPanel_DoubleClick(object sender, EventArgs e)
      {
         DisplaySearchMessages(LogItem.LogItemTypes.Error);
      }

      /// <summary>
      /// Displays the status messages.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   09/27/2012	ADD: 1741735, better error handling display
      /// </history>
      private void sbStatusPanel_DoubleClick(object sender, EventArgs e)
      {
         DisplaySearchMessages(LogItem.LogItemTypes.Status);
      }

      /// <summary>
      /// Handles the DragEnter event for the path combo box.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <remarks>
      /// Handles file drops only by putting the directory into the combo box.
      /// </remarks>
      /// <history>
      /// [Curtis_Beard]	   09/18/2013	ADD: 58, add drag/drop support for path
      /// </history>
      private void cboFilePath_DragEnter(object sender, DragEventArgs e)
      {
         if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
         {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
               e.Effect = DragDropEffects.Copy;
            }
         }
      }

      /// <summary>
      /// Handles the DragDrop event for the path combo box.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <remarks>
      /// Handles file drops only by putting the directory into the combo box.
      /// </remarks>
      /// <history>
      /// [Curtis_Beard]	   09/18/2013	ADD: 58, add drag/drop support for path
      /// </history>
      private void cboFilePath_DragDrop(object sender, DragEventArgs e)
      {
         string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
         if (files != null && files.Length > 0)
         {
            string path1 = files[0];
            if (Directory.Exists(path1))
            {
               AddComboSelection(cboFilePath, path1);
            }
            else if (File.Exists(path1))
            {
               FileInfo file = new FileInfo(path1);
               AddComboSelection(cboFilePath, file.DirectoryName);
            }
         }
      }
      #endregion

      #region Private Methods
      /// <summary>
      /// Load the general settings values.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   10/11/2006	Created
      /// [Curtis_Beard]	   11/22/2006	CHG: Remove use of browse in combobox
      /// [Curtis_Beard]	   02/24/2012	CHG: 3488321, ability to change results font
      /// [Curtis_Beard]	   10/10/2012	ADD: 3479503, ability to change file list font
      /// </history>
      private void LoadSettings()
      {
         //  Only load up to the desired number of paths.
         if (AstroGrep.Core.GeneralSettings.MaximumMRUPaths < 0 || AstroGrep.Core.GeneralSettings.MaximumMRUPaths > Constants.MAX_STORED_PATHS)
         {
            AstroGrep.Core.GeneralSettings.MaximumMRUPaths = Constants.MAX_STORED_PATHS;
         }

         LoadComboBoxEntry(cboFilePath, AstroGrep.Core.GeneralSettings.SearchStarts);
         LoadComboBoxEntry(cboFileName, AstroGrep.Core.GeneralSettings.SearchFilters);
         LoadComboBoxEntry(cboSearchForText, AstroGrep.Core.GeneralSettings.SearchTexts);

         // Path
         if (cboFilePath.Items.Count > 0 && cboFilePath.Items.Count != 1)
         {
            cboFilePath.SelectedIndex = 0;

            SetWindowText();
         }

         // Filter
         if (cboFileName.Items.Count == 0)
         {
            // no entries so create defaults
            cboFileName.Items.AddRange(new object[] { "*.*", "*.txt", "*.java", "*.htm, *.html", "*.jsp, *.asp", "*.js, *.inc", "*.htm, *.html, *.jsp, *.asp", "*.sql", "*.bas, *.cls, *.vb", "*.cs", "*.cpp, *.c, *.h", "*.asm" });
         }
         cboFileName.SelectedIndex = 0;

         // Search
         if (cboSearchForText.Items.Count > 0)
         {
            cboSearchForText.SelectedIndex = 0;
         }

         // Results Window
         txtHits.ForeColor = Convertors.ConvertStringToColor(Core.GeneralSettings.ResultsForeColor);
         txtHits.BackColor = Convertors.ConvertStringToColor(Core.GeneralSettings.ResultsBackColor);
         txtHits.Font = Convertors.ConvertStringToFont(Core.GeneralSettings.ResultsFont);
         txtHits.WordWrap = Core.GeneralSettings.ResultsWordWrap;

         // File list columns
         lstFileNames.Font = Convertors.ConvertStringToFont(Core.GeneralSettings.FilePanelFont);
         SetColumnsText();

         LoadWindowSettings();

         // Load the text editors
         TextEditors.Load();
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
      /// [Curtis_Beard]	   01/31/2012	ADD: save size column width
      /// [Curtis_Beard]		10/27/2014	CHG: 88, add file extension column
      /// </history>
      private void SaveSettings()
      {
         SaveWindowSettings();

         //save column widths
         Core.GeneralSettings.WindowFileColumnNameWidth = lstFileNames.Columns[Constants.COLUMN_INDEX_FILE].Width;
         Core.GeneralSettings.WindowFileColumnLocationWidth = lstFileNames.Columns[Constants.COLUMN_INDEX_DIRECTORY].Width;
         Core.GeneralSettings.WindowFileColumnDateWidth = lstFileNames.Columns[Constants.COLUMN_INDEX_DATE].Width;
         Core.GeneralSettings.WindowFileColumnCountWidth = lstFileNames.Columns[Constants.COLUMN_INDEX_COUNT].Width;
         Core.GeneralSettings.WindowFileColumnSizeWidth = lstFileNames.Columns[Constants.COLUMN_INDEX_SIZE].Width;
         Core.GeneralSettings.WindowFileColumnFileExtWidth = lstFileNames.Columns[Constants.COLUMN_INDEX_FILE_EXT].Width;

         //save divider panel positions
         Core.GeneralSettings.WindowSearchPanelWidth = pnlSearch.Width;
         Core.GeneralSettings.WindowFilePanelHeight = lstFileNames.Height;

         //save search comboboxes
         Core.GeneralSettings.SearchStarts = Convertors.GetComboBoxEntriesAsString(cboFilePath);
         Core.GeneralSettings.SearchFilters = Convertors.GetComboBoxEntriesAsString(cboFileName);
         Core.GeneralSettings.SearchTexts = Convertors.GetComboBoxEntriesAsString(cboSearchForText);

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
            string[] items = Convertors.GetComboBoxEntriesFromString(values);

            if (items.Length > 0)
            {
               int start = items.Length;
               if (start > Core.GeneralSettings.MaximumMRUPaths)
               {
                  start = Core.GeneralSettings.MaximumMRUPaths;
               }

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
      /// [Curtis_Beard]	   01/31/2012	ADD: 1561584, ability to skip hidden/system files/directories
      /// [Curtis_Beard]	   02/07/2012	FIX: 3485448, save modified start/end date, min/max file sizes
      /// [Curtis_Beard]      02/09/2012  ADD: 3424156, size drop down selection
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// [Curtis_Beard]	   02/04/2014	FIX: use NumericUpDown's Value property instead of text
      /// </history>
      private void LoadSearchSettings()
      {
         chkRegularExpressions.Checked = Core.SearchSettings.UseRegularExpressions;
         chkCaseSensitive.Checked = Core.SearchSettings.UseCaseSensitivity;
         chkWholeWordOnly.Checked = Core.SearchSettings.UseWholeWordMatching;
         chkLineNumbers.Checked = Core.SearchSettings.IncludeLineNumbers;
         chkRecurse.Checked = Core.SearchSettings.UseRecursion;
         chkFileNamesOnly.Checked = Core.SearchSettings.ReturnOnlyFileNames;
         txtContextLines.Value = Core.SearchSettings.ContextLines;
         chkNegation.Checked = Core.SearchSettings.UseNegation;
         __FilterItems = FilterItem.ConvertStringToFilterItems(Core.SearchSettings.FilterItems);
      }

      /// <summary>
      /// Save the search options.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   10/11/2006	Created
      /// [Curtis_Beard]	   01/31/2012	ADD: 1561584, ability to skip hidden/system files/directories
      /// [Curtis_Beard]	   02/07/2012	FIX: 3485448, save modified start/end date, min/max file sizes
      /// [Curtis_Beard]      02/09/2012  ADD: 3424156, size drop down selection
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// [Curtis_Beard]	   02/04/2014	FIX: use NumericUpDown's Value property instead of text
      /// </history>
      private void SaveSearchSettings()
      {
         Core.SearchSettings.UseRegularExpressions = chkRegularExpressions.Checked;
         Core.SearchSettings.UseCaseSensitivity = chkCaseSensitive.Checked;
         Core.SearchSettings.UseWholeWordMatching = chkWholeWordOnly.Checked;
         Core.SearchSettings.IncludeLineNumbers = chkLineNumbers.Checked;
         Core.SearchSettings.UseRecursion = chkRecurse.Checked;
         Core.SearchSettings.ReturnOnlyFileNames = chkFileNamesOnly.Checked;
         Core.SearchSettings.ContextLines = Convert.ToInt32(txtContextLines.Value);
         Core.SearchSettings.UseNegation = chkNegation.Checked;
         Core.SearchSettings.FilterItems = FilterItem.ConvertFilterItemsToString(__FilterItems);

         Core.SearchSettings.Save();
      }

      /// <summary>
      /// Show/Hide the Search Options Panel
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   02/05/2005	Created
      /// [Curtis_Beard]	   02/24/2012	CHG: 3489693, save state of search options
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

         Core.GeneralSettings.ShowSearchOptions = __OptionsShow;
      }

      /// <summary>
      /// Sets the file list's columns' text to the correct language.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/25/2006	Created
      /// [Curtis_Beard]	   01/31/2012	ADD: size column width/language
      /// [Curtis_Beard]		10/27/2014	CHG: 88, add file extension column
      /// </history>
      private void SetColumnsText()
      {
         if (lstFileNames.Columns.Count == 0)
         {
            lstFileNames.Columns.Add(Language.GetGenericText("ResultsColumnFile"), Core.GeneralSettings.WindowFileColumnNameWidth, HorizontalAlignment.Left);
            lstFileNames.Columns.Add(Language.GetGenericText("ResultsColumnLocation"), Core.GeneralSettings.WindowFileColumnLocationWidth, HorizontalAlignment.Left);
            lstFileNames.Columns.Add(Language.GetGenericText("ResultsColumnFileExt"), Core.GeneralSettings.WindowFileColumnFileExtWidth, HorizontalAlignment.Left);
            lstFileNames.Columns.Add(Language.GetGenericText("ResultsColumnDate"), Core.GeneralSettings.WindowFileColumnDateWidth, HorizontalAlignment.Left);
            lstFileNames.Columns.Add(Language.GetGenericText("ResultsColumnSize"), Core.GeneralSettings.WindowFileColumnSizeWidth, HorizontalAlignment.Left);
            lstFileNames.Columns.Add(Language.GetGenericText("ResultsColumnCount"), Core.GeneralSettings.WindowFileColumnCountWidth, HorizontalAlignment.Left);
         }
         else
         {
            lstFileNames.Columns[Constants.COLUMN_INDEX_FILE].Text = Language.GetGenericText("ResultsColumnFile");
            lstFileNames.Columns[Constants.COLUMN_INDEX_DIRECTORY].Text = Language.GetGenericText("ResultsColumnLocation");
            lstFileNames.Columns[Constants.COLUMN_INDEX_FILE_EXT].Text = Language.GetGenericText("ResultsColumnFileExt");
            lstFileNames.Columns[Constants.COLUMN_INDEX_SIZE].Text = Language.GetGenericText("ResultsColumnSize");
            lstFileNames.Columns[Constants.COLUMN_INDEX_DATE].Text = Language.GetGenericText("ResultsColumnDate");
            lstFileNames.Columns[Constants.COLUMN_INDEX_COUNT].Text = Language.GetGenericText("ResultsColumnCount");
         }

         AddContextMenuForResults();
      }

      /// <summary>
      /// Verify user selected options
      /// </summary>
      /// <returns>True - Verified, False - Otherwise</returns>
      /// <history>
      /// [Theodore_Ward]   ??/??/????  Initial
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// [Curtis_Beard]	   10/14/2005	CHG: Use max context lines constant in message
      /// [Ed_Jakubowski]	   05/20/2009  CHG: Allow filename only searching
      /// [Curtis_Beard]	   01/31/2012	CHG: 3424154/1816655, allow multiple starting directories
      /// [Curtis_Beard]	   08/01/2012	FIX: 3553252, use | character for path delimitation character
      /// [Curtis_Beard]	   09/27/2012	FIX: 1881938, validate regular expression
      /// [Curtis_Beard]	   02/12/2014	ADD: check for empty minimum file count, use tryparse instead of try/catch for context lines
      /// </history>
      private bool VerifyInterface()
      {
         try
         {
            int _lines = -1;
            if (!int.TryParse(txtContextLines.Text, out _lines) || _lines < 0 || _lines > Constants.MAX_CONTEXT_LINES)
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

            string[] paths = cboFilePath.Text.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string path in paths)
            {
               if (!System.IO.Directory.Exists(path))
               {
                  MessageBox.Show(String.Format(Language.GetGenericText("VerifyErrorInvalidStartPath"), path),
                     Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                  return false;
               }
            }

            //if (cboSearchForText.Text.Trim().Equals(string.Empty))
            //{
            //   MessageBox.Show(Language.GetGenericText("VerifyErrorNoSearchText"),
            //      Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //   return false;
            //}

            if (chkRegularExpressions.Checked && !cboSearchForText.Text.Trim().Equals(string.Empty))
            {
               // test reg ex
               try
               {
                  var reg = new Regex(cboSearchForText.Text, RegexOptions.IgnoreCase);
               }
               catch (Exception ex)
               {
                  MessageBox.Show(String.Format(Language.GetGenericText("VerifyErrorInvalidRegEx"), ex.Message),
                     Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                  return false;
               }
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
            {
               combo.Items.Remove(item);
            }

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
      /// [Ed_Jakubowski]     05/20/2009  CHG: Skip highlight if hitCount = 0
      /// [Curtis_Beard]		01/24/2012	CHG: allow back color use again since using .Net v2+
      /// [Curtis_Beard]      10/27/2014	CHG: 85, remove leading white space, add newline for display, fix windows sounds for empty text
      /// [Curtis_Beard]      11/26/2014	FIX: don't highlight found text that is part of the spacer text
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
         txtHits.ForeColor = Convertors.ConvertStringToColor(Core.GeneralSettings.ResultsForeColor);
         txtHits.BackColor = Convertors.ConvertStringToColor(Core.GeneralSettings.ResultsBackColor);

         if (__Grep.SearchSpec.UseRegularExpressions)
         {
            HighlightTextRegEx(hit);
         }
         else
         {
            // Loop through hits and highlight search for text
            int _index = 0;
            int maxLines = 5000;
            if (hit.LineCount < maxLines)
               maxLines = hit.LineCount;

            for (_index = 0; _index < maxLines; _index++)
            {
               // Retrieve hit text
               string _textToSearch = hit.RetrieveLine(_index);
               string spacerText = hit.RetrieveSpacerText(_index);

               if (GeneralSettings.RemoveLeadingWhiteSpace)
               {
                  _textToSearch = _textToSearch.TrimStart();
               }

               _textToSearch = string.Format("{0}{1}", spacerText, _textToSearch);

               // Set default font
               txtHits.SelectionFont = txtHits.Font;

               _tempLine = _textToSearch;

               // attempt to locate the text in the line
               int _pos = 0;
               if (__Grep.SearchSpec.UseCaseSensitivity)
               {
                  _pos = _tempLine.IndexOf(_searchText, spacerText.Length);
               }
               else
               {
                  _pos = _tempLine.ToLower().IndexOf(_searchText.ToLower(), spacerText.Length);
               }

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
                     txtHits.SelectionColor = Convertors.ConvertStringToColor(Core.GeneralSettings.ResultsForeColor);
                     txtHits.SelectionBackColor = Convertors.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsBackColor);
                     txtHits.SelectedText = _begin;

                     // do a check to see if begin and end are valid for wholeword searches
                     bool _highlight;
                     if (__Grep.SearchSpec.UseWholeWordMatching)
                     {
                        _highlight = Grep.WholeWordOnly(_begin, _end);
                     }
                     else
                     {
                        _highlight = true;
                     }

                     // set highlight color for searched text
                     if (_highlight)
                     {
                        txtHits.SelectionColor = Convertors.ConvertStringToColor(Core.GeneralSettings.HighlightForeColor);
                        txtHits.SelectionBackColor = Convertors.ConvertStringToColor(AstroGrep.Core.GeneralSettings.HighlightBackColor);
                     }
                     txtHits.SelectedText = _text;

                     // Check remaining string for other hits in same line
                     if (__Grep.SearchSpec.UseCaseSensitivity)
                     {
                        _pos = _end.IndexOf(_searchText);
                     }
                     else
                     {
                        _pos = _end.ToLower().IndexOf(_searchText.ToLower());
                     }

                     // set default color for end, if no more hits in line
                     _tempLine = _end;
                     if (_pos < 0)
                     {
                        txtHits.SelectionColor = Convertors.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsForeColor);
                        txtHits.SelectionBackColor = Convertors.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsBackColor);
                        if (!string.IsNullOrEmpty(_end))
                        {
                           txtHits.SelectedText = _end;
                        }
                     }

                  } while (_pos > -1);
               }
               else
               {
                  // set default color, no search text found
                  txtHits.SelectionColor = Convertors.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsForeColor);
                  txtHits.SelectionBackColor = Convertors.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsBackColor);
                  if (!string.IsNullOrEmpty(_textToSearch))
                  {
                     txtHits.SelectedText = _textToSearch;
                  }
               }

               txtHits.SelectedText = Environment.NewLine;
            }
         }

         if (!txtHits.WordWrap)
         {
            // check for really long lines
            var width = TextRenderer.MeasureText(txtHits.Text, txtHits.Font).Width;
            if (width > 20000)
            {
               txtHits.RightMargin = width;
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
      /// [Curtis_Beard]		01/24/2012	CHG: allow back color use again since using .Net v2+
      /// [Curtis_Beard]      10/27/2014	CHG: 85, remove leading white space, add newline for display, fix windows sounds for empty text
      /// [Curtis_Beard]      11/26/2014	FIX: don't highlight found text that is part of the spacer text
      /// </history>
      private void HighlightTextRegEx(HitObject hit)
      {
         string _textToSearch = string.Empty;
         string spacerText = string.Empty;
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
            spacerText = hit.RetrieveSpacerText(_index);

            if (GeneralSettings.RemoveLeadingWhiteSpace)
            {
               _textToSearch = _textToSearch.TrimStart();
            }

            _textToSearch = string.Format("{0}{1}", spacerText, _textToSearch);

            // Set default font
            txtHits.SelectionFont = txtHits.Font;

            // find all reg ex matches in line
            if (__Grep.SearchSpec.UseCaseSensitivity && __Grep.SearchSpec.UseWholeWordMatching)
            {
               _regEx = new Regex("\\b" + __Grep.SearchSpec.SearchText + "\\b");
               _col = _regEx.Matches(_textToSearch, spacerText.Length);
            }
            else if (__Grep.SearchSpec.UseCaseSensitivity)
            {
               _regEx = new Regex(__Grep.SearchSpec.SearchText);
               _col = _regEx.Matches(_textToSearch, spacerText.Length);
            }
            else if (__Grep.SearchSpec.UseWholeWordMatching)
            {
               _regEx = new Regex("\\b" + __Grep.SearchSpec.SearchText + "\\b", RegexOptions.IgnoreCase);
               _col = _regEx.Matches(_textToSearch, spacerText.Length);
            }
            else
            {
               _regEx = new Regex(__Grep.SearchSpec.SearchText, RegexOptions.IgnoreCase);
               _col = _regEx.Matches(_textToSearch, spacerText.Length);
            }

            // loop through the matches
            _lastPos = 0;
            for (_counter = 0; _counter < _col.Count; _counter++)
            {
               _item = _col[_counter];

               // set the start text
               txtHits.SelectionColor = Convertors.ConvertStringToColor(Core.GeneralSettings.ResultsForeColor);
               txtHits.SelectionBackColor = Convertors.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsBackColor);

               // check for empty string to prevent assigning nothing to selection text preventing
               //  a system beep
               _tempString = _textToSearch.Substring(_lastPos, _item.Index - _lastPos);
               if (!_tempString.Equals(string.Empty))
               {
                  txtHits.SelectedText = _tempString;
               }

               // set the hit text
               txtHits.SelectionColor = Convertors.ConvertStringToColor(Core.GeneralSettings.HighlightForeColor);
               txtHits.SelectionBackColor = Convertors.ConvertStringToColor(AstroGrep.Core.GeneralSettings.HighlightBackColor);
               txtHits.SelectedText = _textToSearch.Substring(_item.Index, _item.Length);

               // set the end text
               txtHits.SelectionColor = Convertors.ConvertStringToColor(Core.GeneralSettings.ResultsForeColor);
               txtHits.SelectionBackColor = Convertors.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsBackColor);
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
               txtHits.SelectionColor = Convertors.ConvertStringToColor(Core.GeneralSettings.ResultsForeColor);
               txtHits.SelectionBackColor = Convertors.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsBackColor);
               if (!string.IsNullOrEmpty(_textToSearch))
               {
                  txtHits.SelectedText = _textToSearch;
               }
            }

            txtHits.SelectedText = Environment.NewLine;
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
      /// [Curtis_Beard]	    10/30/2012	ADD: 28, search within results
      /// </history>
      private void SetSearchState(bool enable)
      {
         if (this.InvokeRequired)
         {
            SetSearchStateCallBack _delegate = new SetSearchStateCallBack(SetSearchState);
            this.Invoke(_delegate, new Object[1] { enable });
            return;
         }

         mnuFile.Enabled = enable;
         mnuEdit.Enabled = enable;
         mnuView.Enabled = enable;
         mnuTools.Enabled = enable;
         mnuHelp.Enabled = enable;

         btnSearch.ContextMenu.MenuItems[0].Enabled = (enable && lstFileNames.Items.Count > 0);
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
      /// [Curtis_Beard]		05/06/2014	CHG: 76, use vista based folder selection (downgrades to normal FolderBrowserDialog)
      /// </history>
      private void BrowseForFolder()
      {
         VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();
         if (!API.IsWindowsVistaOrLater)
         {
            dlg.Description = Language.GetGenericText("OpenFolderDescription");
            dlg.ShowNewFolderButton = false;
         }

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

         g.Dispose();

         return _max;
      }

      /// <summary>
      /// Truncate the given file's name if it is to long to fit in the given status bar
      /// </summary>
      /// <param name="file">FileInfo object to measure</param>
      /// <param name="status">StatusStrip to measure against</param>
      /// <returns>file name or truncated file name if to long</returns>
      /// <history>
      /// [Curtis_Beard]	   04/21/2006	Created, fixes bug 1367852
      /// </history>
      private string TruncateFileName(System.IO.FileInfo file, StatusStrip status)
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
      /// <history>
      /// [Curtis_Beard]		07/25/2006	ADD: 1492221, command line parameters
      /// [Curtis_Beard]		05/18/2007	CHG: adapt to new processing
      /// [Curtis_Beard]		09/26/2012	CHG: 3572487, remove args parameter and use class property
      /// [Curtis_Beard]		04/08/2014	CHG: 74, add missing search options
      /// </history>
      private void ProcessCommandLine()
      {
         if (CommandLineArgs.AnyArguments)
         {
            if (CommandLineArgs.IsValidStartPath)
               AddComboSelection(cboFilePath, CommandLineArgs.StartPath);

            if (CommandLineArgs.IsValidFileTypes)
               AddComboSelection(cboFileName, CommandLineArgs.FileTypes);

            if (CommandLineArgs.IsValidSearchText)
               AddComboSelection(cboSearchForText, CommandLineArgs.SearchText);

            // turn on option if specified (options default to last saved otherwise)
            if (CommandLineArgs.UseRegularExpressions)
               chkRegularExpressions.Checked = true;
            if (CommandLineArgs.IsCaseSensitive)
               chkCaseSensitive.Checked = true;
            if (CommandLineArgs.IsWholeWord)
               chkWholeWordOnly.Checked = true;
            if (CommandLineArgs.UseRecursion)
               chkRecurse.Checked = true;
            if (CommandLineArgs.IsFileNamesOnly)
               chkFileNamesOnly.Checked = true;
            if (CommandLineArgs.IsNegation)
               chkNegation.Checked = true;
            if (CommandLineArgs.UseLineNumbers)
               chkLineNumbers.Checked = true;
            if (CommandLineArgs.ContextLines > -1)
               txtContextLines.Value = CommandLineArgs.ContextLines;

            if (CommandLineArgs.SkipHiddenFile)
            {
               // check if exists, enable if necessary, or create them
               var fiFile = GetFilterItemsByFilterType(new FilterType(FilterType.Categories.File, FilterType.SubCategories.Hidden));

               if (fiFile == null || fiFile.Count == 0)
               {
                  __FilterItems.Add(new FilterItem(new FilterType(FilterType.Categories.File, FilterType.SubCategories.Hidden),
                     string.Empty, FilterType.ValueOptions.None, false, true));
               }
               else if (!fiFile.First().Enabled)
               {
                  int index = __FilterItems.IndexOf(fiFile.First());
                  if (index > -1)
                  {
                     __FilterItems[index].Enabled = true;
                  }
               }
            }

            if (CommandLineArgs.SkipHiddenDirectory)
            {
               // check if exists, enable if necessary, or create them
               var fiDir = GetFilterItemsByFilterType(new FilterType(FilterType.Categories.Directory, FilterType.SubCategories.Hidden));

               if (fiDir == null || fiDir.Count == 0)
               {
                  __FilterItems.Add(new FilterItem(new FilterType(FilterType.Categories.Directory, FilterType.SubCategories.Hidden),
                     string.Empty, FilterType.ValueOptions.None, false, true));
               }
               else if (!fiDir.First().Enabled)
               {
                  int index = __FilterItems.IndexOf(fiDir.First());
                  if (index > -1)
                  {
                     __FilterItems[index].Enabled = true;
                  }
               }
            }
            if (CommandLineArgs.SkipSystemFile)
            {
               // check if exists, enable if necessary, or create them
               var fiFile = GetFilterItemsByFilterType(new FilterType(FilterType.Categories.File, FilterType.SubCategories.System));

               if (fiFile == null || fiFile.Count == 0)
               {
                  __FilterItems.Add(new FilterItem(new FilterType(FilterType.Categories.File, FilterType.SubCategories.System),
                     string.Empty, FilterType.ValueOptions.None, false, true));
               }
               else if (!fiFile.First().Enabled)
               {
                  int index = __FilterItems.IndexOf(fiFile.First());
                  if (index > -1)
                  {
                     __FilterItems[index].Enabled = true;
                  }
               }
            }

            if (CommandLineArgs.SkipSystemDirectory)
            {
               // check if exists, enable if necessary, or create them
               var fiDir = GetFilterItemsByFilterType(new FilterType(FilterType.Categories.Directory, FilterType.SubCategories.System));

               if (fiDir == null || fiDir.Count == 0)
               {
                  __FilterItems.Add(new FilterItem(new FilterType(FilterType.Categories.Directory, FilterType.SubCategories.System),
                     string.Empty, FilterType.ValueOptions.None, false, true));
               }
               else if (!fiDir.First().Enabled)
               {
                  int index = __FilterItems.IndexOf(fiDir.First());
                  if (index > -1)
                  {
                     __FilterItems[index].Enabled = true;
                  }
               }
            }

            if (CommandLineArgs.DateModifiedFile != null)
            {
               // check if exists, enable if necessary/set value, or create it
               var fiFile = GetFilterItemsByFilterType(new FilterType(FilterType.Categories.File, FilterType.SubCategories.DateModified));

               bool foundExact = false;
               if (fiFile != null && fiFile.Count > 0)
               {
                  // more than one entry, so disable any that aren't less
                  foreach (var item in fiFile)
                  {
                     if (item.ValueOption == CommandLineArgs.DateModifiedFile.ValueOption && item.Value.Equals(CommandLineArgs.DateModifiedFile.Value.ToString()))
                     {
                        int index = __FilterItems.IndexOf(item);
                        if (index > -1)
                        {
                           __FilterItems[index].Enabled = true;
                           foundExact = true;
                        }
                     }
                     else
                     {
                        int index = __FilterItems.IndexOf(item);
                        if (index > -1)
                        {
                           __FilterItems[index].Enabled = false;
                        }
                     }
                  }
               }

               if (fiFile == null || fiFile.Count == 0 || !foundExact)
               {
                  __FilterItems.Add(new FilterItem(new FilterType(FilterType.Categories.File, FilterType.SubCategories.DateModified),
                     CommandLineArgs.DateModifiedFile.Value.ToString(), CommandLineArgs.DateModifiedFile.ValueOption, false, true));
               }
            }

            if (CommandLineArgs.DateModifiedDirectory != null)
            {
               // check if exists, enable if necessary/set value, or create it
               var fiDir = GetFilterItemsByFilterType(new FilterType(FilterType.Categories.Directory, FilterType.SubCategories.DateModified));

               bool foundExact = false;
               if (fiDir != null && fiDir.Count > 0)
               {
                  // more than one entry, so disable any that aren't less
                  foreach (var item in fiDir)
                  {
                     if (item.ValueOption == CommandLineArgs.DateModifiedDirectory.ValueOption && item.Value.Equals(CommandLineArgs.DateModifiedDirectory.Value.ToString()))
                     {
                        int index = __FilterItems.IndexOf(item);
                        if (index > -1)
                        {
                           __FilterItems[index].Enabled = true;
                           foundExact = true;
                        }
                     }
                     else
                     {
                        int index = __FilterItems.IndexOf(item);
                        if (index > -1)
                        {
                           __FilterItems[index].Enabled = false;
                        }
                     }
                  }
               }

               if (fiDir == null || fiDir.Count == 0 || !foundExact)
               {
                  __FilterItems.Add(new FilterItem(new FilterType(FilterType.Categories.Directory, FilterType.SubCategories.DateModified),
                     CommandLineArgs.DateModifiedDirectory.Value.ToString(), CommandLineArgs.DateModifiedDirectory.ValueOption, false, true));
               }
            }

            if (CommandLineArgs.DateCreatedFile != null)
            {
               // check if exists, enable if necessary/set value, or create it
               var fiFile = GetFilterItemsByFilterType(new FilterType(FilterType.Categories.File, FilterType.SubCategories.DateCreated));

               bool foundExact = false;
               if (fiFile != null && fiFile.Count > 0)
               {
                  // more than one entry, so disable any that aren't less
                  foreach (var item in fiFile)
                  {
                     if (item.ValueOption == CommandLineArgs.DateCreatedFile.ValueOption && item.Value.Equals(CommandLineArgs.DateCreatedFile.Value.ToString()))
                     {
                        int index = __FilterItems.IndexOf(item);
                        if (index > -1)
                        {
                           __FilterItems[index].Enabled = true;
                           foundExact = true;
                        }
                     }
                     else
                     {
                        int index = __FilterItems.IndexOf(item);
                        if (index > -1)
                        {
                           __FilterItems[index].Enabled = false;
                        }
                     }
                  }
               }

               if (fiFile == null || fiFile.Count == 0 || !foundExact)
               {
                  __FilterItems.Add(new FilterItem(new FilterType(FilterType.Categories.File, FilterType.SubCategories.DateCreated),
                     CommandLineArgs.DateCreatedFile.Value.ToString(), CommandLineArgs.DateCreatedFile.ValueOption, false, true));
               }
            }

            if (CommandLineArgs.DateCreatedDirectory != null)
            {
               // check if exists, enable if necessary/set value, or create it
               var fiDir = GetFilterItemsByFilterType(new FilterType(FilterType.Categories.Directory, FilterType.SubCategories.DateCreated));

               bool foundExact = false;
               if (fiDir != null && fiDir.Count > 0)
               {
                  // more than one entry, so disable any that aren't less
                  foreach (var item in fiDir)
                  {
                     if (item.ValueOption == CommandLineArgs.DateCreatedDirectory.ValueOption && item.Value.Equals(CommandLineArgs.DateCreatedDirectory.Value.ToString()))
                     {
                        int index = __FilterItems.IndexOf(item);
                        if (index > -1)
                        {
                           __FilterItems[index].Enabled = true;
                           foundExact = true;
                        }
                     }
                     else
                     {
                        int index = __FilterItems.IndexOf(item);
                        if (index > -1)
                        {
                           __FilterItems[index].Enabled = false;
                        }
                     }
                  }
               }

               if (fiDir == null || fiDir.Count == 0 || !foundExact)
               {
                  __FilterItems.Add(new FilterItem(new FilterType(FilterType.Categories.Directory, FilterType.SubCategories.DateCreated),
                     CommandLineArgs.DateCreatedDirectory.Value.ToString(), CommandLineArgs.DateCreatedDirectory.ValueOption, false, true));
               }
            }

            if (CommandLineArgs.MinFileSize != null)
            {
               // check if exists, enable if necessary/set value, or create it
               var fiFile = GetFilterItemsByFilterType(new FilterType(FilterType.Categories.File, FilterType.SubCategories.Size));

               bool foundExact = false;
               if (fiFile != null && fiFile.Count > 0)
               {
                  // more than one entry, so disable any that aren't less
                  foreach (var item in fiFile)
                  {
                     if (item.ValueOption == CommandLineArgs.MinFileSize.ValueOption && item.Value.Equals(CommandLineArgs.MinFileSize.Value.ToString()))
                     {
                        int index = __FilterItems.IndexOf(item);
                        if (index > -1)
                        {
                           __FilterItems[index].Enabled = true;
                           foundExact = true;
                        }
                     }
                     else
                     {
                        int index = __FilterItems.IndexOf(item);
                        if (index > -1)
                        {
                           __FilterItems[index].Enabled = false;
                        }
                     }
                  }
               }

               if (fiFile == null || fiFile.Count == 0 || !foundExact)
               {
                  __FilterItems.Add(new FilterItem(new FilterType(FilterType.Categories.File, FilterType.SubCategories.Size),
                     CommandLineArgs.MinFileSize.Value.ToString(), CommandLineArgs.MinFileSize.ValueOption, false, "byte", true));
               }
            }

            if (CommandLineArgs.MaxFileSize != null)
            {
               // check if exists, enable if necessary/set value, or create it
               var fiFile = GetFilterItemsByFilterType(new FilterType(FilterType.Categories.File, FilterType.SubCategories.Size));

               bool foundExact = false;
               if (fiFile != null && fiFile.Count > 0)
               {
                  // more than one entry, so disable any that aren't less
                  foreach (var item in fiFile)
                  {
                     if (item.ValueOption == CommandLineArgs.MaxFileSize.ValueOption && item.Value.Equals(CommandLineArgs.MaxFileSize.Value.ToString()))
                     {
                        int index = __FilterItems.IndexOf(item);
                        if (index > -1)
                        {
                           __FilterItems[index].Enabled = true;
                           foundExact = true;
                        }
                     }
                     else
                     {
                        int index = __FilterItems.IndexOf(item);
                        if (index > -1)
                        {
                           __FilterItems[index].Enabled = false;
                        }
                     }
                  }
               }

               if (fiFile == null || fiFile.Count == 0 || !foundExact)
               {
                  __FilterItems.Add(new FilterItem(new FilterType(FilterType.Categories.File, FilterType.SubCategories.Size),
                     CommandLineArgs.MaxFileSize.Value.ToString(), CommandLineArgs.MaxFileSize.ValueOption, false, "byte", true));
               }
            }

            if (CommandLineArgs.MinFileCount > 0)
            {
               // check if exists, enable if necessary/set value, or create it
               var fiFile = GetFilterItemsByFilterType(new FilterType(FilterType.Categories.File, FilterType.SubCategories.MinimumHitCount));

               if (fiFile == null || fiFile.Count == 0)
               {
                  __FilterItems.Add(new FilterItem(new FilterType(FilterType.Categories.File, FilterType.SubCategories.MinimumHitCount),
                     CommandLineArgs.MinFileCount.ToString(), FilterType.ValueOptions.None, false, true));
               }
               else
               {
                  int index = __FilterItems.IndexOf(fiFile.First());
                  if (index > -1)
                  {
                     __FilterItems[index].Enabled = true;
                     __FilterItems[index].Value = CommandLineArgs.MinFileCount.ToString();
                  }
               }
            }

            if (CommandLineArgs.ReadOnlyFile)
            {
               // check if exists, enable if necessary/set value, or create it
               var fiFile = GetFilterItemsByFilterType(new FilterType(FilterType.Categories.File, FilterType.SubCategories.ReadOnly));

               if (fiFile == null || fiFile.Count == 0)
               {
                  __FilterItems.Add(new FilterItem(new FilterType(FilterType.Categories.File, FilterType.SubCategories.ReadOnly),
                     string.Empty, FilterType.ValueOptions.None, false, true));
               }
               else
               {
                  int index = __FilterItems.IndexOf(fiFile.First());
                  if (index > -1)
                  {
                     __FilterItems[index].Enabled = true;
                  }
               }
            }

            // keep last to ensure all options are set before a search begins
            if (CommandLineArgs.StartSearch)
            {
               btnSearch_Click(null, null);
               this.Show();
               this.Refresh();
            }
         }
      }

      private List<FilterItem> GetFilterItemsByFilterType(FilterType ft)
      {
         return __FilterItems.FindAll(f => f.FilterType == ft);
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
      /// Create a new search window.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      05/06/2014  Created
      /// </history>
      private void mnuNewWindow_Click(object sender, EventArgs e)
      {
         System.Diagnostics.Process.Start(Constants.ProductFullName);
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
      /// [Andrew_Radford]    20/09/2009	CHG: Use export class
      /// [Curtis_Beard]	   01/31/2012	CHG: show status bar message only once for text/html/xml
      /// </history>
      private void mnuSaveResults_Click(object sender, System.EventArgs e)
      {
         // only show dialog if information to save
         if (lstFileNames.Items.Count <= 0)
         {
            MessageBox.Show(Language.GetGenericText("SaveNoResults"), Constants.ProductName, MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
            return;
         }

         var dlg = new SaveFileDialog
                      {
                         CheckPathExists = true,
                         AddExtension = true,
                         Title = Language.GetGenericText("SaveDialogTitle"),
                         Filter = "Text (*.txt)|*.txt|HTML (*.html)|*.html|XML (*.xml)|*.xml|JSON (*.json)|*.json"
                      };


         if (dlg.ShowDialog(this) == DialogResult.OK)
         {
            switch (dlg.FilterIndex)
            {
               case 1:
                  // Save to text
                  OutputResults(dlg.FileName, HitListExport.SaveResultsAsText);
                  break;
               case 2:
                  // Save to html
                  OutputResults(dlg.FileName, HitListExport.SaveResultsAsHTML);
                  break;
               case 3:
                  // Save to xml
                  OutputResults(dlg.FileName, HitListExport.SaveResultsAsXML);
                  break;
               case 4:
                  // Save to json
                  OutputResults(dlg.FileName, HitListExport.SaveResultsAsJSON);
                  break;
            }
         }
      }

      /// <summary>
      /// Output results using given export delegate.
      /// </summary>
      /// <param name="filename">Current filename</param>
      /// <param name="outputter">Export delegate</param>
      /// <history>
      /// [Andrew_Radford]   20/09/2009	Initial
      /// [Curtis_Beard]     12/03/2014	CHG: pass grep indexes instead of ListView
      /// </history>
      private void OutputResults(string filename, HitListExport.ExportDelegate outputter)
      {
         SetStatusBarMessage(String.Format(Language.GetGenericText("SaveSaving"), filename));

         try
         {
            // get all grep indexes
            IEnumerable<ListViewItem> lv = lstFileNames.Items.Cast<ListViewItem>();
            var indexes = (from i in lv select int.Parse(i.SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text)).ToList();

            outputter(filename, __Grep, indexes);
            SetStatusBarMessage(Language.GetGenericText("SaveSaved"));
         }
         catch (Exception ex)
         {
            MessageBox.Show(
                String.Format(Language.GetGenericText("SaveError"), ex),
                Constants.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
         }
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
      /// [Curtis_Beard]	   09/28/2012	CHG: use common select method
      /// </history>
      private void mnuSelectAll_Click(object sender, System.EventArgs e)
      {
         SelectAllListItems();
      }

      /// <summary>
      /// Menu View Event
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   11/11/2014	Initial
      /// </history>
      private void mnuView_Select(object sender, EventArgs e)
      {
         ViewStatus.Enabled = GetLogItemsCountByType(LogItem.LogItemTypes.Status) > 0;
         ViewExclusions.Enabled = GetLogItemsCountByType(LogItem.LogItemTypes.Exclusion) > 0;
         ViewError.Enabled = GetLogItemsCountByType(LogItem.LogItemTypes.Error) > 0;
         ViewAll.Enabled = AnyLogItems();
      }

      /// <summary>
      /// View status messages.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   11/11/2014	Initial
      /// </history>
      private void ViewStatus_Click(object sender, System.EventArgs e)
      {
         DisplaySearchMessages(LogItem.LogItemTypes.Status);
      }

      /// <summary>
      /// View exclusion messages.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   11/11/2014	Initial
      /// </history>
      private void ViewExclusions_Click(object sender, System.EventArgs e)
      {
         DisplaySearchMessages(LogItem.LogItemTypes.Exclusion);
      }

      /// <summary>
      /// View error messages.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   11/11/2014	Initial
      /// </history>
      private void ViewError_Click(object sender, System.EventArgs e)
      {
         DisplaySearchMessages(LogItem.LogItemTypes.Error);
      }

      /// <summary>
      /// View all messages.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   11/11/2014	Initial
      /// </history>
      private void ViewAll_Click(object sender, System.EventArgs e)
      {
         DisplaySearchMessages(null);
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

            // open the default editor at first hit
            int index = hit.RetrieveFirstHitIndex();
            TextEditors.EditFile(path, hit.RetrieveLineNumber(index), hit.RetrieveColumn(index), hit.RetrieveLine(index));
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
      /// Clear Search Paths MRU event.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   11/07/2014	ADD: clear each MRU list individually
      /// </history>
      private void ToolsMRUSearchPaths_Click(object sender, EventArgs e)
      {
         cboFilePath.Items.Clear();

         AstroGrep.Core.GeneralSettings.SearchStarts = string.Empty;
         AstroGrep.Core.GeneralSettings.Save();
      }

      /// <summary>
      /// Clear File Types MRU event.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   11/07/2014	ADD: clear each MRU list individually
      /// </history>
      private void ToolsMRUFilterTypes_Click(object sender, EventArgs e)
      {
         cboFileName.Items.Clear();

         AstroGrep.Core.GeneralSettings.SearchFilters = string.Empty;
         AstroGrep.Core.GeneralSettings.Save();
      }

      /// <summary>
      /// Clear Search Text MRU event.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   11/07/2014	ADD: clear each MRU list individually
      /// </history>
      private void ToolsMRUSearchText_Click(object sender, EventArgs e)
      {
         cboSearchForText.Items.Clear();

         AstroGrep.Core.GeneralSettings.SearchTexts = string.Empty;
         AstroGrep.Core.GeneralSettings.Save();
      }

      /// <summary>
      /// Clear All MRUs event.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   11/07/2014	ADD: clear each MRU list individually
      /// </history>
      private void ToolsMRUAll_Click(object sender, EventArgs e)
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
      /// [Curtis_Beard]	   02/24/2012	CHG: 3488321, ability to change results font
      /// [Curtis_Beard]	   09/26/2012	CHG: Update status bar text
      /// [Curtis_Beard]	   10/10/2012	ADD: 3479503, ability to change file list font
      /// </history>
      private void mnuOptions_Click(object sender, System.EventArgs e)
      {
         frmOptions _form = new frmOptions();

         if (_form.ShowDialog(this) == DialogResult.OK)
         {
            //update combobox lengths
            while (cboFilePath.Items.Count > Core.GeneralSettings.MaximumMRUPaths)
               cboFilePath.Items.RemoveAt(cboFilePath.Items.Count - 1);
            while (cboFileName.Items.Count > Core.GeneralSettings.MaximumMRUPaths)
               cboFileName.Items.RemoveAt(cboFileName.Items.Count - 1);
            while (cboSearchForText.Items.Count > Core.GeneralSettings.MaximumMRUPaths)
               cboSearchForText.Items.RemoveAt(cboSearchForText.Items.Count - 1);

            // load new language if necessary
            if (_form.IsLanguageChange)
            {
               Language.ProcessForm(this, this.toolTip1);

               SetColumnsText();

               // reload label
               __SearchOptionsText = lnkSearchOptions.Text;
               if (!__OptionsShow)
                  lnkSearchOptions.Text = String.Format(__SearchOptionsText, ">>");
               else
                  lnkSearchOptions.Text = String.Format(__SearchOptionsText, "<<");

               // clear statusbar text
               SetStatusBarMessage(string.Empty);
               CalculateTotalCount();
            }

            // change results display and rehighlight
            txtHits.ForeColor = Convertors.ConvertStringToColor(Core.GeneralSettings.ResultsForeColor);
            txtHits.BackColor = Convertors.ConvertStringToColor(Core.GeneralSettings.ResultsBackColor);
            txtHits.Font = Convertors.ConvertStringToFont(Core.GeneralSettings.ResultsFont);
            txtHits.WordWrap = Core.GeneralSettings.ResultsWordWrap;

            lstFileNames.Font = Convertors.ConvertStringToFont(Core.GeneralSettings.FilePanelFont);
            lstFileNames_SelectedIndexChanged(null, null);
         }
         _form = null;
      }

      /// <summary>
      /// Shows help file (.chm) to user.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		05/06/2014	Initial
      /// </history>
      private void mnuHelpContents_Click(object sender, EventArgs e)
      {
         //TODO: support currently selected language help file (AstroGrep-Help-en-us.chm, AstroGrep-Help-da-dk.chm, etc.)
         //Help.ShowHelp(this, Path.Combine(Constants.ProductLocation, "AstroGrep-Help.chm"));
         System.Diagnostics.Process.Start("http://astrogrep.sourceforge.net/help/");
      }

      /// <summary>
      /// Shows regular expression help to user.
      /// </summary>
      /// /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		05/06/2014	Initial
      /// </history>
      private void mnuHelpRegEx_Click(object sender, EventArgs e)
      {
         System.Diagnostics.Process.Start("http://msdn.microsoft.com/en-us/library/az24scfc.aspx");
      }

      /// <summary>
      /// Displays update check window to user.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		05/06/2014	Initial
      /// </history>
      private void mnuCheckForUpdates_Click(object sender, EventArgs e)
      {
         var dlg = new frmCheckForUpdateTemp();
         dlg.ShowDialog(this);
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
      private void mnuAbout_Click(object sender, EventArgs e)
      {
         frmAbout _form = new frmAbout();

         _form.ShowDialog(this);
         _form = null;
      }

      /// <summary>
      /// Sends all selected items from the file list to the clipboard
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Ed_Jakbuowski]       05/20/2009  Created
      /// [Curtis_Beard]        01/31/2012  FIX: 3482207, show all columns when copying data
      /// </history>
      private void CopyMenuItem_Click(object sender, System.EventArgs e)
      {
         if (lstFileNames.SelectedItems.Count <= 0)
            return;

         System.Text.StringBuilder data = new System.Text.StringBuilder();
         try
         {
            foreach (ListViewItem lvi in lstFileNames.SelectedItems)
            {
               data.Append(lvi.Text);

               // skip first and last columns (filename, sort order)
               for (int i = 0; i < lvi.SubItems.Count; i++)
               {
                  if (i != 0 && i != lvi.SubItems.Count - 1)
                  {
                     var subLvi = lvi.SubItems[i];
                     data.Append(", ");
                     data.Append(subLvi.Text);
                  }
               }
               data.Append(Environment.NewLine);
            }
            Clipboard.SetDataObject(data.ToString());
         }
         catch (Exception ex)
         {
            MessageBox.Show("Exception: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      /// <summary>
      /// Sends all selected items from the file list to the clipboard
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]        01/31/2012  ADD: 2078252, add right click options for copying name,located in,located in + name
      /// </history>
      private void CopyNameMenuItem_Click(object sender, System.EventArgs e)
      {
         if (lstFileNames.SelectedItems.Count <= 0)
            return;

         System.Text.StringBuilder data = new System.Text.StringBuilder();
         try
         {
            foreach (ListViewItem lvi in lstFileNames.SelectedItems)
            {
               data.Append(lvi.Text);
               data.Append(Environment.NewLine);
            }
            Clipboard.SetDataObject(data.ToString());
         }
         catch (Exception ex)
         {
            MessageBox.Show("Exception: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      /// <summary>
      /// Sends all selected items from the file list to the clipboard
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]        01/31/2012  ADD: 2078252, add right click options for copying name,located in,located in + name
      /// </history>
      private void CopyLocatedInMenuItem_Click(object sender, System.EventArgs e)
      {
         if (lstFileNames.SelectedItems.Count <= 0)
            return;

         System.Text.StringBuilder data = new System.Text.StringBuilder();
         try
         {
            foreach (ListViewItem lvi in lstFileNames.SelectedItems)
            {
               data.Append(lvi.SubItems[1].Text);
               data.Append(Environment.NewLine);
            }
            Clipboard.SetDataObject(data.ToString());
         }
         catch (Exception ex)
         {
            MessageBox.Show("Exception: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      /// <summary>
      /// Sends all selected items from the file list to the clipboard
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]        01/31/2012  ADD: 2078252, add right click options for copying name,located in,located in + name
      /// </history>
      private void CopyLocatedInAndNameMenuItem_Click(object sender, System.EventArgs e)
      {
         if (lstFileNames.SelectedItems.Count <= 0)
            return;

         System.Text.StringBuilder data = new System.Text.StringBuilder();
         try
         {
            foreach (ListViewItem lvi in lstFileNames.SelectedItems)
            {
               data.AppendFormat("{0}{1}{2}", lvi.SubItems[1].Text, Path.DirectorySeparatorChar.ToString(), lvi.Text);
               data.Append(Environment.NewLine);
            }
            Clipboard.SetDataObject(data.ToString());
         }
         catch (Exception ex)
         {
            MessageBox.Show("Exception: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      /// <summary>
      /// Need certain keyboard events on the lstFileNames.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Ed_Jakbuowski]     05/20/2009  Created
      /// [Curtis_Beard]	   09/28/2012	CHG: use common select method
      /// [Curtis_Beard]	   03/21/2014	CHG: 73, edit selected files on enter key
      /// </history>
      private void lstFileNames_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         //ctrl+c  Copy to clipboard
         if (e.KeyCode == Keys.C && e.Control)
         {
            CopyMenuItem_Click(sender, EventArgs.Empty);
         }

         //ctrl+a  Select All
         if (e.KeyCode == Keys.A && e.Control)
         {
            SelectAllListItems();
         }

         //enter Edit selected file
         if (e.KeyCode == Keys.Enter)
         {
            mnuOpenSelected_Click(null, null);
         }

         // ** I think the delete key is a bad idea, because its too easy to modify the results by mistake.
         //delete Delete selected from list
         //if (e.KeyCode == Keys.Delete)
         //{
         //   DeleteMenuItem_Click(sender, EventArgs.Empty);
         //}
      }

      /// <summary>
      /// Context Menu item for opening selected files
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
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
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// /// <history>
      /// [Ed_Jakbuowski]     05/20/2009  Created
      /// [Curtis_Beard]      02/12/2014  CHG: 77, select file in explorer window
      /// </history>
      private void OpenFolderMenuItem_Click(object sender, System.EventArgs e)
      {
         try
         {
            foreach (ListViewItem lvi in lstFileNames.SelectedItems)
            {
               string filename = Path.Combine(lvi.SubItems[Constants.COLUMN_INDEX_DIRECTORY].Text, lvi.SubItems[Constants.COLUMN_INDEX_FILE].Text);
               if (File.Exists(filename))
               {
                  // explorer argument format: "/select, " + filename;
                  string fileSelect = string.Format("/select, \"{0}\"", filename);
                  System.Diagnostics.Process.Start("Explorer.exe", fileSelect);
               }
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show("Exception: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      /// <summary>
      /// Context Menu item for opening selected file with associated application.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      02/12/2014  ADD: 67, open selected file(s) with associated application
      /// </history>
      private void OpenWithAssociatedApp_Click(object sender, System.EventArgs e)
      {
         try
         {
            string path;
            HitObject hit;

            for (int i = 0; i < lstFileNames.SelectedItems.Count; i++)
            {
               // retrieve hit object
               hit = __Grep.RetrieveHitObject(int.Parse(lstFileNames.SelectedItems[i].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text));

               // retrieve the filename
               path = hit.FilePath;

               TextEditors.OpenFileWithDefaultApp(path);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show("Exception: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      /// <summary>
      /// Context Menu Item for deleting items from the list.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// /// <history>
      /// [Ed_Jakbuowski]     05/20/2009  Created
      /// [Curtis_Beard]      09/17/2013  FIX: 40, update counts after a removal from the list
      /// [Curtis_Beard]      12/02/2014  ADD: begin/end update calls when changing file listview
      /// </history>
      private void DeleteMenuItem_Click(object sender, System.EventArgs e)
      {
         if (lstFileNames.SelectedItems.Count <= 0)
            return;

         try
         {
            lstFileNames.BeginUpdate();
            while (lstFileNames.SelectedItems.Count > 0)
            {
               lstFileNames.SelectedItems[0].Remove();
            }
            lstFileNames.EndUpdate();

            // update counts
            CalculateTotalCount();
         }
         catch (Exception ex)
         {
            MessageBox.Show("Exception: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      /// <summary>
      /// Handles deleting the selected files by using the recycle bin.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      09/18/2013  ADD: 65, file operations context menu
      /// [Curtis_Beard]      12/02/2014  ADD: begin/end update calls when changing file listview
      /// </history>
      private void FileDeleteMenuItem_Click(object sender, EventArgs e)
      {
         Dictionary<string, string> files = new Dictionary<string, string>();

         for (int i = 0; i < lstFileNames.SelectedItems.Count; i++)
         {
            // retrieve hit object
            var hit = __Grep.RetrieveHitObject(int.Parse(lstFileNames.SelectedItems[i].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text));
            var index = lstFileNames.SelectedItems[i].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text;

            files.Add(index, hit.FilePath);
         }

         lstFileNames.BeginUpdate();
         foreach (var file in files)
         {
            // recycle bin delete
            API.FileDeletion.Delete(file.Value);

            // remove from list
            ListViewItem[] items = lstFileNames.Items.Find(file.Key, false);
            if (items != null && items.Length == 1)
            {
               items[0].Remove();
            }
         }
         lstFileNames.EndUpdate();

         // clear results area since all selected files are removed
         if (files.Count > 0)
         {
            txtHits.Clear();
            CalculateTotalCount();
         }
      }

      /// <summary>
      /// Handles copying the selected files to the clipboard.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      09/18/2013  ADD: 65, file operations context menu
      /// </history>
      private void FileCopyMenuItem_Click(object sender, EventArgs e)
      {
         System.Collections.Specialized.StringCollection files = new System.Collections.Specialized.StringCollection();

         for (int i = 0; i < lstFileNames.SelectedItems.Count; i++)
         {
            // retrieve hit object
            var hit = __Grep.RetrieveHitObject(int.Parse(lstFileNames.SelectedItems[i].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text));

            // retrieve the filename
            files.Add(hit.FilePath);
         }

         if (files.Count > 0)
         {
            Clipboard.SetFileDropList(files);
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
         const string languageLookupText = "SearchSearching";
         LogItems.Add(new LogItem(LogItem.LogItemTypes.Status, languageLookupText, file.FullName));
         
         SetStatusBarMessage(string.Format(Language.GetGenericText(languageLookupText), TruncateFileName(file, stbStatus)));
      }

      /// <summary>
      /// Handles the Grep object's SearchingFileByPlugin event
      /// </summary>
      /// <param name="pluginName">Name of plugin currently searching file</param>
      /// <history>
      /// [Curtis_Beard]		10/16/2012	Created
      /// </history>
      private void ReceiveSearchingFileByPlugin(string pluginName)
      {
         const string languageLookupText = "SearchSearchingByPlugin";
         LogItems.Add(new LogItem(LogItem.LogItemTypes.Status, languageLookupText, pluginName));

         SetStatusBarMessage(string.Format(Language.GetGenericText(languageLookupText), pluginName));
      }

      /// <summary>
      /// Handles the Grep object's FileEncodingDetected event
      /// </summary>
      /// <param name="file">FileInfo object containg currently being search file</param>
      /// <param name="encoding">File's detected System.Text.Encoding</param>
      /// <param name="usedEncoder">The used encoder's name</param>
      /// <history>
      /// [Curtis_Beard]		12/01/2014	Created
      /// </history>
      private void ReceiveFileEncodingDetected(FileInfo file, System.Text.Encoding encoding, string usedEncoder)
      {
         const string languageLookupText = "SearchSearching";
         
         // find LogItem by SearchSearching and file.FullName
         // update details to include encoding
         foreach (LogItem item in LogItems)
         {
            if (item.Value == languageLookupText &&
               item.Details == file.FullName)
            {
               item.Details = string.Format("{0}||{1} [{2}]", file.FullName, encoding.EncodingName, usedEncoder);
            }
         }
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
         CalculateTotalCount();
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
         CalculateTotalCount();
      }

      /// <summary>
      /// Receives the search error event when a file search causes an uncatchable error
      /// </summary>
      /// <param name="file">FileInfo object of error file</param>
      /// <param name="ex">Exception</param>
      /// <history>
      /// [Curtis_Beard]		03/14/2006	Created
      /// [Curtis_Beard]		05/28/2007  CHG: use Exception and display error
      /// [Curtis_Beard]		08/07/2007  ADD: 1741735, better search error handling
      /// [Curtis_Beard]		02/07/2012  CHG: 1741735, report full error message
      /// </history>
      private void ReceiveSearchError(System.IO.FileInfo file, Exception ex)
      {
         string languageLookupText = "SearchGenericError";

         if (file == null)
         {
            LogItems.Add(new LogItem(LogItem.LogItemTypes.Error, languageLookupText, string.Format("{0}||{1}", string.Empty, ex.Message)));
         }
         else
         {
            languageLookupText = "SearchFileError";
            LogItems.Add(new LogItem(LogItem.LogItemTypes.Error, languageLookupText, string.Format("{0}||{1}", file.FullName, ex.Message)));
         }

         SetStatusBarErrorCount(GetLogItemsCountByType(LogItem.LogItemTypes.Error));
      }

      /// <summary>
      /// Receives the search cancel event when the grep has been cancelled.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/12/2006	Created
      /// [Curtis_Beard]		08/07/2007  ADD: 1741735, display any search errors
      /// [Curtis_Beard]	   12/17/2014	ADD: support for Win7+ taskbar progress
      /// </history>
      private void ReceiveSearchCancel()
      {
         RestoreTaskBarProgress();
         isSearching = false;

         const string languageLookupText = "SearchCancelled";
         LogItems.Add(new LogItem(LogItem.LogItemTypes.Status, languageLookupText));

         SetStatusBarMessage(Language.GetGenericText(languageLookupText));
         SetSearchState(true);
         CalculateTotalCount();

         ShowExclusionsErrorMessageBox();
      }

      /// <summary>
      /// Receives the search complete event when the grep has completed.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/12/2006	Created
      /// [Curtis_Beard]		06/27/2007  CHG: removed message parameter
      /// [Curtis_Beard]		08/07/2007  ADD: 1741735, display any search errors
      /// [Ed_Jakubowski]		05/20/2009  ADD: Display the Count
      /// [Curtis_Beard]		01/30/2012  CHG: use language class for count text
      /// [Curtis_Beard]		04/08/2014	CHG: 74, command line exit, save options
      /// [Curtis_Beard]	   12/17/2014	ADD: support for Win7+ taskbar progress
      /// </history>
      private void ReceiveSearchComplete()
      {
         RestoreTaskBarProgress();
         isSearching = false;

         const string languageLookupText = "SearchFinished";
         LogItems.Add(new LogItem(LogItem.LogItemTypes.Status, languageLookupText));

         SetStatusBarMessage(Language.GetGenericText(languageLookupText));
         CalculateTotalCount();
         SetSearchState(true);

         ShowExclusionsErrorMessageBox();

         if (CommandLineArgs.AnyArguments)
         {
            if (!string.IsNullOrEmpty(CommandLineArgs.OutputPath))
            {

               HitListExport.ExportDelegate del = HitListExport.SaveResultsAsText;
               switch (CommandLineArgs.OutputType)
               {
                  case "xml":
                     del = HitListExport.SaveResultsAsXML;
                     break;

                  case "json":
                     del = HitListExport.SaveResultsAsJSON;
                     break;

                  case "html":
                     del = HitListExport.SaveResultsAsHTML;
                     break;

                  default:
                  case "txt":
                     del = HitListExport.SaveResultsAsText;
                     break;
               }

               OutputResults(CommandLineArgs.OutputPath, del);
            }

            if (CommandLineArgs.ExitAfterSearch)
            {
               this.Close();
            }
         }
      }

      /// <summary>
      /// Handles the Grep object's FileFiltered event
      /// </summary>
      /// <param name="file">FileInfo object containg currently being search file</param>
      /// <param name="filterItem">FilterItem causing the filtering</param>
      /// <param name="filterValue">Value that caused the filtering</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// [Curtis_Beard]	   11/11/2014	CHG: more descriptive exclusion information
      /// </history>
      private void ReceiveFileFiltered(System.IO.FileInfo file, FilterItem filterItem, string filterValue)
      {
         LogItems.Add(new LogItem(LogItem.LogItemTypes.Exclusion, file.FullName, string.Format("{0}~~{1}", filterItem.ToString(), filterValue)));
         SetStatusBarFilterCount(GetLogItemsCountByType(LogItem.LogItemTypes.Exclusion));
      }

      /// <summary>
      /// Handles the Grep object's DirectoryFiltered event
      /// </summary>
      /// <param name="dir">DirectoryInfo object containg currently being searched directory</param>
      /// <param name="filterItem">FilterItem causing the filtering</param>
      /// <param name="filterValue">Value that caused the filtering</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// [Curtis_Beard]	   11/11/2014	CHG: more descriptive exclusion information
      /// </history>
      private void ReceiveDirectoryFiltered(System.IO.DirectoryInfo dir, FilterItem filterItem, string filterValue)
      {
         LogItems.Add(new LogItem(LogItem.LogItemTypes.Exclusion, dir.FullName, string.Format("{0}~~{1}", filterItem.ToString(), filterValue)));
         SetStatusBarFilterCount(GetLogItemsCountByType(LogItem.LogItemTypes.Exclusion));
      }

      /// <summary>
      /// Used to display search messages (like exclusions, errors) to the user.
      /// </summary>
      /// <param name="displayType">The type of message to display</param>
      /// <history>
      /// [Curtis_Beard]	   09/27/2012	ADD: 1741735, better error handling display
      /// [Curtis_Beard]	   12/06/2012	CHG: 1741735, rework to use common LogItems
      /// [Curtis_Beard]	   11/11/2014	CHG: use new log items viewer form
      /// </history>
      private void DisplaySearchMessages(LogItem.LogItemTypes? displayType)
      {
         if (this.InvokeRequired)
         {
            DisplaySearchMessagesCallBack del = new DisplaySearchMessagesCallBack(DisplaySearchMessages);
            this.Invoke(del, new object[1] { displayType });
            return;
         }

         if ((!displayType.HasValue && AnyLogItems()) || (displayType.HasValue && GetLogItemsCountByType(displayType.Value) > 0))
         {
            using (var frm = new frmLogDisplay())
            {
               frm.LogItems = LogItems;

               frm.DefaultFilterType = displayType;
               frm.StartPosition = FormStartPosition.Manual;
               frm.Location = new Point(this.Left + 20, this.Bottom - frm.Height - stbStatus.Height - 20);
               frm.Size = new Size(this.Width - 40, frm.Height);
               frm.ShowDialog(this);
            }
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
            lstFileNames.Invoke(_delegate, new object[1] { hit });
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
            UpdateStatusMessageCallBack _delegate = SetStatusBarMessage;
            stbStatus.Invoke(_delegate, new object[1] { message });
            return;
         }

         sbStatusPanel.Text = message;
      }

      /// <summary>
      /// Set the status bar text for the encoding name. (Thread Safe)
      /// </summary>
      /// <param name="encodingName">Encoding display name</param>
      /// <history>
      /// [Curtis_Beard]	   03/03/2015	Created
      /// </history>
      private void SetStatusBarEncoding(string encodingName)
      {
         if (stbStatus.InvokeRequired)
         {
            UpdateStatusMessageCallBack _delegate = new UpdateStatusMessageCallBack(SetStatusBarEncoding);
            stbStatus.Invoke(_delegate, new object[1] { encodingName });
            return;
         }

         sbEncodingPanel.Text = encodingName;

         // setup borders depending on value
         if (string.IsNullOrEmpty(encodingName))
         {
            sbEncodingPanel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
         }
         else
         {
            sbEncodingPanel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
         }
      }

      /// <summary>
      /// Set the status bar text for the total count. (Thread Safe)
      /// </summary>
      /// <param name="count">Total number of hits</param>
      /// <history>
      /// [Curtis_Beard]	   01/27/2007	Created
      /// </history>
      private void SetStatusBarTotalCount(int count)
      {
         if (stbStatus.InvokeRequired)
         {
            UpdateStatusCountCallBack _delegate = new UpdateStatusCountCallBack(SetStatusBarTotalCount);
            stbStatus.Invoke(_delegate, new object[1] { count });
            return;
         }

         sbTotalCountPanel.Text = string.Format(Language.GetGenericText("ResultsStatusTotalCount"), count);
      }

      /// <summary>
      /// Set the status bar text for the file count. (Thread Safe)
      /// </summary>
      /// <param name="count">Total number of files</param>
      /// <history>
      /// [Curtis_Beard]	   07/02/2007	Created
      /// </history>
      private void SetStatusBarFileCount(int count)
      {
         if (stbStatus.InvokeRequired)
         {
            UpdateStatusCountCallBack _delegate = new UpdateStatusCountCallBack(SetStatusBarFileCount);
            stbStatus.Invoke(_delegate, new object[1] { count });
            return;
         }

         sbFileCountPanel.Text = string.Format(Language.GetGenericText("ResultsStatusFileCount"), count);
      }

      /// <summary>
      /// Set the status bar text for the filter count. (Thread Safe)
      /// </summary>
      /// <param name="count">Total number of filtered items</param>
      /// <history>
      /// [Curtis_Beard]		09/26/2012	Created
      /// [Curtis_Beard]		10/22/2012	CHG: use yellow background color to alert user
      /// </history>
      private void SetStatusBarFilterCount(int count)
      {
         if (stbStatus.InvokeRequired)
         {
            UpdateStatusCountCallBack _delegate = new UpdateStatusCountCallBack(SetStatusBarFilterCount);
            stbStatus.Invoke(_delegate, new object[1] { count });
            return;
         }

         sbFilterCountPanel.Text = string.Format(Language.GetGenericText("ResultsStatusFilterCount"), count);
         sbFilterCountPanel.BackColor = count > 0 ? Color.Yellow : SystemColors.Control;
      }

      /// <summary>
      /// Set the status bar text for the error count. (Thread Safe)
      /// </summary>
      /// <param name="count">Total number of errors</param>
      /// <history>
      /// [Curtis_Beard]		07/02/2007	Created
      /// [Curtis_Beard]		10/22/2012	CHG: use red background color to alert user
      /// </history>
      private void SetStatusBarErrorCount(int count)
      {
         if (stbStatus.InvokeRequired)
         {
            UpdateStatusCountCallBack _delegate = new UpdateStatusCountCallBack(SetStatusBarErrorCount);
            stbStatus.Invoke(_delegate, new object[1] { count });
            return;
         }

         sbErrorCountPanel.Text = string.Format(Language.GetGenericText("ResultsStatusErrorCount"), count);
         sbErrorCountPanel.BackColor = count > 0 ? Color.Red : SystemColors.Control;
      }

      /// <summary>
      /// Calculate and set the total number of hits for the current search. (Thread Safe)
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		01/27/2007	Created
      /// [Curtis_Beard]		07/02/2007	ADD: update file/error total counts
      /// </history>
      private void CalculateTotalCount()
      {
         if (lstFileNames.InvokeRequired)
         {
            CalculateTotalCountCallBack _delegate = new CalculateTotalCountCallBack(CalculateTotalCount);
            lstFileNames.Invoke(_delegate);
            return;
         }

         //update total hit count
         int total = 0;
         int single = 0;

         foreach (ListViewItem item in lstFileNames.Items)
         {
            single = int.Parse(item.SubItems[Constants.COLUMN_INDEX_COUNT].Text);
            total += single;
         }

         SetStatusBarTotalCount(total);
         SetStatusBarFileCount(lstFileNames.Items.Count);
         SetStatusBarFilterCount(GetLogItemsCountByType(LogItem.LogItemTypes.Exclusion));
         SetStatusBarErrorCount(GetLogItemsCountByType(LogItem.LogItemTypes.Error));
      }


      /// <summary>
      /// Start the searching
      /// </summary>
      /// <param name="searchWithInResults">true for searching within current results, false starts a new search</param>
      /// <history>
      /// [Curtis_Beard]		10/17/2005	Created
      /// [Curtis_Beard]		07/03/2006	FIX: 1516775, Remove trim on the search expression
      /// [Curtis_Beard]		07/12/2006	CHG: moved thread actions to grep class
      /// [Curtis_Beard]		11/22/2006	CHG: Remove use of browse in combobox
      /// [Curtis_Beard]		08/07/2007  ADD: 1741735, better search error handling
      /// [Curtis_Beard]		08/21/2007  FIX: 1778467, make sure file pattern is correct if a '\' is present
      /// [Curtis_Beard]	   01/31/2012	CHG: 3424154/1816655, allow multiple starting directories
      /// [Curtis_Beard]		02/07/2012  CHG: 1741735, report full error message
      /// [Curtis_Beard]	   02/24/2012	CHG: 3488322, use hand cursor for results view to signal click
      /// [Curtis_Beard]	   10/30/2012	ADD: 28, search within results
      /// [Curtis_Beard]	   12/01/2014	ADD: support for encoding detection event
      /// [Curtis_Beard]	   12/17/2014	ADD: support for Win7+ taskbar progress
      /// </history>
      private void StartSearch(bool searchWithInResults)
      {
         try
         {
            string path = cboFilePath.Text.Trim();

            string[] filePaths = null;
            if (searchWithInResults)
            {
               // get currently listed file paths from ListView
               filePaths = new string[lstFileNames.Items.Count];
               for (int i = 0; i < lstFileNames.Items.Count; i++)
               {
                  filePaths[i] = Path.Combine(lstFileNames.Items[i].SubItems[Constants.COLUMN_INDEX_DIRECTORY].Text, lstFileNames.Items[i].SubItems[Constants.COLUMN_INDEX_FILE].Text);
               }
            }

            // update combo selections
            AddComboSelection(cboSearchForText, cboSearchForText.Text);
            AddComboSelection(cboFileName, cboFileName.Text);
            AddComboSelection(cboFilePath, path);

            SetWindowText();

            // reset cursor
            txtHits.Cursor = Cursors.IBeam;

            // disable gui
            SetSearchState(false);

            // reset display
            LogItems.Clear();
            SetStatusBarMessage(string.Empty);
            SetStatusBarTotalCount(0);
            SetStatusBarFileCount(0);
            SetStatusBarFilterCount(0);
            SetStatusBarErrorCount(0);

            ClearItems();
            txtHits.Clear();

            // setup structs to pass to grep
            var fileFilterSpec = GetFilterSpecFromUI();
            var searchSpec = GetSearchSpecFromUI(path, fileFilterSpec.FileFilter, filePaths);

            // create new grep instance
            __Grep = new Grep(searchSpec, fileFilterSpec);

            // add plugins
            __Grep.Plugins = Core.PluginManager.Items;

            // attach events
            __Grep.FileHit += ReceiveFileHit;
            __Grep.LineHit += ReceiveLineHit;
            __Grep.SearchCancel += ReceiveSearchCancel;
            __Grep.SearchComplete += ReceiveSearchComplete;
            __Grep.SearchError += ReceiveSearchError;
            __Grep.SearchingFile += ReceiveSearchingFile;
            __Grep.FileFiltered += ReceiveFileFiltered;
            __Grep.DirectoryFiltered += ReceiveDirectoryFiltered;
            __Grep.SearchingFileByPlugin += ReceiveSearchingFileByPlugin;
            __Grep.FileEncodingDetected += ReceiveFileEncodingDetected;

            Windows.API.TaskbarProgress.SetState(this.Handle, API.TaskbarProgress.TaskbarStates.Indeterminate);
            isSearching = true;
            LogItems.Add(new LogItem(LogItem.LogItemTypes.Status, "SearchStarted"));
            __Grep.BeginExecute();
         }
         catch (Exception ex)
         {
            RestoreTaskBarProgress();
            isSearching = false;
            LogItems.Add(new LogItem(LogItem.LogItemTypes.Error, "SearchGenericError", string.Format("{0}||{1}", string.Empty, ex.Message)));

            string message = string.Format(Language.GetGenericText("SearchGenericError"), ex.Message);
            MessageBox.Show(this, message, Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            SetStatusBarMessage(message);
            SetSearchState(true);
            CalculateTotalCount();
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

      /// <summary>
      /// Sets the grep options
      /// </summary>
      /// <history>
      /// [Andrew_Radford]		13/08/2009  CHG: Now retruns IFileFilterSpec rather than altering global state
      /// [Curtis_Beard]		01/31/2012  ADD: 1561584, ability to ignore hidden/system files/directories
      /// [Curtis_Beard]      02/09/2012  ADD: 3424156, size drop down selection
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// [Curtis_Beard]	   02/04/2014	FIX: use NumericUpDown's Value property instead of text
      /// [Curtis_Beard]	   12/01/2014	CHG: updated struct definition and moved struct declaration to SearchInterfaces.cs under Core.
      /// </history>
      private IFileFilterSpec GetFilterSpecFromUI()
      {
         string _fileName = cboFileName.Text;

         // update path and fileName if fileName has a path in it
         int slashPos = _fileName.LastIndexOf(Path.DirectorySeparatorChar.ToString());
         if (slashPos > -1)
            _fileName = _fileName.Substring(slashPos + 1);

         var spec = new SearchInterfaces.FileFilterSpec
         {
            FileFilter = _fileName,
            FilterItems = __FilterItems.FindAll(delegate(FilterItem i) { return i.Enabled; })
         };

         return spec;
      }

      /// <summary>
      /// Sets the grep options
      /// </summary>
      /// <param name="path">current directory path(s)</param>
      /// <param name="fileFilter">current file filter restrictions</param>
      /// <param name="filePaths">optional full file paths (used for search in results)</param>
      /// <history>
      /// [Curtis_Beard]		10/17/2005	Created
      /// [Curtis_Beard]		07/28/2006  ADD: extension exclusion list
      /// [Andrew_Radford]    13/08/2009  CHG: Now retruns ISearchSpec rather than altering global state
      /// [Curtis_Beard]	   01/31/2012	CHG: 3424154/1816655, allow multiple starting directories
      /// [Curtis_Beard]	   08/01/2012	FIX: 3553252, use | character for path delimitation character
      /// [Curtis_Beard]	   10/30/2012	ADD: 28, search within results
      /// [Curtis_Beard]	   02/04/2014	ADD: 66, option to detect file encoding
      /// [Curtis_Beard]	   12/01/2014	CHG: moved struct declaration to SearchInterfaces.cs under Core.
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// </history>
      private ISearchSpec GetSearchSpecFromUI(string path, string fileFilter, string[] filePaths)
      {
         var spec = new SearchInterfaces.SearchSpec
         {
            UseCaseSensitivity = chkCaseSensitive.Checked,
            ContextLines = Convert.ToInt32(txtContextLines.Value),
            IncludeLineNumbers = chkLineNumbers.Checked,
            UseNegation = chkNegation.Checked,
            ReturnOnlyFileNames = chkFileNamesOnly.Checked,
            SearchInSubfolders = chkRecurse.Checked,
            UseRegularExpressions = chkRegularExpressions.Checked,
            UseWholeWordMatching = chkWholeWordOnly.Checked,
            SearchText = cboSearchForText.Text,
            DetectFileEncoding = GeneralSettings.DetectFileEncoding,
            FileEncodings = FileEncoding.ConvertStringToFileEncodings(GeneralSettings.FileEncodings)
         };

         if (filePaths != null && filePaths.Length > 0)
         {
            spec.StartFilePaths = filePaths;
            spec.StartDirectories = null;
         }
         else
         {
            string[] paths = path.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            // fileName has a slash, so append the directory and get the file filter
            int slashPos = fileFilter.LastIndexOf(Path.DirectorySeparatorChar.ToString());
            if (slashPos > -1)
            {
               // append to each starting directory
               for (int i = 0; i < paths.Length; i++)
               {
                  paths[i] += fileFilter.Substring(0, slashPos);
               }
            }
            spec.StartDirectories = paths;
            spec.StartFilePaths = null;
         }

         return spec;
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
      /// [Curtis_Beard]		02/17/2012	CHG: update listview sorting
      /// [Curtis_Beard]		03/06/2012	ADD: listview image for file type
      /// [Curtis_Beard]		10/27/2014	CHG: 88, add file extension column
      /// [Curtis_Beard]		11/10/2014	FIX: 59, check for duplicate entries
      /// </history>
      private void AddHitToList(FileInfo file, int index)
      {
         if (lstFileNames.InvokeRequired)
         {
            AddToListCallBack _delegate = AddHitToList;
            lstFileNames.Invoke(_delegate, new object[2] { file, index });
            return;
         }

         // don't add if it already exists
         foreach (ListViewItem item in lstFileNames.Items)
         {
            HitObject hit = __Grep.RetrieveHitObject(int.Parse(item.SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text));
            if (hit.FilePath.Equals(file.FullName, StringComparison.InvariantCultureIgnoreCase))
            {
               return;
            }
         }

         // Create the list item
         var _listItem = new ListViewItem(file.Name);
         _listItem.Name = index.ToString();
         _listItem.ImageIndex = ListViewImageManager.GetImageIndex(file, ListViewImageList);
         _listItem.SubItems.Add(file.DirectoryName);
         _listItem.SubItems.Add(file.Extension);
         _listItem.SubItems.Add(file.LastWriteTime.ToString());

         // add explorer style of file size for display but store file size in bytes for comparision
         ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem(_listItem, API.StrFormatByteSize(file.Length));
         subItem.Tag = file.Length;
         _listItem.SubItems.Add(subItem);

         _listItem.SubItems.Add("0");

         // must be last
         _listItem.SubItems.Add(index.ToString());

         // Add list item to listview
         lstFileNames.Items.Add(_listItem);

         // clear it out
         _listItem = null;
      }

      /// <summary>
      /// Select all the list items.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]        09/28/2012  Initial: don't load each selected items file
      /// </history>
      private void SelectAllListItems()
      {
         foreach (ListViewItem lvi in lstFileNames.Items)
         {
            lvi.Selected = true;
         }
      }

      /// <summary>
      /// Show a message box to the user if they have exclusions or errors and how to view them.  Can be disabled
      /// via the setting in the Options dialog.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]        10/05/2012  Initial: 1741735, show dialog to user about filter/error
      /// </history>
      private void ShowExclusionsErrorMessageBox()
      {
         if (this.InvokeRequired)
         {
            DisplayExclusionErrorMessagesCallBack _delegate = ShowExclusionsErrorMessageBox;
            this.Invoke(_delegate);
            return;
         }

         // only show if not disabled by user and either filter count or error count is greater than 0
         if (Core.GeneralSettings.ShowExclusionErrorMessage &&
            (GetLogItemsCountByType(LogItem.LogItemTypes.Exclusion) > 0 ||
            GetLogItemsCountByType(LogItem.LogItemTypes.Error) > 0))
         {
            MessageBox.Show(this, Language.GetGenericText("ExclusionErrorMessageText"),
               Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
         }
      }

      /// <summary>
      /// Setup the context menu for the results area.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]        10/10/2012  Initial: 3575509, show copy/select all context menu
      /// </history>
      private void AddContextMenuForResults()
      {
         ContextMenu ctx = new ContextMenu();

         MenuItem item = new MenuItem(Language.GetGenericText("ResultsContextMenu.Copy"));
         item.Click += new EventHandler(txtHitsContextCopy_Click);
         ctx.MenuItems.Add(item);

         item = new MenuItem("-");
         ctx.MenuItems.Add(item);

         item = new MenuItem(Language.GetGenericText("ResultsContextMenu.SelectAll"));
         item.Click += new EventHandler(txtHitsContextSelectAll_Click);
         ctx.MenuItems.Add(item);

         txtHits.ContextMenu = ctx;
      }

      /// <summary>
      /// Copy selected text in results.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]        10/10/2012  Initial: 3575509, show copy/select all context menu
      /// </history>
      private void txtHitsContextCopy_Click(object sender, EventArgs e)
      {
         txtHits.Copy();
      }

      /// <summary>
      /// Select all results text.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]        10/10/2012  Initial: 3575509, show copy/select all context menu
      /// </history>
      private void txtHitsContextSelectAll_Click(object sender, EventArgs e)
      {
         txtHits.Focus();
         txtHits.SelectAll();
      }

      /// <summary>
      /// Gets the number of items in the LogItems list for a given type.
      /// </summary>
      /// <param name="type">LogItemType to determine count</param>
      /// <returns>0 if LogItems is null, count for type otherwise</returns>
      /// <history>
      /// [Curtis_Beard]	   12/06/2012	CHG: 1741735, rework to use common LogItems
      /// </history>
      private int GetLogItemsCountByType(LogItem.LogItemTypes type)
      {
         if (LogItems == null || LogItems.Count == 0)
            return 0;

         return LogItems.FindAll(
             delegate(LogItem item)
             {
                return item.ItemType == type;
             }
             ).Count;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      /// <history>
      /// </history>
      private bool AnyLogItems()
      {
         return (LogItems != null ? LogItems.Count > 0 : false);
      }

      /// <summary>
      /// Sets the form's text to include the first entry of the search path's
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   09/18/2013	CHG: 64/53, add search path to window title
      /// </history>
      private void SetWindowText()
      {
         if (cboFilePath.Items.Count > 0)
         {
            this.Text = string.Format("{0} - AstroGrep", cboFilePath.Items[0].ToString());
         }
      }

      /// <summary>
      /// Restore the Win7+ TaskBar progress state to normal.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   12/17/2014	ADD: support for Win7+ taskbar progress
      /// </history>
      private void RestoreTaskBarProgress()
      {
         if (this.InvokeRequired)
         {
            RestoreTaskBarProgressCallBack _delegate = RestoreTaskBarProgress;
            this.Invoke(_delegate);
            return;
         }

         Windows.API.TaskbarProgress.SetState(this.Handle, API.TaskbarProgress.TaskbarStates.NoProgress);
      }
      #endregion
   }
}
