using System;
using System.IO;

namespace AstroGrep.Core
{
   /// <summary>
   /// Used to access the general settings.
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
   ///   along with this program; if not, write to the Free Software
   ///   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
   /// 
   ///   The author may be contacted at:
   ///   ted@astrocomma.com or curtismbeard@gmail.com
   /// </remarks>
   /// <history>
   /// [Curtis_Beard]      11/02/2006  Created
   /// [Curtis_Beard]      04/14/2007  FIX: 1677004, make sure search panel width is at least default
   /// [Curtis_Beard]      04/25/2007  FIX: 1700029, always get correct config path
   /// [Curtis_Beard]      08/10/2007  CHG: use 800x600 for default size
   /// [Curtis_Beard]	   01/31/2012	ADD: size column width
   /// [Curtis_Beard]	   01/31/2012	CHG: 1947760, update default exclude list to exclude images (bmp,gif,jpg,jpeg,png)
   /// [Curtis_Beard]	   02/24/2012	CHG: 3489693, save state of search options
   /// [Curtis_Beard]	   02/24/2012	CHG: 3488321, ability to change results font
   /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions, empty out exclude list to use search options value instead
   /// [Curtis_Beard]	   10/05/2012	ADD: 1741935, option to show/hide exclusion/error dialog message to user
   /// </history>
   public sealed class GeneralSettings
   {
      // This class is fully static.
      private GeneralSettings()  {}

      /// <summary>Default file panel height</summary>
      public const int DEFAULT_FILE_PANEL_HEIGHT = 195;

      #region Declarations
      private static GeneralSettings __MySettings = null;
      
      private const string VERSION = "1.0";
      private const int DEFAULT_SEARCH_PANEL_WIDTH = 280;

      private string resultsForeColor = string.Format("0{0}0{0}0{0}255", Constants.COLOR_SEPARATOR);
      private string resultsBackColor = string.Format("255{0}255{0}255{0}255", Constants.COLOR_SEPARATOR);
      private string matchForeColor = string.Format("251{0}127{0}6{0}255", Constants.COLOR_SEPARATOR);
      private string matchBackColor = string.Format("255{0}255{0}255{0}255", Constants.COLOR_SEPARATOR);
      private string resultsFont = string.Format("Lucida Console{0}9.75{0}Regular", Constants.FONT_SEPARATOR);
      private int mruListCount = 15;

      private string language = Constants.DEFAULT_LANGUAGE;
      private string extExcludeList = string.Empty;

      private int windowLeft = -1;
      private int windowTop = -1;
      private int windowWidth = 800;
      private int windowHeight = 600;
      private int windowState = -1;
      
      private int searchPanelWidth = DEFAULT_SEARCH_PANEL_WIDTH;
      private int filePanelHeight = DEFAULT_FILE_PANEL_HEIGHT;
      private int columnFile = 100;
      private int columnLocation = 200;
      private int columnDate = 150;
      private int columnCount = 60;
      private int columnSize = 80;

      private string searchStartPaths = string.Empty;
      private string searchFilters = string.Format("*.*{0}*.txt{0}*.java{0}*.htm, *.html{0}*.jsp, *.asp{0}*.js, *.inc{0}*.htm, *.html, *.jsp, *.asp{0}*.sql{0}*.bas, *.cls, *.vb{0}*.cs{0}*.cpp, *.c, *.h{0}*.asm", Constants.SEARCH_ENTRIES_SEPARATOR);
      private string searchTexts = string.Empty;

      private string textEditors = string.Format("notepad{0}%1{0}*", Constants.TEXT_EDITOR_ARGS_SEPARATOR);

      private bool showSearchOptions = false;

      private bool showExclusionErrorMessage = true;
      #endregion
      
      /// <summary>
      /// Contains the static reference of this class.
      /// </summary>
      private static GeneralSettings MySettings
      {
         get
         {
            if (__MySettings == null)
            {
               __MySettings = new GeneralSettings();
               SettingsIO.Load(__MySettings, Location, VERSION);
            }
            return __MySettings;
         }
      }

      /// <summary>
      /// Gets the full location to the config file.
      /// </summary>
      static public string Location
      {
         get
         {
            if (Core.Common.StoreDataLocal)
            {
               return Path.Combine(Constants.ProductLocation, "AstroGrep.general.config");
            }
            else
            {
               string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.ProductName);

               return Path.Combine(path, "AstroGrep.general.config");
            }
         }
      }

      /// <summary>
      /// Save the search options.
      /// </summary>
      /// <returns>Returns true on success, false otherwise</returns>
      static public bool Save()
      {
         return SettingsIO.Save(MySettings, Location, VERSION);
      }

      /// <summary>
      /// Gets/Sets the result fore color.
      /// </summary>
      static public string ResultsForeColor
      {
         get { return MySettings.resultsForeColor; }
         set { MySettings.resultsForeColor = value; }
      }

      /// <summary>
      /// Gets/Sets the result back color.
      /// </summary>
      static public string ResultsBackColor
      {
         get { return MySettings.resultsBackColor; }
         set { MySettings.resultsBackColor = value; }
      }

      /// <summary>
      /// Gets/Sets the highlight fore color.
      /// </summary>
      static public string HighlightForeColor
      {
         get { return MySettings.matchForeColor; }
         set { MySettings.matchForeColor = value; }
      }

