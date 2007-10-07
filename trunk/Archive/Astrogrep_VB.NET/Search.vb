Imports System.Text.RegularExpressions

''' -----------------------------------------------------------------------------
''' <summary>
'''   Search Routines
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
Module SearchCode

#Region "Declarations"
   Public GB_CANCEL As Boolean
   Public GB_NUMHITS As Integer
   Public G_HITS As New CHitObjectCollection

   Public Structure searchOptions
      Dim recursiveSearch As Boolean
      Dim useRegularExpressions As Boolean
      Dim caseSensistiveMatch As Boolean
      Dim wholeWordMatch As Boolean
      Dim negation As Boolean
      Dim onlyFileNames As Boolean
      Dim includeLineNumbers As Boolean
      Dim contextLines As Integer
   End Structure
#End Region

#Region "Public Methods"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Begin Search
   ''' </summary>
   ''' <param name="path">Path of file</param>
   ''' <param name="fileName">File name</param>
   ''' <param name="searchText">Text to Search for</param>
   ''' <param name="recurse">Use recursion</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion/Remove gui dependency
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub Search(ByVal path As String, ByVal fileName As String, _
                     ByVal searchText As String, _
                     ByVal options As searchOptions)

      Dim _fileFinder As New CFileFinder

      SetSearch(True)

      ' Reset the hits collection.
      G_HITS = New CHitObjectCollection
      ClearHits()

      ' Add all desired file names to the file finder object.
      _fileFinder.AddFileName(fileName)

      ' Kick off the search.
      SearchDirectory(path, _fileFinder, searchText, options)

      'Resets the search
      SetSearch(False)

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Enable or disable the searching window
   ''' </summary>
   ''' <param name="enableSearch"></param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub SetSearch(ByVal enableSearch As Boolean)
      GB_CANCEL = Not enableSearch
   End Sub
#End Region

