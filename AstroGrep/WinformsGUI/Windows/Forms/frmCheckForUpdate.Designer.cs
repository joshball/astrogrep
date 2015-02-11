namespace AstroGrep.Windows.Forms
{
   partial class frmCheckForUpdate
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckForUpdate));
         this.btnCancel = new System.Windows.Forms.Button();
         this.lblVersionCheck = new System.Windows.Forms.Label();
         this.btnNext = new System.Windows.Forms.Button();
         this.progressBarDownload = new System.Windows.Forms.ProgressBar();
         this.lblDownloadMessage = new System.Windows.Forms.Label();
         this.txtChangeLog = new System.Windows.Forms.TextBox();
         this.lblWhatsNew = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // btnCancel
         // 
         this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnCancel.Location = new System.Drawing.Point(315, 290);
         this.btnCancel.Name = "btnCancel";
         this.btnCancel.Size = new System.Drawing.Size(75, 23);
         this.btnCancel.TabIndex = 0;
         this.btnCancel.Text = "&Cancel";
         this.btnCancel.UseVisualStyleBackColor = true;
         this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
         // 
         // lblVersionCheck
         // 
         this.lblVersionCheck.AutoSize = true;
         this.lblVersionCheck.Location = new System.Drawing.Point(9, 20);
         this.lblVersionCheck.Name = "lblVersionCheck";
         this.lblVersionCheck.Size = new System.Drawing.Size(117, 13);
         this.lblVersionCheck.TabIndex = 1;
         this.lblVersionCheck.Text = "Checking for updates...";
         // 
         // btnNext
         // 
         this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnNext.Location = new System.Drawing.Point(234, 290);
         this.btnNext.Name = "btnNext";
         this.btnNext.Size = new System.Drawing.Size(75, 23);
         this.btnNext.TabIndex = 3;
         this.btnNext.Text = "&Next >";
         this.btnNext.UseVisualStyleBackColor = true;
         this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
         // 
         // progressBarDownload
         // 
         this.progressBarDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.progressBarDownload.Location = new System.Drawing.Point(12, 246);
         this.progressBarDownload.Name = "progressBarDownload";
         this.progressBarDownload.Size = new System.Drawing.Size(378, 23);
         this.progressBarDownload.TabIndex = 4;
         // 
         // lblDownloadMessage
         // 
         this.lblDownloadMessage.AutoSize = true;
         this.lblDownloadMessage.Location = new System.Drawing.Point(9, 206);
         this.lblDownloadMessage.Name = "lblDownloadMessage";
         this.lblDownloadMessage.Size = new System.Drawing.Size(100, 13);
         this.lblDownloadMessage.TabIndex = 5;
         this.lblDownloadMessage.Text = "Download message";
         // 
         // txtChangeLog
         // 
         this.txtChangeLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.txtChangeLog.BackColor = System.Drawing.SystemColors.Window;
         this.txtChangeLog.Location = new System.Drawing.Point(12, 64);
         this.txtChangeLog.Multiline = true;
         this.txtChangeLog.Name = "txtChangeLog";
         this.txtChangeLog.ReadOnly = true;
         this.txtChangeLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.txtChangeLog.Size = new System.Drawing.Size(378, 139);
         this.txtChangeLog.TabIndex = 6;
         this.txtChangeLog.Visible = false;
         // 
         // lblWhatsNew
         // 
         this.lblWhatsNew.AutoSize = true;
         this.lblWhatsNew.Location = new System.Drawing.Point(9, 46);
         this.lblWhatsNew.Name = "lblWhatsNew";
         this.lblWhatsNew.Size = new System.Drawing.Size(66, 13);
         this.lblWhatsNew.TabIndex = 7;
         this.lblWhatsNew.Text = "What\'s new:";
         this.lblWhatsNew.Visible = false;
         // 
         // frmCheckForUpdate
         // 
         this.AcceptButton = this.btnNext;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.btnCancel;
         this.ClientSize = new System.Drawing.Size(402, 325);
         this.Controls.Add(this.lblWhatsNew);
         this.Controls.Add(this.txtChangeLog);
         this.Controls.Add(this.lblDownloadMessage);
         this.Controls.Add(this.progressBarDownload);
         this.Controls.Add(this.btnNext);
         this.Controls.Add(this.lblVersionCheck);
         this.Controls.Add(this.btnCancel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.Name = "frmCheckForUpdate";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Check for Updates";
         this.Load += new System.EventHandler(this.frmCheckForUpdate_Load);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button btnCancel;
      private System.Windows.Forms.Label lblVersionCheck;
      private System.Windows.Forms.Button btnNext;
      private System.Windows.Forms.ProgressBar progressBarDownload;
      private System.Windows.Forms.Label lblDownloadMessage;
      private System.Windows.Forms.TextBox txtChangeLog;
      private System.Windows.Forms.Label lblWhatsNew;
   }
}