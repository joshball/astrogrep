using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

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
      /// [Curtis_Beard]   11/03/2005   Created
      /// [Curtis_Beard]   09/12/2006   CHG: Implement panel paint instead of form
      /// </history>
      private void HeaderPanel_Paint(object sender, PaintEventArgs e)
      {
         LinearGradientBrush _gradientBrush = new LinearGradientBrush(new RectangleF(0, 0, HeaderPanel.Width, HeaderPanel.Height), Common.ASTROGREP_ORANGE, Color.White, LinearGradientMode.ForwardDiagonal);
         Graphics _graphics = e.Graphics;
         const int _borderBuffer = 10;

         _graphics.SmoothingMode = SmoothingMode.HighQuality;
         _graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

         // Gradient the panel
         _graphics.FillRectangle(_gradientBrush, new RectangleF(0, 0, HeaderPanel.Width, HeaderPanel.Height));

         // Draw picture
         int _yCorner = HeaderPanel.Height - _borderBuffer - picIcon.Height;
         _graphics.DrawImage(picIcon.Image, _borderBuffer, _yCorner);

         // Draw text
         int _xCorner = picIcon.Width + (2 * _borderBuffer);
         _graphics.DrawString(lblTitle.Text, HeaderPanel.Font, Brushes.Black, _xCorner, _yCorner);

         // Draw a bottom border line
         _graphics.DrawLine(SystemPens.ControlDark, 0, HeaderPanel.Height - 2, HeaderPanel.Width, HeaderPanel.Height - 2);
         _graphics.DrawLine(SystemPens.ControlLightLight, 0, HeaderPanel.Height - 1, HeaderPanel.Width, HeaderPanel.Height - 1);

         // Cleanup
         _gradientBrush.Dispose();
         _graphics.Dispose();
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
      /// </history>
      private void frmAbout_Load(object sender, System.EventArgs e)
      {
         System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
         string _appName = _assembly.GetName().Name;
         System.Diagnostics.FileVersionInfo _info = System.Diagnostics.FileVersionInfo.GetVersionInfo(_assembly.Location);

         this.Text = "About {0}";

         lblVersion.Text = string.Format("Version {0}.{1}.{2}", _info.FileMajorPart, _info.FileMinorPart, _info.FileBuildPart);
         lblTitle.Text = _appName;
         lblDescription.Text = "Additional Copyright (C) 2002 to Theodore L. Ward. AstroGrep comes with ABSOLUTELY NO WARRANTY; for details visit http://www.gnu.org/copyleft/gpl.html This is free software, and you are welcome to redistribute it under certain conditions; http://www.gnu.org/copyleft/gpl.html#SEC3";
         lblDisclaimer.Text = "Created by Theodore Ward and converted to .Net by Curtis Beard";

         // Setup the hyperlinks
         LicenseLinkLabel.Links.Add(0, LicenseLinkLabel.Text.Length, "http://www.gnu.org/copyleft/gpl.html");
         LicenseLinkLabel.LinkColor = Color.Blue;
         lnkHomePage.Text = "{0} Home Page";
         lnkHomePage.LinkColor = Color.Blue;

         //Language.GenerateXml(Me, Application.StartupPath & "\" & Me.Name & ".xml")
         Language.ProcessForm(this);

         this.Text = string.Format(this.Text, _appName);
         lnkHomePage.Text = string.Format(lnkHomePage.Text, _appName);
         lnkHomePage.Links.Add(0, lnkHomePage.Text.Length, "http://astrogrep.sourceforge.net/");
         CopyrightLabel.Text = string.Format("Copyright (C) 2002-{0} AstroComma Inc.", DateTime.Now.Year.ToString());
      }
   }
}
