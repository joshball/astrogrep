Imports VB = Microsoft.VisualBasic

''' -----------------------------------------------------------------------------
''' Project	 : AstroGrep
''' Class	 : frmMain
''' 
''' -----------------------------------------------------------------------------
''' <summary>
'''   Main Form
''' </summary>
''' <remarks>
'''   AstroGrep File Searching Utility. Written by Theodore L. Ward
'''   Copyright (C) 2002 AstroComma Incorporated.
'''   
'''   This program is free software; you can redistribute it and/or
'''   modify it under the terms of the GNU General Public License
'''   as published by the Free Software Foundation; either version 2
'''   of the License, or (at your option) any later version.
'''   
'''   This program is distributed in the hope that it will be useful,
'''   but WITHOUT ANY WARRANTY; without even the implied warranty of
'''   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'''   GNU General Public License for more details.
'''   
'''   You should have received a copy of the GNU General Public License
'''   along with this program; if not, write to the Free Software
'''   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
'''   The author may be contacted at:
'''   TheodoreWard@Hotmail.com or TheodoreWard@Yahoo.com
''' </remarks>
''' <history>
'''   [Theodore_Ward]   ??/??/????  Initial
''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion/Comments/Option Strict
''' </history>
''' -----------------------------------------------------------------------------
Public Class frmMain
   Inherits System.Windows.Forms.Form

#Region "Declarations"
   Public stbStatus As New StatusBar

   Private __moving As Boolean
   Private __optionsShow As Boolean
   Private __sortColumn As Integer = -1

   Private Const __BROWSE As String = "Browse..."
#End Region

