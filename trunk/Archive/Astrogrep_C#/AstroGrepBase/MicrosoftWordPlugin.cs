using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AstroGrep.AstroGrepBase
{
   /// <summary>
   /// Used to search a Microsoft Word file for a specified voidstring.
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
   ///   along with this program; if (not, write to the Free Software
   ///   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
   /// 
   ///   The author may be contacted at:
   ///   ted@astrocomma.com or curtismbeard@gmail.com
   /// </remarks>
   /// <history>
   /// [Curtis_Beard]      07/28/2006	Created
   /// </history>
   public class MicrosoftWordPlugin : IDisposable, IAstroGrepPlugin
   {
		#region Declarations
      private bool __IsAvailable = false;
      private bool __IsUsable = false;
      private Type __WordType;
      private object __WordApplication;
      private object __WordDocuments;
      private object __WordSelection;

      private bool __useRegularExpressions = false;
      private bool __caseSensistiveMatch = false;
      private bool __wholeWordMatch = false;
      private bool __onlyFileNames = false;
      private bool __includeLineNumbers = true;
      private int __contextLines = 0;

      private string PLUGIN_NAME = "Microsoft Word";
      private string PLUGIN_VERSION = "1.0";
      private string PLUGIN_AUTHOR = "Curtis Beard";
      private string PLUGIN_DESCRIPTION = "Searches Microsoft Word documents for specified text.  Line numbers are shown as (Line,Page).  Currently doesn't support Regular Expressions or Context lines.";

      private object MISSING_VALUE = System.Reflection.Missing.Value;
      private const string NEW_LINE = "\r\n";
		#endregion

		#region Class Level
      /// <summary>
      /// Initializes a new instance of the MicrosoftWordPlugin class.
      /// </summary>
      /// <history>
      /// 	[Curtis_Beard]		07/28/2006	Created
      /// </history>
      public MicrosoftWordPlugin()
      {
         try
         {
            __WordType = Type.GetTypeFromProgID("Word.Application");

            if (__WordType != null)
               __IsAvailable = true;
			}
         catch (Exception ex)
         {
            __IsAvailable = false;
            Trace(ex.ToString());
         }
      }

      /// <summary>
      /// Handles disposing of the object.
      /// </summary>
      /// <history>
      /// 	[Curtis_Beard]		07/28/2006	Created
      /// </history>
      
      //Implements System.IDisposable.Dispose
      public void Dispose()
      {

         if (__WordType != null && __WordApplication != null)
         {
            // Close the application.
            __WordType.InvokeMember("Quit", BindingFlags.InvokeMethod, null,
                                       __WordApplication, new object[] {});
			}

         if (__WordApplication != null)
            Marshal.ReleaseComObject(__WordApplication);

         __WordApplication = null;
         __WordType = null;

         __IsAvailable = false;
      }

      /// <summary>
      /// Cleanup of object.
      /// </summary>
      /// <history>
      /// 	[Curtis_Beard]		07/28/2006	Created
      /// </history>
      ~MicrosoftWordPlugin()
      {
         this.Dispose();
      }
		#endregion

		#region Properties
      /// <summary>
      /// Gets the name of the plugin.
      /// </summary>
      /// <history>
      /// 	[Curtis_Beard]		07/27/2006	Created
      /// </history>
      public string Name
		{
         get { return PLUGIN_NAME; }
      }

      /// <summary>
      /// Gets the version of the plugin.
      /// </summary>
      /// <history>
      /// 	[Curtis_Beard]		07/27/2006	Created
      /// </history>
      public string Version
		{
         get { return PLUGIN_VERSION; }
      }

      /// <summary>
      /// Gets the author of the plugin.
      /// </summary>
      /// <history>
      /// 	[Curtis_Beard]		07/27/2006	Created
      /// </history>
      public string Author
		{
         get { return PLUGIN_AUTHOR; }
      }

      /// <summary>
      /// Gets the description of the plugin.
      /// </summary>
      /// <history>
      /// 	[Curtis_Beard]		07/27/2006	Created
      /// </history>
      public string Description
		{
         get { return PLUGIN_DESCRIPTION; }
      }

      /// <summary>
      /// Gets the valid extensions for this grep type.
      /// </summary>
      /// <remarks>Comma separated list of strings.</remarks>
      /// <history>
      /// 	[Curtis_Beard]		07/27/2006	Created
      /// </history>
      public string Extensions
      {
         get { return ".doc"; }
      }

      /// <summary>
      /// Checks to see if (Microsoft Word is available on this system.
      /// </summary>
      /// <history>
      /// 	[Curtis_Beard]		07/27/2006	Created
      /// </history>
      public bool IsAvailable
		{
         get { return __IsAvailable; }
      }

      /// <summary>
      /// Checks to see if (Microsoft Word is available to use for searching.
      /// </summary>
      /// <history>
      /// 	[Curtis_Beard]		07/27/2006	Created
      /// </history>
      private bool IsUsable
      {
         get { return __IsUsable; }
      }
		#endregion

		#region public Methods
      /// <summary>
      /// Loads Microsoft Word and prepares it for a grep.
      /// </summary>
      /// <returns>returns true if (successfully loaded or false otherwise</returns>
      /// <history>
      /// 	[Curtis_Beard]		07/27/2006	Created
      /// </history>
      public bool Load()
      {
         return Load(false);
      }

      /// <summary>
      /// Loads Microsoft Word and prepares it for a grep.
      /// </summary>
      /// <param name="visible">if (true, makes Microsoft Word visible, false is make it hidden</param>
      /// <returns>returns true if (successfully loaded or false otherwise</returns>
      /// <history>
      /// 	[Curtis_Beard]		07/27/2006	Created
      /// </history>
      public bool Load(bool visible)
      {
         try
         {
            if (this.IsAvailable)
            {

               if (!this.IsUsable)
               {
                  // load word
                  __WordApplication = Activator.CreateInstance(__WordType);

                  // set visible state
                  __WordType.InvokeMember("Visible", BindingFlags.SetProperty, null,
                                          __WordApplication, new object[1] {visible});

                  // get Documents Property
                  __WordDocuments = __WordType.InvokeMember("Documents", BindingFlags.GetProperty,
                                    null, __WordApplication, null);

                  // if all is good, then say we are usable
                  if (__WordDocuments != null)
                  {
                     __IsUsable = true;
                     return true;
                  }
               }
               else
               {
                  // probably already loaded, but check anyways
                  if (__WordApplication != null && __WordDocuments != null)
                     return true;
               }
            }
         }
         catch (Exception ex)
         {
            Trace(ex.ToString());
         }

         __IsUsable = false;
         return false;
      }

      /// <summary>
      /// Unloads Microsoft Word.
      /// </summary>
      /// <history>
      /// 	[Curtis_Beard]		07/27/2006	Created
      /// </history>
      public void Unload()
		{
         if (__WordType != null && __WordApplication != null)
         {
            // Close the application.
            try
            {
               __WordType.InvokeMember("Quit", BindingFlags.InvokeMethod, null, 
                  __WordApplication, new object[] {});
            }
            catch (Exception ex)
            {
               Trace(ex.ToString());
            }
         }

         if (__WordApplication != null)
         {
            try
            {
               Marshal.ReleaseComObject(__WordApplication);
            }
            catch (Exception ex)
            {
               Trace(ex.ToString());
            }
         }

         __WordApplication = null;
         __IsUsable = false;
      }

      /// <summary>
      /// Searches the given file for the given search text.
      /// </summary>
      /// <param name="file">FileInfo object</param>
      /// <param name="searchText">Text to locate</param>
      /// <returns>Hitobject containing grep results, null if (not found or error</returns>
      /// <history>
      /// 	[Curtis_Beard]		07/27/2006	Created
      /// </history>
      public HitObject Grep(System.IO.FileInfo file, string searchText)
      {
         return Grep(file.FullName, searchText);
      }

      /// <summary>
      /// Searches the given file for the given search text.
      /// </summary>
      /// <param name="path">Fully qualified file path</param>
      /// <param name="searchText">Text to locate</param>
      /// <returns>Hitobject containing grep results, null if (not found or error</returns>
      /// <history>
      /// 	[Curtis_Beard]		07/27/2006	Created
      /// </history>
      public HitObject Grep(string path, string searchText)
      {
         if (this.IsAvailable && this.IsUsable)
         {
            try
            {
               if (File.Exists(path))
               {
                  const int MARGINSIZE = 4;
                  int count = 0;
                  HitObject hit = null;
                  int prevLine = 0;
                  int prevPage = 0;
                  string _spacer = new string(' ', MARGINSIZE);
                  string _contextSpacer = string.Empty;

                  if (__contextLines > 0)
                  {
                     _contextSpacer = new string(' ', MARGINSIZE);
                     //_spacer = Right(_contextSpacer, MARGINSIZE - 2) + "> ";
                     _spacer = _contextSpacer.Substring(_contextSpacer.Length - MARGINSIZE - 2) + "> ";
                  }
                  else
                     _spacer = new string(' ', MARGINSIZE);

                  // Open a given Word document as readonly
                  object wordDocument = OpenDocument(path, true);

                  // Get Selection Property
                  __WordSelection = __WordType.InvokeMember("Selection", BindingFlags.GetProperty,
                                    null, __WordApplication, null);

                  // create range and find objects
                  object range = GetProperty(wordDocument, "Content");
                  object find = GetProperty(range, "Find");

                  // setup find
                  RunRoutine(find, "ClearFormatting", null);
                  SetProperty(find, "Forward", true);
                  SetProperty(find, "Text", searchText);
                  SetProperty(find, "MatchWholeWord", __wholeWordMatch);
                  SetProperty(find, "MatchCase", __caseSensistiveMatch);

                  // start find
                  FindExecute(find);

                  // keep finding text
                  while ((bool)GetProperty(find, "Found") == true)
                  {
                     count += 1;

                     if (count == 1)
                     {
                        // create hit object
                        hit = new HitObject(path);
                     }

                     // since a hit was found and only displaying file names, quickly exit
                     if (__onlyFileNames)
								break;

                     // retrieve find information
                     int start = (int)GetProperty(range, "Start");
                     int colNum = (int)Information(range, WdInformation.wdFirstCharacterColumnNumber);
                     int lineNum = (int)Information(range, WdInformation.wdFirstCharacterLineNumber);
                     int pageNum = (int)Information(range, WdInformation.wdActiveEndPageNumber);
                     string line = GetFindTextLine(start);

                     // don't add a hit if (on same line
                     if (!(prevLine == lineNum && prevPage == pageNum))
                     {
                        // check for line numbers
                        if (__includeLineNumbers)
                        {
                           // setup line header
                           _spacer = "(" + string.Format("{0},{1}", lineNum, pageNum);
                           if (_spacer.Length <= 5)
                           {
                              //_spacer = _spacer + Space(6 - _spacer.Length);
                              _spacer = _spacer + new string(' ', 6 - _spacer.Length);
                           }
                           _spacer = _spacer + ") ";
                           //_contextSpacer = "(" + Space(_spacer.Length - 3) + ") ";
                           _contextSpacer = "(" + new string(' ', _spacer.Length - 3) + ") ";
                        }

                        //  remove any odd characters from the text
                        line = RemoveSpecialCharacters(line);

                        // add context lines before
                        // if (__contextLines > 0){
                        //    For i As int = __contextLines To 1 Step -1
                        //       SetProperty(__WordSelection, "Start", start)
                        //       SelectionMoveUp(WdUnits.wdLine, i, WdMovementType.wdMove)
                        //       Dim cxt As string = GetFindTextLine()
                        //       cxt = RemoveSpecialCharacters(cxt)

                        //       if (Not HitExists(cxt, hit)){
                        //          hit.Add(_contextSpacer & cxt & NEW_LINE, lineNum - i, 1)
                        //       End If
                        //    Next
                        // End If

                        // add line
                        hit.Add(_spacer + line + NEW_LINE, lineNum, colNum);

                        // add context lines after
                        // if (__contextLines > 0){
                        //    For i As int = 1 To __contextLines
                        //       SetProperty(__WordSelection, "Start", start)
                        //       SelectionMoveDown(WdUnits.wdLine, i, WdMovementType.wdMove)
                        //       Dim cxt As string = GetFindTextLine()
                        //       cxt = RemoveSpecialCharacters(cxt)

                        //       if (Not HitExists(cxt, hit)){
                        //          hit.Add(_contextSpacer & cxt & NEW_LINE, lineNum + i, 1)
                        //       End If
                        //    Next
                        // End If
                     }
                     hit.SetHitCount();

                     prevLine = lineNum;
                     prevPage = pageNum;

                     // find again
                     FindExecute(find);
                  }

                  ReleaseSelection();
                  CloseDocument(wordDocument);

                  return hit;
               }
            }
            catch (Exception ex)
            {
               Trace(ex.ToString());
            }
         }

         return null;
      }
		#endregion

		#region private Methods
      private bool HitExists(string line, HitObject hit)
      {
         for (int i = 0; i < hit.LineCount; i++)
			{
            if (hit.RetrieveLine(i).IndexOf(line) > -1)
               return true;
         }

         return false;
      }

      private void ReleaseSelection()
      {
         if (__WordSelection != null)
         {
            Marshal.ReleaseComObject(__WordSelection);
         }
         __WordSelection = null;
      }

      private void FindExecute(object find)
      {
         if (this.IsAvailable && __WordType != null && find != null)
         {
            __WordType.InvokeMember("Execute", BindingFlags.InvokeMethod, null, find, new object[] {});
         }
      }

      private object OpenDocument(string path, bool bReadOnly)
      {
         if (this.IsAvailable && __WordType != null && __WordDocuments != null)
         {
            return __WordType.InvokeMember("Open", BindingFlags.InvokeMethod,
               null, __WordDocuments, new object[3] {path, MISSING_VALUE, bReadOnly});
         }

         return null;
      }

      private void CloseDocument(object doc)
      {
         if (this.IsAvailable && __WordType != null && doc != null)
            __WordType.InvokeMember("Close", BindingFlags.InvokeMethod, null, doc, new object[] {});
      }

      private enum WdInformation
      {
         wdActiveEndPageNumber = 3,
         wdFirstCharacterColumnNumber = 9,
         wdFirstCharacterLineNumber = 10
      }

      private object Information(object obj, WdInformation type)
      {
         if (this.IsAvailable && __WordType != null && obj != null)
            return __WordType.InvokeMember("Information", BindingFlags.GetProperty, null, obj, new object[1] {(int)type});

         return null;
      }

      private object GetProperty(object obj, string prop)
      {
         if (this.IsAvailable && __WordType != null && obj != null)
            return __WordType.InvokeMember(prop, BindingFlags.GetProperty, null, obj, new object[] {});

         return null;
      }

      private void SetProperty(object obj, string prop, object value)
      {
         if (this.IsAvailable && __WordType != null && obj != null)
            __WordType.InvokeMember(prop, BindingFlags.SetProperty, null, obj, new object[1] {value});
      }

      private void RunRoutine(object obj, string routine, object[] parms)
      {
         if (this.IsAvailable && __WordType != null && obj != null)
            __WordType.InvokeMember(routine, BindingFlags.InvokeMethod, null, obj, parms);
      }

      private enum WdUnits
      {
         wdLine = 5
      }

      private enum WdMovementType
      {
         wdMove = 0,
         wdExtend = 1
      }

      private void SelectionHomeKey(WdUnits unit)
      {
         RunRoutine(__WordSelection, "HomeKey", new object[1] {(int)unit});
      }

      private void SelectionHomeKey(WdUnits unit, WdMovementType extend)
      {
         RunRoutine(__WordSelection, "HomeKey", new object[2] {(int)unit, (int)extend});
      }

      private void SelectionEndKey(WdUnits unit)
      {
         RunRoutine(__WordSelection, "EndKey", new object[1] {(int)unit});
      }

      private void SelectionEndKey(WdUnits unit, WdMovementType extend)
      {
         RunRoutine(__WordSelection, "EndKey", new object[2] {(int)unit, (int)extend});
      }

      private void SelectionMoveUp(WdUnits unit, int count, WdMovementType extend)
      {
         RunRoutine(__WordSelection, "MoveUp", new object[3] {(int)unit, count, (int)extend});
      }

      private void SelectionMoveDown(WdUnits unit, int count, WdMovementType extend)
      {
         RunRoutine(__WordSelection, "MoveDown", new object[3] {(int)unit, count, (int)extend});
      }

      private string GetFindTextLine()
      {
         try
         {
            SelectionHomeKey(WdUnits.wdLine);
            SelectionEndKey(WdUnits.wdLine, WdMovementType.wdExtend);

            return GetProperty(__WordSelection, "Text").ToString();
         }
         catch (Exception ex)
         {
            Trace(ex.ToString());
         }

         return string.Empty;
      }

      private string GetFindTextLine(int start)
      {
         try
         {
            SetProperty(__WordSelection, "Start", start);
            SelectionHomeKey(WdUnits.wdLine);
            SelectionEndKey(WdUnits.wdLine, WdMovementType.wdExtend);

            return GetProperty(__WordSelection, "Text").ToString();
         }
         catch (Exception ex)
         {
            Trace(ex.ToString());
         }

         return string.Empty;
      }

      private string RemoveSpecialCharacters(string line)
      {
         string cleanLine = line;

         if (cleanLine.EndsWith("\r\n"))
            cleanLine = cleanLine.Substring(0, cleanLine.LastIndexOf("\r\n"));
         else if (cleanLine.EndsWith("\r"))
            cleanLine = cleanLine.Substring(0, cleanLine.LastIndexOf("\r"));
         else if (cleanLine.EndsWith("\n"))
            cleanLine = cleanLine.Substring(0, cleanLine.LastIndexOf("\n"));

         if (cleanLine.EndsWith("\a"))
            cleanLine = cleanLine.Substring(0, cleanLine.LastIndexOf("\a"));

         if (cleanLine.EndsWith("\r\n"))
            cleanLine = cleanLine.Substring(0, cleanLine.LastIndexOf("\r\n"));
         else if (cleanLine.EndsWith("\r"))
            cleanLine = cleanLine.Substring(0, cleanLine.LastIndexOf("\r"));
         else if (cleanLine.EndsWith("\n"))
            cleanLine = cleanLine.Substring(0, cleanLine.LastIndexOf("\n"));

         return cleanLine;
      }

      private void Trace(string message)
      {
         System.Diagnostics.Debug.WriteLine("Microsoft Word Plugin: " + message);
      }
		#endregion

		#region Grep Options
      /// <summary></summary>
      public int ContextLines
      {
         set { __contextLines = value; }
      }

      /// <summary></summary>
      public bool IncludeLineNumbers
      {
         set { __includeLineNumbers = value; }
      }

      /// <summary></summary>
      public bool ReturnOnlyFileNames
      {
         set { __onlyFileNames = value; }
      }

      /// <summary></summary>
      public bool UseCaseSensitivity
      {
         set { __caseSensistiveMatch = value; }
      }

      /// <summary></summary>
      public bool UseRegularExpressions
      {
         set { __useRegularExpressions = value; }
      }

      /// <summary></summary>
      public bool UseWholeWordMatching
      {
         set { __wholeWordMatch = value; }
      }
		#endregion
   }
}