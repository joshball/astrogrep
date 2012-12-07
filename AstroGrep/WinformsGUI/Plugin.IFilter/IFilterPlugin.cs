using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using libAstroGrep;
using libAstroGrep.Plugin;

namespace Plugin.IFilter
{
   /// <summary>
   /// Used to search any file that has an iFilter module loaded in the system.
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
   /// [Curtis_Beard]      10/17/2012  Created
   /// </history>
   public class IFilterPlugin : IAstroGrepPlugin
   {
      private bool __IsAvailable;

      /// <summary>
      /// Gets the name of the plugin.
      /// </summary>
      public string Name
      {
         get { return "File Handlers"; }
      }

      /// <summary>
      /// Gets the version of the plugin.
      /// </summary>
      public string Version
      {
         get { return "1.0.0"; }
      }

      /// <summary>
      /// Gets the author of the plugin.
      /// </summary>
      public string Author
      {
         get { return "The AstroGrep Team"; }
      }

      /// <summary>
      /// Gets the description of the plugin.
      /// </summary>
      public string Description
      {
         get { return "Searches documents using the system file handler (IFilter) for the given file extension.  Currently doesn't support Context lines or Line Numbers.  Can be slower."; }
      }

      /// <summary>
      /// Gets the valid extensions for this grep type.
      /// </summary>
      /// <remarks>Comma separated list of strings.</remarks>
      public string Extensions
      {
         get { return "File Handlers"; }
      }

      /// <summary>
      /// Checks to see if the plugin is available on this system.
      /// </summary>
      public bool IsAvailable
      {
         get { return __IsAvailable; }
      }

      /// <summary>
      /// Initializes a new instance of the IFilterPlugin class.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      10/17/2012  Created
      /// </history>
      public IFilterPlugin()
      {
         __IsAvailable = true;
      }

      /// <summary>
      /// Handles disposing of the object.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      10/17/2012  Created
      /// </history>
      public void Dispose()
      {
         __IsAvailable = false;
      }

      /// <summary>
      /// Handles destruction of the object.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      10/17/2012  Created
      /// </history>
      ~IFilterPlugin()
      {
         this.Dispose();
      }

      /// <summary>
      /// Loads the plugin and prepares it for a grep.
      /// </summary>
      /// <returns>returns true if (successfully loaded or false otherwise</returns>
      /// <history>
      /// [Curtis_Beard]      10/17/2012  Created
      /// </history>
      public bool Load()
      {
         return Load(false);
      }

      /// <summary>
      /// Loads the plugin and prepares it for a grep.
      /// </summary>
      /// <param name="visible">true makes underlying application visible, false is make it hidden</param>
      /// <returns>returns true if (successfully loaded or false otherwise</returns>
      /// <history>
      /// [Curtis_Beard]      10/17/2012  Created
      /// </history>
      public bool Load(bool visible)
      {
         return true;
      }

      /// <summary>
      /// Unloads Microsoft Word.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      10/17/2012  Created
      /// </history>
      public void Unload()
      {
         
      }

      /// <summary>
      /// Determines if given file is supported by current plugin.
      /// </summary>
      /// <param name="file">Current FileInfo object</param>
      /// <returns>True if supported, False if not supported</returns>
      /// <history>
      /// [Curtis_Beard]      10/17/2012  Created
      /// </history>
      public bool IsFileSupported(FileInfo file)
      {
         return Parser.IsParseable(file.Name);
      }

      /// <summary>
      /// Searches the given file for the given search text.
      /// </summary>
      /// <param name="file">FileInfo object</param>
      /// <param name="searchSpec">ISearchSpec interface value</param>
      /// <param name="ex">Exception holder if error occurs</param>
      /// <returns>Hitobject containing grep results, null if on error</returns>
      /// <history>
      /// [Curtis_Beard]      10/17/2012  Created
      /// </history>
      public HitObject Grep(FileInfo file, ISearchSpec searchSpec, ref Exception ex)
      {
         return Grep(file.FullName, searchSpec, ref ex);
      }

