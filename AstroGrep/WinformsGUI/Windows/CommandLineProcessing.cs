using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

using AstroGrep.Common.Logging;

namespace AstroGrep.Windows
{
   /// <summary>
   /// Parses and allocates command line arguments in a predefined way.
   /// 
   /// The following command line arguments are valid:
   /// [/spath="value"]                - Start Path
   /// [/stypes="value"]               - File Types
   /// [/stext="value"]                - Search Text
   /// [/e]                            - Use regular expressions
   /// [/c]                            - Case sensitive
   /// [/w]                            - Whole Word
   /// [/r]                            - Recursive search (search subfolders)
   /// [/n]                            - Negation
   /// [/l]                            - Line numbers
   /// [/f]                            - File names only
   /// [/cl="value"]                   - Number of context lines
   /// [/sh]                           - Skip hidden files and folders
   /// [/ss]                           - Skip system files and folders
   /// [/shf]                          - Skip hidden files
   /// [/shd]                          - Skip hidden folders
   /// [/ssf]                          - Skip system files
   /// [/ssd]                          - Skip system folders
   /// [/srf]                          - Skip ReadOny files
   /// [/s]                            - Start searching immediately
   /// [/opath="value"]                - Save results to path (/s implied)
   /// [/otype="value"]                - Save results type (json,html,xml,txt[default])
   /// [/exit]                         - Exit application after search
   /// [/dmf="operator|value"]         - File Date Modified (=,!=,&gt;,&lt;,&gt;=,&lt;=|MM/DD/YYYY)
   /// [/dmd="operator|value"]         - Directory Date Modified (=,!=,&gt;,&lt;,&gt;=,&lt;=|MM/DD/YYYY)
   /// [/dcf="operator|value"]         - File Date Created (=,!=,&gt;,&lt;,&gt;=,&lt;=|MM/DD/YYYY)
   /// [/dcd="operator|value"]         - Directory Date Created (=,!=,&gt;,&lt;,&gt;=,&lt;=|MM/DD/YYYY)
   /// [/minfsize="operator|value"]    - Minimum file size (=,!=,&gt;,&lt;,&gt;=,&lt;=|bytes)
   /// [/maxfsize="operator|value"]    - Maximum file size (=,!=,&gt;,&lt;,&gt;=,&lt;=|bytes)
   /// [/minfc="value"]                - Minimum file count   
   /// [/?, /h, /help]                 - Display command line arguments
   /// </summary>
   /// <remarks>
   ///   AstroGrep File Searching Utility. Written by Theodore L. Ward
   ///   Copyright (C) 2002 AstroComma Incorporated.
   ///   
   ///   This program is free software; you can redistribute it and/or
   ///   modify it under the terms of the GNU General Public License
   ///   as published by the Free Software Foundation; either version 2
   ///   of the License, or (at your option) any later version.
   ///   
   ///   This program is distributed in the hope that it will be useful,
   ///   but WITHOUT ANY WARRANTY; without even the implied warranty of
   ///   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   ///   GNU General Public License for more details.
   ///   
   ///   You should have received a copy of the GNU General Public License
   ///   along with this program; if not, write to the Free Software
   ///   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
   /// 
   ///   The author may be contacted at:
   ///   ted@astrocomma.com or curtismbeard@gmail.com
   /// </remarks>
   /// <history>
   /// 	[Curtis_Beard]		07/25/2006	ADD: 1492221, command line parameters
   /// 	[Curtis_Beard]		04/08/2014	CHG: 74, add missing search options, exit, save
   /// 	[Curtis_Beard]		06/02/2015	CHG: 97, remove /local since portable version created
   /// </history>
   public class CommandLineProcessing
   {
      /// <summary>
      /// Holds the parsed command line options.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/26/2006	Created
      /// [Curtis_Beard]		09/26/2012	ADD: display help option
      /// [Curtis_Beard]		04/08/2014	CHG: 74, add missing search options, exit, save
      /// [Curtis_Beard]		06/02/2015	CHG: 97, remove /local since portable version created
      /// </history>
      public struct CommandLineArguments
      {
         /// <summary></summary>
         public bool AnyArguments;
         /// <summary></summary>
         public string StartPath;
         /// <summary></summary>
         public bool IsValidStartPath;

         /// <summary></summary>
         public string ProjectFile;
         /// <summary></summary>
         public bool IsProjectFile;

