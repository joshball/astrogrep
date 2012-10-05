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
         this.txtDisplay = new System.Windows.Forms.TextBox();
         this.btnOK = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // txtDisplay
         // 
         this.txtDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.txtDisplay.BackColor = System.Drawing.SystemColors.Window;
         this.txtDisplay.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.txtDisplay.Location = new System.Drawing.Point(12, 12);
         this.txtDisplay.Multiline = true;
         this.txtDisplay.Name = "txtDisplay";
         this.txtDisplay.ReadOnly = true;
         this.txtDisplay.Size = new System.Drawing.Size(464, 264);
         this.txtDisplay.TabIndex = 0;
         this.txtDisplay.TabStop = false;
         this.txtDisplay.WordWrap = false;
         // 
         // btnOK
         // 
         this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnOK.Location = new System.Drawing.Point(401, 282);
         this.btnOK.Name = "btnOK";
         this.btnOK.Size = new System.Drawing.Size(75, 23);
         this.btnOK.TabIndex = 1;
         this.btnOK.Text = "&OK";
         this.btnOK.UseVisualStyleBackColor = true;
         this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
         // 
         // frmCommandLine
         // 
         this.AcceptButton = this.btnOK;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.btnOK;
         this.ClientSize = new System.Drawing.Size(488, 317);
         this.Controls.Add(this.btnOK);
         this.Controls.Add(this.txtDisplay);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "frmCommandLine";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "AstroGrep";
         this.Load += new System.EventHandler(this.frmCommandLine_Load);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox txtDisplay;
      private System.Windows.Forms.Button btnOK;
   }
}