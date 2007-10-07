VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "Richtx32.ocx"
Object = "{FE0065C0-1B7B-11CF-9D53-00AA003C9CB6}#1.1#0"; "comct232.ocx"
Object = "{6B7E6392-850A-101B-AFC0-4210102A8DA7}#1.3#0"; "comctl32.ocx"
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Begin VB.Form frmMain 
   Caption         =   "AstroGrep"
   ClientHeight    =   5490
   ClientLeft      =   165
   ClientTop       =   855
   ClientWidth     =   6150
   Icon            =   "frmMain.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   5490
   ScaleWidth      =   6150
   StartUpPosition =   3  'Windows Default
   Begin ComctlLib.StatusBar sbStatusBar 
      Align           =   2  'Align Bottom
      Height          =   372
      Left            =   0
      TabIndex        =   29
      Top             =   5112
      Width           =   6156
      _ExtentX        =   10848
      _ExtentY        =   661
      SimpleText      =   ""
      _Version        =   327682
      BeginProperty Panels {0713E89E-850A-101B-AFC0-4210102A8DA7} 
         NumPanels       =   1
         BeginProperty Panel1 {0713E89F-850A-101B-AFC0-4210102A8DA7} 
            Object.Tag             =   ""
         EndProperty
      EndProperty
   End
   Begin VB.Frame fraResults 
      BorderStyle     =   0  'None
      Caption         =   "Frame1"
      Height          =   255
      Left            =   120
      TabIndex        =   24
      Top             =   1800
      Width           =   1935
      Begin VB.Label lblResults 
         Appearance      =   0  'Flat
         Caption         =   "Results:"
         ForeColor       =   &H80000008&
         Height          =   255
         Left            =   0
         TabIndex        =   25
         Top             =   0
         Width           =   1575
      End
   End
   Begin VB.Frame fraSearching 
      BorderStyle     =   0  'None
      Caption         =   "Frame1"
      Height          =   1695
      Left            =   120
      TabIndex        =   18
      Top             =   3000
      Width           =   5535
      Begin VB.CommandButton btnCancel 
         Caption         =   "&Cancel"
         Height          =   315
         Left            =   4560
         TabIndex        =   12
         Top             =   1320
         Width           =   915
      End
      Begin VB.Label lblExpression 
         Height          =   252
         Left            =   0
         TabIndex        =   30
         Top             =   240
         Width           =   5412
      End
      Begin VB.Label Label6 
         Caption         =   "Searching..."
         Height          =   252
         Left            =   0
         TabIndex        =   23
         Top             =   0
         Width           =   972
      End
      Begin VB.Label lblSearchFile 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BorderStyle     =   1  'Fixed Single
         ForeColor       =   &H80000008&
         Height          =   288
         Left            =   480
         TabIndex        =   22
         Top             =   960
         Width           =   4932
      End
      Begin VB.Label Label5 
         AutoSize        =   -1  'True
         Caption         =   "Path:"
         Height          =   192
         Left            =   0
         TabIndex        =   21
         Top             =   600
         Width           =   372
      End
      Begin VB.Label lblSearchDirectory 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BorderStyle     =   1  'Fixed Single
         ForeColor       =   &H80000008&
         Height          =   288
         Left            =   480
         TabIndex        =   20
         Top             =   600
         Width           =   4932
      End
      Begin VB.Label Label2 
         AutoSize        =   -1  'True
         Caption         =   "File:"
         Height          =   192
         Left            =   0
         TabIndex        =   19
         Top             =   960
         Width           =   288
      End
   End
   Begin VB.Frame fraMain 
      BorderStyle     =   0  'None
      Caption         =   "Frame1"
      Height          =   1815
      Left            =   240
      TabIndex        =   14
      Top             =   0
      Width           =   5532
      Begin ComCtl2.UpDown UpDown1 
         Height          =   255
         Left            =   4100
         TabIndex        =   31
         ToolTipText     =   "Show lines above and below the word matched"
         Top             =   1140
         Width           =   255
         _ExtentX        =   450
         _ExtentY        =   450
         _Version        =   327681
         AutoBuddy       =   -1  'True
         BuddyControl    =   "lblContextLines"
         BuddyDispid     =   196632
         OrigLeft        =   4100
         OrigTop         =   1140
         OrigRight       =   4355
         OrigBottom      =   1395
         Max             =   9
         SyncBuddy       =   -1  'True
         BuddyProperty   =   0
         Enabled         =   -1  'True
      End
      Begin VB.CheckBox chkFileNamesOnly 
         Appearance      =   0  'Flat
         Caption         =   "Show file names only"
         ForeColor       =   &H80000008&
         Height          =   195
         Left            =   3120
         TabIndex        =   9
         ToolTipText     =   "Show names but not contents of files that have matches (may be faster on large files)"
         Top             =   1440
         Width           =   1935
      End
      Begin VB.CheckBox chkRecurse 
         Appearance      =   0  'Flat
         Caption         =   "Recurse"
         ForeColor       =   &H80000008&
         Height          =   252
         Left            =   3120
         TabIndex        =   8
         ToolTipText     =   "Search in sub-directories"
         Top             =   1140
         Value           =   1  'Checked
         Width           =   975
      End
      Begin VB.CheckBox chkWholeWordOnly 
         Appearance      =   0  'Flat
         Caption         =   "Whole word"
         ForeColor       =   &H80000008&
         Height          =   252
         Left            =   1800
         TabIndex        =   6
         ToolTipText     =   "Only match entire words (not parts of words)"
         Top             =   1140
         Width           =   1332
      End
      Begin VB.ComboBox cboSearchForText 
         Height          =   315
         Left            =   960
         TabIndex        =   3
         Top             =   720
         Width           =   3550
      End
      Begin VB.CheckBox chkRegularExpressions 
         Appearance      =   0  'Flat
         Caption         =   "Regular Expressions"
         ForeColor       =   &H80000008&
         Height          =   255
         Left            =   0
         TabIndex        =   4
         ToolTipText     =   "Use ""regular expression"" matching"
         Top             =   1140
         Width           =   1815
      End
      Begin VB.ComboBox cboFilePath 
         Height          =   315
         Left            =   960
         TabIndex        =   1
         Top             =   0
         Width           =   4530
      End
      Begin VB.CommandButton btnSearch 
         Appearance      =   0  'Flat
         Caption         =   "&Search"
         Default         =   -1  'True
         Height          =   315
         Left            =   4560
         TabIndex        =   10
         Top             =   360
         Width           =   915
      End
      Begin VB.CommandButton btnExit 
         Appearance      =   0  'Flat
         Caption         =   "E&xit"
         Height          =   315
         Left            =   4560
         TabIndex        =   11
         Top             =   720
         Width           =   915
      End
      Begin VB.CheckBox chkCaseSensitive 
         Appearance      =   0  'Flat
         Caption         =   "Case sensitive"
         ForeColor       =   &H80000008&
         Height          =   195
         Left            =   0
         TabIndex        =   5
         ToolTipText     =   "Match upper and lower case letters exactly"
         Top             =   1440
         Width           =   1455
      End
      Begin VB.CheckBox chkLineNumbers 
         Appearance      =   0  'Flat
         Caption         =   "Line numbers"
         ForeColor       =   &H80000008&
         Height          =   195
         Left            =   1800
         TabIndex        =   7
         ToolTipText     =   "Include line numbers before each match"
         Top             =   1440
         Value           =   1  'Checked
         Width           =   1332
      End
      Begin VB.CommandButton btnBrowse 
         Caption         =   "&Path..."
         Height          =   315
         Left            =   0
         TabIndex        =   0
         Top             =   0
         Width           =   915
      End
      Begin VB.ComboBox cboFileName 
         Appearance      =   0  'Flat
         Height          =   315
         ItemData        =   "frmMain.frx":030A
         Left            =   960
         List            =   "frmMain.frx":032F
         TabIndex        =   2
         Top             =   360
         Width           =   3550
      End
      Begin VB.Label lblContextLines 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BorderStyle     =   1  'Fixed Single
         Caption         =   "0"
         ForeColor       =   &H80000008&
         Height          =   255
         Left            =   4440
         TabIndex        =   28
         ToolTipText     =   "Show lines above and below the word matched"
         Top             =   1140
         Width           =   135
      End
      Begin VB.Label Label3 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         Caption         =   "Search Text:"
         ForeColor       =   &H80000008&
         Height          =   195
         Left            =   0
         TabIndex        =   17
         Top             =   765
         Width           =   915
      End
      Begin VB.Label lblContext 
         Appearance      =   0  'Flat
         Caption         =   "Context lines"
         ForeColor       =   &H80000008&
         Height          =   255
         Left            =   4560
         TabIndex        =   16
         ToolTipText     =   "Show lines above and below the word matched"
         Top             =   1140
         Width           =   975
      End
      Begin VB.Label Label4 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         Caption         =   "File Name(s):"
         ForeColor       =   &H80000008&
         Height          =   195
         Left            =   0
         TabIndex        =   15
         Top             =   420
         Width           =   915
      End
   End
   Begin VB.PictureBox picSplitter 
      BackColor       =   &H00808080&
      BorderStyle     =   0  'None
      FillColor       =   &H00808080&
      Height          =   4800
      Left            =   5280
      ScaleHeight     =   2090.126
      ScaleMode       =   0  'User
      ScaleWidth      =   780
      TabIndex        =   27
      Top             =   480
      Visible         =   0   'False
      Width           =   72
   End
   Begin VB.ListBox lstFileNames 
      Appearance      =   0  'Flat
      Height          =   1740
      IntegralHeight  =   0   'False
      Left            =   120
      MultiSelect     =   2  'Extended
      TabIndex        =   26
      Top             =   2160
      Width           =   1935
   End
   Begin RichTextLib.RichTextBox txtHits 
      Height          =   3135
      Left            =   2280
      TabIndex        =   13
      Top             =   2160
      Width           =   2115
      _ExtentX        =   3731
      _ExtentY        =   5530
      _Version        =   393217
      ReadOnly        =   -1  'True
      ScrollBars      =   3
      Appearance      =   0
      RightMargin     =   99999
      AutoVerbMenu    =   -1  'True
      TextRTF         =   $"frmMain.frx":03BB
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Courier New"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
   End
   Begin MSComDlg.CommonDialog cdlgPrinter 
      Left            =   45
      Top             =   4560
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.Image imgSplitter 
      Appearance      =   0  'Flat
      BorderStyle     =   1  'Fixed Single
      Height          =   4785
      Left            =   2040
      MousePointer    =   9  'Size W E
      Top             =   720
      Width           =   80
   End
   Begin VB.Menu mnuPrint 
      Caption         =   "&Print"
      Begin VB.Menu mnuPrinterSetup 
         Caption         =   "&Setup printer..."
      End
      Begin VB.Menu mnuPrintFileList 
         Caption         =   "&Print List of Filenames"
      End
      Begin VB.Menu mnuPrintCurrent 
         Caption         =   "&Print Current"
      End
      Begin VB.Menu mnuPrintAllHits 
         Caption         =   "&Print Selected"
      End
   End
   Begin VB.Menu mnuTools 
      Caption         =   "&Tools"
      Begin VB.Menu mnuSelectAll 
         Caption         =   "&Select All"
      End
      Begin VB.Menu sep2 
         Caption         =   "-"
      End
      Begin VB.Menu mEditAll 
         Caption         =   "&Edit selected files"
      End
      Begin VB.Menu seperator1 
         Caption         =   "-"
      End
      Begin VB.Menu mnuOptions 
         Caption         =   "&Options..."
      End
      Begin VB.Menu mnuClearMRU 
         Caption         =   "&Clear MRU list"
      End
   End
   Begin VB.Menu mnuAbout 
      Caption         =   "&About..."
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
' AstroGrep File Searching Utility. Written by Theodore L. Ward
' Copyright (C) 2002 AstroComma Incorporated.
'
' This program is free software; you can redistribute it and/or
' modify it under the terms of the GNU General Public License
' as published by the Free Software Foundation; either version 2
' of the License, or (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program; if not, write to the Free Software
' Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
' The author may be contacted at:
' TheodoreWard@Hotmail.com or TheodoreWard@Yahoo.com

