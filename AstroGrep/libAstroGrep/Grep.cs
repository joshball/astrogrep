using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using libAstroGrep.Plugin;

namespace libAstroGrep
{
   /// <summary>
   /// Searches files, given a starting directory, for a given search text.  Results 
   /// are populated into a List of HitObjects which contain information 
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
   /// [Andrew_Radford]    05/08/2008  CHG: Convert code to C# 3.5
   /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
   /// [Curtis_Beard]	   12/01/2014	ADD: support for detected encoding event
   /// </history>
   public class Grep
   {
      private Thread _thread;

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

      /// <summary>
      /// File filtering
      /// </summary>
      /// <param name="file">FileInfo object of file currently being filtered</param>
      /// <param name="filterItem">FilterItem causing filtering</param>
      /// <param name="value">Value causing filtering</param>
      public delegate void FileFilteredOut(FileInfo file, FilterItem filterItem, string value);
      /// <summary>File being filtered</summary>
      public event FileFilteredOut FileFiltered;

      /// <summary>
      /// Directory filtering
      /// </summary>
      /// <param name="dir">DirectoryInfo object of directory currently being filtered</param>
      /// <param name="filterItem">FilterItem causing filtering</param>
      /// <param name="value">Value causing filtering</param>
      public delegate void DirectoryFilteredOut(DirectoryInfo dir, FilterItem filterItem, string value);
      /// <summary>Directory being filtered</summary>
      public event DirectoryFilteredOut DirectoryFiltered;

      /// <summary>The current file is being searched by a plugin</summary>
      /// <param name="pluginName">Name of plugin</param>
      public delegate void SearchingFileByPluginHandler(string pluginName);
      /// <summary>File being searched by a plugin</summary>
      public event SearchingFileByPluginHandler SearchingFileByPlugin;

      /// <summary>The current file's detected encoding</summary>
      /// <param name="file">FileInfo object</param>
      /// <param name="encoding">System.Text.Encoding</param>
      /// <param name="encoderName">The detected encoder name</param>
      public delegate void FileEncodingDetectedHandler(FileInfo file, System.Text.Encoding encoding, string encoderName);
      /// <summary>File's detected encoding</summary>
      public event FileEncodingDetectedHandler FileEncodingDetected;
      #endregion

      #region Public Properties

      /// <summary>Retrieves all HitObjects for grep</summary>
      public IList<HitObject> Greps { get; private set; }

      /// <summary>The PluginCollection containing IAstroGrepPlugins.</summary>
      public List<PluginWrapper> Plugins { get; set; }

      /// <summary>The File filter specification.</summary>
      public IFileFilterSpec FileFilterSpec { get; private set; }

      /// <summary>The Search specification.</summary>
      public ISearchSpec SearchSpec { get; private set; }

      #endregion

      private int userFilterCount = 0;

      /// <summary>
      /// Initializes a new instance of the Grep class.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/12/2006	Created
      /// [Andrew_Radford]    13/08/2009  Added Const. dependency on ISearchSpec, IFileFilterSpec
      /// </history>
      public Grep(ISearchSpec searchSpec, IFileFilterSpec filterSpec)
      {
         SearchSpec = searchSpec;
         FileFilterSpec = filterSpec;
         Greps = new List<HitObject>();

         if (FileFilterSpec.FilterItems != null)
         {
            // get first file->minimum hit count filter (should only be 1)
            var fileCountFilter = (from f in FileFilterSpec.FilterItems where f.FilterType.Category == FilterType.Categories.File && f.FilterType.SubCategory == FilterType.SubCategories.MinimumHitCount select f).FirstOrDefault();

            if (fileCountFilter != null)
            {
               int.TryParse(fileCountFilter.Value, out userFilterCount);
            }
         }
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
         _thread = new Thread(StartGrep) { IsBackground = true };
         _thread.Start();
      }

