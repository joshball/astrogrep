Attribute VB_Name = "SearchCode"
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

Global GB_CANCEL As Integer
Global GB_NUMHITS As Integer
Global G_HITS As New CHitObjectCollection

Global Const MAX_STORED_PATHS = 25
Global NUM_STORED_PATHS As Integer
Global DEFAULT_EDITOR As String
Global ENDOFLINEMARKER As String

Option Explicit

'*******************************************
' Load the stored options from the registry.
'*******************************************

Sub LoadRegistrySettings()

    On Error GoTo eHandler
    Dim Path As String, rKey As String
    Dim i As Integer

    ENDOFLINEMARKER = GetSetting(appname:="AstroGrep", _
        section:="Startup", Key:="ENDOFLINEMARKER", Default:=vbCrLf)

    ' Get the editor that has been chosen with which to open files.
    DEFAULT_EDITOR = GetSetting(appname:="AstroGrep", _
        section:="Startup", Key:="DEFAULT_EDITOR", Default:="notepad")
    
    ' Read the max number of stored paths.
    NUM_STORED_PATHS = GetSetting(appname:="AstroGrep", _
        section:="Startup", Key:="MAX_STORED_PATHS", Default:=10)

    ' Only load up to the desired number of paths.
    If NUM_STORED_PATHS < 0 Or NUM_STORED_PATHS > MAX_STORED_PATHS Then
        NUM_STORED_PATHS = MAX_STORED_PATHS
    End If

    ' Get the MRU Paths and add them to the path combobox.
    For i = 0 To NUM_STORED_PATHS - 1
        rKey = "MRUPath" + Trim(Str(i))
        
        Path = GetSetting(appname:="AstroGrep", _
                section:="Startup", Key:=rKey, Default:="")
                
        ' Add the path to the path combobox.
        If Path <> "" Then
            frmMain.cboFilePath.AddItem Path
        End If

        ' Get the most recent search expressions
        rKey = "MRUExpression" + Trim(Str(i))
        Path = GetSetting(appname:="AstroGrep", _
                section:="Startup", Key:=rKey, Default:="")
                
        ' Add the search expression to the path combobox.
        If Path <> "" Then
            frmMain.cboSearchForText.AddItem Path
        End If
        
        ' Get the most recent File names
        rKey = "MRUFileName" + Trim(Str(i))
        Path = GetSetting(appname:="AstroGrep", _
                section:="Startup", Key:=rKey, Default:="")
                
        ' Add the file name to the path combobox.
        If Path <> "" Then
            frmMain.cboFileName.AddItem Path, i
        End If

    Next i

    frmMain.cboFileName.Text = frmMain.cboFileName.List(0)
    frmMain.cboFilePath.Text = frmMain.cboFilePath.List(0)
    frmMain.cboSearchForText.Text = frmMain.cboSearchForText.List(0)
    
    Exit Sub

eHandler:
    Debug.Print Error(Err)
End Sub

'**************************************
' Store static options to the registry.
'**************************************

Sub UpdateRegistrySettings()
    On Error GoTo eHandler
    
    Dim i As Integer
    Dim val As String
    Dim rKey As String

    ' Only save up to the desired number of paths.
    If NUM_STORED_PATHS < 0 Or NUM_STORED_PATHS > MAX_STORED_PATHS Then
        NUM_STORED_PATHS = MAX_STORED_PATHS
    End If

    ' Store the end of line marker string.
    SaveSetting "AstroGrep", "Startup", "ENDOFLINEMARKER", ENDOFLINEMARKER
    
    ' Store the editor string.
    SaveSetting "AstroGrep", "Startup", "DEFAULT_EDITOR", DEFAULT_EDITOR
    
    ' Store the path.
    SaveSetting "AstroGrep", "Startup", "MAX_STORED_PATHS", NUM_STORED_PATHS

    ' Store the most recently used search paths in the registry.
    Do While i < NUM_STORED_PATHS ' And i < frmMain.cboFilePath.ListCount
        
        If i < frmMain.cboFilePath.ListCount Then
            rKey = "MRUPath" + Trim(Str(i))
            val = frmMain.cboFilePath.List(i)

            ' Store the path.
            SaveSetting "AstroGrep", "Startup", rKey, val
        End If

        If i < frmMain.cboFileName.ListCount Then
            rKey = "MRUFileName" + Trim(Str(i))
            val = frmMain.cboFileName.List(i)

            ' Store the search expression.
            SaveSetting "AstroGrep", "Startup", rKey, val
        End If
        
        If i < frmMain.cboSearchForText.ListCount Then
            rKey = "MRUExpression" + Trim(Str(i))
            val = frmMain.cboSearchForText.List(i)

            ' Store the search expression.
            SaveSetting "AstroGrep", "Startup", rKey, val
        End If
        
        i = i + 1
    Loop
    
    Exit Sub