Option Explicit

Const GBORDER_SIZE = 120
Const SGLSPLITLIMIT = 500

Dim mbMoving As Boolean

Private Sub btnBrowse_Click()
    frmBrowse.Show vbModal
    If frmBrowse.mCancel = False Then
        Me.cboFilePath.Text = frmBrowse.Dir1.Path
    End If
End Sub

Private Sub btnCancel_Click()
    SetSearch False
End Sub

Private Sub btnExit_Click()
    UpdateRegistrySettings
    End
End Sub

Private Function VerifyInterface() As Boolean

    VerifyInterface = False
    On Error GoTo contexterror
    If Me.lblContextLines.Caption < 0 Or Me.lblContextLines.Caption > 9 Then
contexterror:
        MsgBox "Number of context lines must be between 0 and 9", vbOKOnly + vbCritical, "Error"
        Exit Function
    End If
    VerifyInterface = True
End Function
Private Sub btnSearch_Click()
    Dim Path As String
    Dim fn As String
    Dim i As Integer
    Dim expression As String
    
    If VerifyInterface = False Then Exit Sub
    
    fn = Me.cboFileName.Text
    Path = Trim(cboFilePath.Text)
    expression = Trim(cboSearchForText)
    
    'AddPathComboSelection Path
    AddComboSelection frmMain.cboSearchForText, expression
    AddComboSelection frmMain.cboFileName, fn
    AddComboSelection frmMain.cboFilePath, Path
    
    ' Ensure that there is a backslash.
    If Right$(Path, 1) <> "\" Then Path = Path & "\"
    
    For i = Len(fn) To 1 Step -1
        If Mid$(fn, i, 1) = "\" Then
            Path = Left$(fn, i)
            fn = Right$(fn, Len(fn) - i)
            Exit For
        End If
    Next i
    
    Call Search(Path, fn, expression)
    
