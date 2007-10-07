VERSION 5.00
Begin VB.Form frmCancel 
   Caption         =   "Searching..."
   ClientHeight    =   1620
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7260
   ControlBox      =   0   'False
   LinkTopic       =   "Form2"
   MousePointer    =   1  'Arrow
   ScaleHeight     =   1620
   ScaleWidth      =   7260
   Begin VB.CommandButton btnCancel 
      Caption         =   "&Cancel"
      Height          =   495
      Left            =   3120
      TabIndex        =   0
      Top             =   960
      Width           =   975
   End
   Begin VB.Label Label3 
      AutoSize        =   -1  'True
      Caption         =   "File:"
      Height          =   195
      Left            =   120
      TabIndex        =   4
      Top             =   480
      Width           =   285
   End
   Begin VB.Label lblDirectory 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BorderStyle     =   1  'Fixed Single
      ForeColor       =   &H80000008&
      Height          =   255
      Left            =   600
      TabIndex        =   3
      Top             =   120
      Width           =   6495
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      Caption         =   "Path:"
      Height          =   195
      Left            =   120
      TabIndex        =   2
      Top             =   120
      Width           =   375
   End
   Begin VB.Label lblSearch 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BorderStyle     =   1  'Fixed Single
      ForeColor       =   &H80000008&
      Height          =   255
      Left            =   600
      TabIndex        =   1
      Top             =   480
      Width           =   6495
   End
End
Attribute VB_Name = "frmCancel"
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

Private Sub btnCancel_Click()
    GB_CANCEL = True
End Sub

