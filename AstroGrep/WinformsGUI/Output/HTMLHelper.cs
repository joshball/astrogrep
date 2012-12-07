using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using AstroGrep.Core;
using libAstroGrep;

namespace AstroGrep.Output
{
	/// <summary>
	/// Helper class when outputing results to HTML.
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
	/// [Curtis_Beard]      09/05/2006	Created
    /// [Curtis_Beard]		10/30/2012	CHG: use AstroGrep.Output for namespace
	/// </history>
	public class HTMLHelper
	{
		private HTMLHelper()
		{ }

      /// <summary>
      /// Retrieves a given file's contents from the embed resource.
      /// </summary>
      /// <param name="fileName">string containing resource file to retrieve.</param>
      /// <history>
      /// [Curtis_Beard]		09/01/2006	Created
      /// [Curtis_Beard]		10/11/2006	CHG: Close stream
      /// </history>
      public static string GetContents(string fileName)
      {
         try
         {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            string _name = _assembly.GetName().Name;
            string _contents = string.Empty;

            Stream _stream = _assembly.GetManifestResourceStream(string.Format("{0}.Output.{1}", _name, fileName));

            if (_stream != null)
            {
               using (StreamReader _reader = new StreamReader(_stream))
               {
                  _contents = _reader.ReadToEnd();
               }
               _stream.Close();
            }
            _assembly = null;

            return _contents;            
         }
         catch {}

         return string.Empty;
      }

      /// <summary>
      /// Returns the given line with the search text highlighted.
      /// </summary>
      /// <param name="line">Line to check</param>
      /// <param name="grep">Grep Object containing options</param>
      /// <returns>Line with search text highlighted</returns>
      /// <history>
      /// [Curtis_Beard]		09/05/2006	Created
      /// </history>
      public static string GetHighlightLine(string line, Grep grep)
      {
         string newLine;

         if (grep.SearchSpec.UseRegularExpressions)
            newLine = HighlightRegEx(line, grep);
         else
            newLine = HighlightNormal(line, grep);

         return newLine + "<br />";
      }

      /// <summary>
      /// Replaces all the css holders in the given text.
      /// </summary>
      /// <param name="css">Text containing holders</param>
      /// <returns>Text with holders replaced</returns>
      /// <history>
      /// [Curtis_Beard]		09/05/2006	Created
      /// [Curtis_Beard]		01/31/2012	ADD: support highlight back color option
      /// </history>
      public static string ReplaceCssHolders(string css)
      {
         css = css.Replace("%%resultback%%", System.Drawing.ColorTranslator.ToHtml(Convertors.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsBackColor)));
         css = css.Replace("%%resultfore%%", System.Drawing.ColorTranslator.ToHtml(Convertors.ConvertStringToColor(AstroGrep.Core.GeneralSettings.ResultsForeColor)));
         css = css.Replace("%%highlightfore%%", System.Drawing.ColorTranslator.ToHtml(Convertors.ConvertStringToColor(AstroGrep.Core.GeneralSettings.HighlightForeColor)));
         css = css.Replace("%%highlightback%%", System.Drawing.ColorTranslator.ToHtml(Convertors.ConvertStringToColor(AstroGrep.Core.GeneralSettings.HighlightBackColor)));

         return css;
      }