End Sub

Sub AddComboSelection(cbo As ComboBox, newItem As String)
    Dim i As Integer
    
    ' If this path is already in the dropdown, remove it.
    For i = 0 To cbo.ListCount - 1
        If cbo.List(i) = newItem Then
            cbo.RemoveItem i
            Exit For
        End If
    Next i
    
    ' Add this path as the first item in the dropdown.
    cbo.AddItem newItem, 0
    
    ' The combo text gets cleared by the AddItem.
    cbo.Text = newItem
    
    ' Only store as many paths as has been set in options.
    If cbo.ListCount > NUM_STORED_PATHS Then
        ' Remove the last item in the list.
        cbo.RemoveItem cbo.ListCount - 1
    End If
    
End Sub
Private Sub chkFileNamesOnly_Click()
    If chkFileNamesOnly.Value Then
        chkLineNumbers.Enabled = False
        lblContextLines.Enabled = False
        lblContextLines.Enabled = False
        UpDown1.Enabled = False
    Else
        chkLineNumbers.Enabled = True
        lblContextLines.Enabled = True
        lblContextLines.Enabled = True
        UpDown1.Enabled = True
    End If
End Sub

Private Sub Form_Load()

    '***************
    'Define globals.
    '***************
    
    GB_CANCEL = 0   ' Global variable used to cancel a search.
    
    cboFileName.ListIndex = 0
    
    fraMain.Top = 120
    fraMain.Left = 120
    fraMain.Visible = True
    
    fraSearching.Top = 120
    fraSearching.Left = 120
    fraSearching.Visible = False

    LoadRegistrySettings
    
    ' For some reason these controls were hiding under eachother on some systems.
    lblContextLines.Left = UpDown1.Left + UpDown1.Width + 10
    