#Region " Windows Form Designer generated code "

   Public Sub New()
      MyBase.New()

      'This call is required by the Windows Form Designer.
      InitializeComponent()

      'Add any initialization after the InitializeComponent() call
      Me.Controls.Add(stbStatus)
   End Sub

   'Form overrides dispose to clean up the component list.
   Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
      If disposing Then
         If Not (components Is Nothing) Then
            components.Dispose()
         End If
      End If
      MyBase.Dispose(disposing)
   End Sub

   'Required by the Windows Form Designer
   Private components As System.ComponentModel.IContainer

   'NOTE: The following procedure is required by the Windows Form Designer
   'It can be modified using the Windows Form Designer.  
   'Do not modify it using the code editor.
   Friend WithEvents mnuAll As System.Windows.Forms.MainMenu
   Friend WithEvents mnuFile As System.Windows.Forms.MenuItem
   Friend WithEvents mnuHelp As System.Windows.Forms.MenuItem

   Friend WithEvents txtHits As System.Windows.Forms.RichTextBox
   Friend WithEvents lstFileNames As System.Windows.Forms.ListView
   Friend WithEvents pnlSearch As System.Windows.Forms.Panel
   Friend WithEvents pnlRightSide As System.Windows.Forms.Panel
   Friend WithEvents pnlDetails As System.Windows.Forms.Panel
   Friend WithEvents pnlFiles As System.Windows.Forms.Panel
   Friend WithEvents pnlSearchOptions As System.Windows.Forms.Panel
   Friend WithEvents lnkSearchOptions As System.Windows.Forms.LinkLabel
   Friend WithEvents cboSearchForText As System.Windows.Forms.ComboBox
   Friend WithEvents cboFileName As System.Windows.Forms.ComboBox
   Friend WithEvents cboFilePath As System.Windows.Forms.ComboBox
   Friend WithEvents btnCancel As System.Windows.Forms.Button
   Friend WithEvents btnSearch As System.Windows.Forms.Button
   Friend WithEvents lblSearchHeading As System.Windows.Forms.Label
   Friend WithEvents mnuEdit As System.Windows.Forms.MenuItem
   Friend WithEvents mnuTools As System.Windows.Forms.MenuItem
   Friend WithEvents mnuOptions As System.Windows.Forms.MenuItem
   Friend WithEvents mnuAbout As System.Windows.Forms.MenuItem
   Friend WithEvents mnuSaveResults As System.Windows.Forms.MenuItem
   Friend WithEvents mnuPrint As System.Windows.Forms.MenuItem
   Friend WithEvents mnuExit As System.Windows.Forms.MenuItem
   Friend WithEvents mnuClearMRU As System.Windows.Forms.MenuItem
   Friend WithEvents mnuSaveSearchSettings As System.Windows.Forms.MenuItem
   Friend WithEvents mnuSelectAll As System.Windows.Forms.MenuItem
   Friend WithEvents mnuOpenSelected As System.Windows.Forms.MenuItem
   Friend WithEvents chkRegularExpressions As System.Windows.Forms.CheckBox
   Friend WithEvents chkCaseSensitive As System.Windows.Forms.CheckBox
   Friend WithEvents chkWholeWordOnly As System.Windows.Forms.CheckBox
   Friend WithEvents txtContextLines As System.Windows.Forms.NumericUpDown
   Friend WithEvents chkLineNumbers As System.Windows.Forms.CheckBox
   Friend WithEvents chkRecurse As System.Windows.Forms.CheckBox
   Friend WithEvents chkNegation As System.Windows.Forms.CheckBox
   Friend WithEvents chkFileNamesOnly As System.Windows.Forms.CheckBox
   Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
   Friend WithEvents pnlMainSearch As System.Windows.Forms.Panel
   Friend WithEvents cdlgSaveFile As System.Windows.Forms.SaveFileDialog
   Friend WithEvents cdlgFolderBrowse As System.Windows.Forms.FolderBrowserDialog
   Friend WithEvents splitUpDown As System.Windows.Forms.Splitter
   Friend WithEvents splitLeftRight As System.Windows.Forms.Splitter
   Friend WithEvents lblSearchText As System.Windows.Forms.Label
   Friend WithEvents lblFileTypes As System.Windows.Forms.Label
   Friend WithEvents lblSearchPath As System.Windows.Forms.Label
   Friend WithEvents mnuSepFile1 As System.Windows.Forms.MenuItem
   Friend WithEvents mnuSepTools1 As System.Windows.Forms.MenuItem
   Friend WithEvents lblContextLines As System.Windows.Forms.Label
   <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
      Me.components = New System.ComponentModel.Container
      Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMain))
      Me.pnlSearch = New System.Windows.Forms.Panel
      Me.pnlMainSearch = New System.Windows.Forms.Panel
      Me.lblSearchHeading = New System.Windows.Forms.Label
      Me.lblSearchText = New System.Windows.Forms.Label
      Me.lblFileTypes = New System.Windows.Forms.Label
      Me.cboSearchForText = New System.Windows.Forms.ComboBox
      Me.cboFileName = New System.Windows.Forms.ComboBox
      Me.cboFilePath = New System.Windows.Forms.ComboBox
      Me.btnCancel = New System.Windows.Forms.Button
      Me.btnSearch = New System.Windows.Forms.Button
      Me.lblSearchPath = New System.Windows.Forms.Label
      Me.pnlSearchOptions = New System.Windows.Forms.Panel
      Me.lblContextLines = New System.Windows.Forms.Label
      Me.txtContextLines = New System.Windows.Forms.NumericUpDown
      Me.chkLineNumbers = New System.Windows.Forms.CheckBox
      Me.chkNegation = New System.Windows.Forms.CheckBox
      Me.chkFileNamesOnly = New System.Windows.Forms.CheckBox
      Me.chkRecurse = New System.Windows.Forms.CheckBox
      Me.chkWholeWordOnly = New System.Windows.Forms.CheckBox
      Me.chkCaseSensitive = New System.Windows.Forms.CheckBox
      Me.chkRegularExpressions = New System.Windows.Forms.CheckBox
      Me.lnkSearchOptions = New System.Windows.Forms.LinkLabel
      Me.pnlRightSide = New System.Windows.Forms.Panel
      Me.splitUpDown = New System.Windows.Forms.Splitter
      Me.pnlDetails = New System.Windows.Forms.Panel
      Me.txtHits = New System.Windows.Forms.RichTextBox
      Me.pnlFiles = New System.Windows.Forms.Panel
      Me.lstFileNames = New System.Windows.Forms.ListView
      Me.splitLeftRight = New System.Windows.Forms.Splitter
      Me.mnuAll = New System.Windows.Forms.MainMenu
      Me.mnuFile = New System.Windows.Forms.MenuItem
      Me.mnuSaveResults = New System.Windows.Forms.MenuItem
      Me.mnuPrint = New System.Windows.Forms.MenuItem
      Me.mnuSepFile1 = New System.Windows.Forms.MenuItem
      Me.mnuExit = New System.Windows.Forms.MenuItem
      Me.mnuEdit = New System.Windows.Forms.MenuItem
      Me.mnuSelectAll = New System.Windows.Forms.MenuItem
      Me.mnuOpenSelected = New System.Windows.Forms.MenuItem
      Me.mnuTools = New System.Windows.Forms.MenuItem
      Me.mnuClearMRU = New System.Windows.Forms.MenuItem
      Me.mnuSepTools1 = New System.Windows.Forms.MenuItem
      Me.mnuSaveSearchSettings = New System.Windows.Forms.MenuItem
      Me.mnuOptions = New System.Windows.Forms.MenuItem
      Me.mnuHelp = New System.Windows.Forms.MenuItem
      Me.mnuAbout = New System.Windows.Forms.MenuItem
      Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
      Me.cdlgSaveFile = New System.Windows.Forms.SaveFileDialog
      Me.cdlgFolderBrowse = New System.Windows.Forms.FolderBrowserDialog
      Me.pnlSearch.SuspendLayout()
      Me.pnlMainSearch.SuspendLayout()
      Me.pnlSearchOptions.SuspendLayout()
      CType(Me.txtContextLines, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.pnlRightSide.SuspendLayout()
      Me.pnlDetails.SuspendLayout()
      Me.pnlFiles.SuspendLayout()
      Me.SuspendLayout()
      '
      'pnlSearch
      '
      Me.pnlSearch.AutoScroll = True
      Me.pnlSearch.BackColor = System.Drawing.SystemColors.Window
      Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
      Me.pnlSearch.Controls.Add(Me.pnlMainSearch)
      Me.pnlSearch.Controls.Add(Me.pnlSearchOptions)
      Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Left
      Me.pnlSearch.Location = New System.Drawing.Point(0, 0)
      Me.pnlSearch.Name = "pnlSearch"
      Me.pnlSearch.Size = New System.Drawing.Size(240, 433)
      Me.pnlSearch.TabIndex = 0
      '
      'pnlMainSearch
      '
      Me.pnlMainSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.pnlMainSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
      Me.pnlMainSearch.Controls.Add(Me.lblSearchHeading)
      Me.pnlMainSearch.Controls.Add(Me.lblSearchText)
      Me.pnlMainSearch.Controls.Add(Me.lblFileTypes)
      Me.pnlMainSearch.Controls.Add(Me.cboSearchForText)
      Me.pnlMainSearch.Controls.Add(Me.cboFileName)
      Me.pnlMainSearch.Controls.Add(Me.cboFilePath)
      Me.pnlMainSearch.Controls.Add(Me.btnCancel)
      Me.pnlMainSearch.Controls.Add(Me.btnSearch)
      Me.pnlMainSearch.Controls.Add(Me.lblSearchPath)
      Me.pnlMainSearch.Location = New System.Drawing.Point(16, 8)
      Me.pnlMainSearch.Name = "pnlMainSearch"
      Me.pnlMainSearch.Size = New System.Drawing.Size(192, 192)
      Me.pnlMainSearch.TabIndex = 7
      '
      'lblSearchHeading
      '
      Me.lblSearchHeading.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.lblSearchHeading.BackColor = System.Drawing.SystemColors.ActiveCaption
      Me.lblSearchHeading.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.lblSearchHeading.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
      Me.lblSearchHeading.Location = New System.Drawing.Point(0, 0)
      Me.lblSearchHeading.Name = "lblSearchHeading"
      Me.lblSearchHeading.Size = New System.Drawing.Size(192, 16)
      Me.lblSearchHeading.TabIndex = 17
      Me.lblSearchHeading.Text = " AstroGrep Search"
      '
      'lblSearchText
      '
      Me.lblSearchText.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.lblSearchText.Location = New System.Drawing.Point(8, 104)
      Me.lblSearchText.Name = "lblSearchText"
      Me.lblSearchText.Size = New System.Drawing.Size(100, 16)
      Me.lblSearchText.TabIndex = 16
      Me.lblSearchText.Text = "Search Text"
      '
      'lblFileTypes
      '
      Me.lblFileTypes.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.lblFileTypes.Location = New System.Drawing.Point(8, 64)
      Me.lblFileTypes.Name = "lblFileTypes"
      Me.lblFileTypes.Size = New System.Drawing.Size(120, 16)
      Me.lblFileTypes.TabIndex = 15
      Me.lblFileTypes.Text = "File Types"
      '
      'cboSearchForText
      '
      Me.cboSearchForText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.cboSearchForText.Location = New System.Drawing.Point(8, 120)
      Me.cboSearchForText.Name = "cboSearchForText"
      Me.cboSearchForText.Size = New System.Drawing.Size(176, 21)
      Me.cboSearchForText.TabIndex = 14
      '
      'cboFileName
      '
      Me.cboFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.cboFileName.Location = New System.Drawing.Point(8, 80)
      Me.cboFileName.Name = "cboFileName"
      Me.cboFileName.Size = New System.Drawing.Size(176, 21)
      Me.cboFileName.TabIndex = 13
      '
      'cboFilePath
      '
      Me.cboFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.cboFilePath.Location = New System.Drawing.Point(8, 40)
      Me.cboFilePath.Name = "cboFilePath"
      Me.cboFilePath.Size = New System.Drawing.Size(176, 21)
      Me.cboFilePath.TabIndex = 12
      '
      'btnCancel
      '
      Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.btnCancel.Enabled = False
      Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnCancel.Location = New System.Drawing.Point(104, 160)
      Me.btnCancel.Name = "btnCancel"
      Me.btnCancel.TabIndex = 11
      Me.btnCancel.Text = "&Cancel"
      '
      'btnSearch
      '
      Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnSearch.Location = New System.Drawing.Point(8, 160)
      Me.btnSearch.Name = "btnSearch"
      Me.btnSearch.TabIndex = 10
      Me.btnSearch.Text = "&Search"
      '
      'lblSearchPath
      '
      Me.lblSearchPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.lblSearchPath.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.lblSearchPath.Location = New System.Drawing.Point(8, 24)
      Me.lblSearchPath.Name = "lblSearchPath"
      Me.lblSearchPath.Size = New System.Drawing.Size(136, 16)
      Me.lblSearchPath.TabIndex = 9
      Me.lblSearchPath.Text = "Search Path"
      '
      'pnlSearchOptions
      '
      Me.pnlSearchOptions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.pnlSearchOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
      Me.pnlSearchOptions.Controls.Add(Me.lblContextLines)
      Me.pnlSearchOptions.Controls.Add(Me.txtContextLines)
      Me.pnlSearchOptions.Controls.Add(Me.chkLineNumbers)
      Me.pnlSearchOptions.Controls.Add(Me.chkNegation)
      Me.pnlSearchOptions.Controls.Add(Me.chkFileNamesOnly)
      Me.pnlSearchOptions.Controls.Add(Me.chkRecurse)
      Me.pnlSearchOptions.Controls.Add(Me.chkWholeWordOnly)
      Me.pnlSearchOptions.Controls.Add(Me.chkCaseSensitive)
      Me.pnlSearchOptions.Controls.Add(Me.chkRegularExpressions)
      Me.pnlSearchOptions.Controls.Add(Me.lnkSearchOptions)
      Me.pnlSearchOptions.Location = New System.Drawing.Point(16, 211)
      Me.pnlSearchOptions.Name = "pnlSearchOptions"
      Me.pnlSearchOptions.Size = New System.Drawing.Size(192, 224)
      Me.pnlSearchOptions.TabIndex = 6
      '
      'lblContextLines
      '
      Me.lblContextLines.Location = New System.Drawing.Point(48, 192)
      Me.lblContextLines.Name = "lblContextLines"
      Me.lblContextLines.Size = New System.Drawing.Size(100, 16)
      Me.lblContextLines.TabIndex = 9
      Me.lblContextLines.Text = "Context Lines"
      Me.ToolTip1.SetToolTip(Me.lblContextLines, "Show lines above and below the word matched")
      '
      'txtContextLines
      '
      Me.txtContextLines.Location = New System.Drawing.Point(8, 192)
      Me.txtContextLines.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
      Me.txtContextLines.Name = "txtContextLines"
      Me.txtContextLines.Size = New System.Drawing.Size(32, 20)
      Me.txtContextLines.TabIndex = 8
      Me.ToolTip1.SetToolTip(Me.txtContextLines, "Show lines above and below the word matched")
      '
      'chkLineNumbers
      '
      Me.chkLineNumbers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.chkLineNumbers.Checked = True
      Me.chkLineNumbers.CheckState = System.Windows.Forms.CheckState.Checked
      Me.chkLineNumbers.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.chkLineNumbers.Location = New System.Drawing.Point(8, 168)
      Me.chkLineNumbers.Name = "chkLineNumbers"
      Me.chkLineNumbers.Size = New System.Drawing.Size(137, 16)
      Me.chkLineNumbers.TabIndex = 7
      Me.chkLineNumbers.Text = "&Line Numbers"
      Me.ToolTip1.SetToolTip(Me.chkLineNumbers, "Include line numbers before each match")
      '
      'chkNegation
      '
      Me.chkNegation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.chkNegation.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.chkNegation.Location = New System.Drawing.Point(8, 144)
      Me.chkNegation.Name = "chkNegation"
      Me.chkNegation.Size = New System.Drawing.Size(120, 16)
      Me.chkNegation.TabIndex = 6
      Me.chkNegation.Text = "&Negation"
      Me.ToolTip1.SetToolTip(Me.chkNegation, "Find the files without the Search Text in them")
      '
      'chkFileNamesOnly
      '
      Me.chkFileNamesOnly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.chkFileNamesOnly.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.chkFileNamesOnly.Location = New System.Drawing.Point(8, 120)
      Me.chkFileNamesOnly.Name = "chkFileNamesOnly"
      Me.chkFileNamesOnly.Size = New System.Drawing.Size(169, 16)
      Me.chkFileNamesOnly.TabIndex = 5
      Me.chkFileNamesOnly.Text = "Show File Names &Only"
      Me.ToolTip1.SetToolTip(Me.chkFileNamesOnly, "Show names but not contents of files that have matches (may be faster on large fi" & _
      "les)")
      '
      'chkRecurse
      '
      Me.chkRecurse.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.chkRecurse.Checked = True
      Me.chkRecurse.CheckState = System.Windows.Forms.CheckState.Checked
      Me.chkRecurse.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.chkRecurse.Location = New System.Drawing.Point(8, 96)
      Me.chkRecurse.Name = "chkRecurse"
      Me.chkRecurse.Size = New System.Drawing.Size(161, 16)
      Me.chkRecurse.TabIndex = 4
      Me.chkRecurse.Text = "&Recurse"
      Me.ToolTip1.SetToolTip(Me.chkRecurse, "Search in sub-directories")
      '
      'chkWholeWordOnly
      '
      Me.chkWholeWordOnly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.chkWholeWordOnly.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.chkWholeWordOnly.Location = New System.Drawing.Point(8, 72)
      Me.chkWholeWordOnly.Name = "chkWholeWordOnly"
      Me.chkWholeWordOnly.Size = New System.Drawing.Size(161, 16)
      Me.chkWholeWordOnly.TabIndex = 3
      Me.chkWholeWordOnly.Text = "&Whole Word"
      Me.ToolTip1.SetToolTip(Me.chkWholeWordOnly, "Only match entire words (not parts of words)")
      '
      'chkCaseSensitive
      '
      Me.chkCaseSensitive.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.chkCaseSensitive.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.chkCaseSensitive.Location = New System.Drawing.Point(8, 48)
      Me.chkCaseSensitive.Name = "chkCaseSensitive"
      Me.chkCaseSensitive.Size = New System.Drawing.Size(161, 16)
      Me.chkCaseSensitive.TabIndex = 2
      Me.chkCaseSensitive.Text = "&Case Sensitive"
      Me.ToolTip1.SetToolTip(Me.chkCaseSensitive, "Match upper and lower case letters exactly")
      '
      'chkRegularExpressions
      '
      Me.chkRegularExpressions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.chkRegularExpressions.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.chkRegularExpressions.Location = New System.Drawing.Point(8, 24)
      Me.chkRegularExpressions.Name = "chkRegularExpressions"
      Me.chkRegularExpressions.Size = New System.Drawing.Size(169, 16)
      Me.chkRegularExpressions.TabIndex = 1
      Me.chkRegularExpressions.Text = "Regular &Expressions"
      Me.ToolTip1.SetToolTip(Me.chkRegularExpressions, "Use ""regular expression"" matching")
      '
      'lnkSearchOptions
      '
      Me.lnkSearchOptions.ActiveLinkColor = System.Drawing.SystemColors.ActiveCaption
      Me.lnkSearchOptions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.lnkSearchOptions.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.lnkSearchOptions.LinkColor = System.Drawing.SystemColors.ActiveCaption
      Me.lnkSearchOptions.Location = New System.Drawing.Point(0, 0)
      Me.lnkSearchOptions.Name = "lnkSearchOptions"
      Me.lnkSearchOptions.Size = New System.Drawing.Size(192, 16)
      Me.lnkSearchOptions.TabIndex = 0
      Me.lnkSearchOptions.TabStop = True
      Me.lnkSearchOptions.Text = "Search Options >>"
      '
      'pnlRightSide
      '
      Me.pnlRightSide.Controls.Add(Me.splitUpDown)
      Me.pnlRightSide.Controls.Add(Me.pnlDetails)
      Me.pnlRightSide.Controls.Add(Me.pnlFiles)
      Me.pnlRightSide.Dock = System.Windows.Forms.DockStyle.Fill
      Me.pnlRightSide.Location = New System.Drawing.Point(240, 0)
      Me.pnlRightSide.Name = "pnlRightSide"
      Me.pnlRightSide.Size = New System.Drawing.Size(392, 433)
      Me.pnlRightSide.TabIndex = 1
      '
      'splitUpDown
      '
      Me.splitUpDown.Dock = System.Windows.Forms.DockStyle.Top
      Me.splitUpDown.Location = New System.Drawing.Point(0, 120)
      Me.splitUpDown.Name = "splitUpDown"
      Me.splitUpDown.Size = New System.Drawing.Size(392, 8)
      Me.splitUpDown.TabIndex = 2
      Me.splitUpDown.TabStop = False
      '
      'pnlDetails
      '
      Me.pnlDetails.Controls.Add(Me.txtHits)
      Me.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill
      Me.pnlDetails.DockPadding.Left = 7
      Me.pnlDetails.DockPadding.Top = 7
      Me.pnlDetails.Location = New System.Drawing.Point(0, 120)
      Me.pnlDetails.Name = "pnlDetails"
      Me.pnlDetails.Size = New System.Drawing.Size(392, 313)
      Me.pnlDetails.TabIndex = 1
      '
      'txtHits
      '
      Me.txtHits.Dock = System.Windows.Forms.DockStyle.Fill
      Me.txtHits.Location = New System.Drawing.Point(7, 7)
      Me.txtHits.Name = "txtHits"
      Me.txtHits.ReadOnly = True
      Me.txtHits.Size = New System.Drawing.Size(385, 306)
      Me.txtHits.TabIndex = 0
      Me.txtHits.Text = ""
      Me.txtHits.WordWrap = False
      '
      'pnlFiles
      '
      Me.pnlFiles.Controls.Add(Me.lstFileNames)
      Me.pnlFiles.Dock = System.Windows.Forms.DockStyle.Top
      Me.pnlFiles.DockPadding.Left = 7
      Me.pnlFiles.DockPadding.Right = 2
      Me.pnlFiles.Location = New System.Drawing.Point(0, 0)
      Me.pnlFiles.Name = "pnlFiles"
      Me.pnlFiles.Size = New System.Drawing.Size(392, 120)
      Me.pnlFiles.TabIndex = 0
      '
      'lstFileNames
      '
      Me.lstFileNames.Dock = System.Windows.Forms.DockStyle.Fill
      Me.lstFileNames.FullRowSelect = True
      Me.lstFileNames.HideSelection = False
      Me.lstFileNames.Location = New System.Drawing.Point(7, 0)
      Me.lstFileNames.Name = "lstFileNames"
      Me.lstFileNames.Size = New System.Drawing.Size(383, 120)
      Me.lstFileNames.TabIndex = 0
      Me.lstFileNames.View = System.Windows.Forms.View.Details
      '
      'splitLeftRight
      '
      Me.splitLeftRight.Location = New System.Drawing.Point(240, 0)
      Me.splitLeftRight.MinSize = 240
      Me.splitLeftRight.Name = "splitLeftRight"
      Me.splitLeftRight.Size = New System.Drawing.Size(8, 433)
      Me.splitLeftRight.TabIndex = 2
      Me.splitLeftRight.TabStop = False
      '
      'mnuAll
      '
      Me.mnuAll.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFile, Me.mnuEdit, Me.mnuTools, Me.mnuHelp})
      '
      'mnuFile
      '
      Me.mnuFile.Index = 0
      Me.mnuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuSaveResults, Me.mnuPrint, Me.mnuSepFile1, Me.mnuExit})
      Me.mnuFile.Text = "&File"
      '
      'mnuSaveResults
      '
      Me.mnuSaveResults.Index = 0
      Me.mnuSaveResults.Shortcut = System.Windows.Forms.Shortcut.CtrlS
      Me.mnuSaveResults.Text = "&Save Results"
      '
      'mnuPrint
      '
      Me.mnuPrint.Index = 1
      Me.mnuPrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP
      Me.mnuPrint.Text = "&Print..."
      '
      'mnuSepFile1
      '
      Me.mnuSepFile1.Index = 2
      Me.mnuSepFile1.Text = "-"
      '
      'mnuExit
      '
      Me.mnuExit.Index = 3
      Me.mnuExit.Shortcut = System.Windows.Forms.Shortcut.CtrlQ
      Me.mnuExit.Text = "E&xit"
      '
      'mnuEdit
      '
      Me.mnuEdit.Index = 1
      Me.mnuEdit.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuSelectAll, Me.mnuOpenSelected})
      Me.mnuEdit.Text = "&Edit"
      '
      'mnuSelectAll
      '
      Me.mnuSelectAll.Index = 0
      Me.mnuSelectAll.Text = "&Select All Files"
      '
      'mnuOpenSelected
      '
      Me.mnuOpenSelected.Index = 1
      Me.mnuOpenSelected.Text = "&Open Selected Files"
      '
      'mnuTools
      '
      Me.mnuTools.Index = 2
      Me.mnuTools.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuClearMRU, Me.mnuSepTools1, Me.mnuSaveSearchSettings, Me.mnuOptions})
      Me.mnuTools.Text = "&Tools"
      '
      'mnuClearMRU
      '
      Me.mnuClearMRU.Index = 0
      Me.mnuClearMRU.Text = "&Clear MRU Lists"
      '
      'mnuSepTools1
      '
      Me.mnuSepTools1.Index = 1
      Me.mnuSepTools1.Text = "-"
      '
      'mnuSaveSearchSettings
      '
      Me.mnuSaveSearchSettings.Index = 2
      Me.mnuSaveSearchSettings.Text = "&Save Search Options"
      '
      'mnuOptions
      '
      Me.mnuOptions.Index = 3
      Me.mnuOptions.Text = "&Options..."
      '
      'mnuHelp
      '
      Me.mnuHelp.Index = 3
      Me.mnuHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAbout})
      Me.mnuHelp.Text = "&Help"
      '
      'mnuAbout
      '
      Me.mnuAbout.Index = 0
      Me.mnuAbout.Text = "&About..."
      '
      'frmMain
      '
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.ClientSize = New System.Drawing.Size(632, 433)
      Me.Controls.Add(Me.splitLeftRight)
      Me.Controls.Add(Me.pnlRightSide)
      Me.Controls.Add(Me.pnlSearch)
      Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
      Me.Menu = Me.mnuAll
      Me.Name = "frmMain"
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
      Me.Text = "AstroGrep"
      Me.pnlSearch.ResumeLayout(False)
      Me.pnlMainSearch.ResumeLayout(False)
      Me.pnlSearchOptions.ResumeLayout(False)
      CType(Me.txtContextLines, System.ComponentModel.ISupportInitialize).EndInit()
      Me.pnlRightSide.ResumeLayout(False)
      Me.pnlDetails.ResumeLayout(False)
      Me.pnlFiles.ResumeLayout(False)
      Me.ResumeLayout(False)

   End Sub

