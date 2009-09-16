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
private System.Windows.Forms.Label lblTitle;
private System.Windows.Forms.Label lblVersion;
private System.Windows.Forms.Label lblDescription;
private System.Windows.Forms.Label lblDisclaimer;
private System.Windows.Forms.Label CopyrightLabel;
private System.Windows.Forms.ToolTip toolTip1;

		private void InitializeComponent()
		{
         this.components = new System.ComponentModel.Container();
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmAbout));
         this.lnkHomePage = new System.Windows.Forms.LinkLabel();
         this.LicenseLinkLabel = new System.Windows.Forms.LinkLabel();
         this.cmdOK = new System.Windows.Forms.Button();
         this.HeaderPanel = new System.Windows.Forms.Panel();
         this.picIcon = new System.Windows.Forms.PictureBox();
         this.lblTitle = new System.Windows.Forms.Label();
         this.lblVersion = new System.Windows.Forms.Label();
         this.lblDescription = new System.Windows.Forms.Label();
         this.lblDisclaimer = new System.Windows.Forms.Label();
         this.CopyrightLabel = new System.Windows.Forms.Label();
         this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
         this.SuspendLayout();
         // 
         // lnkHomePage
         // 
         this.lnkHomePage.AutoSize = true;
         this.lnkHomePage.Location = new System.Drawing.Point(8, 312);
         this.lnkHomePage.Name = "lnkHomePage";
         this.lnkHomePage.Size = new System.Drawing.Size(119, 16);
         this.lnkHomePage.TabIndex = 2;
         this.lnkHomePage.TabStop = true;
         this.lnkHomePage.Text = "AstroGrep Home Page";
         this.toolTip1.SetToolTip(this.lnkHomePage, "http://astrogrep.sourceforge.net");
         this.lnkHomePage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHomePage_LinkClicked);
         // 
         // LicenseLinkLabel
         // 
         this.LicenseLinkLabel.AutoSize = true;
         this.LicenseLinkLabel.Location = new System.Drawing.Point(8, 223);
         this.LicenseLinkLabel.Name = "LicenseLinkLabel";
         this.LicenseLinkLabel.Size = new System.Drawing.Size(72, 16);
         this.LicenseLinkLabel.TabIndex = 1;
         this.LicenseLinkLabel.TabStop = true;
         this.LicenseLinkLabel.Text = "GNU License";
         this.toolTip1.SetToolTip(this.LicenseLinkLabel, "http://www.gnu.org/copyleft/gpl.html");
         this.LicenseLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LicenseLinkLabel_LinkClicked);
         // 
         // cmdOK
         // 
         this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.cmdOK.Location = new System.Drawing.Point(344, 312);
         this.cmdOK.Name = "cmdOK";
         this.cmdOK.Size = new System.Drawing.Size(84, 23);
         this.cmdOK.TabIndex = 0;
         this.cmdOK.Text = "&OK";
         this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
         // 
         // HeaderPanel
         // 
         this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.HeaderPanel.Font = new System.Drawing.Font("Sylfaen", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
         this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
         this.HeaderPanel.Name = "HeaderPanel";
         this.HeaderPanel.Size = new System.Drawing.Size(434, 100);
         this.HeaderPanel.TabIndex = 3;
         // 
         // picIcon
         // 
         this.picIcon.Image = ((System.Drawing.Image)(resources.GetObject("picIcon.Image")));
         this.picIcon.Location = new System.Drawing.Point(384, 112);
         this.picIcon.Name = "picIcon";
         this.picIcon.Size = new System.Drawing.Size(32, 32);
         this.picIcon.TabIndex = 4;
         this.picIcon.TabStop = false;
         this.picIcon.Visible = false;
         // 
         // lblTitle
         // 
         this.lblTitle.AutoSize = true;
         this.lblTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblTitle.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
         this.lblTitle.Location = new System.Drawing.Point(8, 102);
         this.lblTitle.Name = "lblTitle";
         this.lblTitle.Size = new System.Drawing.Size(84, 16);
         this.lblTitle.TabIndex = 5;
         this.lblTitle.Text = "Application Title";
         // 
         // lblVersion
         // 
         this.lblVersion.AutoSize = true;
         this.lblVersion.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblVersion.Location = new System.Drawing.Point(8, 117);
         this.lblVersion.Name = "lblVersion";
         this.lblVersion.Size = new System.Drawing.Size(43, 16);
         this.lblVersion.TabIndex = 6;
         this.lblVersion.Text = "Version";
         // 
         // lblDescription
         // 
         this.lblDescription.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblDescription.Location = new System.Drawing.Point(8, 161);
         this.lblDescription.Name = "lblDescription";
         this.lblDescription.Size = new System.Drawing.Size(416, 62);
         this.lblDescription.TabIndex = 7;
         this.lblDescription.Text = "label1";
         // 
         // lblDisclaimer
         // 
         this.lblDisclaimer.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblDisclaimer.Location = new System.Drawing.Point(8, 281);
         this.lblDisclaimer.Name = "lblDisclaimer";
         this.lblDisclaimer.Size = new System.Drawing.Size(416, 20);
         this.lblDisclaimer.TabIndex = 8;
         this.lblDisclaimer.Text = "Created by Theodore Ward and converted to .Net by Curtis Beard";
         // 
         // CopyrightLabel
         // 
         this.CopyrightLabel.AutoSize = true;
         this.CopyrightLabel.Location = new System.Drawing.Point(7, 132);
         this.CopyrightLabel.Name = "CopyrightLabel";
         this.CopyrightLabel.Size = new System.Drawing.Size(219, 16);
         this.CopyrightLabel.TabIndex = 10;
         this.CopyrightLabel.Text = "Copyright (C) 2002-2007 AstroComma Inc.";
         // 
         // frmAbout
         // 
         this.AcceptButton = this.cmdOK;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.cmdOK;
         this.ClientSize = new System.Drawing.Size(434, 348);
         this.Controls.Add(this.CopyrightLabel);
         this.Controls.Add(this.lblVersion);
         this.Controls.Add(this.lblTitle);
         this.Controls.Add(this.LicenseLinkLabel);
         this.Controls.Add(this.lnkHomePage);
         this.Controls.Add(this.lblDisclaimer);
         this.Controls.Add(this.lblDescription);
         this.Controls.Add(this.picIcon);
         this.Controls.Add(this.HeaderPanel);
         this.Controls.Add(this.cmdOK);
         this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "frmAbout";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "About MyApp";
         this.Load += new System.EventHandler(this.frmAbout_Load);
         this.ResumeLayout(false);

      }
		#endregion

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
	}
}