End Sub

Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
    If UnloadMode = 0 Then
        UpdateRegistrySettings
    End If
End Sub

Private Sub Form_Resize()
    On Error Resume Next
    
    ' Resize the splitter window.
    If Me.Width < 3000 Then Me.Width = 3000
    SizeControls imgSplitter.Left

    ' Resize controls on main window.
    fraMain.Width = Me.Width - 100
    cboFilePath.Width = fraMain.Width - cboFileName.Left - 300
    cboFileName.Width = fraMain.Width - cboFileName.Left - btnSearch.Width - 400
    cboSearchForText.Width = fraMain.Width - cboFileName.Left - btnExit.Width - 400
    btnSearch.Left = fraMain.Width - btnSearch.Width - 300
    btnExit.Left = fraMain.Width - btnExit.Width - 300
    
    ' Resize controls on window that displays during search.
    fraSearching.Width = Me.Width - 100
    lblSearchDirectory.Width = fraSearching.Width - lblSearchDirectory.Left - 300
    lblSearchFile.Width = fraSearching.Width - lblSearchFile.Left - 300
End Sub

Private Sub lstFileNames_Click()
    Dim ho As CHitObject
    Set ho = G_HITS.Item(lstFileNames.ListIndex + 1)
    frmMain.txtHits.TextRTF = ho.GetHitString()
    frmMain.sbStatusBar.Panels(1).Text = ho.Path
    
    Set ho = Nothing
End Sub

Private Sub lstFileNames_DblClick()
    Dim FileName As String

    FileName = G_HITS.Item(lstFileNames.ListIndex + 1).Path + _
        G_HITS.Item(lstFileNames.ListIndex + 1).FileName
    
    ' Open the default editor.
    EditFile FileName, 1
End Sub

