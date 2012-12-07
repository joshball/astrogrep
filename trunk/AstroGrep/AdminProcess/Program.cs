using System;
using System.Collections.Generic;
using System.Text;

namespace FolderSearchOption
{
   /// <summary>
   /// Handle setting right click option to search using AstroGrep (asks for Escalation)
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
   ///   along with this program; if (not, write to the Free Software
   ///   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
   /// 
   ///   The author may be contacted at:
   ///   ted@astrocomma.com or curtismbeard@gmail.com
   /// </remarks>
   /// <history>
   /// [Curtis_Beard]      10/09/2012	Initial: 3575507, handle UAC request for right click option
   /// </history>
   class Program
   {
      /// <summary>
      /// Run application with given arguments.
      /// </summary>
      /// <param name="args">Command Line Arguments</param>
      /// <history>
      /// [Curtis_Beard]      10/09/2012	Initial: 3575507, handle UAC request for right click option
      /// </history>
      static void Main(string[] args)
      {
         // expecting:  \"True\" \"C:\\Code\\WinformsGUI\\bin\\Debug\\AstroGrep.EXE\" \"Search using {0}...\"
         if (args.Length > 0 && args.Length <= 3)
         {
            bool setOption = false;
            string path = string.Empty;
            string explorerText = "Search using {0}...";

            // setup values from args
            bool.TryParse(args[0], out setOption);
            path = args[1].Replace("\"", "");
            explorerText = args[2].Replace("\"", "");

            SetAsSearchOption(setOption, path, explorerText);
         }         
      }

      /// <summary>
      /// Set registry entry to make application a right-click option on a foler
      /// </summary>
      /// <param name="setOption">True - Set registry value, False - remove registry value</param>
      /// <param name="path">Full path to AstroGrep.exe</param>
      /// <param name="explorerText">Text to be displayed in Explorer context menu</param>
      /// <history>
      /// [Curtis_Beard]	   10/14/2005	Created
      /// [Curtis_Beard]	   07/11/2006	CHG: use drive/directory instead of folder
      /// [Curtis_Beard]	   11/13/2006	CHG: use try/catch to prevent no access to registry
      /// [Curtis_Beard]	   10/09/2012	CHG: 3575507, moved to separate process to handle UAC requests
      /// </history>
      public static void SetAsSearchOption(bool setOption, string path, string explorerText)
      {
         try
         {
            Microsoft.Win32.RegistryKey _key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@"Directory\shell", true);
            Microsoft.Win32.RegistryKey _driveKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@"Drive\shell", true);
            Microsoft.Win32.RegistryKey _astroGrepKey;
            Microsoft.Win32.RegistryKey _astroGrepDriveKey;

            if (_key != null && _driveKey != null)
            {
               if (setOption)
               {
                  // create keys
                  _astroGrepKey = _key.CreateSubKey("astrogrep");
                  _astroGrepDriveKey = _driveKey.CreateSubKey("astrogrep");

                  if (_astroGrepKey != null && _astroGrepDriveKey != null)
                  {
                     _astroGrepKey.SetValue("", String.Format(explorerText, "&AstroGrep"));
                     _astroGrepDriveKey.SetValue("", String.Format(explorerText, "&AstroGrep"));

                     // shows icon in Windows 7+
                     _astroGrepKey.SetValue("Icon", string.Format("\"{0}\",0", path));
                     _astroGrepDriveKey.SetValue("Icon", string.Format("\"{0}\",0", path));

                     Microsoft.Win32.RegistryKey _commandKey = _astroGrepKey.CreateSubKey("command");
                     Microsoft.Win32.RegistryKey _commandDriveKey = _astroGrepDriveKey.CreateSubKey("command");
                     if (_commandKey != null && _commandDriveKey != null)
                     {
                        string keyValue = string.Format("\"{0}\" \"%L\"", path);
                        _commandKey.SetValue("", keyValue);
                        _commandDriveKey.SetValue("", keyValue);
                     }
                  }
               }
               else
               {
                  // remove keys
                  try
                  {
                     _key.DeleteSubKeyTree("astrogrep");
                     _driveKey.DeleteSubKeyTree("astrogrep");
                  }
                  catch { }
               }
            }
         }
         catch { }
      }
   }
}
