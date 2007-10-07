''' -----------------------------------------------------------------------------
''' Project	 : AstroGrep
''' Class	 : frmCancel
''' 
''' -----------------------------------------------------------------------------
''' <summary>
'''   Displays a cancel form
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
Friend Class frmCancel
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
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents lblDirectory As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents lblSearch As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
      Me.components = New System.ComponentModel.Container
      Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
      Me.btnCancel = New System.Windows.Forms.Button
      Me.Label3 = New System.Windows.Forms.Label
      Me.lblDirectory = New System.Windows.Forms.Label
      Me.Label1 = New System.Windows.Forms.Label
      Me.lblSearch = New System.Windows.Forms.Label
      Me.SuspendLayout()
      '
      'btnCancel
      '
      Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
      Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Default
      Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
      Me.btnCancel.Location = New System.Drawing.Point(208, 64)
      Me.btnCancel.Name = "btnCancel"
      Me.btnCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.btnCancel.Size = New System.Drawing.Size(65, 33)
      Me.btnCancel.TabIndex = 0
      Me.btnCancel.Text = "&Cancel"
      '
      'Label3
      '
      Me.Label3.AutoSize = True
      Me.Label3.BackColor = System.Drawing.SystemColors.Control
      Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
      Me.Label3.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
      Me.Label3.Location = New System.Drawing.Point(8, 32)
      Me.Label3.Name = "Label3"
      Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.Label3.Size = New System.Drawing.Size(25, 16)
      Me.Label3.TabIndex = 4
      Me.Label3.Text = "File:"
      '
      'lblDirectory
      '
      Me.lblDirectory.BackColor = System.Drawing.SystemColors.Window
      Me.lblDirectory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
      Me.lblDirectory.Cursor = System.Windows.Forms.Cursors.Default
      Me.lblDirectory.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.lblDirectory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblDirectory.ForeColor = System.Drawing.SystemColors.WindowText
      Me.lblDirectory.Location = New System.Drawing.Point(40, 8)
      Me.lblDirectory.Name = "lblDirectory"
      Me.lblDirectory.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.lblDirectory.Size = New System.Drawing.Size(433, 17)
      Me.lblDirectory.TabIndex = 3
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
      Me.Label1.Size = New System.Drawing.Size(30, 16)
      Me.Label1.TabIndex = 2
      Me.Label1.Text = "Path:"
      '
      'lblSearch
      '
      Me.lblSearch.BackColor = System.Drawing.SystemColors.Window
      Me.lblSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
      Me.lblSearch.Cursor = System.Windows.Forms.Cursors.Default
      Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.lblSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblSearch.ForeColor = System.Drawing.SystemColors.WindowText
      Me.lblSearch.Location = New System.Drawing.Point(40, 32)
      Me.lblSearch.Name = "lblSearch"
      Me.lblSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.lblSearch.Size = New System.Drawing.Size(433, 17)
      Me.lblSearch.TabIndex = 1
      '
      'frmCancel
      '
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.BackColor = System.Drawing.SystemColors.Control
      Me.ClientSize = New System.Drawing.Size(484, 108)
      Me.ControlBox = False
      Me.Controls.Add(Me.btnCancel)
      Me.Controls.Add(Me.Label3)
      Me.Controls.Add(Me.lblDirectory)
      Me.Controls.Add(Me.Label1)
      Me.Controls.Add(Me.lblSearch)
      Me.Cursor = System.Windows.Forms.Cursors.Arrow
      Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.Location = New System.Drawing.Point(4, 23)
      Me.Name = "frmCancel"
      Me.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
      Me.Text = "Searching..."
      Me.ResumeLayout(False)

   End Sub
#End Region 

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Cancel Button Click
   ''' </summary>
   ''' <param name="eventSender">system parm</param>
   ''' <param name="eventArgs">system parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub btnCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles btnCancel.Click
      GB_CANCEL = True
   End Sub
End Class