Private Sub mEditAll_Click()
    Dim i As Long
    Dim FileName As String

    For i = 0 To lstFileNames.ListCount - 1
        If lstFileNames.Selected(i) Then
            FileName = G_HITS.Item(i + 1).Path + G_HITS.Item(i + 1).FileName
    
            ' Open the default editor.
            EditFile FileName, 1
        End If
    Next i
        
End Sub

Private Sub mnuAbout_Click()
    frmAbout.Show 1
End Sub

Private Sub imgSplitter_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)
    With imgSplitter
        picSplitter.Move .Left, .Top, .Width \ 2, .Height - 20
    End With
    picSplitter.Visible = True
    mbMoving = True
End Sub

Private Sub imgSplitter_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
    Dim sglPos As Single

    If mbMoving Then
        sglPos = X + imgSplitter.Left
        If sglPos < SGLSPLITLIMIT Then
            picSplitter.Left = SGLSPLITLIMIT
        ElseIf sglPos > Me.Width - SGLSPLITLIMIT Then
            picSplitter.Left = Me.Width - SGLSPLITLIMIT
        Else
            picSplitter.Left = sglPos
        End If
    End If
End Sub

Private Sub imgSplitter_MouseUp(Button As Integer, Shift As Integer, X As Single, Y As Single)
    SizeControls picSplitter.Left
    picSplitter.Visible = False
    mbMoving = False
End Sub

Sub SizeControls(X As Single)
    On Error Resume Next
    Const SPLITTERSIZE = 40
    Dim statusBarheight As Integer

    If sbStatusBar.Visible = True Then
        sbStatusBar.Panels.Item(1).Width = frmMain.Width - 500
        statusBarheight = sbStatusBar.Height
    Else
        statusBarheight = 0
    End If
    
    'set the width
    If X < 1500 Then X = 1500
    If X > (Me.Width - 1500) Then X = Me.Width - 1500
    lstFileNames.Left = GBORDER_SIZE
    lstFileNames.Width = X - GBORDER_SIZE ' + 15
    
    imgSplitter.Left = X
    
    txtHits.Left = lstFileNames.Width + GBORDER_SIZE + imgSplitter.Width
     
    txtHits.Width = Me.ScaleWidth - (txtHits.Left + GBORDER_SIZE)
    
    'set the top
    lstFileNames.Top = fraMain.Height + GBORDER_SIZE * 2
    txtHits.Top = lstFileNames.Top
   
    'set the height
    lstFileNames.Height = Me.ScaleHeight - fraMain.Height - GBORDER_SIZE * 3 - statusBarheight

    txtHits.Height = lstFileNames.Height
    imgSplitter.Top = lstFileNames.Top
    imgSplitter.Height = lstFileNames.Height
End Sub

Private Sub mnuClearMRU_Click()
    ClearRegistrySettings
End Sub

Private Sub mnuOptions_Click()
    frmProperties.Show vbModal
End Sub

Private Sub mnuPrintAllHits_Click()
    PrintSelectedItems Me.lstFileNames, G_HITS
End Sub

Private Sub mnuPrintCurrent_Click()
    If Me.lstFileNames.ListIndex = -1 Then
        MsgBox "No item selected", vbCritical + vbOKOnly, "Error"
    Else
        PrintSingleItem G_HITS(Me.lstFileNames.ListIndex + 1)
    End If
End Sub

Private Sub mnuPrinterSetup_Click()
    PrinterSetup
End Sub

Private Sub mnuPrintFileList_Click()
    PrintFileList G_HITS
End Sub

Private Sub mnuSelectAll_Click()
    Dim i As Integer
    Dim sel As Integer

    sel = Me.lstFileNames.ListIndex
    
    For i = 0 To Me.lstFileNames.ListCount - 1
        Me.lstFileNames.Selected(i) = True
    Next i
    
    Me.lstFileNames.ListIndex = sel
End Sub

Private Sub txtHits_DblClick()
    Dim i As Long
    Dim FileName As String
    Dim LineNum As Long
    
    ' Make sure there is something to click on.
    If G_HITS.Count = 0 Then Exit Sub
    
    ' Find out the line number the cursor is on.
    i = txtHits.GetLineFromChar(txtHits.SelStart) + 1
    
    ' Use the cursor's linenumber to get the hit's line number.
    LineNum = G_HITS.Item(lstFileNames.ListIndex + 1).GetHitLineNumber(i)
    
    FileName = G_HITS.Item(lstFileNames.ListIndex + 1).Path + _
        G_HITS.Item(lstFileNames.ListIndex + 1).FileName
    
    ' Open the default editor.
    EditFile FileName, LineNum
    
End Sub

