using System;
using System.Reflection;

using AstroGrep.AstroGrepBase;

namespace AstroGrep.Core
{
   /// <summary>
   /// Handles all routines dealing with plugin management.
   /// </summary>
   /// <history>
   /// 	[Curtis_Beard]		07/28/2006	Created
   /// </history>
   public class PluginManager
   {
      private static PluginCollection __PluginCollection = new PluginCollection();

      #region Public Methods
      /// <summary>
      /// Add a plugin to the collection.
      /// </summary>
      /// <param name="path">Full file path to plugin</param>
      /// <returns>Position of plugin in collection</returns>
      /// <history>
      /// [Curtis_Beard]		07/28/2006	Created
      /// </history>
      public static int Add(string path)
      {
         PluginWrapper plugin = LoadPlugin(path);

         if (plugin != null)
            return __PluginCollection.Add(plugin);

         return -1;
      }

      /// <summary>
      /// Add a plugin to the collection.
      /// </summary>
      /// <param name="wrapper">Plugin to add (PluginWrapper)</param>
      /// <returns>Position of plugin in collection</returns>
      /// <history>
      /// [Curtis_Beard]		07/28/2006	Created
      /// </history>
      public static int Add(PluginWrapper wrapper)
      {
         return __PluginCollection.Add(wrapper);
      }

      /// <summary>
      /// Add a plugin to the collection.
      /// </summary>
      /// <param name="plugin">Plugin to add (IAstroGrepPlugin)</param>
      /// <returns>Position of plugin in collection</returns>
      /// <history>
      /// [Curtis_Beard]		07/28/2006	Created
      /// </history>
      public static int Add(IAstroGrepPlugin plugin)
      {
         PluginWrapper wrapper = new PluginWrapper(plugin, string.Empty, plugin.Name, true, true);
         return __PluginCollection.Add(wrapper);
      }

      /// <summary>
      /// Removes all plugins from collection.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/28/2006	Created
      /// </history>
      public static void Clear()
      {
         __PluginCollection.Clear();
      }

      /// <summary>
      /// Gets the total number of plugins.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/28/2006	Created
      /// </history>
      public static int Count
      {
         get { return __PluginCollection.Count; }
      }

      /// <summary>
      /// Gets the plugins collection.
      /// </summary>
      /// <value></value>
      /// <history>
      /// [Curtis_Beard]		07/28/2006	Created
      /// </history>
      public static PluginCollection Items
      {
         get { return __PluginCollection; }
      }

      /// <summary>
      /// Load the plugins.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/28/2006	Created
      /// </history>
      public static void Load()
      {
         // for right now, just load the internal plugins
         MicrosoftWordPlugin wordPlugin = new MicrosoftWordPlugin();
         PluginWrapper wrapper = new PluginWrapper(wordPlugin, string.Empty, wordPlugin.Name, true, true);
         Add(wrapper);

         // enable/disable plugins based on saved state (if found)
         string[] plugins = Common.SplitByString(Core.PluginSettings.Plugins, Constants.PLUGIN_SEPARATOR);
         foreach (string plugin in plugins)
         {
            string[] values = Common.SplitByString(plugin, Constants.PLUGIN_ARGS_SEPARATOR);
            if (values.Length == 3)
            {
               string name = values[0];
               string version = values[1];
               string enabled = values[2];

               for (int i = 0; i < __PluginCollection.Count; i++)
               {
                  if (__PluginCollection[i].Plugin.Name.Equals(name) &&
                     __PluginCollection[i].Plugin.Version.Equals(version))
                  {
                     __PluginCollection[i].Enabled = bool.Parse(enabled);
                     break;
                  }
               }
            }
         }
      }

      /// <summary>
      /// Save the plugin states.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/28/2006	Created
      /// </history>
      public static void Save()
      {
         System.Text.StringBuilder plugins = new System.Text.StringBuilder(__PluginCollection.Count);

         for (int i = 0; i < __PluginCollection.Count; i++)
         {
            if (plugins.Length > 0)
               plugins.Append(Constants.PLUGIN_SEPARATOR);

            plugins.AppendFormat("{1}{0}{2}{0}{3}", Constants.PLUGIN_ARGS_SEPARATOR, __PluginCollection[i].Plugin.Name, __PluginCollection[i].Plugin.Version, __PluginCollection[i].Enabled.ToString());
         }

         Core.PluginSettings.Plugins = plugins.ToString();
         Core.PluginSettings.Save();
      }
      #endregion

      #region Private Methods
      /// <summary>
      /// Load all plugins in a given directory.
      /// </summary>
      /// <param name="path">Full directory path to plugins</param>
      /// <history>
      /// [Curtis_Beard]		09/08/2006	Created
      /// </history>
      private static void LoadPluginsFromDirectory(string path)
      {
         string[] files = System.IO.Directory.GetFiles(path, "*.dll");

         foreach (string file in files)
            Add(file);
      }

      /// <summary>
      /// Load a plugin from file.
      /// </summary>
      /// <param name="path">Full file path to plugin</param>
      /// <returns>PluginWrapper containing plugin</returns>
      /// <history>
      /// [Curtis_Beard]		09/08/2006	Created
      /// </history>
      private static PluginWrapper LoadPlugin(string path)
      {
         try
         {
            Assembly dll = Assembly.LoadFrom(path);

            Type objInterface;

            // Loop through each type in the DLL
            foreach (Type objType in dll.GetTypes())
            {
               // Only look at public types
               if (objType.IsPublic)
               {
                  // Ignore abstract classes
                  if (!((objType.Attributes & TypeAttributes.Abstract) == TypeAttributes.Abstract))
                  {
                     // See if this type implements our interface
                     objInterface = objType.GetInterface("AstroGrepBase.IAstroGrepPlugin", true);
                     if (objInterface != null)
                     {
                        // Load dll
                        // Create and return class instance
                        object objPlugin = dll.CreateInstance(objType.FullName);

                        IAstroGrepPlugin plugin = (IAstroGrepPlugin)objPlugin;
                        PluginWrapper wrapper = new PluginWrapper(plugin, path, dll.FullName, true, false);

                        return wrapper;
                     }
                  }
               }
            }
         }
         catch {}

         return null;
      }
      #endregion
   }
}