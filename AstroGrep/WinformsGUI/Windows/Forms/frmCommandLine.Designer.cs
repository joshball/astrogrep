namespace AstroGrep.Windows.Forms
{
   partial class frmCommandLine
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCommandLine));
         this.btnOK = new System.Windows.Forms.Button();
         this.lstArguments = new System.Windows.Forms.ListView();
         this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.SuspendLayout();
         // 
         // btnOK
         // 
         this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnOK.Location = new System.Drawing.Point(547, 326);
         this.btnOK.Name = "btnOK";
         this.btnOK.Size = new System.Drawing.Size(75, 23);
         this.btnOK.TabIndex = 1;
         this.btnOK.Text = "&OK";
         this.btnOK.UseVisualStyleBackColor = true;
         this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
         // 
         // lstArguments
         // 
         this.lstArguments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lstArguments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
         this.lstArguments.FullRowSelect = true;
         this.lstArguments.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
         this.lstArguments.Location = new System.Drawing.Point(12, 12);
         this.lstArguments.Name = "lstArguments";
         this.lstArguments.Size = new System.Drawing.Size(610, 308);
         this.lstArguments.TabIndex = 2;
         this.lstArguments.UseCompatibleStateImageBehavior = false;
         this.lstArguments.View = System.Windows.Forms.View.Details;
         // 
         // columnHeader1
         // 
         this.columnHeader1.Text = "";
         this.columnHeader1.Width = 275;
         // 
         // columnHeader2
         // 
         this.columnHeader2.Text = "";
         this.columnHeader2.Width = 300;
         // 
         // frmCommandLine
         // 
         this.AcceptButton = this.btnOK;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.btnOK;
         this.ClientSize = new System.Drawing.Size(634, 361);
         this.Controls.Add(this.lstArguments);
         this.Controls.Add(this.btnOK);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "frmCommandLine";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "AstroGrep";
         this.Load += new System.EventHandler(this.frmCommandLine_Load);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button btnOK;
      private System.Windows.Forms.ListView lstArguments;
      private System.Windows.Forms.ColumnHeader columnHeader1;
      private System.Windows.Forms.ColumnHeader columnHeader2;
   }
}