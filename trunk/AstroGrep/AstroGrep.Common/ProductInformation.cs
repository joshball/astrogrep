using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroGrep.Common
{
   /// <summary>
   /// Contains common application related information details (like Name, Version, etc.)
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
   public sealed class ProductInformation
   {
      /// <summary>The application's display name</summary>
      public static string ApplicationName = "AstroGrep";

      /// <summary>
      /// The application's current version.
      /// </summary>
      public static Version ApplicationVersion
      {
         get
         {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetEntryAssembly();
            return _assembly.GetName().Version;
         }
      }

      /// <summary>The application's desired color</summary>
      public static Color ApplicationColor = Color.FromArgb(251, 127, 6);

      /// <summary>Determines if application is in portable mode</summary>
      public static bool IsPortable
      {
         get
         {
#if PORTABLE
            return true;
#else
            return false;
#endif
         }
      }

      /// <summary>The url to the help page</summary>
      public static string HelpUrl = "http://astrogrep.sourceforge.net/help/";
      /// <summary>The url to the regular expressions help page</summary>
      public static string RegExHelpUrl = "https://msdn.microsoft.com/en-us/library/az24scfc.aspx";
      /// <summary>The url to the current version</summary>
      public static string VersionUrl = "http://astrogrep.sourceforge.net/version.html";
      /// <summary>The url to the download page</summary>
      public static string DownloadUrl = "http://astrogrep.sourceforge.net/download/";
   }
}