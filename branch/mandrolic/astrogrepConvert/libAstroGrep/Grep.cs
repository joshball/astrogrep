using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

using libAstroGrep.Plugin;

namespace libAstroGrep
{
   /// <summary>
   /// Searches files, given a starting directory, for a given search text.  Results 
   /// are populated into a HashTable of HitObjects which contain information 
   /// about the file, line numbers, and the actual lines which the search text was
   /// found.
   /// </summary>
   /// <remarks>
   /// AstroGrep File Searching Utility. Written by Theodore L. Ward
   /// Copyright (C) 2002 AstroComma Incorporated.
   /// 
   /// This program is free software; you can redistribute it and/or
   /// modify it under the terms of the GNU General Public License
   /// as published by the Free Software Foundation; either version 2
   /// of the License, or (at your option) any later version.
   /// 
   /// This program is distributed in the hope that it will be useful,
   /// but WITHOUT ANY WARRANTY; without even the implied warranty of
   /// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   /// GNU General Public License for more details.
   /// 
   /// You should have received a copy of the GNU General Public License
   /// along with this program; if not, write to the Free Software
   /// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
   /// 
   /// The author may be contacted at:
   /// ted@astrocomma.com or curtismbeard@gmail.com
   /// </remarks>
   /// <history>
   /// [Curtis_Beard]		09/08/2005	Created
   /// [Curtis_Beard]		12/02/2005	CHG: StatusMessage to SearchingFile
   /// [Curtis_Beard]		07/26/2006	ADD: Events for search complete and search cancelled,
   ///											and routines to perform asynchronously
   /// [Curtis_Beard]		07/28/2006	ADD: extension exclusion list
   /// [Curtis_Beard]		09/12/2006	CHG: Converted to C#
   /// [Curtis_Beard]		01/27/2007	ADD: 1561584, check directories/files if hidden or system
   /// [Curtis_Beard]		05/25/2007	ADD: Virtual methods for events
   /// [Curtis_Beard]		06/27/2007	CHG: removed message parameters for Complete/Cancel events
   /// [Andrew_Radford]     05/08/2008  CHG: Convert code to C# 3.5
   /// </history>
   public class Grep
   {
       private readonly List<String> __exclusionList = new List<String>();
       private Thread _thread;
       //private string __directoryFilter;

      #region Public Events and Delegates
      /// <summary>File being searched</summary>
      /// <param name="file">FileInfo object of file currently being searched</param>
      public delegate void SearchingFileHandler(FileInfo file);
      /// <summary>File being searched</summary>
      public event SearchingFileHandler SearchingFile;

      /// <summary>File containing search text was found</summary>
      /// <param name="file">FileInfo object</param>
      /// <param name="index">Index into grep collection</param>
      public delegate void FileHitHandler(FileInfo file, int index);
      /// <summary>File containing search text was found</summary>
      public event FileHitHandler FileHit;

      /// <summary>Line containing search text was found</summary>
      /// <param name="hit">HitObject containing the information about the find</param>
      /// <param name="index">Position in collection of lines</param>
      public delegate void LineHitHandler(HitObject hit, int index);
      /// <summary>Line containing search text was found</summary>
      public event LineHitHandler LineHit;

      /// <summary>A file search threw an error</summary>
      /// <param name="file">FileInfo object error occurred with</param>
      /// <param name="ex">Exception</param>
      public delegate void SearchErrorHandler(FileInfo file, Exception ex);
      /// <summary>A file search threw an error</summary>
      public event SearchErrorHandler SearchError;

      /// <summary>The search has completed</summary>
      public delegate void SearchCompleteHandler();
      /// <summary>The search has completed</summary>
      public event SearchCompleteHandler SearchComplete;

      /// <summary>The search has been cancelled</summary>
      public delegate void SearchCancelHandler();
      /// <summary>The search has been cancelled</summary>
      public event SearchCancelHandler SearchCancel;
      #endregion

      #region Public Properties

       /// <summary>Retrieves all HitObjects for grep</summary>
       public IList<HitObject> Greps { get; private set; }

        /// <summary>The start (basr) search directory</summary>
       public string StartDirectory { get; set; }

