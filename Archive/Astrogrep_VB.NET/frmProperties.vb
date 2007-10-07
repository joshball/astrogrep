''' -----------------------------------------------------------------------------
''' Project	 : AstroGrep
''' Class	 : frmProperties
''' 
''' -----------------------------------------------------------------------------
''' <summary>
'''   Properties Dialog
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
Friend Class frmProperties
   Inherits System.Windows.Forms.Form

#Region "Windows Form Designer generated code "
	Public Sub New()
		MyBase.New()
		
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
   Public WithEvents btnCancel As System.Windows.Forms.Button
	Public WithEvents btnOK As System.Windows.Forms.Button
   Public WithEvents Label1 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
   Friend WithEvents txtPathMRUCount As System.Windows.Forms.TextBox
   Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
   Public WithEvents chkLF As System.Windows.Forms.CheckBox
   Public WithEvents chkCR As System.Windows.Forms.CheckBox
   Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
   Public WithEvents txtEditor As System.Windows.Forms.TextBox
   Public WithEvents Label2 As System.Windows.Forms.Label
   Friend WithEvents btnBrowse As System.Windows.Forms.Button
   Friend WithEvents lblCmdOptions As System.Windows.Forms.Label
   Friend WithEvents txtCmdOptions As System.Windows.Forms.TextBox
   Friend WithEvents lblCmdOptionsView As System.Windows.Forms.Label
   Friend WithEvents cdlgOpenFile As System.Windows.Forms.OpenFileDialog
   <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
      Me.components = New System.ComponentModel.Container
      Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
      Me.btnBrowse = New System.Windows.Forms.Button
      Me.btnCancel = New System.Windows.Forms.Button
      Me.btnOK = New System.Windows.Forms.Button
      Me.Label1 = New System.Windows.Forms.Label
      Me.txtPathMRUCount = New System.Windows.Forms.TextBox
      Me.GroupBox1 = New System.Windows.Forms.GroupBox
      Me.chkLF = New System.Windows.Forms.CheckBox
      Me.chkCR = New System.Windows.Forms.CheckBox
      Me.GroupBox2 = New System.Windows.Forms.GroupBox
      Me.lblCmdOptionsView = New System.Windows.Forms.Label
      Me.txtCmdOptions = New System.Windows.Forms.TextBox
      Me.lblCmdOptions = New System.Windows.Forms.Label
      Me.txtEditor = New System.Windows.Forms.TextBox
      Me.Label2 = New System.Windows.Forms.Label
      Me.cdlgOpenFile = New System.Windows.Forms.OpenFileDialog
      Me.GroupBox1.SuspendLayout()
      Me.GroupBox2.SuspendLayout()
      Me.SuspendLayout()
      '
      'btnBrowse
      '
      Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnBrowse.Location = New System.Drawing.Point(288, 40)
      Me.btnBrowse.Name = "btnBrowse"
      Me.btnBrowse.Size = New System.Drawing.Size(24, 20)
      Me.btnBrowse.TabIndex = 11
      Me.btnBrowse.Text = "..."
      Me.ToolTip1.SetToolTip(Me.btnBrowse, "Click to locate a file editor")
      '
      'btnCancel
      '
      Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
      Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Default
      Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
      Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
      Me.btnCancel.Location = New System.Drawing.Point(256, 304)
      Me.btnCancel.Name = "btnCancel"
      Me.btnCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.btnCancel.TabIndex = 1
      Me.btnCancel.Text = "&Cancel"
      '
      'btnOK
      '
      Me.btnOK.BackColor = System.Drawing.SystemColors.Control
      Me.btnOK.Cursor = System.Windows.Forms.Cursors.Default
      Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.btnOK.ForeColor = System.Drawing.SystemColors.ControlText
      Me.btnOK.Location = New System.Drawing.Point(176, 304)
      Me.btnOK.Name = "btnOK"
      Me.btnOK.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.btnOK.TabIndex = 0
      Me.btnOK.Text = "&OK"
      '
      'Label1
      '
      Me.Label1.AutoSize = True
      Me.Label1.BackColor = System.Drawing.SystemColors.Control
      Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
      Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
      Me.Label1.Location = New System.Drawing.Point(8, 8)
      Me.Label1.Name = "Label1"
      Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.Label1.Size = New System.Drawing.Size(196, 16)
      Me.Label1.TabIndex = 2
      Me.Label1.Text = "Number of recently used paths to store:"
      '
      'txtPathMRUCount
      '
      Me.txtPathMRUCount.Location = New System.Drawing.Point(216, 8)
      Me.txtPathMRUCount.MaxLength = 2
      Me.txtPathMRUCount.Name = "txtPathMRUCount"
      Me.txtPathMRUCount.Size = New System.Drawing.Size(24, 20)
      Me.txtPathMRUCount.TabIndex = 10
      Me.txtPathMRUCount.Text = ""
      '
      'GroupBox1
      '
      Me.GroupBox1.Controls.Add(Me.chkLF)
      Me.GroupBox1.Controls.Add(Me.chkCR)
      Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.GroupBox1.Location = New System.Drawing.Point(8, 40)
      Me.GroupBox1.Name = "GroupBox1"
      Me.GroupBox1.Size = New System.Drawing.Size(320, 80)
      Me.GroupBox1.TabIndex = 11
      Me.GroupBox1.TabStop = False
      Me.GroupBox1.Text = "End of line is denoted by:"
      '
      'chkLF
      '
      Me.chkLF.BackColor = System.Drawing.SystemColors.Control
      Me.chkLF.Cursor = System.Windows.Forms.Cursors.Default
      Me.chkLF.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.chkLF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.chkLF.ForeColor = System.Drawing.SystemColors.WindowText
      Me.chkLF.Location = New System.Drawing.Point(16, 48)
      Me.chkLF.Name = "chkLF"
      Me.chkLF.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.chkLF.Size = New System.Drawing.Size(112, 17)
      Me.chkLF.TabIndex = 10
      Me.chkLF.Text = "Line Feed"
      '
      'chkCR
      '
      Me.chkCR.BackColor = System.Drawing.SystemColors.Control
      Me.chkCR.Cursor = System.Windows.Forms.Cursors.Default
      Me.chkCR.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.chkCR.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.chkCR.ForeColor = System.Drawing.SystemColors.WindowText
      Me.chkCR.Location = New System.Drawing.Point(16, 24)
      Me.chkCR.Name = "chkCR"
      Me.chkCR.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.chkCR.Size = New System.Drawing.Size(120, 17)
      Me.chkCR.TabIndex = 9
      Me.chkCR.Text = "Carriage Return"
      '
      'GroupBox2
      '
      Me.GroupBox2.Controls.Add(Me.lblCmdOptionsView)
      Me.GroupBox2.Controls.Add(Me.txtCmdOptions)
      Me.GroupBox2.Controls.Add(Me.lblCmdOptions)
      Me.GroupBox2.Controls.Add(Me.btnBrowse)
      Me.GroupBox2.Controls.Add(Me.txtEditor)
      Me.GroupBox2.Controls.Add(Me.Label2)
      Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.GroupBox2.Location = New System.Drawing.Point(8, 128)
      Me.GroupBox2.Name = "GroupBox2"
      Me.GroupBox2.Size = New System.Drawing.Size(320, 168)
      Me.GroupBox2.TabIndex = 12
      Me.GroupBox2.TabStop = False
      Me.GroupBox2.Text = "Open File Editor"
      '
      'lblCmdOptionsView
      '
      Me.lblCmdOptionsView.Location = New System.Drawing.Point(16, 144)
      Me.lblCmdOptionsView.Name = "lblCmdOptionsView"
      Me.lblCmdOptionsView.Size = New System.Drawing.Size(296, 16)
      Me.lblCmdOptionsView.TabIndex = 15
      Me.lblCmdOptionsView.Text = "Preview:"
      '
      'txtCmdOptions
      '
      Me.txtCmdOptions.Location = New System.Drawing.Point(16, 120)
      Me.txtCmdOptions.Name = "txtCmdOptions"
      Me.txtCmdOptions.Size = New System.Drawing.Size(296, 20)
      Me.txtCmdOptions.TabIndex = 13
      Me.txtCmdOptions.Text = "%1"
      '
      'lblCmdOptions
      '
      Me.lblCmdOptions.Location = New System.Drawing.Point(16, 72)
      Me.lblCmdOptions.Name = "lblCmdOptions"
      Me.lblCmdOptions.Size = New System.Drawing.Size(296, 48)
      Me.lblCmdOptions.TabIndex = 12
      Me.lblCmdOptions.Text = "Command Line Options:"
      '
      'txtEditor
      '
      Me.txtEditor.AcceptsReturn = True
      Me.txtEditor.AutoSize = False
      Me.txtEditor.BackColor = System.Drawing.SystemColors.Window
      Me.txtEditor.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.txtEditor.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtEditor.ForeColor = System.Drawing.SystemColors.WindowText
      Me.txtEditor.Location = New System.Drawing.Point(16, 40)
      Me.txtEditor.MaxLength = 0
      Me.txtEditor.Name = "txtEditor"
      Me.txtEditor.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.txtEditor.Size = New System.Drawing.Size(264, 20)
      Me.txtEditor.TabIndex = 7
      Me.txtEditor.Text = ""
      '
      'Label2
      '
      Me.Label2.BackColor = System.Drawing.SystemColors.Control
      Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
      Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
      Me.Label2.Location = New System.Drawing.Point(16, 24)
      Me.Label2.Name = "Label2"
      Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.Label2.Size = New System.Drawing.Size(288, 17)
      Me.Label2.TabIndex = 8
      Me.Label2.Text = "Program to open files with on double click."
      '
      'frmProperties
      '
      Me.AcceptButton = Me.btnOK
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.BackColor = System.Drawing.SystemColors.Control
      Me.CancelButton = Me.btnCancel
      Me.ClientSize = New System.Drawing.Size(338, 335)
      Me.Controls.Add(Me.GroupBox2)
      Me.Controls.Add(Me.GroupBox1)
      Me.Controls.Add(Me.txtPathMRUCount)
      Me.Controls.Add(Me.Label1)
      Me.Controls.Add(Me.btnCancel)
      Me.Controls.Add(Me.btnOK)
      Me.Cursor = System.Windows.Forms.Cursors.Default
      Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
      Me.Location = New System.Drawing.Point(4, 23)
      Me.MaximizeBox = False
      Me.MinimizeBox = False
      Me.Name = "frmProperties"
      Me.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.ShowInTaskbar = False
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
      Me.Text = "AstroGrep Options"
      Me.GroupBox1.ResumeLayout(False)
      Me.GroupBox2.ResumeLayout(False)
      Me.ResumeLayout(False)

   End Sub
#End Region

#Region "Events"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Closes the form
   ''' </summary>
   ''' <param name="eventSender">System parm</param>
   ''' <param name="eventArgs">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub btnCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles btnCancel.Click
      Me.Close()
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Validates and saves the user supplied options
   ''' </summary>
   ''' <param name="eventSender">System parm</param>
   ''' <param name="eventArgs">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion/Cleanup
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub btnOK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles btnOK.Click

      Dim _paths As Short

      'validate the number of paths
      Try
         _paths = CShort(txtPathMRUCount.Text)
         If _paths <= 0 OrElse _paths > MAX_STORED_PATHS Then
            MessageBox.Show("Please enter a value between 1 and " & MAX_STORED_PATHS & ".", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
         End If
      Catch ex As Exception
         MessageBox.Show("Please enter a value between 1 and " & MAX_STORED_PATHS & ".", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
         Exit Sub
      End Try

      'Store the values in the globals
      NUM_STORED_PATHS = _paths
      DEFAULT_EDITOR = txtEditor.Text
      'EDITOR_ARG = txtEditorArg.Text
      EDITOR_ARG = txtCmdOptions.Text

      ENDOFLINEMARKER = ""
      If chkCR.CheckState = CheckState.Checked Then ENDOFLINEMARKER = ENDOFLINEMARKER & vbCr
      If chkLF.CheckState = CheckState.Checked Then ENDOFLINEMARKER = ENDOFLINEMARKER & vbLf

      ' Store settings in registry.
      UpdateRegistrySettings()

      ' Only store as many paths as has been set in options.
      With Common.mainForm.cboFilePath

         Do While .Items.Count > NUM_STORED_PATHS
            ' Remove the last item in the list.
            .Items.RemoveAt(.Items.Count - 1)
         Loop

      End With

      Me.Close()

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Loads the current values
   ''' </summary>
   ''' <param name="eventSender">System parm</param>
   ''' <param name="eventArgs">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub frmProperties_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

      ' Initialize the form.
      txtPathMRUCount.Text = CStr(NUM_STORED_PATHS)
      txtEditor.Text = DEFAULT_EDITOR
      'txtEditorArg.Text = EDITOR_ARG
      txtCmdOptions.Text = EDITOR_ARG

      If InStr(ENDOFLINEMARKER, vbCr) > 0 Then
         chkCR.CheckState = CheckState.Checked
      Else
         chkCR.CheckState = CheckState.Unchecked
      End If

      If InStr(ENDOFLINEMARKER, vbLf) > 0 Then
         chkLF.CheckState = CheckState.Checked
      Else
         chkLF.CheckState = CheckState.Unchecked
      End If

      lblCmdOptions.Text = "Command Line Optons:" & vbCrLf & _
                           "  %1 - File" & vbCrLf & _
                           "  %2 - Line Number"

      lblCmdOptionsView.Text = RetrieveCmdLineViewText()
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   ''' Update the command line preview
   ''' </summary>
   ''' <param name="sender">system parm</param>
   ''' <param name="e">system parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	06/13/2005	Created, Better cmd line arg support
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub txtCmdOptions_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCmdOptions.TextChanged
      lblCmdOptionsView.Text = RetrieveCmdLineViewText()
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   ''' Allow the user to select the executable of a file editor
   ''' </summary>
   ''' <param name="sender">system parm</param>
   ''' <param name="e">system parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	06/13/2005	Created, Select file
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
      cdlgOpenFile.Filter = "Executables (*.exe)|*.exe|All Files (*.*)|*.*"
      cdlgOpenFile.Title = "Select the file editor executable"
      cdlgOpenFile.Multiselect = False

      If cdlgOpenFile.ShowDialog = DialogResult.OK Then
         txtEditor.Text = cdlgOpenFile.FileName
      End If
   End Sub
#End Region

#Region "Private Methods"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   ''' Returns a preview of what the command line will look like to open a file
   ''' </summary>
   ''' <returns>Preview of command line</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	06/13/2005	Created, Better cmd line arg support
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Function RetrieveCmdLineViewText() As String
      Dim _text As String = txtCmdOptions.Text

      _text = _text.Replace("%1", "file.txt")
      _text = _text.Replace("%2", "450")

      Return "Preview: editor.exe " & _text
   End Function
#End Region

End Class