eHandler:
    Debug.Print Error(Err)
End Sub

Sub ClearRegistrySettings()

    On Error GoTo eHandler
        
    ' Clear the combo box.
    frmMain.cboFilePath.Clear
    frmMain.cboFileName.Clear
    frmMain.cboSearchForText.Clear
    
    ' Delete the entire "Startup" section.
    DeleteSetting "AstroGrep", "Startup"

    Exit Sub

eHandler:
    Debug.Print Error(Err)
End Sub

Sub AddHit(Optional AddHits As Integer = 1)

    '*************************************
    ' Display the number of "hits" so far.
    '*************************************
    
    GB_NUMHITS = GB_NUMHITS + AddHits

    frmMain.lblResults = "Results: " & Str(GB_NUMHITS) & " hits"

End Sub

Sub ClearHits()
    GB_NUMHITS = 0
End Sub


Sub Search(Path As String, ByVal FileName As String, SearchText As String)
    
    Dim FileFinder As New CFileFinder

    '*************************
    ' Validate the parameters.
    '*************************
    
    If FileName = "" Then
        MsgBox "You must supply the name of a file in which to search", vbCritical, "Missing Parameter"
        Exit Sub
    End If
    
    If SearchText = "" Then
        MsgBox "You must supply text for which to search", vbCritical, "Missing Parameter"
        Exit Sub
    End If
    
    '*************************
    ' Display the cancel form.
    '*************************
    
    frmMain.lblExpression.Caption = "Expression: " + SearchText
    SetSearch True
    
    '***************************
    ' Reset the hits collection.
    '***************************
    
    Set G_HITS = New CHitObjectCollection
    frmMain.lstFileNames.Clear
    frmMain.txtHits.TextRTF = ""
    
    '***************************************************
    ' Initialize some stuff before beginning the search.
    '***************************************************
    
    frmMain.lblSearchFile = ""    ' Not searching any files.
    frmMain.lblResults = "Results: 0 hits"   ' No hits.
    frmMain.txtHits.TextRTF = ""   ' Clear past hits.
    
    ClearHits   ' Set the number of hits to zero.

    '******************************************************
    ' Add all desired file names to the file finder object.
    '******************************************************
    
    FileFinder.AddFileName FileName

    '*********************
    ' Kick off the search.
    '*********************
    
    SearchDirectory Path, FileFinder, SearchText, frmMain.chkRecurse.Value
    
    SetSearch False
    GB_CANCEL = False       ' Reset the cancel button.
    AddHit 0                ' Update the Results window.
    
End Sub

