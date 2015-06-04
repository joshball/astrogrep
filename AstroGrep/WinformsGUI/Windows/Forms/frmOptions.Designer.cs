using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AstroGrep.Windows.Forms
{
   public partial class frmOptions
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
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

      #region Windows Form Designer generated code
      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.tbcOptions = new System.Windows.Forms.TabControl();
         this.tabGeneral = new System.Windows.Forms.TabPage();
         this.chkSaveMessagesPosition = new System.Windows.Forms.CheckBox();
         this.chkSaveSearchOptions = new System.Windows.Forms.CheckBox();
         this.chkShowExclusionErrorMessage = new System.Windows.Forms.CheckBox();
         this.ShortcutGroup = new System.Windows.Forms.GroupBox();
         this.chkStartMenuShortcut = new System.Windows.Forms.CheckBox();
         this.chkDesktopShortcut = new System.Windows.Forms.CheckBox();
         this.chkRightClickOption = new System.Windows.Forms.CheckBox();
         this.LanguageGroup = new System.Windows.Forms.GroupBox();
         this.cboLanguage = new System.Windows.Forms.ComboBox();
         this.cboPathMRUCount = new System.Windows.Forms.ComboBox();
         this.lblStoredPaths = new System.Windows.Forms.Label();
         this.tabFileEncoding = new System.Windows.Forms.TabPage();
         this.btnCacheClear = new System.Windows.Forms.Button();
         this.chkUseEncodingCache = new System.Windows.Forms.CheckBox();
         this.cboPerformance = new System.Windows.Forms.ComboBox();
         this.lblPerformance = new System.Windows.Forms.Label();
         this.chkDetectFileEncoding = new System.Windows.Forms.CheckBox();
         this.btnFileEncodingDelete = new System.Windows.Forms.Button();
         this.btnFileEncodingEdit = new System.Windows.Forms.Button();
         this.btnFileEncodingAdd = new System.Windows.Forms.Button();
         this.lstFiles = new System.Windows.Forms.ListView();
         this.clhEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.clhFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.clhEncoding = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.tabTextEditors = new System.Windows.Forms.TabPage();
         this.btnEdit = new System.Windows.Forms.Button();
         this.btnRemove = new System.Windows.Forms.Button();
         this.btnAdd = new System.Windows.Forms.Button();
         this.TextEditorsList = new System.Windows.Forms.ListView();
         this.ColumnType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.ColumnEditor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.ColumnArguments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.ColumnTabSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.tabResults = new System.Windows.Forms.TabPage();
         this.grpFileList = new System.Windows.Forms.GroupBox();
         this.lblFileCurrentFont = new System.Windows.Forms.Label();
         this.btnFileFindFont = new System.Windows.Forms.Button();
         this.lblResultPreview = new System.Windows.Forms.Label();
         this.rtxtResultsPreview = new System.Windows.Forms.RichTextBox();
         this.grpResultWindow = new System.Windows.Forms.GroupBox();
         this.btnResultsContextForeColor = new AstroGrep.Windows.Controls.ColorButton();
         this.lblResultsContextForeColor = new System.Windows.Forms.Label();
         this.lblCurrentFont = new System.Windows.Forms.Label();
         this.btnFindFont = new System.Windows.Forms.Button();
         this.btnResultsWindowBackColor = new AstroGrep.Windows.Controls.ColorButton();
         this.btnResultsWindowForeColor = new AstroGrep.Windows.Controls.ColorButton();
         this.lblResultsWindowBack = new System.Windows.Forms.Label();
         this.lblResultsWindowFore = new System.Windows.Forms.Label();
         this.grpResultMatch = new System.Windows.Forms.GroupBox();
         this.BackColorButton = new AstroGrep.Windows.Controls.ColorButton();
         this.ForeColorButton = new AstroGrep.Windows.Controls.ColorButton();
         this.BackColorLabel = new System.Windows.Forms.Label();
         this.ForeColorLabel = new System.Windows.Forms.Label();
         this.tabPlugins = new System.Windows.Forms.TabPage();
         this.btnDown = new System.Windows.Forms.Button();
         this.btnUp = new System.Windows.Forms.Button();
         this.PluginDetailsGroup = new System.Windows.Forms.GroupBox();
         this.lblPluginDetailAuthor = new System.Windows.Forms.Label();
         this.lblPluginVersion = new System.Windows.Forms.Label();
         this.lblPluginName = new System.Windows.Forms.Label();
         this.lblPluginDescription = new System.Windows.Forms.Label();
         this.lblPluginAuthor = new System.Windows.Forms.Label();
         this.lblPluginDetailVersion = new System.Windows.Forms.Label();
         this.lblPluginDetailName = new System.Windows.Forms.Label();
         this.PluginsList = new System.Windows.Forms.ListView();
         this.PluginsColumnEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.PluginsColumnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.PluginsColumnExt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.btnOK = new System.Windows.Forms.Button();
         this.btnCancel = new System.Windows.Forms.Button();
         this.tbcOptions.SuspendLayout();
         this.tabGeneral.SuspendLayout();
         this.ShortcutGroup.SuspendLayout();
         this.LanguageGroup.SuspendLayout();
         this.tabFileEncoding.SuspendLayout();
         this.tabTextEditors.SuspendLayout();
         this.tabResults.SuspendLayout();
         this.grpFileList.SuspendLayout();
         this.grpResultWindow.SuspendLayout();
         this.grpResultMatch.SuspendLayout();
         this.tabPlugins.SuspendLayout();
         this.PluginDetailsGroup.SuspendLayout();
         this.SuspendLayout();
         // 
         // tbcOptions
         // 
         this.tbcOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.tbcOptions.Controls.Add(this.tabGeneral);
         this.tbcOptions.Controls.Add(this.tabFileEncoding);
         this.tbcOptions.Controls.Add(this.tabTextEditors);
         this.tbcOptions.Controls.Add(this.tabResults);
         this.tbcOptions.Controls.Add(this.tabPlugins);
         this.tbcOptions.Location = new System.Drawing.Point(8, 8);
         this.tbcOptions.Name = "tbcOptions";
         this.tbcOptions.SelectedIndex = 0;
         this.tbcOptions.Size = new System.Drawing.Size(561, 374);
         this.tbcOptions.TabIndex = 0;
         this.tbcOptions.SelectedIndexChanged += new System.EventHandler(this.tbcOptions_SelectedIndexChanged);
         // 
         // tabGeneral
         // 
         this.tabGeneral.Controls.Add(this.chkSaveMessagesPosition);
         this.tabGeneral.Controls.Add(this.chkSaveSearchOptions);
         this.tabGeneral.Controls.Add(this.chkShowExclusionErrorMessage);
         this.tabGeneral.Controls.Add(this.ShortcutGroup);
         this.tabGeneral.Controls.Add(this.LanguageGroup);
         this.tabGeneral.Controls.Add(this.cboPathMRUCount);
         this.tabGeneral.Controls.Add(this.lblStoredPaths);
         this.tabGeneral.Location = new System.Drawing.Point(4, 22);
         this.tabGeneral.Name = "tabGeneral";
         this.tabGeneral.Size = new System.Drawing.Size(553, 348);
         this.tabGeneral.TabIndex = 0;
         this.tabGeneral.Text = "General";
         this.tabGeneral.UseVisualStyleBackColor = true;
         // 
         // chkSaveMessagesPosition
         // 
         this.chkSaveMessagesPosition.AutoSize = true;
         this.chkSaveMessagesPosition.Location = new System.Drawing.Point(8, 213);
         this.chkSaveMessagesPosition.Name = "chkSaveMessagesPosition";
         this.chkSaveMessagesPosition.Size = new System.Drawing.Size(182, 17);
         this.chkSaveMessagesPosition.TabIndex = 38;
         this.chkSaveMessagesPosition.Text = "Save messages window position.";
         this.chkSaveMessagesPosition.UseVisualStyleBackColor = true;
         // 
         // chkSaveSearchOptions
         // 
         this.chkSaveSearchOptions.AutoSize = true;
         this.chkSaveSearchOptions.Location = new System.Drawing.Point(8, 190);
         this.chkSaveSearchOptions.Name = "chkSaveSearchOptions";
         this.chkSaveSearchOptions.Size = new System.Drawing.Size(160, 17);
         this.chkSaveSearchOptions.TabIndex = 37;
         this.chkSaveSearchOptions.Text = "Save search options on exit.";
         this.chkSaveSearchOptions.UseVisualStyleBackColor = true;
         // 
         // chkShowExclusionErrorMessage
         // 
         this.chkShowExclusionErrorMessage.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
         this.chkShowExclusionErrorMessage.Location = new System.Drawing.Point(8, 236);
         this.chkShowExclusionErrorMessage.Name = "chkShowExclusionErrorMessage";
         this.chkShowExclusionErrorMessage.Size = new System.Drawing.Size(472, 35);
         this.chkShowExclusionErrorMessage.TabIndex = 36;
         this.chkShowExclusionErrorMessage.Text = "Show a &prompt when a search yields items being excluded or an error occurs.";
         this.chkShowExclusionErrorMessage.TextAlign = System.Drawing.ContentAlignment.TopLeft;
         this.chkShowExclusionErrorMessage.UseVisualStyleBackColor = true;
         // 
         // ShortcutGroup
         // 
         this.ShortcutGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.ShortcutGroup.Controls.Add(this.chkStartMenuShortcut);
         this.ShortcutGroup.Controls.Add(this.chkDesktopShortcut);
         this.ShortcutGroup.Controls.Add(this.chkRightClickOption);
         this.ShortcutGroup.Location = new System.Drawing.Point(8, 40);
         this.ShortcutGroup.Name = "ShortcutGroup";
         this.ShortcutGroup.Size = new System.Drawing.Size(537, 80);
         this.ShortcutGroup.TabIndex = 35;
         this.ShortcutGroup.TabStop = false;
         this.ShortcutGroup.Text = "Shortcuts";
         // 
         // chkStartMenuShortcut
         // 
         this.chkStartMenuShortcut.BackColor = System.Drawing.Color.Transparent;
         this.chkStartMenuShortcut.Location = new System.Drawing.Point(232, 48);
         this.chkStartMenuShortcut.Name = "chkStartMenuShortcut";
         this.chkStartMenuShortcut.Size = new System.Drawing.Size(240, 24);
         this.chkStartMenuShortcut.TabIndex = 29;
         this.chkStartMenuShortcut.Text = "Start Menu Shortcut";
         this.chkStartMenuShortcut.UseVisualStyleBackColor = false;
         // 
         // chkDesktopShortcut
         // 
         this.chkDesktopShortcut.BackColor = System.Drawing.Color.Transparent;
         this.chkDesktopShortcut.Location = new System.Drawing.Point(8, 48);
         this.chkDesktopShortcut.Name = "chkDesktopShortcut";
         this.chkDesktopShortcut.Size = new System.Drawing.Size(224, 24);
         this.chkDesktopShortcut.TabIndex = 28;
         this.chkDesktopShortcut.Text = "Desktop Shortcut";
         this.chkDesktopShortcut.UseVisualStyleBackColor = false;
         // 
         // chkRightClickOption
         // 
         this.chkRightClickOption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.chkRightClickOption.BackColor = System.Drawing.Color.Transparent;
         this.chkRightClickOption.Location = new System.Drawing.Point(8, 24);
         this.chkRightClickOption.Name = "chkRightClickOption";
         this.chkRightClickOption.Size = new System.Drawing.Size(521, 17);
         this.chkRightClickOption.TabIndex = 20;
         this.chkRightClickOption.Text = "Set right-click option on folders";
         this.chkRightClickOption.UseVisualStyleBackColor = false;
         // 
         // LanguageGroup
         // 
         this.LanguageGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.LanguageGroup.Controls.Add(this.cboLanguage);
         this.LanguageGroup.Location = new System.Drawing.Point(8, 128);
         this.LanguageGroup.Name = "LanguageGroup";
         this.LanguageGroup.Size = new System.Drawing.Size(537, 56);
         this.LanguageGroup.TabIndex = 33;
         this.LanguageGroup.TabStop = false;
         this.LanguageGroup.Text = "Language";
         // 
         // cboLanguage
         // 
         this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
         this.cboPathMRUCount.Size = new System.Drawing.Size(56, 21);
         this.cboPathMRUCount.TabIndex = 31;
         // 
         // lblStoredPaths
         // 
         this.lblStoredPaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lblStoredPaths.BackColor = System.Drawing.Color.Transparent;
         this.lblStoredPaths.Cursor = System.Windows.Forms.Cursors.Default;
         this.lblStoredPaths.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.lblStoredPaths.ForeColor = System.Drawing.SystemColors.ControlText;
         this.lblStoredPaths.Location = new System.Drawing.Point(80, 8);
         this.lblStoredPaths.Name = "lblStoredPaths";
         this.lblStoredPaths.RightToLeft = System.Windows.Forms.RightToLeft.No;
         this.lblStoredPaths.Size = new System.Drawing.Size(465, 21);
         this.lblStoredPaths.TabIndex = 32;
         this.lblStoredPaths.Text = "Number of most recently used paths to store";
         this.lblStoredPaths.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // tabFileEncoding
         // 
         this.tabFileEncoding.Controls.Add(this.btnCacheClear);
         this.tabFileEncoding.Controls.Add(this.chkUseEncodingCache);
         this.tabFileEncoding.Controls.Add(this.cboPerformance);
         this.tabFileEncoding.Controls.Add(this.lblPerformance);
         this.tabFileEncoding.Controls.Add(this.chkDetectFileEncoding);
         this.tabFileEncoding.Controls.Add(this.btnFileEncodingDelete);
         this.tabFileEncoding.Controls.Add(this.btnFileEncodingEdit);
         this.tabFileEncoding.Controls.Add(this.btnFileEncodingAdd);
         this.tabFileEncoding.Controls.Add(this.lstFiles);
         this.tabFileEncoding.Location = new System.Drawing.Point(4, 22);
         this.tabFileEncoding.Name = "tabFileEncoding";
         this.tabFileEncoding.Size = new System.Drawing.Size(553, 348);
         this.tabFileEncoding.TabIndex = 4;
         this.tabFileEncoding.Text = "File Encoding";
         this.tabFileEncoding.UseVisualStyleBackColor = true;
         // 
         // btnCacheClear
         // 
         this.btnCacheClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnCacheClear.AutoSize = true;
         this.btnCacheClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnCacheClear.Location = new System.Drawing.Point(466, 59);
         this.btnCacheClear.Name = "btnCacheClear";
         this.btnCacheClear.Size = new System.Drawing.Size(79, 23);
         this.btnCacheClear.TabIndex = 43;
         this.btnCacheClear.Text = "Clear Cache";
         this.btnCacheClear.UseVisualStyleBackColor = true;
         this.btnCacheClear.Click += new System.EventHandler(this.btnCacheClear_Click);
         // 
         // chkUseEncodingCache
         // 
         this.chkUseEncodingCache.AutoSize = true;
         this.chkUseEncodingCache.Location = new System.Drawing.Point(8, 63);
         this.chkUseEncodingCache.Name = "chkUseEncodingCache";
         this.chkUseEncodingCache.Size = new System.Drawing.Size(207, 17);
         this.chkUseEncodingCache.TabIndex = 42;
         this.chkUseEncodingCache.Text = "Enable cache for detected encodings.";
         this.chkUseEncodingCache.UseVisualStyleBackColor = true;
         // 
         // cboPerformance
         // 
         this.cboPerformance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cboPerformance.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.cboPerformance.FormattingEnabled = true;
         this.cboPerformance.Location = new System.Drawing.Point(78, 32);
         this.cboPerformance.Name = "cboPerformance";
         this.cboPerformance.Size = new System.Drawing.Size(121, 21);
         this.cboPerformance.TabIndex = 41;
         // 
         // lblPerformance
         // 
         this.lblPerformance.AutoSize = true;
         this.lblPerformance.Location = new System.Drawing.Point(5, 35);
         this.lblPerformance.Name = "lblPerformance";
         this.lblPerformance.Size = new System.Drawing.Size(67, 13);
         this.lblPerformance.TabIndex = 40;
         this.lblPerformance.Text = "Performance";
         // 
         // chkDetectFileEncoding
         // 
         this.chkDetectFileEncoding.AutoSize = true;
         this.chkDetectFileEncoding.Location = new System.Drawing.Point(8, 9);
         this.chkDetectFileEncoding.Name = "chkDetectFileEncoding";
         this.chkDetectFileEncoding.Size = new System.Drawing.Size(124, 17);
         this.chkDetectFileEncoding.TabIndex = 39;
         this.chkDetectFileEncoding.Text = "Detect file encoding.";
         this.chkDetectFileEncoding.UseVisualStyleBackColor = true;
         this.chkDetectFileEncoding.CheckedChanged += new System.EventHandler(this.chkDetectFileEncoding_CheckedChanged);
         // 
         // btnFileEncodingDelete
         // 
         this.btnFileEncodingDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnFileEncodingDelete.Location = new System.Drawing.Point(184, 310);
         this.btnFileEncodingDelete.Name = "btnFileEncodingDelete";
         this.btnFileEncodingDelete.Size = new System.Drawing.Size(75, 23);
         this.btnFileEncodingDelete.TabIndex = 4;
         this.btnFileEncodingDelete.Text = "&Delete";
         this.btnFileEncodingDelete.UseVisualStyleBackColor = true;
         this.btnFileEncodingDelete.Click += new System.EventHandler(this.btnFileEncodingDelete_Click);
         // 
         // btnFileEncodingEdit
         // 
         this.btnFileEncodingEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnFileEncodingEdit.Location = new System.Drawing.Point(96, 310);
         this.btnFileEncodingEdit.Name = "btnFileEncodingEdit";
         this.btnFileEncodingEdit.Size = new System.Drawing.Size(75, 23);
         this.btnFileEncodingEdit.TabIndex = 3;
         this.btnFileEncodingEdit.Text = "&Edit...";
         this.btnFileEncodingEdit.UseVisualStyleBackColor = true;
         this.btnFileEncodingEdit.Click += new System.EventHandler(this.btnFileEncodingEdit_Click);
         // 
         // btnFileEncodingAdd
         // 
         this.btnFileEncodingAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnFileEncodingAdd.Location = new System.Drawing.Point(8, 310);
         this.btnFileEncodingAdd.Name = "btnFileEncodingAdd";
         this.btnFileEncodingAdd.Size = new System.Drawing.Size(75, 23);
         this.btnFileEncodingAdd.TabIndex = 2;
         this.btnFileEncodingAdd.Text = "&Add...";
         this.btnFileEncodingAdd.UseVisualStyleBackColor = true;
         this.btnFileEncodingAdd.Click += new System.EventHandler(this.btnFileEncodingAdd_Click);
         // 
         // lstFiles
         // 
         this.lstFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lstFiles.CheckBoxes = true;
         this.lstFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhEnabled,
            this.clhFile,
            this.clhEncoding});
         this.lstFiles.FullRowSelect = true;
         this.lstFiles.HideSelection = false;
         this.lstFiles.Location = new System.Drawing.Point(8, 91);
         this.lstFiles.Name = "lstFiles";
         this.lstFiles.Size = new System.Drawing.Size(537, 199);
         this.lstFiles.TabIndex = 1;
         this.lstFiles.UseCompatibleStateImageBehavior = false;
         this.lstFiles.View = System.Windows.Forms.View.Details;
         this.lstFiles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstFiles_ColumnClick);
         this.lstFiles.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstFiles_ItemCheck);
         this.lstFiles.SelectedIndexChanged += new System.EventHandler(this.lstFiles_SelectedIndexChanged);
         this.lstFiles.DoubleClick += new System.EventHandler(this.lstFiles_DoubleClick);
         this.lstFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstFiles_KeyDown);
         this.lstFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstFiles_MouseDown);
         this.lstFiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstFiles_MouseUp);
         // 
         // clhEnabled
         // 
         this.clhEnabled.Text = "Enabled";
         // 
         // clhFile
         // 
         this.clhFile.Text = "File";
         this.clhFile.Width = 326;
         // 
         // clhEncoding
         // 
         this.clhEncoding.Text = "Encoding";
         this.clhEncoding.Width = 143;
         // 
         // tabTextEditors
         // 
         this.tabTextEditors.Controls.Add(this.btnEdit);
         this.tabTextEditors.Controls.Add(this.btnRemove);
         this.tabTextEditors.Controls.Add(this.btnAdd);
         this.tabTextEditors.Controls.Add(this.TextEditorsList);
         this.tabTextEditors.Location = new System.Drawing.Point(4, 22);
         this.tabTextEditors.Name = "tabTextEditors";
         this.tabTextEditors.Size = new System.Drawing.Size(553, 348);
         this.tabTextEditors.TabIndex = 1;
         this.tabTextEditors.Text = "Text Editors";
         this.tabTextEditors.UseVisualStyleBackColor = true;
         // 
         // btnEdit
         // 
         this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnEdit.Location = new System.Drawing.Point(96, 310);
         this.btnEdit.Name = "btnEdit";
         this.btnEdit.Size = new System.Drawing.Size(75, 23);
         this.btnEdit.TabIndex = 16;
         this.btnEdit.Text = "&Edit...";
         this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
         // 
         // btnRemove
         // 
         this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnRemove.Location = new System.Drawing.Point(184, 310);
         this.btnRemove.Name = "btnRemove";
         this.btnRemove.Size = new System.Drawing.Size(75, 23);
         this.btnRemove.TabIndex = 15;
         this.btnRemove.Text = "&Delete";
         this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
         // 
         // btnAdd
         // 
         this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnAdd.Location = new System.Drawing.Point(8, 310);
         this.btnAdd.Name = "btnAdd";
         this.btnAdd.Size = new System.Drawing.Size(75, 23);
         this.btnAdd.TabIndex = 14;
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
            this.ColumnArguments,
            this.ColumnTabSize});
         this.TextEditorsList.FullRowSelect = true;
         this.TextEditorsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
         this.TextEditorsList.HideSelection = false;
         this.TextEditorsList.Location = new System.Drawing.Point(8, 8);
         this.TextEditorsList.MultiSelect = false;
         this.TextEditorsList.Name = "TextEditorsList";
         this.TextEditorsList.Size = new System.Drawing.Size(537, 282);
         this.TextEditorsList.TabIndex = 13;
         this.TextEditorsList.UseCompatibleStateImageBehavior = false;
         this.TextEditorsList.View = System.Windows.Forms.View.Details;
         this.TextEditorsList.SelectedIndexChanged += new System.EventHandler(this.TextEditorsList_SelectedIndexChanged);
         this.TextEditorsList.DoubleClick += new System.EventHandler(this.TextEditorsList_DoubleClick);
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
         // ColumnTabSize
         // 
         this.ColumnTabSize.Text = "Tab Size";
         // 
         // tabResults
         // 
         this.tabResults.Controls.Add(this.grpFileList);
         this.tabResults.Controls.Add(this.lblResultPreview);
         this.tabResults.Controls.Add(this.rtxtResultsPreview);
         this.tabResults.Controls.Add(this.grpResultWindow);
         this.tabResults.Controls.Add(this.grpResultMatch);
         this.tabResults.Location = new System.Drawing.Point(4, 22);
         this.tabResults.Name = "tabResults";
         this.tabResults.Size = new System.Drawing.Size(553, 348);
         this.tabResults.TabIndex = 2;
         this.tabResults.Text = "Results";
         this.tabResults.UseVisualStyleBackColor = true;
         // 
         // grpFileList
         // 
         this.grpFileList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.grpFileList.Controls.Add(this.lblFileCurrentFont);
         this.grpFileList.Controls.Add(this.btnFileFindFont);
         this.grpFileList.Location = new System.Drawing.Point(8, 3);
         this.grpFileList.Name = "grpFileList";
         this.grpFileList.Size = new System.Drawing.Size(537, 66);
         this.grpFileList.TabIndex = 27;
         this.grpFileList.TabStop = false;
         this.grpFileList.Text = "File List";
         // 
         // lblFileCurrentFont
         // 
         this.lblFileCurrentFont.AutoSize = true;
         this.lblFileCurrentFont.Location = new System.Drawing.Point(8, 34);
         this.lblFileCurrentFont.Name = "lblFileCurrentFont";
         this.lblFileCurrentFont.Size = new System.Drawing.Size(65, 13);
         this.lblFileCurrentFont.TabIndex = 1;
         this.lblFileCurrentFont.Text = "Current Font";
         // 
         // btnFileFindFont
         // 
         this.btnFileFindFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnFileFindFont.AutoSize = true;
         this.btnFileFindFont.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnFileFindFont.Location = new System.Drawing.Point(441, 29);
         this.btnFileFindFont.Name = "btnFileFindFont";
         this.btnFileFindFont.Size = new System.Drawing.Size(75, 23);
         this.btnFileFindFont.TabIndex = 0;
         this.btnFileFindFont.Text = "Find Font";
         this.btnFileFindFont.UseVisualStyleBackColor = true;
         this.btnFileFindFont.Click += new System.EventHandler(this.btnFileFindFont_Click);
         // 
         // lblResultPreview
         // 
         this.lblResultPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lblResultPreview.Location = new System.Drawing.Point(8, 279);
         this.lblResultPreview.Name = "lblResultPreview";
         this.lblResultPreview.Size = new System.Drawing.Size(537, 16);
         this.lblResultPreview.TabIndex = 26;
         this.lblResultPreview.Text = "Results Preview";
         // 
         // rtxtResultsPreview
         // 
         this.rtxtResultsPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.rtxtResultsPreview.Location = new System.Drawing.Point(8, 295);
         this.rtxtResultsPreview.Name = "rtxtResultsPreview";
         this.rtxtResultsPreview.ReadOnly = true;
         this.rtxtResultsPreview.Size = new System.Drawing.Size(537, 40);
         this.rtxtResultsPreview.TabIndex = 25;
         this.rtxtResultsPreview.Text = "(21)  Example results line and, match, displayed";
         // 
         // grpResultWindow
         // 
         this.grpResultWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.grpResultWindow.Controls.Add(this.btnResultsContextForeColor);
         this.grpResultWindow.Controls.Add(this.lblResultsContextForeColor);
         this.grpResultWindow.Controls.Add(this.lblCurrentFont);
         this.grpResultWindow.Controls.Add(this.btnFindFont);
         this.grpResultWindow.Controls.Add(this.btnResultsWindowBackColor);
         this.grpResultWindow.Controls.Add(this.btnResultsWindowForeColor);
         this.grpResultWindow.Controls.Add(this.lblResultsWindowBack);
         this.grpResultWindow.Controls.Add(this.lblResultsWindowFore);
         this.grpResultWindow.Location = new System.Drawing.Point(8, 139);
         this.grpResultWindow.Name = "grpResultWindow";
         this.grpResultWindow.Size = new System.Drawing.Size(537, 137);
         this.grpResultWindow.TabIndex = 24;
         this.grpResultWindow.TabStop = false;
         this.grpResultWindow.Text = "Results Window";
         // 
         // btnResultsContextForeColor
         // 
         this.btnResultsContextForeColor.ForeColor = System.Drawing.Color.Silver;
         this.btnResultsContextForeColor.Location = new System.Drawing.Point(144, 68);
         this.btnResultsContextForeColor.Name = "btnResultsContextForeColor";
         this.btnResultsContextForeColor.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
         this.btnResultsContextForeColor.Size = new System.Drawing.Size(75, 23);
         this.btnResultsContextForeColor.TabIndex = 26;
         // 
         // lblResultsContextForeColor
         // 
         this.lblResultsContextForeColor.Location = new System.Drawing.Point(8, 68);
         this.lblResultsContextForeColor.Name = "lblResultsContextForeColor";
         this.lblResultsContextForeColor.Size = new System.Drawing.Size(136, 23);
         this.lblResultsContextForeColor.TabIndex = 25;
         this.lblResultsContextForeColor.Text = "Context Fore Color";
         this.lblResultsContextForeColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // lblCurrentFont
         // 
         this.lblCurrentFont.AutoSize = true;
         this.lblCurrentFont.Location = new System.Drawing.Point(8, 106);
         this.lblCurrentFont.Name = "lblCurrentFont";
         this.lblCurrentFont.Size = new System.Drawing.Size(65, 13);
         this.lblCurrentFont.TabIndex = 24;
         this.lblCurrentFont.Text = "Current Font";
         // 
         // btnFindFont
         // 
         this.btnFindFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnFindFont.AutoSize = true;
         this.btnFindFont.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnFindFont.Location = new System.Drawing.Point(441, 101);
         this.btnFindFont.Name = "btnFindFont";
         this.btnFindFont.Size = new System.Drawing.Size(75, 23);
         this.btnFindFont.TabIndex = 23;
         this.btnFindFont.Text = "&Find Font";
         this.btnFindFont.UseVisualStyleBackColor = true;
         this.btnFindFont.Click += new System.EventHandler(this.btnFindFont_Click);
         // 
         // btnResultsWindowBackColor
         // 
         this.btnResultsWindowBackColor.Location = new System.Drawing.Point(441, 24);
         this.btnResultsWindowBackColor.Name = "btnResultsWindowBackColor";
         this.btnResultsWindowBackColor.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
         this.btnResultsWindowBackColor.Size = new System.Drawing.Size(75, 23);
         this.btnResultsWindowBackColor.TabIndex = 22;
         // 
         // btnResultsWindowForeColor
         // 
         this.btnResultsWindowForeColor.Location = new System.Drawing.Point(144, 24);
         this.btnResultsWindowForeColor.Name = "btnResultsWindowForeColor";
         this.btnResultsWindowForeColor.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
         this.btnResultsWindowForeColor.Size = new System.Drawing.Size(75, 23);
         this.btnResultsWindowForeColor.TabIndex = 21;
         // 
         // lblResultsWindowBack
         // 
         this.lblResultsWindowBack.Location = new System.Drawing.Point(305, 24);
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
         this.grpResultMatch.Location = new System.Drawing.Point(8, 75);
         this.grpResultMatch.Name = "grpResultMatch";
         this.grpResultMatch.Size = new System.Drawing.Size(537, 56);
         this.grpResultMatch.TabIndex = 23;
         this.grpResultMatch.TabStop = false;
         this.grpResultMatch.Text = "Results Match";
         // 
         // BackColorButton
         // 
         this.BackColorButton.Location = new System.Drawing.Point(441, 24);
         this.BackColorButton.Name = "BackColorButton";
         this.BackColorButton.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
         this.BackColorButton.Size = new System.Drawing.Size(75, 23);
         this.BackColorButton.TabIndex = 18;
         // 
         // ForeColorButton
         // 
         this.ForeColorButton.Location = new System.Drawing.Point(144, 24);
         this.ForeColorButton.Name = "ForeColorButton";
         this.ForeColorButton.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
         this.ForeColorButton.Size = new System.Drawing.Size(75, 23);
         this.ForeColorButton.TabIndex = 17;
         // 
         // BackColorLabel
         // 
         this.BackColorLabel.Location = new System.Drawing.Point(305, 24);
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
         // tabPlugins
         // 
         this.tabPlugins.Controls.Add(this.btnDown);
         this.tabPlugins.Controls.Add(this.btnUp);
         this.tabPlugins.Controls.Add(this.PluginDetailsGroup);
         this.tabPlugins.Controls.Add(this.PluginsList);
         this.tabPlugins.Location = new System.Drawing.Point(4, 22);
         this.tabPlugins.Name = "tabPlugins";
         this.tabPlugins.Size = new System.Drawing.Size(553, 348);
         this.tabPlugins.TabIndex = 3;
         this.tabPlugins.Text = "Plugins";
         this.tabPlugins.UseVisualStyleBackColor = true;
         // 
         // btnDown
         // 
         this.btnDown.Location = new System.Drawing.Point(513, 122);
         this.btnDown.Name = "btnDown";
         this.btnDown.Size = new System.Drawing.Size(32, 23);
         this.btnDown.TabIndex = 5;
         this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
         // 
         // btnUp
         // 
         this.btnUp.Location = new System.Drawing.Point(513, 72);
         this.btnUp.Name = "btnUp";
         this.btnUp.Size = new System.Drawing.Size(32, 23);
         this.btnUp.TabIndex = 4;
         this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
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
         this.PluginDetailsGroup.Location = new System.Drawing.Point(8, 225);
         this.PluginDetailsGroup.Name = "PluginDetailsGroup";
         this.PluginDetailsGroup.Size = new System.Drawing.Size(537, 120);
         this.PluginDetailsGroup.TabIndex = 3;
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
         this.lblPluginDescription.Size = new System.Drawing.Size(257, 88);
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
         this.PluginsList.Size = new System.Drawing.Size(492, 211);
         this.PluginsList.TabIndex = 2;
         this.PluginsList.UseCompatibleStateImageBehavior = false;
         this.PluginsList.View = System.Windows.Forms.View.Details;
         this.PluginsList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.PluginsList_ItemCheck);
         this.PluginsList.SelectedIndexChanged += new System.EventHandler(this.PluginsList_SelectedIndexChanged);
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
         // btnOK
         // 
         this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnOK.Location = new System.Drawing.Point(417, 390);
         this.btnOK.Name = "btnOK";
         this.btnOK.Size = new System.Drawing.Size(75, 23);
         this.btnOK.TabIndex = 1;
         this.btnOK.Text = "&OK";
         this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
         // 
         // btnCancel
         // 
         this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnCancel.Location = new System.Drawing.Point(497, 390);
         this.btnCancel.Name = "btnCancel";
         this.btnCancel.Size = new System.Drawing.Size(75, 23);
         this.btnCancel.TabIndex = 2;
         this.btnCancel.Text = "&Cancel";
         this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
         // 
         // frmOptions
         // 
         this.AcceptButton = this.btnOK;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.btnCancel;
         this.ClientSize = new System.Drawing.Size(575, 418);
         this.Controls.Add(this.btnCancel);
         this.Controls.Add(this.btnOK);
         this.Controls.Add(this.tbcOptions);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "frmOptions";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Options";
         this.Load += new System.EventHandler(this.frmOptions_Load);
         this.tbcOptions.ResumeLayout(false);
         this.tabGeneral.ResumeLayout(false);
         this.tabGeneral.PerformLayout();
         this.ShortcutGroup.ResumeLayout(false);
         this.LanguageGroup.ResumeLayout(false);
         this.tabFileEncoding.ResumeLayout(false);
         this.tabFileEncoding.PerformLayout();
         this.tabTextEditors.ResumeLayout(false);
         this.tabResults.ResumeLayout(false);
         this.grpFileList.ResumeLayout(false);
         this.grpFileList.PerformLayout();
         this.grpResultWindow.ResumeLayout(false);
         this.grpResultWindow.PerformLayout();
         this.grpResultMatch.ResumeLayout(false);
         this.tabPlugins.ResumeLayout(false);
         this.PluginDetailsGroup.ResumeLayout(false);
         this.ResumeLayout(false);

      }
      #endregion

      private System.Windows.Forms.TabControl tbcOptions;
      private System.Windows.Forms.Button btnOK;
      private System.Windows.Forms.Button btnCancel;
      private System.Windows.Forms.TabPage tabGeneral;
      private System.Windows.Forms.TabPage tabTextEditors;
      private System.Windows.Forms.TabPage tabResults;
      private System.Windows.Forms.TabPage tabPlugins;
      private System.Windows.Forms.Label lblResultPreview;
      private System.Windows.Forms.RichTextBox rtxtResultsPreview;
      private System.Windows.Forms.GroupBox grpResultWindow;
      private AstroGrep.Windows.Controls.ColorButton btnResultsWindowBackColor;
      private AstroGrep.Windows.Controls.ColorButton btnResultsWindowForeColor;
      private System.Windows.Forms.Label lblResultsWindowBack;
      private System.Windows.Forms.Label lblResultsWindowFore;
      private System.Windows.Forms.GroupBox grpResultMatch;
      private AstroGrep.Windows.Controls.ColorButton BackColorButton;
      private AstroGrep.Windows.Controls.ColorButton ForeColorButton;
      private System.Windows.Forms.Label BackColorLabel;
      private System.Windows.Forms.Label ForeColorLabel;
      private System.Windows.Forms.GroupBox LanguageGroup;
      private System.Windows.Forms.ComboBox cboLanguage;
      private System.Windows.Forms.ComboBox cboPathMRUCount;
      private System.Windows.Forms.Label lblStoredPaths;
      private System.Windows.Forms.GroupBox PluginDetailsGroup;
      private System.Windows.Forms.Label lblPluginDetailAuthor;
      private System.Windows.Forms.Label lblPluginVersion;
      private System.Windows.Forms.Label lblPluginName;
      private System.Windows.Forms.Label lblPluginDescription;
      private System.Windows.Forms.Label lblPluginAuthor;
      private System.Windows.Forms.Label lblPluginDetailVersion;
      private System.Windows.Forms.Label lblPluginDetailName;
      private System.Windows.Forms.ListView PluginsList;
      private System.Windows.Forms.ColumnHeader PluginsColumnEnabled;
      private System.Windows.Forms.ColumnHeader PluginsColumnName;
      private System.Windows.Forms.ColumnHeader PluginsColumnExt;
      private System.Windows.Forms.Button btnEdit;
      private System.Windows.Forms.Button btnRemove;
      private System.Windows.Forms.Button btnAdd;
      private System.Windows.Forms.ListView TextEditorsList;
      private System.Windows.Forms.ColumnHeader ColumnType;
      private System.Windows.Forms.ColumnHeader ColumnEditor;
      private System.Windows.Forms.ColumnHeader ColumnArguments;
      private System.Windows.Forms.GroupBox ShortcutGroup;
      private System.Windows.Forms.CheckBox chkStartMenuShortcut;
      private System.Windows.Forms.CheckBox chkDesktopShortcut;
      private System.Windows.Forms.CheckBox chkRightClickOption;
      private Label lblCurrentFont;
      private Button btnFindFont;
      private CheckBox chkShowExclusionErrorMessage;
      private CheckBox chkSaveSearchOptions;
      private GroupBox grpFileList;
      private Label lblFileCurrentFont;
      private Button btnFileFindFont;
      private ColumnHeader ColumnTabSize;
      private Button btnDown;
      private Button btnUp;
      private TabPage tabFileEncoding;
      private Button btnFileEncodingDelete;
      private Button btnFileEncodingEdit;
      private Button btnFileEncodingAdd;
      private ListView lstFiles;
      private ColumnHeader clhEnabled;
      private ColumnHeader clhFile;
      private ColumnHeader clhEncoding;
      private CheckBox chkDetectFileEncoding;
      private CheckBox chkSaveMessagesPosition;
      private Controls.ColorButton btnResultsContextForeColor;
      private Label lblResultsContextForeColor;
      private ComboBox cboPerformance;
      private Label lblPerformance;
      private CheckBox chkUseEncodingCache;
      private Button btnCacheClear;
   }
}
