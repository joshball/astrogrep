using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

using libAstroGrep;

namespace AstroGrep.Output
{
   /// <summary>
   /// Helper class to save MatchResults to a file.
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
   /// ted@astrocomma.com or curtismbeard@gmail.com or mandrolic@sourceforge.net
   /// </remarks>
   /// <history>
   /// [Andrew_Radford]    20/09/2009  Extracted from main form code-behind
   /// [Curtis_Beard]      04/08/2015	CHG: update export delegate with settings class, rename class
   /// [Curtis_Beard]      04/10/2015	CHG: move printing exporting here
   /// </history>
   public class MatchResultsExport
   {
      /// <summary>
      /// All export settings.
      /// </summary>
      public class ExportSettings
      {
         /// <summary>File path</summary>
         public string Path { get; set; }

         /// <summary>Grep object containing settings and all results</summary>
         public Grep Grep { get; set; }

         /// <summary>The indexes of the grep's MatchResults to export</summary>
         public List<int> GrepIndexes { get; set; }

         /// <summary>Determines whether to show line numbers</summary>
         public bool ShowLineNumbers { get; set; }

         /// <summary>Determines whether to trim leading white space</summary>
         public bool RemoveLeadingWhiteSpace { get; set; }
      }

      /// <summary>
      /// Delegate for saving to file method.
      /// </summary>
      /// <param name="settings">Export settings</param>
      public delegate void FileDelegate(ExportSettings settings);

      /// <summary>
      /// Delegate for printing method.
      /// </summary>
      /// <param name="settings">Export settings</param>
      /// <returns>the print document contents</returns>
      public delegate string PrintDelegate(ExportSettings settings);

      #region File Saving Methods

      /// <summary>
      /// Save results to a text file
      /// </summary>
      /// <param name="settings">Export settings</param>
      /// <history>
      /// [Curtis_Beard]		09/06/2006	Created
      /// [Andrew_Radford]    20/09/2009	Extracted from main form code-behind
      /// [Curtis_Beard]      01/31/2012	CHG: output some basic search information, make divider same length as filename
      /// [Curtis_Beard]		02/12/2014	CHG: handle file search only better, add search options to output
      /// [Curtis_Beard]      10/27/2014	CHG: 85, remove leading white space
      /// [Curtis_Beard]      11/11/2014	ADD: export all filteritems
      /// [Curtis_Beard]      12/03/2014	CHG: use grepIndexes instead of ListView
      /// [Curtis_Beard]      04/08/2015	CHG: update export delegate with settings class
      /// </history>
      public static void SaveResultsAsText(ExportSettings settings)
      {
         // Open the file
         using (var writer = new StreamWriter(settings.Path, false, System.Text.Encoding.UTF8))
         {
            StringBuilder builder = new StringBuilder();
            int totalHits = 0;

            bool isFileSearch = string.IsNullOrEmpty(settings.Grep.SearchSpec.SearchText);

            // loop through File Names list
            for (int i = 0; i < settings.GrepIndexes.Count; i++)
            {
               var _hit = settings.Grep.RetrieveMatchResult(settings.GrepIndexes[i]);
               totalHits += _hit.HitCount;

               // write info to a file
               if (isFileSearch || settings.Grep.SearchSpec.ReturnOnlyFileNames)
               {
                  builder.AppendLine(_hit.File.FullName);
               }
               else
               {
                  builder.AppendLine(new string('-', _hit.File.FullName.Length));
                  builder.AppendLine(_hit.File.FullName);
                  builder.AppendLine(new string('-', _hit.File.FullName.Length));
                  for (int j = 0; j < _hit.Matches.Count; j++)
                  {
                     string line = _hit.Matches[j].Line;
                     if (settings.RemoveLeadingWhiteSpace)
                     {
                        line = line.TrimStart();
                     }

                     if (settings.ShowLineNumbers && _hit.Matches[j].LineNumber > -1)
                     {
                        line = string.Format("{0}: {1}", _hit.Matches[j].LineNumber, line);
                     }

                     builder.AppendLine(line);
                  }
                  builder.AppendLine();
               }
            }

            // output basic search information as a header
            writer.WriteLine("AstroGrep Results");
            writer.WriteLine("-------------------------------------------------------");
            if (!isFileSearch)
            {
               writer.WriteLine(string.Format("{0} was found {1} time{2} in {3} file{4}",
                  settings.Grep.SearchSpec.SearchText,
                  totalHits,
                  totalHits > 1 ? "s" : "",
                  settings.Grep.MatchResults.Count,
                  settings.Grep.MatchResults.Count > 1 ? "s" : ""));

               writer.WriteLine("");

            }

            writer.WriteLine("");

            // output options
            writer.WriteLine("Search Options");
            writer.WriteLine("-------------------------------------------------------");
            writer.WriteLine("Search Paths: {0}", string.Join(", ", settings.Grep.SearchSpec.StartDirectories));
            writer.WriteLine("File Types: {0}", settings.Grep.FileFilterSpec.FileFilter);
            writer.WriteLine("Regular Expressions: {0}", settings.Grep.SearchSpec.UseRegularExpressions.ToString());
            writer.WriteLine("Case Sensitive: {0}", settings.Grep.SearchSpec.UseCaseSensitivity.ToString());
            writer.WriteLine("Whole Word: {0}", settings.Grep.SearchSpec.UseWholeWordMatching.ToString());
            writer.WriteLine("Subfolders: {0}", settings.Grep.SearchSpec.SearchInSubfolders.ToString());
            writer.WriteLine("Show File Names Only: {0}", settings.Grep.SearchSpec.ReturnOnlyFileNames.ToString());
            writer.WriteLine("Negation: {0}", settings.Grep.SearchSpec.UseNegation.ToString());
            writer.WriteLine("Line Numbers: {0}", settings.ShowLineNumbers.ToString());
            writer.WriteLine("Remove Leading White Space: {0}", settings.RemoveLeadingWhiteSpace.ToString());
            writer.WriteLine("Context Lines: {0}", settings.Grep.SearchSpec.ContextLines.ToString());

            // filter items
            if (settings.Grep.FileFilterSpec.FilterItems != null)
            {
               writer.WriteLine("Exclusions:");
               foreach (FilterItem item in settings.Grep.FileFilterSpec.FilterItems)
               {
                  string option = item.ValueOption.ToString();
                  if (item.ValueOption == FilterType.ValueOptions.None)
                  {
                     option = string.Empty;
                  }
                  writer.WriteLine("\t{0} -> {1}: {2} {3}", item.FilterType.Category, item.FilterType.SubCategory, item.Value, option);
               }
            }

            writer.WriteLine("");
            writer.WriteLine("");

            writer.WriteLine("Results");
            if (isFileSearch || settings.Grep.SearchSpec.ReturnOnlyFileNames)
            {
               writer.WriteLine("-------------------------------------------------------");
            }

            // output actual results
            writer.Write(builder.ToString());
         }
      }