#Region "Private Methods"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Search Method
   ''' </summary>
   ''' <param name="path">Path to search</param>
   ''' <param name="fileFinder">File Finder Object</param>
   ''' <param name="searchText">Text to search for</param>
   ''' <param name="options">Search Options</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion, use .Net regular expressions
   ''' 	[Curtis_Beard]	   01/28/2005	ADD: Negation option
   '''   [Curtis_Beard]    02/06/2005  ADD: Use passed in options instead of gui
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub SearchDirectory(ByRef path As String, ByRef fileFinder As CFileFinder, _
                     ByRef searchText As String, _
                     ByVal options As searchOptions)

      Dim file As Integer
      Dim i As Integer
      Dim textLine As String
      Dim FileNameDisplayed As Boolean
      Dim FileName As String
      Dim Tempstr As String
      Dim LineNum As Integer

      Dim Context(10) As String
      Dim contextIndex As Integer
      Dim lastHit As Integer
      Dim numContextLines As Integer
      Dim contextSpacer As String
      Dim spacer As String
      Dim ho As CHitObject
      Dim lcSearchText As String
      Dim temp As Integer
      Dim _regularExp As Regex
      Dim _hitOccurred As Boolean

      Const MARGINSIZE As Integer = 4

      On Error Resume Next

      lcSearchText = LCase(searchText)

      lastHit = 0
      contextIndex = 0
      'numContextLines = CInt(Common.mainForm.txtContextLines.Text)
      numContextLines = options.contextLines

      ' Default spacer (left margin) values. If Line Numbers are on,
      ' these will be reset within the loop to include line numbers.
      If numContextLines > 0 Then
         contextSpacer = Space(MARGINSIZE)
         spacer = Right(contextSpacer, MARGINSIZE - 2) & "> "
      Else
         spacer = Space(MARGINSIZE)
      End If

      ' Notify the user that we are searching for matching files (status bar).
      Common.mainForm.stbStatus.Text = "Searching " & path

      file = FreeFile() ' Get a file handle

      ' Get the first matching filename (there may be more).
      FileName = fileFinder.FindFirstFile(path)

      ' Begin opening files and rooting through them.
      Do While FileName <> ""


         ' Use bitwise comparison to make sure FileName isn't a directory.
         If Not (GetAttr(path & FileName) = FileAttribute.Directory) Then

            Err.Clear()

            ' Only show each file name once regardless of how many "hits" are displayed.
            FileNameDisplayed = False

            ' Notify the user of which file is being searched (status bar).
            Common.mainForm.stbStatus.Text = "Searching " & path & FileName

            FileOpen(file, path & FileName, OpenMode.Input) ' Create filename.

            ' If we have a problem with a file, skip to the next one.
            If Err.Number <> 0 Then
               GoTo Continue
            End If

            LineNum = 0

            'If Common.mainForm.chkNegation.Checked Then _hitOccurred = False
            If options.negation Then _hitOccurred = False

            ' Look through this file for a match on searchtext.
            Do While Not EOF(file)

               temp = Len(Tempstr)

               '********************************************************
               ' Every so often, dump our buffer to our output text box.
               ' It is much faster than always updating the text box.
               ' Always update the text box at first, so the viewer can
               ' see the results right away.
               '********************************************************
               If temp > 25000 Or (Len(Common.mainForm.txtHits.Text) < 1800 And temp > 0) Then
                  Common.mainForm.txtHits.Text = Common.mainForm.txtHits.Text & Tempstr

                  KeepSyntaxHighlighted()

                  If Err.Number = 7 Then
                     MessageBox.Show(ErrorToString(Err.Number) & ": " & Err.Description, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                     GB_CANCEL = True
                     FileClose(file)
                     Exit Sub
                  End If

                  Tempstr = ""
               End If

               System.Windows.Forms.Application.DoEvents() ' Allow our output displays to refresh, etc...

               ' If they hit the cancel button, get out 'o here.
               If GB_CANCEL = True Then
                  Common.mainForm.txtHits.Text = Common.mainForm.txtHits.Text & Tempstr

                  KeepSyntaxHighlighted()

                  FileClose(file)
                  Exit Sub
               End If


               ' Read a line of input.
               textLine = GetLineOfInput(file)
               LineNum = LineNum + 1

               ' See if the our SearchText is in this line.
               'If Common.mainForm.chkRegularExpressions.Checked Then
               If options.useRegularExpressions Then

                  If _regularExp.IsMatch(textLine, searchText) Then

                     'If Common.mainForm.chkNegation.Checked Then _hitOccurred = True
                     If options.negation Then _hitOccurred = True

                     i = 1

                  Else
                     i = 0
                  End If
               Else
                  'If Common.mainForm.chkCaseSensitive.Checked Then
                  If options.caseSensistiveMatch Then

                     ' Need to escape these characters in SearchText:
                     ' < $ + * [ { ( ) .
                     ' with a preceeding \

                     ' If we are looking for whole worlds only, perform the check.
                     'If Common.mainForm.chkWholeWordOnly.Checked Then
                     If options.wholeWordMatch Then
                        If _regularExp.IsMatch(textLine, "(^|\W{1})(" & searchText & ")(\W{1}|$)") Then

                           'If Common.mainForm.chkNegation.Checked Then _hitOccurred = True
                           If options.negation Then _hitOccurred = True

                           i = 1
                        Else
                           i = 0
                        End If
                     Else
                        i = InStr(1, textLine, searchText, CompareMethod.Binary)

                        'If Common.mainForm.chkNegation.Checked AndAlso i > 0 Then _hitOccurred = True
                        If options.negation AndAlso i > 0 Then _hitOccurred = True

                     End If
                  Else
                     ' If we are looking for whole worlds only, perform the check.
                     'If Common.mainForm.chkWholeWordOnly.Checked Then
                     If options.wholeWordMatch Then
                        If _regularExp.IsMatch(LCase(textLine), "(^|\W{1})(" & lcSearchText & ")(\W{1}|$)") Then

                           'If Common.mainForm.chkNegation.Checked Then _hitOccurred = True
                           If options.negation Then _hitOccurred = True

                           i = 1
                        Else
                           i = 0
                        End If
                     Else
                        i = InStr(1, textLine, searchText, CompareMethod.Text)

                        'If Common.mainForm.chkNegation.Checked AndAlso i > 0 Then _hitOccurred = True
                        If options.negation AndAlso i > 0 Then _hitOccurred = True
                     End If
                  End If
               End If

               '*******************************************
               ' We found an occurrence of our search text.
               '*******************************************
               If i <> 0 Then

                  'since we have a hit, check to see if negation is checked
                  'If Common.mainForm.chkNegation.Checked Then Exit Do
                  If options.negation Then Exit Do

                  If Not FileNameDisplayed Then

                     ' Create an instance of a "hit" object to store
                     ' all the hits for a given filename.
                     ho = G_HITS.Add(path, FileName)

                     ' Due to an inconsistency in the way the Dir commannd works
                     ' we may sometimes search the same files twice.
                     ' For example (*.html, *.htm sometimes both find the same files)
                     ' When this happens CHitObjectCollection.Add will return null.
                     ' We will then know that this file has already been
                     ' searched.
                     If ho Is Nothing Then
                        ' Abandon this file, it has already been checked.
                        Exit Do
                     End If

                     ' Add the filename to the filename list.
                     AddHitToList(ho)
                     FileNameDisplayed = True

                  End If

                  AddHit(1) ' Add a "hit" to the status bar.

                  ' If we are only showing filenames, go to the next file.
                  'If (Common.mainForm.chkFileNamesOnly.CheckState = 1) Then Exit Do
                  If options.onlyFileNames Then Exit Do

                  ' Set up line number, or just an indention in front of the line.
                  'If Common.mainForm.chkLineNumbers.Checked Then
                  If options.includeLineNumbers Then
                     spacer = "(" & Trim(Str(LineNum))
                     If Len(spacer) <= 5 Then
                        spacer = spacer & Space(6 - Len(spacer))
                     End If
                     spacer = spacer & ") "
                     contextSpacer = "(" & Space(Len(spacer) - 3) & ") "
                  End If

                  ' Display context lines if applicable.
                  If numContextLines > 0 And lastHit = 0 Then

                     If ho.GetNumHits > 0 Then
                        ' Insert a blank space before the context lines.
                        ho.AddHit(-1, Chr(13) & Chr(10))
                     End If

                     ' Display preceeding n context lines before the hit.
                     For i = numContextLines To 1 Step -1

                        contextIndex = contextIndex + 1
                        If contextIndex > numContextLines Then contextIndex = 1

                        ' If there is a match in the first one or two lines,
                        ' the entire preceeding context may not be available.
                        If LineNum > i Then
                           ' Add the context line.
                           ho.AddHit(LineNum - i, contextSpacer & Context(contextIndex) & Chr(13) & Chr(10))
                        End If
                     Next i

                  End If

                  lastHit = numContextLines

                  ' Add the actual "hit".
                  ho.AddHit(LineNum, spacer & textLine & Chr(13) & Chr(10))

                  '***************************************************
                  ' We didn't find a hit, but since lastHit is > 0, we
                  ' need to display this context line.
                  '***************************************************

               ElseIf lastHit > 0 And numContextLines > 0 Then

                  ho.AddHit(LineNum, contextSpacer & textLine & Chr(13) & Chr(10))
                  lastHit = lastHit - 1

               End If ' Found a hit or not.

               ' If we are showing context lines, keep the last n lines.
               If numContextLines > 0 Then
                  If contextIndex = numContextLines Then
                     contextIndex = 1
                  Else
                     contextIndex = contextIndex + 1
                  End If
                  Context(contextIndex) = textLine
               End If

            Loop  ' Through each line of the file.

            'Check for no hits through out the file
            'If Common.mainForm.chkNegation.Checked AndAlso _hitOccurred = False Then
            If options.negation AndAlso _hitOccurred = False Then
               'add the file to the hit list
               If Not FileNameDisplayed Then

                  ' Create an instance of a "hit" object to store
                  ' all the hits for a given filename.
                  ho = G_HITS.Add(path, FileName)

                  ' Due to an inconsistency in the way the Dir commannd works
                  ' we may sometimes search the same files twice.
                  ' For example (*.html, *.htm sometimes both find the same files)
                  ' When this happens CHitObjectCollection.Add will return null.
                  ' We will then know that this file has already been
                  ' searched.
                  If Not ho Is Nothing Then
                     ' Add the filename to the filename list.
                     AddHitToList(ho)

                     FileNameDisplayed = True
                  End If

               End If
            End If

            lastHit = 0 ' Don't carry context lines across files.
            FileClose(file) ' Close the file.

            ' Close the HitObject.
            ho = Nothing

         End If

Continue:

         FileName = fileFinder.FindNextFile() ' Get the next directory entry.

      Loop

      ' Make sure our output has been completely updated.
      If Len(Tempstr) > 0 Then
         Common.mainForm.txtHits.Text = Common.mainForm.txtHits.Text & Tempstr
      End If

      ' Allow processing and updates in the event that no files matching their
      ' criteria are being found.
      System.Windows.Forms.Application.DoEvents()

      '********************************
      ' If we are recursing, start now.
      '********************************
      Dim DirList(255) As String
      Dim DirCount As Integer
      If (options.recursiveSearch) Then


         ' If they hit the cancel button, get out 'o here.
         If GB_CANCEL = True Then
            FileClose(file)
            Exit Sub
         End If

         DirCount = 0

         ' Get the first matching filename (there may be more).
         FileName = fileFinder.FindFirstDirectory(path)

         ' Store all the subdirectory names from this directory into an array.
         Do While FileName <> ""

            ' Don't overrun our array boundaries.
            If DirCount > 255 Then
               MessageBox.Show("Too many directories in " & path & "!", "Max Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
               Exit Do
            End If

            ' Add a directory to the array.
            DirCount = DirCount + 1
            DirList(DirCount) = FileName

            ' Get the next directory entry.
            FileName = fileFinder.FindNextDirectory()
         Loop

         '*****************************************************************
         ' Call this routine recursively for each directory found.
         ' NOTE: we can't do this inside the loop above to save array space
         '  because if you change directories inside that loop, you cannot
         ' use Dir to obtain the "next" directory entry.
         '*****************************************************************
         For i = 1 To DirCount

            If GB_CANCEL = True Then
               FileClose(file)
               Exit Sub
            End If

            Call SearchDirectory(path & DirList(i) & "\", fileFinder, searchText, options)

         Next i

      End If

      Exit Sub

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   update the number of hits
   ''' </summary>
   ''' <param name="AddHits">Optional - Number of hits</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub AddHit(Optional ByVal hits As Integer = 1)
      GB_NUMHITS = GB_NUMHITS + hits
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Clear number of hits
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub ClearHits()
      GB_NUMHITS = 0
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Read a line of input
   ''' </summary>
   ''' <param name="fileNum">File number</param>
   ''' <returns>A line of input</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Function GetLineOfInput(ByRef fileNumber As Integer) As String
      Dim _textLine As String
      Dim _lineEnd As String

      Do
         _textLine &= InputString(fileNumber, 1)

         _lineEnd = Right(_textLine, Len(ENDOFLINEMARKER))
         If _lineEnd = ENDOFLINEMARKER Then
            _textLine = Left(_textLine, Len(_textLine) - Len(ENDOFLINEMARKER))

            ' Remove the extraneous carriage return if there is one.
            ' This has to do with setting lf as end of line character and then
            ' searching files that end lines with cr/lf.
            If ENDOFLINEMARKER = vbLf And Right(_textLine, 1) = vbCr Then
               _textLine = Left(_textLine, Len(_textLine) - 1)
            End If
            Exit Do
         End If

      Loop While Not EOF(fileNumber)

      Return _textLine
   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Create a new hit object and initialize it (NOT USED)
   ''' </summary>
   ''' <param name="path">Path for file</param>
   ''' <param name="fileName">file name</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub AddHitObject(ByRef path As String, ByRef fileName As String)
      Dim _hitObject As New CHitObject(path, fileName)
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Keeps the syntax highlighted
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   02/02/2005	Initial
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub KeepSyntaxHighlighted()
      If Common.mainForm.lstFileNames.SelectedIndices.Count > 0 Then
         Dim _index As Integer = Common.mainForm.lstFileNames.SelectedIndices.Item(0)

         Common.mainForm.lstFileNames.Items(_index).Selected = False
         Common.mainForm.lstFileNames.Items(_index).Selected = True
      End If
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Adds a hit to the list view
   ''' </summary>
   ''' <param name="hit">Hit to Add</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   02/05/2005	Initial
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub AddHitToList(ByVal hit As CHitObject)
      Dim _listItem As ListViewItem

      'Create the list item
      _listItem = New ListViewItem(hit.FileName)
      _listItem.SubItems.Add(hit.Path)
      _listItem.SubItems.Add(Format(hit.LastWriteDate, "MM/dd/yyyy HH:mm:ss"))
      _listItem.SubItems.Add(hit.HitCollectionIndex.ToString)

      'Add list item to listview
      Common.mainForm.lstFileNames.Items.Add(_listItem)

      'clear it out
      _listItem = Nothing
   End Sub
#End Region

End Module