      /// <summary>
      /// Gets/Sets the highlight back color.
      /// </summary>
      static public string HighlightBackColor
      {
         get { return MySettings.matchBackColor; }
         set { MySettings.matchBackColor = value; }
      }

      /// <summary>
      /// Gets/Sets the maximum number of mru path to save.
      /// </summary>
      static public int MaximumMRUPaths
      {
         get { return MySettings.mruListCount; }
         set { MySettings.mruListCount = value; }
      }

      /// <summary>
      /// Gets/Sets the application language.
      /// </summary>
      static public string Language
      {
         get { return MySettings.language; }
         set { MySettings.language = value; }
      }

      /// <summary>
      /// Gets/Sets the extension exclusion list (NO LONGER USED).
      /// </summary>
      static public string ExtensionExcludeList
      {
         get { return MySettings.extExcludeList; }
         set { MySettings.extExcludeList = value; }
      }

      /// <summary>
      /// Gets/Sets the window's left value.
      /// </summary>
      static public int WindowLeft
      {
         get { return MySettings.windowLeft; }
         set { MySettings.windowLeft = value; }
      }

      /// <summary>
      /// Gets/Sets the window's top value.
      /// </summary>
      static public int WindowTop
      {
         get { return MySettings.windowTop; }
         set { MySettings.windowTop = value; }
      }

      /// <summary>
      /// Gets/Sets the window's height value.
      /// </summary>
      static public int WindowHeight
      {
         get { return MySettings.windowHeight; }
         set { MySettings.windowHeight = value; }
      }

      /// <summary>
      /// Gets/Sets the window's width value.
      /// </summary>
      static public int WindowWidth
      {
         get { return MySettings.windowWidth; }
         set { MySettings.windowWidth = value; }
      }

      /// <summary>
      /// Gets/Sets the window's WindowState value.
      /// </summary>
      static public int WindowState
      {
         get { return MySettings.windowState; }
         set { MySettings.windowState = value; }
      }

      /// <summary>
      /// Gets/Sets the window's search panel width value.
      /// </summary>
      static public int WindowSearchPanelWidth
      {
         get { return MySettings.searchPanelWidth; }
         set 
         { 
            if (value < DEFAULT_SEARCH_PANEL_WIDTH) 
               value = DEFAULT_SEARCH_PANEL_WIDTH;

            MySettings.searchPanelWidth = value; 
         }
      }

      /// <summary>
      /// Gets/Sets the window's file panel height value.
      /// </summary>
      static public int WindowFilePanelHeight
      {
         get { return MySettings.filePanelHeight; }
         set { MySettings.filePanelHeight = value; }
      }

      /// <summary>
      /// Gets/Sets the window's file list name column value.
      /// </summary>
      static public int WindowFileColumnNameWidth
      {
         get { return MySettings.columnFile; }
         set { MySettings.columnFile = value; }
      }

      /// <summary>
      /// Gets/Sets the window's file list location column value.
      /// </summary>
      static public int WindowFileColumnLocationWidth
      {
         get { return MySettings.columnLocation; }
         set { MySettings.columnLocation = value; }
      }

      /// <summary>
      /// Gets/Sets the window's file list date column value.
      /// </summary>
      static public int WindowFileColumnDateWidth
      {
         get { return MySettings.columnDate; }
         set { MySettings.columnDate = value; }
      }

      /// <summary>
      /// Gets/Sets the window's file list count column value.
      /// </summary>
      static public int WindowFileColumnCountWidth
      {
         get { return MySettings.columnCount; }
         set { MySettings.columnCount = value; }
      }

      /// <summary>
      /// Gets/Sets the window's file list size column value.
      /// </summary>
      static public int WindowFileColumnSizeWidth
      {
         get { return MySettings.columnSize; }
         set { MySettings.columnSize = value; }
      }

      /// <summary>
      /// Gets/Sets the search starting paths.
      /// </summary>
      static public string SearchStarts
      {
         get { return MySettings.searchStartPaths; }
         set { MySettings.searchStartPaths = value; }
      }

      /// <summary>
      /// Gets/Sets the search file filters.
      /// </summary>
      static public string SearchFilters
      {
         get { return MySettings.searchFilters; }
         set { MySettings.searchFilters = value; }
      }

      /// <summary>
      /// Gets/Sets the search's search texts.
      /// </summary>
      static public string SearchTexts
      {
         get { return MySettings.searchTexts; }
         set { MySettings.searchTexts = value; }
      }

      /// <summary>
      /// Gets/Sets the text editors.
      /// </summary>
      static public string TextEditors
      {
         get { return MySettings.textEditors; }
         set { MySettings.textEditors = value; }
      }

      /// <summary>
      /// Gets/Sets display of search options.
      /// </summary>
      static public bool ShowSearchOptions
      {
         get { return MySettings.showSearchOptions; }
         set { MySettings.showSearchOptions = value; }
      }

      /// <summary>
      /// Gets/Sets results font.
      /// </summary>
      static public string ResultsFont
      {
         get { return MySettings.resultsFont; }
         set { MySettings.resultsFont = value; }
      }

      /// <summary>
      /// Gets/sets whether to show the exclusion/error message.
      /// </summary>
      static public bool ShowExclusionErrorMessage
      {
         get { return MySettings.showExclusionErrorMessage; }
         set { MySettings.showExclusionErrorMessage = value; }
      }
   }
}