      /// <summary>
      /// Save results to a html file
      /// </summary>
      /// <param name="settings">Export settings</param>
      /// <history>
      /// [Curtis_Beard]		09/06/2006	Created
      /// [Andrew_Radford]    20/09/2009	Extracted from main form code-behind
      /// [Curtis_Beard]		01/31/2012	CHG: make divider same length as filename
      /// [Curtis_Beard]		10/30/2012	CHG: add total in hits in file
      /// [Curtis_Beard]		02/12/2014	CHG: handle file search only better
      /// [Curtis_Beard]      10/27/2014	CHG: 85, remove leading white space
      /// [Curtis_Beard]      12/03/2014	CHG: use grepIndexes instead of ListView
      /// [Curtis_Beard]      04/08/2015	CHG: update export delegate with settings class
      /// </history>
      public static void SaveResultsAsHTML(ExportSettings settings)
      {
         using (var writer = new StreamWriter(settings.Path, false, System.Text.Encoding.UTF8))
         {
            var allSections = new System.Text.StringBuilder();
            string repeater;
            StringBuilder lines;
            string template = HTMLHelper.GetContents("Output.html");
            string css = HTMLHelper.GetContents("Output.css");
            int totalHits = 0;
            bool isFileSearch = string.IsNullOrEmpty(settings.Grep.SearchSpec.SearchText);

            if (settings.Grep.SearchSpec.ReturnOnlyFileNames || isFileSearch)
               template = HTMLHelper.GetContents("Output-fileNameOnly.html");

            css = HTMLHelper.ReplaceCssHolders(css);
            template = template.Replace("%%style%%", css);
            template = template.Replace("%%title%%", "AstroGrep Results");

            int rStart = template.IndexOf("[repeat]");
            int rStop = template.IndexOf("[/repeat]") + "[/repeat]".Length;
            string repeat = template.Substring(rStart, rStop - rStart);

            string repeatSection = repeat;
            repeatSection = repeatSection.Replace("[repeat]", string.Empty);
            repeatSection = repeatSection.Replace("[/repeat]", string.Empty);

            // loop through File Names list
            for (int i = 0; i < settings.GrepIndexes.Count; i++)
            {
               var hitObject = settings.Grep.RetrieveMatchResult(settings.GrepIndexes[i]);

               lines = new StringBuilder();
               repeater = repeatSection;
               string fileLine = string.Format("{0} (Total: {1})", hitObject.File.FullName, hitObject.HitCount);
               repeater = repeater.Replace("%%file%%", fileLine);
               repeater = repeater.Replace("%%filesep%%", new string('-', fileLine.Length));
               totalHits += hitObject.HitCount;

               for (int j = 0; j < hitObject.Matches.Count; j++)
               {
                  string line = hitObject.Matches[j].Line;
                  if (settings.RemoveLeadingWhiteSpace)
                  {
                     line = line.TrimStart();
                  }

                  if (settings.ShowLineNumbers && hitObject.Matches[j].LineNumber > -1)
                  {
                     line = string.Format("{0}: {1}", hitObject.Matches[j].LineNumber, line);
                  }

                  lines.Append(HTMLHelper.GetHighlightLine(line, settings.Grep));
               }
               
               repeater = repeater.Replace("%%lines%%", lines.ToString());

               allSections.Append(repeater);
            }

            template = template.Replace(repeat, allSections.ToString());
            template = HTMLHelper.ReplaceSearchOptions(template, settings.Grep, totalHits, settings.ShowLineNumbers, settings.RemoveLeadingWhiteSpace);

            // write out template to the file
            writer.WriteLine(template);
         }
      }

