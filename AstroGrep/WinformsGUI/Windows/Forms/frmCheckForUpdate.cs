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

namespace AstroGrep.Windows.Forms
{
   /// <summary>
   /// 
   /// </summary>
   public partial class frmCheckForUpdate : Form
   {
      private delegate void UpdateMessageCallBack(string message, string newestVersion);
      private delegate void ChangeLogCallBack(string changelog);
      private string installFile = string.Empty;

      /// <summary>
      /// 
      /// </summary>
      public frmCheckForUpdate()
      {
         InitializeComponent();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void btnCancel_Click(object sender, EventArgs e)
      {
         Close();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void frmCheckForUpdate_Load(object sender, EventArgs e)
      {
         Language.ProcessForm(this);

         btnNext.Enabled = false;
         lblDownloadMessage.Visible = false;
         progressBarDownload.Visible = false;

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
            // give some delay to see the checking for version message
            Thread.Sleep(2000);

            WebRequest webReq = WebRequest.Create(ProductInformation.VersionUrl);
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
               Version currentVersion = AstroGrep.Common.ProductInformation.ApplicationVersion;

               //if (currentVersion.CompareTo(newestVersion) < 0)
               //{
               GetLatestChangeLog();
               UpdateMessage(string.Format("A newer version of AstroGrep is available: {0}", newestVersion.ToString(3)), newestVersion.ToString(3));
               //}
               //else
               //{
               //UpdateMessage(string.Format(Language.GetGenericText("Update.Current"), ProductInformation.ApplicationName, currentVersion.ToString(3)), string.Empty);
               //}
            }
         }
         catch
         {
            UpdateMessage(Language.GetGenericText("Update.Error"), string.Empty);
         }
      }

      /// <summary>
      /// 
      /// </summary>
      private void GetLatestChangeLog()
      {
         try
         {
            // Download url example:
            // http://downloads.sourceforge.net/astrogrep/readme.txt

            WebRequest webReq = WebRequest.Create("http://downloads.sourceforge.net/astrogrep/readme.txt");
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

               SetChangeLog(sbResult.ToString());
            }
         }
         catch
         {
            UpdateMessage(Language.GetGenericText("Update.Error"), string.Empty);
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="changelog"></param>
      private void SetChangeLog(string changelog)
      {
         if (!string.IsNullOrEmpty(changelog))
         {
            if (txtChangeLog.InvokeRequired)
            {
               ChangeLogCallBack _delegate = SetChangeLog;
               txtChangeLog.Invoke(_delegate, new object[1] { changelog });
               return;
            }

            lblWhatsNew.Visible = true;
            txtChangeLog.Text = changelog;
            txtChangeLog.Visible = true;
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
            btnNext.Enabled = true;
            btnNext.Tag = newestVersion;
         }
      }

      private void btnNext_Click(object sender, EventArgs e)
      {
         // Download url example:
         // http://downloads.sourceforge.net/astrogrep/AstroGrep_v4.3.1.zip

         string newestVersion = (string)btnNext.Tag;

         try
         {
            if (btnNext.Text == "Finish")
            {
               // run installer
               if (File.Exists(installFile) && (new FileInfo(installFile).Length > 0))
               {
                  System.Diagnostics.Process.Start(installFile);

                  // close all AstroGrep instances but our own
                  var ourProcesses = System.Diagnostics.Process.GetProcesses().Where(pr => pr.ProcessName.Equals(ProductInformation.ApplicationName));
                  var currentProcess = System.Diagnostics.Process.GetCurrentProcess();

                  foreach (var process in ourProcesses)
                  {
                     if (currentProcess == null || process.Id != currentProcess.Id)
                     {
                        // try to close them down nicely, otherwise kill them
                        if (!process.CloseMainWindow())
                        {
                           process.Kill();
                        }
                     }
                  }

                  Close();

                  // call close on main form instead of Application.Exit since method won't be called.
                  foreach (Form frm in Application.OpenForms)
                  {
                     if (frm.Name.Equals("frmMain"))
                     {
                        frm.Close();
                        break;
                     }
                  }
               }
               else
               {
                  MessageBox.Show(string.Format("File doesn't exist or not valid size: {0}", installFile));
               }
            }
            else
            {
               lblDownloadMessage.Visible = true;
               lblDownloadMessage.Text = "Downloading file...";
               progressBarDownload.Visible = true;

               string file = string.Format("AstroGrep_Setup_v{0}.exe", newestVersion);
               string remoteFile = string.Format("http://downloads.sourceforge.net/astrogrep/{0}", file);
               string localFile = Path.Combine(ApplicationPaths.DataFolder, file);
               installFile = localFile;

               if (File.Exists(localFile))
               {
                  progressBarDownload.Value = progressBarDownload.Maximum;
                  lblDownloadMessage.Text = "File downloaded successfully.\nClick Finish to install and relaunch AstroGrep.";
                  btnNext.Text = "Finish";
               }
               else
               {
                  WebClient client = new WebClient();
                  client.Proxy = WebRequest.GetSystemWebProxy();
                  client.DownloadFileCompleted += client_DownloadFileCompleted;
                  client.DownloadProgressChanged += client_DownloadProgressChanged;
                  client.DownloadFileAsync(new Uri(remoteFile), localFile);
               }
            }
         }
         catch (Exception ex)
         {
            //UpdateMessage("Error downloading file.", string.Empty);
            lblDownloadMessage.Text = "Error downloading file.";
            MessageBox.Show(this, ex.ToString());
         }
      }

      void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
      {
         progressBarDownload.Value = e.ProgressPercentage;
      }

      void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
      {
         if (e.Error == null)
         {
            lblDownloadMessage.Text = "File downloaded successfully.\nClick Finish to install and relaunch AstroGrep.";
            btnNext.Text = "Finish";
         }
         else
         {
            lblDownloadMessage.Text = "Error downloading file.";
            MessageBox.Show(this, e.Error.ToString(), "Download Error");
         }
      }
   }
}
