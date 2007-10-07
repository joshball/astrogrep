Imports System.Drawing.Printing

''' -----------------------------------------------------------------------------
''' Project	 : AstroGrep
''' Class	 : frmPrint
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' 
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Curtis_Beard]	02/02/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class frmPrint
   Inherits System.Windows.Forms.Form

#Region "Declarations"
   Private WithEvents pdoc As New PrintDocument
   Private __document As String = String.Empty
#End Region

#Region " Windows Form Designer generated code "

   Public Sub New()
      MyBase.New()

      'This call is required by the Windows Form Designer.
      InitializeComponent()

      'Add any initialization after the InitializeComponent() call

   End Sub

   'Form overrides dispose to clean up the component list.
   Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
      If disposing Then
         If Not (components Is Nothing) Then
            components.Dispose()
         End If
      End If
      MyBase.Dispose(disposing)
   End Sub

   'Required by the Windows Form Designer
   Private components As System.ComponentModel.IContainer

   'NOTE: The following procedure is required by the Windows Form Designer
   'It can be modified using the Windows Form Designer.  
   'Do not modify it using the code editor.
   Friend WithEvents lstPrintTypes As System.Windows.Forms.ListBox
   Friend WithEvents cmdPreview As System.Windows.Forms.Button
   Friend WithEvents cmdPrint As System.Windows.Forms.Button
   Friend WithEvents cmdPageSetup As System.Windows.Forms.Button
   Friend WithEvents lblSelect As System.Windows.Forms.Label
   Friend WithEvents cmdCancel As System.Windows.Forms.Button
   <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
      Me.lstPrintTypes = New System.Windows.Forms.ListBox
      Me.cmdPreview = New System.Windows.Forms.Button
      Me.cmdPrint = New System.Windows.Forms.Button
      Me.cmdPageSetup = New System.Windows.Forms.Button
      Me.lblSelect = New System.Windows.Forms.Label
      Me.cmdCancel = New System.Windows.Forms.Button
      Me.SuspendLayout()
      '
      'lstPrintTypes
      '
      Me.lstPrintTypes.Location = New System.Drawing.Point(8, 32)
      Me.lstPrintTypes.Name = "lstPrintTypes"
      Me.lstPrintTypes.Size = New System.Drawing.Size(352, 108)
      Me.lstPrintTypes.TabIndex = 0
      '
      'cmdPreview
      '
      Me.cmdPreview.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.cmdPreview.Location = New System.Drawing.Point(128, 152)
      Me.cmdPreview.Name = "cmdPreview"
      Me.cmdPreview.TabIndex = 1
      Me.cmdPreview.Text = "Pre&view"
      '
      'cmdPrint
      '
      Me.cmdPrint.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.cmdPrint.Location = New System.Drawing.Point(48, 152)
      Me.cmdPrint.Name = "cmdPrint"
      Me.cmdPrint.TabIndex = 2
      Me.cmdPrint.Text = "&Print"
      '
      'cmdPageSetup
      '
      Me.cmdPageSetup.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.cmdPageSetup.Location = New System.Drawing.Point(208, 152)
      Me.cmdPageSetup.Name = "cmdPageSetup"
      Me.cmdPageSetup.TabIndex = 3
      Me.cmdPageSetup.Text = "Page &Setup"
      '
      'lblSelect
      '
      Me.lblSelect.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.lblSelect.Location = New System.Drawing.Point(8, 8)
      Me.lblSelect.Name = "lblSelect"
      Me.lblSelect.Size = New System.Drawing.Size(216, 16)
      Me.lblSelect.TabIndex = 4
      Me.lblSelect.Text = "Please select the output type:"
      '
      'cmdCancel
      '
      Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
      Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.cmdCancel.Location = New System.Drawing.Point(288, 152)
      Me.cmdCancel.Name = "cmdCancel"
      Me.cmdCancel.TabIndex = 5
      Me.cmdCancel.Text = "&Cancel"
      '
      'frmPrint
      '
      Me.AcceptButton = Me.cmdPrint
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.CancelButton = Me.cmdCancel
      Me.ClientSize = New System.Drawing.Size(370, 184)
      Me.Controls.Add(Me.cmdCancel)
      Me.Controls.Add(Me.lblSelect)
      Me.Controls.Add(Me.cmdPageSetup)
      Me.Controls.Add(Me.cmdPrint)
      Me.Controls.Add(Me.cmdPreview)
      Me.Controls.Add(Me.lstPrintTypes)
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
      Me.MaximizeBox = False
      Me.MinimizeBox = False
      Me.Name = "frmPrint"
      Me.ShowInTaskbar = False
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
      Me.Text = "Print"
      Me.ResumeLayout(False)

   End Sub

#End Region

#Region "Form Events"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Form Load Event
   ''' </summary>
   ''' <param name="sender">system parm</param>
   ''' <param name="e">system parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	02/02/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub frmPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

      'load the list of types to print
      lstPrintTypes.Items.Add("Selected Items")
      lstPrintTypes.Items.Add("Current Hit")
      lstPrintTypes.Items.Add("All Hits")
      lstPrintTypes.Items.Add("File List")

      'Set the first item as selected
      lstPrintTypes.SelectedIndex = 0

      'Set the default document settings
      pdoc.DefaultPageSettings.Margins.Left = 25
      pdoc.DefaultPageSettings.Margins.Top = 25
      pdoc.DefaultPageSettings.Margins.Bottom = 25
      pdoc.DefaultPageSettings.Margins.Right = 25
   End Sub
