using System;
using System.IO;

namespace AstroGrep.Core
{
   /// <summary>
   /// Used to access the search option settings.
   /// </summary>
   public sealed class PluginSettings
   {
      // This class is fully static.
      private PluginSettings()  {}

      #region Declarations
      private static PluginSettings __MySettings = null;

      private static readonly string Location = Path.Combine(Environment.CurrentDirectory, "AstroGrep.plugins.config");
      private const string VERSION = "1.0";

      private string AllPlugins = string.Empty;
      #endregion

      /// <summary>
      /// Contains the static reference of this class.
      /// </summary>
      private static PluginSettings MySettings
      {
         get
         {
            if (__MySettings == null)
            {
               __MySettings = new PluginSettings();
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
      /// Gets/Sets the string containing all plugins.
      /// </summary>
      static public string Plugins
      {
         get { return MySettings.AllPlugins; }
         set { MySettings.AllPlugins = value; }
      }
   }
}
