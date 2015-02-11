namespace AstroGrep.Windows.Controls
{
   partial class FilterValueType
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

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.dtpValue = new System.Windows.Forms.DateTimePicker();
         this.txtValue = new System.Windows.Forms.TextBox();
         this.numValue = new System.Windows.Forms.NumericUpDown();
         this.pnlSize = new System.Windows.Forms.Panel();
         this.cboSize = new System.Windows.Forms.ComboBox();
         this.numSize = new System.Windows.Forms.NumericUpDown();
         ((System.ComponentModel.ISupportInitialize)(this.numValue)).BeginInit();
         this.pnlSize.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.numSize)).BeginInit();
         this.SuspendLayout();
         // 
         // dtpValue
         // 
         this.dtpValue.Location = new System.Drawing.Point(19, 26);
         this.dtpValue.Name = "dtpValue";
         this.dtpValue.Size = new System.Drawing.Size(200, 20);
         this.dtpValue.TabIndex = 0;
         // 
         // txtValue
         // 
         this.txtValue.Location = new System.Drawing.Point(19, 52);
         this.txtValue.Name = "txtValue";
         this.txtValue.Size = new System.Drawing.Size(100, 20);
         this.txtValue.TabIndex = 1;
         // 
         // numValue
         // 
         this.numValue.Location = new System.Drawing.Point(19, 88);
         this.numValue.Name = "numValue";
         this.numValue.Size = new System.Drawing.Size(120, 20);
         this.numValue.TabIndex = 2;
         // 
         // pnlSize
         // 
         this.pnlSize.Controls.Add(this.numSize);
         this.pnlSize.Controls.Add(this.cboSize);
         this.pnlSize.Location = new System.Drawing.Point(23, 119);
         this.pnlSize.Name = "pnlSize";
         this.pnlSize.Size = new System.Drawing.Size(196, 24);
         this.pnlSize.TabIndex = 3;
         // 
         // cboSize
         // 
         this.cboSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.cboSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cboSize.FormattingEnabled = true;
         this.cboSize.Items.AddRange(new object[] {
            "byte",
            "KB",
            "MB",
            "GB"});
         this.cboSize.Location = new System.Drawing.Point(123, 0);
         this.cboSize.Name = "cboSize";
         this.cboSize.Size = new System.Drawing.Size(73, 21);
         this.cboSize.TabIndex = 1;
         // 
         // numSize
         // 
         this.numSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.numSize.Location = new System.Drawing.Point(0, 1);
         this.numSize.Name = "numSize";
         this.numSize.Size = new System.Drawing.Size(117, 20);
         this.numSize.TabIndex = 2;
         // 
         // FilterValueType
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.pnlSize);
         this.Controls.Add(this.numValue);
         this.Controls.Add(this.txtValue);
         this.Controls.Add(this.dtpValue);
         this.Name = "FilterValueType";
         this.Size = new System.Drawing.Size(306, 159);
         ((System.ComponentModel.ISupportInitialize)(this.numValue)).EndInit();
         this.pnlSize.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.numSize)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.DateTimePicker dtpValue;
      private System.Windows.Forms.TextBox txtValue;
      private System.Windows.Forms.NumericUpDown numValue;
      private System.Windows.Forms.Panel pnlSize;
      private System.Windows.Forms.ComboBox cboSize;
      private System.Windows.Forms.NumericUpDown numSize;
   }
}
