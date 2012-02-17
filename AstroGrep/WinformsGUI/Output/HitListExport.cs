using System.IO;
using System.Text;
using System.Windows.Forms;
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
    /// [Andrew_Radford]      20/9/09	Extracted from main form code-behind
    /// </history>
    public class HitListExport
    {
        public delegate void ExportDelegate(string path, Grep grepper, ListView listview);

        /// <summary>
        /// Save results to a text file
        /// </summary>
        /// <param name="path">Fully qualified file path</param>
        /// <param name="grep">libAstroGrep object</param>
        /// <param name="lstFileNames">ListView containing hits</param>
        /// <history>
        /// [Curtis_Beard]		  09/06/2006	Created
        /// [Andrew_Radford]    20/09/2009	Extracted from main form code-behind
        /// [Curtis_Beard]		  01/31/2012	CHG: output some basic search information, make divider same length as filename
        /// </history>
        public static void SaveResultsAsText(string path, Grep grep, ListView lstFileNames)
        {
            // Open the file
            using (var writer = new System.IO.StreamWriter(path, false, System.Text.Encoding.Default))
            {
                StringBuilder builder = new StringBuilder();
                int totalHits = 0;

                // loop through File Names list
                for (int _index = 0; _index < lstFileNames.Items.Count; _index++)
                {
                    var hitNum = int.Parse(lstFileNames.Items[_index].SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text);

                    var _hit = grep.RetrieveHitObject(hitNum);
                    totalHits += _hit.HitCount;


                    // write info to a file
                    builder.AppendLine(new string('-', _hit.FilePath.Length));
                    builder.AppendLine(_hit.FilePath);
                    builder.AppendLine(new string('-', _hit.FilePath.Length));
                    builder.AppendLine(_hit.Lines);
                    builder.AppendLine();
                }

                // output basic search information as a header
                writer.WriteLine("AstroGrep Results");
                writer.WriteLine("-----------------");
                writer.WriteLine(string.Format("{0} was found {1} time{2} in {3} file{4}",
                   grep.SearchSpec.SearchText, 
                   totalHits, 
                   totalHits > 1 ? "s" : "", 
                   grep.Greps.Count, 
                   grep.Greps.Count > 1 ? "s" : ""));
                writer.WriteLine("");
                writer.WriteLine("");

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
        /// [Curtis_Beard]		  09/06/2006	Created
        /// [Andrew_Radford]    20/09/2009	Extracted from main form code-behind
        /// [Curtis_Beard]		  01/31/2012	CHG: make divider same length as filename
        /// </history>
        public static void SaveResultsAsHTML(string path, Grep grep, ListView lstFileNames)
        {
            using (var writer = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                var allSections = new System.Text.StringBuilder();
                string repeater;
                StringBuilder lines;
                string template = HTMLHelper.GetContents("Output.html");
                string css = HTMLHelper.GetContents("Output.css");
                int totalHits = 0;

                if (grep.SearchSpec.ReturnOnlyFileNames)
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
                    repeater = repeater.Replace("%%file%%", hitObject.FilePath);
                    repeater = repeater.Replace("%%filesep%%", new string('-', hitObject.FilePath.Length));
                    totalHits += hitObject.HitCount;

                    for (int _jIndex = 0; _jIndex < hitObject.LineCount; _jIndex++)
                        lines.Append(HTMLHelper.GetHighlightLine(hitObject.RetrieveLine(_jIndex), grep));

                    repeater = repeater.Replace("%%lines%%", lines.ToString());

                    allSections.Append(repeater);
                }

                template = template.Replace(repeat, allSections.ToString());
                template = template.Replace("%%totalhits%%", totalHits.ToString());
                template = HTMLHelper.ReplaceSearchOptions(template, grep);

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
        /// [Curtis_Beard]		09/06/2006	Created
        /// [Andrew_Radford]  20/09/2009	Extracted from main form code-behind
        /// [Curtis_Beard]		01/31/2012	ADD: display for additional options (skip hidden/system options, search paths, modified dates, file sizes)
        /// </history>
        public static void SaveResultsAsXML(string path, Grep grep, ListView lstFileNames)
        {
            using (var writer = new System.Xml.XmlTextWriter(path, Encoding.UTF8))
            {
                writer.Formatting = System.Xml.Formatting.Indented;

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
                writer.WriteElementString("recurse", grep.SearchSpec.SearchInSubfolders.ToString());
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
                    for (int _jIndex = 0; _jIndex < _hit.LineCount; _jIndex++)
                        writer.WriteElementString("line", _hit.RetrieveLine(_jIndex));
                    writer.WriteEndElement();
                }

                writer.WriteEndElement(); //search
                writer.WriteEndElement(); //astrogrep
            }
        }
    }
}