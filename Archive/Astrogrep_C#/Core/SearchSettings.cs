using System;
using System.IO;

namespace AstroGrep.Core
{
   /// <summary>
   /// Used to access the search option settings.
   /// </summary>
   /// <history>
   /// [Curtis_Beard]      11/02/2006  Created
   /// </history>
   public sealed class SearchSettings
   {
      // This class is fully static.
      private SearchSettings()  {}

      #region Declarations
      private static SearchSettings __MySettings = null;

      private static readonly string Location = Path.Combine(Environment.CurrentDirectory, "AstroGrep.search.config");
      private const string VERSION = "1.0";

      private bool regularExpressions = false;
      private bool caseSensitive = false;
      private bool wholeWord = false;
      private bool recurse = true;
      private bool fileNamesOnly = false;
      private bool negation = false;
      private bool lineNumbers = true;
      private int contextLines = 0;
      #endregion

      /// <summary>
      /// Contains the static reference of this class.
      /// </summary>
      private static SearchSettings MySettings
      {
         get
         {
            if (__MySettings == null)
            {
               __MySettings = new SearchSettings();
               SettingsIO.Load(__MySettings, Location, VERSION);
            }
            return __MySettings;
         }
      }

      /// <summary>
      /// Save the search options.
      /// </summary>
      /// <returns>Returns true on success, false otherwise</returns>
      public static bool Save()
      {
         return SettingsIO.Save(MySettings, Location, VERSION);
      }

      /// <summary>
      /// Use regular expressions.
      /// </summary>
      static public bool UseRegularExpressions
      {
         get { return MySettings.regularExpressions; }
         set { MySettings.regularExpressions = value; }
      }

      /// <summary>
      /// Use case sensitivity.
      /// </summary>
      static public bool UseCaseSensitivity
      {
         get { return MySettings.caseSensitive; }
         set { MySettings.caseSensitive = value; }
      }

      /// <summary>
      /// Use whole word matching.
      /// </summary>
      static public bool UseWholeWordMatching
      {
         get { return MySettings.wholeWord; }
         set { MySettings.wholeWord = value; }
      }

      /// <summary>
      /// Use recursion to search sub directories.
      /// </summary>
      static public bool UseRecursion
      {
         get { return MySettings.recurse; }
         set { MySettings.recurse = value; }
      }

      /// <summary>
      /// Retrieve only file names.
      /// </summary>
      static public bool ReturnOnlyFileNames
      {
         get { return MySettings.fileNamesOnly; }
         set { MySettings.fileNamesOnly = value; }
      }

      /// <summary>
      /// Retrieve all files not containing search text.
      /// </summary>
      static public bool UseNegation
      {
         get { return MySettings.negation; }
         set { MySettings.negation = value; }
      }

      /// <summary>
      /// Include line numbers.
      /// </summary>
      static public bool IncludeLineNumbers
      {
         get { return MySettings.lineNumbers; }
         set { MySettings.lineNumbers = value; }
      }

      /// <summary>
      /// Number of context lines to display.
      /// </summary>
      static public int ContextLines
      {
         get { return MySettings.contextLines; }
         set { MySettings.contextLines = value; }
      }
   }
}