      /// <summary>
      /// Cancels an asynchronous grep request.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/12/2006	Created
      /// </history>
      /// Todo: do a kill signal to stop the exception
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
      /// [Curtis_Beard]	   09/17/2013	CHG: 61, ability to split file filters by comma and semi colon (, ;)
      /// [Curtis_Beard]		11/10/2014	FIX: 59, check for duplicate entries of file filter
      /// </history>
      public void Execute()
      {
         if (SearchSpec.StartFilePaths != null && SearchSpec.StartFilePaths.Length > 0)
         {
            foreach (string path in SearchSpec.StartFilePaths)
            {
               SearchFile(new FileInfo(path));
            }
         }
         else
         {
            if (string.IsNullOrEmpty(FileFilterSpec.FileFilter))
            {
               foreach (var dir in SearchSpec.StartDirectories)
               {
                  Execute(new DirectoryInfo(dir), null, null);
               }
            }
            else
            {
               string[] filters = FileFilterSpec.FileFilter.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
               
               // remove any duplicates
               List<string> fileFilters = filters.Distinct(StringComparer.InvariantCultureIgnoreCase).ToList();

               foreach (var filter in fileFilters)
               {
                  foreach (var dir in SearchSpec.StartDirectories)
                  {
                     Execute(new DirectoryInfo(dir), null, filter);
                  }
               }
            }
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
            return Greps[index];
         }
         catch { }

         return null;
      }

      /// <summary>
      ///   Check to see if the begin/end text around searched text is valid
      ///   for a whole word search
      /// </summary>
      /// <param name="beginText">Text in front of searched text</param>
      /// <param name="endText">Text behind searched text</param>
      /// <returns>True - valid, False - otherwise</returns>
      /// <history>
      ///   [Curtis_Beard]	   01/27/2005	Created
      /// 	[Curtis_Beard]	   02/17/2012	CHG: check for valid endswith as well
      /// </history>
      public static bool WholeWordOnly(string beginText, string endText)
      {
         return (IsValidText(beginText, true) && IsValidText(endText, false));
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
            OnSearchCancel();
         }
         catch (Exception ex)
         {
            OnSearchError(null, ex);

            OnSearchCancel();
         }
         finally
         {
            UnloadPlugins();
         }
      }

      /// <summary>
      /// Grep files for specified text.
      /// </summary>
      /// <param name="sourceDirectory">directory to begin grep</param>
      /// <param name="sourceDirectoryFilter">any directory specifications</param>
      /// <param name="sourceFileFilter">any file specifications</param>
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
      /// [Andrew_Radford]    13/08/2009  CHG: Remove searchtext param
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// [Curtis_Beard]	   10/10/2012	CHG: 3131609, signal when directories are filtered out due to system/hidden flag
      /// [Curtis_Beard]      09/17/2013    FIX: 45, check against a specific extension when only 3 characters is defined (*.txt can return things like *.txtabc due to .net GetFiles)
      /// [Curtis_Beard]      09/20/2013    CHG: use EnumerateFiles and EnumerateDirectories instead of GetFiles,GetDirectories to not lock up on waiting for those methods.
      /// </history>
      private void Execute(DirectoryInfo sourceDirectory, string sourceDirectoryFilter, string sourceFileFilter)
      {
         // skip directory if matches an exclusion item
         if (FileFilterSpec != null && FileFilterSpec.FilterItems != null)
         {
            var dirFilterItems = from f in FileFilterSpec.FilterItems where f.FilterType.Category == FilterType.Categories.Directory select f;
            foreach (FilterItem item in dirFilterItems)
            {
               string filterValue = string.Empty;
               if (item.ShouldExcludeDirectory(sourceDirectory, out filterValue))
               {
                  OnDirectoryFiltered(sourceDirectory, item, filterValue);
                  return;
               }
            }
         }

         // Check for File Filter
         string filePattern = "*";
         if (sourceFileFilter != null)
            filePattern = sourceFileFilter.Trim();

         // Check for Folder Filter
         string dirPattern = "*";
         if (sourceDirectoryFilter != null)
            dirPattern = sourceDirectoryFilter.Trim();

         //Search Every File for search text
         foreach (FileInfo SourceFile in sourceDirectory.EnumerateFiles(filePattern))
         {
            bool processFile = true;
            if (sourceFileFilter != null && !StriktMatch(SourceFile.Extension, sourceFileFilter.Trim()))
            {
               processFile = false;

               string filterValue = SourceFile.Extension;
               FilterItem filterItem = new FilterItem(new FilterType(FilterType.Categories.File, FilterType.SubCategories.Extension), string.Empty, FilterType.ValueOptions.None, false, true);
               OnFileFiltered(SourceFile, filterItem, filterValue);
            }

            if (processFile)
            {
               SearchFile(SourceFile);
            }
         }

         if (SearchSpec.SearchInSubfolders)
         {
            //Recursively go through every subdirectory and it's files (according to folder filter)
            foreach (var sourceSubDirectory in sourceDirectory.EnumerateDirectories(dirPattern))
            {
               try
               {
                  Execute(sourceSubDirectory, sourceDirectoryFilter, sourceFileFilter);
               }
               catch
               {
                  //skip any invalid directory
               }
            }
         }
      }