      /// <summary>
      /// Save results to a xml file
      /// </summary>
      /// <param name="settings">Export settings</param>
      /// <history>
      /// [Curtis_Beard]		09/06/2006	Created
      /// [Andrew_Radford]    20/09/2009	Extracted from main form code-behind
      /// [Curtis_Beard]		01/31/2012	ADD: display for additional options (skip hidden/system options, search paths, modified dates, file sizes)
      /// [Curtis_Beard]		10/30/2012	ADD: file hit count, CHG: recurse to subFolders
      /// [Curtis_Beard]		02/12/2014	CHG: handle file search only better
      /// [Curtis_Beard]      10/27/2014	CHG: 85, remove leading white space
      /// [Curtis_Beard]      11/11/2014	ADD: export all filteritems
      /// [Curtis_Beard]      12/03/2014	CHG: use grepIndexes instead of ListView
      /// [Curtis_Beard]      04/08/2015	CHG: update export delegate with settings class
      /// </history>
      public static void SaveResultsAsXML(ExportSettings settings)
      {
         using (var writer = new XmlTextWriter(settings.Path, Encoding.UTF8))
         {
            bool isFileSearch = string.IsNullOrEmpty(settings.Grep.SearchSpec.SearchText);

            writer.Formatting = Formatting.Indented;

            writer.WriteStartDocument(true);
            writer.WriteStartElement("astrogrep");
            writer.WriteAttributeString("version", "1.0");

            // write out search options
            writer.WriteStartElement("options");
            writer.WriteElementString("searchPaths", string.Join(", ", settings.Grep.SearchSpec.StartDirectories));
            writer.WriteElementString("fileTypes", settings.Grep.FileFilterSpec.FileFilter);
            writer.WriteElementString("searchText", settings.Grep.SearchSpec.SearchText);
            writer.WriteElementString("regularExpressions", settings.Grep.SearchSpec.UseRegularExpressions.ToString());
            writer.WriteElementString("caseSensitive", settings.Grep.SearchSpec.UseCaseSensitivity.ToString());
            writer.WriteElementString("wholeWord", settings.Grep.SearchSpec.UseWholeWordMatching.ToString());
            writer.WriteElementString("subFolders", settings.Grep.SearchSpec.SearchInSubfolders.ToString());
            writer.WriteElementString("showFileNamesOnly", settings.Grep.SearchSpec.ReturnOnlyFileNames.ToString());
            writer.WriteElementString("negation", settings.Grep.SearchSpec.UseNegation.ToString());
            writer.WriteElementString("lineNumbers", settings.ShowLineNumbers.ToString());
            writer.WriteElementString("removeLeadingWhiteSpace", settings.RemoveLeadingWhiteSpace.ToString());
            writer.WriteElementString("contextLines", settings.Grep.SearchSpec.ContextLines.ToString());
            // filter items
            if (settings.Grep.FileFilterSpec.FilterItems != null)
            {
               writer.WriteStartElement("exclusions");
               foreach (FilterItem item in settings.Grep.FileFilterSpec.FilterItems)
               {
                  string option = item.ValueOption.ToString();
                  if (item.ValueOption == FilterType.ValueOptions.None)
                  {
                     option = string.Empty;
                  }
                  writer.WriteStartElement("exclusion");
                  writer.WriteAttributeString("category", item.FilterType.Category.ToString());
                  writer.WriteAttributeString("type", item.FilterType.SubCategory.ToString());
                  writer.WriteAttributeString("value", item.Value);
                  writer.WriteAttributeString("options", option);
                  writer.WriteAttributeString("ignoreCase", item.ValueIgnoreCase.ToString());
                  writer.WriteAttributeString("sizeType", item.ValueSizeOption);
                  writer.WriteEndElement();
               }
               writer.WriteEndElement();
            }
            writer.WriteEndElement(); // end options

            writer.WriteStartElement("search");
            writer.WriteAttributeString("totalfiles", settings.Grep.MatchResults.Count.ToString());

            // get total hits
            int totalHits = 0;
            for (int i = 0; i < settings.GrepIndexes.Count; i++)
            {
               var _hit = settings.Grep.RetrieveMatchResult(settings.GrepIndexes[i]);

               // add to total
               totalHits += _hit.HitCount;
            }
            writer.WriteAttributeString("totalfound", totalHits.ToString());

            for (int i = 0; i < settings.GrepIndexes.Count; i++)
            {
               writer.WriteStartElement("item");
               var _hit = settings.Grep.RetrieveMatchResult(settings.GrepIndexes[i]);
               writer.WriteAttributeString("file", _hit.File.FullName);
               writer.WriteAttributeString("total", _hit.HitCount.ToString());

               // write out lines
               if (!isFileSearch && !settings.Grep.SearchSpec.ReturnOnlyFileNames)
               {
                  for (int j = 0; j < _hit.Matches.Count; j++)
                  {
                     string line = _hit.Matches[j].Line;
                     if (settings.RemoveLeadingWhiteSpace)
                     {
                        line = line.TrimStart();
                     }

                     if (settings.ShowLineNumbers && _hit.Matches[j].LineNumber > -1)
                     {
                        line = string.Format("{0}: {1}", _hit.Matches[j].LineNumber, line);
                     }

                     writer.WriteElementString("line", line);
                  }
               }
               writer.WriteEndElement();
            }

            writer.WriteEndElement(); //search
            writer.WriteEndElement(); //astrogrep
         }
      }

