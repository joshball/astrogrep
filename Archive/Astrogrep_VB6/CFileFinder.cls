VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "CFileFinder"
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

Option Explicit

' This will be an array of valid file names.
Dim mFileList() As String
Dim mFileCount As Integer
Dim mCurFileIndex As Integer
Dim mCurFilePath As String

Dim mCurDirectoryPath As String

' Find the first instance of a single filename in the given path.
Public Function FindFirstFile(Path As String) As String
    
    ' Store the new path name so we can use it in the "next" call.
    mCurFilePath = Path
    
    ' Reset to the beginning of our file list.
    mCurFileIndex = 0

    ' Loop through all files until we find a match, or are done.
    Do
        mCurFileIndex = mCurFileIndex + 1
        FindFirstFile = Dir(mCurFilePath & mFileList(mCurFileIndex), vbNormal)
    Loop While FindFirstFile = "" And mCurFileIndex < mFileCount

End Function

Function FindNextFile() As String
    
    ' If we have already searched through all files in this path,
    ' or a FindFirst has never been performed, return empty.
    If mCurFileIndex > mFileCount Or mCurFileIndex < 0 Then
        FindNextFile = ""
        Exit Function
    End If
    
    ' Get the next file of the same name as the previous file (for wildcards).
    FindNextFile = Dir
    
    ' If no file found, go through the list of filenames.
    Do While FindNextFile = "" And mCurFileIndex < mFileCount
        mCurFileIndex = mCurFileIndex + 1
        FindNextFile = Dir(mCurFilePath & mFileList(mCurFileIndex), vbNormal)
    Loop
    
End Function

Sub AddFileName(NewFile As String)
    Dim i As Integer

    NewFile = Trim(NewFile)
    
    If NewFile = "" Then Exit Sub
    
    ' If there is a comma in the string, this must be a list of filenames.
    If InStr(NewFile, ",") Then
        AddFileNameList NewFile
    Else
        ' Bump up the array size if neccessary.
        If mFileCount >= UBound(mFileList) Then
            ReDim Preserve mFileList(mFileCount + 10) As String
        End If
        
        mFileCount = mFileCount + 1
        mFileList(mFileCount) = NewFile
    End If
    
End Sub

' This takes a string of comma separatee
Sub AddFileNameList(NewFiles As String)
    
    Dim done As Boolean
    Dim FileName As String
    Dim fileNameLen As Integer
    Dim cPos1 As Integer, cPos2 As Integer
    
    done = False
    cPos2 = 0

    Do
        ' Move past the previously found comma.
        cPos1 = cPos2 + 1
        
        ' Find the position of the next comma.
        cPos2 = InStr(cPos1, NewFiles, ",")

        ' When we get a zero, it means no more commas.
        If cPos2 = 0 Then
        
            ' This is the last filename, so set the flag.
            done = True
            
            ' Set cpos2 so that the correct length will be
            ' calculated for the last filename in the string.
            ' (the + 1 offsets the missing comma at the end)
            cPos2 = Len(NewFiles) + 1
            
        End If

        ' Calculate the length between the previous comma and the next comma.
        fileNameLen = cPos2 - cPos1

        ' Get the filename from between the commas.
        FileName = Mid$(NewFiles, cPos1, fileNameLen)
        
        ' Add the filename to our list of filenames.
        AddFileName FileName
        
    Loop While Not done

End Sub

'***************************************************************
' A directory is valid if, it is of directory type and isn't the
' current, or previous directory ("." and "..")
'***************************************************************

Public Function IsDirectory(Path As String, DirectoryName As String) As Boolean

    On Error GoTo errHandler
    
    IsDirectory = False
    
    ' Make sure it isn't one of the "wrong" directory types.
    If DirectoryName <> "." And DirectoryName <> ".." Then

        ' Do the bitwise compare.
        If (GetAttr(Path & DirectoryName) And vbDirectory) Then
            IsDirectory = True
        End If

    End If
    Exit Function

errHandler:

End Function

Public Function FindFirstDirectory(Path As String) As String
    
    ' Store the directory for use in the "next" call.
    mCurDirectoryPath = Path

    ' Get the first filename in this path.
    FindFirstDirectory = Dir(Path, vbDirectory)
    
    ' If the first file we encountered doesn't work,
    ' let "next" do all the work.
    If Not IsDirectory(Path, FindFirstDirectory) Then
        FindFirstDirectory = FindNextDirectory()
    End If
    
End Function

Public Function FindNextDirectory() As String
    
    On Error GoTo errHandler

    Dim found As Boolean
    
    Do
        ' Get the next directory entry.
        FindNextDirectory = Dir
        found = IsDirectory(mCurDirectoryPath, FindNextDirectory)
    Loop While FindNextDirectory <> "" And Not found
    Exit Function
    
errHandler:
    FindNextDirectory = ""
End Function

Private Sub Class_Initialize()
    ReDim mFileList(10)
    mFileCount = 0
    mCurFileIndex = -1
    mCurFilePath = ""
    mCurDirectoryPath = ""

End Sub