Sub SearchDirectory(Path As String, FileFinder As CFileFinder, _
    SearchText As String, Recurse As Boolean)
    
    Dim file As Integer, i As Integer
    Dim textLine As String
    Dim FileNameDisplayed As Boolean
    Dim FileName As String
    Dim Tempstr As String
    Dim LineNum As Long
    
    Dim Context(10) As String, contextIndex As Integer
    Dim lastHit As Integer, numContextLines As Integer
    Dim contextSpacer As String, spacer As String
    Dim ho As CHitObject
    Dim lcSearchText As String
    
    Const MARGINSIZE = 4
    
    On Error Resume Next
    
    lcSearchText = LCase(SearchText)

    lastHit = 0
    contextIndex = 0
    numContextLines = val(frmMain.lblContextLines.Caption)
    
    '*************************************************************
    ' Default spacer (left margin) values. If Line Numbers are on,
    ' these will be reset within the loop to include line numbers.
    '*************************************************************
    
    If numContextLines > 0 Then
        contextSpacer = Space(MARGINSIZE)
        spacer = Right$(contextSpacer, MARGINSIZE - 2) & "> "
    Else
        spacer = Space(MARGINSIZE)
    End If

    '***********************************************************************
    ' Notify the user that we are searching for matching files (status bar).
    '***********************************************************************
    
    frmMain.lblSearchDirectory = Path
    frmMain.lblSearchFile = "Searching: ..."
    
    file = FreeFile ' Get a file handle
    
    '*****************************************************
    ' Get the first matching filename (there may be more).
    '*****************************************************
    
    FileName = FileFinder.FindFirstFile(Path)

    '**********************************************
    ' Begin opening files and rooting through them.
    '**********************************************
    
    Do While FileName <> ""
        
        '****************************************************************
        ' Use bitwise comparison to make sure FileName isn't a directory.
        '****************************************************************
        
        If Not (GetAttr(Path & FileName) And vbDirectory) Then
         
            Err = 0
            
            '********************************************
            ' Only show each file name once regardless of
            ' how many "hits" are displayed.
            '********************************************
            
            FileNameDisplayed = False
            
            '**************************************************************
            ' Notify the user of which file is being searched (status bar).
            '**************************************************************
            
            frmMain.lblSearchFile = FileName
            
            Open Path & FileName For Input As #file      ' Create filename.

            '********************************************************
            ' If we have a problem with a file, skip to the next one.
            '********************************************************
            
            If Err <> 0 Then GoTo Continue
            
            LineNum = 0
            
            '**************************************************
            ' Look through this file for a match on searchtext.
            '**************************************************
            
            Do While Not EOF(file)
            
                Dim temp As Integer
                temp = Len(Tempstr)

                 '********************************************************
                ' Every so often, dump our buffer to our output text box.
                ' It is much faster than always updating the text box.
                ' Always update the text box at first, so the viewer can
                ' see the results right away.
                '********************************************************

                If temp > 25000 Or (Len(frmMain.txtHits.TextRTF) < 1800 And temp > 0) Then
                    frmMain.txtHits.TextRTF = frmMain.txtHits.TextRTF & Tempstr
                    If Err = 7 Then
                        MsgBox Error(Err)
                        GB_CANCEL = True
                        Exit Sub
                    End If
                    Tempstr = ""
                End If
                
                DoEvents    ' Allow our output displays to refresh, etc...
                
                '************************************************
                ' If they hit the cancel button, get out 'o here.
                '************************************************
                
                If GB_CANCEL = True Then
                    frmMain.txtHits.TextRTF = frmMain.txtHits.TextRTF & Tempstr
                    Exit Sub
                End If
                
                '**********************
                ' Read a line of input.
                '**********************
                
                textLine = getLineOfInput(file)
                LineNum = LineNum + 1
                
                '*******************************************
                ' See if the our SearchText is in this line.
                '*******************************************
                
                Dim regularExp As New RegExp
                If frmMain.chkRegularExpressions.Value = vbChecked Then
                    regularExp.Pattern = SearchText
                    If regularExp.Test(textLine) Then
                        i = 1
                    Else
                        i = 0
                    End If
                Else
                    If frmMain.chkCaseSensitive.Value = vbChecked Then

                        ' Need to escape these characters in SearchText:
                        ' < $ + * [ { ( ) .
                        ' with a preceeding \
                    
                        ' If we are looking for whole worlds only, perform the check.
                        If frmMain.chkWholeWordOnly.Value = vbChecked Then
                            regularExp.Pattern = "(^|\W{1})(" + SearchText + ")(\W{1}|$)"
                            If regularExp.Test(textLine) Then i = 1 Else i = 0
                        Else
                            i = InStr(1, textLine, SearchText, vbBinaryCompare)
                        End If
                    Else
                        ' If we are looking for whole worlds only, perform the check.
                        If frmMain.chkWholeWordOnly.Value = vbChecked Then
                            regularExp.Pattern = "(^|\W{1})(" + lcSearchText + ")(\W{1}|$)"
                            If regularExp.Test(LCase(textLine)) Then i = 1 Else i = 0
                        Else
                            i = InStr(1, textLine, SearchText, vbTextCompare)
                        End If
                    End If
                End If
                
                '*******************************************
                ' We found an occurrence of our search text.
                '*******************************************
                
                If i <> 0 Then
                    
                    If Not FileNameDisplayed Then
                    
                        ' Create an instance of a "hit" object to store
                        ' all the hits for a given filename.
                        
                        Set ho = G_HITS.Add(Path, FileName)

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
                        frmMain.lstFileNames.AddItem FileName

                        FileNameDisplayed = 1

                    End If

                    AddHit 1 ' Add a "hit" to the status bar.

                    ' If we are only showing filenames, go to the next file.
                    If (frmMain.chkFileNamesOnly.Value = 1) Then Exit Do
                    
                    ' Set up line number, or just an indention in front of the line.
                    If frmMain.chkLineNumbers.Value Then
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
                            ho.AddHit -1, Chr(13) & Chr(10)
                        End If
                        
                        ' Display preceeding n context lines before the hit.
                        For i = numContextLines To 1 Step -1
                        
                            contextIndex = contextIndex + 1
                            If contextIndex > numContextLines Then contextIndex = 1
                            
                            ' If there is a match in the first one or two lines,
                            ' the entire preceeding context may not be available.
                            If LineNum > i Then
                                ' Add the context line.
                                ho.AddHit LineNum - i, contextSpacer & Context(contextIndex) _
                                    & Chr(13) & Chr(10)
                            End If
                        Next i
                       
                    End If

                    lastHit = numContextLines

                    ' Add the actual "hit".
                    ho.AddHit LineNum, spacer & textLine & Chr(13) & Chr(10)

                '***************************************************
                ' We didn't find a hit, but since lastHit is > 0, we
                ' need to display this context line.
                '***************************************************
                
                ElseIf lastHit > 0 And numContextLines > 0 Then
                    
                    ho.AddHit LineNum, contextSpacer & textLine & Chr(13) & Chr(10)
                    lastHit = lastHit - 1
                    
                End If  ' Found a hit or not.

                ' If we are showing context lines, keep the last n lines.
                If numContextLines > 0 Then
                    If contextIndex = numContextLines Then
                        contextIndex = 1
                    Else
                        contextIndex = contextIndex + 1
                    End If
                    Context(contextIndex) = textLine
                End If

            Loop    ' Through each line of the file.

            lastHit = 0 ' Don't carry context lines across files.
            Close #file ' Close the file.
            
            ' Close the HitObject.
            Set ho = Nothing
            
        End If
        
