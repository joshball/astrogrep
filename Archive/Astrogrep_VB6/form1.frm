VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.1#0"; "RICHTX32.OCX"
Object = "{FE0065C0-1B7B-11CF-9D53-00AA003C9CB6}#1.0#0"; "COMCT232.OCX"
Object = "{6B7E6392-850A-101B-AFC0-4210102A8DA7}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form frmMain 
   Caption         =   "AstroGrep"
   ClientHeight    =   5475
   ClientLeft      =   165
   ClientTop       =   735
   ClientWidth     =   5745
   Icon            =   "Form1.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   5475
   ScaleWidth      =   5745
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame fraResults 
      BorderStyle     =   0  'None
      Caption         =   "Frame1"
      Height          =   255
      Left            =   120
      TabIndex        =   23
      Top             =   1800
      Width           =   1935
      Begin VB.Label lblResults 
         Appearance      =   0  'Flat
         BackColor       =   &H80000004&
         Caption         =   "Results:"
         ForeColor       =   &H80000008&
         Height          =   255
         Left            =   0
         TabIndex        =   24
         Top             =   0
         Width           =   1575
      End
   End
   Begin VB.Frame fraSearching 
      BorderStyle     =   0  'None
      Caption         =   "Frame1"
      Height          =   1695
      Left            =   120
      TabIndex        =   17
      Top             =   3000
      Width           =   5535
      Begin VB.CommandButton btnCancel 
         Caption         =   "&Cancel"
         Height          =   315
         Left            =   4560
         TabIndex        =   11
         Top             =   1080
         Width           =   915
      End
      Begin VB.Label Label6 
         Caption         =   "Searching..."
         Height          =   255
         Left            =   0
         TabIndex        =   22
         Top             =   0
         Width           =   1935
      End
      Begin VB.Label lblSearchFile 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BorderStyle     =   1  'Fixed Single
         ForeColor       =   &H80000008&
         Height          =   285
         Left            =   480
         TabIndex        =   21
         Top             =   720
         Width           =   4935
      End
      Begin VB.Label Label5 
         AutoSize        =   -1  'True
         Caption         =   "Path:"
         Height          =   195
         Left            =   0
         TabIndex        =   20
         Top             =   360
         Width           =   375
      End
      Begin VB.Label lblSearchDirectory 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BorderStyle     =   1  'Fixed Single
         ForeColor       =   &H80000008&
         Height          =   285
         Left            =   480
         TabIndex        =   19
         Top             =   360
         Width           =   4935
      End
      Begin VB.Label Label2 
         AutoSize        =   -1  'True
         Caption         =   "File:"
         Height          =   195
         Left            =   0
         TabIndex        =   18
         Top             =   720
         Width           =   285
      End
   End
   Begin VB.Frame fraMain 
      BorderStyle     =   0  'None
      Caption         =   "Frame1"
      Height          =   1815
      Left            =   240
      TabIndex        =   13
      Top             =   0
      Width           =   5535
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
         TabIndex        =   9
         Top             =   360
         Width           =   915
      End
      Begin VB.CommandButton btnExit 
         Appearance      =   0  'Flat
         Caption         =   "E&xit"
         Height          =   315
         Left            =   4560
         TabIndex        =   10
         Top             =   720
         Width           =   915
      End
      Begin VB.TextBox txtSearchForText 
         Height          =   315
         Left            =   960
         TabIndex        =   3
         Top             =   720
         Width           =   3550
      End
      Begin VB.CheckBox chkFileNamesOnly 
         Appearance      =   0  'Flat
         BackColor       =   &H80000004&
         Caption         =   "Show file names only"
         ForeColor       =   &H80000008&
         Height          =   195
         Left            =   2160
         TabIndex        =   6
         Top             =   1140
         Width           =   1935
      End
      Begin VB.CheckBox chkCaseSensitive 
         Appearance      =   0  'Flat
         BackColor       =   &H80000004&
         Caption         =   "Case sensitive"
         ForeColor       =   &H80000008&
         Height          =   195
         Left            =   0
         TabIndex        =   4
         Top             =   1140
         Width           =   1455
      End
      Begin VB.CheckBox chkRecurse 
         Appearance      =   0  'Flat
         BackColor       =   &H80000004&
         Caption         =   "Recurse subdirectories"
         ForeColor       =   &H80000008&
         Height          =   195
         Left            =   0
         TabIndex        =   5
         Top             =   1440
         Value           =   1  'Checked
         Width           =   1935
      End
      Begin VB.CheckBox chkLineNumbers 
         Appearance      =   0  'Flat
         BackColor       =   &H80000004&
         Caption         =   "Line numbers"
         ForeColor       =   &H80000008&
         Height          =   195
         Left            =   2160
         TabIndex        =   7
         Top             =   1440
         Value           =   1  'Checked
         Width           =   1455
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
         ItemData        =   "Form1.frx":030A
         Left            =   960
         List            =   "Form1.frx":032C
         TabIndex        =   2
         Top             =   360
         Width           =   3550
      End
      Begin ComCtl2.UpDown UpDown1 
         Height          =   255
         Left            =   4100
         TabIndex        =   8
         Top             =   1140
         Width           =   195
         _ExtentX        =   344
         _ExtentY        =   450
         _Version        =   327680
         Alignment       =   0
         BuddyControl    =   "lblContextLines"
         BuddyDispid     =   196630
         OrigLeft        =   4080
         OrigTop         =   1125
         OrigRight       =   4275
         OrigBottom      =   1410
         Max             =   5
         SyncBuddy       =   -1  'True
         BuddyProperty   =   65537
         Enabled         =   -1  'True
      End
      Begin VB.Label lblContextLines 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BorderStyle     =   1  'Fixed Single
         Caption         =   "0"
         ForeColor       =   &H80000008&
         Height          =   255
         Left            =   4300
         TabIndex        =   28
         Top             =   1140
         Width           =   150
      End
      Begin VB.Label Label3 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000004&
         Caption         =   "Search Text:"
         ForeColor       =   &H80000008&
         Height          =   195
         Left            =   0
         TabIndex        =   16
         Top             =   765
         Width           =   915
      End
      Begin VB.Label lblContext 
         Appearance      =   0  'Flat
         BackColor       =   &H80000004&
         Caption         =   "Context lines"
         ForeColor       =   &H80000008&
         Height          =   255
         Left            =   4540
         TabIndex        =   15
         Top             =   1170
         Width           =   975
      End
      Begin VB.Label Label4 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000004&
         Caption         =   "File Name(s):"
         ForeColor       =   &H80000008&
         Height          =   195
         Left            =   0
         TabIndex        =   14
         Top             =   420
         Width           =   915
      End
   End
   Begin ComctlLib.StatusBar sbStatusBar 
      Align           =   2  'Align Bottom
      Height          =   375
      Left            =   0
      TabIndex        =   27
      Top             =   5100
      Width           =   5745
      _ExtentX        =   10134
      _ExtentY        =   661
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   1
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
         EndProperty
      EndProperty
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
      TabIndex        =   26
      Top             =   480
      Visible         =   0   'False
      Width           =   72
   End
   Begin VB.ListBox lstFileNames 
      Appearance      =   0  'Flat
      Height          =   1740
      IntegralHeight  =   0   'False
      Left            =   120
      TabIndex        =   25
      Top             =   2160
      Width           =   1935
   End
   Begin RichTextLib.RichTextBox txtHits 
      Height          =   3135
      Left            =   2280
      TabIndex        =   12
      Top             =   2160
      Width           =   2115
      _ExtentX        =   3731
      _ExtentY        =   5530
      _Version        =   327680
      ReadOnly        =   -1  'True
      ScrollBars      =   3
      Appearance      =   0
      TextRTF         =   $"Form1.frx":03AB
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
   Begin VB.Image imgSplitter 
      Appearance      =   0  'Flat
      BorderStyle     =   1  'Fixed Single
      Height          =   4785
      Left            =   2040
      MousePointer    =   9  'Size W E
      Top             =   720
      Width           =   80
   End
   Begin VB.Menu mnuTools 
      Caption         =   "&Tools"
      Begin VB.Menu mnuOptions 
         Caption         =   "&Options..."
      End
      Begin VB.Menu seperator1 
         Caption         =   "-"
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
    If Me.lblContextLines.Caption < 0 Or Me.lblContextLines.Caption > 5 Then