         /// <summary></summary>
         public string FileTypes;
         /// <summary></summary>
         public bool IsValidFileTypes;

         /// <summary></summary>
         public string SearchText;
         /// <summary></summary>
         public bool IsValidSearchText;

         /// <summary></summary>
         public bool StartSearch;
         /// <summary></summary>
         public bool UseRegularExpressions;
         /// <summary></summary>
         public bool IsCaseSensitive;
         /// <summary></summary>
         public bool IsWholeWord;
         /// <summary></summary>
         public bool UseRecursion;
         /// <summary></summary>
         public bool IsNegation;
         /// <summary></summary>
         public bool UseLineNumbers;
         /// <summary></summary>
         public bool IsFileNamesOnly;
         /// <summary></summary>
         public int ContextLines;
         /// <summary></summary>
         public bool SkipHiddenFile;
         /// <summary></summary>
         public bool SkipHiddenDirectory;
         /// <summary></summary>
         public bool SkipSystemFile;
         /// <summary></summary>
         public bool SkipSystemDirectory;

         /// <summary></summary>
         public bool DisplayHelp;

         /// <summary></summary>
         public string OutputPath;
         /// <summary></summary>
         public string OutputType;
         /// <summary></summary>
         public bool ExitAfterSearch;
         /// <summary></summary>
         public ValueOptionPair DateModifiedFile;
         /// <summary></summary>
         public ValueOptionPair DateModifiedDirectory;
         /// <summary></summary>
         public ValueOptionPair DateCreatedFile;
         /// <summary></summary>
         public ValueOptionPair DateCreatedDirectory;
         /// <summary></summary>
         public ValueOptionPair MinFileSize;
         /// <summary></summary>
         public ValueOptionPair MaxFileSize;
         /// <summary></summary>
         public int MinFileCount;
         /// <summary></summary>
         public bool ReadOnlyFile;
      }

      /// <summary>
      /// 
      /// </summary>
      public class ValueOptionPair
      {
         /// <summary></summary>
         public libAstroGrep.FilterType.ValueOptions ValueOption { get; set; }

         /// <summary></summary>
         public object Value { get; set; }
      }

      /// <summary>
      /// Parses the command line arguments for valid options and returns them.
      /// </summary>
      /// <param name="CommandLineArgs">Array of strings containing the command line arguments</param>
      /// <returns>CommandLineArguments structure holding the options from the command line</returns>
      /// <history>
      /// [Curtis_Beard]		07/26/2006	Created
      /// [Curtis_Beard]		05/08/2007	ADD: 1590157, support project file
      /// [Curtis_Beard]	  04/08/2015	CHG: add logging
      /// </history>
      public static CommandLineArguments Process(string[] CommandLineArgs)
      {
         // create the args structure and initialize it
         CommandLineArguments args = new CommandLineArguments();
         InitializeArgs(ref args);

         // process the command line
         Arguments myArgs = new Arguments(CommandLineArgs);

         // check for just a single directory / project file
         if (CommandLineArgs.Length == 2)
         {
            args.AnyArguments = true;

            // Check command line for a path to start at
            string arg1 = CommandLineArgs[1];

            // remove an extra quote if (a drive letter
            if (arg1.EndsWith("\""))
               arg1 = arg1.Substring(0, arg1.Length - 1);

            //// check for a project file
            //if (arg1.EndsWith(".agproj"))
            //{
            //   args.ProjectFile = arg1;
            //   args.IsProjectFile = true;
            //}

            // check for a directory
            if (!args.IsProjectFile && System.IO.Directory.Exists(arg1))
            {
               args.StartPath = arg1;
               args.IsValidStartPath = true;
            }
            
            // do this before setting defaults to prevent loading wrong config file.
            if (!args.IsValidStartPath && !args.IsProjectFile)
            {
               // check for some other single setting, such as /local
               ProcessFlags(myArgs, ref args);               
            }
         }
         else if (CommandLineArgs.Length > 1)
         {
            args.AnyArguments = true;

            ProcessFlags(myArgs, ref args);
         }

         if (CommandLineArgs.Length > 1)
         {
            LogClient.Instance.Logger.Info("Processed command line arguments: {0}", string.Join(", ", CommandLineArgs));
         }

         return args;
      }