      /// <summary>
      /// Save results to a file with json formatting.
      /// </summary>
      /// <param name="settings">Export settings</param>
      /// <history>
      /// [Curtis_Beard]		10/30/2012	Created
      /// [Curtis_Beard]      02/12/2014	CHG: handle file search only better
      /// [Curtis_Beard]      10/27/2014	CHG: 85, remove leading white space
      /// [Curtis_Beard]      11/11/2014	ADD: export all filteritems
      /// [Curtis_Beard]      12/03/2014	CHG: use grepIndexes instead of ListView
      /// [Curtis_Beard]      04/08/2015	CHG: update export delegate with settings class
      /// </history>
      public static void SaveResultsAsJSON(ExportSettings settings)
      {
         // Open the file
         using (var writer = new StreamWriter(settings.Path, false, System.Text.Encoding.UTF8))
         {
            bool isFileSearch = string.IsNullOrEmpty(settings.Grep.SearchSpec.SearchText);
            writer.WriteLine("{");

            // write out search options
            writer.WriteLine("\t\"options\":");
            writer.WriteLine("\t{");
            writer.WriteLine(string.Format("\t\t\"searchPaths\":{0},", JSONHelper.ToJSONString(settings.Grep.SearchSpec.StartDirectories)));
            writer.WriteLine(string.Format("\t\t\"fileTypes\":{0},", JSONHelper.ToJSONString(settings.Grep.FileFilterSpec.FileFilter)));
            writer.WriteLine(string.Format("\t\t\"searchText\":{0},", JSONHelper.ToJSONString(settings.Grep.SearchSpec.SearchText)));
            writer.WriteLine(string.Format("\t\t\"regularExpressions\":{0},", JSONHelper.ToJSONString(settings.Grep.SearchSpec.UseRegularExpressions)));
            writer.WriteLine(string.Format("\t\t\"caseSensitive\":{0},", JSONHelper.ToJSONString(settings.Grep.SearchSpec.UseCaseSensitivity)));
            writer.WriteLine(string.Format("\t\t\"wholeWord\":{0},", JSONHelper.ToJSONString(settings.Grep.SearchSpec.UseWholeWordMatching)));
            writer.WriteLine(string.Format("\t\t\"subFolders\":{0},", JSONHelper.ToJSONString(settings.Grep.SearchSpec.SearchInSubfolders)));
            writer.WriteLine(string.Format("\t\t\"showFileNamesOnly\":{0},", JSONHelper.ToJSONString(settings.Grep.SearchSpec.ReturnOnlyFileNames)));
            writer.WriteLine(string.Format("\t\t\"negation\":{0},", JSONHelper.ToJSONString(settings.Grep.SearchSpec.UseNegation)));
            writer.WriteLine(string.Format("\t\t\"lineNumbers\":{0},", JSONHelper.ToJSONString(settings.ShowLineNumbers)));
            writer.WriteLine(string.Format("\t\t\"removeLeadingWhiteSpace\":{0},", JSONHelper.ToJSONString(settings.RemoveLeadingWhiteSpace)));
            writer.WriteLine(string.Format("\t\t\"contextLines\":{0},", JSONHelper.ToJSONString(settings.Grep.SearchSpec.ContextLines)));
            // filter items
            if (settings.Grep.FileFilterSpec.FilterItems != null)
            {
               writer.WriteLine("\t\t\"exclusions\":");
               writer.WriteLine("\t\t\t[");
               foreach (FilterItem item in settings.Grep.FileFilterSpec.FilterItems)
               {
                  writer.Write("\t\t\t\t{");
                  string option = item.ValueOption.ToString();
                  if (item.ValueOption == FilterType.ValueOptions.None)
                  {
                     option = string.Empty;
                  }
                  writer.Write(string.Format("\"category\":{0}, ", JSONHelper.ToJSONString(item.FilterType.Category.ToString())));
                  writer.Write(string.Format("\"type\":{0}, ", JSONHelper.ToJSONString(item.FilterType.SubCategory.ToString())));
                  writer.Write(string.Format("\"value\":{0}, ", JSONHelper.ToJSONString(item.Value)));
                  writer.Write(string.Format("\"options\":{0}, ", JSONHelper.ToJSONString(option)));
                  writer.Write(string.Format("\"ignoreCase\":{0}, ", JSONHelper.ToJSONString(item.ValueIgnoreCase.ToString())));
                  writer.Write(string.Format("\"sizeType\":{0}", JSONHelper.ToJSONString(item.ValueSizeOption.ToString())));
                  writer.WriteLine("}");
               }
               writer.WriteLine("\t\t\t]");
            }
            writer.WriteLine("\t},"); // end options
            writer.WriteLine();

            writer.WriteLine("\t\"search\":");
            writer.WriteLine("\t{");
            writer.WriteLine(string.Format("\t\t\"totalfiles\":{0},", JSONHelper.ToJSONString(settings.Grep.MatchResults.Count)));

            // get total hits
            int totalHits = 0;
            for (int i = 0; i < settings.GrepIndexes.Count; i++)
            {
               var _hit = settings.Grep.RetrieveMatchResult(settings.GrepIndexes[i]);

               // add to total
               totalHits += _hit.HitCount;
            }
            writer.WriteLine(string.Format("\t\t\"totalfound\":{0},", JSONHelper.ToJSONString(totalHits)));

            writer.WriteLine("\t\t\"items\":");
            writer.WriteLine("\t\t\t[");

            for (int i = 0; i < settings.GrepIndexes.Count; i++)
            {
               var _hit = settings.Grep.RetrieveMatchResult(settings.GrepIndexes[i]);
               
               writer.WriteLine("\t\t\t\t{");

               writer.WriteLine(string.Format("\t\t\t\t\t\"file\":{0},", JSONHelper.ToJSONString(_hit.File.FullName)));
               writer.WriteLine(string.Format("\t\t\t\t\t\"total\":{0},", JSONHelper.ToJSONString(_hit.HitCount)));


               // write out lines
               if (!isFileSearch && !settings.Grep.SearchSpec.ReturnOnlyFileNames)
               {
                  writer.Write("\t\t\t\t\t\"lines\":[");
                  for (int j = 0; j < _hit.Matches.Count; j++)
                  {
                     if (j > 0)
                     {
                        writer.Write(",");
                     }

                     string line = _hit.Matches[j].Line;
                     if (settings.RemoveLeadingWhiteSpace)
                     {
                        line = line.TrimStart();
                     }

                     if (settings.ShowLineNumbers && _hit.Matches[j].LineNumber > -1)
                     {
                        line = string.Format("{0}: {1}", _hit.Matches[j].LineNumber, line);
                     }

                     writer.Write(JSONHelper.ToJSONString(line));
                  }
                  writer.WriteLine("]");  // end lines
               }

               string itemEnd = i + 1 < settings.GrepIndexes.Count ? "\t\t\t\t}," : "\t\t\t\t}";
               writer.WriteLine(itemEnd);  // end item
            }
            writer.WriteLine("\t\t\t]");  // end items

            writer.WriteLine("\t}");  // end search

            writer.Write("}"); // end all
         }
      }

