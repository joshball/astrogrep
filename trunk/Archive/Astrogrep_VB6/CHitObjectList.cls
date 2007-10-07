VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "CHitObjectCollection"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "SavedWithClassBuilder" ,"Yes"
Attribute VB_Ext_KEY = "Collection" ,"CHitObject"
Attribute VB_Ext_KEY = "Member0" ,"CHitObject"
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

'local variable to hold collection
Private mCol As Collection

Public Function Add(Path As String, FileName As String) As CHitObject
    On Error GoTo finished
        
    'create a new object
    Dim objNewMember As CHitObject
    Set objNewMember = New CHitObject
    
    objNewMember.Initialize Path, FileName
    mCol.Add objNewMember, Path & FileName

    'return the object created
    Set Add = objNewMember
    Set objNewMember = Nothing
    Exit Function
finished:
    ' This may sometimes happen if we search the same file twice. This may happen because
    ' The Dir command which finds files based on things like "*.html" will sometimes
    ' treat *.htm and *.html the same, therefore finding the same file twice.
    Set Add = Nothing
End Function

Public Function AddObject(ho As CHitObject) As CHitObject
    
    If Not ho Is Nothing Then
        mCol.Add ho, ho.Path & ho.FileName
    End If
    
    Set AddObject = ho
    
End Function

Public Property Get Item(vntIndexKey As Variant) As CHitObject
Attribute Item.VB_UserMemId = 0
  On Error GoTo returnnothing
  Set Item = mCol(vntIndexKey)
  Exit Property
returnnothing:
  Set Item = Nothing
End Property

Public Property Get Count() As Long
    'used when retrieving the number of elements in the
    'collection. Syntax: Debug.Print x.Count
    Count = mCol.Count
End Property

Public Sub Remove(vntIndexKey As Variant)
    'used when removing an element from the collection
    'vntIndexKey contains either the Index or Key, which is why
    'it is declared as a Variant
    'Syntax: x.Remove(xyz)

    mCol.Remove vntIndexKey
End Sub

Public Property Get NewEnum() As IUnknown
Attribute NewEnum.VB_UserMemId = -4
Attribute NewEnum.VB_MemberFlags = "40"
    'this property allows you to enumerate
    'this collection with the For...Each syntax
    Set NewEnum = mCol.[_NewEnum]
End Property

Private Sub Class_Initialize()
    'creates the collection when this class is created
    Set mCol = New Collection
End Sub

Private Sub Class_Terminate()
    'destroys collection when this class is terminated
    Set mCol = Nothing
End Sub

