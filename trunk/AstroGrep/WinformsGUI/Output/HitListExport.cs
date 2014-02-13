using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using libAstroGrep;

namespace AstroGrep.Output
{
    /// <summary>
    /// Helper class to save hitlist
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
    /// [Andrew_Radford]    20/9/2009   Extracted from main form code-behind
    /// </history>
    public class HitListExport
    {
        /// <summary>
        /// Delegate for Export method.
        /// </summary>
        /// <param name="path">file path</param>
        /// <param name="grepper">Grep object containing settings</param>
        /// <param name="listview">ListView containing current hits</param>
        public delegate void ExportDelegate(string path, Grep grepper, ListView listview);

        /// <summary>
        /// Save results to a text file
        /// </summary>
        /// <param name="path">Fully qualified file path</param>
        /// <param name="grep">libAstroGrep object</param>
        /// <param name="lstFileNames">ListView containing hits</param>
        /// <history>
        /// [Curtis_Beard]		   09/06/2006	Created
        /// [Andrew_Radford]     20/09/2009	Extracted from main form code-behind
        /// [Curtis_Beard]		   01/31/2012	CHG: output some basic search information, make divider same length as filename
        /// [Curtis_Beard]		   02/12/2014	CHG: handle file search only better, add search options to output
        /// </history>
        public static void SaveResultsAsText(string path, Grep grep, ListView lstFileNames)
        {
            // Open the file
            using (var writer = new StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
                StringBuilder builder = new StringBuilder();
                int totalHits = 0;

                bool isFileSearch = string.IsNullOrEmpty(grep.SearchSpec.SearchText);

                // loop through File Names list
                for (int _index = 0; _index < lstFileNames.Items.Count; _index++)
                {
                    var hitNum = int.Parse(lstFileNames.Items[_index].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text);

                    var _hit = grep.RetrieveHitObject(hitNum);
                    totalHits += _hit.HitCount;

                    // write info to a file
                    if (isFileSearch || grep.SearchSpec.ReturnOnlyFileNames)
                    {
                       builder.AppendLine(_hit.FilePath);
                    }
                    else
                    {
                       builder.AppendLine(new string('-', _hit.FilePath.Length));
                       builder.AppendLine(_hit.FilePath);
                       builder.AppendLine(new string('-', _hit.FilePath.Length));
                       builder.AppendLine(_hit.Lines);
                       builder.AppendLine();
                    }
                }

                // output basic search information as a header
                writer.WriteLine("AstroGrep Results");
                writer.WriteLine("-------------------------------------------------------");
                if (!isFileSearch)
                {
                   writer.WriteLine(string.Format("{0} was found {1} time{2} in {3} file{4}",
                      grep.SearchSpec.SearchText,
                      totalHits,
                      totalHits > 1 ? "s" : "",
                      grep.Greps.Count,
                      grep.Greps.Count > 1 ? "s" : ""));

                   writer.WriteLine("");
                   
                }

                writer.WriteLine("");
               
                // output options
                writer.WriteLine("\tSearch Options");
                writer.WriteLine("\t---------------------------------------------------");
                //writer.WriteLine("searchPaths", string.Join(", ", grep.SearchSpec.StartDirectories));
                writer.WriteLine("\tFile Types: {0}", grep.FileFilterSpec.FileFilter);
                writer.WriteLine("\tRegular Expressions: {0}", grep.SearchSpec.UseRegularExpressions.ToString());
                writer.WriteLine("\tCase Sensitive: {0}", grep.SearchSpec.UseCaseSensitivity.ToString());
                writer.WriteLine("\tWhole Word: {0}", grep.SearchSpec.UseWholeWordMatching.ToString());
                writer.WriteLine("\tSubfolders: {0}", grep.SearchSpec.SearchInSubfolders.ToString());
                writer.WriteLine("\tShow File Names Only: {0}", grep.SearchSpec.ReturnOnlyFileNames.ToString());
                writer.WriteLine("\tNegation: {0}", grep.SearchSpec.UseNegation.ToString());
                writer.WriteLine("\tLine Numbers: {0}", grep.SearchSpec.IncludeLineNumbers.ToString());
                writer.WriteLine("\tContext Lines: {0}", grep.SearchSpec.ContextLines.ToString());
                writer.WriteLine("\tSkip Hidden Files/Directories: {0}", grep.FileFilterSpec.SkipHiddenFiles.ToString());
                writer.WriteLine("\tSkip System Files/Directories: {0}", grep.FileFilterSpec.SkipSystemFiles.ToString());
                if (grep.FileFilterSpec.DateModifiedStart != DateTimePicker.MinimumDateTime)
                {
                   writer.WriteLine("\tModified Date Start: {0}", grep.FileFilterSpec.DateModifiedStart.ToString());
                }
                if (grep.FileFilterSpec.DateModifiedEnd != DateTimePicker.MaximumDateTime)
                {
                   writer.WriteLine("\tModified Date End: {0}", grep.FileFilterSpec.DateModifiedEnd.ToString());
                }
                if (grep.FileFilterSpec.FileSizeMin != long.MinValue)
                {
                   writer.WriteLine("\tMin File Size: {0}", grep.FileFilterSpec.FileSizeMin.ToString());
                }
                if (grep.FileFilterSpec.FileSizeMax != long.MaxValue)
                {
                   writer.WriteLine("\tMax File Size: {0}", grep.FileFilterSpec.FileSizeMax.ToString());
                }
                if (grep.FileFilterSpec.FileHitCount > 0)
                {
                   writer.WriteLine("\tMinimum File Count: {0}", grep.FileFilterSpec.FileHitCount.ToString());
                }

                writer.WriteLine("");
                writer.WriteLine("");

                writer.WriteLine("Results");
                if (isFileSearch || grep.SearchSpec.ReturnOnlyFileNames)
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
        /// <param name="path">Fully qualified file path</param>
        /// <param name="grep">libAstroGrep object</param>
        /// <param name="lstFileNames">ListView containing hits</param>
        /// <history>
        /// [Curtis_Beard]		   09/06/2006	Created
        /// [Andrew_Radford]     20/09/2009	Extracted from main form code-behind
        /// [Curtis_Beard]		   01/31/2012	CHG: make divider same length as filename
        /// [Curtis_Beard]		   10/30/2012	CHG: add total in hits in file
        /// [Curtis_Beard]		   02/12/2014	CHG: handle file search only better
        /// </history>
        public static void SaveResultsAsHTML(string path, Grep grep, ListView lstFileNames)
        {
            using (var writer = new StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
                var allSections = new System.Text.StringBuilder();
                string repeater;
                StringBuilder lines;
                string template = HTMLHelper.GetContents("Output.html");
                string css = HTMLHelper.GetContents("Output.css");
                int totalHits = 0;
                bool isFileSearch = string.IsNullOrEmpty(grep.SearchSpec.SearchText);

                if (grep.SearchSpec.ReturnOnlyFileNames || isFileSearch)
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
                for (int _index = 0; _index < lstFileNames.Items.Count; _index++)
                {
                    var hitNum = int.Parse(lstFileNames.Items[_index].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text);
                    var hitObject = grep.RetrieveHitObject(hitNum);

                    lines = new StringBuilder();
                    repeater = repeatSection;
                    string fileLine = string.Format("{0} (Total: {1})", hitObject.FilePath, hitObject.HitCount);
                    repeater = repeater.Replace("%%file%%", fileLine);
                    repeater = repeater.Replace("%%filesep%%", new string('-', fileLine.Length));
                    totalHits += hitObject.HitCount;

                    for (int _jIndex = 0; _jIndex < hitObject.LineCount; _jIndex++)
                        lines.Append(HTMLHelper.GetHighlightLine(hitObject.RetrieveLine(_jIndex), grep));

                    repeater = repeater.Replace("%%lines%%", lines.ToString());

                    allSections.Append(repeater);
                }

                template = template.Replace(repeat, allSections.ToString());
                template = HTMLHelper.ReplaceSearchOptions(template, grep, totalHits);

                // write out template to the file
                writer.WriteLine(template);
            }
        }

        /// <summary>
        /// Save results to a xml file
        /// </summary>
        /// <param name="path">Fully qualified file path</param>
        /// <param name="grep">libAstroGrep object</param>
        /// <param name="lstFileNames">ListView containing hits</param>
        /// <history>
        /// [Curtis_Beard]		   09/06/2006	Created
        /// [Andrew_Radford]     20/09/2009	Extracted from main form code-behind
        /// [Curtis_Beard]		   01/31/2012	ADD: display for additional options (skip hidden/system options, search paths, modified dates, file sizes)
        /// [Curtis_Beard]		   10/30/2012	ADD: file hit count, CHG: recurse to subFolders
        /// [Curtis_Beard]		   02/12/2014	CHG: handle file search only better
        /// </history>
        public static void SaveResultsAsXML(string path, Grep grep, ListView lstFileNames)
        {
            using (var writer = new XmlTextWriter(path, Encoding.UTF8))
            {
               bool isFileSearch = string.IsNullOrEmpty(grep.SearchSpec.SearchText);

                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument(true);
                writer.WriteStartElement("astrogrep");
                writer.WriteAttributeString("version", "1.0");

                // write out search options
                writer.WriteStartElement("options");
                writer.WriteElementString("searchPaths", string.Join(", ", grep.SearchSpec.StartDirectories));
                writer.WriteElementString("fileTypes", grep.FileFilterSpec.FileFilter);
                writer.WriteElementString("searchText", grep.SearchSpec.SearchText);
                writer.WriteElementString("regularExpressions", grep.SearchSpec.UseRegularExpressions.ToString());
                writer.WriteElementString("caseSensitive", grep.SearchSpec.UseCaseSensitivity.ToString());
                writer.WriteElementString("wholeWord", grep.SearchSpec.UseWholeWordMatching.ToString());
                writer.WriteElementString("subFolders", grep.SearchSpec.SearchInSubfolders.ToString());
                writer.WriteElementString("showFileNamesOnly", grep.SearchSpec.ReturnOnlyFileNames.ToString());
                writer.WriteElementString("negation", grep.SearchSpec.UseNegation.ToString());
                writer.WriteElementString("lineNumbers", grep.SearchSpec.IncludeLineNumbers.ToString());
                writer.WriteElementString("contextLines", grep.SearchSpec.ContextLines.ToString());
                writer.WriteElementString("skipHidden", grep.FileFilterSpec.SkipHiddenFiles.ToString());
                writer.WriteElementString("skipSystem", grep.FileFilterSpec.SkipSystemFiles.ToString());
                if (grep.FileFilterSpec.DateModifiedStart != DateTimePicker.MinimumDateTime)
                {
                    writer.WriteElementString("dateModifiedStart", grep.FileFilterSpec.DateModifiedStart.ToString());
                }
                if (grep.FileFilterSpec.DateModifiedEnd != DateTimePicker.MaximumDateTime)
                {
                    writer.WriteElementString("dateModifiedEnd", grep.FileFilterSpec.DateModifiedEnd.ToString());
                }
                if (grep.FileFilterSpec.FileSizeMin != long.MinValue)
                {
                    writer.WriteElementString("fileSizeMin", grep.FileFilterSpec.FileSizeMin.ToString());
                }
                if (grep.FileFilterSpec.FileSizeMax != long.MaxValue)
                {
                    writer.WriteElementString("fileSizeMax", grep.FileFilterSpec.FileSizeMax.ToString());
                }
                if (grep.FileFilterSpec.FileHitCount > 0)
                {
                    writer.WriteElementString("minimumFileCount", grep.FileFilterSpec.FileHitCount.ToString());
                }
                writer.WriteEndElement();

                writer.WriteStartElement("search");
                writer.WriteAttributeString("totalfiles", grep.Greps.Count.ToString());

                // get total hits
                int totalHits = 0;
                for (int _index = 0; _index < lstFileNames.Items.Count; _index++)
                {
                    var hitNum = int.Parse(lstFileNames.Items[_index].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text);
                    var _hit = grep.RetrieveHitObject(hitNum);

                    // add to total
                    totalHits += _hit.HitCount;
                }
                writer.WriteAttributeString("totalfound", totalHits.ToString());

                for (int _index = 0; _index < lstFileNames.Items.Count; _index++)
                {
                    writer.WriteStartElement("item");
                    var hitNum = int.Parse(lstFileNames.Items[_index].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text);
                    var _hit = grep.RetrieveHitObject(hitNum);
                    writer.WriteAttributeString("file", _hit.FilePath);
                    writer.WriteAttributeString("total", _hit.HitCount.ToString());

                    // write out lines
                    if (!isFileSearch && !grep.SearchSpec.ReturnOnlyFileNames)
                    {
                       for (int _jIndex = 0; _jIndex < _hit.LineCount; _jIndex++)
                          writer.WriteElementString("line", _hit.RetrieveLine(_jIndex));
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
        /// <param name="path">Fully qualified file path</param>
        /// <param name="grep">libAstroGrep object</param>
        /// <param name="lstFileNames">ListView containing hits</param>
        /// <history>
        /// [Curtis_Beard]		   10/30/2012	Created
        /// [Curtis_Beard]		   02/12/2014	CHG: handle file search only better
        /// </history>
        public static void SaveResultsAsJSON(string path, Grep grep, ListView lstFileNames)
        {
            // Open the file
            using (var writer = new StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
               bool isFileSearch = string.IsNullOrEmpty(grep.SearchSpec.SearchText);
                writer.WriteLine("{");

                // write out search options
                writer.WriteLine("\t\"options\":");
                writer.WriteLine("\t{");
                writer.WriteLine(string.Format("\t\t\"searchPaths\":{0},", JSONHelper.ToJSONString(grep.SearchSpec.StartDirectories)));
                writer.WriteLine(string.Format("\t\t\"fileTypes\":{0},", JSONHelper.ToJSONString(grep.FileFilterSpec.FileFilter)));
                writer.WriteLine(string.Format("\t\t\"searchText\":{0},", JSONHelper.ToJSONString(grep.SearchSpec.SearchText)));
                writer.WriteLine(string.Format("\t\t\"regularExpressions\":{0},", JSONHelper.ToJSONString(grep.SearchSpec.UseRegularExpressions)));
                writer.WriteLine(string.Format("\t\t\"caseSensitive\":{0},", JSONHelper.ToJSONString(grep.SearchSpec.UseCaseSensitivity)));
                writer.WriteLine(string.Format("\t\t\"wholeWord\":{0},", JSONHelper.ToJSONString(grep.SearchSpec.UseWholeWordMatching)));
                writer.WriteLine(string.Format("\t\t\"subFolders\":{0},", JSONHelper.ToJSONString(grep.SearchSpec.SearchInSubfolders)));
                writer.WriteLine(string.Format("\t\t\"showFileNamesOnly\":{0},", JSONHelper.ToJSONString(grep.SearchSpec.ReturnOnlyFileNames)));
                writer.WriteLine(string.Format("\t\t\"negation\":{0},", JSONHelper.ToJSONString(grep.SearchSpec.UseNegation)));
                writer.WriteLine(string.Format("\t\t\"lineNumbers\":{0},", JSONHelper.ToJSONString(grep.SearchSpec.IncludeLineNumbers)));
                writer.WriteLine(string.Format("\t\t\"contextLines\":{0},", JSONHelper.ToJSONString(grep.SearchSpec.ContextLines)));
                writer.WriteLine(string.Format("\t\t\"skipHidden\":{0},", JSONHelper.ToJSONString(grep.FileFilterSpec.SkipHiddenFiles)));
                writer.WriteLine(string.Format("\t\t\"skipSystem\":{0},", JSONHelper.ToJSONString(grep.FileFilterSpec.SkipSystemFiles)));
                bool writeBefore = false;
                if (grep.FileFilterSpec.DateModifiedStart != DateTimePicker.MinimumDateTime)
                {
                   if (writeBefore)
                   {
                      writer.WriteLine(",");
                   }
                   writer.Write(string.Format("\t\t\"dateModifiedStart\":{0}", JSONHelper.ToJSONString(grep.FileFilterSpec.DateModifiedStart)));
                   writeBefore = true;
                }
                if (grep.FileFilterSpec.DateModifiedEnd != DateTimePicker.MaximumDateTime)
                {
                   if (writeBefore)
                   {
                      writer.WriteLine(",");
                   }
                   writer.Write(string.Format("\t\t\"dateModifiedEnd\":{0}", JSONHelper.ToJSONString(grep.FileFilterSpec.DateModifiedEnd)));
                   writeBefore = true;
                }
                if (grep.FileFilterSpec.FileSizeMin != long.MinValue)
                {
                   if (writeBefore)
                   {
                      writer.WriteLine(",");
                   }
                   writer.Write(string.Format("\t\t\"fileSizeMin\":{0}", JSONHelper.ToJSONString(grep.FileFilterSpec.FileSizeMin)));
                   writeBefore = true;
                }
                if (grep.FileFilterSpec.FileSizeMax != long.MaxValue)
                {
                   if (writeBefore)
                   {
                      writer.WriteLine(",");
                   }
                   writer.Write(string.Format("\t\t\"fileSizeMax\":{0}", JSONHelper.ToJSONString(grep.FileFilterSpec.FileSizeMax)));
                   writeBefore = true;
                }
                if (grep.FileFilterSpec.FileHitCount > 0)
                {
                   if (writeBefore)
                   {
                      writer.WriteLine(",");
                   }
                   writer.Write(string.Format("\t\t\"minimumFileCount\":{0}", JSONHelper.ToJSONString(grep.FileFilterSpec.FileHitCount)));
                   writeBefore = true;
                }
                writer.WriteLine();
                writer.WriteLine("\t},"); // end options
                writer.WriteLine();

                writer.WriteLine("\t\"search\":");
                writer.WriteLine("\t{");
                writer.WriteLine(string.Format("\t\t\"totalfiles\":{0},", JSONHelper.ToJSONString(grep.Greps.Count)));

                // get total hits
                int totalHits = 0;
                for (int _index = 0; _index < lstFileNames.Items.Count; _index++)
                {
                    var hitNum = int.Parse(lstFileNames.Items[_index].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text);
                    var _hit = grep.RetrieveHitObject(hitNum);

                    // add to total
                    totalHits += _hit.HitCount;
                }
                writer.WriteLine(string.Format("\t\t\"totalfound\":{0},", JSONHelper.ToJSONString(totalHits)));

                writer.WriteLine("\t\t\"items\":");
                writer.WriteLine("\t\t\t[");
                for (int _index = 0; _index < lstFileNames.Items.Count; _index++)
                {
                    var hitNum = int.Parse(lstFileNames.Items[_index].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text);
                    var _hit = grep.RetrieveHitObject(hitNum);
                    
                    if (_index == 0)
                       writer.WriteLine("\t\t\t\t{");
                    else
                       writer.WriteLine("\t\t\t\t{");

                    writer.WriteLine(string.Format("\t\t\t\t\t\"file\":{0},", JSONHelper.ToJSONString(_hit.FilePath)));
                    writer.WriteLine(string.Format("\t\t\t\t\t\"total\":{0},", JSONHelper.ToJSONString(_hit.HitCount)));
                    

                    // write out lines
                    if (!isFileSearch && !grep.SearchSpec.ReturnOnlyFileNames)
                    {
                       writer.Write("\t\t\t\t\t\"lines\":[");
                       for (int _jIndex = 0; _jIndex < _hit.LineCount; _jIndex++)
                       {
                          if (_jIndex > 0)
                             writer.Write(",");

                          writer.Write(JSONHelper.ToJSONString(_hit.RetrieveLine(_jIndex)));
                       }
                       writer.WriteLine("]");  // end lines
                    }

                    string itemEnd = _index + 1 < lstFileNames.Items.Count ? "\t\t\t\t}," : "\t\t\t\t}";
                    writer.WriteLine(itemEnd);  // end item
                }
                writer.WriteLine("\t\t\t]");  // end items

                writer.WriteLine("\t}");  // end search

                writer.Write("}"); // end all
            }
        }
    }
}