      #endregion

      #region Printing Methods

      /// <summary>
      /// Print selected match results.
      /// </summary>
      /// <param name="settings">Export settings</param>
      /// <returns>print document as a string</returns>
      /// <history>
      /// [Curtis_Beard]      04/10/2015	CHG: move printing exporting here
      /// </history>
      public static string PrintSelected(ExportSettings settings)
      {
         StringBuilder document = new StringBuilder();

         MatchResult match = null;

         for (int i = 0; i < settings.GrepIndexes.Count; i++)
         {
            match = settings.Grep.MatchResults[settings.GrepIndexes[i]];

            document.AppendLine("----------------------------------------------------------------------");
            document.AppendLine(match.File.FullName);
            document.AppendLine("----------------------------------------------------------------------");

            for (int j = 0; j < match.Matches.Count; j++)
            {
               var matchLine = match.Matches[j];
               if (matchLine.LineNumber > -1)
               {
                  document.AppendLine(string.Format("{0}: {1}", matchLine.LineNumber, matchLine.Line));
               }
               else
               {
                  document.AppendLine(string.Empty);
               }
            }

            document.AppendLine(string.Empty);
         }

         return document.ToString();
      }

      /// <summary>
      /// Print all match results.
      /// </summary>
      /// <param name="settings">Export settings</param>
      /// <returns>print document as a string</returns>
      /// <history>
      /// [Curtis_Beard]      04/10/2015	CHG: move printing exporting here
      /// </history>
      public static string PrintAll(ExportSettings settings)
      {
         StringBuilder document = new StringBuilder();

         foreach (var match in settings.Grep.MatchResults)
         {
            document.AppendLine("----------------------------------------------------------------------");
            document.AppendLine(match.File.FullName);
            document.AppendLine("----------------------------------------------------------------------");

            for (int i = 0; i < match.Matches.Count; i++)
            {
               var matchLine = match.Matches[i];
               if (matchLine.LineNumber > -1)
               {
                  document.AppendLine(string.Format("{0}: {1}", matchLine.LineNumber, matchLine.Line));
               }
               else
               {
                  document.AppendLine(string.Empty);
               }
            }

            document.AppendLine(string.Empty);
         }

         return document.ToString();
      }

      /// <summary>
      /// Print file paths only.
      /// </summary>
      /// <param name="settings">Export settings</param>
      /// <returns>print document as a string</returns>
      /// <history>
      /// [Curtis_Beard]      04/10/2015	CHG: move printing exporting here
      /// </history>
      public static string PrintFileList(ExportSettings settings)
      {
         StringBuilder document = new StringBuilder();
         document.AppendLine("----------------------------------------------------------------------");

         foreach (var match in settings.Grep.MatchResults)
         {
            document.AppendLine(match.File.FullName);
            document.AppendLine("----------------------------------------------------------------------");
         }

         return document.ToString();
      }

      #endregion
   }
}