contexterror:
        MsgBox "Number of context lines must be between 0 and 5", vbOKOnly + vbCritical, "Error"
        Exit Function
    End If
    VerifyInterface = True
End Function
Private Sub btnSearch_Click()
    Dim Path As String
    Dim fn As String
    Dim i As Integer
    
    If VerifyInterface = False Then Exit Sub
    
    fn = Me.cboFileName.Text
    Path = Trim(cboFilePath.Text)
    
    AddPathComboSelection Path

    ' Ensure that there is a backslash.
    If Right$(Path, 1) <> "\" Then Path = Path & "\"
    
    For i = Len(fn) To 1 Step -1
        If Mid$(fn, i, 1) = "\" Then
            Path = Left$(fn, i)
            fn = Right$(fn, Len(fn) - i)
            Exit For
        End If
    Next i
    
    Call Search(Path, fn, Me.txtSearchForText)
    
End Sub

Sub AddPathComboSelection(Path As String)
    Dim i As Integer
    
    With frmMain.cboFilePath
    
        ' If this path is already in the dropdown, remove it.
        For i = 0 To .ListCount - 1
            If .List(i) = Path Then
                .RemoveItem i
                Exit For
            End If
        Next i
        
        ' Add this path as the first item in the dropdown.
        .AddItem Path, 0
        
        ' The combo text gets cleared by the AddItem.
        .Text = Path
        
        ' Only store as many paths as has been set in options.
        If .ListCount > NUM_STORED_PATHS Then
            ' Remove the last item in the list.
            .RemoveItem .ListCount - 1
        End If
    End With
    
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
    
