using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Gtk;

using AstroGrep.AstroGrepBase;

namespace AstroGrep.Linux.Forms
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
   /// [Curtis_Beard]      11/03/2006  Created
   /// </history>
   public class frmMain : Window
   {
      //private static string BROWSE_TEXT = "Browse...";
      private System.Collections.ArrayList alSearchErrors;

      /// <summary>
      /// Creates an instance of the frmMain class.
      /// </summary>
      /// /// <history>
      /// [Curtis_Beard]	   11/03/2006	Created
      /// </history>
      public frmMain() : base ("")
      {
         Language.Load(Core.GeneralSettings.Language);

         InitializeComponent();

         // change System.Drawing.Color defaults to Gdk.Color defaults
         if (Core.GeneralSettings.ResultsBackColor.EndsWith("-255"))
            Core.GeneralSettings.ResultsBackColor = "65535-65535-65535-1229600";
         if (Core.GeneralSettings.ResultsForeColor.EndsWith("-255"))
            Core.GeneralSettings.ResultsForeColor = "0-0-0-1229600";
         if (Core.GeneralSettings.HighlightBackColor.EndsWith("-255"))
            Core.GeneralSettings.HighlightBackColor = "65535-65535-65535-1229600";
         if (Core.GeneralSettings.HighlightForeColor.EndsWith("-255"))
            Core.GeneralSettings.HighlightForeColor = Common.ConvertColorToString(Common.ASTROGREP_ORANGE);

         // update results display
         txtViewer.ModifyBase(txtViewer.State, Common.ConvertStringToColor(Core.GeneralSettings.ResultsBackColor));
         txtViewer.ModifyText(txtViewer.State, Common.ConvertStringToColor(Core.GeneralSettings.ResultsForeColor));
         AddHighlightTextTag();

         LoadSearchSettings();
      }

      #region Graphical Layout Code
      private Statusbar sbStatus;
      private Button btnSearch;
      private Button btnCancel;
      private Button btnBrowse;
      private TextView txtViewer;
      private Grep gpGrep;
      private Gtk.Tooltips MainTooltips;

      private Label lblSearchStart;
      private ComboBoxEntry cboSearchStart;
      private Label lblSearchFilter;
      private ComboBoxEntry cboSearchFilter;
      private Label lblSearchText;
      private ComboBoxEntry cboSearchText;

      private CheckButton chkRegularExpressions;
      private CheckButton chkCaseSensitive;
      private CheckButton chkWholeWord;
      private CheckButton chkRecurse;
      private CheckButton chkFileNamesOnly;
      private CheckButton chkNegation;
      private CheckButton chkLineNumbers;
      private ComboBox cboContextLines;
      private Label lblContextLines;

      private MenuBar mbMain;
      private AccelGroup agMenuAccel;

      private Gtk.TreeView tvFiles;
      //private Gtk.ListStore lsFiles;

      private Menu mnuFile;
      private Menu mnuEdit;
      private Menu mnuTools;
      private Menu mnuHelp;

      private Gtk.ImageMenuItem SaveMenuItem;
      private Gtk.ImageMenuItem PrintMenuItem;
      private Gtk.ImageMenuItem ExitMenuItem;
      private Gtk.ImageMenuItem SelectAllMenuItem;
      private Gtk.ImageMenuItem OpenSelectedMenuItem;

      private HPaned panelLeft;
      private VPaned panelRight;

      /// <summary>
      /// Initializes all GUI components.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      11/03/2006  Created
      /// </history>
      private void InitializeComponent()
      {
         this.SetDefaultSize (Core.GeneralSettings.WindowWidth, Core.GeneralSettings.WindowHeight);
         if (Core.GeneralSettings.WindowLeft == -1 && Core.GeneralSettings.WindowTop == -1)
            this.SetPosition(WindowPosition.Center);
         else
            this.Move(Core.GeneralSettings.WindowLeft, Core.GeneralSettings.WindowTop);

         this.DeleteEvent += new DeleteEventHandler(OnWindowDelete);
         this.Title = Constants.ProductName;
         this.Icon = Images.GetPixbuf("AstroGrep_Icon.ico");

         MainTooltips = new Tooltips();

         VBox vbox = new VBox();
         vbox.BorderWidth = 0;

         Frame leftFrame = new Frame();
         leftFrame.Shadow = ShadowType.In;
         leftFrame.WidthRequest = 200;

         VBox searchBox = new VBox();
         VBox searchOptionsBox = new VBox();
         searchBox.BorderWidth = 3;
         searchOptionsBox.BorderWidth = 3;
         lblSearchStart = new Label("Search Path");
         lblSearchStart.SetAlignment(0,0);

         btnBrowse = new Button();
         btnBrowse.SetSizeRequest(32, 20);
         Gtk.Image img = new Image();
         img.Pixbuf = Images.GetPixbuf("folder-open.png");
         VBox browseBox = new VBox();
         browseBox.PackStart(img, false, false, 0);
         MainTooltips.SetTip(btnBrowse, "Select the folder to start the search", "");
         btnBrowse.Clicked += new EventHandler(btnBrowse_Clicked);
         btnBrowse.Add(browseBox);

         cboSearchStart = ComboBoxEntry.NewText();
         cboSearchFilter = ComboBoxEntry.NewText();
         cboSearchText = ComboBoxEntry.NewText();
         
         LoadComboBoxEntry(cboSearchStart, Core.GeneralSettings.SearchStarts, true);
         LoadComboBoxEntry(cboSearchFilter, Core.GeneralSettings.SearchFilters, false);
         LoadComboBoxEntry(cboSearchText, Core.GeneralSettings.SearchTexts, false);

         cboSearchStart.Changed += new EventHandler(cboSearchStart_Changed);
         lblSearchFilter = new Label("File Types");
         lblSearchFilter.SetAlignment(0,0);                  
         lblSearchText = new Label("Search Text");
         lblSearchText.SetAlignment(0,0);
         
         // search path
         VBox startVBox = new VBox();
         startVBox.BorderWidth = 0;
         cboSearchStart.WidthRequest = 100;
         SetActiveComboBoxEntry(cboSearchStart);

         HBox startHBox = new HBox();
         startHBox.BorderWidth = 0;
         startHBox.PackStart(cboSearchStart, true, true, 0);
         startHBox.PackEnd(btnBrowse, false, false, 0);

         startVBox.PackStart(lblSearchStart, false, false, 0);
         startVBox.PackStart(startHBox, true, false, 0);         
         searchBox.PackStart(startVBox, true, false, 0);

         // search filter
         VBox filterVBox = new VBox();
         cboSearchFilter.Active = 0;         
         filterVBox.BorderWidth = 0;
         filterVBox.PackStart(lblSearchFilter, false, false, 0);
         filterVBox.PackStart(cboSearchFilter, true, false, 0);
         searchBox.PackStart(filterVBox, true, false, 0);
         
         // search text
         VBox textVBox = new VBox();
         cboSearchText.Active = 0;         
         textVBox.BorderWidth = 0;
         textVBox.PackStart(lblSearchText, false, false, 0);
         textVBox.PackStart(cboSearchText, true, false, 0);
         searchBox.PackStart(textVBox, true, false, 0);
         
         // Search/Cancel buttons
         searchBox.PackStart(CreateButtons(), false, false, 0);

         // Search Options
         chkRegularExpressions = new CheckButton("Regular Expressions");
         chkCaseSensitive = new CheckButton("Case Sensitive");
         chkWholeWord = new CheckButton("Whole Word");
         chkRecurse = new CheckButton("Recurse");
         chkFileNamesOnly = new CheckButton("Show File Names Only");
         chkFileNamesOnly.Clicked += new EventHandler(chkFileNamesOnly_Clicked);
         chkNegation = new CheckButton("Negation");
         chkNegation.Clicked += new EventHandler(chkNegation_Clicked);
         chkLineNumbers = new CheckButton("Line Numbers");
         cboContextLines = ComboBox.NewText();
         cboContextLines.WidthRequest = 100;
         cboContextLines.WrapWidth = 3;
         for (int i = 0; i <= Constants.MAX_CONTEXT_LINES; i++)
            cboContextLines.AppendText(i.ToString());
         lblContextLines = new Label("Context Lines");
         HBox cxtBox = new HBox();
         cxtBox.BorderWidth = 0;
         cxtBox.PackStart(cboContextLines, false, false, 3);
         cxtBox.PackStart(lblContextLines, false, false, 3);

         searchOptionsBox.PackStart(chkRegularExpressions, true, false, 0);
         searchOptionsBox.PackStart(chkCaseSensitive, true, false, 0);
         searchOptionsBox.PackStart(chkWholeWord, true, false, 0);
         searchOptionsBox.PackStart(chkRecurse, true, false, 0);
         searchOptionsBox.PackStart(chkFileNamesOnly, true, false, 0);
         searchOptionsBox.PackStart(chkNegation, true, false, 0);
         searchOptionsBox.PackStart(chkLineNumbers, true, false, 0);
         searchOptionsBox.PackStart(cxtBox, true, false, 0);
         searchBox.PackEnd(searchOptionsBox, true, true, 0);

         leftFrame.Add(searchBox);
         
         panelLeft = new HPaned();
         panelLeft.BorderWidth = 0;
         panelRight = new VPaned();
         panelRight.BorderWidth = 0;

         // File List
         Gtk.Frame treeFrame = new Gtk.Frame();
         treeFrame.Shadow = ShadowType.In;
         Gtk.ScrolledWindow treeWin = new Gtk.ScrolledWindow();
         tvFiles = new Gtk.TreeView ();
         SetColumnsText();

         tvFiles.Model = new ListStore(typeof (string), typeof (string), typeof (string), typeof (string), typeof (int));
         (tvFiles.Model as ListStore).DefaultSortFunc = new TreeIterCompareFunc(DefaultTreeIterCompareFunc);
         tvFiles.Selection.Changed += new EventHandler(Tree_OnSelectionChanged);
         tvFiles.RowActivated += new RowActivatedHandler(tvFiles_RowActivated);

         tvFiles.RulesHint = true;
         tvFiles.HeadersClickable = true;
         tvFiles.HeadersVisible = true;
         tvFiles.Selection.Mode = SelectionMode.Multiple;

         SetSortingFunctions();
                  
         treeWin.Add(tvFiles);
         treeFrame.BorderWidth = 0;
         treeFrame.Add(treeWin);

         // txtHits
         Gtk.Frame ScrolledWindowFrm = new Gtk.Frame();
         ScrolledWindowFrm.Shadow = ShadowType.In;
         Gtk.ScrolledWindow TxtViewWin = new Gtk.ScrolledWindow();
         txtViewer = new Gtk.TextView();
         txtViewer.Buffer.Text = "";
         txtViewer.Editable = false;
         TxtViewWin.Add(txtViewer);
         ScrolledWindowFrm.BorderWidth = 0;
         ScrolledWindowFrm.Add(TxtViewWin);

         // Add file list and txtHits to right panel
         panelRight.Pack1(treeFrame, true, true);
         panelRight.Pack2(ScrolledWindowFrm, true, true);

// TLW

//Notebook notebook = new Notebook();
//    Table table = new Table(3, 6);
    
    // Create a new notebook, place the position of the tabs
  //  table.attach(notebook, 0, 6, 0, 1);
    
    
    
         // Status Bar
         sbStatus = new Statusbar();

         #region Menu bar

         agMenuAccel = new AccelGroup();
         this.AddAccelGroup(agMenuAccel);

         mbMain = new Gtk.MenuBar();
			
         // File menu
         mnuFile = new Menu();
         MenuItem mnuFileItem = new MenuItem("_File");
         mnuFileItem.Submenu = mnuFile;
         mnuFile.AccelGroup = agMenuAccel;
         mnuFile.Shown += new EventHandler(mnuFile_Shown);

         // Edit menu
         mnuEdit = new Menu();
         MenuItem mnuEditItem = new MenuItem("_Edit");
         mnuEditItem.Submenu = mnuEdit;
         mnuEdit.AccelGroup = agMenuAccel;
         mnuEdit.Shown += new EventHandler(mnuEdit_Shown);

         // Tools menu
         mnuTools = new Menu();
         MenuItem mnuToolsItem = new MenuItem("_Tools");
         mnuToolsItem.Submenu = mnuTools;
         mnuTools.AccelGroup = agMenuAccel;
    		
         // Help menu
         mnuHelp = new Menu();
         MenuItem mnuHelpItem = new MenuItem("_Help");
         mnuHelpItem.Submenu = mnuHelp;
         mnuHelp.AccelGroup = agMenuAccel;
    		
         // File Save menu item
         SaveMenuItem = new ImageMenuItem(Stock.Save, agMenuAccel);
         SaveMenuItem.Activated += new EventHandler(SaveMenuItem_Activated);
         mnuFile.Append(SaveMenuItem);

         // File Print menu item
         PrintMenuItem = new ImageMenuItem(Stock.Print, agMenuAccel);
         PrintMenuItem.Activated += new EventHandler(PrintMenuItem_Activated);
         mnuFile.Append(PrintMenuItem);

         // File Separator menu item
         SeparatorMenuItem Separator1MenuItem = new SeparatorMenuItem();
         mnuFile.Append(Separator1MenuItem);

         // File Exit menu item
         ExitMenuItem = new ImageMenuItem(Stock.Quit, agMenuAccel);
         ExitMenuItem.Activated += new EventHandler(ExitMenuItem_Activated);
         mnuFile.Append(ExitMenuItem);

         // Edit Select All menu item
         SelectAllMenuItem = new ImageMenuItem("_Select All Files", agMenuAccel);
         SelectAllMenuItem.Activated += new EventHandler(SelectAllMenuItem_Activated);
         mnuEdit.Append(SelectAllMenuItem);

         // Edit Open Selected menu item
         OpenSelectedMenuItem = new ImageMenuItem("_Open Selected Files", agMenuAccel);
         OpenSelectedMenuItem.Activated += new EventHandler(OpenSelectedMenuItem_Activated);
         mnuEdit.Append(OpenSelectedMenuItem);

         // Create preferences for every other os except windows
         if (!Common.IsWindows)
         {
            Separator1MenuItem = new SeparatorMenuItem();
            mnuEdit.Append(Separator1MenuItem);
         
            // Preferences
            Gtk.ImageMenuItem OptionsMenuItem = new ImageMenuItem(Stock.Preferences, agMenuAccel);
            OptionsMenuItem.Activated += new EventHandler(OptionsMenuItem_Activated);
            mnuEdit.Append(OptionsMenuItem);
         }         

         // Clear MRU List
         Gtk.ImageMenuItem ClearMRUsMenuItem = new ImageMenuItem("_Clear Most Recently Used Lists", agMenuAccel);
         ClearMRUsMenuItem.Activated += new EventHandler(ClearMRUsMenuItem_Activated);
         mnuTools.Append(ClearMRUsMenuItem);
         
         Separator1MenuItem = new SeparatorMenuItem();
         mnuTools.Append(Separator1MenuItem);

         // Save Search Options
         Gtk.ImageMenuItem SaveOptionsMenuItem = new ImageMenuItem("_Save Search Options", agMenuAccel);
         SaveOptionsMenuItem.Activated += new EventHandler(SaveOptionsMenuItem_Activated);
         mnuTools.Append(SaveOptionsMenuItem);

         // Create Options menu for windows
         if (Common.IsWindows)
         {
            // Options menu item
            Gtk.ImageMenuItem OptionsMenuItem = new ImageMenuItem("_Options...", agMenuAccel);
            OptionsMenuItem.Activated += new EventHandler(OptionsMenuItem_Activated);
            OptionsMenuItem.Image = new Gtk.Image(Stock.Preferences, IconSize.Menu);
            mnuTools.Append(OptionsMenuItem);
         }

         // Help About menu item
         MenuItem AboutMenuItem = new ImageMenuItem(Stock.About, agMenuAccel);
         AboutMenuItem.Activated += new EventHandler(AboutMenuItem_Activated);
         mnuHelp.Append(AboutMenuItem);

         // Add the menus to the menubar		
         mbMain.Append(mnuFileItem);
         mbMain.Append(mnuEditItem);
         mbMain.Append(mnuToolsItem);
         mbMain.Append(mnuHelpItem);

         // Add the menubar to the Menu panel
         vbox.PackStart(mbMain, false, false, 0);
    		
         #endregion

         // add items to container
         panelLeft.Pack1(leftFrame, true, false);
         
         // TLW 
         //panelLeft.Pack2(tabControl, true, false);
         panelLeft.Pack2(panelRight, true, false);
         
         
         // set starting position of splitter
         panelLeft.Position = Core.GeneralSettings.WindowSearchPanelWidth;
         panelRight.Position = Core.GeneralSettings.WindowFilePanelHeight;

         vbox.PackStart(panelLeft, true, true, 0);
         vbox.PackEnd(sbStatus, false, true, 3);

         this.Add (vbox);

         this.ShowAll ();
      }
      #endregion

      #region Menu Events
      /// <summary>
      /// Enable/Disable menu items when menu is shown.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      11/03/2006  Created
      /// </history>
      private void mnuFile_Shown(object sender, EventArgs e)
      {
         if ((tvFiles.Model as ListStore).IterNChildren() == 0)
         {
            SaveMenuItem.Sensitive = false;
            PrintMenuItem.Sensitive = false;
         }
         else
         {
            SaveMenuItem.Sensitive = true;
            PrintMenuItem.Sensitive = true;
         }
      }

      /// <summary>
      /// Save results to file.
      /// </summary>
      /// <param name="o">system parameter</param>
      /// <param name="args">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      11/03/2006  Created
      /// </history>
      private void SaveMenuItem_Activated(object o, EventArgs args)
      {
			//save results to file (text, html, xml)
         //MessageBox(this, MessageType.Info, "Not implemented in this version.");

         FileChooserDialog dlg = new Gtk.FileChooserDialog("", this, FileChooserAction.Save);
         dlg.Modal = true;
         dlg.Title = Language.GetGenericText("SaveDialogTitle");
         dlg.SelectMultiple = false;

         FileFilter filter = new FileFilter();
         filter.AddPattern("*.txt");
         filter.Name = "Text";
         dlg.AddFilter(filter);

         filter = new FileFilter();
         filter.AddPattern("*.html");
         filter.Name = "HTML";
         dlg.AddFilter(filter);

         filter = new FileFilter();
         filter.AddPattern("*.xml");
         filter.Name = "XML";
         dlg.AddFilter(filter);

         if (Common.IsWindows)
         {
            dlg.AddButton (Stock.Save, ResponseType.Ok);
            dlg.AddButton (Stock.Cancel, ResponseType.Cancel);
         }
         else
         {
            dlg.AddButton (Stock.Cancel, ResponseType.Cancel);
            dlg.AddButton (Stock.Save, ResponseType.Ok);
         }

         Gtk.ResponseType result = (Gtk.ResponseType)dlg.Run ();
         dlg.Hide();

         if(result == Gtk.ResponseType.Ok)
         {
            //save file based on type
         }

         dlg.Destroy();
      }

      /// <summary>
      /// Print results.
      /// </summary>
      /// <param name="o">system parameter</param>
      /// <param name="args">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      11/03/2006  Created
      /// </history>
      private void PrintMenuItem_Activated(object o, EventArgs args)
      {
         // print
         MessageBox(this, MessageType.Info, "Not implemented in this version.");
      }

      /// <summary>
      /// Exit the application.
      /// </summary>
      /// <param name="o">system parameter</param>
      /// <param name="args">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      11/03/2006  Created
      /// </history>
      private void ExitMenuItem_Activated(object o, EventArgs args)
      {
         ApplicationExit();
      }

      /// <summary>
      /// Enable/Disable menu items when menu is shown.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      11/03/2006  Created
      /// </history>
      private void mnuEdit_Shown(object sender, EventArgs e)
      {
         if ((tvFiles.Model as ListStore).IterNChildren() == 0)
         {
            SelectAllMenuItem.Sensitive = false;
            OpenSelectedMenuItem.Sensitive = false;
         }
         else
         {
            SelectAllMenuItem.Sensitive = true;
            OpenSelectedMenuItem.Sensitive = true;
         }
      }

      /// <summary>
      /// Select all the result files.
      /// </summary>
      /// <param name="o">system parameter</param>
      /// <param name="args">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      11/03/2006  Created
      /// </history>
      private void SelectAllMenuItem_Activated(object o, EventArgs args)
      {
         tvFiles.Selection.SelectAll();
      }

      /// <summary>
      /// Open the selected result files.
      /// </summary>
      /// <param name="o">system parameter</param>
      /// <param name="args">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      11/03/2006  Created
      /// </history>
      private void OpenSelectedMenuItem_Activated(object o, EventArgs args)
      {
         // open each selected file
         MessageBox(this, MessageType.Info, "Not implemented in this version.");
      }

      /// <summary>
      /// Clear the most recently used lists.
      /// </summary>
      /// <param name="o">system parameter</param>
      /// <param name="args">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      11/03/2006  Created
      /// </history>
      private void ClearMRUsMenuItem_Activated(object o, EventArgs args)
      {
         // clear the most recently used lists
         ClearComboBoxEntries(cboSearchStart, true);
         ClearComboBoxEntries(cboSearchFilter, false);
         ClearComboBoxEntries(cboSearchText, false);

         // clear settings
         Core.GeneralSettings.SearchStarts = string.Empty;
         Core.GeneralSettings.SearchFilters = string.Empty;
         Core.GeneralSettings.SearchTexts = string.Empty;
      }

      /// <summary>
      /// Save the searh options.
      /// </summary>
      /// <param name="o">system parameter</param>
      /// <param name="args">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      11/03/2006  Created
      /// </history>
      private void SaveOptionsMenuItem_Activated(object o, EventArgs args)
      {
         SaveSearchSettings();
      }

      /// <summary>
      /// Display the preferences dialog.
      /// </summary>
      /// <param name="o">system parameter</param>
      /// <param name="args">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      11/03/2006  Created
      /// </history>
      private void OptionsMenuItem_Activated(object o, EventArgs args)
      {
         frmOptions dlg = new frmOptions(this, Gtk.DialogFlags.Modal);

         ResponseType result = (ResponseType)dlg.Run();
         dlg.Hide();

         if (result == ResponseType.Ok)
         {
            // update results display
            txtViewer.ModifyBase(txtViewer.State, Common.ConvertStringToColor(Core.GeneralSettings.ResultsBackColor));
            txtViewer.ModifyText(txtViewer.State, Common.ConvertStringToColor(Core.GeneralSettings.ResultsForeColor));
            
            // re-highlight matches
            AddHighlightTextTag();
            Tree_OnSelectionChanged(tvFiles.Selection, null);

            // update mru lists
            //RemoveComboBoxEntry(cboSearchStart, Language.GetGenericText("Browse"));
            while (cboSearchStart.Model.IterNChildren() > Core.GeneralSettings.MaximumMRUPaths)
               cboSearchStart.RemoveText(cboSearchStart.Model.IterNChildren() - 1);
            //cboSearchStart.AppendText(Language.GetGenericText("Browse"));

            while (cboSearchFilter.Model.IterNChildren() > Core.GeneralSettings.MaximumMRUPaths)
               cboSearchFilter.RemoveText(cboSearchFilter.Model.IterNChildren() - 1);
            while (cboSearchText.Model.IterNChildren() > Core.GeneralSettings.MaximumMRUPaths)
               cboSearchText.RemoveText(cboSearchText.Model.IterNChildren() - 1);
               
            // update language
            if (dlg.IsLanguageChange)
            {
               //RemoveComboBoxEntry(cboSearchStart, Language.GetGenericText("Browse"));

               Language.Load(Core.GeneralSettings.Language);
               Language.ProcessForm(this, MainTooltips);

               SetColumnsText();
               //cboSearchStart.AppendText(Language.GetGenericText("Browse"));

               // reload label
//               __SearchOptionsText = lnkSearchOptions.Text;
//               if (!__OptionsShow)
//                  lnkSearchOptions.Text = String.Format(__SearchOptionsText, ">>");
//               else
//                  lnkSearchOptions.Text = String.Format(__SearchOptionsText, "<<");
            }
         }
         dlg.Destroy();         
      }

      /// <summary>
      /// Display the about dialog.
      /// </summary>
      /// <param name="o">system parameter</param>
      /// <param name="args">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      11/03/2006  Created
      /// </history>
      private void AboutMenuItem_Activated(object o, EventArgs args)
      {
         frmAbout dlg = new frmAbout(this, Gtk.DialogFlags.Modal);
         dlg.Run();
         dlg.Destroy();
      }
      #endregion      

      #region Events
      /// <summary>
      /// Start a search.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      private void btnSearch_Clicked(object sender, EventArgs e)
      {
			if (!ValidateInput())
				return;
				
         // gui state
         SetSearchState(null, new SearchEventArgs(false));
         sbStatus.Pop(1);
         txtViewer.Buffer.Clear();
         (tvFiles.Model as ListStore).Clear();

         alSearchErrors = null;

         AddComboSelectionSpecial(cboSearchStart, cboSearchStart.ActiveText);
         AddComboSelection(cboSearchFilter, cboSearchFilter.ActiveText);
         AddComboSelection(cboSearchText, cboSearchText.ActiveText);

         // search pattern
         gpGrep = new Grep();
         gpGrep.StartDirectory = cboSearchStart.ActiveText;
         gpGrep.FileFilter = cboSearchFilter.ActiveText;
         gpGrep.SearchText = cboSearchText.ActiveText;         

         // exclusion list
         string[] extensions = Core.GeneralSettings.ExtensionExcludeList.Split(';');
         foreach (string ext in extensions)
            gpGrep.AddExclusionExtension(ext.ToLower());
         
         // options
         gpGrep.UseRegularExpressions = chkRegularExpressions.Active;
         gpGrep.UseCaseSensitivity = chkCaseSensitive.Active;
         gpGrep.UseWholeWordMatching = chkWholeWord.Active;
         gpGrep.UseRecursion = chkRecurse.Active;
         gpGrep.ReturnOnlyFileNames = chkFileNamesOnly.Active;
         gpGrep.UseNegation = chkNegation.Active;
         gpGrep.IncludeLineNumbers = chkLineNumbers.Active;
         gpGrep.ContextLines = cboContextLines.Active;

         // events
         gpGrep.SearchingFile += new Grep.SearchingFileHandler(gpGrep_SearchingFile);
         gpGrep.FileHit += new Grep.FileHitHandler(gpGrep_FileHit);
         gpGrep.LineHit += new Grep.LineHitHandler(gpGrep_LineHit);
         gpGrep.SearchCancel += new Grep.SearchCancelHandler(gpGrep_SearchCancel);
         gpGrep.SearchComplete += new Grep.SearchCompleteHandler(gpGrep_SearchComplete);
         gpGrep.SearchError += new Grep.SearchErrorHandler(gpGrep_SearchError);

         gpGrep.BeginExecute();
      }

      /// <summary>
      /// Cancel a search.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      private void btnCancel_Clicked(object sender, EventArgs e)
      {
         if (gpGrep != null)
            gpGrep.Abort();
      }

      /// <summary>
      /// Browse for a starting folder.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      private void btnBrowse_Clicked(object sender, EventArgs e)
      {
         BrowseForFolder();
      }

      /// <summary>
      /// Exit the application properly.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      private void OnWindowDelete(object sender, DeleteEventArgs e) 
      {
         ApplicationExit();
         e.RetVal = true;
      }

      /// <summary>
      /// Retrieve the hits from the selected hit file.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      private void Tree_OnSelectionChanged (object sender, EventArgs e)
      {
         TreeIter iter;

         TreeSelection select = (TreeSelection)sender;
         TreePath[] paths = select.GetSelectedRows();

         if (paths.Length > 0 && (tvFiles.Model as ListStore).GetIter(out iter, paths[0]))
         {
            int index = (int)(tvFiles.Model as ListStore).GetValue (iter, Constants.COLUMN_INDEX_GREP_INDEX);

            HitObject hit = gpGrep.RetrieveHitObject(index);

            HighlightText(hit);
         }

//         // For single mode only
//         TreeModel model;

//         if (((TreeSelection)o).GetSelected (out model, out iter))
//         {
//            int index = (int) model.GetValue (iter, 4);
//
//            HitObject hit = gpGrep.RetrieveHitObject(index);
//
//            txtViewer.Buffer.Text = hit.Lines;
//         }
      }

      /// <summary>
      /// Handles tree view double click.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      private void tvFiles_RowActivated(object sender, RowActivatedArgs e)
      {
         if (e.Path != null)
         {
            // display editor
            //txtViewer.Buffer.Text = "item clicked: " + args.Path.ToString();            
         }
      }

      /// <summary>
      /// Negation option clicked.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      private void chkNegation_Clicked(object sender, EventArgs e)
      {
         chkFileNamesOnly.Active = chkNegation.Active;
         chkFileNamesOnly.Sensitive = !chkNegation.Active;
      }

      /// <summary>
      /// File names only option clicked.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      private void chkFileNamesOnly_Clicked(object sender, EventArgs e)
      {
         if (chkFileNamesOnly.Active)
         {
            chkLineNumbers.Sensitive = false;
            cboContextLines.Sensitive = false;
            lblContextLines.Sensitive = false;
         }
         else
         {
            chkLineNumbers.Sensitive = true;
            cboContextLines.Sensitive = true;
            lblContextLines.Sensitive = true;
         }
      }

      /// <summary>
      /// St
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      private void cboSearchStart_Changed(object sender, EventArgs e)
      {
//         if (sender == null)
//            return;
//
//         ComboBoxEntry combo = sender as ComboBoxEntry;
//         TreeIter iter;
//
//         if (combo.GetActiveIter (out iter))
//         {
//            string value = (string)combo.Model.GetValue(iter, 0);
//            if (value.Equals(Language.GetGenericText("Browse")))
//            {
//               combo.Popdown();
//               BrowseForFolder();
//            }
//         }
      }
      #endregion

      #region Private Methods
      /// <summary>
      /// Sets the file list's columns' text to the correct language.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		11/06/2006	Created
      /// </history>
      private void SetColumnsText()
      {
         if (tvFiles.Columns.Length == 0)
         {
            tvFiles.AppendColumn(CreateTreeViewColumn(Language.GetGenericText("ResultsColumnFile"), 60, Core.GeneralSettings.WindowFileColumnNameWidth, Constants.COLUMN_INDEX_FILE));
            tvFiles.AppendColumn(CreateTreeViewColumn(Language.GetGenericText("ResultsColumnLocation"), 200, Core.GeneralSettings.WindowFileColumnLocationWidth, Constants.COLUMN_INDEX_DIRECTORY));
            tvFiles.AppendColumn(CreateTreeViewColumn(Language.GetGenericText("ResultsColumnDate"), 100, Core.GeneralSettings.WindowFileColumnDateWidth, Constants.COLUMN_INDEX_DATE));
            tvFiles.AppendColumn(CreateTreeViewColumn(Language.GetGenericText("ResultsColumnCount"), 30, Core.GeneralSettings.WindowFileColumnCountWidth, Constants.COLUMN_INDEX_COUNT));
         }
         else
         {
            tvFiles.Columns[Constants.COLUMN_INDEX_FILE].Title = Language.GetGenericText("ResultsColumnFile");
            tvFiles.Columns[Constants.COLUMN_INDEX_DIRECTORY].Title = Language.GetGenericText("ResultsColumnLocation");
            tvFiles.Columns[Constants.COLUMN_INDEX_DATE].Title = Language.GetGenericText("ResultsColumnDate");
            tvFiles.Columns[Constants.COLUMN_INDEX_COUNT].Title = Language.GetGenericText("ResultsColumnCount");
         }
      }

      /// <summary>
      /// Set the GUI's state based on the search state.
      /// </summary>
      /// <param name="sender">null</param>
      /// <param name="args">SearchEventArgs containing state information</param>
      private void SetSearchState(object sender, System.EventArgs args)
      {
         bool enable = ((SearchEventArgs)args).Enable;

         mbMain.Sensitive = enable;

         btnSearch.Sensitive = enable;
         btnCancel.Sensitive = !enable;
         chkRegularExpressions.Sensitive = enable;
         chkCaseSensitive.Sensitive = enable;
         chkWholeWord.Sensitive = enable;
         chkRecurse.Sensitive = enable;
         chkFileNamesOnly.Sensitive = enable;
         chkNegation.Sensitive = enable;
         chkLineNumbers.Sensitive = enable;
         cboContextLines.Sensitive = enable;
         lblContextLines.Sensitive = enable;

         cboSearchStart.Sensitive = enable;
         cboSearchFilter.Sensitive = enable;
         cboSearchText.Sensitive = enable;

         if (enable)
            btnSearch.GrabFocus();
         else
            btnCancel.GrabFocus();
      }

      /// <summary>
      /// Load all search options and set the control's values.
      /// </summary>
      private void LoadSearchSettings()
      {
         chkRegularExpressions.Active = Core.SearchSettings.UseRegularExpressions;
         chkCaseSensitive.Active = Core.SearchSettings.UseCaseSensitivity;
         chkWholeWord.Active = Core.SearchSettings.UseWholeWordMatching;
         chkRecurse.Active = Core.SearchSettings.UseRecursion;
         chkFileNamesOnly.Active = Core.SearchSettings.ReturnOnlyFileNames;
         chkNegation.Active = Core.SearchSettings.UseNegation;
         chkLineNumbers.Active = Core.SearchSettings.IncludeLineNumbers;
         cboContextLines.Active = Core.SearchSettings.ContextLines;
      }

      /// <summary>
      /// Save all search options.
      /// </summary>
      private void SaveSearchSettings()
      {
         Core.SearchSettings.UseRegularExpressions = chkRegularExpressions.Active;
         Core.SearchSettings.UseCaseSensitivity = chkCaseSensitive.Active;
         Core.SearchSettings.UseWholeWordMatching = chkWholeWord.Active;
         Core.SearchSettings.UseRecursion = chkRecurse.Active;
         Core.SearchSettings.ReturnOnlyFileNames = chkFileNamesOnly.Active;
         Core.SearchSettings.UseNegation = chkNegation.Active;
         Core.SearchSettings.IncludeLineNumbers = chkLineNumbers.Active;
         Core.SearchSettings.ContextLines = cboContextLines.Active;

         Core.SearchSettings.Save();
      }

      /// <summary>
      /// Creates and returns a new TreeViewColumn.
      /// </summary>
      /// <param name="title">Name of column</param>
      /// <param name="minSize">Minimum size of column</param>
      /// <param name="width">Starting size of column</param>
      /// <param name="index">Index of column</param>
      /// <returns>TreeViewColumn</returns>
      private TreeViewColumn CreateTreeViewColumn(string title, int minSize, int width, int index)
      {
         Gtk.TreeViewColumn col = new TreeViewColumn(title, new Gtk.CellRendererText(), "text", index);
         col.MinWidth = minSize;
         col.Resizable = true;
         col.SortColumnId = index;
         col.Clickable = true;
         col.FixedWidth = width;
         col.Sizing = TreeViewColumnSizing.Fixed;
         col.Title = title;
         col.Reorderable = false;
         col.Visible = true;

         return col;
      }

      /// <summary>
      /// Exit application properly, saving any settings.
      /// </summary>
      private void ApplicationExit()
      {
         //save window position and size
         int x;
         int y;
         int width;
         int height;
         this.GetPosition( out x, out y);
         this.GetSize(out width, out height);

         Core.GeneralSettings.WindowLeft = x;
         Core.GeneralSettings.WindowTop = y;
         Core.GeneralSettings.WindowWidth = width;
         Core.GeneralSettings.WindowHeight = height;

         //save column widths
         Core.GeneralSettings.WindowFileColumnNameWidth = tvFiles.Columns[0].Width;
         Core.GeneralSettings.WindowFileColumnLocationWidth = tvFiles.Columns[1].Width;
         Core.GeneralSettings.WindowFileColumnDateWidth = tvFiles.Columns[2].Width;
         Core.GeneralSettings.WindowFileColumnCountWidth = tvFiles.Columns[3].Width;

         //save divider panel positions
         Core.GeneralSettings.WindowSearchPanelWidth = panelLeft.Position;
         Core.GeneralSettings.WindowFilePanelHeight = panelRight.Position;

         //save search comboboxes
         Core.GeneralSettings.SearchStarts = GetComboBoxEntriesAsString(cboSearchStart, true);
         Core.GeneralSettings.SearchFilters = GetComboBoxEntriesAsString(cboSearchFilter, false);
         Core.GeneralSettings.SearchTexts = GetComboBoxEntriesAsString(cboSearchText, false);
         
         // save the settings to file
         Core.GeneralSettings.Save();

         // exit the application
         Application.Quit();
      }

      /// <summary>
      /// Retrieve all entries from the Gtk.ComboBoxEntry as a string with a separator between
      /// the entries.
      /// </summary>
      /// <param name="combo">Gtk.ComboBoxEntry</param>
      /// <param name="special">True, remove browse from combo first, otherwise get all entries</param>
      /// <remarks>The separator is defined in Core.GeneralSettings.SEARCH_SEPARATOR</remarks>
      /// <returns>string of all entries with a separator between them</returns>
      private string GetComboBoxEntriesAsString(Gtk.ComboBoxEntry combo, bool special)
      {
//         if (special)
//            RemoveComboBoxEntry(combo, Language.GetGenericText("Browse"));

         return Common.GetComboBoxEntriesAsString(combo);
      }

      /// <summary>
      /// Add an item to the Gtk.ComboBoxEntry.
      /// </summary>
      /// <param name="combo">Gtk.ComboBoxEntry</param>
      /// <param name="item">string value to add</param>
      private void AddComboSelection(Gtk.ComboBoxEntry combo, string item)
      {
         // remove any old instance
         RemoveComboBoxEntry(combo, item);

         // insert the item at the top
         combo.PrependText(item);
 
         // set the item to be the active one
         combo.Active = 0;

         // Only store as many paths as has been set in options.
         if (combo.Model.IterNChildren() > Core.GeneralSettings.MaximumMRUPaths)
         {
            // Remove the last item in the list.
            combo.RemoveText(combo.Model.IterNChildren() - 1);
         }
      }

      /// <summary>
      /// Add an item to the Gtk.ComboBoxEntry, but make sure the Browse... is at the end.
      /// </summary>
      /// <param name="combo">Gtk.ComboBoxEntry</param>
      /// <param name="item">string value to add</param>
      private void AddComboSelectionSpecial(Gtk.ComboBoxEntry combo, string item)
      {
         // remove browse text
//         RemoveComboBoxEntry(combo, Language.GetGenericText("Browse"));

         // add item
         AddComboSelection(combo, item);

         // add browse text
//         combo.AppendText(Language.GetGenericText("Browse"));
      }

      /// <summary>
      /// Find the first entry of the item in the Gtk.ComboBoxEntry.
      /// </summary>
      /// <param name="combo">Gtk.ComboBoxEntry</param>
      /// <param name="item">string value to find</param>
      /// <returns>Position in Gtk.ComboBoxEntry, -1 if not found</returns>
      private int FindComboBoxEntry(Gtk.ComboBoxEntry combo, string item)
      {
         TreeIter iter;
         int index = -1;
         
         if (combo.Model.GetIterFirst(out iter))
         {
            do
            {
               string val = (string) combo.Model.GetValue(iter, 0);
               if (val.Equals(item))
               {
                  index = combo.Model.GetPath(iter).Indices[0];
                  break;
               }

            } while (combo.Model.IterNext(ref iter));
         }

         return index;
      }

      /// <summary>
      /// Remove the first entry of the item from the Gtk.ComboBoxEntry.
      /// </summary>
      /// <param name="combo">Gtk.ComboBoxEntry</param>
      /// <param name="item">string value to remove</param>
      private void RemoveComboBoxEntry(Gtk.ComboBoxEntry combo, string item)
      {
         int index = FindComboBoxEntry(combo, item);
         if (index != -1)
            combo.RemoveText(index);
      }

      /// <summary>
      /// Sets the active entry of the given Gtk.ComboBoxEntry.
      /// </summary>
      /// <param name="combo">Gtk.ComboBoxEntry</param>
      private void SetActiveComboBoxEntry(Gtk.ComboBoxEntry combo)
      {
         int total = combo.Model.IterNChildren();
         if (total > 0 && total != 1)
         {
            // first item
            combo.Active = 0;
         }
         else
         {
            // blank
            combo.Active = -1;
         }
      }

      /// <summary>
      /// Create the Search/Cancel buttons HBox.
      /// </summary>
      /// <returns>A HBox containing the Search/Cancel buttons</returns>
      private HBox CreateButtons()
      {
         HBox hbox = new HBox (false, 4);
         hbox.BorderWidth = 3;

         btnSearch = Button.NewWithMnemonic("_Search");
         btnSearch.Clicked += new EventHandler(btnSearch_Clicked);

         hbox.PackStart (btnSearch, false, false, 0);

         btnCancel = Button.NewWithMnemonic("_Cancel");
         btnCancel.Sensitive = false;
         btnCancel.Clicked += new EventHandler(btnCancel_Clicked);

         hbox.PackEnd (btnCancel, false, false, 0);

         return hbox;
      }
      
      /// <summary>
      /// Validate all user input.
      /// </summary>
      /// <returns>Returns true when all is valid, false otherwise.</returns>
      private bool ValidateInput()
		{
         try
         {
            try
            {
               if (cboContextLines.Active < 0 || cboContextLines.Active > Constants.MAX_CONTEXT_LINES)
               {
                  MessageBox(this, Gtk.MessageType.Warning, string.Format("The number of context lines must be between {0} and {1}.", 0, Constants.MAX_CONTEXT_LINES));
                  return false;
               }
            }
            catch (Exception ex)
            {
               System.Console.WriteLine(ex.ToString());

               MessageBox(this, Gtk.MessageType.Warning, string.Format("The number of context lines must be between {0} and {1}.", 0, Constants.MAX_CONTEXT_LINES));
               return false;
            }

            if (cboSearchFilter.ActiveText.Trim().Equals(String.Empty))
            {
               MessageBox(this, Gtk.MessageType.Warning, string.Format("You must supply the file type filter to search."));
               return false;
            }

            if (cboSearchStart.ActiveText.Trim().Equals(String.Empty))
            {
               MessageBox(this, Gtk.MessageType.Warning, string.Format("You must supply the path to begin the search."));
               return false;
            }

            if (!System.IO.Directory.Exists(cboSearchStart.ActiveText))
            {
               MessageBox(this, Gtk.MessageType.Warning, string.Format("The directory specified does not exist.\n{0}", cboSearchStart.ActiveText));
               return false;
            }

            if (cboSearchText.ActiveText.Trim().Equals(String.Empty))
            {
               MessageBox(this, Gtk.MessageType.Warning, string.Format("You must supply text for which to search."));
               return false;
            }
         }
         catch (Exception ex)
         {
            System.Console.WriteLine(ex.ToString());

            MessageBox(this, Gtk.MessageType.Warning, string.Format("Unable to validate specified parameters.  Please verify they are valid.")); 
            return false;
         }
			
			return true;
		}

      /// <summary>
      /// Display to user a selection dialog to chose a folder.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      11/06/2006  Created
      /// [Curtis_Beard]      11/13/2006  CHG: set icon to match browse button
      /// </history>
      private void BrowseForFolder()
      {
         FileChooserDialog dlg = new Gtk.FileChooserDialog("", this, FileChooserAction.SelectFolder);
         dlg.Modal = true;
         dlg.Title = "Select the folder to start the search";
         dlg.SelectMultiple = false;
         dlg.Icon = Images.GetPixbuf("folder-open.png", 16, 16);

         if (Common.IsWindows)
         {
            dlg.AddButton (Stock.Open, ResponseType.Ok);
            dlg.AddButton (Stock.Cancel, ResponseType.Cancel);
         }
         else
         {
            dlg.AddButton (Stock.Cancel, ResponseType.Cancel);
            dlg.AddButton (Stock.Open, ResponseType.Ok);
         }

         Gtk.ResponseType result = (Gtk.ResponseType)dlg.Run ();
         dlg.Hide();

         if(result == Gtk.ResponseType.Ok)
         {
            AddComboSelectionSpecial(cboSearchStart, dlg.Filename);
         }
         else
         {
            SetActiveComboBoxEntry(cboSearchStart);
         }

         dlg.Destroy();
      }

      /// <summary>
      /// Display a generic message dialog.
      /// </summary>
      /// <param name="parent">Parent window</param>
      /// <param name="type">Message type</param>
      /// <param name="text">Message</param>
      private void MessageBox(Gtk.Window parent, Gtk.MessageType type, string text)
      {
         MessageDialog dlg = new MessageDialog(this, Gtk.DialogFlags.Modal, type, 
            Gtk.ButtonsType.Close, text);
         dlg.Run();
         dlg.Destroy();
      }

      /// <summary>
      /// Log a search error for later reporting.
      /// </summary>
      /// <param name="sender">should be null</param>
      /// <param name="e">MessageEventArgs class object</param>
      private void LogSearchError(object sender, System.EventArgs e)
      {
         MessageEventArgs args = (MessageEventArgs)e;

         if (alSearchErrors == null)
            alSearchErrors = new System.Collections.ArrayList(5);

         alSearchErrors.Add(args);
      }

      /// <summary>
      /// Highlight the found matches in the given HitObject.
      /// </summary>
      /// <param name="hit">HitObject to highlight</param>
      private void HighlightText(HitObject hit)
      {
         string textToSearch = string.Empty;
         string searchText = gpGrep.SearchText;
         string tempLine = string.Empty;
         int pos = -1;
         bool highlight = false;
         string begin = string.Empty;
         string text = string.Empty;
         string end = string.Empty;

         // clear contents
         txtViewer.Buffer.Text = "";

         if (gpGrep.UseRegularExpressions)
            HighlightTextRegEx(hit);
         else
         {            
            // Loop through hits and highlight search for text
            for (int i = 0; i < hit.LineCount; i++)
            {
               TextIter locIter = txtViewer.Buffer.GetIterAtOffset(txtViewer.Buffer.Text.Length);

               //Retrieve hit text
               textToSearch = hit.RetrieveLine(i);

               tempLine = textToSearch;

               if (gpGrep.UseCaseSensitivity)
                  pos = tempLine.IndexOf(searchText);
               else
                  pos = tempLine.ToLower().IndexOf(searchText.ToLower());

               if (pos > -1)
               {
                  while (pos > -1)
                  {
                     highlight = false;

                     begin = tempLine.Substring(0, pos);
                     text = tempLine.Substring(pos, searchText.Length);
                     end = tempLine.Substring(pos + searchText.Length);

                     txtViewer.Buffer.Insert(ref locIter, begin);

                     if (gpGrep.UseWholeWordMatching)
                        highlight = Grep.WholeWordOnly(begin, end);
                     else
                        highlight = true;

                     if (highlight)
                        txtViewer.Buffer.InsertWithTagsByName(ref locIter, text, new string[1] {"highlight"});
                     else
                        txtViewer.Buffer.Insert(ref locIter, text);

                     //Check remaining for other matches
                     if (gpGrep.UseCaseSensitivity)
                        pos = end.IndexOf(searchText);
                     else
                        pos = end.ToLower().IndexOf(searchText.ToLower());

                     //if no more hits in line, insert last part
                     tempLine = end;
                     if (pos < 0)
                        txtViewer.Buffer.Insert(ref locIter, end);
                  }
               }
               else
                  txtViewer.Buffer.Insert(ref locIter, textToSearch);
            }
         }
      }

      /// <summary>
      /// Highlight the found matches in the given HitObject based on a regular expression.
      /// </summary>
      /// <param name="hit">HitObject to highlight</param>
      /// <remarks>Called from HighlightText only.</remarks>
      private void HighlightTextRegEx(HitObject hit)
      {
         string _textToSearch = string.Empty;
         string _tempString = string.Empty;
         int _lastPos = 0;
         Regex _regEx = new Regex(gpGrep.SearchText);
         MatchCollection _col;
         Match _item;

         // Loop through hits and highlight search for text
         for (int _index = 0; _index < hit.LineCount; _index++)
         {
            TextIter locIter = txtViewer.Buffer.GetIterAtOffset(txtViewer.Buffer.Text.Length);

            //Retrieve hit text
            _textToSearch = hit.RetrieveLine(_index);

            // find all reg ex matches in line
            if (gpGrep.UseCaseSensitivity && gpGrep.UseWholeWordMatching)
            {
               _regEx = new Regex("\\b\\w*" + gpGrep.SearchText + "\\w*\\b");
               _col = _regEx.Matches(_textToSearch);
            }
            else if (gpGrep.UseCaseSensitivity)
            {
               _regEx = new Regex(gpGrep.SearchText);
               _col = _regEx.Matches(_textToSearch);
            }
            else if (gpGrep.UseWholeWordMatching)
            {
               _regEx = new Regex("\\b\\w*" + gpGrep.SearchText + "\\w*\\b", RegexOptions.IgnoreCase);
               _col = _regEx.Matches(_textToSearch);
            }
            else
            {
               _regEx = new Regex(gpGrep.SearchText, RegexOptions.IgnoreCase);
               _col = _regEx.Matches(_textToSearch);
            }

            //loop through the matches
            _lastPos = 0;
            for (int _counter = 0; _counter < _col.Count; _counter++)
            {
               _item = _col[_counter];

               // set the start text
               // check for empty string to prevent assigning nothing to selection text preventing
               //   a system beep
               _tempString = _textToSearch.Substring(_lastPos, _item.Index - _lastPos);
               if (!_tempString.Equals(string.Empty))
                  txtViewer.Buffer.Insert(ref locIter, _tempString);

               //set the hit text
               txtViewer.Buffer.InsertWithTagsByName(ref locIter, _textToSearch.Substring(_item.Index, _item.Length), new string[1] {"highlight"});

               //set the end text
               if (_counter + 1 >= _col.Count)
               {
                  // no more hits so just set the rest
                  txtViewer.Buffer.Insert(ref locIter, _textToSearch.Substring(_item.Index + _item.Length));
                  _lastPos = _item.Index + _item.Length;
               }
               else
               {
                  // another hit so just set inbetween
                  txtViewer.Buffer.Insert(ref locIter, _textToSearch.Substring(_item.Index + _item.Length, _col[_counter + 1].Index - (_item.Index + _item.Length)));
                  _lastPos = _col[_counter + 1].Index;
               }
            }

            // no match, just a context line
            if (_col.Count == 0)
               txtViewer.Buffer.Insert(ref locIter, _textToSearch);
         }
      }

      /// <summary>
      /// Add the highlight tag to the TagTable for the textview.
      /// </summary>
      private void AddHighlightTextTag()
      {
         TextTag temp = txtViewer.Buffer.TagTable.Lookup("highlight");
         if (temp != null)
            txtViewer.Buffer.TagTable.Remove(temp);
         temp = null;

         TextTag tag = new TextTag("highlight");
         tag.BackgroundGdk = Common.ConvertStringToColor(Core.GeneralSettings.HighlightBackColor);
         tag.ForegroundGdk = Common.ConvertStringToColor(Core.GeneralSettings.HighlightForeColor);

         txtViewer.Buffer.TagTable.Add(tag);
      }

      /// <summary>
      /// Display any search errors in a nice dialog.
      /// </summary>
      /// <param name="sender">null</param>
      /// <param name="e">null</param>
      private void DisplaySearchErrors(object sender, System.EventArgs e)
      {
         if (alSearchErrors != null && alSearchErrors.Count > 0)
         {
            frmErrorLog dlg = new frmErrorLog(this, Gtk.DialogFlags.Modal, alSearchErrors);
            dlg.Run();
            dlg.Destroy();
         }
      }
      
      /// <summary>
      /// Loads the given Gtk.ComboBoxEntry with the values.
      /// </summary>
      /// <param name="combo">Gtk.ComboBoxEntry</param>
      /// <param name="values">string of the values to load</param>
      /// <param name="special">If true, add the Browse_Text to the Gtk.ComboBoxEntry</param>
      /// <history>
      /// [Curtis_Beard]      11/03/2006  Created
      /// </history>
      private void LoadComboBoxEntry(Gtk.ComboBoxEntry combo, string values, bool special)
      {
         if (!values.Equals(string.Empty))
         {
            string[] items = Common.GetComboBoxEntriesFromString(values);
         
            if (items.Length > 0)
            {
               for (int i = items.Length - 1; i > -1; i--)
               {
                  if (special)
                     AddComboSelectionSpecial(combo, items[i]);
                  else
                     AddComboSelection(combo, items[i]);
               }
            }
//            else if (special)
//            {
//               // no items
//               RemoveComboBoxEntry(combo, Language.GetGenericText("Browse"));
//               combo.AppendText(Language.GetGenericText("Browse"));
//            }
         }
//         else if (special)
//         {
//            // no items
//            RemoveComboBoxEntry(combo, Language.GetGenericText("Browse"));
//            combo.AppendText(Language.GetGenericText("Browse"));
//         }
      }

      /// <summary>
      /// Clears the given Gtk.ComboBoxEntry values.
      /// </summary>
      /// <param name="combo">Gtk.ComboBoxEntry</param>
      /// <param name="special">If true, add the Browse_Text to the Gtk.ComboBoxEntry</param>
      /// <history>
      /// [Curtis_Beard]      11/03/2006  Created
      /// </history>
      private void ClearComboBoxEntries(Gtk.ComboBoxEntry combo, bool special)
      {
         for (int i = 0; i < combo.Model.IterNChildren(); i++)
            combo.RemoveText(i);

         combo.Active = -1;
      }
      #endregion

      #region Sorting
      private void SetSortingFunctions()
      {
         (tvFiles.Model as ListStore).SetSortFunc(Constants.COLUMN_INDEX_FILE, new TreeIterCompareFunc(ColumnCompareName));
         (tvFiles.Model as ListStore).SetSortFunc(Constants.COLUMN_INDEX_DIRECTORY, new Gtk.TreeIterCompareFunc(ColumnCompareLocation));
         (tvFiles.Model as ListStore).SetSortFunc(Constants.COLUMN_INDEX_DATE, new Gtk.TreeIterCompareFunc(ColumnCompareDate));
         (tvFiles.Model as ListStore).SetSortFunc(Constants.COLUMN_INDEX_COUNT, new Gtk.TreeIterCompareFunc(ColumnCompareCount));
      }

      public int DefaultTreeIterCompareFunc(TreeModel _model, TreeIter a, TreeIter b)
      {
         return 0;
      }

      public int ColumnCompareName(Gtk.TreeModel model, Gtk.TreeIter tia, Gtk.TreeIter tib)     
      {
         try
         {
            return String.Compare ((string) model.GetValue (tia, Constants.COLUMN_INDEX_FILE),
               (string) model.GetValue (tib, Constants.COLUMN_INDEX_FILE));
         }
         catch {}

         return 0;
      }

      public int ColumnCompareLocation(Gtk.TreeModel model, Gtk.TreeIter tia, Gtk.TreeIter tib)     
      {
         try
         {
            return String.Compare ((string) model.GetValue (tia, Constants.COLUMN_INDEX_DIRECTORY),
               (string) model.GetValue (tib, Constants.COLUMN_INDEX_DIRECTORY));
         }
         catch {}

         return 0;
      }

      public int ColumnCompareDate(Gtk.TreeModel model, Gtk.TreeIter tia, Gtk.TreeIter tib)     
      {
         try
         {
            // Parse the two objects passed as DateTime.
            DateTime first = DateTime.Parse((string)model.GetValue (tia, Constants.COLUMN_INDEX_DATE));
            DateTime second = DateTime.Parse((string)model.GetValue (tib, Constants.COLUMN_INDEX_DATE));

            return DateTime.Compare(first, second);

            //return String.Compare ((string) model.GetValue (tia, Constants.COLUMN_INDEX_DATE),
            //   (string) model.GetValue (tib, Constants.COLUMN_INDEX_DATE));
         }
         catch {}

         return 0;
      }

      public int ColumnCompareCount(Gtk.TreeModel model, Gtk.TreeIter tia, Gtk.TreeIter tib)     
      {
         int _returnVal = 0;

         try
         {
            // Parse the two objects passed as integers.
            int firstInt = int.Parse((string)model.GetValue (tia, Constants.COLUMN_INDEX_COUNT));
            int secondInt = int.Parse((string)model.GetValue (tib, Constants.COLUMN_INDEX_COUNT));

            // Compare the two integers.
            if (firstInt < secondInt)
               _returnVal = -1;
            else if (firstInt > secondInt)
               _returnVal = 1;
         }
         catch {}

         return _returnVal;
      }
      #endregion

      #region Grep Events
      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void UpdateStatusBar(object sender, System.EventArgs e)
      {
         sbStatus.Push(1, sender.ToString());
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void AddFileFound(object sender, System.EventArgs e)
      {
         System.IO.FileInfo file = (System.IO.FileInfo)sender;
         FileEventArgs args = (FileEventArgs)e;

         if (gpGrep.UseNegation)
            (tvFiles.Model as ListStore).AppendValues(file.Name, file.DirectoryName, file.LastWriteTime.ToString(), "0", args.Index);
         else
            (tvFiles.Model as ListStore).AppendValues(file.Name, file.DirectoryName, file.LastWriteTime.ToString(), "1", args.Index);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void UpdateCount(object sender, System.EventArgs e)
      {
         HitObject hit = (HitObject)sender;

         TreeIter iter = new TreeIter();
         int index = 0;

         foreach (object[] row in (tvFiles.Model as ListStore))
         {
            if (row[Constants.COLUMN_INDEX_GREP_INDEX].ToString().Equals(hit.Index.ToString()))
            {
               //found it
               if ((tvFiles.Model as ListStore).GetIter(out iter, new TreePath(index.ToString())))
               {
                  (tvFiles.Model as ListStore).SetValue(iter, Constants.COLUMN_INDEX_COUNT, hit.HitCount.ToString());
               }

               break;
            }

            index += 1;
         }
      }

      private void UpdateAllCounts(object sender, System.EventArgs e)
      {
         TreeIter iter = new TreeIter();

         // go through each result and update hit count
         for (int i = 0; i < (tvFiles.Model as ListStore).IterNChildren(); i++)
         {
            // search grep hit collection
            for (int j = 0; j < gpGrep.Greps.Count; j++)
            {
               HitObject hit = gpGrep.RetrieveHitObject(j);
               if ((tvFiles.Model as ListStore).GetIter(out iter, new TreePath(i.ToString())))
               {
                  if ((int)(tvFiles.Model as ListStore).GetValue(iter, Constants.COLUMN_INDEX_GREP_INDEX) == hit.Index )
                  {
                     // found it, so update
                     (tvFiles.Model as ListStore).SetValue(iter, Constants.COLUMN_INDEX_COUNT, hit.HitCount.ToString());
                     break;
                  }
               }
            }
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="file"></param>
      private void gpGrep_SearchingFile(System.IO.FileInfo file)
      {
         Gtk.Application.Invoke("Searching " + file.FullName, null, new System.EventHandler(UpdateStatusBar));
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="file"></param>
      /// <param name="index"></param>
      private void gpGrep_FileHit(System.IO.FileInfo file, int index)
      {
         FileEventArgs args = new FileEventArgs(index);
         Gtk.Application.Invoke(file, args, new System.EventHandler(AddFileFound));
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="hit"></param>
      /// <param name="index"></param>
      private void gpGrep_LineHit(HitObject hit, int index)
      {
         //Gtk.Application.Invoke(hit, null, new System.EventHandler(UpdateCount));
      }      

      /// <summary>
      /// 
      /// </summary>
      /// <param name="message"></param>
      private void gpGrep_SearchCancel(string message)
      {
         Gtk.Application.Invoke(null, new SearchEventArgs(true), new System.EventHandler(SetSearchState));
         Gtk.Application.Invoke("Search Cancelled", null, new System.EventHandler(UpdateStatusBar));
         Gtk.Application.Invoke(null, null, new System.EventHandler(UpdateAllCounts));
         Gtk.Application.Invoke(new System.EventHandler(DisplaySearchErrors));
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="message"></param>
      private void gpGrep_SearchComplete(string message)
      {
         Gtk.Application.Invoke(null, new SearchEventArgs(true), new System.EventHandler(SetSearchState));
         Gtk.Application.Invoke("Search Completed", null, new System.EventHandler(UpdateStatusBar));
         Gtk.Application.Invoke(null, null, new System.EventHandler(UpdateAllCounts));
         Gtk.Application.Invoke(new System.EventHandler(DisplaySearchErrors));
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="file"></param>
      /// <param name="message"></param>
      private void gpGrep_SearchError(System.IO.FileInfo file, string message)
      {
         string msg = string.Empty;

         if (file == null)
            msg = string.Format("An error occurred during the search. [{0}]", message);
         else
            msg = string.Format("An error occurred searching file: {0}. [{1}]", file.FullName, message);

         Gtk.Application.Invoke(null, new MessageEventArgs(file, msg), new System.EventHandler(LogSearchError));
      }
      #endregion
   }

   #region EventArgs Classes
   /// <summary>
   /// Wrapper class to provide EventArgs with an index value.
   /// </summary>
   internal class FileEventArgs : System.EventArgs
   {
      private int m_Index = -1;

      /// <summary>
      /// Initializes the class with an index value.
      /// </summary>
      /// <param name="index">Index event argument</param>
      public FileEventArgs(int index) :  base()
      {
         m_Index = index;
      }

      /// <summary>
      /// Gets the index from the event arguments.
      /// </summary>
      public int Index
      {
         get { return m_Index; }
         set { m_Index = value; }
      }
   }

   internal class SearchEventArgs : System.EventArgs
   {
      private bool state = false;

      public SearchEventArgs(bool enable)
      {
         state = enable;
      }

      public bool Enable
      {
         get { return state; }
         set { state = value; }
      }
   }

   public class MessageEventArgs : System.EventArgs
   {
      private System.IO.FileInfo errFile;
      private string msg = string.Empty;

      public MessageEventArgs(System.IO.FileInfo file, string message)
      {
         errFile = file;
         msg = message;
      }

      public System.IO.FileInfo ErrorFile
      {
         get { return errFile; }
         set { errFile = value; }
      }

      public string Message
      {
         get { return msg; }
         set { msg = value; }
      }
   }
   #endregion
}