      /// <summary>
      /// Initializes values of the given CommandLineArguments structure.
      /// </summary>
      /// <param name="args">CommandLineArguments to initialize</param>
      /// <history>
      /// [Curtis_Beard]		07/26/2006	Created
      /// [Curtis_Beard]		09/26/2012	ADD: display help option
      /// [Curtis_Beard]		04/08/2014	CHG: 74, add missing search options, exit, save
      /// [Curtis_Beard]		06/02/2015	CHG: 97, remove /local since portable version created
      /// </history>
      private static void InitializeArgs(ref CommandLineArguments args)
      {
         args.AnyArguments = false;

         args.StartPath = string.Empty;
         args.IsValidStartPath = false;

         args.ProjectFile = string.Empty;
         args.IsProjectFile = false;

         args.FileTypes = string.Empty;
         args.IsValidFileTypes = false;

         args.SearchText = string.Empty;
         args.IsValidSearchText = false;

         args.StartSearch = false;

         // default to all turned off, user must specify each one, or use /d for defaults
         args.UseRegularExpressions = false;
         args.IsCaseSensitive = false;
         args.IsWholeWord = false;
         args.UseRecursion = false;
         args.IsNegation = false;
         args.UseLineNumbers = false;
         args.IsFileNamesOnly = false;
         args.ContextLines = -1;
         args.SkipHiddenFile = false;
         args.SkipHiddenDirectory = false;
         args.SkipSystemFile = false;
         args.SkipSystemDirectory = false;

         args.DisplayHelp = false;

         args.OutputPath = string.Empty;
         args.OutputType = string.Empty;
         args.ExitAfterSearch = false;

         args.DateModifiedFile = null;
         args.DateModifiedDirectory = null;

         args.DateCreatedFile = null;
         args.DateCreatedDirectory = null;

         args.MinFileSize = null;
         args.MaxFileSize = null;

         args.MinFileCount = 0;

         args.ReadOnlyFile = false;
      }