Continue:
        
        FileName = FileFinder.FindNextFile()  ' Get the next directory entry.
        
    Loop

    '**************************************************
    ' Make sure our output has been completely updated.
    '**************************************************
    
    If Len(Tempstr) > 0 Then
        frmMain.txtHits.TextRTF = frmMain.txtHits.TextRTF & Tempstr
    End If

    '***********************************************************************
    ' Allow processing and updates in the event that no files matching their
    ' criteria are being found.
    '***********************************************************************
    
    DoEvents
    
    '********************************
    ' If we are recursing, start now.
    '********************************
    
    If (Recurse) Then
        
        Dim DirList(255) As String
        Dim DirCount As Integer
        
        '************************************************
        ' If they hit the cancel button, get out 'o here.
        '************************************************
        
        If GB_CANCEL = True Then Exit Sub

        DirCount = 0
        
        '*****************************************************
        ' Get the first matching filename (there may be more).
        '*****************************************************
        
        FileName = FileFinder.FindFirstDirectory(Path)
        
        '********************************************************************
        ' Store all the subdirectory names from this directory into an array.
        '********************************************************************
        
        Do While FileName <> ""
        
            ' Don't overrun our array boundaries.
            If DirCount > 255 Then
                MsgBox "Too many directories in " & Path & "!", vbCritical
                Exit Do
            End If
            
            ' Add a directory to the array.
            DirCount = DirCount + 1
            DirList(DirCount) = FileName
            
            ' Get the next directory entry.
            FileName = FileFinder.FindNextDirectory()
        Loop
        
        '*****************************************************************
        ' Call this routine recursively for each directory found.
        ' NOTE: we can't do this inside the loop above to save array space
        '  because if you change directories inside that loop, you cannot
        ' use Dir to obtain the "next" directory entry.
        '*****************************************************************
        
        For i = 1 To DirCount
            
            If GB_CANCEL = True Then Exit Sub

            Call SearchDirectory(Path & DirList(i) & "\", FileFinder, _
                SearchText, Recurse)
            
        Next i

    End If
    
    Exit Sub
    
End Sub

Function getLineOfInput(fileNum As Integer)

    '**********************
    ' Read a line of input.
    '**********************

    Dim textLine As String
    Dim lineEnd As String
    textLine = ""

    Do
        textLine = textLine + Input(1, fileNum)

        lineEnd = Right$(textLine, Len(ENDOFLINEMARKER))
        If lineEnd = ENDOFLINEMARKER Then
            textLine = Left$(textLine, Len(textLine) - Len(ENDOFLINEMARKER))

            ' Remove the extraneous carriage return if there is one.
            ' This has to do with setting lf as end of line character and then
            ' searching files that end lines with cr/lf.
            If ENDOFLINEMARKER = vbLf And Right$(textLine, 1) = vbCr Then
                textLine = Left$(textLine, Len(textLine) - 1)
            End If
            Exit Do
        End If

    Loop While Not EOF(fileNum)

    getLineOfInput = textLine
End Function

'****************************************
' Enable or disable the searching window.
'****************************************

Sub SetSearch(EnableSearch As Boolean)
    GB_CANCEL = Not EnableSearch
    frmMain.fraMain.Visible = Not EnableSearch
    frmMain.fraSearching.Visible = EnableSearch
End Sub

Function AddHitObject(Path As String, FileName As String)
    Dim ho As New CHitObject
    ho.Initialize Path, FileName
End Function

'*************************************************
' Edit a file that the user has double clicked on.
'*************************************************

Sub EditFile(FileName As String, LineNum As Long)
    On Error GoTo errHandler
    Shell DEFAULT_EDITOR + " """ + FileName + """", vbNormalFocus
    Exit Sub
errHandler:
    MsgBox ("Error editing file: " + Error$(Err))
End Sub