      /// <summary>
      /// Replaces all the search option holders in the given text.
      /// </summary>
      /// <param name="text">Text containing holders</param>
      /// <param name="grep">Grep object containing settings</param>
      /// <returns>Text with holders replaced</returns>
      /// <history>
      /// [Curtis_Beard]		09/05/2006	Created
      /// [Curtis_Beard]		01/31/2012	ADD: display for additional options (skip hidden/system options, search paths, modified dates, file sizes)
      /// [Curtis_Beard]		10/30/2012	CHG: use year replacement for copyright
      /// [Curtis_Beard]		10/30/2012	ADD: file hit count, CHG: recurse to Subfolders
      /// </history>
      public static string ReplaceSearchOptions(string text, Grep grep)
      {
         var spec = grep.SearchSpec;

         text = text.Replace("%%year%%", DateTime.Now.Year.ToString());
         text = text.Replace("%%searchpaths%%", "Search Path(s): " + string.Join(", ", spec.StartDirectories));
         text = text.Replace("%%filetypes%%", "File Types: " + grep.FileFilterSpec.FileFilter);
         text = text.Replace("%%regex%%", "Regular Expressions: " + spec.UseRegularExpressions);
         text = text.Replace("%%casesen%%", "Case Sensitive: " + spec.UseCaseSensitivity);
         text = text.Replace("%%wholeword%%", "Whole Word: " + spec.UseWholeWordMatching);
         text = text.Replace("%%recurse%%", "Subfolders: " + spec.SearchInSubfolders);
         text = text.Replace("%%filenameonly%%", "Show File Names Only: " + spec.ReturnOnlyFileNames);
         text = text.Replace("%%negation%%", "Negation: " + spec.UseNegation);
         text = text.Replace("%%linenumbers%%", "Line Numbers: " + spec.IncludeLineNumbers);
         text = text.Replace("%%contextlines%%", "Context Lines: " + spec.ContextLines);
         text = text.Replace("%%skiphidden%%", "Skip Hidden Files/Directories: " + grep.FileFilterSpec.SkipHiddenFiles);
         text = text.Replace("%%skipsystem%%", "Skip System Files/Directories: " + grep.FileFilterSpec.SkipSystemFiles);

         string modDateStart = string.Empty;
         if (grep.FileFilterSpec.DateModifiedStart != DateTimePicker.MinimumDateTime)
         {
            modDateStart = "Modified Date Start: " + grep.FileFilterSpec.DateModifiedStart + "<br/>";
         }
         text = text.Replace("%%moddatestart%%", modDateStart);

         string modDateEnd = string.Empty;
         if (grep.FileFilterSpec.DateModifiedEnd != DateTimePicker.MaximumDateTime)
         {
            modDateEnd = "Modified Date End: " + grep.FileFilterSpec.DateModifiedEnd + "<br/>";
         }         
         text = text.Replace("%%moddateend%%", modDateEnd);

         string minSize = string.Empty;
         if (grep.FileFilterSpec.FileSizeMin != long.MinValue)
         {
            minSize = "Min File Size: " + grep.FileFilterSpec.FileSizeMin + "<br/>";
         }
         text = text.Replace("%%filesizemin%%", minSize);

         string maxSize = string.Empty;
         if (grep.FileFilterSpec.FileSizeMax != long.MaxValue)
         {
            maxSize = "Max File Size: " + grep.FileFilterSpec.FileSizeMax + "<br/>";
         }         
         text = text.Replace("%%filesizemax%%", maxSize);

         string fileHitCount = string.Empty;
         if (grep.FileFilterSpec.FileHitCount > 0)
         {
             fileHitCount = "File Hit Count: " + grep.FileFilterSpec.FileHitCount + "<br/>";
         }
         text = text.Replace("%%filehitcount%%", fileHitCount);

         text = text.Replace("%%totalfiles%%", grep.Greps.Count.ToString());
         text = text.Replace("%%searchterm%%", spec.SearchText);

         if (spec.UseNegation)
            text = text.Replace("%%usenegation%%", "not ");
         else
            text = text.Replace("%%usenegation%%", string.Empty);

         return text;
      }

      #region Private Methods
      
