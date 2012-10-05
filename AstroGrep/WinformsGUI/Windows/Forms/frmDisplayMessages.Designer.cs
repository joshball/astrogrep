namespace AstroGrep.Windows.Forms
{
   /// <summary>
   /// 
   /// </summary>
   partial class frmDisplayMessages
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
         this.btnOK = new System.Windows.Forms.Button();
         this.txtMessages = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // btnOK
         // 
         this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnOK.Location = new System.Drawing.Point(607, 433);
         this.btnOK.Name = "btnOK";
         this.btnOK.Size = new System.Drawing.Size(75, 23);
         this.btnOK.TabIndex = 0;
         this.btnOK.Text = "&OK";
         this.btnOK.UseVisualStyleBackColor = true;
         this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
         // 
         // txtMessages
         // 
         this.txtMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.txtMessages.BackColor = System.Drawing.SystemColors.Window;
         this.txtMessages.Location = new System.Drawing.Point(12, 12);
         this.txtMessages.Multiline = true;
         this.txtMessages.Name = "txtMessages";
         this.txtMessages.ReadOnly = true;
         this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this.txtMessages.Size = new System.Drawing.Size(670, 415);
         this.txtMessages.TabIndex = 1;
         this.txtMessages.WordWrap = false;
         // 
         // frmDisplayMessages
         // 
         this.AcceptButton = this.btnOK;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.btnOK;
         this.ClientSize = new System.Drawing.Size(694, 468);
         this.Controls.Add(this.txtMessages);
         this.Controls.Add(this.btnOK);
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "frmDisplayMessages";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Display Messages";
         this.Load += new System.EventHandler(this.frmDisplayMessages_Load);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button btnOK;
      private System.Windows.Forms.TextBox txtMessages;
   }
}