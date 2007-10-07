using System;

namespace AstroGrep.Windows
{
   /// <summary>
   /// Parses and allocates command line arguments in a predefined way.
   /// </summary>
   /// <history>
   /// 	[Curtis_Beard]		07/25/2006	ADD: 1492221, command line parameters
   /// </history>
   public class CommandLineProcessing
   {
      /// <summary>
      /// Holds the parsed command line options.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/26/2006	Created
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
      }

      /// <summary>
      /// Parses the command line arguments for valid options and returns them.
      /// </summary>
      /// <param name="CommandLineArgs">Array of strings containing the command line arguments</param>
      /// <returns>CommandLineArguments structure holding the options from the command line</returns>
      /// <history>
      /// [Curtis_Beard]		07/26/2006	Created
      /// </history>
      public static CommandLineArguments Process(string[] CommandLineArgs)
      {
         CommandLineArguments args = new CommandLineArguments();
         InitializeArgs(ref args);

         // check for command line arguments
         if (CommandLineArgs.Length > 1)
         {
            args.AnyArguments = true;

            // Check command line for a path to start at
            string dir = CommandLineArgs[1];

            // remove an extra quote if (a drive letter
            if (dir.EndsWith("\""))
               dir = dir.Substring(0, dir.Length - 1);

            if (System.IO.Directory.Exists(dir))
            {
               args.StartPath = dir;
               args.IsValidStartPath = true;
            }

            if (CommandLineArgs.Length == 2)
            {
               // just the directory is specified (most cases should be the right-click option)
               // so set the search options to the default values
               args.UseRegularExpressions = AstroGrep.Core.SearchSettings.UseRegularExpressions;
               args.IsCaseSensitive = AstroGrep.Core.SearchSettings.UseCaseSensitivity;
               args.IsWholeWord = AstroGrep.Core.SearchSettings.UseWholeWordMatching;
               args.UseRecursion = AstroGrep.Core.SearchSettings.UseRecursion;
               args.IsNegation = AstroGrep.Core.SearchSettings.UseNegation;
               args.UseLineNumbers = AstroGrep.Core.SearchSettings.IncludeLineNumbers;
               args.IsFileNamesOnly = AstroGrep.Core.SearchSettings.ReturnOnlyFileNames;
            }

            // check for file types
            if (CommandLineArgs.Length > 2)
            {
               string types = CommandLineArgs[2];
               if (types.Length > 0)
               {
                  args.FileTypes = types;
                  args.IsValidFileTypes = true;
               }
            }

            // check for search text
            if (CommandLineArgs.Length > 3)
            {
               string search  = CommandLineArgs[3];
               if (search.Length > 0)  
               {
                  args.SearchText = search;
                  args.IsValidSearchText = true;
               }
            }

            // check for flags
            if (CommandLineArgs.Length > 4)
            {
               string flags = CommandLineArgs[4];
               if (flags.Length > 0)
                  ProcessFlags(flags, ref args);
            }
         }

         return args;
      }

      /// <summary>
      /// Initializes values of the given CommandLineArguments structure.
      /// </summary>
      /// <param name="args">CommandLineArguments to initialize</param>
      /// <history>
      /// [Curtis_Beard]		07/26/2006	Created
      /// </history>
      private static void InitializeArgs(ref CommandLineArguments args)
      {
         args.AnyArguments = false;

         args.StartPath = string.Empty;
         args.IsValidStartPath = false;

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
      }

      /// <summary>
      /// Parse the given string for valid command line option flags.
      /// </summary>
      /// <param name="flags">String containing potential options.</param>
      /// <param name="args">CommandLineArguments structure to hold found options.</param>
      /// <history>
      /// [Curtis_Beard]		07/26/2006	Created
      /// </history>
      private static void ProcessFlags(string flags, ref CommandLineArguments args)
      {
         if (flags.IndexOf("/s") > -1)
            args.StartSearch = true;

         if (flags.IndexOf("/d") > -1)
         {
            // set defaults to application defaults
            args.UseRegularExpressions = AstroGrep.Core.SearchSettings.UseRegularExpressions;
            args.IsCaseSensitive = AstroGrep.Core.SearchSettings.UseCaseSensitivity;
            args.IsWholeWord = AstroGrep.Core.SearchSettings.UseWholeWordMatching;
            args.UseRecursion = AstroGrep.Core.SearchSettings.UseRecursion;
            args.IsNegation = AstroGrep.Core.SearchSettings.UseNegation;
            args.UseLineNumbers = AstroGrep.Core.SearchSettings.IncludeLineNumbers;
            args.IsFileNamesOnly = AstroGrep.Core.SearchSettings.ReturnOnlyFileNames;
         }

         // the rest of the settings can override the defaults if (set
         if (flags.IndexOf("/e") > -1)
            args.UseRegularExpressions = true;

         if (flags.IndexOf("/c") > -1)
            args.IsCaseSensitive = true;

         if (flags.IndexOf("/w") > -1)
            args.IsWholeWord = true;

         if (flags.IndexOf("/r") > -1)
            args.UseRecursion = true;

         if (flags.IndexOf("/n") > -1)
            args.IsNegation = true;

         if (flags.IndexOf("/l") > -1)
            args.UseLineNumbers = true;

         if (flags.IndexOf("/f") > -1)
            args.IsFileNamesOnly = true;
      }
   }
}