      /// <summary>
      /// Parse the given string for valid command line option flags.
      /// </summary>
      /// <param name="myArgs">String containing potential options.</param>
      /// <param name="args">CommandLineArguments structure to hold found options.</param>
      /// <history>
      /// [Curtis_Beard]		07/26/2006	Created
      /// [Curtis_Beard]		05/18/2007	CHG: use new command line arguments
      /// [Curtis_Beard]		09/26/2012	ADD: display help option
      /// [Curtis_Beard]		04/08/2014	CHG: 74, add missing search options, exit, save
      /// [Curtis_Beard]		06/02/2015	CHG: 97, remove /local since portable version created
      /// </history>
      private static void ProcessFlags(Arguments myArgs, ref CommandLineArguments args)
      {
         if (myArgs["s"] != null)
            args.StartSearch = true;

         if (myArgs["e"] != null)
            args.UseRegularExpressions = true;

         if (myArgs["c"] != null)
            args.IsCaseSensitive = true;

         if (myArgs["w"] != null)
            args.IsWholeWord = true;

         if (myArgs["r"] != null)
            args.UseRecursion = true;

         if (myArgs["n"] != null)
            args.IsNegation = true;

         if (myArgs["l"] != null)
            args.UseLineNumbers = true;

         if (myArgs["f"] != null)
            args.IsFileNamesOnly = true;

         if (myArgs["cl"] != null)
         {
            try
            {
               int num = int.Parse(myArgs["cl"]);

               if (num >= 0 && num <= Constants.MAX_CONTEXT_LINES)
                  args.ContextLines = num;
            }
            catch {}
         }

         if (myArgs["sh"] != null)
         {
            args.SkipHiddenFile = true;
            args.SkipHiddenDirectory = true;
         }

         if (myArgs["ss"] != null)
         {
            args.SkipSystemFile = true;
            args.SkipSystemDirectory = true;
         }

         if (myArgs["shf"] != null)
         {
            args.SkipHiddenFile = true;
         }

         if (myArgs["shd"] != null)
         {
            args.SkipHiddenDirectory = true;
         }

         if (myArgs["ssf"] != null)
         {
            args.SkipSystemFile = true;
         }

         if (myArgs["ssd"] != null)
         {
            args.SkipSystemDirectory = true;
         }

         if (myArgs["spath"] != null)
         {
            if (System.IO.Directory.Exists(myArgs["spath"]))
            {
               args.IsValidStartPath = true;
               args.StartPath = myArgs["spath"];
            }
         }         

         if (myArgs["stypes"] != null)
         {
            args.IsValidFileTypes = true;
            args.FileTypes = myArgs["stypes"];
         }

         if (myArgs["stext"] != null)
         {
            args.IsValidSearchText = true;
            args.SearchText = myArgs["stext"];
         }

         if (myArgs["h"] != null || myArgs["?"] != null || myArgs["help"] != null)
         {
            args.DisplayHelp = true;
         }

         if (myArgs["opath"] != null)
         {
            args.OutputPath = myArgs["opath"];

            // default to txt (override by supplying outputtype)
            args.OutputType = "txt";

            // since they want to save results, then we have to start search
            args.StartSearch = true;
         }

         if (myArgs["otype"] != null)
         {
            args.OutputType = myArgs["otype"].ToLower();

            // set default path if not defined
            if (string.IsNullOrEmpty(args.OutputPath))
            {
               args.OutputPath = System.IO.Path.Combine(Environment.CurrentDirectory, string.Format("results.{0}", args.OutputType));
            }

            // since they want to save results, then we have to start search
            args.StartSearch = true;
         }

         if (myArgs["exit"] != null)
         {
            args.ExitAfterSearch = true;
         }

         if (myArgs["dmf"] != null)
         {
            string[] values = myArgs["dmf"].Split('|');
            if (values.Length == 2)
            {
               libAstroGrep.FilterType.ValueOptions valueOption = GetValueOptionFromOperator(values[0]);
               DateTime value = DateTime.MinValue;
               if (DateTime.TryParse(values[1], out value) && valueOption != libAstroGrep.FilterType.ValueOptions.None)
               {
                  args.DateModifiedFile = new ValueOptionPair() { Value = value, ValueOption = valueOption };
               }
            }
         }

         if (myArgs["dmd"] != null)
         {
            string[] values = myArgs["dmd"].Split('|');
            if (values.Length == 2)
            {
               libAstroGrep.FilterType.ValueOptions valueOption = GetValueOptionFromOperator(values[0]);
               DateTime value = DateTime.MinValue;
               if (DateTime.TryParse(values[1], out value) && valueOption != libAstroGrep.FilterType.ValueOptions.None)
               {
                  args.DateModifiedDirectory = new ValueOptionPair() { Value = value, ValueOption = valueOption };
               }
            }
         }

         if (myArgs["dcf"] != null)
         {
            string[] values = myArgs["dcf"].Split('|');
            if (values.Length == 2)
            {
               libAstroGrep.FilterType.ValueOptions valueOption = GetValueOptionFromOperator(values[0]);
               DateTime value = DateTime.MinValue;
               if (DateTime.TryParse(values[1], out value) && valueOption != libAstroGrep.FilterType.ValueOptions.None)
               {
                  args.DateCreatedFile = new ValueOptionPair() { Value = value, ValueOption = valueOption };
               }
            }
         }

         if (myArgs["dcd"] != null)
         {
            string[] values = myArgs["dcd"].Split('|');
            if (values.Length == 2)
            {
               libAstroGrep.FilterType.ValueOptions valueOption = GetValueOptionFromOperator(values[0]);
               DateTime value = DateTime.MinValue;
               if (DateTime.TryParse(values[1], out value) && valueOption != libAstroGrep.FilterType.ValueOptions.None)
               {
                  args.DateCreatedDirectory = new ValueOptionPair() { Value = value, ValueOption = valueOption };
               }
            }
         }

         if (myArgs["minfsize"] != null)
         {
            string[] values = myArgs["minfsize"].Split('|');
            if (values.Length == 2)
            {
               libAstroGrep.FilterType.ValueOptions valueOption = GetValueOptionFromOperator(values[0]);
               long value = 0;
               if (Int64.TryParse(values[1], out value) && valueOption != libAstroGrep.FilterType.ValueOptions.None)
               {
                  args.MinFileSize = new ValueOptionPair() { Value = value, ValueOption = valueOption };
               }
            }
         }

         if (myArgs["maxfsize"] != null)
         {
            string[] values = myArgs["maxfsize"].Split('|');
            if (values.Length == 2)
            {
               libAstroGrep.FilterType.ValueOptions valueOption = GetValueOptionFromOperator(values[0]);
               long value = 0;
               if (Int64.TryParse(values[1], out value) && valueOption != libAstroGrep.FilterType.ValueOptions.None)
               {
                  args.MaxFileSize = new ValueOptionPair() { Value = value, ValueOption = valueOption };
               }
            }
         }

         if (myArgs["minfc"] != null)
         {
            int value = 0;
            if (Int32.TryParse(myArgs["minfc"], out value))
            {
               args.MinFileCount = value;
            }
         }

         if (myArgs["srf"] != null)
         {
            args.ReadOnlyFile = true;
         }
      }

