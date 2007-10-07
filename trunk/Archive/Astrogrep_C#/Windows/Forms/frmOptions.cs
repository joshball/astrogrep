using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AstroGrep.Windows.Forms
{
   /// <summary>
   /// Used to display user definable settings for this application.
   /// </summary>
   /// <history>
   /// [Curtis_Beard]		07/19/2006	ADD: 1512027, Multiple text editors and new options dialog
   /// </history>
	public class frmOptions : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      private System.Windows.Forms.Panel SelectionPanel;
      private System.Windows.Forms.Button btnOK;
      private System.Windows.Forms.Button btnCancel;
      private System.Windows.Forms.Panel GeneralPanel;
      private System.Windows.Forms.ComboBox cboPathMRUCount;
      private System.Windows.Forms.CheckBox chkRightClickOption;
      private System.Windows.Forms.Label lblStoredPaths;
      private System.Windows.Forms.Panel ResultsPanel;
      private AstroGrep.Windows.Controls.ColorButton BackColorButton;
      private AstroGrep.Windows.Controls.ColorButton ForeColorButton;
      private System.Windows.Forms.Label BackColorLabel;
      private System.Windows.Forms.Label ForeColorLabel;
      private System.Windows.Forms.Panel TextEditorsPanel;
      private System.Windows.Forms.Button btnEdit;
      private System.Windows.Forms.Button btnRemove;
      private System.Windows.Forms.Button btnAdd;
      private System.Windows.Forms.ListView TextEditorsList;
      private System.Windows.Forms.ColumnHeader ColumnType;
      private System.Windows.Forms.ColumnHeader ColumnEditor;
      private System.Windows.Forms.ColumnHeader ColumnArguments;
      private System.Windows.Forms.Panel BottomPanel;  
      private AstroGrep.Windows.Controls.OptionsCategoryItem GeneralItem;  
      private AstroGrep.Windows.Controls.OptionsCategoryItem TextEditorsItem;  
      private AstroGrep.Windows.Controls.OptionsCategoryItem ResultsItem;
      private System.Windows.Forms.GroupBox grpResultMatch;
      private System.Windows.Forms.GroupBox grpResultWindow;  
      private AstroGrep.Windows.Controls.ColorButton btnResultsWindowBackColor;  
      private AstroGrep.Windows.Controls.ColorButton btnResultsWindowForeColor;
      private System.Windows.Forms.Label lblResultsWindowBack;
      private System.Windows.Forms.Label lblResultsWindowFore;  
      private System.Windows.Forms.RichTextBox rtxtResultsPreview;  
      private System.Windows.Forms.Label lblResultPreview;  
      private System.Windows.Forms.ComboBox cboLanguage;  
      private System.Windows.Forms.TextBox txtExcludeList;  
      private AstroGrep.Windows.Controls.OptionsCategoryItem PluginsItem;  
      private System.Windows.Forms.ColumnHeader PluginsColumnEnabled;  
      private System.Windows.Forms.ColumnHeader PluginsColumnName;  
      private System.Windows.Forms.ColumnHeader PluginsColumnExt;  
      private System.Windows.Forms.Panel PluginsPanel;  
      private System.Windows.Forms.ListView PluginsList;  
      private System.Windows.Forms.GroupBox PluginDetailsGroup;  
      private System.Windows.Forms.Label lblPluginDescription;  
      private System.Windows.Forms.Label lblPluginDetailName;  
      private System.Windows.Forms.Label lblPluginDetailVersion;  
      private System.Windows.Forms.Label lblPluginAuthor;  
      private System.Windows.Forms.Label lblPluginName;  
      private System.Windows.Forms.Label lblPluginVersion;  
      private System.Windows.Forms.Label lblPluginDetailAuthor;  
      private System.Windows.Forms.GroupBox LanguageGroup;  
      private System.Windows.Forms.GroupBox ExtensionExclusionGroup;  
      private System.Windows.Forms.CheckBox chkDesktopShortcut;  
      private System.Windows.Forms.CheckBox chkStartMenuShortcut;
      private System.Windows.Forms.GroupBox ShortcutGroup;

      private bool __LanguageChange = false;

      private enum OptionPanels : int
      {
         General = 0,
         TextEditors,
         Results,
         Plugins
      }      

      /// <summary>
      /// 
      /// </summary>
		public frmOptions()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         SelectionPanel.Paint += new PaintEventHandler(SelectionPanel_Paint);
         ForeColorButton.ColorChange += new AstroGrep.Windows.Controls.ColorButton.ColorChangeHandler(NewColor);
         BackColorButton.ColorChange += new AstroGrep.Windows.Controls.ColorButton.ColorChangeHandler(NewColor);
         btnResultsWindowForeColor.ColorChange += new AstroGrep.Windows.Controls.ColorButton.ColorChangeHandler(NewColor);
         btnResultsWindowBackColor.ColorChange += new AstroGrep.Windows.Controls.ColorButton.ColorChangeHandler(NewColor);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmOptions));
         this.btnOK = new System.Windows.Forms.Button();
         this.btnCancel = new System.Windows.Forms.Button();
         this.SelectionPanel = new System.Windows.Forms.Panel();
         this.ResultsItem = new AstroGrep.Windows.Controls.OptionsCategoryItem();
         this.GeneralItem = new AstroGrep.Windows.Controls.OptionsCategoryItem();
         this.TextEditorsItem = new AstroGrep.Windows.Controls.OptionsCategoryItem();
         this.PluginsItem = new AstroGrep.Windows.Controls.OptionsCategoryItem();
         this.GeneralPanel = new System.Windows.Forms.Panel();
         this.ShortcutGroup = new System.Windows.Forms.GroupBox();
         this.chkStartMenuShortcut = new System.Windows.Forms.CheckBox();
         this.chkDesktopShortcut = new System.Windows.Forms.CheckBox();
         this.chkRightClickOption = new System.Windows.Forms.CheckBox();
         this.ExtensionExclusionGroup = new System.Windows.Forms.GroupBox();
         this.txtExcludeList = new System.Windows.Forms.TextBox();
         this.LanguageGroup = new System.Windows.Forms.GroupBox();
         this.cboLanguage = new System.Windows.Forms.ComboBox();
         this.cboPathMRUCount = new System.Windows.Forms.ComboBox();
         this.lblStoredPaths = new System.Windows.Forms.Label();
         this.ResultsPanel = new System.Windows.Forms.Panel();
         this.lblResultPreview = new System.Windows.Forms.Label();
         this.rtxtResultsPreview = new System.Windows.Forms.RichTextBox();
         this.grpResultWindow = new System.Windows.Forms.GroupBox();
         this.btnResultsWindowBackColor = new AstroGrep.Windows.Controls.ColorButton();
         this.btnResultsWindowForeColor = new AstroGrep.Windows.Controls.ColorButton();
         this.lblResultsWindowBack = new System.Windows.Forms.Label();
         this.lblResultsWindowFore = new System.Windows.Forms.Label();
         this.grpResultMatch = new System.Windows.Forms.GroupBox();
         this.BackColorButton = new AstroGrep.Windows.Controls.ColorButton();
         this.ForeColorButton = new AstroGrep.Windows.Controls.ColorButton();
         this.BackColorLabel = new System.Windows.Forms.Label();
         this.ForeColorLabel = new System.Windows.Forms.Label();
         this.TextEditorsPanel = new System.Windows.Forms.Panel();
         this.btnEdit = new System.Windows.Forms.Button();
         this.btnRemove = new System.Windows.Forms.Button();
         this.btnAdd = new System.Windows.Forms.Button();
         this.TextEditorsList = new System.Windows.Forms.ListView();
         this.ColumnType = new System.Windows.Forms.ColumnHeader();
         this.ColumnEditor = new System.Windows.Forms.ColumnHeader();
         this.ColumnArguments = new System.Windows.Forms.ColumnHeader();
         this.BottomPanel = new System.Windows.Forms.Panel();
         this.PluginsPanel = new System.Windows.Forms.Panel();
         this.PluginDetailsGroup = new System.Windows.Forms.GroupBox();
         this.lblPluginDetailAuthor = new System.Windows.Forms.Label();
         this.lblPluginVersion = new System.Windows.Forms.Label();
         this.lblPluginName = new System.Windows.Forms.Label();
         this.lblPluginDescription = new System.Windows.Forms.Label();
         this.lblPluginAuthor = new System.Windows.Forms.Label();
         this.lblPluginDetailVersion = new System.Windows.Forms.Label();
         this.lblPluginDetailName = new System.Windows.Forms.Label();
         this.PluginsList = new System.Windows.Forms.ListView();
         this.PluginsColumnEnabled = new System.Windows.Forms.ColumnHeader();
         this.PluginsColumnName = new System.Windows.Forms.ColumnHeader();
         this.PluginsColumnExt = new System.Windows.Forms.ColumnHeader();
         this.SelectionPanel.SuspendLayout();
         this.GeneralPanel.SuspendLayout();
         this.ShortcutGroup.SuspendLayout();
         this.ExtensionExclusionGroup.SuspendLayout();
         this.LanguageGroup.SuspendLayout();
         this.ResultsPanel.SuspendLayout();
         this.grpResultWindow.SuspendLayout();
         this.grpResultMatch.SuspendLayout();
         this.TextEditorsPanel.SuspendLayout();
         this.BottomPanel.SuspendLayout();
         this.PluginsPanel.SuspendLayout();
         this.PluginDetailsGroup.SuspendLayout();
         this.SuspendLayout();
         // 
         // btnOK
         // 
         this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnOK.Location = new System.Drawing.Point(352, 8);
         this.btnOK.Name = "btnOK";
         this.btnOK.TabIndex = 0;
         this.btnOK.Text = "&OK";
         this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
         // 
         // btnCancel
         // 
         this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnCancel.Location = new System.Drawing.Point(432, 8);
         this.btnCancel.Name = "btnCancel";
         this.btnCancel.TabIndex = 1;
         this.btnCancel.Text = "&Cancel";
         this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
         // 
         // SelectionPanel
         // 
         this.SelectionPanel.BackColor = System.Drawing.Color.White;
         this.SelectionPanel.Controls.Add(this.ResultsItem);
         this.SelectionPanel.Controls.Add(this.GeneralItem);
         this.SelectionPanel.Controls.Add(this.TextEditorsItem);
         this.SelectionPanel.Controls.Add(this.PluginsItem);
         this.SelectionPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.SelectionPanel.Location = new System.Drawing.Point(0, 0);
         this.SelectionPanel.Name = "SelectionPanel";
         this.SelectionPanel.Size = new System.Drawing.Size(512, 66);
         this.SelectionPanel.TabIndex = 2;
         // 
         // ResultsItem
         // 
         this.ResultsItem.Image = ((System.Drawing.Image)(resources.GetObject("ResultsItem.Image")));
         this.ResultsItem.IsMnemonicKeyVisible = false;
         this.ResultsItem.IsSelected = false;
         this.ResultsItem.Location = new System.Drawing.Point(152, 0);
         this.ResultsItem.Name = "ResultsItem";
         this.ResultsItem.Size = new System.Drawing.Size(68, 64);
         this.ResultsItem.TabIndex = 5;
         this.ResultsItem.Text = "&Results";
         this.ResultsItem.Click += new System.EventHandler(this.ResultsItem_Click);
         // 
         // GeneralItem
         // 
         this.GeneralItem.Image = ((System.Drawing.Image)(resources.GetObject("GeneralItem.Image")));
         this.GeneralItem.IsMnemonicKeyVisible = false;
         this.GeneralItem.IsSelected = false;
         this.GeneralItem.Location = new System.Drawing.Point(8, 0);
         this.GeneralItem.Name = "GeneralItem";
         this.GeneralItem.Size = new System.Drawing.Size(68, 64);
         this.GeneralItem.TabIndex = 3;
         this.GeneralItem.Text = "&General";
         this.GeneralItem.Click += new System.EventHandler(this.GeneralItem_Click);
         // 
         // TextEditorsItem
         // 
         this.TextEditorsItem.Image = ((System.Drawing.Image)(resources.GetObject("TextEditorsItem.Image")));
         this.TextEditorsItem.IsMnemonicKeyVisible = false;
         this.TextEditorsItem.IsSelected = false;
         this.TextEditorsItem.Location = new System.Drawing.Point(80, 0);
         this.TextEditorsItem.Name = "TextEditorsItem";
         this.TextEditorsItem.Size = new System.Drawing.Size(68, 64);
         this.TextEditorsItem.TabIndex = 4;
         this.TextEditorsItem.Text = "&Text Editors";
         this.TextEditorsItem.Click += new System.EventHandler(this.TextEditorsItem_Click);
         // 
         // PluginsItem
         // 
         this.PluginsItem.Image = ((System.Drawing.Image)(resources.GetObject("PluginsItem.Image")));
         this.PluginsItem.IsMnemonicKeyVisible = false;
         this.PluginsItem.IsSelected = false;
         this.PluginsItem.Location = new System.Drawing.Point(224, 0);
         this.PluginsItem.Name = "PluginsItem";
         this.PluginsItem.Size = new System.Drawing.Size(68, 64);
         this.PluginsItem.TabIndex = 6;
         this.PluginsItem.Text = "&Plugins";
         this.PluginsItem.Click += new System.EventHandler(this.PluginsItem_Click);
         // 
         // GeneralPanel
         // 
         this.GeneralPanel.Controls.Add(this.ShortcutGroup);
         this.GeneralPanel.Controls.Add(this.ExtensionExclusionGroup);
         this.GeneralPanel.Controls.Add(this.LanguageGroup);
         this.GeneralPanel.Controls.Add(this.cboPathMRUCount);
         this.GeneralPanel.Controls.Add(this.lblStoredPaths);
         this.GeneralPanel.Location = new System.Drawing.Point(8, 88);
         this.GeneralPanel.Name = "GeneralPanel";
         this.GeneralPanel.Size = new System.Drawing.Size(496, 224);
         this.GeneralPanel.TabIndex = 4;
         // 
         // ShortcutGroup
         // 
         this.ShortcutGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.ShortcutGroup.Controls.Add(this.chkStartMenuShortcut);
         this.ShortcutGroup.Controls.Add(this.chkDesktopShortcut);
         this.ShortcutGroup.Controls.Add(this.chkRightClickOption);
         this.ShortcutGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.ShortcutGroup.Location = new System.Drawing.Point(8, 40);
         this.ShortcutGroup.Name = "ShortcutGroup";
         this.ShortcutGroup.Size = new System.Drawing.Size(480, 80);
         this.ShortcutGroup.TabIndex = 30;
         this.ShortcutGroup.TabStop = false;
         this.ShortcutGroup.Text = "Shortcuts";
         // 
         // chkStartMenuShortcut
         // 
         this.chkStartMenuShortcut.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkStartMenuShortcut.Location = new System.Drawing.Point(232, 48);
         this.chkStartMenuShortcut.Name = "chkStartMenuShortcut";
         this.chkStartMenuShortcut.Size = new System.Drawing.Size(240, 24);
         this.chkStartMenuShortcut.TabIndex = 29;
         this.chkStartMenuShortcut.Text = "Start Menu Shortcut";
         // 
         // chkDesktopShortcut
         // 
         this.chkDesktopShortcut.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkDesktopShortcut.Location = new System.Drawing.Point(8, 48);
         this.chkDesktopShortcut.Name = "chkDesktopShortcut";
         this.chkDesktopShortcut.Size = new System.Drawing.Size(224, 24);
         this.chkDesktopShortcut.TabIndex = 28;
         this.chkDesktopShortcut.Text = "Desktop Shortcut";
         // 
         // chkRightClickOption
         // 
         this.chkRightClickOption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.chkRightClickOption.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkRightClickOption.Location = new System.Drawing.Point(8, 24);
         this.chkRightClickOption.Name = "chkRightClickOption";
         this.chkRightClickOption.Size = new System.Drawing.Size(464, 17);
         this.chkRightClickOption.TabIndex = 20;
         this.chkRightClickOption.Text = "Set right-click option on folders";
         // 
         // ExtensionExclusionGroup
         // 
         this.ExtensionExclusionGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.ExtensionExclusionGroup.Controls.Add(this.txtExcludeList);
         this.ExtensionExclusionGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.ExtensionExclusionGroup.Location = new System.Drawing.Point(8, 192);
         this.ExtensionExclusionGroup.Name = "ExtensionExclusionGroup";
         this.ExtensionExclusionGroup.Size = new System.Drawing.Size(480, 56);
         this.ExtensionExclusionGroup.TabIndex = 27;
         this.ExtensionExclusionGroup.TabStop = false;
         this.ExtensionExclusionGroup.Text = "Exclude File Extensions";
         // 
         // txtExcludeList
         // 
         this.txtExcludeList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.txtExcludeList.Location = new System.Drawing.Point(16, 24);
         this.txtExcludeList.Name = "txtExcludeList";
         this.txtExcludeList.Size = new System.Drawing.Size(448, 20);
         this.txtExcludeList.TabIndex = 25;
         this.txtExcludeList.Text = "";
         // 
         // LanguageGroup
         // 
         this.LanguageGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.LanguageGroup.Controls.Add(this.cboLanguage);
         this.LanguageGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.LanguageGroup.Location = new System.Drawing.Point(8, 128);
         this.LanguageGroup.Name = "LanguageGroup";
         this.LanguageGroup.Size = new System.Drawing.Size(480, 56);
         this.LanguageGroup.TabIndex = 26;
         this.LanguageGroup.TabStop = false;
         this.LanguageGroup.Text = "Language";
         // 
         // cboLanguage
         // 
         this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cboLanguage.Items.AddRange(new object[] {
                                                         "English",
                                                         "Español",
                                                         "Deutsch",
                                                         "Italiano"});
         this.cboLanguage.Location = new System.Drawing.Point(16, 24);
         this.cboLanguage.Name = "cboLanguage";
         this.cboLanguage.Size = new System.Drawing.Size(144, 21);
         this.cboLanguage.TabIndex = 23;
         // 
         // cboPathMRUCount
         // 
         this.cboPathMRUCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cboPathMRUCount.Items.AddRange(new object[] {
                                                             "1",
                                                             "2",
                                                             "3",
                                                             "4",
                                                             "5",
                                                             "6",
                                                             "7",
                                                             "8",
                                                             "9",
                                                             "10",
                                                             "11",
                                                             "12",
                                                             "13",
                                                             "14",
                                                             "15",
                                                             "16",
                                                             "17",
                                                             "18",
                                                             "19",
                                                             "20",
                                                             "21",
                                                             "22",
                                                             "23",
                                                             "24",
                                                             "25"});
         this.cboPathMRUCount.Location = new System.Drawing.Point(8, 8);
         this.cboPathMRUCount.Name = "cboPathMRUCount";
         this.cboPathMRUCount.Size = new System.Drawing.Size(64, 21);
         this.cboPathMRUCount.TabIndex = 19;
         // 
         // lblStoredPaths
         // 
         this.lblStoredPaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lblStoredPaths.BackColor = System.Drawing.SystemColors.Control;
         this.lblStoredPaths.Cursor = System.Windows.Forms.Cursors.Default;
         this.lblStoredPaths.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
         this.lblStoredPaths.ForeColor = System.Drawing.SystemColors.ControlText;
         this.lblStoredPaths.Location = new System.Drawing.Point(80, 8);
         this.lblStoredPaths.Name = "lblStoredPaths";
         this.lblStoredPaths.RightToLeft = System.Windows.Forms.RightToLeft.No;
         this.lblStoredPaths.Size = new System.Drawing.Size(408, 20);
         this.lblStoredPaths.TabIndex = 21;
         this.lblStoredPaths.Text = "Number of most recently used paths to store";
         this.lblStoredPaths.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // ResultsPanel
         // 
         this.ResultsPanel.Controls.Add(this.lblResultPreview);
         this.ResultsPanel.Controls.Add(this.rtxtResultsPreview);
         this.ResultsPanel.Controls.Add(this.grpResultWindow);
         this.ResultsPanel.Controls.Add(this.grpResultMatch);
         this.ResultsPanel.Location = new System.Drawing.Point(8, 80);
         this.ResultsPanel.Name = "ResultsPanel";
         this.ResultsPanel.Size = new System.Drawing.Size(480, 240);
         this.ResultsPanel.TabIndex = 6;
         // 
         // lblResultPreview
         // 
         this.lblResultPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lblResultPreview.Location = new System.Drawing.Point(8, 168);
         this.lblResultPreview.Name = "lblResultPreview";
         this.lblResultPreview.Size = new System.Drawing.Size(464, 16);
         this.lblResultPreview.TabIndex = 22;
         this.lblResultPreview.Text = "Results Preview";
         // 
         // rtxtResultsPreview
         // 
         this.rtxtResultsPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.rtxtResultsPreview.Location = new System.Drawing.Point(8, 184);
         this.rtxtResultsPreview.Name = "rtxtResultsPreview";
         this.rtxtResultsPreview.ReadOnly = true;
         this.rtxtResultsPreview.Size = new System.Drawing.Size(464, 40);
         this.rtxtResultsPreview.TabIndex = 21;
         this.rtxtResultsPreview.Text = "(21)  Example results line and, match, displayed";
         // 
         // grpResultWindow
         // 
         this.grpResultWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.grpResultWindow.Controls.Add(this.btnResultsWindowBackColor);
         this.grpResultWindow.Controls.Add(this.btnResultsWindowForeColor);
         this.grpResultWindow.Controls.Add(this.lblResultsWindowBack);
         this.grpResultWindow.Controls.Add(this.lblResultsWindowFore);
         this.grpResultWindow.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.grpResultWindow.Location = new System.Drawing.Point(8, 72);
         this.grpResultWindow.Name = "grpResultWindow";
         this.grpResultWindow.Size = new System.Drawing.Size(464, 56);
         this.grpResultWindow.TabIndex = 20;
         this.grpResultWindow.TabStop = false;
         this.grpResultWindow.Text = "Results Window";
         // 
         // btnResultsWindowBackColor
         // 
         this.btnResultsWindowBackColor.Location = new System.Drawing.Point(376, 24);
         this.btnResultsWindowBackColor.Name = "btnResultsWindowBackColor";
         this.btnResultsWindowBackColor.SelectedColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
         this.btnResultsWindowBackColor.TabIndex = 22;
         // 
         // btnResultsWindowForeColor
         // 
         this.btnResultsWindowForeColor.Location = new System.Drawing.Point(144, 24);
         this.btnResultsWindowForeColor.Name = "btnResultsWindowForeColor";
         this.btnResultsWindowForeColor.SelectedColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(0)));
         this.btnResultsWindowForeColor.TabIndex = 21;
         // 
         // lblResultsWindowBack
         // 
         this.lblResultsWindowBack.Location = new System.Drawing.Point(240, 24);
         this.lblResultsWindowBack.Name = "lblResultsWindowBack";
         this.lblResultsWindowBack.Size = new System.Drawing.Size(136, 23);
         this.lblResultsWindowBack.TabIndex = 20;
         this.lblResultsWindowBack.Text = "Back Color";
         this.lblResultsWindowBack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // lblResultsWindowFore
         // 
         this.lblResultsWindowFore.Location = new System.Drawing.Point(8, 24);
         this.lblResultsWindowFore.Name = "lblResultsWindowFore";
         this.lblResultsWindowFore.Size = new System.Drawing.Size(136, 23);
         this.lblResultsWindowFore.TabIndex = 19;
         this.lblResultsWindowFore.Text = "Fore Color";
         this.lblResultsWindowFore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // grpResultMatch
         // 
         this.grpResultMatch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.grpResultMatch.Controls.Add(this.BackColorButton);
         this.grpResultMatch.Controls.Add(this.ForeColorButton);
         this.grpResultMatch.Controls.Add(this.BackColorLabel);
         this.grpResultMatch.Controls.Add(this.ForeColorLabel);
         this.grpResultMatch.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.grpResultMatch.Location = new System.Drawing.Point(8, 8);
         this.grpResultMatch.Name = "grpResultMatch";
         this.grpResultMatch.Size = new System.Drawing.Size(464, 56);
         this.grpResultMatch.TabIndex = 19;
         this.grpResultMatch.TabStop = false;
         this.grpResultMatch.Text = "Results Match";
         // 
         // BackColorButton
         // 
         this.BackColorButton.Location = new System.Drawing.Point(376, 24);
         this.BackColorButton.Name = "BackColorButton";
         this.BackColorButton.SelectedColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
         this.BackColorButton.TabIndex = 18;
         // 
         // ForeColorButton
         // 
         this.ForeColorButton.Location = new System.Drawing.Point(144, 24);
         this.ForeColorButton.Name = "ForeColorButton";
         this.ForeColorButton.SelectedColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(0)));
         this.ForeColorButton.TabIndex = 17;
         // 
         // BackColorLabel
         // 
         this.BackColorLabel.Location = new System.Drawing.Point(240, 24);
         this.BackColorLabel.Name = "BackColorLabel";
         this.BackColorLabel.Size = new System.Drawing.Size(136, 23);
         this.BackColorLabel.TabIndex = 16;
         this.BackColorLabel.Text = "Back Color";
         this.BackColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // ForeColorLabel
         // 
         this.ForeColorLabel.Location = new System.Drawing.Point(8, 24);
         this.ForeColorLabel.Name = "ForeColorLabel";
         this.ForeColorLabel.Size = new System.Drawing.Size(136, 23);
         this.ForeColorLabel.TabIndex = 15;
         this.ForeColorLabel.Text = "Fore Color";
         this.ForeColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // TextEditorsPanel
         // 
         this.TextEditorsPanel.Controls.Add(this.btnEdit);
         this.TextEditorsPanel.Controls.Add(this.btnRemove);
         this.TextEditorsPanel.Controls.Add(this.btnAdd);
         this.TextEditorsPanel.Controls.Add(this.TextEditorsList);
         this.TextEditorsPanel.Location = new System.Drawing.Point(104, 80);
         this.TextEditorsPanel.Name = "TextEditorsPanel";
         this.TextEditorsPanel.Size = new System.Drawing.Size(328, 240);
         this.TextEditorsPanel.TabIndex = 7;
         // 
         // btnEdit
         // 
         this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnEdit.Location = new System.Drawing.Point(96, 208);
         this.btnEdit.Name = "btnEdit";
         this.btnEdit.TabIndex = 12;
         this.btnEdit.Text = "&Edit...";
         this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
         // 
         // btnRemove
         // 
         this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnRemove.Location = new System.Drawing.Point(184, 208);
         this.btnRemove.Name = "btnRemove";
         this.btnRemove.TabIndex = 11;
         this.btnRemove.Text = "&Delete";
         this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
         // 
         // btnAdd
         // 
         this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnAdd.Location = new System.Drawing.Point(8, 208);
         this.btnAdd.Name = "btnAdd";
         this.btnAdd.TabIndex = 10;
         this.btnAdd.Text = "&Add...";
         this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
         // 
         // TextEditorsList
         // 
         this.TextEditorsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.TextEditorsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                          this.ColumnType,
                                                                                          this.ColumnEditor,
                                                                                          this.ColumnArguments});
         this.TextEditorsList.FullRowSelect = true;
         this.TextEditorsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
         this.TextEditorsList.HideSelection = false;
         this.TextEditorsList.Location = new System.Drawing.Point(8, 8);
         this.TextEditorsList.MultiSelect = false;
         this.TextEditorsList.Name = "TextEditorsList";
         this.TextEditorsList.Size = new System.Drawing.Size(312, 184);
         this.TextEditorsList.TabIndex = 4;
         this.TextEditorsList.View = System.Windows.Forms.View.Details;
         this.TextEditorsList.DoubleClick += new System.EventHandler(this.TextEditorsList_DoubleClick);
         this.TextEditorsList.SelectedIndexChanged += new System.EventHandler(this.TextEditorsList_SelectedIndexChanged);
         // 
         // ColumnType
         // 
         this.ColumnType.Text = "File Type";
         this.ColumnType.Width = 100;
         // 
         // ColumnEditor
         // 
         this.ColumnEditor.Text = "Text Editor";
         this.ColumnEditor.Width = 250;
         // 
         // ColumnArguments
         // 
         this.ColumnArguments.Text = "Command Line";
         this.ColumnArguments.Width = 100;
         // 
         // BottomPanel
         // 
         this.BottomPanel.Controls.Add(this.btnOK);
         this.BottomPanel.Controls.Add(this.btnCancel);
         this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.BottomPanel.Location = new System.Drawing.Point(0, 334);
         this.BottomPanel.Name = "BottomPanel";
         this.BottomPanel.Size = new System.Drawing.Size(512, 40);
         this.BottomPanel.TabIndex = 8;
         // 
         // PluginsPanel
         // 
         this.PluginsPanel.Controls.Add(this.PluginDetailsGroup);
         this.PluginsPanel.Controls.Add(this.PluginsList);
         this.PluginsPanel.Location = new System.Drawing.Point(8, 72);
         this.PluginsPanel.Name = "PluginsPanel";
         this.PluginsPanel.Size = new System.Drawing.Size(488, 240);
         this.PluginsPanel.TabIndex = 9;
         // 
         // PluginDetailsGroup
         // 
         this.PluginDetailsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.PluginDetailsGroup.Controls.Add(this.lblPluginDetailAuthor);
         this.PluginDetailsGroup.Controls.Add(this.lblPluginVersion);
         this.PluginDetailsGroup.Controls.Add(this.lblPluginName);
         this.PluginDetailsGroup.Controls.Add(this.lblPluginDescription);
         this.PluginDetailsGroup.Controls.Add(this.lblPluginAuthor);
         this.PluginDetailsGroup.Controls.Add(this.lblPluginDetailVersion);
         this.PluginDetailsGroup.Controls.Add(this.lblPluginDetailName);
         this.PluginDetailsGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.PluginDetailsGroup.Location = new System.Drawing.Point(8, 112);
         this.PluginDetailsGroup.Name = "PluginDetailsGroup";
         this.PluginDetailsGroup.Size = new System.Drawing.Size(472, 120);
         this.PluginDetailsGroup.TabIndex = 1;
         this.PluginDetailsGroup.TabStop = false;
         this.PluginDetailsGroup.Text = "Plugin Details";
         // 
         // lblPluginDetailAuthor
         // 
         this.lblPluginDetailAuthor.Location = new System.Drawing.Point(16, 88);
         this.lblPluginDetailAuthor.Name = "lblPluginDetailAuthor";
         this.lblPluginDetailAuthor.Size = new System.Drawing.Size(80, 23);
         this.lblPluginDetailAuthor.TabIndex = 7;
         this.lblPluginDetailAuthor.Text = "Author:";
         // 
         // lblPluginVersion
         // 
         this.lblPluginVersion.Location = new System.Drawing.Point(96, 56);
         this.lblPluginVersion.Name = "lblPluginVersion";
         this.lblPluginVersion.Size = new System.Drawing.Size(168, 23);
         this.lblPluginVersion.TabIndex = 6;
         // 
         // lblPluginName
         // 
         this.lblPluginName.Location = new System.Drawing.Point(96, 24);
         this.lblPluginName.Name = "lblPluginName";
         this.lblPluginName.Size = new System.Drawing.Size(168, 23);
         this.lblPluginName.TabIndex = 5;
         // 
         // lblPluginDescription
         // 
         this.lblPluginDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lblPluginDescription.Location = new System.Drawing.Point(272, 24);
         this.lblPluginDescription.Name = "lblPluginDescription";
         this.lblPluginDescription.Size = new System.Drawing.Size(192, 88);
         this.lblPluginDescription.TabIndex = 3;
         // 
         // lblPluginAuthor
         // 
         this.lblPluginAuthor.Location = new System.Drawing.Point(96, 88);
         this.lblPluginAuthor.Name = "lblPluginAuthor";
         this.lblPluginAuthor.Size = new System.Drawing.Size(168, 23);
         this.lblPluginAuthor.TabIndex = 2;
         // 
         // lblPluginDetailVersion
         // 
         this.lblPluginDetailVersion.Location = new System.Drawing.Point(16, 56);
         this.lblPluginDetailVersion.Name = "lblPluginDetailVersion";
         this.lblPluginDetailVersion.Size = new System.Drawing.Size(80, 23);
         this.lblPluginDetailVersion.TabIndex = 1;
         this.lblPluginDetailVersion.Text = "Version:";
         // 
         // lblPluginDetailName
         // 
         this.lblPluginDetailName.Location = new System.Drawing.Point(16, 24);
         this.lblPluginDetailName.Name = "lblPluginDetailName";
         this.lblPluginDetailName.Size = new System.Drawing.Size(80, 23);
         this.lblPluginDetailName.TabIndex = 0;
         this.lblPluginDetailName.Text = "Name:";
         // 
         // PluginsList
         // 
         this.PluginsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.PluginsList.CheckBoxes = true;
         this.PluginsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                      this.PluginsColumnEnabled,
                                                                                      this.PluginsColumnName,
                                                                                      this.PluginsColumnExt});
         this.PluginsList.FullRowSelect = true;
         this.PluginsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
         this.PluginsList.HideSelection = false;
         this.PluginsList.Location = new System.Drawing.Point(8, 8);
         this.PluginsList.MultiSelect = false;
         this.PluginsList.Name = "PluginsList";
         this.PluginsList.Size = new System.Drawing.Size(472, 96);
         this.PluginsList.TabIndex = 0;
         this.PluginsList.View = System.Windows.Forms.View.Details;
         this.PluginsList.SelectedIndexChanged += new System.EventHandler(this.PluginsList_SelectedIndexChanged);
         this.PluginsList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.PluginsList_ItemCheck);
         // 
         // PluginsColumnEnabled
         // 
         this.PluginsColumnEnabled.Text = "Enabled";
         this.PluginsColumnEnabled.Width = 72;
         // 
         // PluginsColumnName
         // 
         this.PluginsColumnName.Text = "Name";
         this.PluginsColumnName.Width = 246;
         // 
         // PluginsColumnExt
         // 
         this.PluginsColumnExt.Text = "Extensions";
         this.PluginsColumnExt.Width = 134;
         // 
         // frmOptions
         // 
         this.AcceptButton = this.btnOK;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.btnCancel;
         this.ClientSize = new System.Drawing.Size(512, 374);
         this.Controls.Add(this.ResultsPanel);
         this.Controls.Add(this.GeneralPanel);
         this.Controls.Add(this.TextEditorsPanel);
         this.Controls.Add(this.PluginsPanel);
         this.Controls.Add(this.BottomPanel);
         this.Controls.Add(this.SelectionPanel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "frmOptions";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Options";
         this.Load += new System.EventHandler(this.frmOptions_Load);
         this.SelectionPanel.ResumeLayout(false);
         this.GeneralPanel.ResumeLayout(false);
         this.ShortcutGroup.ResumeLayout(false);
         this.ExtensionExclusionGroup.ResumeLayout(false);
         this.LanguageGroup.ResumeLayout(false);
         this.ResultsPanel.ResumeLayout(false);
         this.grpResultWindow.ResumeLayout(false);
         this.grpResultMatch.ResumeLayout(false);
         this.TextEditorsPanel.ResumeLayout(false);
         this.BottomPanel.ResumeLayout(false);
         this.PluginsPanel.ResumeLayout(false);
         this.PluginDetailsGroup.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

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
      /// [Curtis_Beard]      07/28/2006  ADD: extension exclusion list
      /// </history>
      private void frmOptions_Load(object sender, System.EventArgs e)
      {
         cboPathMRUCount.SelectedIndex = AstroGrep.Core.GeneralSettings.MaximumMRUPaths - 1;
         chkRightClickOption.Checked = Common.IsSearchOption();
         chkRightClickOption.Enabled = Common.IsSearchOptionEnabled();
         txtExcludeList.Text = AstroGrep.Core.GeneralSettings.ExtensionExcludeList;
         chkDesktopShortcut.Checked = Common.IsDesktopShortcut();
         chkStartMenuShortcut.Checked = Common.IsStartMenuShortcut();

         // ColorButton init
         ForeColorButton.SelectedColor = Common.ConvertStringToColor(AstroGrep.Core.GeneralSettings.HighlightForeColor);
         BackColorButton.SelectedColor = Common.ConvertStringToColor(AstroGrep.Core.GeneralSettings.HighlightBackColor);
         btnResultsWindowForeColor.SelectedColor = Common.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsForeColor);
         btnResultsWindowBackColor.SelectedColor = Common.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsBackColor);

         // Disable back color, not supported in .Net v1.1
         BackColorLabel.Visible = false;
         BackColorButton.Visible = false;
         BackColorButton.SelectedColor = SystemColors.Window;

         UpdateResultsPreview();

         GeneralPanel.Dock = DockStyle.Fill;
         TextEditorsPanel.Dock = DockStyle.Fill;
         ResultsPanel.Dock = DockStyle.Fill;
         PluginsPanel.Dock = DockStyle.Fill;

         // setup first selected
         ShowPanel(OptionPanels.General);
         GeneralItem.IsSelected = true;

         LoadEditors(Common.GetTextEditors());
         LoadPlugins();

         if (cboLanguage.Items.Count > 0)
            cboLanguage.SelectedItem = AstroGrep.Core.GeneralSettings.Language;

         //Language.GenerateXml(this, Application.StartupPath + "\\" + this.Name + ".xml");
         Language.ProcessForm(this);

         // set column text
         TextEditorsList.Columns[0].Text = Language.GetGenericText("TextEditorsColumnFileType");
         TextEditorsList.Columns[1].Text = Language.GetGenericText("TextEditorsColumnLocation");
         TextEditorsList.Columns[2].Text = Language.GetGenericText("TextEditorsColumnCmdLine");
         PluginsList.Columns[0].Text = Language.GetGenericText("PluginsColumnEnabled");
         PluginsList.Columns[1].Text = Language.GetGenericText("PluginsColumnName");
         PluginsList.Columns[2].Text = Language.GetGenericText("PluginsColumnExtensions");
      }

      /// <summary>
      /// Handle display mnemonic keys for Options Category Items
      /// </summary>
      /// <param name="keyData">Keys pressed</param>
      /// <returns>True if processed, False otherwise.</returns>
      /// <history>
      /// [Curtis_Beard]		07/19/2006	Created
      /// </history>
      protected override bool ProcessDialogKey(Keys keyData)
      {
         if (keyData == (Keys.Menu | Keys.Alt) || keyData == Keys.F10)
         {
            ShowItemsMnemonicKey();
            return true;
         }
      
         return base.ProcessDialogKey (keyData);
      }


      /// <summary>
      /// Handles showing or hiding the mnemonic key indicator for custom controls.
      /// </summary>
      /// <param name="m">Forms message</param>
      /// <history>
      /// [Curtis_Beard]		07/20/2006	Created
      /// </history>
      protected override void WndProc(ref Message m)
      {
         const int WM_CHANGEUISTATE = 0x127;
         const int WM_UPDATEUISTATE = 0x128;

         const int SHOW_UNDERLINE = 131074;
         const int HIDE_UNDERLINE = 196609;

         if (m.Msg == WM_CHANGEUISTATE)
            ShowItemsMnemonicKey();
         else if (m.Msg == WM_UPDATEUISTATE)
         {
            if (m.WParam.ToInt32() == HIDE_UNDERLINE)
               HideItemsMnemonicKey();
            else if (m.WParam.ToInt32() == SHOW_UNDERLINE)
               ShowItemsMnemonicKey();
         }

         base.WndProc (ref m);
      }

      #region Private Methods
      /// <summary>
      /// Shows the given panel.
      /// </summary>
      /// <param name="active">The panel to show</param>
      /// <history>
      /// [Curtis_Beard]		07/19/2006	Created
      /// </history>
      private void ShowPanel(OptionPanels active)
      {
         GeneralItem.IsSelected = false;
         TextEditorsItem.IsSelected = false;
         ResultsItem.IsSelected = false;
         PluginsItem.IsSelected = false;

         switch (active)
         {
            case OptionPanels.General:
               GeneralPanel.BringToFront();
               break;
            case OptionPanels.TextEditors:
               SetTextEditorsButtonState();
               TextEditorsPanel.BringToFront();
               break;
            case OptionPanels.Results:
               ResultsPanel.BringToFront();
               break;
            case OptionPanels.Plugins:
               PluginsPanel.BringToFront();
               break;
         }
      }

      /// <summary>
      /// Update the results preview.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      private void UpdateResultsPreview()
      {
         string PREVIEW_TEXT = Language.GetGenericText("ResultsPreviewText");
         string PREVIEW_MATCH_TEXT = Language.GetGenericText("ResultsPreviewTextMatch");

         string _textToSearch = string.Empty;
         string _searchText = PREVIEW_MATCH_TEXT;
         string _tempLine = string.Empty;

         string _begin = string.Empty;
         string _text = string.Empty;
         string _end = string.Empty;
         int _pos = 0;
         bool _highlight = false;

         // Clear the contents
         rtxtResultsPreview.Text = string.Empty;
         rtxtResultsPreview.ForeColor = btnResultsWindowForeColor.SelectedColor;
         rtxtResultsPreview.BackColor = btnResultsWindowBackColor.SelectedColor;

         // Retrieve hit text
         _textToSearch = PREVIEW_TEXT;

         // Set default font
         rtxtResultsPreview.SelectionFont = new Font("Courier new", 9.75F, FontStyle.Regular);

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
               // txtHits.SelectionBackColor = btnResultsWindowBackColor.SelectedColor;
               rtxtResultsPreview.SelectedText = _begin;

               // do a check to see if begin and end are valid for wholeword searches
               _highlight = true;

               // set highlight color for searched text
               if (_highlight)
               {
                  rtxtResultsPreview.SelectionColor = ForeColorButton.SelectedColor;
                  // rtxtResultsPreview.SelectionBackColor = BackColorButton.SelectedColor;
               }
               rtxtResultsPreview.SelectedText = _text;

               // Check remaining string for other hits in same line
               _pos = _end.IndexOf(_searchText);

               // set default color for end, if no more hits in line
               _tempLine = _end;
               if (_pos < 0)
               {
                  rtxtResultsPreview.SelectionColor = btnResultsWindowForeColor.SelectedColor;
                  // rtxtResultsPreview.SelectionBackColor = btnResultsWindowBackColor.SelectedColor;
                  rtxtResultsPreview.SelectedText = _end;
               }

            }while (_pos > -1);
         }
         else
         {
            // set default color, no search text found
            rtxtResultsPreview.SelectionColor = btnResultsWindowForeColor.SelectedColor;
            // rtxtResultsPreview.SelectionBackColor = btnResultsWindowBackColor.SelectedColor;
            rtxtResultsPreview.SelectedText = _textToSearch;
         }
      }

      /// <summary>
      /// Handle when a new color h been selected.
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
               item.Selected = true;
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
      /// </history>
      private void SaveEditors()
      {
         if (TextEditorsList.Items.Count == 0)
         {
            Common.SaveTextEditors(null);
            return;
         }

         TextEditor[] editors = new TextEditor[TextEditorsList.Items.Count];
         int index = 0;
         foreach (ListViewItem item in TextEditorsList.Items)
         {
            TextEditor editor = new TextEditor();
            editor.FileType = item.Text;
            editor.Editor = item.SubItems[1].Text;
            editor.Arguments = item.SubItems[2].Text;
            editors[index] = editor;
            index += 1;
         }

         Common.SaveTextEditors(editors);
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
      /// Add Mnemonic character to Items' text
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/20/2006	Created
      /// </history>
      private void ShowItemsMnemonicKey()
      {
         GeneralItem.IsMnemonicKeyVisible = true;
         TextEditorsItem.IsMnemonicKeyVisible = true;
         ResultsItem.IsMnemonicKeyVisible = true;
         PluginsItem.IsMnemonicKeyVisible = true;
      }

      /// <summary>
      /// Remove Mnemonic character from Items' text
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/20/2006	Created
      /// </history>
      private void HideItemsMnemonicKey()
      {
         GeneralItem.IsMnemonicKeyVisible = false;
         TextEditorsItem.IsMnemonicKeyVisible = false;
         ResultsItem.IsMnemonicKeyVisible = false;
         PluginsItem.IsMnemonicKeyVisible = false;
      }
      #endregion

      #region Control Events
      /// <summary>
      /// Handles the selection of the General item.
      /// </summary>
      /// <param name="sender">System parameter</param>
      /// <param name="e">System parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/19/2006	Created
      /// </history>
      private void GeneralItem_Click(object sender, System.EventArgs e)
      {
         if (!GeneralItem.IsSelected)
            ShowPanel(OptionPanels.General);
      }

      /// <summary>
      /// Handles the selection of the TextEditors item.
      /// </summary>
      /// <param name="sender">System parameter</param>
      /// <param name="e">System parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/19/2006	Created
      /// </history>
      private void TextEditorsItem_Click(object sender, System.EventArgs e)
      {
         if (!TextEditorsItem.IsSelected)
            ShowPanel(OptionPanels.TextEditors);
      }

      /// <summary>
      /// Handles the selection of the Results item.
      /// </summary>
      /// <param name="sender">System parameter</param>
      /// <param name="e">System parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/19/2006	Created
      /// </history>
      private void ResultsItem_Click(object sender, System.EventArgs e)
      {
         if (!ResultsItem.IsSelected)
            ShowPanel(OptionPanels.Results);
      }

      /// <summary>
      /// Handles the selection of the Plugins item.
      /// </summary>
      /// <param name="sender">System parameter</param>
      /// <param name="e">System parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/19/2006	Created
      /// </history>
      private void PluginsItem_Click(object sender, System.EventArgs e)
      {
         if (!PluginsItem.IsSelected)
            ShowPanel(OptionPanels.Plugins);
      }

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
      /// </history>
      private void btnOK_Click(object sender, System.EventArgs e)
      {
         // Store the values in the globals
         AstroGrep.Core.GeneralSettings.MaximumMRUPaths = cboPathMRUCount.SelectedIndex + 1;
         AstroGrep.Core.GeneralSettings.HighlightForeColor = Common.ConvertColorToString(ForeColorButton.SelectedColor);
         AstroGrep.Core.GeneralSettings.HighlightBackColor = Common.ConvertColorToString(BackColorButton.SelectedColor);
         AstroGrep.Core.GeneralSettings.ResultsForeColor = Common.ConvertColorToString(btnResultsWindowForeColor.SelectedColor);
         AstroGrep.Core.GeneralSettings.ResultsBackColor = Common.ConvertColorToString(btnResultsWindowBackColor.SelectedColor);
         AstroGrep.Core.GeneralSettings.ExtensionExcludeList = txtExcludeList.Text;

         // Only load new language on a change
         if (!AstroGrep.Core.GeneralSettings.Language.Equals(cboLanguage.SelectedItem.ToString()))
         {
            AstroGrep.Core.GeneralSettings.Language = cboLanguage.SelectedItem.ToString();
            __LanguageChange = true;
         }

         // Since .Net v1.1 has no support for BackColor, always save as Window color
         AstroGrep.Core.GeneralSettings.HighlightBackColor = Common.ConvertColorToString(SystemColors.Window);

         if (chkRightClickOption.Enabled)
         {
            // set as search option if checked
            Common.SetAsSearchOption(chkRightClickOption.Checked);
         }

         // set shortcuts
         Common.SetDesktopShortcut(chkDesktopShortcut.Checked);
         Common.SetStartMenuShortcut(chkStartMenuShortcut.Checked);

         SaveEditors();

         Core.PluginManager.Save();

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
      /// Handles painting the selection panel bottom border.
      /// </summary>
      /// <param name="sender">System parameter</param>
      /// <param name="e">System parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/19/2006	Created
      /// </history>
      private void SelectionPanel_Paint(object sender, PaintEventArgs e)
      {
         e.Graphics.DrawLine(SystemPens.ControlDark, 0, SelectionPanel.Height - 2, SelectionPanel.Width, SelectionPanel.Height - 2);
         e.Graphics.DrawLine(SystemPens.ControlLightLight, 0, SelectionPanel.Height - 1, SelectionPanel.Width, SelectionPanel.Height - 1);
      }

      /// <summary>
      /// Add a new text editor.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]		07/20/2006	Created
      /// </history>
      private void btnAdd_Click(object sender, System.EventArgs e)
      {
         if (TextEditorsItem.IsSelected)
         {
            frmAddEditTextEditor dlg = new frmAddEditTextEditor();
            dlg.IsAdd = true;
            dlg.IsAllTypesDefined = IsAllTypesDefined();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
               // get values
               ListViewItem item = new ListViewItem();
               item.Text = dlg.TextEditorFileType;
               item.SubItems.Add(dlg.TextEditorLocation);
               item.SubItems.Add(dlg.TextEditorCommandLine);

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
      /// </history>
      private void btnEdit_Click(object sender, System.EventArgs e)
      {
         if (TextEditorsItem.IsSelected)
         {
            if (TextEditorsList.SelectedItems.Count > 0)
            {
               ListViewItem item = TextEditorsList.SelectedItems[0];
               frmAddEditTextEditor dlg = new frmAddEditTextEditor();

               // set values
               dlg.IsAdd = false;
               dlg.IsAllTypesDefined = IsAllTypesDefined();
               dlg.TextEditorFileType = item.Text;
               dlg.TextEditorLocation = item.SubItems[1].Text;
               dlg.TextEditorCommandLine = item.SubItems[2].Text;

               if (dlg.ShowDialog(this) == DialogResult.OK)
               {
                  // get values
                  TextEditorsList.SelectedItems[0].Text = dlg.TextEditorFileType;
                  TextEditorsList.SelectedItems[0].SubItems[1].Text = dlg.TextEditorLocation;
                  TextEditorsList.SelectedItems[0].SubItems[2].Text = dlg.TextEditorCommandLine;
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
         if (TextEditorsItem.IsSelected)
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
      /// <param name="plugin"></param>
      /// <history>
      /// [Curtis_Beard]		09/05/2006	Created
      /// </history>
      private void LoadPluginDetails(AstroGrepBase.IAstroGrepPlugin plugin)
      {
         lblPluginName.Text = plugin.Name;
         lblPluginVersion.Text = plugin.Version;
         lblPluginAuthor.Text = plugin.Author;
         lblPluginDescription.Text = plugin.Description;
      }
      #endregion
   }
}