#End Region

#Region "Form Events"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Form Load Event
   ''' </summary>
   ''' <param name="eventSender">System parm</param>
   ''' <param name="eventArgs">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' 	[Curtis_Beard]	   04/12/2005	ADD: Command line additions
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub frmMain_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
      ' Global variable used to cancel a search.
      GB_CANCEL = False

      'Add the column and their headers
      lstFileNames.Columns.Add("File", 100, HorizontalAlignment.Left)
      lstFileNames.Columns.Add("Located In", 200, HorizontalAlignment.Left)
      lstFileNames.Columns.Add("Date Modified", 150, HorizontalAlignment.Left)

      'Hide the Search Options
      __optionsShow = True
      ShowSearchOptions()

      'Load the settings and search options
      LoadRegistrySettings()
      LoadSearchSettings()
      SetSearchSettings()

      'Add the Browse.. item to the path list
      cboFilePath.Items.Insert(cboFilePath.Items.Count, __BROWSE)

      'Check command line for a path to start at
      If Environment.GetCommandLineArgs.Length > 1 Then
         AddComboSelectionSpecial(cboFilePath, CStr(Environment.GetCommandLineArgs.GetValue(1)))
      End If
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Closed Event - Save settings and exit
   ''' </summary>
   ''' <param name="sender">System parm</param>
   ''' <param name="e">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub frmMain_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

      'remove Browse... from file path list before saving to registry
      Dim _index As Integer = cboFilePath.Items.IndexOf(__BROWSE)
      If _index >= 0 Then
         cboFilePath.Items.RemoveAt(_index)
      End If

      UpdateRegistrySettings()
      End
   End Sub