      /// <summary>
      /// Searches the given file for the given search text.
      /// </summary>
      /// <param name="path">Fully qualified file path</param>
      /// <param name="searchSpec">ISearchSpec interface value</param>
      /// <param name="ex">Exception holder if error occurs</param>
      /// <returns>Hitobject containing grep results, null on error</returns>
      /// <history>
      /// [Curtis_Beard]      10/17/2012  Created
      /// </history>
      public HitObject Grep(string path, ISearchSpec searchSpec, ref Exception ex)
      {
         // initialize Exception object to null
         ex = null;
         HitObject hit = null;

         if (Parser.IsParseable(path))
         {
            string fileContent = Parser.Parse(path);

            if (!string.IsNullOrEmpty(fileContent))
            {
               string[] lines = fileContent.Split(new char[] { '\n', '\r' });
               for (int i = 0; i < lines.Length; i++)
               {
                  string line = lines[i];

                  if (searchSpec.UseRegularExpressions)
                  {
                     Regex reg;
                     MatchCollection regCol;

                     if (searchSpec.UseCaseSensitivity && searchSpec.UseWholeWordMatching)
                     {
                        reg = new Regex("\\b" + searchSpec.SearchText + "\\b");
                        regCol = reg.Matches(line);
                     }
                     else if (searchSpec.UseCaseSensitivity)
                     {
                        reg = new Regex(searchSpec.SearchText);
                        regCol = reg.Matches(line);
                     }
                     else if (searchSpec.UseWholeWordMatching)
                     {
                        reg = new Regex("\\b" + searchSpec.SearchText + "\\b", RegexOptions.IgnoreCase);
                        regCol = reg.Matches(line);
                     }
                     else
                     {
                        reg = new Regex(searchSpec.SearchText, RegexOptions.IgnoreCase);
                        regCol = reg.Matches(line);
                     }

                     if (regCol.Count > 0)
                     {
                        if (hit == null)
                        {
                           hit = new HitObject(path);

                           // found hit in file so just return 
                           if (searchSpec.ReturnOnlyFileNames)
                           {
                              break;
                           }
                        }

                        //hit.Add(line + Environment.NewLine, i, regCol[0].Index + 1);
                        hit.Add(line + Environment.NewLine, 0, 0);   // currently use 0,0 for all hits since we parsed out the text
                        hit.SetHitCount(regCol.Count);
                     }
                  }
                  else
                  {
                     int posInStr = -1;
                     if (searchSpec.UseCaseSensitivity)
                     {
                        // Need to escape these characters in SearchText:
                        // < $ + * [ { ( ) .
                        // with a preceeding \

                        // If we are looking for whole worlds only, perform the check.
                        if (searchSpec.UseWholeWordMatching)
                        {
                           Regex reg = new Regex("\\b" + searchSpec.SearchText + "\\b");
                           Match mtc = reg.Match(line);
                           if (mtc != null && mtc.Success)
                           {
                              posInStr = mtc.Index;
                           }
                        }
                        else
                        {
                           posInStr = line.IndexOf(searchSpec.SearchText);
                        }
                     }
                     else
                     {
                        // If we are looking for whole worlds only, perform the check.
                        if (searchSpec.UseWholeWordMatching)
                        {
                           Regex reg = new Regex("\\b" + searchSpec.SearchText + "\\b", RegexOptions.IgnoreCase);
                           Match mtc = reg.Match(line);
                           if (mtc != null && mtc.Success)
                           {
                              posInStr = mtc.Index;
                           }
                        }
                        else
                        {
                           posInStr = line.ToLower().IndexOf(searchSpec.SearchText.ToLower());
                        }
                     }

                     if (posInStr > -1)
                     {
                        if (hit == null)
                        {
                           hit = new HitObject(path);

                           // found hit in file so just return 
                           if (searchSpec.ReturnOnlyFileNames)
                           {
                              break;
                           }
                        }
                        //hit.Add(line + Environment.NewLine, i, posInStr + 1);
                        hit.Add(line + Environment.NewLine, 0, 0);   // currently use 0,0 for all hits since we parsed out the text
                        hit.SetHitCount(RetrieveLineHitCount(line, searchSpec));
                     }
                  }
               }
            }
         }

         return hit;
      }

      /// <summary>
      /// Retrieves the number of instances of searchText in the given line
      /// </summary>
      /// <param name="line">Line of text to search</param>
      /// <param name="searchSpec">ISearchSpec interface value</param>
      /// <returns>Count of how many instances</returns>
      /// <history>
      /// [Curtis_Beard]      12/06/2005	Created
      /// [Curtis_Beard]      01/12/2007	FIX: check for correct position of IndexOf
      /// </history>
      private int RetrieveLineHitCount(string line, ISearchSpec searchSpec)
      {
         int _count = 0;
         string _end;
         int _pos = -1;

         string _tempLine = line;

         // attempt to locate the text in the line
         if (searchSpec.UseCaseSensitivity)
            _pos = _tempLine.IndexOf(searchSpec.SearchText);
         else
            _pos = _tempLine.ToLower().IndexOf(searchSpec.SearchText.ToLower());

         while (_pos > -1)
         {
            // retrieve parts of text
            string _begin = _tempLine.Substring(0, _pos);
            _end = _tempLine.Substring(_pos + searchSpec.SearchText.Length);

            // do a check to see if begin and end are valid for wholeword searches
            bool _highlight;
            if (searchSpec.UseWholeWordMatching)
               _highlight = WholeWordOnly(_begin, _end);
            else
               _highlight = true;

            // found a hit
            if (_highlight)
               _count += 1;

            // Check remaining string for other hits in same line
            if (searchSpec.UseCaseSensitivity)
               _pos = _end.IndexOf(searchSpec.SearchText);
            else
               _pos = _end.ToLower().IndexOf(searchSpec.SearchText.ToLower());

            // update the temp line with the next part to search (if any)
            _tempLine = _end;
         }

         return _count;
      }

      static readonly List<string> validTexts = new List<string> { " ", "<", "$", "+", "*", "[", "{", "(", ".", "?", "!", ",", ":", ";", "-", "\\", "/", "'", "\"", Environment.NewLine, "\r\n", "\r", "\n" };

      /// <summary>
      /// Validate a start text.
      /// </summary>
      /// <param name="text">text to validate</param>
      /// <param name="checkEndText">true if checking end, false if checking beginning</param>
      /// <returns>True - valid, False - otherwise</returns>
      /// <history>
      /// [Curtis_Beard]		12/06/2005	Created
      /// [Curtis_Beard]		02/09/2007	FIX: 1655533, update whole word matching
      /// [Curtis_Beard]		08/21/2007	ADD: '/' character and Environment.NewLine
      /// [Andrew_Radford]      09/08/2009  CHG: refactored to use list, combined begin and end text methods
      /// [Curtis_Beard]		02/17/2012	CHG: check end text as well
      /// </history>
      private bool IsValidText(string text, bool checkEndText)
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
      private bool WholeWordOnly(string beginText, string endText)
      {
         return (IsValidText(beginText, true) && IsValidText(endText, false));
      }
   }
}
