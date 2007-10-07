''' -----------------------------------------------------------------------------
''' Project	 : AstroGrep
''' Class	 : CFileFinder
''' 
''' -----------------------------------------------------------------------------
''' <summary>
'''   File Finder Class
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
Friend Class CFileFinder

#Region "Declarations"
   Dim __fileList() As String           ' This will be an array of valid file names.
   Dim __fileCount As Integer
   Dim __curFileIndex As Integer
   Dim __curFilePath As String
   Dim __curDirectoryPath As String
#End Region

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Find the first instance of a single filename in the given path
   ''' </summary>
   ''' <param name="Path">path to find a file in</param>
   ''' <returns>filename, empty string if not found</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function FindFirstFile(ByRef Path As String) As String

      ' Store the new path name so we can use it in the "next" call.
      __curFilePath = Path

      ' Reset to the beginning of our file list.
      __curFileIndex = 0

      ' Loop through all files until we find a match, or are done.
      Do
         __curFileIndex += 1
         FindFirstFile = Dir(__curFilePath & __fileList(__curFileIndex), FileAttribute.Normal)
      Loop While FindFirstFile = "" And __curFileIndex < __fileCount

   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Find the next file
   ''' </summary>
   ''' <returns>next file</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function FindNextFile() As String

      ' If we have already searched through all files in this path,
      ' or a FindFirst has never been performed, return empty.
      If __curFileIndex > __fileCount Or __curFileIndex < 0 Then
         FindNextFile = ""
         Exit Function
      End If

      ' Get the next file of the same name as the previous file (for wildcards).
      FindNextFile = Dir()

      ' If no file found, go through the list of filenames.
      Do While FindNextFile = "" And __curFileIndex < __fileCount
         __curFileIndex += 1
         FindNextFile = Dir(__curFilePath & __fileList(__curFileIndex), FileAttribute.Normal)
      Loop

   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Add a file name
   ''' </summary>
   ''' <param name="NewFile">file name (possible to be a list)</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub AddFileName(ByRef NewFile As String)
      Dim i As Short

      NewFile = Trim(NewFile)

      If NewFile = "" Then Exit Sub

      ' If there is a comma in the string, this must be a list of filenames.
      If InStr(NewFile, ",") > 0 Then
         AddFileNameList(NewFile)
      Else
         ' Bump up the array size if neccessary.
         If __fileCount >= UBound(__fileList) Then
            ReDim Preserve __fileList(__fileCount + 10)
         End If

         __fileCount += 1
         __fileList(__fileCount) = NewFile
      End If

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   This takes a string of comma separatee
   ''' </summary>
   ''' <param name="NewFiles">Comma separated list of files</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub AddFileNameList(ByRef NewFiles As String)

      Dim done As Boolean
      Dim FileName As String
      Dim fileNameLen As Integer
      Dim cPos1, cPos2 As Integer

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
         FileName = Mid(NewFiles, cPos1, fileNameLen)

         ' Add the filename to our list of filenames.
         AddFileName(FileName)

      Loop While Not done

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   A directory is valid if, it is of directory type and isn't the
   '''   current, or previous directory ("." and "..")
   ''' </summary>
   ''' <param name="Path">path</param>
   ''' <param name="DirectoryName">directory name to check</param>
   ''' <returns></returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function IsDirectory(ByRef Path As String, ByRef DirectoryName As String) As Boolean

      On Error GoTo errHandler

      IsDirectory = False

      ' Make sure it isn't one of the "wrong" directory types.
      If DirectoryName <> "." And DirectoryName <> ".." Then

         ' Do the bitwise compare.
         'If (GetAttr(Path & DirectoryName) And FileAttribute.Directory) Then
         If (GetAttr(Path & DirectoryName) = FileAttribute.Directory) Then
            IsDirectory = True
         End If

      End If
      Exit Function

errHandler:

   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Find the first directory
   ''' </summary>
   ''' <param name="Path">path to start</param>
   ''' <returns>first directory from given path</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function FindFirstDirectory(ByRef Path As String) As String

      ' Store the directory for use in the "next" call.
      __curDirectoryPath = Path

      ' Get the first filename in this path.
      FindFirstDirectory = Dir(Path, FileAttribute.Directory)

      ' If the first file we encountered doesn't work,
      ' let "next" do all the work.
      If Not IsDirectory(Path, FindFirstDirectory) Then
         FindFirstDirectory = FindNextDirectory()
      End If

   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Find the next directory
   ''' </summary>
   ''' <returns>next directory</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function FindNextDirectory() As String

      On Error GoTo errHandler

      Dim found As Boolean

      Do
         ' Get the next directory entry.
         FindNextDirectory = Dir()
         found = IsDirectory(__curDirectoryPath, FindNextDirectory)
      Loop While FindNextDirectory <> "" And Not found
      Exit Function

errHandler:
      FindNextDirectory = ""
   End Function

#Region "Class Level"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Initialize class level variables
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub Initialize()
      ReDim __fileList(10)
      __fileCount = 0
      __curFileIndex = -1
      __curFilePath = String.Empty
      __curDirectoryPath = String.Empty
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Create a new instance of the class
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub New()
      MyBase.New()

      Initialize()
   End Sub
#End Region

End Class