#End Region

#Region "Button Click Events"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Cancel Button Event
   ''' </summary>
   ''' <param name="sender">system parm</param>
   ''' <param name="e">system parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	02/02/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
      Me.Close()
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Page Setup Button Event
   ''' </summary>
   ''' <param name="sender">system parm</param>
   ''' <param name="e">system parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	02/02/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub cmdPageSetup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPageSetup.Click
      Dim psd As New PageSetupDialog

      Try
         With psd
            .Document = pdoc
            .PageSettings = pdoc.DefaultPageSettings

            'Set default margins to .25 inches
            .PageSettings.Margins.Left = 25
            .PageSettings.Margins.Top = 25
            .PageSettings.Margins.Bottom = 25
            .PageSettings.Margins.Right = 25
         End With

         If psd.ShowDialog = DialogResult.OK Then
            pdoc.DefaultPageSettings = psd.PageSettings
         End If
      Catch ex As Exception
         MessageBox.Show("An error occurred while trying to load the " & _
             "Page Settings. Make sure you currently have " & _
             "access to a printer. A printer must be connected and " & _
             "accessible for Page Settings to work.", Me.Text, _
              MessageBoxButtons.OK, MessageBoxIcon.Error)
      End Try
      
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Print Preview Button Event
   ''' </summary>
   ''' <param name="sender">system parm</param>
   ''' <param name="e">system parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	02/02/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub cmdPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPreview.Click
      Dim ppd As New PrintPreviewDialog
      Try
         SetDocument()
         ppd.Document = pdoc

         'Set properties of preview dialog
         ppd.StartPosition = FormStartPosition.CenterScreen
         ppd.Size = New Size(640, 480)
         ppd.FormBorderStyle = FormBorderStyle.FixedDialog

         'set initial zoom level to 100%
         ppd.PrintPreviewControl.Zoom = 1.0

         ppd.ShowDialog(Common.mainForm)
      Catch exp As Exception
         MessageBox.Show("An error occurred while trying to load the " & _
             "document for Print Preview. Make sure you currently have " & _
             "access to a printer. A printer must be connected and " & _
             "accessible for Print Preview to work.", Me.Text, _
              MessageBoxButtons.OK, MessageBoxIcon.Error)
      End Try
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Print Button Event
   ''' </summary>
   ''' <param name="sender">system parm</param>
   ''' <param name="e">system parm</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	02/02/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub cmdPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
      Dim dialog As New PrintDialog

      Try
         SetDocument()
         dialog.Document = pdoc

         If dialog.ShowDialog = DialogResult.OK Then
            pdoc.Print()
         End If
      Catch ex As Exception
         MessageBox.Show("An error occurred while trying to load the " & _
             "document for Printing. Make sure you currently have " & _
             "access to a printer. A printer must be connected and " & _
             "accessible for Printing to work.", Me.Text, _
              MessageBoxButtons.OK, MessageBoxIcon.Error)
      End Try

   End Sub
#End Region

