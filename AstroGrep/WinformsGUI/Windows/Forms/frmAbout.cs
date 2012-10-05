using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
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
      private delegate void UpdateMessageCallBack(string message, string newestVersion);

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
      /// </history>
      private void HeaderPanel_Paint(object sender, PaintEventArgs e)
      {
         const int _borderBuffer = 10;

         using (Graphics _graphics = e.Graphics)
         {
            _graphics.SmoothingMode = SmoothingMode.HighQuality;
            _graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Gradient the panel
            using (LinearGradientBrush _gradientBrush = new LinearGradientBrush(new RectangleF(0, 0, HeaderPanel.Width, HeaderPanel.Height), Common.ASTROGREP_ORANGE, Color.White, LinearGradientMode.ForwardDiagonal))
            {
               _graphics.FillRectangle(_gradientBrush, new RectangleF(0, 0, HeaderPanel.Width, HeaderPanel.Height));
            }

            // Draw picture
            int picX = _borderBuffer;
            int picY = _borderBuffer + 20;
            _graphics.DrawImage(picIcon.Image, picX, picY);

            // Draw text
            int _xCorner = _borderBuffer + picIcon.Width + 10;
            int _yCorner = _borderBuffer + 15;
            _graphics.DrawString(Constants.ProductName, HeaderPanel.Font, Brushes.Black, _xCorner, _yCorner);
            _graphics.DrawString(Constants.ProductVersion.ToString(3), new Font("Microsoft Sans Serif", 8.25F), Brushes.Black, _xCorner, _yCorner + 30);

            // Draw a bottom border line
            _graphics.DrawLine(SystemPens.ControlDark, 0, HeaderPanel.Height - 2, HeaderPanel.Width, HeaderPanel.Height - 2);
            _graphics.DrawLine(SystemPens.ControlLightLight, 0, HeaderPanel.Height - 1, HeaderPanel.Width, HeaderPanel.Height - 1);
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

         // start update process on separate thread
         Thread _thread = new Thread(CheckForUpdate) { IsBackground = true };
         _thread.Start();
      }

      /// <summary>
      /// Preform the update process.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   02/07/2012	Initial: 3485450, add check for updates
      /// </history>
      private void CheckForUpdate()
      {
         try
         {
            WebRequest webReq = WebRequest.Create("http://astrogrep.sourceforge.net/version.html");
            webReq.Proxy = WebRequest.GetSystemWebProxy();
            webReq.Timeout = 4000;

            using (WebResponse webRes = webReq.GetResponse())
            {
               // Pipe the stream to a higher level stream reader with the required encoding format. 
               StringBuilder sbResult = new StringBuilder();
               using (StreamReader readStream = new StreamReader(webRes.GetResponseStream(), Encoding.UTF8))
               {
                  // Read 256 charcters at a time.    
                  int bufferSize = 256;
                  Char[] read = new Char[bufferSize];
                  int count;
                  do
                  {
                     // append the 256 characters to the string builder
                     count = readStream.Read(read, 0, bufferSize);
                     sbResult.Append(read, 0, count);
                  } while (count > 0);
               }

               // newest version
               Version newestVersion = new Version(sbResult.ToString());

               // current version
               Version currentVersion = AstroGrep.Constants.ProductVersion;

               if (currentVersion.CompareTo(newestVersion) < 0)
               {
                  UpdateMessage("", newestVersion.ToString(3));
               }
               else
               {
                  UpdateMessage(string.Format(Language.GetGenericText("Update.Current"), Constants.ProductName, currentVersion.ToString(3)), string.Empty);
               }
            }
         }
         catch (Exception ex)
         {
            UpdateMessage(string.Format(Language.GetGenericText("Update.Error"), ex.Message), string.Empty);
         }
      }

      /// <summary>
      /// Display a message to the user (thread safe).
      /// </summary>
      /// <param name="message">message to display</param>
      /// <param name="newestVersion">newest version, string.empty if not a new version</param>
      /// <history>
      /// [Curtis_Beard]	   02/07/2012	Initial: 3485450, add check for updates
      /// </history>
      private void UpdateMessage(string message, string newestVersion)
      {
         if (lblVersionCheck.InvokeRequired)
         {
            UpdateMessageCallBack _delegate = UpdateMessage;
            lblVersionCheck.Invoke(_delegate, new object[2] { message, newestVersion });
            return;
         }

         lblVersionCheck.Text = message;
         if (!string.IsNullOrEmpty(newestVersion))
         {
            lnkDownload.Visible = true;
            lnkDownload.Left = lblVersionCheck.Left;
            lblVersionCheck.Visible = false;
            lnkDownload.Text = string.Format(Language.GetGenericText("Update.Latest"), Constants.ProductName, newestVersion);
         }
      }

      /// <summary>
      /// Launch default browser to download file.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   02/07/2012	Initial: 3485450, add check for updates
      /// </history>
      private void lnkDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         System.Diagnostics.Process.Start("http://astrogrep.sourceforge.net/download/");
      }
   }
}