#End Region

#Region "Menu Events"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Save the results to a file
   ''' </summary>
   ''' <param name="sender">System parm</param>
   ''' <param name="e">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   01/11/2005	Initial
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub mnuSaveResults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSaveResults.Click
      cdlgSaveFile.CheckPathExists = True
      cdlgSaveFile.AddExtension = True
      cdlgSaveFile.Title = "Select the file to save the results to"
      cdlgSaveFile.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

      'only show dialog if information to save
      If lstFileNames.Items.Count > 0 Then
         If cdlgSaveFile.ShowDialog = DialogResult.OK Then
            'attempt to save the results to a file
            Dim _hitObject As CHitObject
            Dim _index As Integer
            Dim _hitResult As String
            Dim _fileNumber As Integer
            Dim _hitIndex As Integer

            Try
               'Open the file
               _fileNumber = FreeFile()
               VB.FileOpen(_fileNumber, cdlgSaveFile.FileName, OpenMode.Output)

               stbStatus.Text = "Saving Results to file: " & cdlgSaveFile.FileName

               'loop through File Names list
               For _index = 0 To lstFileNames.Items.Count - 1
                  _hitIndex = CInt(lstFileNames.Items(_index).SubItems(3).Text)
                  'retrieve hit object
                  '_hitObject = G_HITS.Item(_index + 1)
                  _hitObject = G_HITS.Item(_hitIndex)

                  'write info to a file
                  VB.PrintLine(_fileNumber, "-------------------------------------------------------------------------------")
                  VB.PrintLine(_fileNumber, _hitObject.Path & _hitObject.FileName)
                  VB.PrintLine(_fileNumber, "-------------------------------------------------------------------------------")
                  VB.Print(_fileNumber, _hitObject.GetHitString)
                  VB.PrintLine(_fileNumber, "")

                  'clear hit object
                  _hitObject = Nothing
               Next

               'Close file
               VB.FileClose(_fileNumber)

               stbStatus.Text = "Results saved."
            Catch ex As Exception
               MessageBox.Show("Unable to save results: " & ex.ToString, "Unable to Save", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

         End If
      Else
         MessageBox.Show("There are no results to save.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
      End If

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Show Print Dialog
   ''' </summary>
   ''' <param name="sender">system parm</param>
   ''' <param name="e">system parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub mnuPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrint.Click

      If lstFileNames.Items.Count > 0 Then
         Dim _form As New frmPrint
         _form.ShowDialog(Me)
         _form = Nothing
      Else
         MessageBox.Show("There are currently no items available to print.", "No Items", MessageBoxButtons.OK, MessageBoxIcon.Information)
      End If

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Menu Exit
   ''' </summary>
   ''' <param name="sender">System parm</param>
   ''' <param name="e">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
      Me.Close()
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Menu Select All Event
   ''' </summary>
   ''' <param name="eventSender">System parm</param>
   ''' <param name="eventArgs">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub mnuSelectAll_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuSelectAll.Click
      Dim _index As Integer
      Dim _selected As Integer

      For _index = 0 To Me.lstFileNames.Items.Count - 1
         lstFileNames.Items(_index).Selected = True
      Next _index

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Open Selected Files Event
   ''' </summary>
   ''' <param name="sender">System parm</param>
   ''' <param name="e">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub mnuOpenSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOpenSelected.Click
      Dim _index As Integer
      Dim _fileName As String
      Dim _hitIndex As Integer

      For _index = 0 To lstFileNames.SelectedItems.Count - 1

         'retrieve the index
         _hitIndex = CInt(lstFileNames.SelectedItems.Item(_index).SubItems(3).Text)

         'retrieve the filename
         _fileName = G_HITS.Item(_hitIndex).Path & G_HITS.Item(_hitIndex).FileName

         'open the default editor
         EditFile(_fileName, 1)
      Next
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Clear MRU Event
   ''' </summary>
   ''' <param name="eventSender">System parm</param>
   ''' <param name="eventArgs">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub mnuClearMRU_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuClearMRU.Click
      ClearRegistrySettings()

      'Add back in the browse
      cboFilePath.Items.Add(__BROWSE)
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Save Search Settings Event
   ''' </summary>
   ''' <param name="sender">system parm</param>
   ''' <param name="e">system parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   01/28/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub mnuSaveSearchSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSaveSearchSettings.Click

      If VerifyInterface() Then
         Common.USE_REG_EXPRESSIONS = chkRegularExpressions.Checked
         Common.USE_CASE_SENSITIVE = chkCaseSensitive.Checked
         Common.USE_WHOLE_WORD = chkWholeWordOnly.Checked
         Common.USE_LINE_NUMBERS = chkLineNumbers.Checked
         Common.USE_RECURSION = chkRecurse.Checked
         Common.SHOW_FILE_NAMES_ONLY = chkFileNamesOnly.Checked
         Common.NUM_CONTEXT_LINES = CInt(txtContextLines.Text)
         Common.USE_NEGATION = chkNegation.Checked

         SaveSearchSettings()
      End If

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Menu Options Event
   ''' </summary>
   ''' <param name="eventSender">System parm</param>
   ''' <param name="eventArgs">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub mnuOptions_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuOptions.Click
      Dim _form As New frmProperties

      _form.ShowDialog()
      _form = Nothing
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Menu About Event
   ''' </summary>
   ''' <param name="eventSender">System parm</param>
   ''' <param name="eventArgs">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub mnuAbout_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuAbout.Click
      Dim _form As New frmAbout

      _form.ShowDialog()
      _form = Nothing
   End Sub
#End Region

#Region "Control Events"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Hide/Show the Search Options
   ''' </summary>
   ''' <param name="sender">System parm</param>
   ''' <param name="e">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   02/04/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub lnkSearchOptions_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSearchOptions.LinkClicked
      ShowSearchOptions()
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   File Name List Select Index Change Event
   ''' </summary>
   ''' <param name="eventSender">System parm</param>
   ''' <param name="eventArgs">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub lstFileNames_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstFileNames.SelectedIndexChanged

      If lstFileNames.SelectedIndices.Count > 0 Then
         Dim _hitObject As CHitObject
         Dim _index As Integer

         'retrieve the index
         _index = CInt(lstFileNames.SelectedItems.Item(0).SubItems(3).Text)

         _hitObject = G_HITS.Item(_index)

         HighlightText(_hitObject)

         _hitObject = Nothing
      End If

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   File Name List Double Click Event
   ''' </summary>
   ''' <param name="eventSender">System parm</param>
   ''' <param name="eventArgs">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub lstFileNames_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstFileNames.DoubleClick
      Dim _fileName As String
      Dim _index As Integer

      ' Make sure there is something to click on
      If G_HITS.Count = 0 Then Exit Sub

      'retrieve the index
      _index = CInt(lstFileNames.SelectedItems.Item(0).SubItems(3).Text)

      ' Retrieve the filename
      _fileName = G_HITS.Item(_index).Path & G_HITS.Item(_index).FileName

      ' Open the default editor.
      EditFile(_fileName, 1)
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   txtHits Mouse Down Event - Used to detect a double click
   ''' </summary>
   ''' <param name="eventSender">System parm</param>
   ''' <param name="eventArgs">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion/Replaced with mouse down
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub txtHits_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtHits.MouseDown
      If e.Clicks = 2 Then
         Dim _lineNumber As Integer
         Dim _fileName As String
         Dim _hitLineNumber As Integer
         Dim _index As Integer

         ' Make sure there is something to click on.
         If G_HITS.Count = 0 Then Exit Sub

         'retrieve the index
         _index = CInt(lstFileNames.SelectedItems.Item(0).SubItems(3).Text)

         ' Find out the line number the cursor is on.
         _lineNumber = txtHits.GetLineFromCharIndex(txtHits.SelectionStart) + 1

         ' Use the cursor's linenumber to get the hit's line number.
         _hitLineNumber = G_HITS.Item(_index).GetHitLineNumber(_lineNumber)

         ' Retrieve the filename
         _fileName = G_HITS.Item(_index).Path & G_HITS.Item(_index).FileName

         ' Open the default editor.
         EditFile(_fileName, _hitLineNumber)
      End If
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Cancel Button Event
   ''' </summary>
   ''' <param name="eventSender">System parm</param>
   ''' <param name="eventArgs">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub btnCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles btnCancel.Click
      SetSearch(False)
      SetSearchState(True)
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Search Button Event
   ''' </summary>
   ''' <param name="eventSender">System parm</param>
   ''' <param name="eventArgs">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub btnSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles btnSearch.Click
      Dim _path As String
      Dim _fileName As String
      Dim _index As Integer
      Dim _expression As String

      If VerifyInterface() = False Then Exit Sub

      _fileName = cboFileName.Text
      _path = cboFilePath.Text.Trim
      _expression = cboSearchForText.Text.Trim

      'AddPathComboSelection Path
      AddComboSelection(cboSearchForText, _expression)
      AddComboSelection(cboFileName, _fileName)

      'AddComboSelection(cboFilePath, _path)
      AddComboSelectionSpecial(cboFilePath, _path)

      ' Ensure that there is a backslash.
      If Not _path.EndsWith("\") Then _path &= "\"

      For _index = Len(_fileName) To 1 Step -1
         If Mid(_fileName, _index, 1) = "\" Then
            _path = VB.Left(_fileName, _index)
            _fileName = VB.Right(_fileName, Len(_fileName) - _index)
            Exit For
         End If
      Next _index

      'disable
      SetSearchState(False)

      ' Clear the display
      Common.mainForm.stbStatus.Text = ""
      Common.mainForm.lstFileNames.Items.Clear()
      Common.mainForm.txtHits.Text = ""

      'begin searching
      Search(_path, _fileName, _expression, GetSearchOptions)

      'enable
      SetSearchState(True)

      'Update status bar
      Common.mainForm.stbStatus.Text = "Search Finished"

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   File Names Only Check Box Event
   ''' </summary>
   ''' <param name="eventSender">System parm</param>
   ''' <param name="eventArgs">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   '''   [Curtis_Beard]	   06/13/2005	CHG: Gray out context lines label
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub chkFileNamesOnly_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFileNamesOnly.CheckStateChanged
      If chkFileNamesOnly.Checked Then
         chkLineNumbers.Enabled = False
         txtContextLines.Enabled = False
         lblContextLines.Enabled = False
      Else
         chkLineNumbers.Enabled = True
         txtContextLines.Enabled = True
         lblContextLines.Enabled = True
      End If
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Negation check event
   ''' </summary>
   ''' <param name="sender">system parm</param>
   ''' <param name="e">system parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   01/28/2005	Created
   ''' 	[Curtis_Beard]	   06/13/2005	CHG: Gray out file names only when checked
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub chkNegation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNegation.CheckStateChanged
      chkFileNamesOnly.Checked = chkNegation.Checked
      chkFileNamesOnly.Enabled = Not chkNegation.Checked
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Allow sorting of list view columns
   ''' </summary>
   ''' <param name="sender">System parm</param>
   ''' <param name="e">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   02/06/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub lstFileNames_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lstFileNames.ColumnClick

      ' Determine whether the column is the same as the last column clicked.
      If e.Column <> __sortColumn Then
         ' Set the sort column to the new column.
         __sortColumn = e.Column
         ' Set the sort order to ascending by default.
         lstFileNames.Sorting = SortOrder.Ascending
      Else
         ' Determine what the last sort order was and change it.
         If lstFileNames.Sorting = SortOrder.Ascending Then
            lstFileNames.Sorting = SortOrder.Descending
         Else
            lstFileNames.Sorting = SortOrder.Ascending
         End If
      End If

      ' Set the ListViewItemSorter property to a new ListViewItemComparer
      ' object.
      lstFileNames.ListViewItemSorter = New ListViewItemComparer(e.Column, _
                                                lstFileNames.Sorting)

      ' Call the sort method to manually sort.
      lstFileNames.Sort()
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Open Browser for Folder dialog if Browse selected
   ''' </summary>
   ''' <param name="sender">System parm</param>
   ''' <param name="e">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   02/11/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub cboFilePath_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFilePath.SelectedIndexChanged

      If cboFilePath.SelectedItem.Equals(__BROWSE) Then
         cdlgFolderBrowse.Description = "Please select a starting folder"
         cdlgFolderBrowse.ShowNewFolderButton = False

         If cdlgFolderBrowse.ShowDialog = DialogResult.OK Then
            AddComboSelectionSpecial(cboFilePath, cdlgFolderBrowse.SelectedPath)
         Else
            If cboFilePath.Items.Count > 0 Then
               cboFilePath.SelectedIndex = 0
            End If
         End If
      End If

   End Sub

#End Region

#Region "Private Methods"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Verify user selected options
   ''' </summary>
   ''' <returns>True - Verified, False - Otherwise</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Function VerifyInterface() As Boolean

      Dim _lines As Integer

      Try
         Try
            _lines = CInt(txtContextLines.Text)
            If _lines < 0 OrElse _lines > MAX_CONTEXT_LINES Then
               MessageBox.Show("Number of context lines must be between 0 and 9", _
                     "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
               Return False
            End If
         Catch ex As Exception
            MessageBox.Show("Number of context lines must be between 0 and 9", _
                     "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
         End Try


         If cboFileName.Text.Trim.Equals(String.Empty) Then
            MessageBox.Show("You must supply the name of a file in which to search.", _
                  "Missing Parameter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
         End If

         If cboFilePath.Text.Trim.Equals(String.Empty) Then
            MessageBox.Show("You must supply the path to begin the search.", _
                  "Missing Parameter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
         End If

         If Not System.IO.Directory.Exists(cboFilePath.Text.Trim) Then
            MessageBox.Show(String.Format("'{0}' is not a valid folder.", cboFilePath.Text), _
                  "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
         End If

         If cboSearchForText.Text.Trim.Equals(String.Empty) Then
            MessageBox.Show("You must supply text for which to search.", _
                  "Missing Parameter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
         End If

      Catch ex As Exception
         MessageBox.Show("Unable to validate specified parameters.  Please verify they are valid.", _
               "AstroGrep Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
         Return False
      End Try

      Return True
   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Add an item to a combo box
   ''' </summary>
   ''' <param name="cbo">Combo Box</param>
   ''' <param name="newItem">Item to add</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub AddComboSelection(ByRef combo As System.Windows.Forms.ComboBox, ByRef newItem As String)
      Dim _index As Integer

      ' If this path is already in the dropdown, remove it.
      For _index = 0 To combo.Items.Count - 1
         If CStr(combo.Items.Item(_index)).Equals(newItem) Then
            combo.Items.RemoveAt(_index)
            Exit For
         End If
      Next _index

      ' Add this path as the first item in the dropdown.
      combo.Items.Insert(0, newItem)

      ' The combo text gets cleared by the AddItem.
      combo.Text = newItem

      ' Only store as many paths as has been set in options.
      If combo.Items.Count > NUM_STORED_PATHS Then
         ' Remove the last item in the list.
         combo.Items.RemoveAt(combo.Items.Count - 1)
      End If

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Add an item to a combo box that needs the Browse.. as last selection
   ''' </summary>
   ''' <param name="combo">Combo Box</param>
   ''' <param name="newItem">Item to add</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	01/11/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub AddComboSelectionSpecial(ByRef combo As ComboBox, ByVal newItem As String)
      'remove Browse.. at end of list
      Dim _index As Integer = combo.Items.IndexOf(__BROWSE)
      If _index >= 0 Then
         combo.Items.RemoveAt(_index)
      End If

      'Add item to list
      AddComboSelection(combo, newItem)

      'add Browse...
      combo.Items.Insert(combo.Items.Count, __BROWSE)
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Enable/Disable menu items
   ''' </summary>
   ''' <param name="show">True - enable menu items, False - disable</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub SetSearchState(ByVal enable As Boolean)
      mnuFile.Enabled = enable
      mnuEdit.Enabled = enable
      mnuTools.Enabled = enable
      mnuHelp.Enabled = enable

      btnSearch.Enabled = enable
      btnCancel.Enabled = Not enable
      pnlSearchOptions.Enabled = enable
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Highlight the searched text in the results
   ''' </summary>
   ''' <param name="hitObject">Hit Object containing results</param>
   ''' <remarks>
   '''   - Currently only supports non-regular expression searches
   '''   - Does support whole word vs partial matches
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   01/27/2005	Created
   ''' 	[Curtis_Beard]	   04/12/2005	FIX: 1180741, Don't capitalize hit line
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub HighlightText(ByVal hitObject As CHitObject)
      Dim _textToSearch As String
      Dim _searchText As String = cboSearchForText.Text.Trim
      Dim _index As Integer
      Dim _tempLine As String

      Dim _begin As String
      Dim _text As String
      Dim _end As String
      Dim _pos As Integer
      Dim _highlight As Boolean = False

      'Clear the contents
      txtHits.Text = ""

      ' Don't attempt highlight if regular expression
      If chkRegularExpressions.Checked Then
         'just display the results
         txtHits.Text = hitObject.GetHitString
      Else
         ' Loop through hits and highlight search for text
         For _index = 1 To hitObject.GetNumHits
            'Retrieve hit text
            _textToSearch = hitObject.GetHit(_index)

            'Set default font
            txtHits.SelectionFont = New Font("Courier New", 9.75, FontStyle.Regular)

            _tempLine = _textToSearch

            'attempt to locate the text in the line
            If chkCaseSensitive.Checked Then
               _pos = InStr(_tempLine, _searchText, CompareMethod.Binary)
            Else
               _pos = InStr(_tempLine, _searchText, CompareMethod.Text)
            End If

            If _pos > 0 Then

               Do While _pos > 0
                  _highlight = False
                  'found it(should always be)
                  '
                  'retrieve first part of text
                  _begin = VB.Left(_tempLine, _pos - 1)
                  _text = Mid(_tempLine, _pos, Len(_searchText))
                  _end = Mid(_tempLine, _pos + Len(_searchText))

                  txtHits.SelectionColor = SystemColors.WindowText
                  txtHits.SelectedText = _begin

                  'do a check to see if begin and end are valid for wholeword searches
                  If chkWholeWordOnly.Checked Then
                     _highlight = WholeWordOnly(_begin, _end)
                  Else
                     _highlight = True
                  End If

                  If _highlight Then txtHits.SelectionColor = Color.Blue
                  txtHits.SelectedText = _text

                  'Check remaining string for other hits in same line
                  If chkCaseSensitive.Checked Then
                     _pos = InStr(_end, _searchText, CompareMethod.Binary)
                  Else
                     _pos = InStr(_end, _searchText, CompareMethod.Text)
                  End If

                  _tempLine = _end
                  If _pos <= 0 Then
                     txtHits.SelectionColor = SystemColors.WindowText
                     txtHits.SelectedText = _end
                  End If

               Loop

            Else
               txtHits.SelectionColor = SystemColors.WindowText
               txtHits.SelectedText = _textToSearch
            End If

         Next
      End If

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Set the Common Search Settings on the form
   ''' </summary>
   ''' <remarks>
   '''   Should call Common.LoadSearchSettings to load the values
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   01/28/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub SetSearchSettings()
      chkRegularExpressions.Checked = Common.USE_REG_EXPRESSIONS
      chkCaseSensitive.Checked = Common.USE_CASE_SENSITIVE
      chkWholeWordOnly.Checked = Common.USE_WHOLE_WORD
      chkLineNumbers.Checked = Common.USE_LINE_NUMBERS
      chkRecurse.Checked = Common.USE_RECURSION
      chkFileNamesOnly.Checked = Common.SHOW_FILE_NAMES_ONLY
      txtContextLines.Text = CStr(Common.NUM_CONTEXT_LINES)
      chkNegation.Checked = Common.USE_NEGATION
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Show/Hide the Search Options Panel
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   02/05/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub ShowSearchOptions()
      Const _PANEL_HEIGHT As Integer = 224

      If __optionsShow Then
         ' hide and set text
         lnkSearchOptions.Text = "Search Options >>"
         lnkSearchOptions.LinkBehavior = LinkBehavior.AlwaysUnderline
         lnkSearchOptions.BackColor = pnlSearchOptions.BackColor
         lnkSearchOptions.LinkColor = SystemColors.ActiveCaption
         lnkSearchOptions.ActiveLinkColor = SystemColors.ActiveCaption
         pnlSearchOptions.BorderStyle = BorderStyle.None
         pnlSearchOptions.Height = lnkSearchOptions.Height

         __optionsShow = False
      Else
         ' set text
         lnkSearchOptions.Text = "Search Options <<"
         lnkSearchOptions.LinkBehavior = LinkBehavior.NeverUnderline
         lnkSearchOptions.BackColor = SystemColors.ActiveCaption
         lnkSearchOptions.LinkColor = SystemColors.ActiveCaptionText
         lnkSearchOptions.ActiveLinkColor = SystemColors.ActiveCaptionText
         pnlSearchOptions.BorderStyle = BorderStyle.FixedSingle
         pnlSearchOptions.Height = _PANEL_HEIGHT
         pnlSearchOptions.BringToFront()

         __optionsShow = True
      End If
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Retrieve the user selected search options
   ''' </summary> 
   ''' <returns>Search Options in a structure</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   02/06/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Function GetSearchOptions() As searchOptions
      Dim _options As New searchOptions

      'retrieve values from user selected search options
      _options.caseSensistiveMatch = chkCaseSensitive.Checked
      _options.contextLines = CInt(txtContextLines.Value)
      _options.includeLineNumbers = chkLineNumbers.Checked
      _options.negation = chkNegation.Checked
      _options.onlyFileNames = chkFileNamesOnly.Checked
      _options.recursiveSearch = chkRecurse.Checked
      _options.useRegularExpressions = chkRegularExpressions.Checked
      _options.wholeWordMatch = chkWholeWordOnly.Checked

      Return _options
   End Function
#End Region

End Class
