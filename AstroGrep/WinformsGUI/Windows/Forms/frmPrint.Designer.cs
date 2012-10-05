using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace AstroGrep.Windows.Forms
{
	public partial class frmPrint
	{
		#region Windows Form Designer generated code
		private ListView __listView;
private System.Windows.Forms.Button cmdPrint;
private System.Windows.Forms.Button cmdPreview;
private System.Windows.Forms.Button cmdPageSetup;
private System.Windows.Forms.Button cmdCancel;
private System.Windows.Forms.Label lblSelect;
private System.Windows.Forms.ListBox lstPrintTypes;
private System.ComponentModel.Container components = null;

		private void InitializeComponent()
      {
         this.cmdPrint = new System.Windows.Forms.Button();
         this.cmdPreview = new System.Windows.Forms.Button();
         this.cmdPageSetup = new System.Windows.Forms.Button();
         this.cmdCancel = new System.Windows.Forms.Button();
         this.lblSelect = new System.Windows.Forms.Label();
         this.lstPrintTypes = new System.Windows.Forms.ListBox();
         this.SuspendLayout();
         // 
         // cmdPrint
         // 
         this.cmdPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.cmdPrint.Location = new System.Drawing.Point(8, 160);
         this.cmdPrint.Name = "cmdPrint";
         this.cmdPrint.Size = new System.Drawing.Size(96, 23);
         this.cmdPrint.TabIndex = 1;
         this.cmdPrint.Text = "&Print";
         this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
         // 
         // cmdPreview
         // 
         this.cmdPreview.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.cmdPreview.Location = new System.Drawing.Point(120, 160);
         this.cmdPreview.Name = "cmdPreview";
         this.cmdPreview.Size = new System.Drawing.Size(96, 23);
         this.cmdPreview.TabIndex = 2;
         this.cmdPreview.Text = "Pre&view";
         this.cmdPreview.Click += new System.EventHandler(this.cmdPreview_Click);
         // 
         // cmdPageSetup
         // 
         this.cmdPageSetup.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.cmdPageSetup.Location = new System.Drawing.Point(232, 160);
         this.cmdPageSetup.Name = "cmdPageSetup";
         this.cmdPageSetup.Size = new System.Drawing.Size(96, 23);
         this.cmdPageSetup.TabIndex = 3;
         this.cmdPageSetup.Text = "Page &Setup";
         this.cmdPageSetup.Click += new System.EventHandler(this.cmdPageSetup_Click);
         // 
         // cmdCancel
         // 
         this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.cmdCancel.Location = new System.Drawing.Point(344, 160);
         this.cmdCancel.Name = "cmdCancel";
         this.cmdCancel.Size = new System.Drawing.Size(96, 23);
         this.cmdCancel.TabIndex = 4;
         this.cmdCancel.Text = "&Cancel";
         this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
         // 
         // lblSelect
         // 
         this.lblSelect.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblSelect.Location = new System.Drawing.Point(8, 8);
         this.lblSelect.Name = "lblSelect";
         this.lblSelect.Size = new System.Drawing.Size(432, 16);
         this.lblSelect.TabIndex = 4;
         this.lblSelect.Text = "Please select the output type:";
         // 
         // lstPrintTypes
         // 
         this.lstPrintTypes.Location = new System.Drawing.Point(8, 32);
         this.lstPrintTypes.Name = "lstPrintTypes";
         this.lstPrintTypes.Size = new System.Drawing.Size(432, 121);
         this.lstPrintTypes.TabIndex = 0;
         // 
         // frmPrint
         // 
         this.AcceptButton = this.cmdPrint;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.cmdCancel;
         this.ClientSize = new System.Drawing.Size(450, 191);
         this.Controls.Add(this.lstPrintTypes);
         this.Controls.Add(this.lblSelect);
         this.Controls.Add(this.cmdCancel);
         this.Controls.Add(this.cmdPageSetup);
         this.Controls.Add(this.cmdPreview);
         this.Controls.Add(this.cmdPrint);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "frmPrint";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Print";
         this.Load += new System.EventHandler(this.frmPrint_Load);
         this.ResumeLayout(false);

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
	}
}