      /// <summary>
      /// Convert a string operator to a FilterType.ValueOptions enum.
      /// </summary>
      /// <param name="op">string value of operator (supports =,!=,&lt;,&gt;,&lt;=,&gt;=)</param>
      /// <returns>FilterType.ValueOptions enum value</returns>
      private static libAstroGrep.FilterType.ValueOptions GetValueOptionFromOperator(string op)
      {
         libAstroGrep.FilterType.ValueOptions valueOption = libAstroGrep.FilterType.ValueOptions.None;

         switch (op)
         {
            case "=":
               valueOption = libAstroGrep.FilterType.ValueOptions.Equals;
               break;
            case "!=":
               valueOption = libAstroGrep.FilterType.ValueOptions.NotEquals;
               break;
            case ">":
               valueOption = libAstroGrep.FilterType.ValueOptions.GreaterThan;
               break;
            case ">=":
               valueOption = libAstroGrep.FilterType.ValueOptions.GreaterThanEquals;
               break;
            case "<":
               valueOption = libAstroGrep.FilterType.ValueOptions.LessThan;
               break;
            case "<=":
               valueOption = libAstroGrep.FilterType.ValueOptions.LessThanEquals;
               break;
         }

         return valueOption;
      }
   }

   /// <summary>
   /// Arguments class
   /// </summary>
   internal class Arguments
   {
      // Variables
      private StringDictionary Parameters;

      // Constructor
      public Arguments(string[] Args)
      {
         Parameters=new StringDictionary();
         //Regex Spliter=new Regex(@"^-{1,2}|^/|=|:",RegexOptions.IgnoreCase|RegexOptions.Compiled);
         Regex Spliter=new Regex(@"^-{1,2}|^/|=",RegexOptions.IgnoreCase|RegexOptions.Compiled);
         Regex Remover= new Regex(@"^['""]?(.*?)['""]?$",RegexOptions.IgnoreCase|RegexOptions.Compiled);
         string Parameter=null;
         string[] Parts;

         // Valid parameters forms:
         // {-,/,--}param{ ,=,:}((",')value(",'))
         // Examples: -param1 value1 --param2 /param3:"Test-:-work" /param4=happy -param5 '--=nice=--'
         foreach(string Txt in Args)
         {
            // Look for new parameters (-,/ or --) and a possible enclosed value (=,:)
            Parts=Spliter.Split(Txt,3);
            switch(Parts.Length)
            {
                  // Found a value (for the last parameter found (space separator))
               case 1:
                  if(Parameter!=null)
                  {
                     if(!Parameters.ContainsKey(Parameter))
                     {
                        Parts[0]=Remover.Replace(Parts[0],"$1");
                        Parameters.Add(Parameter,Parts[0]);
                     }
                     Parameter=null;
                  }
                  // else Error: no parameter waiting for a value (skipped)
                  break;
                  // Found just a parameter
               case 2:
                  // The last parameter is still waiting. With no value, set it to true.
                  if(Parameter!=null)
                  {
                     if(!Parameters.ContainsKey(Parameter)) Parameters.Add(Parameter,"true");
                  }
                  Parameter=Parts[1];
                  break;
                  // Parameter with enclosed value
               case 3:
                  // The last parameter is still waiting. With no value, set it to true.
                  if(Parameter!=null)
                  {
                     if(!Parameters.ContainsKey(Parameter)) Parameters.Add(Parameter,"true");
                  }
                  Parameter=Parts[1];
                  // Remove possible enclosing characters (",')
                  if(!Parameters.ContainsKey(Parameter))
                  {
                     Parts[2]=Remover.Replace(Parts[2],"$1");
                     Parameters.Add(Parameter,Parts[2]);
                  }
                  Parameter=null;
                  break;
            }
         }
         // In case a parameter is still waiting
         if(Parameter!=null)
         {
            if(!Parameters.ContainsKey(Parameter)) Parameters.Add(Parameter,"true");
         }
      }

      // Retrieve a parameter value if it exists
      public string this [string Param]
      {
         get
         {
            return(Parameters[Param]);
         }
      }
   }
}