End Sub

Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
    If UnloadMode = 0 Then
        UpdateRegistrySettings
    End If
End Sub

Private Sub Form_Resize()
    On Error Resume Next
    If Me.Width < 3000 Then Me.Width = 3000
    SizeControls imgSplitter.Left

Exit Sub

    Dim xWidth As Single, xHeight As Single
    
    xWidth = Me.ScaleWidth - txtHits.Left * 2
    If xWidth < 0 Then xWidth = 0
    txtHits.Width = xWidth
    
    xHeight = Me.ScaleHeight - txtHits.Top
    xHeight = xHeight
    xHeight = xHeight - txtHits.Left
    If xHeight < 0 Then xHeight = 0
    txtHits.Height = xHeight
    
End Sub

Private Sub lstFileNames_Click()
    Dim ho As CHitObject
    Set ho = G_HITS.Item(lstFileNames.ListIndex + 1)
    frmMain.txtHits.Text = ho.GetHitString()
    frmMain.sbStatusBar.Panels(1).Text = ho.Path
    
    Set ho = Nothing
End Sub

Private Sub txtFilePath_Change()

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


Private Sub txtHits_DblClick()
    Dim i As Long
    Dim FileName As String
    Dim LineNum As Long
    
    ' Find out the line number the cursor is on.
    i = txtHits.GetLineFromChar(txtHits.SelStart) + 1
    
    ' Use the cursor's linenumber to get the hit's line number.
    LineNum = G_HITS.Item(lstFileNames.ListIndex + 1).GetHitLineNumber(i)
    
    FileName = G_HITS.Item(lstFileNames.ListIndex + 1).Path + _
        G_HITS.Item(lstFileNames.ListIndex + 1).FileName
    
    ' Open the default editor.
    EditFile FileName, LineNum
    
End Sub
