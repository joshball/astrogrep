using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroGrep.Common
{
   /// <summary>
   /// Contains common application locations (data folder, log file, etc.)
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
   /// [Curtis_Beard]		06/02/2015	Initial, moved some from Core\Common to here
   /// </history>
   public sealed class ApplicationPaths
   {
      /// <summary>
      /// The data storage location.
      /// </summary>
      public static string DataFolder
      {
         get
         {
            if (ProductInformation.IsPortable)
               return Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            else
               return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ProductInformation.ApplicationName);
         }
      }

      /// <summary>The full path to the cache directory</summary>
      public static string CacheDirectory = Path.Combine(DataFolder, "Cache");

      /// <summary>The full path to the log directory</summary>
      public static string LogDirectory = Path.Combine(DataFolder, "Log");
      /// <summary>The full path to the log file</summary>
      public static string LogFile = Path.Combine(LogDirectory, ProductInformation.ApplicationName + ".log");
      /// <summary>The full path to the log archive file</summary>
      public static string LogArchiveFile = Path.Combine(LogDirectory, ProductInformation.ApplicationName + ".{#}.log");

      /// <summary>The full path to the executing assembly's folder</summary>
      public static string ExecutionFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
      /// <summary>The full path to the executing assembly</summary>
      public static string ExecutingAssembly = Path.GetFullPath(System.Reflection.Assembly.GetEntryAssembly().Location);
   }
}
