using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using AstroGrep.Common;
using AstroGrep.Core;

namespace AstroGrep.Windows.Forms
{
   /// <summary>
   /// Handles getting/showing user an update to this program if available.  Currently used instead of full
   /// frmCheckForUpdate dialog as actually updating the program from an installer isn't ready yet.
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
   /// [Curtis_Beard]	   11/26/2014	Initial: partial update to update checking
   /// </history>
   public partial class frmCheckForUpdateTemp : Form
   {
      private delegate void UpdateMessageCallBack(string message, string newestVersion);

      /// <summary>
      /// Creates an instance of this class.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   11/26/2014	Initial: partial update to update checking
      /// </history>
      public frmCheckForUpdateTemp()
      {
         InitializeComponent();
      }

      /// <summary>
      /// Handles OK button click to close the form.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   11/26/2014	Initial: partial update to update checking
      /// </history>
      private void btnOK_Click(object sender, EventArgs e)
      {
         Close();
      }

      /// <summary>
      /// Handles setting up the form's controls.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   11/26/2014	Initial: partial update to update checking
      /// </history>
      private void frmCheckForUpdateTemp_Load(object sender, EventArgs e)
      {
         Language.ProcessForm(this);

         // start update process on separate thread
         Thread thread = new Thread(CheckForUpdate) { IsBackground = true };
         thread.Start();
      }

      /// <summary>
      /// Preform the update process.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   02/07/2012	Initial: 3485450, add check for updates
      /// [Curtis_Beard]	   11/26/2014	CHG: partial update to update checking
      /// [Curtis_Beard]		06/02/2015	CHG: use Common code to get url
      /// </history>
      private void CheckForUpdate()
      {
         try
         {
            // give some delay to see the checking for version message
            Thread.Sleep(2000);

            WebRequest webReq = WebRequest.Create(ProductInformation.VersionUrl);
            webReq.Proxy = WebRequest.GetSystemWebProxy();
            webReq.Timeout = 9000;

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
               Version currentVersion = ProductInformation.ApplicationVersion;

               if (currentVersion.CompareTo(newestVersion) < 0)
               {
                  UpdateMessage(string.Format(Language.GetGenericText("Update.NewVersion"), ProductInformation.ApplicationName, newestVersion.ToString(3)), newestVersion.ToString(3));
               }
               else
               {
                  UpdateMessage(string.Format(Language.GetGenericText("Update.Current"), ProductInformation.ApplicationName, currentVersion.ToString(3)), string.Empty);
               }
            }
         }
         catch
         {
            UpdateMessage(Language.GetGenericText("Update.Error"), string.Empty);
         }
      }

      /// <summary>
      /// Display a message to the user (thread safe).
      /// </summary>
      /// <param name="message">message to display</param>
      /// <param name="newestVersion">newest version, string.empty if not a new version</param>
      /// <history>
      /// [Curtis_Beard]	   02/07/2012	Initial: 3485450, add check for updates
      /// [Curtis_Beard]	   11/26/2014	CHG: partial update to update checking
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
            lnkDownload.Text = string.Format(Language.GetGenericText("Update.Latest"), ProductInformation.ApplicationName, newestVersion);
         }
      }

      /// <summary>
      /// Launch default browser to download file.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   02/07/2012	Initial: 3485450, add check for updates
      /// [Curtis_Beard]	   11/26/2014	CHG: partial update to update checking
      /// [Curtis_Beard]		06/02/2015	CHG: use Common code to get url
      /// </history>
      private void lnkDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         System.Diagnostics.Process.Start(ProductInformation.DownloadUrl);
      }
   }
}
