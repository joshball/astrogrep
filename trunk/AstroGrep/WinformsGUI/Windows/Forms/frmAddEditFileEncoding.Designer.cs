namespace AstroGrep.Windows.Forms
{
   partial class frmAddEditFileEncoding
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
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddEditFileEncoding));
         this.btnOK = new System.Windows.Forms.Button();
         this.btnCancel = new System.Windows.Forms.Button();
         this.txtFile = new System.Windows.Forms.TextBox();
         this.cboEncodings = new System.Windows.Forms.ComboBox();
         this.lblFilePath = new System.Windows.Forms.Label();
         this.lblEncoding = new System.Windows.Forms.Label();
         this.picBrowse = new AstroGrep.Windows.Controls.PictureButton();
         this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
         ((System.ComponentModel.ISupportInitialize)(this.picBrowse)).BeginInit();
         this.SuspendLayout();
         // 
         // btnOK
         // 
         this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnOK.Location = new System.Drawing.Point(236, 161);
         this.btnOK.Name = "btnOK";
         this.btnOK.Size = new System.Drawing.Size(75, 23);
         this.btnOK.TabIndex = 0;
         this.btnOK.Text = "&OK";
         this.btnOK.UseVisualStyleBackColor = true;
         this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
         // 
         // btnCancel
         // 
         this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnCancel.Location = new System.Drawing.Point(317, 161);
         this.btnCancel.Name = "btnCancel";
         this.btnCancel.Size = new System.Drawing.Size(75, 23);
         this.btnCancel.TabIndex = 1;
         this.btnCancel.Text = "&Cancel";
         this.btnCancel.UseVisualStyleBackColor = true;
         this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
         // 
         // txtFile
         // 
         this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.txtFile.Location = new System.Drawing.Point(15, 28);
         this.txtFile.Name = "txtFile";
         this.txtFile.Size = new System.Drawing.Size(353, 20);
         this.txtFile.TabIndex = 2;
         // 
         // cboEncodings
         // 
         this.cboEncodings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.cboEncodings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cboEncodings.FormattingEnabled = true;
         this.cboEncodings.Location = new System.Drawing.Point(15, 87);
         this.cboEncodings.Name = "cboEncodings";
         this.cboEncodings.Size = new System.Drawing.Size(377, 21);
         this.cboEncodings.TabIndex = 3;
         // 
         // lblFilePath
         // 
         this.lblFilePath.AutoSize = true;
         this.lblFilePath.Location = new System.Drawing.Point(12, 9);
         this.lblFilePath.Name = "lblFilePath";
         this.lblFilePath.Size = new System.Drawing.Size(48, 13);
         this.lblFilePath.TabIndex = 4;
         this.lblFilePath.Text = "File Path";
         // 
         // lblEncoding
         // 
         this.lblEncoding.AutoSize = true;
         this.lblEncoding.Location = new System.Drawing.Point(12, 68);
         this.lblEncoding.Name = "lblEncoding";
         this.lblEncoding.Size = new System.Drawing.Size(52, 13);
         this.lblEncoding.TabIndex = 5;
         this.lblEncoding.Text = "Encoding";
         // 
         // picBrowse
         // 
         this.picBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.picBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
         this.picBrowse.Image = ((System.Drawing.Image)(resources.GetObject("picBrowse.Image")));
         this.picBrowse.Location = new System.Drawing.Point(372, 29);
         this.picBrowse.Name = "picBrowse";
         this.picBrowse.Size = new System.Drawing.Size(16, 16);
         this.picBrowse.TabIndex = 6;
         this.picBrowse.TabStop = false;
         this.picBrowse.Click += new System.EventHandler(this.picBrowse_Click);
         // 
         // frmAddEditFileEncoding
         // 
         this.AcceptButton = this.btnOK;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.btnCancel;
         this.ClientSize = new System.Drawing.Size(404, 196);
         this.Controls.Add(this.picBrowse);
         this.Controls.Add(this.lblEncoding);
         this.Controls.Add(this.lblFilePath);
         this.Controls.Add(this.cboEncodings);
         this.Controls.Add(this.txtFile);
         this.Controls.Add(this.btnCancel);
         this.Controls.Add(this.btnOK);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "frmAddEditFileEncoding";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "File Encoding";
         this.Load += new System.EventHandler(this.frmAddEditForceEncodingFile_Load);
         ((System.ComponentModel.ISupportInitialize)(this.picBrowse)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button btnOK;
      private System.Windows.Forms.Button btnCancel;
      private System.Windows.Forms.TextBox txtFile;
      private System.Windows.Forms.ComboBox cboEncodings;
      private System.Windows.Forms.Label lblFilePath;
      private System.Windows.Forms.Label lblEncoding;
      private Controls.PictureButton picBrowse;
      private System.Windows.Forms.ToolTip toolTip1;
   }
}