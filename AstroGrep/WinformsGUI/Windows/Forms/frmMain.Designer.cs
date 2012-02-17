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
private System.Windows.Forms.Button btnSearch;
private System.Windows.Forms.Panel pnlSearchOptions;
private System.Windows.Forms.Panel pnlMainSearch;
private System.Windows.Forms.Label lblSearchText;
private System.Windows.Forms.Label lblFileTypes;
private System.Windows.Forms.Label lblSearchPath;
private System.Windows.Forms.Label lblSearchHeading;
private System.Windows.Forms.Splitter splitUpDown;
private System.Windows.Forms.Splitter splitLeftRight;
private System.Windows.Forms.Panel pnlRightSide;
private System.Windows.Forms.StatusBar stbStatus;
private System.Windows.Forms.LinkLabel lnkSearchOptions;
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
private System.Windows.Forms.MenuItem mnuSaveResults;
private System.Windows.Forms.MenuItem mnuPrintResults;
private System.Windows.Forms.MenuItem mnuExit;
private System.Windows.Forms.MenuItem mnuEdit;
private System.Windows.Forms.MenuItem mnuTools;
private System.Windows.Forms.MenuItem mnuOptions;
private System.Windows.Forms.MenuItem mnuHelp;
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
private Label label2;
private DateTimePicker dateModEnd;
private Label label1;
private DateTimePicker dateModBegin;
private Label label4;
private TextBox txtMinSize;
private Label label3;
private TextBox txtMaxSize;
private System.Windows.Forms.ComboBox cboMinSizeType;
private System.Windows.Forms.ComboBox cboMaxSizeType;
private System.Windows.Forms.CheckBox chkSkipSystem;
private System.Windows.Forms.CheckBox chkSkipHidden;

	    private void InitializeComponent()
		{
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
         this.pnlSearch = new System.Windows.Forms.Panel();
         this.pnlSearchOptions = new System.Windows.Forms.Panel();
         this.PanelOptionsContainer = new System.Windows.Forms.Panel();
         this.txtMaxSize = new System.Windows.Forms.TextBox();
         this.label4 = new System.Windows.Forms.Label();
         this.txtMinSize = new System.Windows.Forms.TextBox();
         this.label3 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.dateModEnd = new System.Windows.Forms.DateTimePicker();
         this.label1 = new System.Windows.Forms.Label();
         this.dateModBegin = new System.Windows.Forms.DateTimePicker();
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
         this.pnlMainSearch = new System.Windows.Forms.Panel();
         this.picBrowse = new AstroGrep.Windows.Controls.PictureButton();
         this.btnSearch = new System.Windows.Forms.Button();
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
		 this.CopyNameMenuItem = new System.Windows.Forms.MenuItem();
		 this.CopyLocatedInMenuItem = new System.Windows.Forms.MenuItem();
		 this.CopyLocatedInAndNameMenuItem = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.DeleteMenuItem = new System.Windows.Forms.MenuItem();
         this.splitLeftRight = new System.Windows.Forms.Splitter();
         this.mnuAll = new System.Windows.Forms.MainMenu(this.components);
         this.mnuFile = new System.Windows.Forms.MenuItem();
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
         this.mnuAbout = new System.Windows.Forms.MenuItem();
         this.stbStatus = new System.Windows.Forms.StatusBar();
         this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
         this.ListViewImageList = new System.Windows.Forms.ImageList(this.components);
         this.chkSkipHidden = new System.Windows.Forms.CheckBox();
         this.chkSkipSystem = new System.Windows.Forms.CheckBox();
		 this.cboMinSizeType = new System.Windows.Forms.ComboBox();
		 this.cboMaxSizeType = new System.Windows.Forms.ComboBox();
         this.pnlSearch.SuspendLayout();
         this.pnlSearchOptions.SuspendLayout();
         this.PanelOptionsContainer.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.txtContextLines)).BeginInit();
         this.pnlMainSearch.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.picBrowse)).BeginInit();
         this.pnlRightSide.SuspendLayout();
         this.SuspendLayout();
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
         this.pnlSearchOptions.Size = new System.Drawing.Size(200, 453);
         this.pnlSearchOptions.TabIndex = 1;
         // 
         // PanelOptionsContainer
         // 
         this.PanelOptionsContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.PanelOptionsContainer.Controls.Add(this.chkSkipSystem);
         this.PanelOptionsContainer.Controls.Add(this.chkSkipHidden);
		 this.PanelOptionsContainer.Controls.Add(this.cboMaxSizeType);
         this.PanelOptionsContainer.Controls.Add(this.txtMaxSize);
         this.PanelOptionsContainer.Controls.Add(this.label4);
		 this.PanelOptionsContainer.Controls.Add(this.cboMinSizeType);
         this.PanelOptionsContainer.Controls.Add(this.txtMinSize);
         this.PanelOptionsContainer.Controls.Add(this.label3);
         this.PanelOptionsContainer.Controls.Add(this.label2);
         this.PanelOptionsContainer.Controls.Add(this.dateModEnd);
         this.PanelOptionsContainer.Controls.Add(this.label1);
         this.PanelOptionsContainer.Controls.Add(this.dateModBegin);
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
         this.PanelOptionsContainer.Size = new System.Drawing.Size(200, 433);
         this.PanelOptionsContainer.TabIndex = 1;
		 // 
         // cboMaxSizeType
         // 
		 this.cboMaxSizeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		 this.cboMaxSizeType.Items.AddRange(new object[] {
            "byte",
            "KB",
            "MB",
            "GB"});
         this.cboMaxSizeType.Location = new System.Drawing.Point(140, 355);
         this.cboMaxSizeType.Name = "cboMaxSizeType";
		 this.cboMaxSizeType.SelectedIndex = 0;
         this.cboMaxSizeType.Size = new System.Drawing.Size(75, 18);
         this.cboMaxSizeType.TabIndex = 23;
         // 
         // txtMaxSize
         // 
         this.txtMaxSize.Location = new System.Drawing.Point(7, 355);
         this.txtMaxSize.Name = "txtMaxSize";
         this.txtMaxSize.Size = new System.Drawing.Size(129, 20);
         this.txtMaxSize.TabIndex = 22;
         // 
         // label4
         // 
         this.label4.AutoSize = true;
		 this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.label4.Location = new System.Drawing.Point(4, 339);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(53, 13);
         this.label4.TabIndex = 21;
         this.label4.Text = "Max Size:";
		 // 
         // cboMinSizeType
         // 
		 this.cboMinSizeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cboMinSizeType.Location = new System.Drawing.Point(140, 316);
		 this.cboMinSizeType.Items.AddRange(new object[] {
            "byte",
            "KB",
            "MB",
            "GB"});
         this.cboMinSizeType.Name = "cboMinSizeType";
		 this.cboMinSizeType.SelectedIndex = 0;
         this.cboMinSizeType.Size = new System.Drawing.Size(75, 18);
         this.cboMinSizeType.TabIndex = 20;
         // 
         // txtMinSize
         // 
         this.txtMinSize.Location = new System.Drawing.Point(7, 316);
         this.txtMinSize.Name = "txtMinSize";
         this.txtMinSize.Size = new System.Drawing.Size(129, 20);
         this.txtMinSize.TabIndex = 19;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
		 this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.label3.Location = new System.Drawing.Point(4, 300);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(50, 13);
         this.label3.TabIndex = 18;
         this.label3.Text = "Min Size:";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
		 this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.label2.Location = new System.Drawing.Point(4, 251);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(84, 13);
         this.label2.TabIndex = 17;
         this.label2.Text = "Modified Before:";
         // 
         // dateModEnd
         // 
         this.dateModEnd.Location = new System.Drawing.Point(7, 267);
         this.dateModEnd.Name = "dateModEnd";
         this.dateModEnd.Size = new System.Drawing.Size(208, 20);
         this.dateModEnd.TabIndex = 16;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
		 this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.label1.Location = new System.Drawing.Point(4, 207);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(75, 13);
         this.label1.TabIndex = 15;
         this.label1.Text = "Modified After:";
         // 
         // dateModBegin
         // 
         this.dateModBegin.Location = new System.Drawing.Point(7, 223);
         this.dateModBegin.Name = "dateModBegin";
         this.dateModBegin.Size = new System.Drawing.Size(208, 20);
         this.dateModBegin.TabIndex = 14;
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
         this.btnSearch.BackColor = System.Drawing.SystemColors.Control;
         this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnSearch.Location = new System.Drawing.Point(8, 160);
         this.btnSearch.Name = "btnSearch";
         this.btnSearch.Size = new System.Drawing.Size(75, 23);
         this.btnSearch.TabIndex = 0;
         this.btnSearch.Text = "&Search";
         this.btnSearch.UseVisualStyleBackColor = false;
         this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
         // 
         // btnCancel
         // 
         this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
         this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnCancel.Enabled = false;
         this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnCancel.Location = new System.Drawing.Point(116, 160);
         this.btnCancel.Name = "btnCancel";
         this.btnCancel.Size = new System.Drawing.Size(75, 23);
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
         this.lblSearchHeading.Size = new System.Drawing.Size(198, 16);
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
         // 
         // fileLstMnu
         // 
         this.fileLstMnu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.CopyMenuItem,
			this.CopyNameMenuItem,
		    this.CopyLocatedInMenuItem,
		    this.CopyLocatedInAndNameMenuItem,
            this.menuItem4,
            this.OpenMenuItem,
            this.OpenFolderMenuItem,
            this.menuItem2,
            this.DeleteMenuItem});
         // 
         // CopyMenuItem
         // 
         this.CopyMenuItem.Index = 0;
         this.CopyMenuItem.Text = "Copy";
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
         this.OpenMenuItem.Index = 5;
         this.OpenMenuItem.Text = "Open File";
         this.OpenMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
         // 
         // OpenFolderMenuItem
         // 
         this.OpenFolderMenuItem.Index = 6;
         this.OpenFolderMenuItem.Text = "Open Directory";
         this.OpenFolderMenuItem.Click += new System.EventHandler(this.OpenFolderMenuItem_Click);
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 7;
         this.menuItem2.Text = "-";
         // 
         // DeleteMenuItem
         // 
         this.DeleteMenuItem.Index = 8;
         this.DeleteMenuItem.Text = "Delete Item";
         this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
         // 
         // splitLeftRight
         // 
         this.splitLeftRight.Location = new System.Drawing.Point(240, 0);
         this.splitLeftRight.MinExtra = 100;
         this.splitLeftRight.MinSize = 280;
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
            this.mnuTools,
            this.mnuHelp});
         // 
         // mnuFile
         // 
         this.mnuFile.Index = 0;
         this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
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
         this.mnuBrowse.Index = 0;
         this.mnuBrowse.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
         this.mnuBrowse.Text = "Select Sea&rch Path...";
         this.mnuBrowse.Click += new System.EventHandler(this.mnuBrowse_Click);
         // 
         // mnuFileSep2
         // 
         this.mnuFileSep2.Index = 1;
         this.mnuFileSep2.Text = "-";
         // 
         // mnuSaveResults
         // 
         this.mnuSaveResults.Index = 2;
         this.mnuSaveResults.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
         this.mnuSaveResults.Text = "&Save Results";
         this.mnuSaveResults.Click += new System.EventHandler(this.mnuSaveResults_Click);
         // 
         // mnuPrintResults
         // 
         this.mnuPrintResults.Index = 3;
         this.mnuPrintResults.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
         this.mnuPrintResults.Text = "&Print Results";
         this.mnuPrintResults.Click += new System.EventHandler(this.mnuPrintResults_Click);
         // 
         // mnuFileSep
         // 
         this.mnuFileSep.Index = 4;
         this.mnuFileSep.Text = "-";
         // 
         // mnuExit
         // 
         this.mnuExit.Index = 5;
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
         // mnuTools
         // 
         this.mnuTools.Index = 2;
         this.mnuTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuClearMRU,
            this.mnuToolsSep,
            this.mnuSaveSearchSettings,
            this.mnuOptions});
         this.mnuTools.Text = "&Tools";
         // 
         // mnuClearMRU
         // 
         this.mnuClearMRU.Index = 0;
         this.mnuClearMRU.Text = "&Clear Most Recently Used Lists";
         this.mnuClearMRU.Click += new System.EventHandler(this.mnuClearMRU_Click);
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
         this.mnuHelp.Index = 3;
         this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuAbout});
         this.mnuHelp.Text = "&Help";
         // 
         // mnuAbout
         // 
         this.mnuAbout.Index = 0;
         this.mnuAbout.Text = "&About...";
         this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
         // 
         // stbStatus
         // 
         this.stbStatus.Location = new System.Drawing.Point(0, 430);
         this.stbStatus.Name = "stbStatus";
         this.stbStatus.Size = new System.Drawing.Size(784, 26);
         this.stbStatus.TabIndex = 3;
         // 
         // ListViewImageList
         // 
         this.ListViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListViewImageList.ImageStream")));
         this.ListViewImageList.TransparentColor = System.Drawing.Color.Transparent;
         this.ListViewImageList.Images.SetKeyName(0, "");
         this.ListViewImageList.Images.SetKeyName(1, "");
         // 
         // chkSkipHidden
         // 
         this.chkSkipHidden.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.chkSkipHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkSkipHidden.Location = new System.Drawing.Point(7, 381);
         this.chkSkipHidden.Name = "chkSkipHidden";
         this.chkSkipHidden.Size = new System.Drawing.Size(178, 16);
         this.chkSkipHidden.TabIndex = 22;
         this.chkSkipHidden.Text = "Skip Hidden Files/Directories";
         this.toolTip1.SetToolTip(this.chkSkipHidden, "Ignore hidden files/directories");
         // 
         // chkSkipSystem
         // 
         this.chkSkipSystem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.chkSkipSystem.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkSkipSystem.Location = new System.Drawing.Point(7, 403);
         this.chkSkipSystem.Name = "chkSkipSystem";
         this.chkSkipSystem.Size = new System.Drawing.Size(178, 16);
         this.chkSkipSystem.TabIndex = 23;
         this.chkSkipSystem.Text = "Skip System Files/Directories";
         this.toolTip1.SetToolTip(this.chkSkipSystem, "Ignore system files/directories");
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

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
	}
}
