using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

using AstroGrep.Common;
using AstroGrep.Core;

namespace AstroGrep.Windows.Forms
{
   /// <summary>
   /// About Dialog
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
   /// [Theodore_Ward]     ??/??/????  Initial
   /// [Curtis_Beard]	   01/11/2005	.Net Conversion/Cleanup
   /// [Curtis_Beard]	   11/03/2005	CHG: set hover text to link
   /// [Andrew_Radford]    17/08/2008	CHG: Moved Winforms designer stuff to a .designer file
   /// [Curtis_Beard]	   05/06/2014	CHG: removed updater code
   /// [Curtis_Beard]      06/04/2015  CHG: remove header, text, image painting
   /// </history>
	public partial class frmAbout : Form
	{
      private System.ComponentModel.IContainer components;

      /// <summary>
      /// Creates an instance of the frmAbout class.
      /// </summary>
      /// <history>
      /// [Theodore_Ward]     ??/??/????  Initial
      /// [Curtis_Beard]      01/11/2005	.Net Conversion/Cleanup
      /// </history>
		public frmAbout()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      /// <summary>
      /// Opens the systems default browser and displays the web link
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Curtis_Beard] 	11/03/2005	Created
      /// </history>
      private void LicenseLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
      }

      /// <summary>
      /// Opens the systems default browser and displays the web link
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// </history>
      private void lnkHomePage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
      }

      /// <summary>
      /// Load values for form
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Theodore_Ward]     ??/??/????  Initial
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// [Curtis_Beard]	   11/03/2005	CHG: make link always blue, new license link
      /// [Curtis_Beard]	   07/07/2006	CHG: call reflection and fileversion info once
      /// [Curtis_Beard]	   05/18/2007	CHG: always use current year for copyright
      /// [Curtis_Beard]	   02/07/2012	CHG: 3485450, add check for updates, cleanup about dialog
      /// [Curtis_Beard]      06/04/2015  CHG: add product name and version text setup
      /// </history>
      private void frmAbout_Load(object sender, System.EventArgs e)
      {
         this.Text = "About {0}";

         // Setup the hyperlinks
         LicenseLinkLabel.Links.Add(0, LicenseLinkLabel.Text.Length, "http://www.gnu.org/copyleft/gpl.html");
         LicenseLinkLabel.LinkColor = Color.Blue;
         lnkHomePage.Text = "{0} Home Page";
         lnkHomePage.LinkColor = Color.Blue;

         //Language.GenerateXml(Me, Application.StartupPath & "\" & Me.Name & ".xml")
         Language.ProcessForm(this);

         this.Text = string.Format(this.Text, ProductInformation.ApplicationName);
         lnkHomePage.Text = string.Format(lnkHomePage.Text, ProductInformation.ApplicationName);
         lnkHomePage.Links.Add(0, lnkHomePage.Text.Length, "http://astrogrep.sourceforge.net/");
         CopyrightLabel.Text = string.Format("Copyright (C) 2002-{0} AstroComma Inc.", DateTime.Now.Year.ToString());
         lblProductName.Text = ProductInformation.ApplicationName;
         lblProductVersion.Text = string.Format("{0}{1}", ProductInformation.ApplicationVersion.ToString(3), ProductInformation.IsPortable ? " (Portable)" : string.Empty);
      }

      /// <summary>
      /// Process Escape and Enter keys for this form.
      /// </summary>
      /// <param name="keyData">Current key data</param>
      /// <returns>true if processed, false otherwise</returns>
      /// <history>
      /// [Curtis_Beard]      06/04/2015  Initial, handle escape and enter keys
      /// </history>
      protected override bool ProcessDialogKey(Keys keyData)
      {
         if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
         {
            this.Close();
            return true;
         }
         else if (Form.ModifierKeys == Keys.None && keyData == Keys.Enter)
         {
            return true;
         }

         return base.ProcessDialogKey(keyData);
      }
   }
}
