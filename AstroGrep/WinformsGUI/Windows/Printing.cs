using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using libAstroGrep;

namespace AstroGrep.Windows
{
   /// <summary>
   /// Printing routines based on passed in listView and HashTable containing grep
   /// hit information.
   /// </summary>
   /// <remarks>
   ///   AstroGrep File Searching Utility. Written by Theodore L. Ward
   ///   Copyright (C) 2002 AstroComma Incorporated.
   ///   
   ///   This program is free software; you can redistribute it and/or
   ///   modify it under the terms of the GNU General public License
   ///   as published by the Free Software Foundation; either version 2
   ///   of the License, or (at your option) any later version.
   ///   
   ///   This program is distributed in the hope that it will be useful,
   ///   but WITHOUT ANY WARRANTY; without even the implied warranty of
   ///   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   ///   GNU General public License for more details.
   ///   
   ///   You should have received a copy of the GNU General public License
   ///   along with this program; if not, write to the Free Software
   ///   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
   /// 
   ///   The author may be contacted at:
   ///   ted@astrocomma.com or curtismbeard@gmail.com
   /// </remarks>
   /// <history>
   ///   [Theodore_Ward]   ??/??/????  Initial
   /// 	[Curtis_Beard]	   01/11/2005	.Net Conversion/Comments/Option Strict
   /// 	[Curtis_Beard]	   09/10/2005	CHG: to class, pass in information
   /// 	[Curtis_Beard]	   07/19/2006	CHG: Apply correct namespace and reformat comments
   /// 	[Curtis_Beard]	   11/14/2014	CHG: Use StringBuilder instead of string for storing document contents
   /// </history>
   public class GrepPrint
   {
      #region Declarations
      private StringBuilder documentBuilder = new StringBuilder();
      private ListView __listView;
      private IList<HitObject> __grepTable;
      #endregion

      #region Public Methods
      /// <summary>
      /// Initializes a new instance of the GrepPrint class.
      /// </summary>
      /// <param name="fileList">ListView containing the files.</param>
      /// <param name="greps">Hashtable containing HitObjects</param>
      /// <history>
      /// [Curtis_Beard]	   09/10/2005	Created
      /// </history>
      public GrepPrint(ListView fileList, IList<HitObject> greps)
      {
         __listView = fileList;
         __grepTable = greps;
      }

      /// <summary>
      /// Print the file names only
      /// </summary>
      /// <returns>Document to print</returns>
      /// <history>
      /// [Theodore_Ward]   ??/??/????  Initial
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// </history>
      public string PrintFileList()
      {
         SetupDocument(string.Empty);

         AddLine("----------------------------------------------------------------------");

         foreach (var _hit in __grepTable)
         {
            AddLine(_hit.FilePath);
            AddLine("----------------------------------------------------------------------");
         }

         return GetDocument();
      }

      /// <summary>
      /// Print selected hits
      /// </summary>
      /// <returns>Document to print</returns>
      /// <history>
      /// [Theodore_Ward]   ??/??/????  Initial
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// [Curtis_Beard]	   11/01/2005	CHG: Get correct hit object index
      /// [Curtis_Beard]      11/14/2014	CHG: use correct index variable when printing hit lines 
      /// </history>
      public string PrintSelectedItems()
      {
         HitObject _hit;

         SetupDocument(string.Empty);

         for (int _index = 0; _index < __listView.SelectedItems.Count; _index++)
         {

            _hit = __grepTable[int.Parse(__listView.SelectedItems[_index].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text)];

            AddLine("----------------------------------------------------------------------");
            AddLine(_hit.FilePath);
            AddLine("----------------------------------------------------------------------");

            for (int _internalIndex = 0; _internalIndex < _hit.LineCount; _internalIndex++)
               PrintHit(string.Format("{0}{1}", _hit.RetrieveSpacerText(_internalIndex), _hit.RetrieveLine(_internalIndex)));

            AddLine("");
         }

         return GetDocument();
      }

      /// <summary>
      ///   Print all the hits
      /// </summary>
      /// <returns>Document to print</returns>
      /// <history>
      ///   [Theodore_Ward]   ??/??/????  Initial
      /// 	[Curtis_Beard]	   01/11/2005	.Net Conversion
      /// </history>
      public string PrintAllHits()
      {
         SetupDocument(string.Empty);

         foreach (var _hit in __grepTable)
         {
            AddLine("----------------------------------------------------------------------");
            AddLine(_hit.FilePath);
            AddLine("----------------------------------------------------------------------");

            for (int _index = 0; _index < _hit.LineCount; _index++)
               PrintHit(string.Format("{0}{1}", _hit.RetrieveSpacerText(_index), _hit.RetrieveLine(_index)));

            AddLine("");
         }

         return GetDocument();
      }
      #endregion

      #region Private Methods
      /// <summary>
      ///   Print a single hit
      /// </summary>
      /// <param name="hit">Hit to print</param>
      /// <history>
      ///   [Theodore_Ward]   ??/??/????  Initial
      /// 	[Curtis_Beard]	   01/11/2005	.Net Conversion
      /// </history>
      private void PrintHit(string hit)
      {
         // Remove the CR/LF at the end of each hit.
         if (hit.EndsWith("\r\n"))
            AddLine(hit.Substring(0, hit.Length - 2));
         else
            AddLine(hit);
      }

      /// <summary>
      ///   Setup the document
      /// </summary>
      /// <param name="header">Optional - Header of document</param>
      /// <history>
      /// 	[Curtis_Beard]	   01/11/2005	.Net Conversion
      /// </history>
      private void SetupDocument(string header)
      {
         documentBuilder.Clear();

         if (!string.IsNullOrEmpty(header))
         {
            documentBuilder.AppendLine(header);
         }
      }

      /// <summary>
      ///   Add the line to the document
      /// </summary>
      /// <param name="line">Line to add</param>
      /// <history>
      /// 	[Curtis_Beard]	   01/11/2005	.Net Conversion
      /// </history>
      private void AddLine(string line)
      {
         documentBuilder.AppendLine(line);
      }

      /// <summary>
      /// Retrieves all the contents of the document.
      /// </summary>
      /// <returns>string of all contents</returns>
      /// <history>
      /// [Curtis_Beard]	   11/14/2014	Initial
      /// </history>
      private string GetDocument()
      {
         return documentBuilder.ToString();
      }
      #endregion
   }
}
