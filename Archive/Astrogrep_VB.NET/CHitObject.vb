''' -----------------------------------------------------------------------------
''' Project	 : AstroGrep
''' Class	 : CHitObject
''' 
''' -----------------------------------------------------------------------------
''' <summary>
'''   Hit Object Class
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
''' 	[Curtis_Beard]	   02/02/2005	ADD: last write date of file/hit collection index
''' </history>
''' -----------------------------------------------------------------------------
Friend Class CHitObject

#Region "Declarations"
   Private __fileName As String
   Private __path As String
   Private __lastWriteDate As DateTime
   Private __hitCollectionIndex As Integer

   Private __hitList() As String
   Private __hitLineNumberList() As Integer
   Private __hitListIndex As Integer
#End Region

#Region "Properties"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Gets/Sets the path
   ''' </summary>
   ''' <value>Path</value>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Property Path() As String
      Get
         Return __path
      End Get
      Set(ByVal Value As String)
         __path = Value
      End Set
   End Property

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Gets/Sets the filename
   ''' </summary>
   ''' <value>Filename</value>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Property FileName() As String
      Get
         Return __fileName
      End Get
      Set(ByVal Value As String)
         __fileName = Value
      End Set
   End Property

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Retrieves the number of hits
   ''' </summary>
   ''' <value>Number of hits</value>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public ReadOnly Property GetNumHits() As Integer
      Get
         Return __hitListIndex
      End Get
   End Property

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Retrieves the last write date of file
   ''' </summary>
   ''' <value>Last Write Date of file</value>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   02/02/2005	Initial
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public ReadOnly Property LastWriteDate() As DateTime
      Get
         Return __lastWriteDate
      End Get
   End Property

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Retrieves this hits index in the global hit collection
   ''' </summary>
   ''' <value>Hit collection index</value>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   02/06/2005	Initial
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Property HitCollectionIndex() As Integer
      Get
         Return __hitCollectionIndex
      End Get
      Set(ByVal Value As Integer)
         __hitCollectionIndex = Value
      End Set
   End Property
#End Region

#Region "Public Methods"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Retrieve the entire hit line
   ''' </summary>
   ''' <returns>The entire hit line</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function GetHitString() As String
      Dim _index As Integer
      Dim _hit As String

      For _index = 1 To __hitListIndex
         _hit &= __hitList(_index)
      Next

      Return _hit
   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Retrieve a hit line number
   ''' </summary>
   ''' <param name="Index">Index of hit in hit array</param>
   ''' <returns>A hit line number</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function GetHitLineNumber(ByRef Index As Integer) As Integer

      If Index > __hitListIndex Then Return 0

      Return __hitLineNumberList(Index)

   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Retrieve a hit line
   ''' </summary>
   ''' <param name="Index">Index of hit in hit array</param>
   ''' <returns>A hit line</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function GetHit(ByRef Index As Integer) As String

      If Index > __hitListIndex Then Return String.Empty

      Return __hitList(Index)

   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Add a hit to the hit array
   ''' </summary>
   ''' <param name="LineNum">Line number of hit</param>
   ''' <param name="Line">line of text hit occurred</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub AddHit(ByVal lineNumber As Integer, ByVal line As String)
      ' Ensure we have enough space in our array to store this hit.
      AdjustHitArraySize()

      ' Point the index to the next hit.
      __hitListIndex = __hitListIndex + 1

      ' Store the hit and it's line number.
      __hitList(__hitListIndex) = line
      __hitLineNumberList(__hitListIndex) = lineNumber

   End Sub
#End Region

#Region "Private Methods"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Adjust the hit array size
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub AdjustHitArraySize()
      Dim _listSize As Integer

      _listSize = UBound(__hitList)

      ' If our index is already at the end of our hitlist,
      ' increase the size of the hitlist.
      If __hitListIndex = _listSize Then
         ReDim Preserve __hitList(_listSize + 10)
         ReDim Preserve __hitLineNumberList(_listSize + 10)
      End If

   End Sub
#End Region

#Region "Class Level"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Initialize Class level variables
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub Init()
      ReDim __hitList(10)
      ReDim __hitLineNumberList(10)

      __hitListIndex = 0
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Create a new instance of class
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub New()
      MyBase.New()

      Init()
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Create a new instance of class and set path and filename variables
   ''' </summary>
   ''' <param name="path">Path to search</param>
   ''' <param name="fileName">Filename to search</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   '''   [Curtis_Beard]	   02/02/2005	ADD: last write date of file
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub New(ByVal path As String, ByVal fileName As String)
      MyBase.New()

      Init()

      __path = path
      __fileName = fileName

      Try
         If System.IO.File.Exists(path & fileName) Then
            __lastWriteDate = System.IO.File.GetLastWriteTime(path & fileName)
         Else
            __lastWriteDate = Now
         End If
      Catch ex As Exception
         __lastWriteDate = Now
      End Try

   End Sub
#End Region

End Class