       /// <summary>The FileFilter</summary>
       public string FileFilter { get; set; }

       /// <summary>The PluginCollection containing IAstroGrepPlugins.</summary>
       public PluginCollection Plugins { get; set; }

       /// <summary>Whether to skip hidden files and directories.</summary>
       public bool SkipHiddenFiles { get; set; }

       
       /// <summary>Whether to skip system files and directories.</summary>
       public bool SkipSystemFiles { get; set; }

       /// <summary>Whether to include line numbers as part of a line</summary>
       public bool IncludeLineNumbers { get; set; }



// Search specs start here
       
       /// <summary>Use of directory recursion for grep</summary>
       public bool SearchInSubfolders { get; set; }

       /// <summary>Use of regular expressions for grep</summary>
       public bool UseRegularExpressions { get; set; }

       /// <summary>Use of a case sensitive grep</summary>
       public bool UseCaseSensitivity { get; set; }

       /// <summary>Use of a whole word match grep</summary>
       public bool UseWholeWordMatching { get; set; }

       /// <summary>Use of negation of the grep results</summary>
       public bool UseNegation { get; set; }

       /// <summary>The number of context lines included in grep results</summary>
       public int ContextLines { get; set; }

       /// <summary>The search text</summary>
       public string SearchText { get; set; }

       /// <summary>Whether to return only file names for grep results</summary>
       public bool ReturnOnlyFileNames { get; set; }

    
    
       #endregion

      /// <summary>
      /// Initializes a new instance of the Grep class.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/12/2006	Created
      /// </history>
      public Grep()
      {
          IncludeLineNumbers = true;
          SearchInSubfolders = true;
          Greps = new List<HitObject>();
         
      }

       #region Public Methods

      /// <summary>
      /// Begins an asynchronous grep of files for a specified text.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/12/2006	Created
      /// </history>
      public void BeginExecute()
      {
         _thread = new Thread(StartGrep) {IsBackground = true};
         _thread.Start();
      }

      /// <summary>
      /// Cancels an asynchronous grep request.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/12/2006	Created
      /// </history>
      public void Abort()
      {
         if (_thread != null)
         {
            _thread.Abort();
            _thread = null;
         }
      }

      /// <summary>
      /// Grep files for a specified text.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   09/08/2005	Created
      /// [Curtis_Beard]	   10/13/2005	ADD: Support for comma-separated fileFilter
      /// [Curtis_Beard]	   07/12/2006	CHG: remove parameters and use properties
      /// </history>
      public void Execute()
      {
         string[] _filters = FileFilter.Split(char.Parse(","));

         if (_filters.Length > 0)
         {
            // have to grep for each filter separately
             foreach (var _filter in _filters)
                 Execute(new DirectoryInfo(StartDirectory), null, _filter, SearchText);
         }
         else
         {
            Execute(new DirectoryInfo(StartDirectory), null, FileFilter, SearchText);
         }
      }

      /// <summary>
      /// Retrieve a specified hitObject form the hash table
      /// </summary>
      /// <param name="index">the key in the hash table</param>
      /// <returns>HitObject at given index.  On error returns nothing.</returns>
      /// <history>
      /// [Curtis_Beard]      09/08/2005	Created
      /// </history>
      public HitObject RetrieveHitObject(int index)
      {
         try
         {
            return (HitObject)Greps[index];
         }
         catch {}

         return null;
      }

      /// <summary>
      /// Add a file extension (.xxx) to the exclusion list.
      /// </summary>
      /// <param name="ext">File extension to add</param>
      /// <history>
      /// 	[Curtis_Beard]		07/28/2006	Created
      /// [Curtis_Beard]		08/30/2007	CHG: only add if doesn't exist and make lower case
      /// </history>
      public void AddExclusionExtension(string ext)
      {
         ext = ext.ToLower();

         if (!__exclusionList.Contains(ext))
         {
            __exclusionList.Add(ext);
         }
      }

