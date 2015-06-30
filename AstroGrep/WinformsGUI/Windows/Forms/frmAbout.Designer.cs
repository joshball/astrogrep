namespace AstroGrep.Windows.Forms
{
   public partial class frmAbout
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components;



      /// <summary>
      /// Dispose form.
      /// </summary>
      /// <param name="disposing">system parameter</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing)
         {
            if (components != null)
            {
               components.Dispose();
            }
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code
      private System.Windows.Forms.PictureBox picIcon;
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
         this.HeaderPanel = new System.Windows.Forms.Panel();
         this.picIcon = new System.Windows.Forms.PictureBox();
         this.lblProductVersion = new System.Windows.Forms.Label();
         this.lblProductName = new System.Windows.Forms.Label();
         this.CopyrightLabel = new System.Windows.Forms.Label();
         this.lblDescription = new System.Windows.Forms.Label();
         this.lblDisclaimer = new System.Windows.Forms.Label();
         this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
         this.HeaderPanel.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
         this.SuspendLayout();
         // 
         // lnkHomePage
         // 
         this.lnkHomePage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.lnkHomePage.AutoSize = true;
         this.lnkHomePage.LinkColor = System.Drawing.Color.Blue;
         this.lnkHomePage.Location = new System.Drawing.Point(10, 202);
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
         this.LicenseLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.LicenseLinkLabel.AutoSize = true;
         this.LicenseLinkLabel.LinkColor = System.Drawing.Color.Blue;
         this.LicenseLinkLabel.Location = new System.Drawing.Point(431, 202);
         this.LicenseLinkLabel.Name = "LicenseLinkLabel";
         this.LicenseLinkLabel.Size = new System.Drawing.Size(70, 14);
         this.LicenseLinkLabel.TabIndex = 1;
         this.LicenseLinkLabel.TabStop = true;
         this.LicenseLinkLabel.Text = "GNU License";
         this.toolTip1.SetToolTip(this.LicenseLinkLabel, "http://www.gnu.org/copyleft/gpl.html");
         this.LicenseLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LicenseLinkLabel_LinkClicked);
         // 
         // HeaderPanel
         // 
         this.HeaderPanel.BackColor = System.Drawing.Color.White;
         this.HeaderPanel.Controls.Add(this.picIcon);
         this.HeaderPanel.Controls.Add(this.lblProductVersion);
         this.HeaderPanel.Controls.Add(this.lblProductName);
         this.HeaderPanel.Controls.Add(this.CopyrightLabel);
         this.HeaderPanel.Controls.Add(this.lblDescription);
         this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.HeaderPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
         this.HeaderPanel.Name = "HeaderPanel";
         this.HeaderPanel.Size = new System.Drawing.Size(513, 165);
         this.HeaderPanel.TabIndex = 3;
         // 
         // picIcon
         // 
         this.picIcon.Image = ((System.Drawing.Image)(resources.GetObject("picIcon.Image")));
         this.picIcon.Location = new System.Drawing.Point(9, 4);
         this.picIcon.Name = "picIcon";
         this.picIcon.Size = new System.Drawing.Size(128, 128);
         this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.picIcon.TabIndex = 4;
         this.picIcon.TabStop = false;
         // 
         // lblProductVersion
         // 
         this.lblProductVersion.AutoSize = true;
         this.lblProductVersion.Location = new System.Drawing.Point(155, 45);
         this.lblProductVersion.Name = "lblProductVersion";
         this.lblProductVersion.Size = new System.Drawing.Size(42, 13);
         this.lblProductVersion.TabIndex = 12;
         this.lblProductVersion.Text = "Version";
         // 
         // lblProductName
         // 
         this.lblProductName.AutoSize = true;
         this.lblProductName.Font = new System.Drawing.Font("Sylfaen", 20.25F, System.Drawing.FontStyle.Italic);
         this.lblProductName.Location = new System.Drawing.Point(152, 7);
         this.lblProductName.Name = "lblProductName";
         this.lblProductName.Size = new System.Drawing.Size(132, 35);
         this.lblProductName.TabIndex = 11;
         this.lblProductName.Text = "AstroGrep";
         // 
         // CopyrightLabel
         // 
         this.CopyrightLabel.AutoSize = true;
         this.CopyrightLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.CopyrightLabel.Location = new System.Drawing.Point(155, 80);
         this.CopyrightLabel.Name = "CopyrightLabel";
         this.CopyrightLabel.Size = new System.Drawing.Size(204, 13);
         this.CopyrightLabel.TabIndex = 10;
         this.CopyrightLabel.Text = "Copyright (C) 2002-2007 AstroComma Inc.";
         // 
         // lblDescription
         // 
         this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lblDescription.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblDescription.Location = new System.Drawing.Point(155, 105);
         this.lblDescription.Name = "lblDescription";
         this.lblDescription.Size = new System.Drawing.Size(339, 55);
         this.lblDescription.TabIndex = 7;
         this.lblDescription.Text = "Additional Copyright (C) 2002 to Theodore L. Ward. AstroGrep comes with ABSOLUTEL" +
    "Y NO WARRANTY. This is free software, and you are welcome to redistribute it und" +
    "er certain conditions.";
         // 
         // lblDisclaimer
         // 
         this.lblDisclaimer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lblDisclaimer.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblDisclaimer.Location = new System.Drawing.Point(12, 170);
         this.lblDisclaimer.Name = "lblDisclaimer";
         this.lblDisclaimer.Size = new System.Drawing.Size(495, 20);
         this.lblDisclaimer.TabIndex = 8;
         this.lblDisclaimer.Text = "Created by Theodore Ward and converted to .Net by Curtis Beard";
         // 
         // frmAbout
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(513, 225);
         this.Controls.Add(this.LicenseLinkLabel);
         this.Controls.Add(this.lnkHomePage);
         this.Controls.Add(this.lblDisclaimer);
         this.Controls.Add(this.HeaderPanel);
         this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "frmAbout";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "About AstroGrep";
         this.Load += new System.EventHandler(this.frmAbout_Load);
         this.HeaderPanel.ResumeLayout(false);
         this.HeaderPanel.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }
      #endregion

      private System.Windows.Forms.Label lblProductVersion;
      private System.Windows.Forms.Label lblProductName;
   }
}
