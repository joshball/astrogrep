using System;
using System.Collections.Generic;
using System.Reflection;

using AstroGrep.Core.Logging;
using libAstroGrep.Plugin;

namespace AstroGrep.Core
{
   /// <summary>
   /// Handles all routines dealing with plugin management.
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
   /// 	[Curtis_Beard]		07/28/2006	Created
   /// </history>
   public class PluginManager
   {
      private static List<PluginWrapper> __PluginCollection = new List<PluginWrapper>();
      private const string DELIMETER = "|;;|";

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
         {
            plugin.Index = __PluginCollection.Count;
            __PluginCollection.Add(plugin);
            return __PluginCollection.Count - 1;
         }

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
         __PluginCollection.Add(wrapper);
         return __PluginCollection.Count - 1;
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
         PluginWrapper wrapper = new PluginWrapper(plugin, string.Empty, plugin.Name, true, true, __PluginCollection.Count);
         __PluginCollection.Add(wrapper);
         return __PluginCollection.Count - 1;
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
      public static List<PluginWrapper> Items
      {
         get { return __PluginCollection; }
      }

      /// <summary>
      /// Load the plugins.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      07/28/2006	Created
      /// [Curtis_Beard]      08/07/2007	CHG: remove check against version
      /// [Curtis_Beard]      10/16/2012	ADD: iFilter plugin, load any external plugins
      /// </history>
      public static void Load()
      {
         // load the internal plugins
         // Mono2.4 doesn't like the internal plugins.
         if (Type.GetType("System.MonoType") == null)
         {
            Plugin.MicrosoftWord.MicrosoftWordPlugin wordPlugin = new Plugin.MicrosoftWord.MicrosoftWordPlugin();
            PluginWrapper wrapper = new PluginWrapper(wordPlugin, string.Empty, wordPlugin.Name, true, true, 0);
            Add(wrapper);

            Plugin.IFilter.IFilterPlugin iFilterPlugin = new Plugin.IFilter.IFilterPlugin();
            PluginWrapper iFilterWrapper = new PluginWrapper(iFilterPlugin, string.Empty, iFilterPlugin.Name, true, false, 1);
            Add(iFilterWrapper);
         }

         // load any external plugins
         string pluginPath = System.IO.Path.Combine(Constants.ProductLocation, "Plugins");
         if (System.IO.Directory.Exists(pluginPath))
         {
            LoadPluginsFromDirectory(pluginPath);
         }

         // enable/disable plugins based on saved state (if found)
         string[] plugins = Common.SplitByString(Core.PluginSettings.Plugins, DELIMETER);
         foreach (string plugin in plugins)
         {
            string[] values = Common.SplitByString(plugin, Constants.PLUGIN_ARGS_SEPARATOR);
            if (values.Length == 3 || values.Length == 4)
            {
               string name = values[0];
               string version = values[1];
               string enabled = values[2];

               for (int i = 0; i < __PluginCollection.Count; i++)
               {
                  if (__PluginCollection[i].Plugin.Name.Equals(name))
                  {
                     __PluginCollection[i].Enabled = bool.Parse(enabled);
                     __PluginCollection[i].Index = values.Length == 4 ? int.Parse(values[3]) : i;
                     break;
                  }
               }
            }
         }

         // adjust order
         __PluginCollection.Sort(new PluginWrapperComparer());
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
            {
               plugins.Append(DELIMETER);
            }

            plugins.AppendFormat("{1}{0}{2}{0}{3}{0}{4}", Constants.PLUGIN_ARGS_SEPARATOR, __PluginCollection[i].Plugin.Name, __PluginCollection[i].Plugin.Version, __PluginCollection[i].Enabled.ToString(), i);
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
      /// [Curtis_Beard]		06/28/2007	CHG: make all plugins external and enabled by default
      /// [Curtis_Beard]	   04/08/2015	CHG: add logging
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
                     objInterface = objType.GetInterface("libAstroGrep.Plugin.IAstroGrepPlugin", true);
                     if (objInterface != null)
                     {
                        // Load dll
                        // Create and return class instance
                        object objPlugin = dll.CreateInstance(objType.FullName);

                        IAstroGrepPlugin plugin = (IAstroGrepPlugin)objPlugin;
                        PluginWrapper wrapper = new PluginWrapper(plugin, path, dll.FullName, false, true, 0);

                        return wrapper;
                     }
                  }
               }
            }
         }
         catch (Exception ex) 
         {
            LogClient.Instance.Logger.Error("Unable to load plugin at {0} with message {1}", path, ex.Message);
         }

         return null;
      }
      #endregion

      internal class PluginWrapperComparer : IComparer<PluginWrapper>
      {
         public int Compare(PluginWrapper x, PluginWrapper y)
         {
            return x.Index.CompareTo(y.Index);
         }
      }
   }
}