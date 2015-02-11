using System;
using System.Windows.Forms;

namespace AstroGrep.Windows
{
    /// <summary>
    /// Startup for application
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
    /// [Curtis_Beard]	   01/27/2005	.Net Conversion/Support for xp themes
    /// [Curtis_Beard]	   10/14/2005	CHG: Made a class instead of module
    /// </history>
    public class Program
    {
        /// <summary>
        /// Starts the application
        /// </summary>
        /// <remarks>
        /// Enables visual styles for the controls if available.
        /// </remarks>
        /// <history>
        /// [Curtis_Beard]	    10/14/2005	Created
        /// [Curtis_Beard]	    07/21/2006	CHG: Use a try/catch block for any erroneous errors
        /// [Curtis_Beard]	    10/11/2006	CHG: Remove setting reference to frmMain in Common class
        /// [Curtis_Beard]     09/26/2012	CHG: 3572487, detect command line arg for displaying help and show dialog with options
        /// </history>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.DoEvents();

                // Parse command line, must be done before any use of user settings and form creation in case of help
                CommandLineProcessing.CommandLineArguments args = CommandLineProcessing.Process(Environment.GetCommandLineArgs());

                Legacy.ConvertLanguageValue();
                Language.Load(AstroGrep.Core.GeneralSettings.Language);

                if (args.AnyArguments && args.DisplayHelp)
                {
                    // display command line options
                    Application.Run(new Windows.Forms.frmCommandLine());
                }
                else
                {
                    // display main form
                    Windows.Forms.frmMain mainForm = new AstroGrep.Windows.Forms.frmMain();
                    mainForm.CommandLineArgs = args;

                    Application.AddMessageFilter(new ListViewMouseWheelMoveMessageFilter());
                    Application.Run(mainForm);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("A critical error occurred and AstroGrep must be shutdown.  Please restart AstroGrep.\n({0})", ex.Message),
               Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}