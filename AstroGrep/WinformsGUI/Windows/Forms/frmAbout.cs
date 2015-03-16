using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

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

			HeaderPanel.Paint += HeaderPanel_Paint;
		}
	
      /// <summary>
      /// Used to draw custom header
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]      11/03/2005  Created
      /// [Curtis_Beard]      09/12/2006  CHG: Implement panel paint instead of form
      /// [Curtis_Beard]	   02/07/2012  CHG: 3485450, add check for updates
      /// [Curtis_Beard]	   03/02/2015   FIX: 49, graphical glitch when using 125% dpi setting
      /// </history>
      private void HeaderPanel_Paint(object sender, PaintEventArgs e)
      {
         const int _borderBuffer = 10;

         using (Graphics graphics = e.Graphics)
         {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Gradient the panel
            using (LinearGradientBrush _gradientBrush = new LinearGradientBrush(new RectangleF(0, 0, HeaderPanel.Width, HeaderPanel.Height), Core.Common.ASTROGREP_ORANGE, Color.White, LinearGradientMode.ForwardDiagonal))
            {
               graphics.FillRectangle(_gradientBrush, new RectangleF(0, 0, HeaderPanel.Width, HeaderPanel.Height));
            }

            // Draw picture
            int picX = _borderBuffer;
            int picY = (HeaderPanel.Height - picIcon.Height) / 2;
            graphics.DrawImage(picIcon.Image, picX, picY);

            // Draw text
            int dpiChange = 0;
            if (Windows.API.GetCurrentDPIFontScalingSize(graphics) == API.DPIFontScalingSizes.Medium)
            {
                dpiChange = -5;
            }
            int _xCorner = _borderBuffer + picIcon.Width + 10;
            graphics.DrawString(string.Format("{0} {1}", Constants.ProductName, Constants.ProductVersion.ToString(3)), HeaderPanel.Font, Brushes.Black, _xCorner, picY + dpiChange);

            // Draw a bottom border line
            graphics.DrawLine(SystemPens.ControlDark, 0, HeaderPanel.Height - 2, HeaderPanel.Width, HeaderPanel.Height - 2);
            graphics.DrawLine(SystemPens.ControlLightLight, 0, HeaderPanel.Height - 1, HeaderPanel.Width, HeaderPanel.Height - 1);
         }
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
      /// Closes form
      /// </summary>
      /// <param name="sender">System parm</param>
      /// <param name="e">System parm</param>
      /// <history>
      /// [Theodore_Ward]     ??/??/????  Initial
      /// [Curtis_Beard]	   01/11/2005	.Net Conversion
      /// </history>
      private void cmdOK_Click(object sender, EventArgs e)
      {
         this.Close();
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

         this.Text = string.Format(this.Text, Constants.ProductName);
         lnkHomePage.Text = string.Format(lnkHomePage.Text, Constants.ProductName);
         lnkHomePage.Links.Add(0, lnkHomePage.Text.Length, "http://astrogrep.sourceforge.net/");
         CopyrightLabel.Text = string.Format("Copyright (C) 2002-{0} AstroComma Inc.", DateTime.Now.Year.ToString());
      }
   }
}
