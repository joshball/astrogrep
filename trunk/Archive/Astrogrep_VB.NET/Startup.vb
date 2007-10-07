''' -----------------------------------------------------------------------------
''' <summary>
'''   Startup for application - used to apply xp style themes
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Curtis_Beard]	01/27/2005	.Net Conversion/Support for xp themes
''' </history>
''' -----------------------------------------------------------------------------
Module Startup
   Public Sub Main()
      Application.EnableVisualStyles()
      Application.DoEvents()

      Dim clientmainForm As New frmMain
      Common.mainForm = clientmainForm
      Application.Run(Common.mainForm)
   End Sub
End Module
