using System;

namespace libAstroGrep.Plugin
{
   /// <summary>
   /// Interface definition for AstroGrep plugins.
   /// </summary>
   /// <remarks>
   ///   AstroGrep File Searching Utility. Written by Theodore L. Ward
   ///   Copyright (C) 2002 AstroComma Incorporated.
   ///   
   ///   This program is free software; you can redistribute it and/or
   ///   modify it under the terms of the GNU General  License
   ///   as published by the Free Software Foundation; either version 2
   ///   of the License, or (at your option) any later version.
   ///   
   ///   This program is distributed in the hope that it will be useful,
   ///   but WITHOUT ANY WARRANTY; without even the implied warranty of
   ///   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   ///   GNU General  License for more details.
   ///   
   ///   You should have received a copy of the GNU General  License
   ///   along with this program; if not, write to the Free Software
   ///   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
   /// 
   ///   The author may be contacted at:
   ///   ted@astrocomma.com or curtismbeard@gmail.com
   /// </remarks>
   /// <history>
   /// [Curtis_Beard]		07/27/2006	Created
   /// [Curtis_Beard]		05/25/2007	ADD: Grep now supports returning an Exception object.
   /// </history>
   public interface IAstroGrepPlugin
   {
      // Plugin Load/Unload
      /// <summary>
      /// Loads the plugin.
      /// </summary>
      /// <returns>Returns true if loaded, false otherwise.</returns>
      bool Load();

      /// <summary>
      /// Loads the plugin.
      /// </summary>
      /// <param name="visible">hide/show plugin information or 
      /// external application during grep process</param>
      /// <returns>Returns true if loaded, false otherwise.</returns>
      bool Load(bool visible);

      /// <summary>
      /// Unloads the plugin.
      /// </summary>
      void Unload();

      // Plugin Grep Methods
      /// <summary>
      /// Method that performs grep.
      /// </summary>
      /// <param name="file">FileInfo containing current file</param>
      /// <param name="ex">Contains an Exception if one occurred</param>
      /// <returns>HitObject containing valid hit</returns>
      HitObject Grep(System.IO.FileInfo file, ISearchSpec searchSpec, ref Exception ex);

      /// <summary>
      /// Method that performs grep.
      /// </summary>
      /// <param name="path">Fully qualified file path</param>
      /// <param name="searchProperties"></param>
      /// <param name="ex">Contains an Exception if one occurred</param>
      /// <returns>HitObject containing valid hit</returns>
      HitObject Grep(string path, ISearchSpec searchSpec, ref Exception ex);

     


      /// <summary>
      /// Gets whether plugin is available to use.
      /// </summary>
      bool IsAvailable
      { get; }

      /// <summary>
      /// Supported extensions of plugin.
      /// </summary>
      string Extensions
      { get; }

      // Plugin Details
      /// <summary>
      /// Display name of plugin.
      /// </summary>
      string Name
      { get; }

      /// <summary>
      /// Version of plugin.
      /// </summary>
      string Version
      { get; }
      /// <summary>
      /// Author of plugin.
      /// </summary>
      string Author
      { get; }

      /// <summary>
      /// Description of plugin.
      /// </summary>
      string Description
      { get; }

     
   }
}