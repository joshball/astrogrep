VERSION 5.00
Begin VB.Form frmBrowse 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Search In"
   ClientHeight    =   2640
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   3075
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2640
   ScaleWidth      =   3075
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin VB.DriveListBox Drive1 
      Height          =   315
      Left            =   120
      TabIndex        =   3
      Top             =   120
      Width           =   2835
   End
   Begin VB.CommandButton Command2 
      Cancel          =   -1  'True
      Caption         =   "&Cancel"
      Height          =   375
      Left            =   1200
      TabIndex        =   2
      Top             =   2100
      Width           =   855
   End
   Begin VB.CommandButton Command1 
      Caption         =   "&Ok"
      Default         =   -1  'True
      Height          =   375
      Left            =   2160
      TabIndex        =   1
      Top             =   2100
      Width           =   855
   End
   Begin VB.DirListBox Dir1 
      Height          =   1440
      Left            =   120
      TabIndex        =   0
      Top             =   540
      Width           =   2835
   End
End
Attribute VB_Name = "frmBrowse"
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

Public mCancel As Boolean

Private Sub Command1_Click()
    mCancel = False
    Me.Hide
End Sub

Private Sub Command2_Click()
    mCancel = True
    Me.Hide
End Sub

Private Sub Drive1_Change()
    Me.Dir1.Path = Drive1.Drive
End Sub

Private Sub Form_Load()
    Me.Top = Screen.Height / 2 - Me.Height / 2
    Me.Left = Screen.Width / 2 - Me.Width / 2
    
    mCancel = True
End Sub
