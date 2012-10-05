using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace AstroGrep.Windows.Forms
{
	public partial class frmAddEditTextEditor
	{
		#region Windows Form Designer generated code
		private System.Windows.Forms.Button btnOK;
private System.Windows.Forms.Button btnCancel;
private AstroGrep.Windows.Controls.PictureButton btnBrowse;
private System.Windows.Forms.TextBox txtFileType;
private System.Windows.Forms.TextBox txtTextEditorLocation;
private System.Windows.Forms.TextBox txtCmdLineArgs;
private System.Windows.Forms.Label lblFileType;
private System.Windows.Forms.Label lblAllTypesMessage;
private System.Windows.Forms.Label lblCmdLineArgs;
private System.Windows.Forms.Label lblCmdOptionsView;
private System.Windows.Forms.Label lblTextEditorLocation;
private System.Windows.Forms.Label lblCmdOptions;
private System.Windows.Forms.ToolTip HoverTips;

		private void InitializeComponent()
		{
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddEditTextEditor));
         this.btnOK = new System.Windows.Forms.Button();
         this.btnCancel = new System.Windows.Forms.Button();
         this.txtFileType = new System.Windows.Forms.TextBox();
         this.txtTextEditorLocation = new System.Windows.Forms.TextBox();
         this.txtCmdLineArgs = new System.Windows.Forms.TextBox();
         this.lblFileType = new System.Windows.Forms.Label();
         this.lblAllTypesMessage = new System.Windows.Forms.Label();
         this.lblCmdLineArgs = new System.Windows.Forms.Label();
         this.lblCmdOptionsView = new System.Windows.Forms.Label();
         this.lblTextEditorLocation = new System.Windows.Forms.Label();
         this.lblCmdOptions = new System.Windows.Forms.Label();
         this.HoverTips = new System.Windows.Forms.ToolTip(this.components);
         this.lblMultiTypes = new System.Windows.Forms.Label();
         this.btnBrowse = new AstroGrep.Windows.Controls.PictureButton();
         ((System.ComponentModel.ISupportInitialize)(this.btnBrowse)).BeginInit();
         this.SuspendLayout();
         // 
         // btnOK
         // 
         this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnOK.Location = new System.Drawing.Point(270, 294);
         this.btnOK.Name = "btnOK";
         this.btnOK.Size = new System.Drawing.Size(75, 22);
         this.btnOK.TabIndex = 5;
         this.btnOK.Text = "&OK";
         this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
         // 
         // btnCancel
         // 
         this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnCancel.Location = new System.Drawing.Point(350, 294);
         this.btnCancel.Name = "btnCancel";
         this.btnCancel.Size = new System.Drawing.Size(75, 22);
         this.btnCancel.TabIndex = 6;
         this.btnCancel.Text = "&Cancel";
         this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
         // 
         // txtFileType
         // 
         this.txtFileType.Location = new System.Drawing.Point(11, 24);
         this.txtFileType.Name = "txtFileType";
         this.txtFileType.Size = new System.Drawing.Size(195, 20);
         this.txtFileType.TabIndex = 1;
         this.txtFileType.TextChanged += new System.EventHandler(this.txtFileType_TextChanged);
         // 
         // txtTextEditorLocation
         // 
         this.txtTextEditorLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.txtTextEditorLocation.Location = new System.Drawing.Point(11, 110);
         this.txtTextEditorLocation.Name = "txtTextEditorLocation";
         this.txtTextEditorLocation.Size = new System.Drawing.Size(387, 20);
         this.txtTextEditorLocation.TabIndex = 2;
         this.txtTextEditorLocation.TextChanged += new System.EventHandler(this.txtTextEditorLocation_TextChanged);
         // 
         // txtCmdLineArgs
         // 
         this.txtCmdLineArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.txtCmdLineArgs.Location = new System.Drawing.Point(11, 244);
         this.txtCmdLineArgs.Name = "txtCmdLineArgs";
         this.txtCmdLineArgs.Size = new System.Drawing.Size(387, 20);
         this.txtCmdLineArgs.TabIndex = 4;
         this.txtCmdLineArgs.TextChanged += new System.EventHandler(this.txtCmdLineArgs_TextChanged);
         // 
         // lblFileType
         // 
         this.lblFileType.AutoSize = true;
         this.lblFileType.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblFileType.Location = new System.Drawing.Point(12, 8);
         this.lblFileType.Name = "lblFileType";
         this.lblFileType.Size = new System.Drawing.Size(50, 13);
         this.lblFileType.TabIndex = 1;
         this.lblFileType.Text = "File Type";
         this.lblFileType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // lblAllTypesMessage
         // 
         this.lblAllTypesMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lblAllTypesMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblAllTypesMessage.Location = new System.Drawing.Point(216, 8);
         this.lblAllTypesMessage.Name = "lblAllTypesMessage";
         this.lblAllTypesMessage.Size = new System.Drawing.Size(204, 48);
         this.lblAllTypesMessage.TabIndex = 22;
         this.lblAllTypesMessage.Text = "A Text Editor can be used for all unknown types by using a * for the file type.";
         // 
         // lblCmdLineArgs
         // 
         this.lblCmdLineArgs.AutoSize = true;
         this.lblCmdLineArgs.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblCmdLineArgs.Location = new System.Drawing.Point(12, 228);
         this.lblCmdLineArgs.Name = "lblCmdLineArgs";
         this.lblCmdLineArgs.Size = new System.Drawing.Size(77, 13);
         this.lblCmdLineArgs.TabIndex = 4;
         this.lblCmdLineArgs.Text = "Command Line";
         this.lblCmdLineArgs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // lblCmdOptionsView
         // 
         this.lblCmdOptionsView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lblCmdOptionsView.AutoSize = true;
         this.lblCmdOptionsView.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblCmdOptionsView.Location = new System.Drawing.Point(8, 299);
         this.lblCmdOptionsView.Name = "lblCmdOptionsView";
         this.lblCmdOptionsView.Size = new System.Drawing.Size(48, 13);
         this.lblCmdOptionsView.TabIndex = 20;
         this.lblCmdOptionsView.Text = "Preview:";
         this.lblCmdOptionsView.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // lblTextEditorLocation
         // 
         this.lblTextEditorLocation.AutoSize = true;
         this.lblTextEditorLocation.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblTextEditorLocation.Location = new System.Drawing.Point(12, 94);
         this.lblTextEditorLocation.Name = "lblTextEditorLocation";
         this.lblTextEditorLocation.Size = new System.Drawing.Size(102, 13);
         this.lblTextEditorLocation.TabIndex = 3;
         this.lblTextEditorLocation.Text = "Text Editor Location";
         this.lblTextEditorLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // lblCmdOptions
         // 
         this.lblCmdOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lblCmdOptions.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblCmdOptions.Location = new System.Drawing.Point(12, 164);
         this.lblCmdOptions.Name = "lblCmdOptions";
         this.lblCmdOptions.Size = new System.Drawing.Size(390, 64);
         this.lblCmdOptions.TabIndex = 21;
         this.lblCmdOptions.Text = "Command Line Options:";
         // 
         // lblMultiTypes
         // 
         this.lblMultiTypes.AutoSize = true;
         this.lblMultiTypes.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblMultiTypes.Location = new System.Drawing.Point(12, 47);
         this.lblMultiTypes.Name = "lblMultiTypes";
         this.lblMultiTypes.Size = new System.Drawing.Size(156, 13);
         this.lblMultiTypes.TabIndex = 23;
         this.lblMultiTypes.Text = "Separate types with | character.";
         // 
         // btnBrowse
         // 
         this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
         this.btnBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowse.Image")));
         this.btnBrowse.Location = new System.Drawing.Point(408, 112);
         this.btnBrowse.Name = "btnBrowse";
         this.btnBrowse.Size = new System.Drawing.Size(16, 16);
         this.btnBrowse.TabIndex = 3;
         this.btnBrowse.TabStop = false;
         this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
         // 
         // frmAddEditTextEditor
         // 
         this.AcceptButton = this.btnOK;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.btnCancel;
         this.ClientSize = new System.Drawing.Size(430, 323);
         this.Controls.Add(this.lblMultiTypes);
         this.Controls.Add(this.lblCmdOptions);
         this.Controls.Add(this.lblTextEditorLocation);
         this.Controls.Add(this.lblCmdOptionsView);
         this.Controls.Add(this.lblCmdLineArgs);
         this.Controls.Add(this.lblAllTypesMessage);
         this.Controls.Add(this.lblFileType);
         this.Controls.Add(this.txtCmdLineArgs);
         this.Controls.Add(this.txtTextEditorLocation);
         this.Controls.Add(this.txtFileType);
         this.Controls.Add(this.btnBrowse);
         this.Controls.Add(this.btnCancel);
         this.Controls.Add(this.btnOK);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "frmAddEditTextEditor";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Text Editors";
         this.Load += new System.EventHandler(this.frmAddEditTextEditor_Load);
         ((System.ComponentModel.ISupportInitialize)(this.btnBrowse)).EndInit();
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

      private Label lblMultiTypes;
	}
}
