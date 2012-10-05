namespace AstroGrep.Windows.Forms
{
	public partial class frmAbout
	{
		#region Windows Form Designer generated code
		private System.Windows.Forms.PictureBox picIcon;
private System.Windows.Forms.Button cmdOK;
private System.Windows.Forms.Panel HeaderPanel;
private System.Windows.Forms.LinkLabel lnkHomePage;
private System.Windows.Forms.LinkLabel LicenseLinkLabel;
private System.Windows.Forms.Label lblDescription;
private System.Windows.Forms.Label lblDisclaimer;
private System.Windows.Forms.Label CopyrightLabel;
private System.Windows.Forms.ToolTip toolTip1;

		private void InitializeComponent()
		{
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
         this.lnkHomePage = new System.Windows.Forms.LinkLabel();
         this.LicenseLinkLabel = new System.Windows.Forms.LinkLabel();
         this.cmdOK = new System.Windows.Forms.Button();
         this.HeaderPanel = new System.Windows.Forms.Panel();
         this.picIcon = new System.Windows.Forms.PictureBox();
         this.lblDescription = new System.Windows.Forms.Label();
         this.lblDisclaimer = new System.Windows.Forms.Label();
         this.CopyrightLabel = new System.Windows.Forms.Label();
         this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
         this.lblVersionCheck = new System.Windows.Forms.Label();
         this.lnkDownload = new System.Windows.Forms.LinkLabel();
         ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
         this.SuspendLayout();
         // 
         // lnkHomePage
         // 
         this.lnkHomePage.AutoSize = true;
         this.lnkHomePage.Location = new System.Drawing.Point(6, 197);
         this.lnkHomePage.Name = "lnkHomePage";
         this.lnkHomePage.Size = new System.Drawing.Size(115, 14);
         this.lnkHomePage.TabIndex = 2;
         this.lnkHomePage.TabStop = true;
         this.lnkHomePage.Text = "AstroGrep Home Page";
         this.toolTip1.SetToolTip(this.lnkHomePage, "http://astrogrep.sourceforge.net");
         this.lnkHomePage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHomePage_LinkClicked);
         // 
         // LicenseLinkLabel
         // 
         this.LicenseLinkLabel.AutoSize = true;
         this.LicenseLinkLabel.Location = new System.Drawing.Point(87, 150);
         this.LicenseLinkLabel.Name = "LicenseLinkLabel";
         this.LicenseLinkLabel.Size = new System.Drawing.Size(70, 14);
         this.LicenseLinkLabel.TabIndex = 1;
         this.LicenseLinkLabel.TabStop = true;
         this.LicenseLinkLabel.Text = "GNU License";
         this.toolTip1.SetToolTip(this.LicenseLinkLabel, "http://www.gnu.org/copyleft/gpl.html");
         this.LicenseLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LicenseLinkLabel_LinkClicked);
         // 
         // cmdOK
         // 
         this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.cmdOK.Location = new System.Drawing.Point(423, 256);
         this.cmdOK.Name = "cmdOK";
         this.cmdOK.Size = new System.Drawing.Size(84, 23);
         this.cmdOK.TabIndex = 0;
         this.cmdOK.Text = "&OK";
         this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
         // 
         // HeaderPanel
         // 
         this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.HeaderPanel.Font = new System.Drawing.Font("Sylfaen", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
         this.HeaderPanel.Name = "HeaderPanel";
         this.HeaderPanel.Size = new System.Drawing.Size(513, 100);
         this.HeaderPanel.TabIndex = 3;
         // 
         // picIcon
         // 
         this.picIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.picIcon.Image = ((System.Drawing.Image)(resources.GetObject("picIcon.Image")));
         this.picIcon.Location = new System.Drawing.Point(469, 218);
         this.picIcon.Name = "picIcon";
         this.picIcon.Size = new System.Drawing.Size(32, 32);
         this.picIcon.TabIndex = 4;
         this.picIcon.TabStop = false;
         this.picIcon.Visible = false;
         // 
         // lblDescription
         // 
         this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lblDescription.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblDescription.Location = new System.Drawing.Point(8, 122);
         this.lblDescription.Name = "lblDescription";
         this.lblDescription.Size = new System.Drawing.Size(495, 43);
         this.lblDescription.TabIndex = 7;
         this.lblDescription.Text = resources.GetString("lblDescription.Text");
         // 
         // lblDisclaimer
         // 
         this.lblDisclaimer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lblDisclaimer.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblDisclaimer.Location = new System.Drawing.Point(8, 177);
         this.lblDisclaimer.Name = "lblDisclaimer";
         this.lblDisclaimer.Size = new System.Drawing.Size(495, 20);
         this.lblDisclaimer.TabIndex = 8;
         this.lblDisclaimer.Text = "Created by Theodore Ward and converted to .Net by Curtis Beard";
         // 
         // CopyrightLabel
         // 
         this.CopyrightLabel.AutoSize = true;
         this.CopyrightLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.CopyrightLabel.Location = new System.Drawing.Point(8, 103);
         this.CopyrightLabel.Name = "CopyrightLabel";
         this.CopyrightLabel.Size = new System.Drawing.Size(211, 14);
         this.CopyrightLabel.TabIndex = 10;
         this.CopyrightLabel.Text = "Copyright (C) 2002-2007 AstroComma Inc.";
         // 
         // lblVersionCheck
         // 
         this.lblVersionCheck.AutoSize = true;
         this.lblVersionCheck.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblVersionCheck.Location = new System.Drawing.Point(8, 260);
         this.lblVersionCheck.Name = "lblVersionCheck";
         this.lblVersionCheck.Size = new System.Drawing.Size(119, 14);
         this.lblVersionCheck.TabIndex = 11;
         this.lblVersionCheck.Text = "Checking for updates...";
         // 
         // lnkDownload
         // 
         this.lnkDownload.AutoSize = true;
         this.lnkDownload.Location = new System.Drawing.Point(328, 260);
         this.lnkDownload.Name = "lnkDownload";
         this.lnkDownload.Size = new System.Drawing.Size(89, 14);
         this.lnkDownload.TabIndex = 12;
         this.lnkDownload.TabStop = true;
         this.lnkDownload.Text = "Download Latest";
         this.toolTip1.SetToolTip(this.lnkDownload, "http://astrogrep.sourceforge.net/download/");
         this.lnkDownload.Visible = false;
         this.lnkDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDownload_LinkClicked);
         // 
         // frmAbout
         // 
         this.AcceptButton = this.cmdOK;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.cmdOK;
         this.ClientSize = new System.Drawing.Size(513, 292);
         this.Controls.Add(this.lnkDownload);
         this.Controls.Add(this.lblVersionCheck);
         this.Controls.Add(this.CopyrightLabel);
         this.Controls.Add(this.LicenseLinkLabel);
         this.Controls.Add(this.lnkHomePage);
         this.Controls.Add(this.lblDisclaimer);
         this.Controls.Add(this.lblDescription);
         this.Controls.Add(this.picIcon);
         this.Controls.Add(this.HeaderPanel);
         this.Controls.Add(this.cmdOK);
         this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "frmAbout";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "About MyApp";
         this.Load += new System.EventHandler(this.frmAbout_Load);
         ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }
		#endregion

      /// <summary>
      /// Dispose form.
      /// </summary>
      /// <param name="disposing">system parameter</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

      private System.Windows.Forms.Label lblVersionCheck;
      private System.Windows.Forms.LinkLabel lnkDownload;
	}
}
