using System;

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
    /// </history>
    public class TextEditor
    {
        #region Declarations
        /// <summary>Delimiter for separating the properties in string form</summary>
        public const string DELIMETER = "|@@|";
        private string __type = string.Empty;
        private string __editor = string.Empty;
        private string __editorArgs = string.Empty;
        private int __tabSize = 0;
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
            __editor = editorPath;
            __editorArgs = editorArgs;
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
            __type = fileType;
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
            __tabSize = tabSize;
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
            get { return __type; }
            set { __type = value; }
        }

        /// <summary>
        /// Contains the location.
        /// </summary>
        /// <history>
        /// [Curtis_Beard]		07/21/2006	Created
        /// </history>
        public string Editor
        {
            get { return __editor; }
            set { __editor = value; }
        }

        /// <summary>
        /// Contains the command line arguments.
        /// </summary>
        /// <history>
        /// [Curtis_Beard]		07/21/2006	Created
        /// </history>
        public string Arguments
        {
            get { return __editorArgs; }
            set { __editorArgs = value; }
        }

        /// <summary>
        /// Contains the editor's tab size.
        /// </summary>
        /// <history>
        /// [Curtis_Beard]		10/12/2012	Created
        /// </history>
        public int TabSize
        {
            get { return __tabSize; }
            set { __tabSize = value; }
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
        /// </history>
        public override string ToString()
        {
            return string.Format("{1}{0}{2}{0}{3}{0}{4}", DELIMETER, __editor, __editorArgs, __type, __tabSize);
        }

        /// <summary>
        /// Translates the given string representation into the class/s properties.
        /// </summary>
        /// <param name="classAsString">The string representation of this class</param>
        /// <history>
        /// [Curtis_Beard]		07/21/2006	Created
        /// [Curtis_Beard]		10/12/2012	ADD: tab size
        /// </history>
        public static TextEditor FromString(string classAsString)
        {
            TextEditor editor = new TextEditor();

            if (classAsString.Length > 0 && classAsString.IndexOf(DELIMETER) > -1)
            {
                string[] values = Core.Common.SplitByString(classAsString, DELIMETER);

                if (values.Length == 3)
                {
                    editor.Editor = values[0];
                    editor.Arguments = values[1];
                    editor.FileType = values[2];
                    editor.TabSize = 0;
                }
                else if (values.Length == 4)
                {
                    editor.Editor = values[0];
                    editor.Arguments = values[1];
                    editor.FileType = values[2];

                    int size = 0;
                    int.TryParse(values[3], out size);
                    editor.TabSize = size;
                }
            }

            return editor;
        }
        #endregion
    }
}
