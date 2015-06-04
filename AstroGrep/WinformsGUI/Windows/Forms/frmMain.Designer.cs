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
      private AstroGrep.Windows.Controls.TextEditorEx txtHits;
      private System.Windows.Forms.Integration.ElementHost textElementHost;
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
      private System.Windows.Forms.Label lblSearchOptions;
      private System.Windows.Forms.LinkLabel lnkExclusions;
      private System.Windows.Forms.ToolTip toolTip1;
      private System.Windows.Forms.Panel PanelOptionsContainer;
      private System.Windows.Forms.CheckBox chkNegation;
      private System.Windows.Forms.CheckBox chkCaseSensitive;
      private System.Windows.Forms.CheckBox chkRecurse;
      private System.Windows.Forms.CheckBox chkFileNamesOnly;
      private System.Windows.Forms.CheckBox chkRegularExpressions;
      private System.Windows.Forms.CheckBox chkWholeWordOnly;
      private System.Windows.Forms.NumericUpDown txtContextLines;
      private System.Windows.Forms.Label lblContextLines;
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
      private System.Windows.Forms.MenuItem FileOperationsMenuItem;
      private System.Windows.Forms.MenuItem FileCopyMenuItem;
      private System.Windows.Forms.MenuItem FileDeleteMenuItem;
      private System.Windows.Forms.MenuItem menuItem6;
      private System.Windows.Forms.MenuItem MenuBtnSearch;
      private System.Windows.Forms.ImageList ListViewImageList;
      private AstroGrep.Windows.Controls.PictureButton picBrowse;      
      private System.Windows.Forms.ContextMenu fileLstMnu;      
      private System.Windows.Forms.ContextMenu ctxMenuBtnSearch;
      private System.Windows.Forms.CheckBox chkAllResultsAfterSearch;      

      private MenuStrip MainMenu;

      private ToolStripMenuItem FileMenu;
      private ToolStripMenuItem NewWindowMenuItem;
      private ToolStripSeparator FileSep1Separator;
      private ToolStripMenuItem SelectPathMenuItem;
      private ToolStripSeparator FileSep2Separator;
      private ToolStripMenuItem SaveResultsMenuItem;
      private ToolStripMenuItem PrintResultsMenuItem;
      private ToolStripSeparator FileSep3Separator;
      private ToolStripMenuItem ExitMenuItem;

      private ToolStripMenuItem EditMenu;
      private ToolStripMenuItem SelectAllMenuItem;
      private ToolStripMenuItem OpenSelectedMenuItem;

      private ToolStripMenuItem ViewMenu;
      private ToolStripMenuItem StatusMessageMenuItem;
      private ToolStripMenuItem ExclusionMessageMenuItem;
      private ToolStripMenuItem ErrorMessageMenuItem;
      private ToolStripMenuItem AllMessageMenuItem;
      private ToolStripSeparator ViewSep1Separator;
      private ToolStripMenuItem ZoomMenuItem;
      private ToolStripMenuItem ZoomInMenuItem;
      private ToolStripMenuItem ZoomOutMenuItem;
      private ToolStripMenuItem ZoomRestoreMenuItem;
      private ToolStripMenuItem LineNumbersMenuItem;
      private ToolStripMenuItem WordWrapMenuItem;
      private ToolStripMenuItem RemoveWhiteSpaceMenuItem;
      private ToolStripMenuItem EntireFileMenuItem;
      private ToolStripSeparator ViewSep2Separator;
      private ToolStripMenuItem AllResultsMenuItem;

      private ToolStripMenuItem ToolsMenu;
      private ToolStripMenuItem ClearMRUMenuItem;
      private ToolStripMenuItem ClearMRUPathsMenuItem;
      private ToolStripMenuItem ClearMRUTypesMenuItem;
      private ToolStripMenuItem ClearMRUTextsMenuItem;
      private ToolStripSeparator ClearMRUSep1Separator;
      private ToolStripMenuItem ClearMRUAllMenuItem;
      private ToolStripSeparator ToolsSep1Separator;
      private ToolStripMenuItem SaveSearchOptionsMenuItem;
      private ToolStripMenuItem OptionsMenuItem;

      private ToolStripMenuItem HelpMenu;
      private ToolStripMenuItem ViewHelpMenuItem;
      private ToolStripMenuItem ViewRegExHelpMenuItem;
      private ToolStripMenuItem LogFileMenuItem;
      private ToolStripSeparator HelpSep1Separator;
      private ToolStripMenuItem CheckForUpdateMenuItem;
      private ToolStripSeparator HelpSep2Separator;
      private ToolStripMenuItem AboutMenuItem;

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
         this.chkFileNamesOnly = new System.Windows.Forms.CheckBox();
         this.chkRecurse = new System.Windows.Forms.CheckBox();
         this.chkCaseSensitive = new System.Windows.Forms.CheckBox();
         this.lblSearchOptions = new System.Windows.Forms.Label();
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
         this.txtHits = new AstroGrep.Windows.Controls.TextEditorEx();
         this.textElementHost = new System.Windows.Forms.Integration.ElementHost();
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
         this.chkAllResultsAfterSearch = new CheckBox();

         this.MainMenu = new MenuStrip();

         this.FileMenu = new ToolStripMenuItem();
         this.NewWindowMenuItem = new ToolStripMenuItem();
         this.FileSep1Separator = new ToolStripSeparator();
         this.SelectPathMenuItem = new ToolStripMenuItem();
         this.FileSep2Separator = new ToolStripSeparator();
         this.SaveResultsMenuItem = new ToolStripMenuItem();
         this.PrintResultsMenuItem = new ToolStripMenuItem();
         this.FileSep3Separator = new ToolStripSeparator();
         this.ExitMenuItem = new ToolStripMenuItem();

         this.EditMenu = new ToolStripMenuItem();
         this.SelectAllMenuItem = new ToolStripMenuItem();
         this.OpenSelectedMenuItem = new ToolStripMenuItem();

         this.ViewMenu = new ToolStripMenuItem();
         this.StatusMessageMenuItem = new ToolStripMenuItem();
         this.ExclusionMessageMenuItem = new ToolStripMenuItem();
         this.ErrorMessageMenuItem = new ToolStripMenuItem();
         this.AllMessageMenuItem = new ToolStripMenuItem();
         this.ViewSep1Separator = new ToolStripSeparator();
         this.ZoomMenuItem = new ToolStripMenuItem();
         this.ZoomInMenuItem = new ToolStripMenuItem();
         this.ZoomOutMenuItem = new ToolStripMenuItem();
         this.ZoomRestoreMenuItem = new ToolStripMenuItem();
         this.LineNumbersMenuItem = new ToolStripMenuItem();
         this.WordWrapMenuItem = new ToolStripMenuItem();
         this.RemoveWhiteSpaceMenuItem = new ToolStripMenuItem();
         this.EntireFileMenuItem = new ToolStripMenuItem();
         this.ViewSep2Separator = new ToolStripSeparator();
         this.AllResultsMenuItem = new ToolStripMenuItem();

         this.ToolsMenu = new ToolStripMenuItem();
         this.ClearMRUMenuItem = new ToolStripMenuItem();
         this.ClearMRUPathsMenuItem = new ToolStripMenuItem();
         this.ClearMRUTypesMenuItem = new ToolStripMenuItem();
         this.ClearMRUTextsMenuItem = new ToolStripMenuItem();
         this.ClearMRUSep1Separator = new ToolStripSeparator();
         this.ClearMRUAllMenuItem = new ToolStripMenuItem();
         this.ToolsSep1Separator = new ToolStripSeparator();
         this.SaveSearchOptionsMenuItem = new ToolStripMenuItem();
         this.OptionsMenuItem = new ToolStripMenuItem();

         this.HelpMenu = new ToolStripMenuItem();
         this.ViewHelpMenuItem = new ToolStripMenuItem();
         this.ViewRegExHelpMenuItem = new ToolStripMenuItem();
         this.LogFileMenuItem = new ToolStripMenuItem();
         this.HelpSep1Separator = new ToolStripSeparator();
         this.CheckForUpdateMenuItem = new ToolStripMenuItem();
         this.HelpSep2Separator = new ToolStripSeparator();
         this.AboutMenuItem = new ToolStripMenuItem();         

         this.MainMenu.SuspendLayout();
		   this.pnlSearch.SuspendLayout();
         this.pnlSearchOptions.SuspendLayout();
         this.txtHits.BeginInit();

         this.PanelOptionsContainer.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.txtContextLines)).BeginInit();
         this.pnlMainSearch.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.picBrowse)).BeginInit();
         this.pnlRightSide.SuspendLayout();
         this.SuspendLayout();

         this.MenuBtnSearch.Text = "Search in results";
         this.MenuBtnSearch.Click += mnuSearchInResults_Click;
         this.ctxMenuBtnSearch.MenuItems.Add(this.MenuBtnSearch);

         this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.EditMenu,
            this.ViewMenu,
            this.ToolsMenu,
            this.HelpMenu});
         this.MainMenu.Location = new System.Drawing.Point(0, 0);
         this.MainMenu.Name = "MainMenu";
         this.MainMenu.Size = new System.Drawing.Size(1036, 24);
         this.MainMenu.TabIndex = 0;
         this.MainMenu.Text = "MainMenu";

         this.FileMenu.Name = "FileMenu";
         this.FileMenu.Text = "File";
         this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            NewWindowMenuItem,
            FileSep1Separator,
            SelectPathMenuItem,
            FileSep2Separator,
            SaveResultsMenuItem,
            PrintResultsMenuItem,
            FileSep3Separator,
            ExitMenuItem});
         this.FileMenu.DropDownOpening += FileMenu_DropDownOpening;

         this.NewWindowMenuItem.Click += NewWindowMenuItem_Click;
         this.NewWindowMenuItem.ShortcutKeys = Keys.Control | Keys.N;
         this.NewWindowMenuItem.Text = "&New Window";

         this.SelectPathMenuItem.ShortcutKeys = Keys.Control | Keys.O;
         this.SelectPathMenuItem.Text = "Select Sea&rch Path...";
         this.SelectPathMenuItem.Click += SelectPathMenuItem_Click;

         this.SaveResultsMenuItem.ShortcutKeys = Keys.Control | Keys.S;
         this.SaveResultsMenuItem.Text = "&Save Results";
         this.SaveResultsMenuItem.Click += SaveResultsMenuItem_Click;

         this.PrintResultsMenuItem.ShortcutKeys = Keys.Control | Keys.P;
         this.PrintResultsMenuItem.Text = "&Print Results";
         this.PrintResultsMenuItem.Click += PrintResultsMenuItem_Click;

         this.ExitMenuItem.Click += ExitMenuItem_Click;
         this.ExitMenuItem.ShortcutKeys = Keys.Control | Keys.Q;
         this.ExitMenuItem.Text = "E&xit";

         this.EditMenu.Text = "Edit";
         this.EditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            SelectAllMenuItem,
            OpenSelectedMenuItem});
         this.EditMenu.DropDownOpening += EditMenu_DropDownOpening;

         this.SelectAllMenuItem.Text = "&Select All Files";
         this.SelectAllMenuItem.Click += SelectAllMenuItem_Click;

         this.OpenSelectedMenuItem.Text = "&Open Selected Files";
         this.OpenSelectedMenuItem.Click += OpenSelectedMenuItem_Click;
         
         this.ViewMenu.Name = "ViewMenu";
         this.ViewMenu.Text = "View";
         this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            StatusMessageMenuItem,
            ExclusionMessageMenuItem,
            ErrorMessageMenuItem,
            AllMessageMenuItem,
            ViewSep1Separator,
            ZoomMenuItem,
            LineNumbersMenuItem,
            WordWrapMenuItem,
            RemoveWhiteSpaceMenuItem,
            EntireFileMenuItem,
            ViewSep2Separator,
            AllResultsMenuItem});
         this.ViewMenu.DropDownOpening += ViewMenu_DropDownOpening;

         this.ZoomMenuItem.Name = "ZoomMenuItem";
         this.ZoomMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            ZoomInMenuItem,
            ZoomOutMenuItem,
            ZoomRestoreMenuItem});
         this.ZoomMenuItem.Text = "Zoom";

         this.StatusMessageMenuItem.Text = "&Status Messages";
         this.StatusMessageMenuItem.Click += StatusMessageMenuItem_Click;

         this.ExclusionMessageMenuItem.Text = "&Exclusion Messages";
         this.ExclusionMessageMenuItem.Click += ExclusionMessageMenuItem_Click;

         this.ErrorMessageMenuItem.Text = "E&rror Messages";
         this.ErrorMessageMenuItem.Click += ErrorMessageMenuItem_Click;

         this.AllMessageMenuItem.Text = "&All Messages";
         this.AllMessageMenuItem.Click += AllMessageMenuItem_Click;

         this.LineNumbersMenuItem.Text = "Line Numbers";
         this.LineNumbersMenuItem.Click += LineNumbersMenuItem_Click;

         this.WordWrapMenuItem.Text = "Word Wrap";
         this.WordWrapMenuItem.Click += WordWrapMenuItem_Click;

         this.RemoveWhiteSpaceMenuItem.Text = "Remove Leading White Space";
         this.RemoveWhiteSpaceMenuItem.Click += RemoveWhiteSpaceMenuItem_Click;

         this.EntireFileMenuItem.Text = "Entire File";
         this.EntireFileMenuItem.Click += EntireFileMenuItem_Click;

         this.AllResultsMenuItem.Text = "All Results";
         this.AllResultsMenuItem.Click += AllResultsMenuItem_Click;

         this.ZoomInMenuItem.Text = "Zoom In (Ctrl+Mouse Wheel Up)";
         this.ZoomInMenuItem.Click += ZoomInMenuItem_Click;
         this.ZoomInMenuItem.ShortcutKeys = Keys.Control | Keys.Add;
         this.ZoomInMenuItem.ShortcutKeyDisplayString = "Ctrl+Num +";

         this.ZoomOutMenuItem.Text = "Zoom Out (Ctrl+Mouse Wheel Down)";
         this.ZoomOutMenuItem.Click += ZoomOutMenuItem_Click;
         this.ZoomOutMenuItem.ShortcutKeys = Keys.Control | Keys.Subtract;
         this.ZoomOutMenuItem.ShortcutKeyDisplayString = "Ctrl+Num -";

         this.ZoomRestoreMenuItem.Text = "Restore Default Zoom";
         this.ZoomRestoreMenuItem.Click += ZoomRestoreMenuItem_Click;
         this.ZoomRestoreMenuItem.ShortcutKeys = Keys.Control | Keys.Divide;
         this.ZoomRestoreMenuItem.ShortcutKeyDisplayString = "Ctrl+Num /";

         this.ToolsMenu.Name = "ToolsMenu";
         this.ToolsMenu.Text = "Tools";
         this.ToolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            ClearMRUMenuItem,
            ToolsSep1Separator,
            SaveSearchOptionsMenuItem,
            OptionsMenuItem});

         this.ClearMRUMenuItem.Text = "&Clear Most Recently Used Lists";
         this.ClearMRUMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            ClearMRUPathsMenuItem,
            ClearMRUTypesMenuItem,
            ClearMRUTextsMenuItem,
            ClearMRUSep1Separator,
            ClearMRUAllMenuItem});

         this.ClearMRUPathsMenuItem.Text = "&Search Paths";
         this.ClearMRUPathsMenuItem.Click += ClearMRUPathsMenuItem_Click;

         this.ClearMRUTypesMenuItem.Text = "&File Types";
         this.ClearMRUTypesMenuItem.Click += ClearMRUTypesMenuItem_Click;

         this.ClearMRUTextsMenuItem.Text = "Search &Text";
         this.ClearMRUTextsMenuItem.Click += ClearMRUTextsMenuItem_Click;

         this.ClearMRUAllMenuItem.Text = "&All";
         this.ClearMRUAllMenuItem.Click += ClearMRUAllMenuItem_Click;

         this.SaveSearchOptionsMenuItem.Text = "&Save Search Options";
         this.SaveSearchOptionsMenuItem.Click += SaveSearchOptionsMenuItem_Click;

         this.OptionsMenuItem.ShortcutKeys = Keys.F9;
         this.OptionsMenuItem.Text = "&Options...";
         this.OptionsMenuItem.Click += OptionsMenuItem_Click;

         this.HelpMenu.Name = "HelpMenu";
         this.HelpMenu.Text = "&Help";
         this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            ViewHelpMenuItem,
            ViewRegExHelpMenuItem,
            LogFileMenuItem,
            HelpSep1Separator,
            CheckForUpdateMenuItem,
            HelpSep2Separator,
            AboutMenuItem});

         this.ViewHelpMenuItem.Text = "&View Help";
         this.ViewHelpMenuItem.ShortcutKeys = Keys.F1;
         this.ViewHelpMenuItem.Click += ViewHelpMenuItem_Click;

         this.ViewRegExHelpMenuItem.Text = "&Regular Expressions";
         this.ViewRegExHelpMenuItem.Click += ViewRegExHelpMenuItem_Click;

         this.LogFileMenuItem.Text = "&Log File";
         this.LogFileMenuItem.Click += LogFileMenuItem_Click;

         this.CheckForUpdateMenuItem.Text = "&Check for Updates...";
         this.CheckForUpdateMenuItem.Click += CheckForUpdateMenuItem_Click;

         this.AboutMenuItem.Text = "&About AstroGrep";
         this.AboutMenuItem.Click += AboutMenuItem_Click;

         // 
         // pnlSearch
         // 
         this.pnlSearch.AutoScroll = true;
         this.pnlSearch.BackColor = System.Drawing.SystemColors.Window;
         this.pnlSearch.Controls.Add(this.pnlSearchOptions);
         this.pnlSearch.Controls.Add(this.pnlMainSearch);
         this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Left;
         this.pnlSearch.Location = new System.Drawing.Point(0, 0);
         this.pnlSearch.Name = "pnlSearch";
         this.pnlSearch.Size = new System.Drawing.Size(240, 430);
         this.pnlSearch.TabIndex = 0;
         this.pnlSearch.SizeChanged += pnlSearch_SizeChanged;
         // 
         // pnlSearchOptions
         // 
         this.pnlSearchOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.pnlSearchOptions.Controls.Add(this.PanelOptionsContainer);
         this.pnlSearchOptions.Controls.Add(this.lblSearchOptions);
         this.pnlSearchOptions.Location = new System.Drawing.Point(16, 209);
         this.pnlSearchOptions.Name = "pnlSearchOptions";
         this.pnlSearchOptions.Size = new System.Drawing.Size(200, 270);
         this.pnlSearchOptions.Padding = new Padding(0, 15, 0, 0);
         this.pnlSearchOptions.TabIndex = 1;
         // 
         // PanelOptionsContainer
         // 
         this.PanelOptionsContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.PanelOptionsContainer.Controls.Add(this.lnkExclusions);
         this.PanelOptionsContainer.Controls.Add(this.lblContextLines);
         this.PanelOptionsContainer.Controls.Add(this.chkAllResultsAfterSearch);
         this.PanelOptionsContainer.Controls.Add(this.txtContextLines);
         this.PanelOptionsContainer.Controls.Add(this.chkWholeWordOnly);
         this.PanelOptionsContainer.Controls.Add(this.chkRegularExpressions);
         this.PanelOptionsContainer.Controls.Add(this.chkNegation);
         this.PanelOptionsContainer.Controls.Add(this.chkFileNamesOnly);
         this.PanelOptionsContainer.Controls.Add(this.chkRecurse);
         this.PanelOptionsContainer.Controls.Add(this.chkCaseSensitive);
         this.PanelOptionsContainer.Location = new System.Drawing.Point(0, 40);
         this.PanelOptionsContainer.Name = "PanelOptionsContainer";
         this.PanelOptionsContainer.Size = new System.Drawing.Size(200, 240);
         this.PanelOptionsContainer.TabIndex = 1;
         // 
         // lblContextLines
         // 
         this.lblContextLines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lblContextLines.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblContextLines.Location = new System.Drawing.Point(56, 177);
         this.lblContextLines.Name = "lblContextLines";
         this.lblContextLines.Size = new System.Drawing.Size(127, 20);
         this.lblContextLines.TabIndex = 8;
         this.lblContextLines.Text = "Context Lines";
         this.lblContextLines.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.toolTip1.SetToolTip(this.lblContextLines, "Show lines above and below the word matched");
         // 
         // chkAllResultsAfterSearch
         // 
         this.chkAllResultsAfterSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.chkAllResultsAfterSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkAllResultsAfterSearch.Location = new System.Drawing.Point(7, 152);
         this.chkAllResultsAfterSearch.Name = "chkAllResultsAfterSearch";
         this.chkAllResultsAfterSearch.Size = new System.Drawing.Size(178, 16);
         this.chkAllResultsAfterSearch.AutoSize = true;
         this.chkAllResultsAfterSearch.TabIndex = 8;
         this.chkAllResultsAfterSearch.Text = "&Show all results after search";
         this.toolTip1.SetToolTip(this.chkAllResultsAfterSearch, "Shows all the results together in the preview after a search.");
         // 
         // txtContextLines
         // 
         this.txtContextLines.Location = new System.Drawing.Point(7, 177);
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
         this.chkWholeWordOnly.AutoSize = true;
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
         this.chkRegularExpressions.AutoSize = true;
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
         this.chkNegation.AutoSize = true;
         this.chkNegation.TabIndex = 11;
         this.chkNegation.Text = "&Negation";
         this.chkNegation.CheckedChanged += chkNegation_CheckedChanged;
         this.toolTip1.SetToolTip(this.chkNegation, "Find the files without the Search Text in them");
         // 
         // chkFileNamesOnly
         // 
         this.chkFileNamesOnly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.chkFileNamesOnly.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkFileNamesOnly.Location = new System.Drawing.Point(7, 104);
         this.chkFileNamesOnly.Name = "chkFileNamesOnly";
         this.chkFileNamesOnly.Size = new System.Drawing.Size(178, 16);
         this.chkFileNamesOnly.AutoSize = true;
         this.chkFileNamesOnly.TabIndex = 10;
         this.chkFileNamesOnly.Text = "Show File Names &Only";
         this.chkFileNamesOnly.CheckedChanged += chkFileNamesOnly_CheckedChanged;
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
         this.chkRecurse.AutoSize = true;
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
         this.chkCaseSensitive.AutoSize = true;
         this.chkCaseSensitive.TabIndex = 7;
         this.chkCaseSensitive.Text = "&Case Sensitive";
         this.toolTip1.SetToolTip(this.chkCaseSensitive, "Match upper and lower case letters exactly");
         // 
         // lblSearchOptions
         // 
         this.lblSearchOptions.Dock = System.Windows.Forms.DockStyle.Top;
         this.lblSearchOptions.Location = new System.Drawing.Point(0, 0);
         this.lblSearchOptions.Name = "lblSearchOptions";
         this.lblSearchOptions.Size = new System.Drawing.Size(200, 23);
         this.lblSearchOptions.TabIndex = 5;
         this.lblSearchOptions.Text = "Search Options";
         this.lblSearchOptions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.lblSearchOptions.BackColor = SystemColors.Window;
         this.lblSearchOptions.ForeColor = AstroGrep.Common.ProductInformation.ApplicationColor;
         this.lblSearchOptions.Padding = new Padding(0, 0, 0, 2);
         this.lblSearchOptions.Paint += lblSearchOptions_Paint;
         // 
         // lnkExclusions
         // 
         this.lnkExclusions.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
         this.lnkExclusions.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lnkExclusions.LinkColor = System.Drawing.SystemColors.HotTrack;
         this.lnkExclusions.Location = new System.Drawing.Point(5, 207);
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
         this.pnlMainSearch.Size = new System.Drawing.Size(200, 200);
         this.pnlMainSearch.TabIndex = 0;
         // 
         // picBrowse
         // 
         this.picBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.picBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
         this.picBrowse.Image = ((System.Drawing.Image)(resources.GetObject("picBrowse.Image")));
         this.picBrowse.Location = new System.Drawing.Point(175, 48);
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
         this.btnSearch.Location = new System.Drawing.Point(7, 172);
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
         this.btnCancel.Location = new System.Drawing.Point(111, 172);
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
         this.cboFilePath.Location = new System.Drawing.Point(8, 46);
         this.cboFilePath.Name = "cboFilePath";
         this.cboFilePath.Size = new System.Drawing.Size(154, 21);
         this.cboFilePath.TabIndex = 1;
         this.cboFilePath.DropDown += cboFilePath_DropDown;
         this.cboFilePath.DragDrop += cboFilePath_DragDrop;
         this.cboFilePath.DragEnter += cboFilePath_DragEnter;
         this.cboFilePath.AllowDrop = true;
         // 
         // cboFileName
         // 
         this.cboFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.cboFileName.Location = new System.Drawing.Point(8, 94);
         this.cboFileName.Name = "cboFileName";
         this.cboFileName.Size = new System.Drawing.Size(180, 21);
         this.cboFileName.TabIndex = 2;
         this.cboFileName.DropDown += cboFileName_DropDown;
         // 
         // cboSearchForText
         // 
         this.cboSearchForText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.cboSearchForText.Location = new System.Drawing.Point(8, 140);
         this.cboSearchForText.Name = "cboSearchForText";
         this.cboSearchForText.Size = new System.Drawing.Size(180, 21);
         this.cboSearchForText.TabIndex = 3;
         this.cboSearchForText.DropDown += cboSearchForText_DropDown;
         // 
         // lblSearchText
         // 
         this.lblSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lblSearchText.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblSearchText.Location = new System.Drawing.Point(8, 124);
         this.lblSearchText.Name = "lblSearchText";
         this.lblSearchText.Size = new System.Drawing.Size(163, 16);
         this.lblSearchText.AutoSize = true;
         this.lblSearchText.TabIndex = 3;
         this.lblSearchText.Text = "Search Text";
         // 
         // lblFileTypes
         // 
         this.lblFileTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lblFileTypes.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblFileTypes.Location = new System.Drawing.Point(8, 78);
         this.lblFileTypes.Name = "lblFileTypes";
         this.lblFileTypes.Size = new System.Drawing.Size(163, 16);
         this.lblFileTypes.AutoSize = true;
         this.lblFileTypes.TabIndex = 2;
         this.lblFileTypes.Text = "File Types";
         this.lblFileTypes.Padding = new Padding(8, 0, 0, 0);
         // 
         // lblSearchPath
         // 
         this.lblSearchPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lblSearchPath.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblSearchPath.Location = new System.Drawing.Point(8, 30);
         this.lblSearchPath.Name = "lblSearchPath";
         this.lblSearchPath.Size = new System.Drawing.Size(163, 16);
         this.lblSearchPath.AutoSize = true;
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
         this.lblSearchHeading.Size = new System.Drawing.Size(199, 23);
         this.lblSearchHeading.TabIndex = 0;
         this.lblSearchHeading.Text = "AstroGrep Search";
         this.lblSearchHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.lblSearchHeading.BackColor = SystemColors.Window;
         this.lblSearchHeading.ForeColor = AstroGrep.Common.ProductInformation.ApplicationColor;
         this.lblSearchHeading.Paint += lblSearchHeading_Paint;
         this.lblSearchHeading.Padding = new Padding(0, 0, 0, 4);
         // 
         // pnlRightSide
         // 
         this.pnlRightSide.Controls.Add(this.textElementHost);
         this.pnlRightSide.Controls.Add(this.splitUpDown);
         this.pnlRightSide.Controls.Add(this.lstFileNames);
         this.pnlRightSide.Dock = System.Windows.Forms.DockStyle.Fill;
         this.pnlRightSide.Location = new System.Drawing.Point(240, 0);
         this.pnlRightSide.Name = "pnlRightSide";
         this.pnlRightSide.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
         this.pnlRightSide.Size = new System.Drawing.Size(544, 430);
         this.pnlRightSide.BackColor = SystemColors.Window;
         this.pnlRightSide.TabIndex = 1;
         // 
         // textElementHost
         // 
         this.textElementHost.Dock = System.Windows.Forms.DockStyle.Fill;
         this.textElementHost.Location = new System.Drawing.Point(8, 200);
         this.textElementHost.Size = new System.Drawing.Size(536, 230);
         this.textElementHost.TabIndex = 1;
         this.textElementHost.Child = this.txtHits;
         //
         // txtHits
         //
         this.txtHits.Name = "txtHits";
         this.txtHits.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
         this.txtHits.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
         // 
         // splitUpDown
         // 
         this.splitUpDown.Dock = System.Windows.Forms.DockStyle.Top;
         this.splitUpDown.Location = new System.Drawing.Point(8, 192);
         this.splitUpDown.Name = "splitUpDown";
         this.splitUpDown.Size = new System.Drawing.Size(536, 2);
         this.splitUpDown.TabIndex = 2;
         this.splitUpDown.TabStop = false;
         this.splitUpDown.Cursor = Cursors.SizeNS;
         this.splitUpDown.BackColor = SystemColors.Control;
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
         this.lstFileNames.BorderStyle = BorderStyle.None;
         this.lstFileNames.Enter += lstFileNames_Enter;
         this.lstFileNames.Leave += lstFileNames_Leave;
         this.lstFileNames.MouseDown += lstFileNames_MouseDown;
         this.lstFileNames.ColumnClick += lstFileNames_ColumnClick;
         this.lstFileNames.ItemDrag += lstFileNames_ItemDrag;
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
         this.splitLeftRight.Size = new System.Drawing.Size(2, 430);
         this.splitLeftRight.TabIndex = 2;
         this.splitLeftRight.TabStop = false;
         this.splitLeftRight.Cursor = Cursors.SizeWE;
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
         this.sbFilterCountPanel.DoubleClick += new EventHandler(sbFilterCountPanel_DoubleClick);
         // 
         // sbErrorCountPanel
         // 
         this.sbErrorCountPanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.sbErrorCountPanel.Name = "sbErrorCountPanel";
         this.sbErrorCountPanel.Size = new System.Drawing.Size(44, 17);
         this.sbErrorCountPanel.Text = "Error: 0";
         this.sbErrorCountPanel.DoubleClickEnabled = true;
         this.sbErrorCountPanel.ToolTipText = "Double click to display errors.";
         this.sbErrorCountPanel.DoubleClick += new EventHandler(sbErrorCountPanel_DoubleClick);
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
         this.MinimumSize = new Size(600, 400);
         this.Controls.Add(this.splitLeftRight);
         this.Controls.Add(this.pnlRightSide);
         this.Controls.Add(this.pnlSearch);
         this.Controls.Add(this.stbStatus);
         this.Controls.Add(this.MainMenu);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MainMenuStrip = this.MainMenu;
         this.Name = "frmMain";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "AstroGrep";
         this.Load += new System.EventHandler(this.frmMain_Load);
         this.Closed += frmMain_Closed;
         this.txtHits.EndInit();
         this.MainMenu.ResumeLayout(false);
         this.MainMenu.PerformLayout();
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