      /// <summary>
      /// Search the given file.
      /// </summary>
      /// <param name="SourceFile">FileInfo object to be searched</param>
      private void SearchFile(FileInfo SourceFile)
      {
         try
         {
            // skip any files that are filtered out
            FilterItem filterItem = null;
            string filterValue = string.Empty;
            if (ShouldFilterOut(SourceFile, FileFilterSpec, out filterItem, out filterValue))
            {
               OnFileFiltered(SourceFile, filterItem, filterValue);
            }
            else if (string.IsNullOrEmpty(SearchSpec.SearchText))
            {
               // return a 'file hit' if the search text is empty
               var _grepHit = new HitObject(SourceFile) { Index = Greps.Count };
               _grepHit.Add(string.Empty, string.Empty, 0);
               Greps.Add(_grepHit);
               OnFileHit(SourceFile, _grepHit.Index);
            }
            else
            {
               SearchFileContents(SourceFile);
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

      /// <summary>
      /// Return true if the file does not pass the fileFilterSpec, i.e should be skipped
      /// </summary>
      /// <param name="file">FileInfo object of current file</param>
      /// <param name="fileFilterSpec">Current file filter settings</param>
      /// <param name="filterItem">Item causing filtering, null if none</param>
      /// <param name="filterValue">Output of actual filter value</param>
      /// <returns>true if file does not pass file filter settings, false otherwise</returns>
      /// <history>
      /// [Andrew_Radford]    13/08/2009  Created
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private static bool ShouldFilterOut(FileInfo file, IFileFilterSpec fileFilterSpec, out FilterItem filterItem, out string filterValue)
      {
         filterItem = null;
         filterValue = string.Empty;

         if (fileFilterSpec.FilterItems != null && fileFilterSpec.FilterItems.Count > 0)
         {
            var fileFilterItems = from f in fileFilterSpec.FilterItems where f.FilterType.Category == FilterType.Categories.File select f;
            foreach (FilterItem item in fileFilterItems)
            {
               filterValue = string.Empty;
               if (item.ShouldExcludeFile(file, out filterValue))
               {
                  filterItem = item;
                  return true;
               }
            }
         }

         return false;
      }

      /// <summary>
      /// Search a given file for the searchText.
      /// </summary>
      /// <param name="file">FileInfo object for file to search for searchText</param>
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
      /// [Curtis_Beard]		10/09/2012	FIX: don't overwrite position when getting context lines
      /// [Curtis_Beard]		10/12/2012	FIX: get correct position when using whole word option
      /// [Curtis_Beard]		10/12/2012	CHG: 32, implement a hit count filter
      /// [Curtis_Beard]		10/31/2012	CHG: renamed to SearchFileContents, remove parameter searchText
      /// [Curtis_Beard]		08/19/2014	FIX: 57, escape search text when whole word is enabled but not regular expressions
      /// [Curtis_Beard]      10/27/2014	CHG: 85, remove leading white space, remove use of newline so each line is in hit object
      /// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
      /// </history>
      private void SearchFileContents(FileInfo file)
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
         string _contextSpacer = string.Empty;
         string _spacer;
         int userFilterCount = 0;

         try
         {
            #region Plugin Processing

            if (Plugins != null)
            {
               for (int i = 0; i < Plugins.Count; i++)
               {
                  // find a valid plugin for this file type
                  if (Plugins[i].Enabled && Plugins[i].Plugin.IsAvailable)
                  {
                     // detect if plugin supports extension
                     bool isFound = Plugins[i].Plugin.IsFileSupported(file);

                     // if extension not supported try another plugin
                     if (!isFound)
                        continue;

                     Exception pluginEx = null;

                     // load plugin and perform grep
                     if (Plugins[i].Plugin.Load())
                     {
                        OnSearchingFileByPlugin(Plugins[i].Plugin.Name);
                        _grepHit = Plugins[i].Plugin.Grep(file, SearchSpec, ref pluginEx);
                     }
                     else
                     {
                        OnSearchError(file, new Exception(string.Format("Plugin {0} failed to load.", Plugins[i].Plugin.Name)));
                     }

                     Plugins[i].Plugin.Unload();

                     // if the plugin processed successfully
                     if (pluginEx == null)
                     {
                        // check for a hit
                        if (_grepHit != null)
                        {
                           // only perform is not using negation
                           if (!SearchSpec.UseNegation)
                           {
                              if (DoesPassHitCountCheck(_grepHit))
                              {
                                 _grepHit.Index = Greps.Count;
                                 Greps.Add(_grepHit);
                                 OnFileHit(file, _grepHit.Index);

                                 if (SearchSpec.ReturnOnlyFileNames)
                                    _grepHit.SetHitCount();

                                 OnLineHit(_grepHit, _grepHit.Index);
                              }
                           }
                        }
                        else if (SearchSpec.UseNegation)
                        {
                           // no hit but using negation so create one
                           _grepHit = new HitObject(file) { Index = Greps.Count };
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
            #endregion

            #region Encoding Detection

            string usedEncoder = string.Empty;
            System.Text.Encoding encoding = null;

            //
            // find a user specified file encoding
            //
            FileEncoding fileEncoding = SearchSpec.FileEncodings != null && SearchSpec.FileEncodings.Count > 0 ? 
               (from f in SearchSpec.FileEncodings where f.FilePath.Equals(file.FullName, StringComparison.InvariantCultureIgnoreCase) && f.Enabled select f).ToList().FirstOrDefault()
               :  null;
            if (fileEncoding != null)
            {
               usedEncoder = "User";
               encoding = fileEncoding.Encoding;
            }
            else
            {
               //
               // no user specified encoding, try to detect one
               //
               byte[] sampleBytes;

               //Check if can read first
               try
               {
                  sampleBytes = EncodingTools.ReadFileContentSample(file.FullName);
               }
               catch (Exception ex)
               {
                  // can't read file
                  OnSearchError(file, ex);
                  return;
               }

               usedEncoder = string.Empty;
               encoding = DetectEncoding(sampleBytes, out usedEncoder);
            }

            if (encoding == null)
            {
               // Could not detect file encoding
               OnSearchError(file, new Exception("Could not detect file encoding."));
               return;
            }

            OnFileEncodingDetected(file, encoding, usedEncoder);

            // process all encoding detectors and display results to output window
            //var values = EncodingDetector.DetectAll(sampleBytes);
            //if (values.Count > 0)
            //{
            //   System.Diagnostics.Debug.WriteLine(string.Format("File: {0}", file.FullName));
            //   foreach (var value in values)
            //   {
            //      System.Diagnostics.Debug.WriteLine(string.Format("Encoding: {0} ({1})", value.Encoding != null ? value.Encoding.EncodingName : "None", value.Option.ToString()));
            //   }
            //   System.Diagnostics.Debug.WriteLine(Environment.NewLine);
            //}

            #endregion

            _stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            _reader = new StreamReader(_stream, encoding);

            // Default spacer (left margin) values. If Line Numbers are on,
            // these will be reset within the loop to include line numbers.
            if (SearchSpec.ContextLines > 0)
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
                  if (SearchSpec.UseRegularExpressions)
                  {
                     _posInStr = -1;
                     if (textLine.Length > 0)
                     {
                        if (SearchSpec.UseCaseSensitivity && SearchSpec.UseWholeWordMatching)
                        {
                           _regularExp = new Regex("\\b" + SearchSpec.SearchText + "\\b");
                           _regularExpCol = _regularExp.Matches(textLine);
                        }
                        else if (SearchSpec.UseCaseSensitivity)
                        {
                           _regularExp = new Regex(SearchSpec.SearchText);
                           _regularExpCol = _regularExp.Matches(textLine);
                        }
                        else if (SearchSpec.UseWholeWordMatching)
                        {
                           _regularExp = new Regex("\\b" + SearchSpec.SearchText + "\\b", RegexOptions.IgnoreCase);
                           _regularExpCol = _regularExp.Matches(textLine);
                        }
                        else
                        {
                           _regularExp = new Regex(SearchSpec.SearchText, RegexOptions.IgnoreCase);
                           _regularExpCol = _regularExp.Matches(textLine);
                        }

                        if (_regularExpCol.Count > 0)
                        {
                           if (SearchSpec.UseNegation)
                              _hitOccurred = true;

                           _posInStr = 1;
                        }
                     }
                  }
                  else
                  {
                     if (SearchSpec.UseCaseSensitivity)
                     {
                        // Need to escape these characters in SearchText:
                        // < $ + * [ { ( ) .
                        // with a preceeding \

                        // If we are looking for whole worlds only, perform the check.
                        if (SearchSpec.UseWholeWordMatching)
                        {
                           _regularExp = new Regex("\\b" + Regex.Escape(SearchSpec.SearchText) + "\\b");
                           Match mtc = _regularExp.Match(textLine);
                           if (mtc != null && mtc.Success)
                           {
                              if (SearchSpec.UseNegation)
                                 _hitOccurred = true;

                              _posInStr = mtc.Index;
                           }
                           else
                              _posInStr = -1;
                        }
                        else
                        {
                           _posInStr = textLine.IndexOf(SearchSpec.SearchText);

                           if (SearchSpec.UseNegation && _posInStr > -1)
                              _hitOccurred = true;
                        }
                     }
                     else
                     {
                        // If we are looking for whole worlds only, perform the check.
                        if (SearchSpec.UseWholeWordMatching)
                        {
                           _regularExp = new Regex("\\b" + Regex.Escape(SearchSpec.SearchText) + "\\b", RegexOptions.IgnoreCase);
                           Match mtc = _regularExp.Match(textLine);
                           if (mtc != null && mtc.Success)
                           {
                              if (SearchSpec.UseNegation)
                                 _hitOccurred = true;

                              _posInStr = mtc.Index;
                           }
                           else
                              _posInStr = -1;
                        }
                        else
                        {
                           _posInStr = textLine.ToLower().IndexOf(SearchSpec.SearchText.ToLower());

                           if (SearchSpec.UseNegation && _posInStr > -1)
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
                     if (SearchSpec.UseNegation)
                        break;

                     // create new hit and add to collection
                     if (_grepHit == null)
                     {
                        _grepHit = new HitObject(file) { Index = Greps.Count, DetectedEncoding = encoding };
                        Greps.Add(_grepHit);
                     }

                     // don't show until passes count check
                     if (!_fileNameDisplayed && DoesPassHitCountCheck(_grepHit))
                     {
                        OnFileHit(file, _grepHit.Index);

                        _fileNameDisplayed = true;
                     }

                     // If we are only showing filenames, go to the next file.
                     if (SearchSpec.ReturnOnlyFileNames)
                     {
                        if (!_fileNameDisplayed)
                        {
                           OnFileHit(file, _grepHit.Index);

                           _fileNameDisplayed = true;
                        }

                        //notify that at least 1 hit is in file
                        _grepHit.SetHitCount();
                        OnLineHit(_grepHit, _grepHit.Index);

                        break;
                     }

                     // Set up line number, or just an indention in front of the line.
                     if (SearchSpec.IncludeLineNumbers)
                     {
                        _spacer = "(" + _lineNumber.ToString().Trim();
                        if (_spacer.Length <= 5)
                           _spacer = _spacer + new string(char.Parse(" "), 6 - _spacer.Length);

                        _spacer = _spacer + ") ";
                        _contextSpacer = "(" + new string(char.Parse(" "), _spacer.Length - 3) + ") ";
                     }

                     // Display context lines if applicable.
                     if (SearchSpec.ContextLines > 0 && _lastHit == 0)
                     {
                        if (_grepHit.LineCount > 0)
                        {
                           // Insert a blank space before the context lines.
                           int _pos = _grepHit.Add(string.Empty, string.Empty, -1);

                           if (DoesPassHitCountCheck(_grepHit))
                           {
                              OnLineHit(_grepHit, _pos);
                           }
                        }

                        // Display preceeding n context lines before the hit.
                        int tempPosInStr = _posInStr;
                        for (tempPosInStr = SearchSpec.ContextLines; tempPosInStr >= 1; tempPosInStr--)
                        {
                           _contextIndex = _contextIndex + 1;
                           if (_contextIndex > SearchSpec.ContextLines)
                              _contextIndex = 1;

                           // If there is a match in the first one or two lines,
                           // the entire preceeding context may not be available.
                           if (_lineNumber > tempPosInStr)
                           {
                              // Add the context line.
                              int _pos = _grepHit.Add(_contextSpacer, _context[_contextIndex], _lineNumber - tempPosInStr);

                              if (DoesPassHitCountCheck(_grepHit))
                              {
                                 OnLineHit(_grepHit, _pos);
                              }
                           }
                        }
                     }

                     _lastHit = SearchSpec.ContextLines;

                     //
                     // Add the actual "hit".
                     //
                     // set first hit column position
                     if (SearchSpec.UseRegularExpressions)
                     {
                        // zero based
                        _posInStr = _regularExpCol[0].Index;
                     }
                     _posInStr += 1;
                     int _index = _grepHit.Add(_spacer, textLine, _lineNumber, _posInStr);

                     if (SearchSpec.UseRegularExpressions)
                     {
                        _grepHit.SetHitCount(_regularExpCol.Count);
                     }
                     else
                     {
                        //determine number of hits in single line
                        _grepHit.SetHitCount(RetrieveLineHitCount(textLine, SearchSpec.SearchText));
                     }

                     if (DoesPassHitCountCheck(_grepHit))
                     {
                        OnLineHit(_grepHit, _index);
                     }
                  }
                  else if (_lastHit > 0 && SearchSpec.ContextLines > 0)
                  {
                     //***************************************************
                     // We didn't find a hit, but since lastHit is > 0, we
                     // need to display this context line.
                     //***************************************************
                     int _index = _grepHit.Add(_contextSpacer, textLine, _lineNumber);

                     if (DoesPassHitCountCheck(_grepHit))
                     {
                        OnLineHit(_grepHit, _index);
                     }
                     _lastHit -= 1;

                  } // Found a hit or not.

                  // If we are showing context lines, keep the last n lines.
                  if (SearchSpec.ContextLines > 0)
                  {
                     if (_contextIndex == SearchSpec.ContextLines)
                        _contextIndex = 1;
                     else
                        _contextIndex += 1;

                     _context[_contextIndex] = textLine;
                  }
               }
            }
            while (true);

            // send event file/line hit if we haven't yet but it should be
            if (!_fileNameDisplayed && _grepHit != null && DoesPassHitCountCheck(_grepHit))
            {
               // need to display it
               OnFileHit(file, _grepHit.Index);
               OnLineHit(_grepHit, _grepHit.Index);
            }

            // send event for file filtered if it fails the file hit count filter
            if (!SearchSpec.UseNegation && !SearchSpec.ReturnOnlyFileNames && _grepHit != null && !DoesPassHitCountCheck(_grepHit))
            {
               // remove from grep collection only if
               // not negation
               // not filenames only
               // actually have a hit
               // doesn't pass the hit count filter
               Greps.RemoveAt(Greps.Count - 1);

               string filterValue = _grepHit.HitCount.ToString();
               FilterItem filterItem = new FilterItem(new FilterType(FilterType.Categories.File, FilterType.SubCategories.MinimumHitCount), userFilterCount.ToString(), FilterType.ValueOptions.None, false, true);
               OnFileFiltered(file, filterItem, filterValue);
            }

            //
            // Check for no hits through out the file
            //
            if (SearchSpec.UseNegation && _hitOccurred == false)
            {
               //add the file to the hit list
               if (!_fileNameDisplayed)
               {
                  _grepHit = new HitObject(file) { Index = Greps.Count, DetectedEncoding = encoding };
                  Greps.Add(_grepHit);
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
         string _end;
         int _pos = -1;

         string _tempLine = line;

         // attempt to locate the text in the line
         if (SearchSpec.UseCaseSensitivity)
            _pos = _tempLine.IndexOf(searchText);
         else
            _pos = _tempLine.ToLower().IndexOf(searchText.ToLower());

         while (_pos > -1)
         {
            // retrieve parts of text
            string _begin = _tempLine.Substring(0, _pos);
            _end = _tempLine.Substring(_pos + searchText.Length);

            // do a check to see if begin and end are valid for wholeword searches
            bool _highlight;
            if (SearchSpec.UseWholeWordMatching)
               _highlight = WholeWordOnly(_begin, _end);
            else
               _highlight = true;

            // found a hit
            if (_highlight)
               _count += 1;

            // Check remaining string for other hits in same line
            if (SearchSpec.UseCaseSensitivity)
               _pos = _end.IndexOf(searchText);
            else
               _pos = _end.ToLower().IndexOf(searchText.ToLower());

            // update the temp line with the next part to search (if any)
            _tempLine = _end;
         }

         return _count;
      }


      static readonly List<string> validTexts = new List<string> { " ", "<", ">", "$", "+", "*", "[", "]", "{", "}", "(", ")", ".", "?", "!", ",", ":", ";", "-", "\\", "/", "'", "\"", Environment.NewLine, "\r\n", "\r", "\n" };

      /// <summary>
      /// Validate a start text.
      /// </summary>
      /// <param name="text">text to validate</param>
      /// <returns>True - valid, False - otherwise</returns>
      /// <history>
      /// [Curtis_Beard]		12/06/2005	Created
      /// [Curtis_Beard]		02/09/2007	FIX: 1655533, update whole word matching
      /// [Curtis_Beard]		08/21/2007	ADD: '/' character and Environment.NewLine
      /// [Andrew_Radford]    09/08/2009  CHG: refactored to use list, combined begin and end text methods
      /// [Curtis_Beard]		02/17/2012	CHG: check end text as well
      /// [Curtis_Beard]		03/24/2014	FIX: 41/53, add ending >, ], }, ) values
      /// </history>
      private static bool IsValidText(string text, bool checkEndText)
      {
         if (string.IsNullOrEmpty(text))
            return true;

         bool found = false;
         validTexts.ForEach(s =>
         {
            if (checkEndText)
            {
               if (text.EndsWith(s))
                  found = true;
            }
            else if (text.StartsWith(s))
               found = true;
         });
         return found;
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
      /// Determines if current hit count passes the file hit count filter.
      /// </summary>
      /// <param name="hit">Current HitObject</param>
      /// <returns>true if hit count valid, false if not</returns>
      /// <history>
      /// [Curtis_Beard]      10/12/2012  Created: 32, implement file hit count
      /// </history>
      private bool DoesPassHitCountCheck(HitObject hit)
      {
         if (userFilterCount <= 0)
            return true;

         if (userFilterCount > 0 && (hit != null && hit.HitCount >= userFilterCount))
            return true;

         return false;
      }

      /// <summary>
      /// Determines if file extension is valid against the search pattern
      /// </summary>
      /// <param name="fileExtension">Current file extension</param>
      /// <param name="searchPattern">Current file search pattern</param>
      /// <returns>true if valid, false otherwise</returns>
      /// <history>
      /// [Curtis_Beard]      09/17/2013    FIX: 45, check against a specific extension when only 3 characters is defined (*.txt can return things like *.txtabc due to .net GetFiles)
      /// [Curtis_Beard]      05/08/2014    FIX: 55, handle when no . in searchPattern (e.g. *)
      /// </history>
      private bool StriktMatch(string fileExtension, string searchPattern)
      {
         bool isStriktMatch = false;

         int index = searchPattern.LastIndexOf('.');
         string extension = index > -1 ? searchPattern.Substring(index) : searchPattern;

         if (String.IsNullOrEmpty(extension))
         {
            isStriktMatch = true;
         }
         else if (extension.IndexOfAny(new char[] { '*', '?' }) != -1)
         {
            isStriktMatch = true;
         }
         else if (String.Compare(fileExtension, extension, true) == 0)
         {
            isStriktMatch = true;
         }
         else
         {
            isStriktMatch = false;
         }

         return isStriktMatch;
      }

      /// <summary>
      /// Detect file encoding using passed in sample bytes of file.
      /// If DetectFileEncoding is false, fall back to using Encoding.Default like before.
      /// </summary>
      /// <param name="sampleBytes">sample byts of current file</param>
      /// <param name="usedEncoder">The used encoder name</param>
      /// <returns>Encoding detected, otherwise if disabled use Encoding.Default</returns>
      /// <history>
      /// [Curtis_Beard]	   02/04/2014	ADD: 66, option to detect file encoding
      /// </history>
      private System.Text.Encoding DetectEncoding(byte[] sampleBytes, out string usedEncoder)
      {
         if (SearchSpec.DetectFileEncoding)
         {
            return EncodingDetector.Detect(sampleBytes, out usedEncoder, defaultEncoding: System.Text.Encoding.Default);
         }

         // this is the encoding previous versions used (so it is an option to return to that)
         usedEncoder = "Default";
         return System.Text.Encoding.Default;
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

      /// <summary>
      /// Raise file filtered event.
      /// </summary>
      /// <param name="file">FileInfo object</param>
      /// <param name="filterItem">FilterItem file was filtered on</param>
      /// <param name="value">Current value causing filtering</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      protected virtual void OnFileFiltered(FileInfo file, FilterItem filterItem, string value)
      {
         if (FileFiltered != null)
         {
            FileFiltered(file, filterItem, value);
         }
      }

      /// <summary>
      /// Raise directory filtered event.
      /// </summary>
      /// <param name="dir">DirectoryInfo object</param>
      /// <param name="filterItem">FilterItem directory was filtered on</param>
      /// <param name="value">Current value causing filtering</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      protected virtual void OnDirectoryFiltered(DirectoryInfo dir, FilterItem filterItem, string value)
      {
         if (DirectoryFiltered != null)
         {
            DirectoryFiltered(dir, filterItem, value);
         }
      }

      /// <summary>
      /// Raise searching file by plugin event.
      /// </summary>
      /// <param name="pluginName">Name of plugin</param>
      /// <history>
      /// [Curtis_Beard]	   10/16/2012	Initial
      /// </history>
      protected virtual void OnSearchingFileByPlugin(string pluginName)
      {
         if (SearchingFileByPlugin != null)
         {
            SearchingFileByPlugin(pluginName);
         }
      }

      /// <summary>
      /// Raise file encoding detected event.
      /// </summary>
      /// <param name="file">Current FileInfo</param>
      /// <param name="encoding">Detected encoding</param>
      /// <param name="encoderName">The detected encoder's name</param>
      /// <history>
      /// [Curtis_Beard]	   12/01/2014	Initial
      /// </history>
      protected virtual void OnFileEncodingDetected(FileInfo file, System.Text.Encoding encoding, string encoderName)
      {
         if (FileEncodingDetected != null)
         {
            FileEncodingDetected(file, encoding, encoderName);
         }
      }
      #endregion
   }
}
