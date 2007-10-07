''' -----------------------------------------------------------------------------
''' <summary>
'''   Common Routines and Variables
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
''' 	[Curtis_Beard]	   01/11/2005	Initial - Moved common routines into this module
''' </history>
''' ''' -----------------------------------------------------------------------------
Module Common

#Region "Declarations"
   Public Const MAX_STORED_PATHS As Integer = 25
   Public NUM_STORED_PATHS As Integer
   Public DEFAULT_EDITOR As String
   Public EDITOR_ARG As String
   Public ENDOFLINEMARKER As String
   Public mainForm As frmMain

   Public USE_REG_EXPRESSIONS As Boolean
   Public USE_CASE_SENSITIVE As Boolean
   Public USE_WHOLE_WORD As Boolean
   Public USE_LINE_NUMBERS As Boolean
   Public USE_RECURSION As Boolean
   Public SHOW_FILE_NAMES_ONLY As Boolean
   Public NUM_CONTEXT_LINES As Integer
   Public Const MAX_CONTEXT_LINES As Integer = 9
   Public USE_NEGATION As Boolean
#End Region

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Load the stored options from the registry
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion, Try/Catch, naming Conventions
   '''   [Curtis_Beard]	   02/11/2005	ADD: Load window settings
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub LoadRegistrySettings()
      Dim _registryValue As String
      Dim _registryKey As String
      Dim _index As Integer

      Try
         ENDOFLINEMARKER = GetSetting("AstroGrep", "Startup", "ENDOFLINEMARKER", vbCrLf)

         ' Get the editor that has been chosen with which to open files.
         DEFAULT_EDITOR = GetSetting("AstroGrep", "Startup", "DEFAULT_EDITOR", "notepad")

         ' Get the editor that has been chosen with which to open files.
         EDITOR_ARG = GetSetting("AstroGrep", "Startup", "EDITOR_ARG", "")

         ' Read the max number of stored paths.
         Try
            NUM_STORED_PATHS = CInt(GetSetting("AstroGrep", "Startup", "MAX_STORED_PATHS", CStr(10)))
         Catch ex As Exception
            NUM_STORED_PATHS = 10
         End Try

         ' Only load up to the desired number of paths.
         If NUM_STORED_PATHS < 0 OrElse NUM_STORED_PATHS > MAX_STORED_PATHS Then
            NUM_STORED_PATHS = MAX_STORED_PATHS
         End If

         ' Get the MRU Paths and add them to the path combobox.
         For _index = 0 To NUM_STORED_PATHS - 1
            _registryKey = "MRUPath" & Trim(Str(_index))

            _registryValue = GetSetting("AstroGrep", "Startup", _registryKey, "")

            ' Add the path to the path combobox.
            If Not _registryValue.Equals(String.Empty) Then
               Common.mainForm.cboFilePath.Items.Add(_registryValue)
            End If

            ' Get the most recent search expressions
            _registryKey = "MRUExpression" & Trim(Str(_index))
            _registryValue = GetSetting("AstroGrep", "Startup", _registryKey, "")

            ' Add the search expression to the path combobox.
            If Not _registryValue.Equals(String.Empty) Then
               Common.mainForm.cboSearchForText.Items.Add(_registryValue)
            End If

            ' Get the most recent File names
            _registryKey = "MRUFileName" & Trim(Str(_index))
            _registryValue = GetSetting("AstroGrep", "Startup", _registryKey, "")

            ' Add the file name to the path combobox.
            If Not _registryValue.Equals(String.Empty) Then
               Common.mainForm.cboFileName.Items.Insert(_index, _registryValue)
            End If

         Next _index

         If Common.mainForm.cboFilePath.Items.Count > 0 Then
            Common.mainForm.cboFilePath.SelectedIndex = 0
         End If

         If Common.mainForm.cboSearchForText.Items.Count > 0 Then
            Common.mainForm.cboSearchForText.SelectedIndex = 0
         End If

         If Common.mainForm.cboFileName.Items.Count > 0 Then
            Common.mainForm.cboFileName.SelectedIndex = 0
         Else
            'no entries so create defaults
            Common.mainForm.cboFileName.Items.AddRange(New Object() {"*.*", "*.txt", "*.java", "*.htm, *.html", "*.jsp, *.asp", "*.js, *.inc", "*.htm, *.html, *.jsp, *.asp", "*.sql", "*.bas, *.cls", "*.cpp, *.c, *.h", "*.asm"})
            Common.mainForm.cboFileName.SelectedIndex = 0
         End If

         'Set Window Settings
         SetWindowSettings()

      Catch ex As Exception
         System.Diagnostics.Debug.WriteLine(ex.ToString)
      End Try

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Store static options to the registry
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion, Try/Catch, naming Conventions
   '''   [Curtis_Beard]	   02/11/2005	ADD: Save window settings
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub UpdateRegistrySettings()
      Dim _registryValue As String
      Dim _registryKey As String
      Dim _index As Integer

      Try
         ' Only save up to the desired number of paths.
         If NUM_STORED_PATHS < 0 Or NUM_STORED_PATHS > MAX_STORED_PATHS Then
            NUM_STORED_PATHS = MAX_STORED_PATHS
         End If

         ' Store the end of line marker string.
         SaveSetting("AstroGrep", "Startup", "ENDOFLINEMARKER", ENDOFLINEMARKER)

         ' Store the editor string.
         SaveSetting("AstroGrep", "Startup", "DEFAULT_EDITOR", DEFAULT_EDITOR)

         ' Store the editor string.
         SaveSetting("AstroGrep", "Startup", "EDITOR_ARG", EDITOR_ARG)

         ' Store the path.
         SaveSetting("AstroGrep", "Startup", "MAX_STORED_PATHS", CStr(NUM_STORED_PATHS))

         ' Store the most recently used search paths in the registry.
         Do While _index < NUM_STORED_PATHS ' And _index < frmMain.cboFilePath.ListCount

            If _index < Common.mainForm.cboFilePath.Items.Count Then
               _registryKey = "MRUPath" & Trim(Str(_index))
               _registryValue = CStr(Common.mainForm.cboFilePath.Items.Item(_index))

               'safety check
               If Not _registryValue.Trim.Equals("Browse...") Then
                  ' Store the path.
                  SaveSetting("AstroGrep", "Startup", _registryKey, _registryValue)
               End If

            End If

            If _index < Common.mainForm.cboFileName.Items.Count Then
               _registryKey = "MRUFileName" & Trim(Str(_index))
               _registryValue = CStr(Common.mainForm.cboFileName.Items.Item(_index))

               ' Store the search expression.
               SaveSetting("AstroGrep", "Startup", _registryKey, _registryValue)
            End If

            If _index < Common.mainForm.cboSearchForText.Items.Count Then
               _registryKey = "MRUExpression" & Trim(Str(_index))
               _registryValue = CStr(Common.mainForm.cboSearchForText.Items.Item(_index))

               ' Store the search expression.
               SaveSetting("AstroGrep", "Startup", _registryKey, _registryValue)
            End If

            _index = _index + 1
         Loop

         'Save the window positions
         SaveSetting("AstroGrep", "Startup", "POS_TOP", Common.mainForm.Top.ToString)
         SaveSetting("AstroGrep", "Startup", "POS_LEFT", Common.mainForm.Left.ToString)
         SaveSetting("AstroGrep", "Startup", "POS_WIDTH", Common.mainForm.Width.ToString)
         SaveSetting("AstroGrep", "Startup", "POS_HEIGHT", Common.mainForm.Height.ToString)
         SaveSetting("AstroGrep", "Startup", "POS_STATE", CStr(Common.mainForm.WindowState))

      Catch ex As Exception
         System.Diagnostics.Debug.WriteLine(ex.ToString)
      End Try

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Clear the registry entries and update the form to reflect
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion, Try/Catch
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub ClearRegistrySettings()

      Try
         ' Clear the combo box.
         Common.mainForm.cboFilePath.Items.Clear()
         Common.mainForm.cboFileName.Items.Clear()
         Common.mainForm.cboSearchForText.Items.Clear()

         ' Delete the entire "Startup" section.
         DeleteSetting("AstroGrep", "Startup")

      Catch ex As Exception
         System.Diagnostics.Debug.WriteLine(ex.ToString)
      End Try

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Load the common search settings
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	01/28/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub LoadSearchSettings()

      Try
         USE_REG_EXPRESSIONS = CBool(GetSetting("AstroGrep", "Startup", "USE_REG_EXPRESSIONS", "False"))
         USE_CASE_SENSITIVE = CBool(GetSetting("AstroGrep", "Startup", "USE_CASE_SENSITIVE", "False"))
         USE_WHOLE_WORD = CBool(GetSetting("AstroGrep", "Startup", "USE_WHOLE_WORD", "False"))
         USE_LINE_NUMBERS = CBool(GetSetting("AstroGrep", "Startup", "USE_LINE_NUMBERS", "True"))
         USE_RECURSION = CBool(GetSetting("AstroGrep", "Startup", "USE_RECURSION", "True"))
         SHOW_FILE_NAMES_ONLY = CBool(GetSetting("AstroGrep", "Startup", "SHOW_FILE_NAMES_ONLY", "False"))
         USE_NEGATION = CBool(GetSetting("AstroGrep", "Startup", "USE_NEGATION", "False"))
         NUM_CONTEXT_LINES = CInt(GetSetting("AstroGrep", "Startup", "NUM_CONTEXT_LINES", "0"))

         If NUM_CONTEXT_LINES < 0 OrElse NUM_CONTEXT_LINES > MAX_CONTEXT_LINES Then
            NUM_CONTEXT_LINES = 0
         End If

      Catch ex As Exception
         System.Diagnostics.Debug.WriteLine(ex.ToString)
      End Try

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Save the common search settings
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	01/28/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub SaveSearchSettings()

      Try

         If NUM_CONTEXT_LINES < 0 OrElse NUM_CONTEXT_LINES > MAX_CONTEXT_LINES Then
            NUM_CONTEXT_LINES = 0
         End If

         SaveSetting("AstroGrep", "Startup", "USE_REG_EXPRESSIONS", CStr(USE_REG_EXPRESSIONS))
         SaveSetting("AstroGrep", "Startup", "USE_CASE_SENSITIVE", CStr(USE_CASE_SENSITIVE))
         SaveSetting("AstroGrep", "Startup", "USE_WHOLE_WORD", CStr(USE_WHOLE_WORD))
         SaveSetting("AstroGrep", "Startup", "USE_LINE_NUMBERS", CStr(USE_LINE_NUMBERS))
         SaveSetting("AstroGrep", "Startup", "USE_RECURSION", CStr(USE_RECURSION))
         SaveSetting("AstroGrep", "Startup", "SHOW_FILE_NAMES_ONLY", CStr(SHOW_FILE_NAMES_ONLY))
         SaveSetting("AstroGrep", "Startup", "NUM_CONTEXT_LINES", CStr(NUM_CONTEXT_LINES))
         SaveSetting("AstroGrep", "Startup", "USE_NEGATION", CStr(USE_NEGATION))

      Catch ex As Exception
         System.Diagnostics.Debug.WriteLine(ex.ToString)
      End Try

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Edit a file that the user has double clicked on
   ''' </summary>
   ''' <param name="FileName"></param>
   ''' <param name="LineNum"></param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion, Try/Catch
   '''   [Curtis_Beard]	   06/13/2005	CHG: Used new cmd line arg specification
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub EditFile(ByRef FileName As String, ByRef LineNum As Integer)

      Try
         If EDITOR_ARG.Equals(String.Empty) OrElse EDITOR_ARG.IndexOf("%1") = -1 Then
            'no file argument specified
            MessageBox.Show("No command line argument has been specified for the filename." & vbCrLf & "Please specify the file argument under Tools->Options", "Error - Editing File", MessageBoxButtons.OK, MessageBoxIcon.Information)
         Else
            'replace %1 with filename and %2 with linenum
            Dim _text As String = EDITOR_ARG
            _text = _text.Replace("%1", """" & FileName & """")
            _text = _text.Replace("%2", CStr(LineNum))

            'Check to see if editor needs quotes around it
            Dim _editor As String = DEFAULT_EDITOR
            If _editor.IndexOf(".exe") > 0 Then
               _editor = """" & _editor & """"
            End If

            Shell(_editor & " " & _text, AppWinStyle.NormalFocus)
         End If

      Catch ex As Exception
         MessageBox.Show("Error editing file: " & FileName & vbCrLf & ex.ToString, "Error - Editing File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      End Try

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Check to see if the begin/end text around searched text is valid
   '''   for a whole word search
   ''' </summary>
   ''' <param name="beginText">Text in front of searched text</param>
   ''' <param name="endText">Text behind searched text</param>
   ''' <returns>True - valid, False - otherwise</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	01/27/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function WholeWordOnly(ByVal beginText As String, ByVal endText As String) As Boolean
      Dim _valid As Boolean = False

      If ValidBeginText(beginText) AndAlso ValidEndText(endText) Then
         _valid = True
      End If

      Return _valid
   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Replaces TwipsToPixels found in compability dll
   ''' </summary>
   ''' <param name="twips">twips to convert</param>
   ''' <returns>Pixels, Twips on failure</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	01/27/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function ConvertTwipsToPixels(ByVal twips As Integer) As Integer

      Try
         Return CInt((twips * 96) / 1440)
      Catch ex As Exception
         Return twips
      End Try

   End Function

#Region "Private Methods"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Validate a start text
   ''' </summary>
   ''' <param name="beginText">text to validate</param>
   ''' <returns>True - valid, False - otherwise</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[cb230008]	01/27/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Function ValidBeginText(ByVal beginText As String) As Boolean
      'space, <, $, +, *, [, {, (, .

      If beginText.EndsWith(" ") OrElse _
         beginText.EndsWith("<") OrElse _
         beginText.EndsWith("$") OrElse _
         beginText.EndsWith("+") OrElse _
         beginText.EndsWith("*") OrElse _
         beginText.EndsWith("[") OrElse _
         beginText.EndsWith("{") OrElse _
         beginText.EndsWith("(") OrElse _
         beginText.EndsWith(".") OrElse _
         beginText.EndsWith(vbCrLf) OrElse _
         beginText.EndsWith(vbCr) OrElse _
         beginText.EndsWith(vbLf) Then

         Return True
      End If

      Return False
   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Validate an end text
   ''' </summary>
   ''' <param name="endText">text to validate</param>
   ''' <returns>True - valid, False - otherwise</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[cb230008]	01/27/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Function ValidEndText(ByVal endText As String) As Boolean

      If endText.StartsWith(" ") OrElse _
         endText.StartsWith("<") OrElse _
         endText.StartsWith("$") OrElse _
         endText.StartsWith("+") OrElse _
         endText.StartsWith("*") OrElse _
         endText.StartsWith("[") OrElse _
         endText.StartsWith("{") OrElse _
         endText.StartsWith("(") OrElse _
         endText.StartsWith(".") OrElse _
         endText.StartsWith(">") OrElse _
         endText.StartsWith("]") OrElse _
         endText.StartsWith("}") OrElse _
         endText.StartsWith(")") OrElse _
         endText.StartsWith(vbCrLf) OrElse _
         endText.StartsWith(vbCr) OrElse _
         endText.StartsWith(vbLf) Then

         Return True
      End If

      Return False
   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Attempts to set last window settings
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   02/11/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub SetWindowSettings()
      Dim _width As Integer
      Dim _height As Integer
      Dim _top As Integer
      Dim _left As Integer
      Dim _state As Integer

      Try
         _top = CInt(GetSetting("AstroGrep", "Startup", "POS_TOP", "-1"))
      Catch ex As Exception
         _top = -1
      End Try

      Try
         _left = CInt(GetSetting("AstroGrep", "Startup", "POS_LEFT", "-1"))
      Catch ex As Exception
         _left = -1
      End Try

      Try
         _width = CInt(GetSetting("AstroGrep", "Startup", "POS_WIDTH", "-1"))
      Catch ex As Exception
         _width = -1
      End Try

      Try
         _height = CInt(GetSetting("AstroGrep", "Startup", "POS_HEIGHT", "-1"))
      Catch ex As Exception
         _height = -1
      End Try

      Try
         _state = CInt(GetSetting("AstroGrep", "Startup", "POS_STATE", "-1"))
      Catch ex As Exception
         _state = -1
      End Try


      If _state <> -1 AndAlso _state <> FormWindowState.Maximized AndAlso _state <> FormWindowState.Minimized Then
         'set the top, left, width, and height
         If _top <> -1 Then Common.mainForm.Top = _top
         If _left <> -1 Then Common.mainForm.Left = _left
         If _width <> -1 Then Common.mainForm.Width = _width
         If _height <> -1 Then Common.mainForm.Height = _height
      End If

   End Sub
#End Region

End Module