#Region "PrintDocument Events"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   PrintPage Event
   ''' </summary>
   ''' <param name="sender">system parm</param>
   ''' <param name="e">system parm</param>
   ''' <remarks>
   '''   PrintPage is the foundational printing event. This event gets fired for every 
   '''   page that will be printed. You could also handle the BeginPrint and EndPrint
   '''   events for more control.
   '''   
   '''   The following is very 
   '''   fast and useful for plain text as MeasureString calculates the text that
   '''   can be fitted on an entire page. This is not that useful, however, for 
   '''   formatted text. In that case you would want to have word-level (vs page-level)
   '''   control, which is more complicated.
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	02/02/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub pdoc_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pdoc.PrintPage
      ' Declare a variable to hold the position of the last printed char. Declare
      ' as static so that subsequent PrintPage events can reference it.
      Static intCurrentChar As Int32
      ' Initialize the font to be used for printing.
      'Dim font As New Font("Microsoft Sans Serif", 24)
      Dim font As New Font(Common.mainForm.txtHits.Font.Name, Common.mainForm.txtHits.Font.Size)

      Dim intPrintAreaHeight, intPrintAreaWidth, marginLeft, marginTop As Int32
      With pdoc.DefaultPageSettings
         ' Initialize local variables that contain the bounds of the printing 
         ' area rectangle.
         intPrintAreaHeight = .PaperSize.Height - .Margins.Top - .Margins.Bottom
         intPrintAreaWidth = .PaperSize.Width - .Margins.Left - .Margins.Right

         ' Initialize local variables to hold margin values that will serve
         ' as the X and Y coordinates for the upper left corner of the printing 
         ' area rectangle.
         marginLeft = .Margins.Left ' X coordinate
         marginTop = .Margins.Top ' Y coordinate
      End With

      ' If the user selected Landscape mode, swap the printing area height 
      ' and width.
      If pdoc.DefaultPageSettings.Landscape Then
         Dim intTemp As Int32
         intTemp = intPrintAreaHeight
         intPrintAreaHeight = intPrintAreaWidth
         intPrintAreaWidth = intTemp
      End If

      ' Calculate the total number of lines in the document based on the height of
      ' the printing area and the height of the font.
      Dim intLineCount As Int32 = CInt(intPrintAreaHeight / font.Height)
      ' Initialize the rectangle structure that defines the printing area.
      Dim rectPrintingArea As New RectangleF(marginLeft, marginTop, intPrintAreaWidth, intPrintAreaHeight)

      ' Instantiate the StringFormat class, which encapsulates text layout 
      ' information (such as alignment and line spacing), display manipulations 
      ' (such as ellipsis insertion and national digit substitution) and OpenType 
      ' features. Use of StringFormat causes MeasureString and DrawString to use
      ' only an integer number of lines when printing each page, ignoring partial
      ' lines that would otherwise likely be printed if the number of lines per 
      ' page do not divide up cleanly for each page (which is usually the case).
      ' See further discussion in the SDK documentation about StringFormatFlags.
      Dim fmt As New StringFormat(StringFormatFlags.LineLimit)
      ' Call MeasureString to determine the number of characters that will fit in
      ' the printing area rectangle. The CharFitted Int32 is passed ByRef and used
      ' later when calculating intCurrentChar and thus HasMorePages. LinesFilled 
      ' is not needed for this sample but must be passed when passing CharsFitted.
      ' Mid is used to pass the segment of remaining text left off from the 
      ' previous page of printing (recall that intCurrentChar was declared as 
      ' static.
      Dim intLinesFilled, intCharsFitted As Int32
      e.Graphics.MeasureString(Mid(__document, intCurrentChar + 1), font, _
                  New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, _
                  intCharsFitted, intLinesFilled)

      ' Print the text to the page.
      e.Graphics.DrawString(Mid(__document, intCurrentChar + 1), font, _
          Brushes.Black, rectPrintingArea, fmt)

      ' Advance the current char to the last char printed on this page. As 
      ' intCurrentChar is a static variable, its value can be used for the next
      ' page to be printed. It is advanced by 1 and passed to Mid() to print the
      ' next page (see above in MeasureString()).
      intCurrentChar += intCharsFitted

      ' HasMorePages tells the printing module whether another PrintPage event
      ' should be fired.
      If intCurrentChar < __document.Length Then
         e.HasMorePages = True
      Else
         e.HasMorePages = False
         ' You must explicitly reset intCurrentChar as it is static.
         intCurrentChar = 0
      End If
   End Sub
#End Region

#Region "Private Methods"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Set the document to print
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	02/02/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub SetDocument()

      Select Case lstPrintTypes.SelectedIndex
         Case 0
            __document = PrintSelectedItems(Common.mainForm.lstFileNames, G_HITS)
         Case 1
            If Common.mainForm.lstFileNames.SelectedItems.Count > 0 Then
               Dim _index As Integer = CInt(Common.mainForm.lstFileNames.SelectedItems(0).SubItems(3).Text)
               __document = PrintSingleItem(G_HITS(_index))
            End If
         Case 2
               __document = PrintAllHits(G_HITS)
         Case 3
               __document = PrintFileList(G_HITS)
         Case Else
               __document = String.Empty
      End Select

   End Sub
#End Region

End Class