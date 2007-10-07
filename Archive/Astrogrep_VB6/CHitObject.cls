VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "CHitObject"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "SavedWithClassBuilder" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
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

Private mFileName As String
Private mPath As String
Private mHitList() As String
Private mHitLineNumberList() As Long
Private mHitListIndex As Long

Public Property Let Path(vData As String)
    mPath = vData
End Property

Public Property Get Path() As String
    Path = mPath
End Property

Public Property Let FileName(ByVal vData As String)
    mFileName = vData
End Property

Public Property Get FileName() As String
    FileName = mFileName
End Property

Public Sub Initialize(Path As String, FileName As String)
    mPath = Path
    mFileName = FileName
End Sub

Public Function GetHitString() As String
    Dim i As Integer
    GetHitString = ""
    For i = 1 To mHitListIndex
        GetHitString = GetHitString + mHitList(i)
    Next i
End Function

Public Function GetNumHits() As Long
    GetNumHits = mHitListIndex
End Function

Public Function GetHitLineNumber(Index As Long) As Long
    GetHitLineNumber = 0
    If Index > mHitListIndex Then Exit Function
    GetHitLineNumber = mHitLineNumberList(Index)
End Function

Public Function GetHit(Index As Long) As String
    GetHit = ""
    If Index > mHitListIndex Then Exit Function
    GetHit = mHitList(Index)
End Function

Public Sub AddHit(LineNum As Long, Line As String)
    ' Ensure we have enough space in our array to store this hit.
    adjustHitArraySize
    
    ' Point the index to the next hit.
    mHitListIndex = mHitListIndex + 1
    
    ' Store the hit and it's line number.
    mHitList(mHitListIndex) = Line
    mHitLineNumberList(mHitListIndex) = LineNum
    
End Sub

Private Sub adjustHitArraySize()
    Dim listSize As Integer
    
    listSize = UBound(mHitList)
    
    ' If our index is already at the end of our hitlist,
    ' increase the size of the hitlist.
    If mHitListIndex = listSize Then
        ReDim Preserve mHitList(listSize + 10)
        ReDim Preserve mHitLineNumberList(listSize + 10)
    End If

End Sub

Private Sub Class_Initialize()
    ReDim mHitList(10)
    ReDim mHitLineNumberList(10)
    
    mHitListIndex = 0
End Sub
