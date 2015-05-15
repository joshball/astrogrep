using System;
using System.IO;
using System.Windows.Forms;

using AstroGrep.Core.Logging;
using AstroGrep.Windows;

namespace AstroGrep.Core
{
    /// <summary>
    /// Methods used to create/delete/verify shortcuts.
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
    /// [Curtis_Beard]		11/07/2012	Initial, move methods to own class
    /// </history>
    public class Shortcuts
    {
        #region Public Methods

        /// <summary>
        /// Create or delete an application shortcut on the user's desktop.
        /// </summary>
        /// <param name="create">True to create shortcut, False to delete it</param>
        /// <history>
        /// [Curtis_Beard]		09/05/2006	Created
        /// </history>
        public static void SetDesktopShortcut(bool create)
        {
            CreateApplicationShortcut(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), create);
        }

        /// <summary>
        /// Create or delete an application shortcut on the user's start menu.
        /// </summary>
        /// <param name="create">True to create shortcut, False to delete it</param>
        /// <history>
        /// [Curtis_Beard]		09/05/2006	Created
        /// </history>
        public static void SetStartMenuShortcut(bool create)
        {
            CreateApplicationShortcut(Environment.GetFolderPath(Environment.SpecialFolder.Programs), create);
        }

        /// <summary>
        /// Checks to see if the desktop shortcut exists.
        /// </summary>
        /// <returns>Returns true if the shortcut exists, false otherwise</returns>
        /// <history>
        /// [Curtis_Beard]		09/05/2006	Created
        /// </history>
        public static bool IsDesktopShortcut()
        {
            return IsShortcut(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
        }

        /// <summary>
        /// Checks to see if the start menu shortcut exists.
        /// </summary>
        /// <returns>Returns true if the shortcut exists, false otherwise</returns>
        /// <history>
        /// [Curtis_Beard]		09/05/2006	Created
        /// </history>
        public static bool IsStartMenuShortcut()
        {
            return IsShortcut(Environment.GetFolderPath(Environment.SpecialFolder.Programs));
        }

        /// <summary>
        /// Checks to see if AstroGrep is a search option on right-click of folders
        /// </summary>
        /// <returns>True - set, False - not set</returns>
        /// <history>
        /// [Curtis_Beard]	   10/15/2005	Created
        /// [Curtis_Beard]	   07/11/2006	CHG: remove Folder based if exists
        /// [Curtis_Beard]	   11/13/2006	CHG: renamed from CheckIfSearchOption
        /// </history>
        public static bool IsSearchOption()
        {
            if (Legacy.CheckIfOldSearchOption())
            {
                Legacy.RemoveOldSearchOption();
            }

            Microsoft.Win32.RegistryKey _key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@"Directory\shell\astrogrep", false);

            // key exists
            if (_key != null)
            {
                _key.Close();
                return true;
            }

            // key doesn't
            return false;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates a shortcut (lnk file) using for the application.
        /// </summary>
        /// <param name="location">Directory where the shortcut should be created.</param>
        /// <param name="create">True to create shortcut, False to delete it</param>
        /// <history>
        /// [Curtis_Beard]		09/05/2006	Created
        /// [Curtis_Beard]		10/05/2012	CHG: use ShellLink class to create real shortcut files.
        /// [Curtis_Beard]	  04/08/2015	CHG: add logging
        /// </history>
        private static void CreateApplicationShortcut(string location, bool create)
        {
            string path = System.IO.Path.Combine(location, string.Format("{0}.lnk", Constants.ProductName));
            string oldPath = string.Format("{0}\\{1}.url", location, Constants.ProductName);

            if (create)
            {
                //
                // Create shortcut
                //
                try
                {
                    using (API.ShellLink shortcut = new API.ShellLink())
                    {
                        shortcut.Target = Application.ExecutablePath;
                        shortcut.WorkingDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                        shortcut.Description = string.Empty;
                        shortcut.DisplayMode = API.ShellLink.LinkDisplayMode.edmNormal;
                        shortcut.Save(path);
                    }
                }
                catch (Exception ex)
                {
                   LogClient.Instance.Logger.Error("Unable to create shortcut at {0} with message {1}", location, ex.Message);
                }
            }
            else
            {
                //
                // Delete shortcut, if exists
                //
                try
                {
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);

                    // delete old url if exists
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
                catch (Exception ex)
                {
                   LogClient.Instance.Logger.Error("Unable to delete shortcut at {0} with message {1}", location, ex.Message);
                }
            }
        }

        /// <summary>
        /// Checks to see if a shortcut exists.
        /// </summary>
        /// <param name="location">Directory where the shortcut could be</param>
        /// <returns>Returns true if the shortcut exists, false otherwise</returns>
        /// <history>
        /// [Curtis_Beard]		09/05/2006	Created
        /// [Curtis_Beard]		10/05/2012	CHG: check for new extension.
        /// </history>
        private static bool IsShortcut(string location)
        {
            string path = Path.Combine(location, string.Format("{0}.lnk", Constants.ProductName));
            string oldPath = string.Format("{0}\\{1}.url", location, Constants.ProductName);

            if (File.Exists(path))
                return true;

            // check for older url based shortcut and create new one
            if (File.Exists(oldPath))
            {
                // delete
                CreateApplicationShortcut(location, false);

                // recreate
                CreateApplicationShortcut(location, true);

                return true;
            }

            return false;
        }
        #endregion
    }
}