      /// <summary>
      /// Returns the given line with the search text highlighted.
      /// </summary>
      /// <param name="line">Line to check</param>
      /// <param name="grep">Grep Object containing options</param>
      /// <returns>Line with search text highlighted</returns>
      /// <history>
      /// [Curtis_Beard]		09/05/2006	Created
      /// </history>
      private static string HighlightNormal(string line, Grep grep)
      {
         var _searchText = grep.SearchSpec.SearchText;
         int _pos = 0;
         string _newLine = string.Empty;

         // Retrieve hit text
         string _textToSearch = line;
         var _tempLine = _textToSearch;

         // attempt to locate the text in the line
         if (grep.SearchSpec.UseCaseSensitivity)
            _pos = _tempLine.IndexOf(_searchText);
         else
            _pos = _tempLine.ToLower().IndexOf(_searchText.ToLower());

         if (_pos > -1)
         {
            while (_pos > -1)
            {
               bool _highlight = false;

               //retrieve parts of text
               var _begin = _tempLine.Substring(0, _pos);
               var _text = _tempLine.Substring(_pos, _searchText.Length);
               var _end = _tempLine.Substring(_pos + _searchText.Length);

               _newLine += _begin;

               // do a check to see if begin and end are valid for wholeword searches
               if (grep.SearchSpec.UseWholeWordMatching)
                  _highlight = Grep.WholeWordOnly(_begin, _end);
               else
                  _highlight = true;

               // set highlight color for searched text
               if (_highlight)
                  _newLine += string.Format("<span class=\"searchtext\">{0}</span>", _text);
               else
                  _newLine += _text;

               // Check remaining string for other hits in same line
               if (grep.SearchSpec.UseCaseSensitivity)
                  _pos = _end.IndexOf(_searchText);
               else
                  _pos = _end.ToLower().IndexOf(_searchText.ToLower());

               // set default color for end, if no more hits in line
               _tempLine = _end;
               if (_pos < 0)
                  _newLine += _end;
            }
         }
         else
            _newLine += _textToSearch;
         
         return _newLine;
      }

      /// <summary>
      /// Returns the given line with the search text highlighted.
      /// </summary>
      /// <param name="line">Line to check</param>
      /// <param name="grep">Grep Object containing options</param>
      /// <returns>Line with search text highlighted</returns>
      /// <history>
      /// [Curtis_Beard]		09/05/2006	Created
      /// [Curtis_Beard]	   05/18/2006	FIX: 1723815, use correct whole word matching regex
      /// </history>
      private static string HighlightRegEx(string line, Grep grep)
      {
          string _tempstring;
         int _lastPos = 0;
         int _counter = 0;
         Regex _regEx;
         MatchCollection _col;
         Match _item;
         string _newLine = string.Empty;

         //Retrieve hit text
         string _textToSearch = line;

         // find all reg ex matches in line
         if (grep.SearchSpec.UseCaseSensitivity && grep.SearchSpec.UseWholeWordMatching)
         {
             _regEx = new Regex("\\b" + grep.SearchSpec.SearchText + "\\b");
            _col = _regEx.Matches(_textToSearch);
         }
         else if (grep.SearchSpec.UseCaseSensitivity)
         {
             _regEx = new Regex(grep.SearchSpec.SearchText);
            _col = _regEx.Matches(_textToSearch);
         }
         else if (grep.SearchSpec.UseWholeWordMatching)
         {
             _regEx = new Regex("\\b" + grep.SearchSpec.SearchText + "\\b", RegexOptions.IgnoreCase);
            _col = _regEx.Matches(_textToSearch);
         }
         else
         {
             _regEx = new Regex(grep.SearchSpec.SearchText, RegexOptions.IgnoreCase);
            _col = _regEx.Matches(_textToSearch);
         }

         // loop through the matches
         _lastPos = 0;
         for (_counter = 0; _counter < _col.Count; _counter++)
         {
            _item = _col[_counter];

            // check for empty string to prevent assigning nothing to selection text preventing
            //  a system beep
            _tempstring = _textToSearch.Substring(_lastPos, _item.Index - _lastPos);
            if (!_tempstring.Equals(string.Empty))
               _newLine += _tempstring;

            // set the hit text
            _newLine += string.Format("<span class=\"searchtext\">{0}</span>", _textToSearch.Substring(_item.Index, _item.Length));

            // set the end text
            if (_counter + 1 >= _col.Count)
            {
               // no more hits so just set the rest
               _newLine += _textToSearch.Substring(_item.Index + _item.Length);

               _lastPos = _item.Index + _item.Length;
            }
            else
            {
               // another hit so just set inbetween
               _newLine += _textToSearch.Substring(_item.Index + _item.Length, _col[_counter + 1].Index - (_item.Index + _item.Length));
               _lastPos = _col[_counter + 1].Index;
            }
         }

         if (_col.Count == 0)
         {
            // no match, just a context line
            _newLine += _textToSearch;
         }

         return _newLine;
      }
      #endregion
	}
}
