using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace AstroGrep.Windows.Forms
{
   public partial class frmMain
   {
      #region Windows Form Designer generated code
      private System.Windows.Forms.ListView lstFileNames;
      private System.Windows.Forms.RichTextBox txtHits;
      private System.Windows.Forms.Panel pnlSearch;
      private System.Windows.Forms.ComboBox cboSearchForText;
      private System.Windows.Forms.ComboBox cboFileName;
      private System.Windows.Forms.ComboBox cboFilePath;
      private System.Windows.Forms.Button btnCancel;
      private AstroGrep.Windows.Controls.SplitButton btnSearch;
      private System.Windows.Forms.Panel pnlSearchOptions;
      private System.Windows.Forms.Panel pnlMainSearch;
      private System.Windows.Forms.Label lblSearchText;
      private System.Windows.Forms.Label lblFileTypes;
      private System.Windows.Forms.Label lblSearchPath;
      private System.Windows.Forms.Label lblSearchHeading;
      private System.Windows.Forms.Splitter splitUpDown;
      private System.Windows.Forms.Splitter splitLeftRight;
      private System.Windows.Forms.Panel pnlRightSide;
      private System.Windows.Forms.StatusStrip stbStatus;
      private System.Windows.Forms.ToolStripStatusLabel sbStatusPanel;
      private System.Windows.Forms.ToolStripStatusLabel sbEncodingPanel;
      private System.Windows.Forms.ToolStripStatusLabel sbTotalCountPanel;
      private System.Windows.Forms.ToolStripStatusLabel sbFileCountPanel;
      private System.Windows.Forms.ToolStripStatusLabel sbFilterCountPanel;
      private System.Windows.Forms.ToolStripStatusLabel sbErrorCountPanel;
      private System.Windows.Forms.LinkLabel lnkSearchOptions;
      private System.Windows.Forms.LinkLabel lnkExclusions;
      private System.Windows.Forms.ToolTip toolTip1;
      private System.Windows.Forms.Panel PanelOptionsContainer;
      private System.Windows.Forms.CheckBox chkNegation;
      private System.Windows.Forms.CheckBox chkCaseSensitive;
      private System.Windows.Forms.CheckBox chkRecurse;
      private System.Windows.Forms.CheckBox chkFileNamesOnly;
      private System.Windows.Forms.CheckBox chkLineNumbers;
      private System.Windows.Forms.CheckBox chkRegularExpressions;
      private System.Windows.Forms.CheckBox chkWholeWordOnly;
      private System.Windows.Forms.NumericUpDown txtContextLines;
      private System.Windows.Forms.Label lblContextLines;
      private System.Windows.Forms.MenuItem mnuFile;
      private System.Windows.Forms.MenuItem mnuNewWindow;
      private System.Windows.Forms.MenuItem mnuSaveResults;
      private System.Windows.Forms.MenuItem mnuPrintResults;
      private System.Windows.Forms.MenuItem mnuExit;
      private System.Windows.Forms.MenuItem mnuEdit;
      private System.Windows.Forms.MenuItem mnuTools;
      private System.Windows.Forms.MenuItem mnuOptions;
      private System.Windows.Forms.MenuItem mnuHelp;
      private System.Windows.Forms.MenuItem mnuHelpContents;
      private System.Windows.Forms.MenuItem mnuHelpRegEx;
      private System.Windows.Forms.MenuItem mnuHelpSep1;
      private System.Windows.Forms.MenuItem mnuCheckForUpdates;
      private System.Windows.Forms.MenuItem mnuHelpSep2;
      private System.Windows.Forms.MenuItem mnuAbout;
      private System.Windows.Forms.MenuItem mnuSelectAll;
      private System.Windows.Forms.MenuItem mnuOpenSelected;
      private System.Windows.Forms.MenuItem mnuClearMRU;
      private System.Windows.Forms.MainMenu mnuAll;
      private System.Windows.Forms.MenuItem mnuFileSep;
      private System.Windows.Forms.MenuItem mnuToolsSep;
      private System.Windows.Forms.MenuItem mnuSaveSearchSettings;
      private System.Windows.Forms.ImageList ListViewImageList;
      private AstroGrep.Windows.Controls.PictureButton picBrowse;
      private System.Windows.Forms.MenuItem mnuBrowse;
      private System.Windows.Forms.MenuItem mnuFileSep2;
      private System.Windows.Forms.MenuItem mnuFileSep3;
      private System.Windows.Forms.ContextMenu fileLstMnu;
      private System.Windows.Forms.MenuItem menuItem4;
      private System.Windows.Forms.MenuItem CopyMenuItem;
      private System.Windows.Forms.MenuItem OpenMenuItem;
      private System.Windows.Forms.MenuItem DeleteMenuItem;
      private System.Windows.Forms.MenuItem OpenFolderMenuItem;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem CopyNameMenuItem;
      private System.Windows.Forms.MenuItem CopyLocatedInMenuItem;
      private System.Windows.Forms.MenuItem CopyLocatedInAndNameMenuItem;
      private System.Windows.Forms.MenuItem OpenWithAssociatedApp;
	   private MenuItem FileOperationsMenuItem;
      private MenuItem FileCopyMenuItem;
      private MenuItem FileDeleteMenuItem;
      private MenuItem menuItem6;
      private System.Windows.Forms.ContextMenu ctxMenuBtnSearch;
      private MenuItem MenuBtnSearch;
      private MenuItem ToolsMRUSearchPaths;
      private MenuItem ToolsMRUFilterTypes;
      private MenuItem ToolsMRUSearchText;
      private MenuItem ToolsMRUSep;
      private MenuItem ToolsMRUAll;
      private MenuItem mnuView;
      private MenuItem ViewStatus;
      private MenuItem ViewExclusions;
      private MenuItem ViewError;
      private MenuItem ViewAll;

      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
         this.pnlSearch = new System.Windows.Forms.Panel();
         this.pnlSearchOptions = new System.Windows.Forms.Panel();
         this.PanelOptionsContainer = new System.Windows.Forms.Panel();
         this.lblContextLines = new System.Windows.Forms.Label();
         this.txtContextLines = new System.Windows.Forms.NumericUpDown();
         this.chkWholeWordOnly = new System.Windows.Forms.CheckBox();
         this.chkRegularExpressions = new System.Windows.Forms.CheckBox();
         this.chkNegation = new System.Windows.Forms.CheckBox();
         this.chkLineNumbers = new System.Windows.Forms.CheckBox();
         this.chkFileNamesOnly = new System.Windows.Forms.CheckBox();
         this.chkRecurse = new System.Windows.Forms.CheckBox();
         this.chkCaseSensitive = new System.Windows.Forms.CheckBox();
         this.lnkSearchOptions = new System.Windows.Forms.LinkLabel();
         this.lnkExclusions = new System.Windows.Forms.LinkLabel();
         this.pnlMainSearch = new System.Windows.Forms.Panel();
         this.picBrowse = new AstroGrep.Windows.Controls.PictureButton();
         this.btnSearch = new Windows.Controls.SplitButton();
         this.btnCancel = new System.Windows.Forms.Button();
         this.cboFilePath = new System.Windows.Forms.ComboBox();
         this.cboFileName = new System.Windows.Forms.ComboBox();
         this.cboSearchForText = new System.Windows.Forms.ComboBox();
         this.lblSearchText = new System.Windows.Forms.Label();
         this.lblFileTypes = new System.Windows.Forms.Label();
         this.lblSearchPath = new System.Windows.Forms.Label();
         this.lblSearchHeading = new System.Windows.Forms.Label();
         this.pnlRightSide = new System.Windows.Forms.Panel();
         this.txtHits = new System.Windows.Forms.RichTextBox();
         this.splitUpDown = new System.Windows.Forms.Splitter();
         this.lstFileNames = new System.Windows.Forms.ListView();
         this.fileLstMnu = new System.Windows.Forms.ContextMenu();
         this.CopyMenuItem = new System.Windows.Forms.MenuItem();
         this.menuItem4 = new System.Windows.Forms.MenuItem();
         this.OpenMenuItem = new System.Windows.Forms.MenuItem();
         this.OpenFolderMenuItem = new System.Windows.Forms.MenuItem();
         this.OpenWithAssociatedApp = new System.Windows.Forms.MenuItem();
         this.CopyNameMenuItem = new System.Windows.Forms.MenuItem();
         this.CopyLocatedInMenuItem = new System.Windows.Forms.MenuItem();
         this.CopyLocatedInAndNameMenuItem = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.DeleteMenuItem = new System.Windows.Forms.MenuItem();
         this.splitLeftRight = new System.Windows.Forms.Splitter();
         this.mnuAll = new System.Windows.Forms.MainMenu(this.components);
         this.mnuFile = new System.Windows.Forms.MenuItem();
         this.mnuNewWindow = new System.Windows.Forms.MenuItem();
         this.mnuFileSep3 = new System.Windows.Forms.MenuItem();
         this.mnuBrowse = new System.Windows.Forms.MenuItem();
         this.mnuFileSep2 = new System.Windows.Forms.MenuItem();
         this.mnuSaveResults = new System.Windows.Forms.MenuItem();
         this.mnuPrintResults = new System.Windows.Forms.MenuItem();
         this.mnuFileSep = new System.Windows.Forms.MenuItem();
         this.mnuExit = new System.Windows.Forms.MenuItem();
         this.mnuEdit = new System.Windows.Forms.MenuItem();
         this.mnuSelectAll = new System.Windows.Forms.MenuItem();
         this.mnuOpenSelected = new System.Windows.Forms.MenuItem();
         this.mnuTools = new System.Windows.Forms.MenuItem();
         this.mnuClearMRU = new System.Windows.Forms.MenuItem();
         this.mnuToolsSep = new System.Windows.Forms.MenuItem();
         this.mnuSaveSearchSettings = new System.Windows.Forms.MenuItem();
         this.mnuOptions = new System.Windows.Forms.MenuItem();
         this.mnuHelp = new System.Windows.Forms.MenuItem();
         this.mnuHelpContents = new System.Windows.Forms.MenuItem();
         this.mnuHelpRegEx = new System.Windows.Forms.MenuItem();
         this.mnuHelpSep1 = new System.Windows.Forms.MenuItem();
         this.mnuCheckForUpdates = new System.Windows.Forms.MenuItem();
         this.mnuHelpSep2 = new System.Windows.Forms.MenuItem();
         this.mnuAbout = new System.Windows.Forms.MenuItem();
         this.stbStatus = new System.Windows.Forms.StatusStrip();
         this.sbStatusPanel = new System.Windows.Forms.ToolStripStatusLabel();
         this.sbEncodingPanel = new System.Windows.Forms.ToolStripStatusLabel();
         this.sbTotalCountPanel = new System.Windows.Forms.ToolStripStatusLabel();
         this.sbFileCountPanel = new System.Windows.Forms.ToolStripStatusLabel();
         this.sbFilterCountPanel = new System.Windows.Forms.ToolStripStatusLabel();
         this.sbErrorCountPanel = new System.Windows.Forms.ToolStripStatusLabel();
         this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
         this.ListViewImageList = new System.Windows.Forms.ImageList(this.components);
         this.FileOperationsMenuItem = new System.Windows.Forms.MenuItem();
         this.FileCopyMenuItem = new System.Windows.Forms.MenuItem();
         this.FileDeleteMenuItem = new System.Windows.Forms.MenuItem();
         this.menuItem6 = new System.Windows.Forms.MenuItem();
         this.ctxMenuBtnSearch = new System.Windows.Forms.ContextMenu();
         this.MenuBtnSearch = new MenuItem();
         this.ToolsMRUSearchPaths = new MenuItem();
         this.ToolsMRUFilterTypes = new MenuItem();
         this.ToolsMRUSearchText = new MenuItem();
         this.ToolsMRUAll = new MenuItem();
         this.ToolsMRUSep = new MenuItem();
         this.mnuView = new MenuItem();
         this.ViewStatus = new MenuItem();
         this.ViewExclusions = new MenuItem();
         this.ViewError = new MenuItem();
         this.ViewAll = new MenuItem();
		   this.pnlSearch.SuspendLayout();
         this.pnlSearchOptions.SuspendLayout();

         this.PanelOptionsContainer.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.txtContextLines)).BeginInit();
         this.pnlMainSearch.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.picBrowse)).BeginInit();
         this.pnlRightSide.SuspendLayout();
         this.SuspendLayout();

         this.MenuBtnSearch.Text = "Search in results";
         this.MenuBtnSearch.Click += mnuSearchInResults_Click;
         this.ctxMenuBtnSearch.MenuItems.Add(this.MenuBtnSearch);
         // 
         // pnlSearch
         // 
         this.pnlSearch.AutoScroll = true;
         this.pnlSearch.BackColor = System.Drawing.SystemColors.Window;
         this.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.pnlSearch.Controls.Add(this.pnlSearchOptions);
         this.pnlSearch.Controls.Add(this.pnlMainSearch);
         this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Left;
         this.pnlSearch.Location = new System.Drawing.Point(0, 0);
         this.pnlSearch.Name = "pnlSearch";
         this.pnlSearch.Size = new System.Drawing.Size(240, 430);
         this.pnlSearch.TabIndex = 0;
         // 
         // pnlSearchOptions
         // 
         this.pnlSearchOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.pnlSearchOptions.Controls.Add(this.PanelOptionsContainer);
         this.pnlSearchOptions.Controls.Add(this.lnkSearchOptions);
         this.pnlSearchOptions.Location = new System.Drawing.Point(16, 209);
         this.pnlSearchOptions.Name = "pnlSearchOptions";
         this.pnlSearchOptions.Size = new System.Drawing.Size(200, 250);
         this.pnlSearchOptions.TabIndex = 1;
         // 
         // PanelOptionsContainer
         // 
         this.PanelOptionsContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.PanelOptionsContainer.Controls.Add(this.lnkExclusions);
         this.PanelOptionsContainer.Controls.Add(this.lblContextLines);
         this.PanelOptionsContainer.Controls.Add(this.txtContextLines);
         this.PanelOptionsContainer.Controls.Add(this.chkWholeWordOnly);
         this.PanelOptionsContainer.Controls.Add(this.chkRegularExpressions);
         this.PanelOptionsContainer.Controls.Add(this.chkNegation);
         this.PanelOptionsContainer.Controls.Add(this.chkLineNumbers);
         this.PanelOptionsContainer.Controls.Add(this.chkFileNamesOnly);
         this.PanelOptionsContainer.Controls.Add(this.chkRecurse);
         this.PanelOptionsContainer.Controls.Add(this.chkCaseSensitive);
         this.PanelOptionsContainer.Location = new System.Drawing.Point(0, 16);
         this.PanelOptionsContainer.Name = "PanelOptionsContainer";
         this.PanelOptionsContainer.Size = new System.Drawing.Size(200, 233);
         this.PanelOptionsContainer.TabIndex = 1;
         // 
         // lblContextLines
         // 
         this.lblContextLines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lblContextLines.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblContextLines.Location = new System.Drawing.Point(56, 175);
         this.lblContextLines.Name = "lblContextLines";
         this.lblContextLines.Size = new System.Drawing.Size(127, 20);
         this.lblContextLines.TabIndex = 8;
         this.lblContextLines.Text = "Context Lines";
         this.lblContextLines.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.toolTip1.SetToolTip(this.lblContextLines, "Show lines above and below the word matched");
         // 
         // txtContextLines
         // 
         this.txtContextLines.Location = new System.Drawing.Point(7, 175);
         this.txtContextLines.Name = "txtContextLines";
         this.txtContextLines.Size = new System.Drawing.Size(41, 20);
         this.txtContextLines.TabIndex = 13;
         this.toolTip1.SetToolTip(this.txtContextLines, "Show lines above and below the word matched");
         // 
         // chkWholeWordOnly
         // 
         this.chkWholeWordOnly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.chkWholeWordOnly.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkWholeWordOnly.Location = new System.Drawing.Point(7, 56);
         this.chkWholeWordOnly.Name = "chkWholeWordOnly";
         this.chkWholeWordOnly.Size = new System.Drawing.Size(178, 16);
         this.chkWholeWordOnly.TabIndex = 8;
         this.chkWholeWordOnly.Text = "&Whole Word";
         this.toolTip1.SetToolTip(this.chkWholeWordOnly, "Only match entire words (not parts of words)");
         // 
         // chkRegularExpressions
         // 
         this.chkRegularExpressions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.chkRegularExpressions.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkRegularExpressions.Location = new System.Drawing.Point(7, 8);
         this.chkRegularExpressions.Name = "chkRegularExpressions";
         this.chkRegularExpressions.Size = new System.Drawing.Size(178, 16);
         this.chkRegularExpressions.TabIndex = 6;
         this.chkRegularExpressions.Text = "Regular &Expressions";
         this.toolTip1.SetToolTip(this.chkRegularExpressions, "Use \"regular expression\" matching");
         // 
         // chkNegation
         // 
         this.chkNegation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.chkNegation.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkNegation.Location = new System.Drawing.Point(7, 128);
         this.chkNegation.Name = "chkNegation";
         this.chkNegation.Size = new System.Drawing.Size(178, 16);
         this.chkNegation.TabIndex = 11;
         this.chkNegation.Text = "&Negation";
         this.toolTip1.SetToolTip(this.chkNegation, "Find the files without the Search Text in them");
         // 
         // chkLineNumbers
         // 
         this.chkLineNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.chkLineNumbers.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkLineNumbers.Location = new System.Drawing.Point(7, 152);
         this.chkLineNumbers.Name = "chkLineNumbers";
         this.chkLineNumbers.Size = new System.Drawing.Size(178, 16);
         this.chkLineNumbers.TabIndex = 12;
         this.chkLineNumbers.Text = "&Line Numbers";
         this.toolTip1.SetToolTip(this.chkLineNumbers, "Include line numbers before each match");
         // 
         // chkFileNamesOnly
         // 
         this.chkFileNamesOnly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.chkFileNamesOnly.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkFileNamesOnly.Location = new System.Drawing.Point(7, 104);
         this.chkFileNamesOnly.Name = "chkFileNamesOnly";
         this.chkFileNamesOnly.Size = new System.Drawing.Size(178, 16);
         this.chkFileNamesOnly.TabIndex = 10;
         this.chkFileNamesOnly.Text = "Show File Names &Only";
         this.toolTip1.SetToolTip(this.chkFileNamesOnly, "Show names but not contents of files that have matches (may be faster on large fi" +
                 "les)");
         // 
         // chkRecurse
         // 
         this.chkRecurse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.chkRecurse.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkRecurse.Location = new System.Drawing.Point(7, 80);
         this.chkRecurse.Name = "chkRecurse";
         this.chkRecurse.Size = new System.Drawing.Size(178, 16);
         this.chkRecurse.TabIndex = 9;
         this.chkRecurse.Text = "&Recurse";
         this.toolTip1.SetToolTip(this.chkRecurse, "Search in subdirectories");
         // 
         // chkCaseSensitive
         // 
         this.chkCaseSensitive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.chkCaseSensitive.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkCaseSensitive.Location = new System.Drawing.Point(7, 32);
         this.chkCaseSensitive.Name = "chkCaseSensitive";
         this.chkCaseSensitive.Size = new System.Drawing.Size(178, 16);
         this.chkCaseSensitive.TabIndex = 7;
         this.chkCaseSensitive.Text = "&Case Sensitive";
         this.toolTip1.SetToolTip(this.chkCaseSensitive, "Match upper and lower case letters exactly");
         // 
         // lnkSearchOptions
         // 
         this.lnkSearchOptions.ActiveLinkColor = System.Drawing.SystemColors.ActiveCaption;
         this.lnkSearchOptions.Dock = System.Windows.Forms.DockStyle.Top;
         this.lnkSearchOptions.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lnkSearchOptions.LinkColor = System.Drawing.SystemColors.ActiveCaption;
         this.lnkSearchOptions.Location = new System.Drawing.Point(0, 0);
         this.lnkSearchOptions.Name = "lnkSearchOptions";
         this.lnkSearchOptions.Size = new System.Drawing.Size(200, 16);
         this.lnkSearchOptions.TabIndex = 5;
         this.lnkSearchOptions.TabStop = true;
         this.lnkSearchOptions.Text = "Search Options >>";
         this.lnkSearchOptions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.lnkSearchOptions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchOptions_LinkClicked);
         // 
         // lnkExclusions
         // 
         this.lnkExclusions.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
         this.lnkExclusions.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lnkExclusions.LinkColor = System.Drawing.SystemColors.HotTrack;
         this.lnkExclusions.Location = new System.Drawing.Point(5, 207);
         this.lnkExclusions.LinkBehavior = LinkBehavior.AlwaysUnderline;

         this.lnkExclusions.LinkBehavior = LinkBehavior.AlwaysUnderline;
         this.lnkExclusions.Name = "lnkExclusions";
         this.lnkExclusions.Size = new System.Drawing.Size(150, 16);
         this.lnkExclusions.TabIndex = 24;
         this.lnkExclusions.TabStop = true;
         this.lnkExclusions.Text = "Exclusions...";
         this.lnkExclusions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.lnkExclusions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkExclusions_LinkClicked);
         this.lnkExclusions.BackColor = SystemColors.Window;
         // 
         // pnlMainSearch
         // 
         this.pnlMainSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.pnlMainSearch.Controls.Add(this.picBrowse);
         this.pnlMainSearch.Controls.Add(this.btnSearch);
         this.pnlMainSearch.Controls.Add(this.btnCancel);
         this.pnlMainSearch.Controls.Add(this.cboFilePath);
         this.pnlMainSearch.Controls.Add(this.cboFileName);
         this.pnlMainSearch.Controls.Add(this.cboSearchForText);
         this.pnlMainSearch.Controls.Add(this.lblSearchText);
         this.pnlMainSearch.Controls.Add(this.lblFileTypes);
         this.pnlMainSearch.Controls.Add(this.lblSearchPath);
         this.pnlMainSearch.Controls.Add(this.lblSearchHeading);
         this.pnlMainSearch.Location = new System.Drawing.Point(16, 9);
         this.pnlMainSearch.Name = "pnlMainSearch";
         this.pnlMainSearch.Size = new System.Drawing.Size(200, 192);
         this.pnlMainSearch.TabIndex = 0;
         // 
         // picBrowse
         // 
         this.picBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.picBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
         this.picBrowse.Image = ((System.Drawing.Image)(resources.GetObject("picBrowse.Image")));
         this.picBrowse.Location = new System.Drawing.Point(175, 42);
         this.picBrowse.Name = "picBrowse";
         this.picBrowse.Size = new System.Drawing.Size(16, 16);
         this.picBrowse.TabIndex = 6;
         this.picBrowse.TabStop = false;
         this.picBrowse.Click += new System.EventHandler(this.picBrowse_Click);
         // 
         // btnSearch
         // 
         this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
         this.btnSearch.BackColor = System.Drawing.SystemColors.Control;
         this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
         this.btnSearch.ShowSplit = true;
         this.btnSearch.Location = new System.Drawing.Point(8, 160);
         this.btnSearch.Name = "btnSearch";
         this.btnSearch.Size = new System.Drawing.Size(80, 23);
         this.btnSearch.TabIndex = 0;
         this.btnSearch.Text = "&Search";
         this.btnSearch.UseVisualStyleBackColor = false;
         this.btnSearch.Click += new EventHandler(btnSearch_Click);
         this.btnSearch.ContextMenu = ctxMenuBtnSearch;
         this.btnSearch.ContextMenu.MenuItems[0].Enabled = false;
         // 
         // btnCancel
         // 
         this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
         this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnCancel.Enabled = false;
         this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnCancel.Location = new System.Drawing.Point(111, 160);
         this.btnCancel.Name = "btnCancel";
         this.btnCancel.Size = new System.Drawing.Size(80, 23);
         this.btnCancel.TabIndex = 4;
         this.btnCancel.Text = "&Cancel";
         this.btnCancel.UseVisualStyleBackColor = false;
         this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
         // 
         // cboFilePath
         // 
         this.cboFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.cboFilePath.Location = new System.Drawing.Point(11, 40);
         this.cboFilePath.Name = "cboFilePath";
         this.cboFilePath.Size = new System.Drawing.Size(154, 21);
         this.cboFilePath.TabIndex = 1;
         // 
         // cboFileName
         // 
         this.cboFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.cboFileName.Location = new System.Drawing.Point(11, 80);
         this.cboFileName.Name = "cboFileName";
         this.cboFileName.Size = new System.Drawing.Size(180, 21);
         this.cboFileName.TabIndex = 2;
         // 
         // cboSearchForText
         // 
         this.cboSearchForText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.cboSearchForText.Location = new System.Drawing.Point(11, 120);
         this.cboSearchForText.Name = "cboSearchForText";
         this.cboSearchForText.Size = new System.Drawing.Size(180, 21);
         this.cboSearchForText.TabIndex = 3;
         // 
         // lblSearchText
         // 
         this.lblSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lblSearchText.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblSearchText.Location = new System.Drawing.Point(8, 104);
         this.lblSearchText.Name = "lblSearchText";
         this.lblSearchText.Size = new System.Drawing.Size(163, 16);
         this.lblSearchText.TabIndex = 3;
         this.lblSearchText.Text = "Search Text";
         // 
         // lblFileTypes
         // 
         this.lblFileTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lblFileTypes.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblFileTypes.Location = new System.Drawing.Point(8, 64);
         this.lblFileTypes.Name = "lblFileTypes";
         this.lblFileTypes.Size = new System.Drawing.Size(163, 16);
         this.lblFileTypes.TabIndex = 2;
         this.lblFileTypes.Text = "File Types";
         // 
         // lblSearchPath
         // 
         this.lblSearchPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lblSearchPath.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblSearchPath.Location = new System.Drawing.Point(8, 24);
         this.lblSearchPath.Name = "lblSearchPath";
         this.lblSearchPath.Size = new System.Drawing.Size(163, 16);
         this.lblSearchPath.TabIndex = 1;
         this.lblSearchPath.Text = "Search Path";
         // 
         // lblSearchHeading
         // 
         this.lblSearchHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lblSearchHeading.BackColor = System.Drawing.SystemColors.ActiveCaption;
         this.lblSearchHeading.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
         this.lblSearchHeading.Location = new System.Drawing.Point(1, 0);
         this.lblSearchHeading.Name = "lblSearchHeading";
         this.lblSearchHeading.Size = new System.Drawing.Size(199, 16);
         this.lblSearchHeading.TabIndex = 0;
         this.lblSearchHeading.Text = "AstroGrep Search";
         this.lblSearchHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // pnlRightSide
         // 
         this.pnlRightSide.Controls.Add(this.txtHits);
         this.pnlRightSide.Controls.Add(this.splitUpDown);
         this.pnlRightSide.Controls.Add(this.lstFileNames);
         this.pnlRightSide.Dock = System.Windows.Forms.DockStyle.Fill;
         this.pnlRightSide.Location = new System.Drawing.Point(240, 0);
         this.pnlRightSide.Name = "pnlRightSide";
         this.pnlRightSide.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
         this.pnlRightSide.Size = new System.Drawing.Size(544, 430);
         this.pnlRightSide.TabIndex = 1;
         // 
         // txtHits
         // 
         this.txtHits.DetectUrls = false;
         this.txtHits.Dock = System.Windows.Forms.DockStyle.Fill;
         this.txtHits.Location = new System.Drawing.Point(8, 200);
         this.txtHits.Name = "txtHits";
         this.txtHits.ReadOnly = true;
         this.txtHits.Size = new System.Drawing.Size(536, 230);
         this.txtHits.TabIndex = 1;
         this.txtHits.Text = "";
         this.txtHits.WordWrap = false;
         // 
         // splitUpDown
         // 
         this.splitUpDown.Dock = System.Windows.Forms.DockStyle.Top;
         this.splitUpDown.Location = new System.Drawing.Point(8, 192);
         this.splitUpDown.Name = "splitUpDown";
         this.splitUpDown.Size = new System.Drawing.Size(536, 8);
         this.splitUpDown.TabIndex = 2;
         this.splitUpDown.TabStop = false;
         // 
         // lstFileNames
         // 
         this.lstFileNames.ContextMenu = this.fileLstMnu;
         this.lstFileNames.Dock = System.Windows.Forms.DockStyle.Top;
         this.lstFileNames.FullRowSelect = true;
         this.lstFileNames.HideSelection = false;
         this.lstFileNames.Location = new System.Drawing.Point(8, 0);
         this.lstFileNames.Name = "lstFileNames";
         this.lstFileNames.Size = new System.Drawing.Size(536, 192);
         this.lstFileNames.TabIndex = 0;
         this.lstFileNames.UseCompatibleStateImageBehavior = false;
         this.lstFileNames.View = System.Windows.Forms.View.Details;
         this.lstFileNames.SelectedIndexChanged += new System.EventHandler(this.lstFileNames_SelectedIndexChanged);
         this.lstFileNames.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstFileNames_KeyDown);
         this.lstFileNames.SmallImageList = ListViewImageList;
         // 
         // fileLstMnu
         // 
         this.fileLstMnu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.CopyMenuItem,
			this.CopyNameMenuItem,
		    this.CopyLocatedInMenuItem,
		    this.CopyLocatedInAndNameMenuItem,
            this.menuItem4,
            this.FileOperationsMenuItem,
            this.menuItem6,
            this.OpenMenuItem,
            this.OpenFolderMenuItem,
            this.OpenWithAssociatedApp,
            this.menuItem2,
            this.DeleteMenuItem});
         // 
         // CopyMenuItem
         // 
         this.CopyMenuItem.Index = 0;
         this.CopyMenuItem.Text = "Copy All";
         this.CopyMenuItem.Click += new System.EventHandler(this.CopyMenuItem_Click);
         // 
         // CopyNameMenuItem
         //
         this.CopyNameMenuItem.Index = 1;
         this.CopyNameMenuItem.Text = "Copy Name";
         this.CopyNameMenuItem.Click += new System.EventHandler(this.CopyNameMenuItem_Click);
         // 
         // CopyLocatedInMenuItem
         //
         this.CopyLocatedInMenuItem.Index = 2;
         this.CopyLocatedInMenuItem.Text = "Copy Located In";
         this.CopyLocatedInMenuItem.Click += new System.EventHandler(this.CopyLocatedInMenuItem_Click);
         // 
         // CopyLocatedInAndNameMenuItem
         //
         this.CopyLocatedInAndNameMenuItem.Index = 3;
         this.CopyLocatedInAndNameMenuItem.Text = "Copy Located In + Name";
         this.CopyLocatedInAndNameMenuItem.Click += new System.EventHandler(this.CopyLocatedInAndNameMenuItem_Click);
         // 
         // menuItem4
         // 
         this.menuItem4.Index = 4;
         this.menuItem4.Text = "-";
         // 
         // OpenMenuItem
         // 
         this.OpenMenuItem.Index = 7;
         this.OpenMenuItem.Text = "Open File";
         this.OpenMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
         // 
         // OpenFolderMenuItem
         // 
         this.OpenFolderMenuItem.Index = 8;
         this.OpenFolderMenuItem.Text = "Open Directory";
         this.OpenFolderMenuItem.Click += new System.EventHandler(this.OpenFolderMenuItem_Click);
         //
         // OpenWithAssociatedApp
         //
         this.OpenWithAssociatedApp.Index = 9;
         this.OpenWithAssociatedApp.Text = "Open With Associated App";
         this.OpenWithAssociatedApp.Click += new System.EventHandler(this.OpenWithAssociatedApp_Click);
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 10;
         this.menuItem2.Text = "-";
         // 
         // DeleteMenuItem
         // 
         this.DeleteMenuItem.Index = 11;
         this.DeleteMenuItem.Text = "Remove from list";
         this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
         // 
         // splitLeftRight
         // 
         this.splitLeftRight.Location = new System.Drawing.Point(240, 0);
         this.splitLeftRight.MinExtra = 100;
         this.splitLeftRight.MinSize = 290;
         this.splitLeftRight.Name = "splitLeftRight";
         this.splitLeftRight.Size = new System.Drawing.Size(8, 430);
         this.splitLeftRight.TabIndex = 2;
         this.splitLeftRight.TabStop = false;
         // 
         // mnuAll
         // 
         this.mnuAll.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuTools,
            this.mnuHelp});
         // 
         // mnuFile
         // 
         this.mnuFile.Index = 0;
         this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuNewWindow,
            this.mnuFileSep3,
            this.mnuBrowse,
            this.mnuFileSep2,
            this.mnuSaveResults,
            this.mnuPrintResults,
            this.mnuFileSep,
            this.mnuExit});
         this.mnuFile.Text = "&File";
         // 
         // mnuBrowse
         // 
         this.mnuNewWindow.Index = 0;
         this.mnuNewWindow.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
         this.mnuNewWindow.Text = "&New Window";
         this.mnuNewWindow.Click += new System.EventHandler(this.mnuNewWindow_Click);
         // 
         // mnuFileSep3
         // 
         this.mnuFileSep3.Index = 1;
         this.mnuFileSep3.Text = "-";
         // 
         // mnuBrowse
         // 
         this.mnuBrowse.Index = 2;
         this.mnuBrowse.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
         this.mnuBrowse.Text = "Select Sea&rch Path...";
         this.mnuBrowse.Click += new System.EventHandler(this.mnuBrowse_Click);
         // 
         // mnuFileSep2
         // 
         this.mnuFileSep2.Index = 3;
         this.mnuFileSep2.Text = "-";
         // 
         // mnuSaveResults
         // 
         this.mnuSaveResults.Index = 4;
         this.mnuSaveResults.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
         this.mnuSaveResults.Text = "&Save Results";
         this.mnuSaveResults.Click += new System.EventHandler(this.mnuSaveResults_Click);
         // 
         // mnuPrintResults
         // 
         this.mnuPrintResults.Index = 5;
         this.mnuPrintResults.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
         this.mnuPrintResults.Text = "&Print Results";
         this.mnuPrintResults.Click += new System.EventHandler(this.mnuPrintResults_Click);
         // 
         // mnuFileSep
         // 
         this.mnuFileSep.Index = 6;
         this.mnuFileSep.Text = "-";
         // 
         // mnuExit
         // 
         this.mnuExit.Index = 7;
         this.mnuExit.Shortcut = System.Windows.Forms.Shortcut.CtrlQ;
         this.mnuExit.Text = "E&xit";
         this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
         // 
         // mnuEdit
         // 
         this.mnuEdit.Index = 1;
         this.mnuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuSelectAll,
            this.mnuOpenSelected});
         this.mnuEdit.Text = "&Edit";
         // 
         // mnuSelectAll
         // 
         this.mnuSelectAll.Index = 0;
         this.mnuSelectAll.Text = "&Select All Files";
         this.mnuSelectAll.Click += new System.EventHandler(this.mnuSelectAll_Click);
         // 
         // mnuOpenSelected
         // 
         this.mnuOpenSelected.Index = 1;
         this.mnuOpenSelected.Text = "&Open Selected Files";
         this.mnuOpenSelected.Click += new System.EventHandler(this.mnuOpenSelected_Click);
         //
         // mnuView
         //
         this.mnuView.Index = 2;
         this.mnuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]{
            this.ViewStatus,
            this.ViewExclusions,
            this.ViewError,
            this.ViewAll});
         this.mnuView.Text = "&View";
         // 
         // ViewStatus
         // 
         this.ViewStatus.Index = 0;
         this.ViewStatus.Text = "&Status Messages";
         this.ViewStatus.Click += new System.EventHandler(this.ViewStatus_Click);
         // 
         // ViewExclusions
         // 
         this.ViewExclusions.Index = 1;
         this.ViewExclusions.Text = "&Exclusion Messages";
         this.ViewExclusions.Click += new System.EventHandler(this.ViewExclusions_Click);
         // 
         // ViewError
         // 
         this.ViewError.Index = 2;
         this.ViewError.Text = "E&rror Messages";
         this.ViewError.Click += new System.EventHandler(this.ViewError_Click);
         // 
         // ViewAll
         // 
         this.ViewAll.Index = 3;
         this.ViewAll.Text = "&All Messages";
         this.ViewAll.Click += new System.EventHandler(this.ViewAll_Click);
         // 
         // mnuTools
         // 
         this.mnuTools.Index = 3;
         this.mnuTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuClearMRU,
            this.mnuToolsSep,
            this.mnuSaveSearchSettings,
            this.mnuOptions});
         this.mnuTools.Text = "&Tools";
         // 
         // ToolsMRUSearchPaths
         // 
         this.ToolsMRUSearchPaths.Index = 0;
         this.ToolsMRUSearchPaths.Text = "&Search Paths";
         this.ToolsMRUSearchPaths.Click += new System.EventHandler(this.ToolsMRUSearchPaths_Click);
         // 
         // ToolsMRUFilterTypes
         // 
         this.ToolsMRUFilterTypes.Index = 1;
         this.ToolsMRUFilterTypes.Text = "&Search Paths";
         this.ToolsMRUFilterTypes.Click += new System.EventHandler(this.ToolsMRUFilterTypes_Click);
         // 
         // ToolsMRUSearchText
         // 
         this.ToolsMRUSearchText.Index = 2;
         this.ToolsMRUSearchText.Text = "&Search Paths";
         this.ToolsMRUSearchText.Click += new System.EventHandler(this.ToolsMRUSearchText_Click);
         // 
         // ToolsMRUSep
         // 
         this.ToolsMRUSep.Index = 3;
         this.ToolsMRUSep.Text = "-";
         // 
         // ToolsMRUAll
         // 
         this.ToolsMRUAll.Index = 4;
         this.ToolsMRUAll.Text = "&Search Paths";
         this.ToolsMRUAll.Click += new System.EventHandler(this.ToolsMRUAll_Click);
         // 
         // mnuClearMRU
         // 
         this.mnuClearMRU.Index = 0;         
         this.mnuClearMRU.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ToolsMRUSearchPaths,
            this.ToolsMRUFilterTypes,
            this.ToolsMRUSearchText,
            this.ToolsMRUSep,
            this.ToolsMRUAll});
         this.mnuClearMRU.Text = "&Clear Most Recently Used Lists";
         //this.mnuClearMRU.Click += new System.EventHandler(this.mnuClearMRU_Click);
         // 
         // mnuToolsSep
         // 
         this.mnuToolsSep.Index = 1;
         this.mnuToolsSep.Text = "-";
         // 
         // mnuSaveSearchSettings
         // 
         this.mnuSaveSearchSettings.Index = 2;
         this.mnuSaveSearchSettings.Text = "&Save Search Options";
         this.mnuSaveSearchSettings.Click += new System.EventHandler(this.mnuSaveSearchSettings_Click);
         // 
         // mnuOptions
         // 
         this.mnuOptions.Index = 3;
         this.mnuOptions.Shortcut = System.Windows.Forms.Shortcut.F9;
         this.mnuOptions.Text = "&Options...";
         this.mnuOptions.Click += new System.EventHandler(this.mnuOptions_Click);
         // 
         // mnuHelp
         // 
         this.mnuHelp.Index = 4;
         this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuHelpContents,
            this.mnuHelpRegEx,
            this.mnuHelpSep1,
            this.mnuCheckForUpdates,
            this.mnuHelpSep2,
            this.mnuAbout});
         this.mnuHelp.Text = "&Help";
         // 
         // mnuHelpContents
         // 
         this.mnuHelpContents.Index = 0;
         this.mnuHelpContents.Text = "&View Help";
         this.mnuHelpContents.Click += new System.EventHandler(this.mnuHelpContents_Click);
         // 
         // mnuHelpRegEx
         // 
         this.mnuHelpRegEx.Index = 1;
         this.mnuHelpRegEx.Text = "&Regular Expressions";
         this.mnuHelpRegEx.Click += new System.EventHandler(this.mnuHelpRegEx_Click);
         // 
         // mnuHelpSep1
         // 
         this.mnuHelpSep1.Index = 2;
         this.mnuHelpSep1.Text = "-";
         // 
         // mnuCheckForUpdates
         // 
         this.mnuCheckForUpdates.Index = 3;
         this.mnuCheckForUpdates.Text = "&Check for Updates...";
         this.mnuCheckForUpdates.Click += new System.EventHandler(this.mnuCheckForUpdates_Click);
         // 
         // mnuHelpSep2
         // 
         this.mnuHelpSep2.Index = 4;
         this.mnuHelpSep2.Text = "-";
         // 
         // mnuAbout
         // 
         this.mnuAbout.Index = 5;
         this.mnuAbout.Text = "&About AstroGrep";
         this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
         // 
         // stbStatus
         // 
         this.stbStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbStatusPanel,
            this.sbEncodingPanel,
            this.sbTotalCountPanel,
            this.sbFileCountPanel,
            this.sbFilterCountPanel,
            this.sbErrorCountPanel});
         this.stbStatus.Location = new System.Drawing.Point(0, 609);
         this.stbStatus.Name = "stbStatus";
         this.stbStatus.Size = new System.Drawing.Size(795, 22);
         this.stbStatus.TabIndex = 1;
         this.stbStatus.ShowItemToolTips = true;
         // 
         // sbStatusPanel
         // 
         this.sbStatusPanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.sbStatusPanel.Name = "sbStatusPanel";
         this.sbStatusPanel.Size = new System.Drawing.Size(648, 17);
         this.sbStatusPanel.Spring = true;
         this.sbStatusPanel.DoubleClickEnabled = true;
         this.sbStatusPanel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // sbEncodingPanel
         // 
         this.sbEncodingPanel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
         this.sbEncodingPanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.sbEncodingPanel.Name = "sbEncodingPanel";
         this.sbEncodingPanel.Size = new System.Drawing.Size(48, 17);
         this.sbEncodingPanel.Text = string.Empty;
         // 
         // sbTotalCountPanel
         // 
         this.sbTotalCountPanel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
         this.sbTotalCountPanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.sbTotalCountPanel.Name = "sbTotalCountPanel";
         this.sbTotalCountPanel.Size = new System.Drawing.Size(48, 17);
         this.sbTotalCountPanel.Text = "Total: 0";
         // 
         // sbFileCountPanel
         // 
         this.sbFileCountPanel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
         this.sbFileCountPanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.sbFileCountPanel.Name = "sbFileCountPanel";
         this.sbFileCountPanel.Size = new System.Drawing.Size(40, 17);
         this.sbFileCountPanel.Text = "File: 0";
         // 
         // sbFilterCountPanel
         // 
         this.sbFilterCountPanel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
         this.sbFilterCountPanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.sbFilterCountPanel.Name = "sbFilterCountPanel";
         this.sbFilterCountPanel.Size = new System.Drawing.Size(47, 17);
         this.sbFilterCountPanel.Text = "Exclusions: 0";
         this.sbFilterCountPanel.DoubleClickEnabled = true;
         this.sbFilterCountPanel.ToolTipText = "Double click to display exclusions.";
         // 
         // sbErrorCountPanel
         // 
         this.sbErrorCountPanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.sbErrorCountPanel.Name = "sbErrorCountPanel";
         this.sbErrorCountPanel.Size = new System.Drawing.Size(44, 17);
         this.sbErrorCountPanel.Text = "Error: 0";
         this.sbErrorCountPanel.DoubleClickEnabled = true;
         this.sbErrorCountPanel.ToolTipText = "Double click to display errors.";
         // 
         // FileOperationsMenuItem
         // 
         this.FileOperationsMenuItem.Index = 5;
         this.FileOperationsMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.FileCopyMenuItem,
            this.FileDeleteMenuItem});
         this.FileOperationsMenuItem.Text = "File Operations";
         // 
         // FileCopyMenuItem
         // 
         this.FileCopyMenuItem.Index = 0;
         this.FileCopyMenuItem.Text = "Copy";
         this.FileCopyMenuItem.Click += new System.EventHandler(this.FileCopyMenuItem_Click);
         // 
         // FileDeleteMenuItem
         // 
         this.FileDeleteMenuItem.Index = 1;
         this.FileDeleteMenuItem.Text = "Delete";
         this.FileDeleteMenuItem.Click += new System.EventHandler(this.FileDeleteMenuItem_Click);
         // 
         // menuItem6
         // 
         this.menuItem6.Index = 6;
         this.menuItem6.Text = "-";
		   // 
         // ListViewImageList
         // 
         this.ListViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListViewImageList.ImageStream")));
         this.ListViewImageList.TransparentColor = System.Drawing.Color.Transparent;
         this.ListViewImageList.Images.SetKeyName(0, "");
         this.ListViewImageList.Images.SetKeyName(1, "");
         // 
         // frmMain
         // 
         this.AcceptButton = this.btnSearch;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.btnCancel;
         this.ClientSize = new System.Drawing.Size(784, 456);
         this.Controls.Add(this.splitLeftRight);
         this.Controls.Add(this.pnlRightSide);
         this.Controls.Add(this.pnlSearch);
         this.Controls.Add(this.stbStatus);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Menu = this.mnuAll;
         this.Name = "frmMain";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "AstroGrep";
         this.Load += new System.EventHandler(this.frmMain_Load);
         this.pnlSearch.ResumeLayout(false);
         this.pnlSearchOptions.ResumeLayout(false);
         this.PanelOptionsContainer.ResumeLayout(false);
         this.PanelOptionsContainer.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.txtContextLines)).EndInit();
         this.pnlMainSearch.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.picBrowse)).EndInit();
         this.pnlRightSide.ResumeLayout(false);
         this.ResumeLayout(false);

      }
      
      #endregion

      /// <summary>
      /// Dispose form.
      /// </summary>
      /// <param name="disposing">system parameter</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing)
         {
            if (components != null)
            {
               components.Dispose();
            }
         }
         base.Dispose(disposing);
      }
   }
}
