Attribute VB_Name = "PrintCode"
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

Option Explicit
Sub PrinterSetup()

    ' Set Cancel to True.
    frmMain.cdlgPrinter.CancelError = True
    On Error GoTo errHandler
    ' Display the Print dialog box.
    frmMain.cdlgPrinter.ShowPrinter
    Exit Sub
errHandler:
    ' User pressed Cancel button.
    Exit Sub
End Sub

Sub PrintFileList(Hits As CHitObjectCollection)
    Dim ho As CHitObject
    Dim numCopies As Long
    
    'Printer.Orientation = frmMain.cdlgPrinter.Orientation
    Printer.Font = frmMain.txtHits.Font

    For numCopies = 1 To frmMain.cdlgPrinter.Copies
        For Each ho In Hits
            Printer.Print ho.Path + ho.FileName
        Next
    Next
    Printer.EndDoc
End Sub

Sub PrintSelectedItems(lstBox As ListBox, Hits As CHitObjectCollection)
    Dim ho As CHitObject
    Dim l As Long
    Dim i As Integer
    Dim numCopies As Long

    'Printer.Orientation = frmMain.cdlgPrinter.Orientation
    Printer.Font = frmMain.txtHits.Font

    For numCopies = 1 To frmMain.cdlgPrinter.Copies
        For i = 0 To lstBox.ListCount - 1
            If lstBox.Selected(i) Then
                Set ho = Hits(i + 1)
                Printer.Print ho.Path + ho.FileName
                For l = 1 To ho.GetNumHits
                    PrintHit ho.GetHit(l)
                Next l
            End If
        Next
    Next
    Printer.EndDoc
End Sub

Sub PrintAllHits(Hits As CHitObjectCollection)
    Dim ho As CHitObject
    Dim l As Long
    Dim numCopies As Long
    
    'Printer.Orientation = frmMain.cdlgPrinter.Orientation
    Printer.Font = frmMain.txtHits.Font

    For numCopies = 1 To frmMain.cdlgPrinter.Copies
        For Each ho In Hits
            Printer.Print ho.Path + ho.FileName
            For l = 1 To ho.GetNumHits
                PrintHit ho.GetHit(1)
            Next l
        Next
    Next
    Printer.EndDoc
End Sub

Sub PrintSingleItem(ho As CHitObject)
    Dim l As Long
    Dim numCopies As Long

    'Printer.Orientation = frmMain.cdlgPrinter.Orientation
    Printer.Font = frmMain.txtHits.Font
    For numCopies = 1 To frmMain.cdlgPrinter.Copies
        Printer.Print ho.Path + ho.FileName
        For l = 1 To ho.GetNumHits
            PrintHit ho.GetHit(l)
        Next l
    Next
    Printer.EndDoc
End Sub

Sub PrintHit(hit As String)
    ' Remove the CR/LF at the end of each hit.
    If Right$(hit, 2) = vbCrLf Then
        Printer.Print Left$(hit, Len(hit) - 2)
    Else
        Printer.Print hit
    End If
End Sub
