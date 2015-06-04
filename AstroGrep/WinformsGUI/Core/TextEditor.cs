using System;

using AstroGrep.Common;

namespace AstroGrep
{
   /// <summary>
   /// Represents a Text Editor application.
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
   /// [Curtis_Beard]		07/21/2006	Created
   /// [Curtis_Beard]		10/12/2012	ADD: tab size
   /// [Curtis_Beard]		03/06/2015	CHG: make delimeter a private constant
   /// </history>
   public class TextEditor
   {
      #region Declarations
      private const string DELIMETER = "|@@|";

      private string fileType = string.Empty;
      private string editor = string.Empty;
      private string arguments = string.Empty;
      private int tabSize = 0;
      private bool useQuotesAroundFileName = true; 
      #endregion

      #region Constructors
      /// <summary>
      /// Initializes a new instance of the TextEditor class.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      public TextEditor()
      {

      }

      /// <summary>
      /// Initializes a new instance of the TextEditor class.
      /// </summary>
      /// <param name="editorPath">Text Editor Path</param>
      /// <param name="editorArgs">Text Editor Command Line Arguments</param>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      public TextEditor(string editorPath, string editorArgs)
      {
         editor = editorPath;
         arguments = editorArgs;
      }

      /// <summary>
      /// Initializes a new instance of the TextEditor class.
      /// </summary>
      /// <param name="fileType">Text Editor File Type</param>
      /// <param name="editorPath">Text Editor Path</param>
      /// <param name="editorArgs">Text Editor Command Line Arguments</param>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      public TextEditor(string fileType, string editorPath, string editorArgs)
         : this(editorPath, editorArgs)
      {
         this.fileType = fileType;
      }

      /// <summary>
      /// Initializes a new instance of the TextEditor class.
      /// </summary>
      /// <param name="fileType">Text Editor File Type</param>
      /// <param name="editorPath">Text Editor Path</param>
      /// <param name="editorArgs">Text Editor Command Line Arguments</param>
      /// <param name="tabSize">Tab Size for Text Editor</param>
      /// <history>
      /// [Curtis_Beard]		10/12/2012	Created
      /// </history>
      public TextEditor(string fileType, string editorPath, string editorArgs, int tabSize)
         : this(fileType, editorPath, editorArgs)
      {
         this.tabSize = tabSize;
      }

      /// <summary>
      /// Initializes a new instance of the TextEditor class.
      /// </summary>
      /// <param name="fileType">Text Editor File Type</param>
      /// <param name="editorPath">Text Editor Path</param>
      /// <param name="editorArgs">Text Editor Command Line Arguments</param>
      /// <param name="tabSize">Tab Size for Text Editor</param>
      /// <param name="useQuotesAroundFileName">Use quotes around file name</param>
      /// <history>
      /// [Curtis_Beard]		03/06/2015	FIX: 65, add boolean for using quotes
      /// </history>
      public TextEditor(string fileType, string editorPath, string editorArgs, int tabSize, bool useQuotesAroundFileName)
         : this(fileType, editorPath, editorArgs, tabSize)
      {
         this.useQuotesAroundFileName = useQuotesAroundFileName;
      }
      #endregion

      #region Properties
      /// <summary>
      /// Contains the file type.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      public string FileType
      {
         get { return fileType; }
         set { fileType = value; }
      }

      /// <summary>
      /// Contains the location.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      public string Editor
      {
         get { return editor; }
         set { editor = value; }
      }

      /// <summary>
      /// Contains the command line arguments.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      public string Arguments
      {
         get { return arguments; }
         set { arguments = value; }
      }

      /// <summary>
      /// Contains the editor's tab size.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		10/12/2012	Created
      /// </history>
      public int TabSize
      {
         get { return tabSize; }
         set { tabSize = value; }
      }

      /// <summary>
      /// Determines whether to wrap the file name with quotes.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		03/06/2015	FIX: 65, handle file path quotes separately
      /// </history>
      public bool UseQuotesAroundFileName
      {
         get { return useQuotesAroundFileName; }
         set { useQuotesAroundFileName = value; }
      }
      #endregion

      #region public Methods
      /// <summary>
      /// Gets the string representation of this class.
      /// </summary>
      /// <returns></returns>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// [Curtis_Beard]		10/12/2012	ADD: tab size
      /// [Curtis_Beard]		03/06/2015	FIX: 65, add boolean for using quotes
      /// </history>
      public override string ToString()
      {
         return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}", DELIMETER, editor, arguments, fileType, tabSize, useQuotesAroundFileName);
      }

      /// <summary>
      /// Translates the given string representation into the class/s properties.
      /// </summary>
      /// <param name="classAsString">The string representation of this class</param>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// [Curtis_Beard]		10/12/2012	ADD: tab size
      /// [Curtis_Beard]		03/06/2015	FIX: 65, add boolean for using quotes
      /// [Curtis_Beard]		04/07/2015	CHG: fix issue with length checks so that all values are processed if found
      /// </history>
      public static TextEditor FromString(string classAsString)
      {
         TextEditor editor = new TextEditor();

         if (classAsString.Length > 0 && classAsString.IndexOf(DELIMETER) > -1)
         {
            string[] values = Utils.SplitByString(classAsString, DELIMETER);

            if (values.Length >= 3)
            {
               editor.Editor = values[0];
               editor.Arguments = values[1];
               editor.FileType = values[2];
            }
            
            if (values.Length >= 4)
            {
               int size = 0;
               if (int.TryParse(values[3], out size))
               {
                  editor.TabSize = size;
               }
            }
            
            if (values.Length >= 5)
            {
               bool useQuotes = true;
               if (bool.TryParse(values[4], out useQuotes))
               {
                  editor.UseQuotesAroundFileName = useQuotes;
               }
            }
         }

         return editor;
      }
      #endregion
   }
}
