''' -----------------------------------------------------------------------------
''' Project	 : AstroGrep
''' Class	 : CHitObjectCollection
''' 
''' -----------------------------------------------------------------------------
''' <summary>
'''   Hit Object Collection
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
Friend Class CHitObjectCollection
   Implements System.Collections.IEnumerable

#Region "Declarations"
   Private __hitCollection As Collection          'local variable to hold collection
#End Region

#Region "Properties"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Retrieve Item from collection
   ''' </summary>
   ''' <param name="vntIndexKey">Item index</param>
   ''' <value>CHitObject, Nothing on error</value>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As CHitObject
      Get
         Try
            Return CType(__hitCollection.Item(vntIndexKey), CHitObject)
         Catch ex As Exception
            Return Nothing
         End Try
      End Get
   End Property

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Retrieve number of elements in collection
   ''' </summary>
   ''' <value>Number of elements in collection</value>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public ReadOnly Property Count() As Integer
      Get
         Return __hitCollection.Count()
      End Get
   End Property
#End Region

#Region "Methods"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Add a CHitObject to the collection
   ''' </summary>
   ''' <param name="Path">path to search</param>
   ''' <param name="FileName">filename</param>
   ''' <returns>CHitObject that was added</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   '''   [Curtis_Beard]    02/06/2005  ADD: Assign hit object's hit collection index
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function Add(ByRef Path As String, ByRef FileName As String) As CHitObject
      Dim _hitObject As CHitObject

      Try
         _hitObject = New CHitObject(Path, FileName)

         __hitCollection.Add(_hitObject, Path & FileName)

         _hitObject.HitCollectionIndex = __hitCollection.Count

         Return _hitObject
      Catch ex As Exception
         Return Nothing
      End Try

   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Add a CHitObject to the collection
   ''' </summary>
   ''' <param name="hitObject">CHitObject to add</param>
   ''' <returns>CHitObject added</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function AddObject(ByRef hitObject As CHitObject) As CHitObject

      If Not hitObject Is Nothing Then
         __hitCollection.Add(hitObject, hitObject.Path & hitObject.FileName)
      End If

      Return hitObject

   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Remove an element from the collection
   ''' </summary>
   ''' <param name="vntIndexKey">Index of element to remove</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub Remove(ByVal index As Integer)
      __hitCollection.Remove(index)
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Remove an element from the collection
   ''' </summary>
   ''' <param name="key">Key of element to remove</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub Remove(ByVal key As String)
      __hitCollection.Remove(key)
   End Sub
#End Region

#Region "Class Level"
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Initializes local variables of class
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub Init()
      __hitCollection = New Collection
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
   '''   Cleanup of collection
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   '''   [Theodore_Ward]   ??/??/????  Initial
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Private Sub Terminate()
      __hitCollection = Nothing
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Finalize of Class
   ''' </summary>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Protected Overrides Sub Finalize()
      Terminate()

      MyBase.Finalize()
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   '''   Retrieves the enumerator used in the for...each
   ''' </summary>
   ''' <returns>Enumerator for collection</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[Curtis_Beard]	   01/11/2005	.Net Conversion
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
      Return __hitCollection.GetEnumerator
   End Function
#End Region

End Class