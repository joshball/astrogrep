''' -----------------------------------------------------------------------------
''' <summary>
'''   Print Routines
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
''' ''' -----------------------------------------------------------------------------
Module PrintCode

#Region "Declarations"
   Private __document As String
   Private Const PRINT_DOC As String = "prt.txt"
#End Region

#Region "Public Methods"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Print the file names only
   ''' </summary>
   ''' <param name="Hits">Hits Collection</param>
   ''' <returns>Document to print</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function PrintFileList(ByVal Hits As CHitObjectCollection) As String ', ByVal printSettings As Printing.PrinterSettings) As String
      Dim ho As CHitObject

      SetupDocument("AstroGrep File List")

      AddLine("----------------------------------------------------------------------")

      For Each ho In Hits
         AddLine(ho.Path & ho.FileName)
         AddLine("----------------------------------------------------------------------")
      Next ho

      Return __document
   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Print selected hits
   ''' </summary>
   ''' <param name="lstBox">List Box of selections</param>
   ''' <param name="Hits">Hits Collection</param>
   ''' <returns>Document to print</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function PrintSelectedItems(ByVal lstBox As System.Windows.Forms.ListView, ByVal Hits As CHitObjectCollection) As String ', ByVal printSettings As Printing.PrinterSettings)
      Dim _hitObject As CHitObject
      Dim _internalIndex As Integer
      Dim _index As Integer
      Dim _hitIndex As Integer

      SetupDocument("AstroGrep Selected Hits")

      For _index = 0 To lstBox.SelectedItems.Count - 1

         _hitIndex = CInt(lstBox.SelectedItems(_index).SubItems(3).Text)
         '_hitObject = Hits(_index + 1)
         _hitObject = Hits(_hitIndex)

         AddLine("----------------------------------------------------------------------")
         AddLine(_hitObject.Path & _hitObject.FileName)
         AddLine("----------------------------------------------------------------------")

         For _internalIndex = 1 To _hitObject.GetNumHits
            PrintHit(_hitObject.GetHit(_internalIndex))
         Next

         AddLine("")

      Next

      Return __document
   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Print all the hits
   ''' </summary>
   ''' <param name="Hits">Hits Collection</param>
   ''' <returns>Document to print</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function PrintAllHits(ByVal Hits As CHitObjectCollection) As String ', ByVal printSettings As Printing.PrinterSettings)
      Dim _hitObject As CHitObject
      Dim _index As Integer

      SetupDocument("AstroGrep All Hits")

      For Each _hitObject In Hits

         AddLine("----------------------------------------------------------------------")
         AddLine(_hitObject.Path & _hitObject.FileName)
         AddLine("----------------------------------------------------------------------")

         For _index = 1 To _hitObject.GetNumHits
            PrintHit(_hitObject.GetHit(_index))
         Next

         AddLine("")

      Next

      Return __document
   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Print a single hit item
   ''' </summary>
   ''' <param name="ho">Hit to print</param>
   ''' <returns>Document to print</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function PrintSingleItem(ByVal ho As CHitObject) As String ', ByVal printSettings As Printing.PrinterSettings)
      Dim _index As Integer

      SetupDocument("AstroGrep Hit")

      AddLine("----------------------------------------------------------------------")
      AddLine(ho.Path & ho.FileName)
      AddLine("----------------------------------------------------------------------")

      For _index = 1 To ho.GetNumHits
         PrintHit(ho.GetHit(_index))
      Next

      Return __document
   End Function
#End Region

#Region "Private Methods"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Print a single hit
   ''' </summary>
   ''' <param name="hit">Hit to print</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub PrintHit(ByRef hit As String)

      ' Remove the CR/LF at the end of each hit.
      If Right(hit, 2) = vbCrLf Then
         AddLine(Left(hit, Len(hit) - 2))
      Else
         AddLine(hit)
      End If

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Setup the document
   ''' </summary>
   ''' <param name="header">Optional - Header of document</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub SetupDocument(Optional ByVal header As String = "")
      __document = String.Empty
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Add the line to the document
   ''' </summary>
   ''' <param name="line">Line to add</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub AddLine(ByVal line As String)
      __document += line & vbCrLf
   End Sub
#End Region

End Module