      /// <summary>
      ///   Check to see if the begin/end text around searched text is valid
      ///   for a whole word search
      /// </summary>
      /// <param name="beginText">Text in front of searched text</param>
      /// <param name="endText">Text behind searched text</param>
      /// <returns>True - valid, False - otherwise</returns>
      /// <history>
      /// 	[Curtis_Beard]	   01/27/2005	Created
      /// </history>
      public static bool WholeWordOnly(string beginText, string endText)
      {
          return (ValidBeginText(beginText) && ValidEndText(endText));
      }
      #endregion

      #region Private Methods
      /// <summary>
      /// Used as the starting routine for a threaded grep
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      07/12/2006	Created
      /// [Curtis_Beard]		08/21/2007	FIX: 1778467, send cancel event on generic error
      /// </history>
      private void StartGrep()
      {
         try
         {
            Execute();

            OnSearchComplete();
         }
         catch (ThreadAbortException)
         {
            UnloadPlugins();
            OnSearchCancel();
         }
         catch (Exception ex)
         {
            OnSearchError(null, ex);

            UnloadPlugins();
            OnSearchCancel();
         }
      }

      /// <summary>
      /// Grep files for specified text.
      /// </summary>
      /// <param name="SourceDirectory">directory to begin grep</param>
      /// <param name="SourceDirectoryFilter">any directory specifications</param>
      /// <param name="SourceFileFilter">any file specifications</param>
      /// <param name="searchText">text to grep for</param>
      /// <remarks>Recursive algorithm</remarks>
      /// <history>
      /// [Curtis_Beard]      09/08/2005	Created
      /// [Curtis_Beard]      12/06/2005	CHG: skip any invalid directories
      /// [Curtis_Beard]      03/14/2006  CHG: catch any errors generated during a file search
      /// [Curtis_Beard]      07/03/2006  FIX: 1516774, ignore a thread abort exception
      /// [Curtis_Beard]      07/12/2006  CHG: make private
      /// [Curtis_Beard]      07/28/2006  ADD: check extension against exclusion list
      /// [Curtis_Beard]      01/27/2007  ADD: 1561584, check directories/files if hidden or system
      /// [Ed_Jakubowski]     05/20/2009  ADD: When a blank searchText is given only list files
      /// </history>
      private void Execute(DirectoryInfo SourceDirectory, string SourceDirectoryFilter, string SourceFileFilter, string searchText)
      {
         DirectoryInfo[] SourceSubDirectories; 
         FileInfo[] SourceFiles;

         // Check for File Filter
         if (SourceFileFilter != null)
            SourceFiles = SourceDirectory.GetFiles(SourceFileFilter.Trim());
         else
            SourceFiles = SourceDirectory.GetFiles();

         // Check for Folder Filter
         if (SourceDirectoryFilter != null)
            SourceSubDirectories = SourceDirectory.GetDirectories(SourceDirectoryFilter.Trim());
         else
            SourceSubDirectories = SourceDirectory.GetDirectories();

         //Search Every File for search text
         foreach ( FileInfo SourceFile in SourceFiles)
         {
            try
            {
               if (SkipSystemFiles && (SourceFile.Attributes & FileAttributes.System) == FileAttributes.System)
                  continue;
               if (SkipHiddenFiles && (SourceFile.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                  continue;

               if (!__exclusionList.Contains(SourceFile.Extension.ToLower()))
			   {
                  if (searchText == "")
				  {
				     var _grepHit = new HitObject(SourceFile) {Index = Greps.Count};
				      _grepHit.Add("" + Environment.NewLine, 0);
					 Greps.Add(_grepHit);
					 OnFileHit(SourceFile, _grepHit.Index);
				     
				  }
				  else
				     SearchFile(SourceFile, searchText);
			      
			   }
            }
            catch (ThreadAbortException)
            {
               UnloadPlugins();
            }
            catch (Exception ex)
            {
               OnSearchError(SourceFile, ex);
            }
         }

         if (SearchInSubfolders)
         {
            //Recursively go through every subdirectory and it's files (according to folder filter)
            foreach (DirectoryInfo SourceSubDirectory in SourceSubDirectories)
            {
               try
               {
                  if (SkipSystemFiles && (SourceSubDirectory.Attributes & FileAttributes.System) == FileAttributes.System)
                     continue;
                  if (SkipHiddenFiles && (SourceSubDirectory.Attributes & FileAttributes.Hidden) == System.IO.FileAttributes.Hidden)
                     continue;

                  Execute(SourceSubDirectory, SourceDirectoryFilter, SourceFileFilter, searchText);
               }
               catch
               {
                  //skip any invalid directory
               }
            }
         }
      }

      /// <summary>
      /// Search a given file for the searchText.
      /// </summary>
      /// <param name="file">FileInfo object for file to search for searchText</param>
      /// <param name="searchText">Text to find in file</param>
      /// <history>
      /// [Curtis_Beard]		09/08/2005	Created
      /// [Curtis_Beard]		11/21/2005	ADD: update hit count when actual line added
      /// [Curtis_Beard]		12/02/2005	CHG: use SearchingFile instead of StatusMessage
      /// [Curtis_Beard]		04/21/2006	CHG: use a regular expression match collection to get
      ///											correct count of hits in a line when using RegEx
      /// [Curtis_Beard]		07/03/2006	FIX: 1500174, use a FileStream to open the files readonly
      /// [Curtis_Beard]		07/07/2006	FIX: 1512029, RegEx use Case Sensitivity and WholeWords,
      ///											also use different whole word matching regex
      /// [Curtis_Beard]		07/26/2006	ADD: 1512026, column position
      /// [Curtis_Beard]		07/26/2006	FIX: 1530023, retrieve file with correct encoding
      /// [Curtis_Beard]		09/12/2006	CHG: Converted to C#
      /// [Curtis_Beard]		09/28/2006	FIX: check for any plugins before looping through them
      /// [Curtis_Beard]		05/18/2006	FIX: 1723815, use correct whole word matching regex
      /// [Curtis_Beard]		06/26/2007	FIX: correctly detect plugin extension support
      /// [Curtis_Beard]		06/26/2007	FIX: 1779270, increase array size holding context lines
      /// </history>
      private void SearchFile(FileInfo file, string searchText)
      {
         const int MARGINSIZE = 4;

         // Raise SearchFile Event
         OnSearchingFile(file);

         FileStream _stream = null;
         StreamReader _reader = null;
         int _lineNumber = 0;
         HitObject _grepHit = null;
          Regex _regularExp;
          MatchCollection _regularExpCol = null;
         bool _hitOccurred = false;
         bool _fileNameDisplayed = false;
         var _context = new string[11];
         int _contextIndex = 0;
         int _lastHit = 0;
         int _contextLinesCount = ContextLines;
         string _contextSpacer = string.Empty;
         string _spacer;         

         try
         {
            // Process plugins
            if (Plugins != null)
            {
               for (int i = 0; i < Plugins.Count; i++)
               {
                  // find a valid plugin for this file type
                  if (Plugins[i].Enabled && Plugins[i].Plugin.IsAvailable)
                  {
                     // detect if plugin supports extension
                     bool isFound = IsInList(file.Extension, Plugins[i].Plugin.Extensions, ',');
                     
                     // if extension not supported try another plugin
                     if (!isFound)
                        continue;

                     Exception pluginEx = null;

                     // setup plugin options
                     Plugins[i].Plugin.ContextLines = this.ContextLines;
                     Plugins[i].Plugin.IncludeLineNumbers = this.IncludeLineNumbers;
                     Plugins[i].Plugin.ReturnOnlyFileNames = this.ReturnOnlyFileNames;
                     Plugins[i].Plugin.UseCaseSensitivity = this.UseCaseSensitivity;
                     Plugins[i].Plugin.UseRegularExpressions = this.UseRegularExpressions;
                     Plugins[i].Plugin.UseWholeWordMatching = this.UseWholeWordMatching;

                     // load plugin and perform grep
                     if (Plugins[i].Plugin.Load())
                     {
                        _grepHit = Plugins[i].Plugin.Grep(file, searchText, ref pluginEx);
                     }
                     Plugins[i].Plugin.Unload();

                     // if the plugin processed successfully
                     if (pluginEx == null)
                     {
                        // check for a hit
                        if (_grepHit != null)
                        {
                           // only perform is not using negation
                           if (!UseNegation)
                           {
                              _grepHit.Index = Greps.Count;
                              Greps.Add(_grepHit);
                              OnFileHit(file, _grepHit.Index);

                              if (ReturnOnlyFileNames)
                                 _grepHit.SetHitCount();

                              OnLineHit(_grepHit, _grepHit.Index);
                           }
                        }
                        else if (UseNegation)
                        {
                           // no hit but using negation so create one
                           _grepHit = new HitObject(file) {Index = Greps.Count};
                            Greps.Add(_grepHit);
                           OnFileHit(file, _grepHit.Index);
                        }
                     }
                     else
                     {
                        // the plugin had an error
                        OnSearchError(file, pluginEx);
                     }

                     return;
                  }
               }
            }

            _stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            _reader = new StreamReader(_stream, System.Text.Encoding.Default);

            // Default spacer (left margin) values. If Line Numbers are on,
            // these will be reset within the loop to include line numbers.
            if (_contextLinesCount > 0)
            {
               _contextSpacer = new string(char.Parse(" "), MARGINSIZE);
               _spacer = _contextSpacer.Substring(MARGINSIZE - 2) + "> ";
            }
            else
               _spacer = new string(char.Parse(" "), MARGINSIZE);

            do
            {
               string textLine = _reader.ReadLine();

               if (textLine == null)
                  break;
               else
               {
                  _lineNumber += 1;

                   int _posInStr;
                   if (UseRegularExpressions)
                  {
                     _posInStr = -1;
                     if (textLine.Length > 0)
                     {
                        if (UseCaseSensitivity && UseWholeWordMatching)
                        {
                           _regularExp = new Regex("\\b" + searchText + "\\b");
                           _regularExpCol = _regularExp.Matches(textLine);
                        }
                        else if (UseCaseSensitivity)
                        {
                           _regularExp = new Regex(searchText);
                           _regularExpCol = _regularExp.Matches(textLine);
                        }
                        else if (UseWholeWordMatching)
                        {
                           _regularExp = new Regex("\\b" + searchText + "\\b", RegexOptions.IgnoreCase);
                           _regularExpCol = _regularExp.Matches(textLine);
                        }
                        else
                        {
                           _regularExp = new Regex(searchText, RegexOptions.IgnoreCase);
                           _regularExpCol = _regularExp.Matches(textLine);
                        }

                        if (_regularExpCol.Count > 0)
                        {
                           if (UseNegation)
                              _hitOccurred = true;

                           _posInStr = 1;
                        }
                     }
                  }
                  else
                  {
                     if (UseCaseSensitivity)

                        // Need to escape these characters in SearchText:
                        // < $ + * [ { ( ) .
                        // with a preceeding \

                        // If we are looking for whole worlds only, perform the check.
                        if (UseWholeWordMatching)
                        {
                           _regularExp = new Regex("\\b" + searchText + "\\b");
                           if (_regularExp.IsMatch(textLine))
                           {
                              if (UseNegation)
                                 _hitOccurred = true;

                              _posInStr = 1;
                           }
                           else
                              _posInStr = -1;
                        }
                        else
                        {
                           _posInStr = textLine.IndexOf(searchText);

                           if (UseNegation && _posInStr > -1)
                              _hitOccurred = true;
                        }
                     else
                     {
                        // If we are looking for whole worlds only, perform the check.
                        if (UseWholeWordMatching)
                        {
                           _regularExp = new Regex("\\b" + searchText + "\\b", RegexOptions.IgnoreCase);
                           if (_regularExp.IsMatch(textLine))
                           {
                              if (UseNegation)
                                 _hitOccurred = true;

                              _posInStr = 1;
                           }
                           else
                              _posInStr = -1;
                        }
                        else
                        {
                           _posInStr = textLine.ToLower().IndexOf(searchText.ToLower());

                           if (UseNegation && _posInStr > -1)
                              _hitOccurred = true;
                        }
                     }
                  }

                  //*******************************************
                  // We found an occurrence of our search text.
                  //*******************************************
                  if (_posInStr > -1)
                  {
                     //since we have a hit, check to see if negation is checked
                     if (UseNegation)
                        break;

                     if (!_fileNameDisplayed)
                     {
                        _grepHit = new HitObject(file) {Index = Greps.Count};
                         Greps.Add(_grepHit);

                        OnFileHit(file, _grepHit.Index);

                        _fileNameDisplayed = true;
                     }

                     // If we are only showing filenames, go to the next file.
                     if (ReturnOnlyFileNames)
                     {
                        //notify that at least 1 hit is in file
                        _grepHit.SetHitCount();
                        OnLineHit(_grepHit, _grepHit.Index);

                        break;
                     }

                     // Set up line number, or just an indention in front of the line.
                     if (IncludeLineNumbers)
                     {
                        _spacer = "(" + _lineNumber.ToString().Trim();
                        if (_spacer.Length <= 5)
                           _spacer = _spacer + new string(char.Parse(" "), 6 - _spacer.Length);

                        _spacer = _spacer + ") ";
                        _contextSpacer = "(" + new string(char.Parse(" "), _spacer.Length - 3) + ") ";
                     }

                     // Display context lines if applicable.
                     if (_contextLinesCount > 0 && _lastHit == 0)
                     {
                        if (_grepHit.LineCount > 0)
                        {
                           // Insert a blank space before the context lines.
                           int _pos = _grepHit.Add(Environment.NewLine, -1);
                           OnLineHit(_grepHit, _pos);
                        }

                        // Display preceeding n context lines before the hit.
                        for (_posInStr = _contextLinesCount; _posInStr >= 1; _posInStr--)
                        {
                           _contextIndex = _contextIndex + 1;
                           if (_contextIndex > _contextLinesCount)
                              _contextIndex = 1;

                           // If there is a match in the first one or two lines,
                           // the entire preceeding context may not be available.
                           if (_lineNumber > _posInStr)
                           {
                              // Add the context line.
                              int _pos = _grepHit.Add(_contextSpacer + _context[_contextIndex] + Environment.NewLine, _lineNumber - _posInStr);
                              OnLineHit(_grepHit, _pos);
                           }
                        }
                     }

                     _lastHit = _contextLinesCount;

                     //
                     // Add the actual "hit".
                     //
                     // set first hit column position
                     if (UseRegularExpressions)
                     {
                        // zero based
                        _posInStr = _regularExpCol[0].Index;
                     }                     
                     _posInStr += 1;
                     int _index = _grepHit.Add(_spacer + textLine + Environment.NewLine, _lineNumber, _posInStr);

                     if (UseRegularExpressions)
                        _grepHit.SetHitCount(_regularExpCol.Count);
                     else
                     {
                        //determine number of hits
                        _grepHit.SetHitCount(RetrieveLineHitCount(textLine, searchText));
                     }

                     OnLineHit(_grepHit, _index);
                  }
                  else if (_lastHit > 0 && _contextLinesCount > 0)
                  {
                     //***************************************************
                     // We didn't find a hit, but since lastHit is > 0, we
                     // need to display this context line.
                     //***************************************************
                     int _index = _grepHit.Add(_contextSpacer + textLine + Environment.NewLine, _lineNumber);
                     OnLineHit(_grepHit, _index);
                     _lastHit -= 1;

                  } // Found a hit or not.

                  // If we are showing context lines, keep the last n lines.
                  if (_contextLinesCount > 0)
                  {
                     if (_contextIndex == _contextLinesCount)
                        _contextIndex = 1;
                     else
                        _contextIndex += 1;

                     _context[_contextIndex] = textLine;
                  }
               }
            } 
            while (true);

            //
            // Check for no hits through out the file
            //
            if (UseNegation && _hitOccurred == false)
            {
               //add the file to the hit list
               if (!_fileNameDisplayed)
               {
                  _grepHit = new HitObject(file) {Index = Greps.Count};
                   Greps.Add( _grepHit);
                  OnFileHit(file, _grepHit.Index);
               }
            }
         }
         finally
         {
            if (_reader != null)
               _reader.Close();

            if (_stream != null)
               _stream.Close();
         }
      }

      /// <summary>
      /// Retrieves the number of instances of searchText in the given line
      /// </summary>
      /// <param name="line">Line of text to search</param>
      /// <param name="searchText">Text to search for</param>
      /// <returns>Count of how many instances</returns>
      /// <history>
      /// [Curtis_Beard]      12/06/2005	Created
      /// [Curtis_Beard]      01/12/2007	FIX: check for correct position of IndexOf
      /// </history>
      private int RetrieveLineHitCount(string line, string searchText)
      {
         int _count = 0;
         string _searchText = searchText;
         string _tempLine = string.Empty;
         string _begin = string.Empty;
         string _end = string.Empty;
         int _pos = -1;
         bool _highlight = false;

         _tempLine = line;

         // attempt to locate the text in the line
         if (UseCaseSensitivity)
            _pos = _tempLine.IndexOf(_searchText);
         else
            _pos = _tempLine.ToLower().IndexOf(_searchText.ToLower());

         while (_pos > -1)
         {
             // retrieve parts of text
            _begin = _tempLine.Substring(0, _pos);
            _end = _tempLine.Substring(_pos + _searchText.Length);

            // do a check to see if begin and end are valid for wholeword searches
            if (UseWholeWordMatching)
               _highlight = WholeWordOnly(_begin, _end);
            else
               _highlight = true;

            // found a hit
            if (_highlight)
               _count += 1;

            // Check remaining string for other hits in same line
            if (UseCaseSensitivity)
               _pos = _end.IndexOf(_searchText);
            else
               _pos = _end.ToLower().IndexOf(_searchText.ToLower());

            // update the temp line with the next part to search (if any)
            _tempLine = _end;
         }

         return _count;
      }

      /// <summary>
      /// Validate a start text.
      /// </summary>
      /// <param name="beginText">text to validate</param>
      /// <returns>True - valid, False - otherwise</returns>
      /// <history>
      /// [Curtis_Beard]		12/06/2005	Created
      /// [Curtis_Beard]		02/09/2007	FIX: 1655533, update whole word matching
      /// [Curtis_Beard]		08/21/2007	ADD: '/' character and Environment.NewLine
      /// </history>
      private static bool ValidBeginText(string beginText)
      {
         if (beginText.Equals(string.Empty) ||
            beginText.EndsWith(" ") ||
            beginText.EndsWith("<") ||
            beginText.EndsWith("$") ||
            beginText.EndsWith("+") ||
            beginText.EndsWith("*") ||
            beginText.EndsWith("[") ||
            beginText.EndsWith("{") ||
            beginText.EndsWith("(") ||
            beginText.EndsWith(".") ||
            beginText.EndsWith("?") ||
            beginText.EndsWith("!") ||
            beginText.EndsWith(",") ||
            beginText.EndsWith(":") ||
            beginText.EndsWith(";") ||
            beginText.EndsWith("-") ||
            beginText.EndsWith("\\") ||
            beginText.EndsWith("/") ||
            beginText.EndsWith("'") ||
            beginText.EndsWith("\"") ||
            beginText.EndsWith(Environment.NewLine) ||
            beginText.EndsWith("\r\n") ||
            beginText.EndsWith("\r") ||
            beginText.EndsWith("\n")
            )
         {  
            return true;
         }

         return false;
      }

      /// <summary>
      /// Validate an end text.
      /// </summary>
      /// <param name="endText">text to validate</param>
      /// <returns>True - valid, False - otherwise</returns>
      /// <history>
      /// [Curtis_Beard]		2/06/2005	Created
      /// [Curtis_Beard]		02/09/2007	FIX: 1655533, update whole word matching
      /// [Curtis_Beard]		08/21/2007	ADD: '/' character and Environment.NewLine
      /// </history>
      private static bool ValidEndText(string endText)
      {
         if (endText.Equals(string.Empty) ||
            endText.StartsWith(" ") ||
            endText.StartsWith("<") ||
            endText.StartsWith("$") ||
            endText.StartsWith("+") ||
            endText.StartsWith("*") ||
            endText.StartsWith("[") ||
            endText.StartsWith("{") ||
            endText.StartsWith("(") ||
            endText.StartsWith(".") ||
            endText.StartsWith("?") ||
            endText.StartsWith("!") ||
            endText.StartsWith(",") ||
            endText.StartsWith(":") ||
            endText.StartsWith(";") ||
            endText.StartsWith("-") ||
            endText.StartsWith(">") ||
            endText.StartsWith("]") ||
            endText.StartsWith("}") ||
            endText.StartsWith(")") ||
            endText.StartsWith("\\") ||
            endText.StartsWith("/") ||
            endText.StartsWith("'") ||
            endText.StartsWith("\"") ||
            endText.StartsWith(Environment.NewLine) ||
            endText.StartsWith("\r\n") ||
            endText.StartsWith("\r") ||
            endText.StartsWith("\n")
            )
         {
            return true;
         }

         return false;
      }

      /// <summary>
      /// Unload any plugins that are enabled and available.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      11/28/2006  Created
      /// [Theodore_Ward]     01/12/2007  FIX: check for null plugins list
      /// </history>
      private void UnloadPlugins()
      {
         if (Plugins != null)
         {
            for (int i = 0; i < Plugins.Count; i++)
            {
               if (Plugins[i].Plugin != null)
                  Plugins[i].Plugin.Unload();
            }
         }
      }

      /// <summary>
      /// Determines if a given value is within the given list.
      /// </summary>
      /// <param name="value">Value to find</param>
      /// <param name="list">List to check</param>
      /// <param name="separator">List item separator</param>
      /// <returns>True if found, False otherwise</returns>
      /// <history>
      /// [Curtis_Beard]		09/05/2007	Created
      /// </history>
      static public bool IsInList(string value, string list, char separator)
      {
         string[] items = list.ToLower().Split(separator);
         value = value.ToLower();

         foreach (string item in items)
         {
            if (item.Equals(value))
               return true;
         }

         return false;
      }
      #endregion

      #region Virtual Methods for Events
      /// <summary>
      /// Raise search error event.
      /// </summary>
      /// <param name="file">FileInfo when error occurred. (Can be null)</param>
      /// <param name="ex">Exception</param>
      /// <history>
      /// [Curtis_Beard]      05/25/2007  Created
      /// </history>
      protected virtual void OnSearchError(FileInfo file, Exception ex)
      {
         if (SearchError != null)
         {
            SearchError(file, ex);
         }
      }

      /// <summary>
      /// Raise search cancel event.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      05/25/2007  Created
      /// [Curtis_Beard]      06/27/2007  CHG: removed message parameter
      /// </history>
      protected virtual void OnSearchCancel()
      {
         if (SearchCancel != null)
         {
            SearchCancel();
         }
      }

      /// <summary>
      /// Raise search complete event.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      05/25/2007  Created
      /// [Curtis_Beard]      06/27/2007  CHG: removed message parameter
      /// </history>
      protected virtual void OnSearchComplete()
      {
         if (SearchComplete != null)
         {
            SearchComplete();
         }
      }

      /// <summary>
      /// Raise searching file event.
      /// </summary>
      /// <param name="file">FileInfo object being searched</param>
      /// <history>
      /// [Curtis_Beard]      05/25/2007  Created
      /// </history>
      protected virtual void OnSearchingFile(FileInfo file)
      {
         if (SearchingFile != null)
         {
            SearchingFile(file);
         }
      }

      /// <summary>
      /// Raise file hit event.
      /// </summary>
      /// <param name="file">FileInfo object that was found to contain a hit</param>
      /// <param name="index">Index into array of HitObjects</param>
      /// <history>
      /// [Curtis_Beard]      05/25/2007  Created
      /// </history>
      protected virtual void OnFileHit(FileInfo file, int index)
      {
         if (FileHit != null)
         {
            FileHit(file, index);
         }
      }

      /// <summary>
      /// Raise line hit event.
      /// </summary>
      /// <param name="hit">HitObject containing line hit</param>
      /// <param name="index">Index to line</param>
      /// <history>
      /// [Curtis_Beard]      05/25/2007  Created
      /// </history>
      protected virtual void OnLineHit(HitObject hit, int index)
      {
         if (LineHit != null)
         {
            LineHit(hit, index);
         }
      }
      #endregion
   }
}
