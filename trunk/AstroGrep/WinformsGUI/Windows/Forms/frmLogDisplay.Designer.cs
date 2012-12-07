namespace AstroGrep.Windows.Forms
{
    partial class frmLogDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogDisplay));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.sbtnStatus = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.sbtnExclusions = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.sbtnError = new System.Windows.Forms.ToolStripButton();
            this.lstLog = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbtnStatus,
            this.toolStripSeparator1,
            this.sbtnExclusions,
            this.toolStripSeparator2,
            this.sbtnError});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(10, 5, 1, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1011, 28);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // sbtnStatus
            // 
            this.sbtnStatus.CheckOnClick = true;
            this.sbtnStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbtnStatus.Image = ((System.Drawing.Image)(resources.GetObject("sbtnStatus.Image")));
            this.sbtnStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sbtnStatus.Name = "sbtnStatus";
            this.sbtnStatus.Size = new System.Drawing.Size(43, 20);
            this.sbtnStatus.Text = "Status";
            this.sbtnStatus.CheckedChanged += new System.EventHandler(this.sbtnStatus_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // sbtnExclusions
            // 
            this.sbtnExclusions.CheckOnClick = true;
            this.sbtnExclusions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbtnExclusions.Image = ((System.Drawing.Image)(resources.GetObject("sbtnExclusions.Image")));
            this.sbtnExclusions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sbtnExclusions.Name = "sbtnExclusions";
            this.sbtnExclusions.Size = new System.Drawing.Size(65, 20);
            this.sbtnExclusions.Text = "Exclusions";
            this.sbtnExclusions.CheckedChanged += new System.EventHandler(this.sbtnExclusions_CheckedChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // sbtnError
            // 
            this.sbtnError.CheckOnClick = true;
            this.sbtnError.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbtnError.Image = ((System.Drawing.Image)(resources.GetObject("sbtnError.Image")));
            this.sbtnError.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sbtnError.Name = "sbtnError";
            this.sbtnError.Size = new System.Drawing.Size(36, 20);
            this.sbtnError.Text = "Error";
            this.sbtnError.CheckedChanged += new System.EventHandler(this.sbtnError_CheckedChanged);
            // 
            // lstLog
            // 
            this.lstLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstLog.FullRowSelect = true;
            this.lstLog.HideSelection = false;
            this.lstLog.Location = new System.Drawing.Point(12, 30);
            this.lstLog.MultiSelect = false;
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(987, 307);
            this.lstLog.TabIndex = 2;
            this.lstLog.UseCompatibleStateImageBehavior = false;
            this.lstLog.View = System.Windows.Forms.View.Details;
            this.lstLog.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstLog_KeyUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Type";
            this.columnHeader1.Width = 86;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 668;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Details";
            this.columnHeader3.Width = 168;
            // 
            // frmLogDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 349);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogDisplay";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Log Display";
            this.Load += new System.EventHandler(this.frmLogDisplay_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmLogDisplay_KeyUp);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton sbtnStatus;
        private System.Windows.Forms.ToolStripButton sbtnExclusions;
        private System.Windows.Forms.ToolStripButton sbtnError;
        private System.Windows.Forms.ListView lstLog;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}