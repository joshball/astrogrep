''' -----------------------------------------------------------------------------
''' Project	 : AstroGrep
''' Class	 : frmAbout
''' 
''' -----------------------------------------------------------------------------
''' <summary>
'''   About Dialog
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
''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion/Cleanup
''' </history>
''' -----------------------------------------------------------------------------
Friend Class frmAbout
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
	Public WithEvents picIcon As System.Windows.Forms.PictureBox
	Public WithEvents cmdOK As System.Windows.Forms.Button
   Public WithEvents lblDescription As System.Windows.Forms.Label
	Public WithEvents lblTitle As System.Windows.Forms.Label
   Public WithEvents lblVersion As System.Windows.Forms.Label
	Public WithEvents lblDisclaimer As System.Windows.Forms.Label
   'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
   Friend WithEvents lnkHomePage As System.Windows.Forms.LinkLabel
   <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
      Me.components = New System.ComponentModel.Container
      Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmAbout))
      Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
      Me.picIcon = New System.Windows.Forms.PictureBox
      Me.cmdOK = New System.Windows.Forms.Button
      Me.lblDescription = New System.Windows.Forms.Label
      Me.lblTitle = New System.Windows.Forms.Label
      Me.lblVersion = New System.Windows.Forms.Label
      Me.lblDisclaimer = New System.Windows.Forms.Label
      Me.lnkHomePage = New System.Windows.Forms.LinkLabel
      Me.SuspendLayout()
      '
      'picIcon
      '
      Me.picIcon.BackColor = System.Drawing.SystemColors.Control
      Me.picIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
      Me.picIcon.Cursor = System.Windows.Forms.Cursors.Default
      Me.picIcon.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.picIcon.ForeColor = System.Drawing.SystemColors.ControlText
      Me.picIcon.Image = CType(resources.GetObject("picIcon.Image"), System.Drawing.Image)
      Me.picIcon.Location = New System.Drawing.Point(16, 16)
      Me.picIcon.Name = "picIcon"
      Me.picIcon.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.picIcon.Size = New System.Drawing.Size(32, 32)
      Me.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
      Me.picIcon.TabIndex = 1
      Me.picIcon.TabStop = False
      '
      'cmdOK
      '
      Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
      Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
      Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
      Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.cmdOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
      Me.cmdOK.Location = New System.Drawing.Point(280, 208)
      Me.cmdOK.Name = "cmdOK"
      Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.cmdOK.Size = New System.Drawing.Size(84, 23)
      Me.cmdOK.TabIndex = 0
      Me.cmdOK.Text = "&OK"
      '
      'lblDescription
      '
      Me.lblDescription.BackColor = System.Drawing.SystemColors.Control
      Me.lblDescription.Cursor = System.Windows.Forms.Cursors.Default
      Me.lblDescription.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblDescription.ForeColor = System.Drawing.Color.Black
      Me.lblDescription.Location = New System.Drawing.Point(16, 64)
      Me.lblDescription.Name = "lblDescription"
      Me.lblDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.lblDescription.Size = New System.Drawing.Size(352, 88)
      Me.lblDescription.TabIndex = 3
      Me.lblDescription.Text = "AstroGrep, Copyright (C) 2002 AstroComma Inc. and Theodore L. Ward. AstroGrep com" & _
      "es with ABSOLUTELY NO WARRANTY; for details visit http://www.gnu.org/copyleft/gp" & _
      "l.html This is free software, and you are welcome to redistribute it under certa" & _
      "in conditions; http://www.gnu.org/copyleft/gpl.html#SEC3"
      '
      'lblTitle
      '
      Me.lblTitle.BackColor = System.Drawing.SystemColors.Control
      Me.lblTitle.Cursor = System.Windows.Forms.Cursors.Default
      Me.lblTitle.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.lblTitle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblTitle.ForeColor = System.Drawing.Color.Black
      Me.lblTitle.Location = New System.Drawing.Point(70, 16)
      Me.lblTitle.Name = "lblTitle"
      Me.lblTitle.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.lblTitle.Size = New System.Drawing.Size(290, 24)
      Me.lblTitle.TabIndex = 5
      Me.lblTitle.Text = "Application Title"
      '
      'lblVersion
      '
      Me.lblVersion.BackColor = System.Drawing.SystemColors.Control
      Me.lblVersion.Cursor = System.Windows.Forms.Cursors.Default
      Me.lblVersion.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.lblVersion.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblVersion.Location = New System.Drawing.Point(70, 40)
      Me.lblVersion.Name = "lblVersion"
      Me.lblVersion.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.lblVersion.Size = New System.Drawing.Size(288, 15)
      Me.lblVersion.TabIndex = 6
      Me.lblVersion.Text = "Version"
      '
      'lblDisclaimer
      '
      Me.lblDisclaimer.BackColor = System.Drawing.SystemColors.Control
      Me.lblDisclaimer.Cursor = System.Windows.Forms.Cursors.Default
      Me.lblDisclaimer.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.lblDisclaimer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblDisclaimer.ForeColor = System.Drawing.Color.Black
      Me.lblDisclaimer.Location = New System.Drawing.Point(16, 160)
      Me.lblDisclaimer.Name = "lblDisclaimer"
      Me.lblDisclaimer.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.lblDisclaimer.Size = New System.Drawing.Size(352, 40)
      Me.lblDisclaimer.TabIndex = 4
      Me.lblDisclaimer.Text = "Feel free to contact the author at TedWard@AstroComma.com visit http://www.AstroC" & _
      "omma.com for updates."
      '
      'lnkHomePage
      '
      Me.lnkHomePage.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.lnkHomePage.Location = New System.Drawing.Point(16, 208)
      Me.lnkHomePage.Name = "lnkHomePage"
      Me.lnkHomePage.Size = New System.Drawing.Size(256, 16)
      Me.lnkHomePage.TabIndex = 7
      Me.lnkHomePage.TabStop = True
      Me.lnkHomePage.Text = "AstroGrep Home Page"
      '
      'frmAbout
      '
      Me.AcceptButton = Me.cmdOK
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.BackColor = System.Drawing.SystemColors.Control
      Me.CancelButton = Me.cmdOK
      Me.ClientSize = New System.Drawing.Size(370, 239)
      Me.Controls.Add(Me.lnkHomePage)
      Me.Controls.Add(Me.picIcon)
      Me.Controls.Add(Me.cmdOK)
      Me.Controls.Add(Me.lblDescription)
      Me.Controls.Add(Me.lblTitle)
      Me.Controls.Add(Me.lblVersion)
      Me.Controls.Add(Me.lblDisclaimer)
      Me.Cursor = System.Windows.Forms.Cursors.Default
      Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
      Me.Location = New System.Drawing.Point(156, 129)
      Me.MaximizeBox = False
      Me.MinimizeBox = False
      Me.Name = "frmAbout"
      Me.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.ShowInTaskbar = False
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
      Me.Text = "About MyApp"
      Me.ResumeLayout(False)

   End Sub
#End Region 

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Closes form
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
   Private Sub cmdOK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click
      Me.Close()
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Load values for form
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
   Private Sub frmAbout_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
      Me.Text = "About " & System.Reflection.Assembly.GetExecutingAssembly.GetName.Name
      lblVersion.Text = "Version " & System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileMajorPart & "." & System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileMinorPart & "." & System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileBuildPart
      lblTitle.Text = System.Reflection.Assembly.GetExecutingAssembly.GetName.Name
      lblDescription.Text = "AstroGrep, Copyright (C) 2002 AstroComma Inc. and Theodore L. Ward. AstroGrep comes with ABSOLUTELY NO WARRANTY; for details visit http://www.gnu.org/copyleft/gpl.html This is free software, and you are welcome to redistribute it under certain conditions; http://www.gnu.org/copyleft/gpl.html#SEC3"
      lblDisclaimer.Text = "Created by Theodore Ward and converted to .Net by Curtis Beard"

      'Setup the hyperlink
      lnkHomePage.Links.Add(0, Len(lnkHomePage.Text), "http://astrogrep.sourceforge.net/")
      lnkHomePage.LinkColor = SystemColors.ActiveCaption
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Opens the systems default browser and displays the web link
   ''' </summary>
   ''' <param name="sender">System parm</param>
   ''' <param name="e">System parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub lnkHomePage_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHomePage.LinkClicked
      System.Diagnostics.Process.Start(e.Link.LinkData.ToString())
   End Sub

End Class