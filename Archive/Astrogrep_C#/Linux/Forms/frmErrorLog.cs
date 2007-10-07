using System;
using System.Collections;

using Gtk;

namespace AstroGrep.Linux.Forms
{
	public class frmErrorLog : Gtk.Dialog
	{
      private ArrayList alErrors;
		public frmErrorLog(Window w, DialogFlags f, ArrayList errors) : base("", w, f)
		{
         alErrors = errors;
         InitializeComponent();
      }

      #region Graphical Layout Code
      private Gtk.TreeView tvErrors;
      private Gtk.ListStore lsErrors;
      private Gtk.TextView txtErrorDetails;
      
      private void InitializeComponent()
      {
         this.Title = "Search Errors";
         this.Modal = false;
         this.WindowPosition = Gtk.WindowPosition.CenterOnParent;
         this.Resizable = true;
         this.SetDefaultSize(480, 320);         
         this.IconName = Stock.DialogError;
         this.AddButton(Stock.Close, Gtk.ResponseType.Close);

         Gtk.Frame treeFrame = new Gtk.Frame();
         treeFrame.Shadow = ShadowType.In;
         Gtk.ScrolledWindow treeWin = new Gtk.ScrolledWindow();
         tvErrors = new Gtk.TreeView ();
         tvErrors.SetSizeRequest(480,200);
         tvErrors.Selection.Mode = SelectionMode.Single;

         tvErrors.AppendColumn(CreateTreeViewColumn("Name", 60, 0));
         tvErrors.AppendColumn(CreateTreeViewColumn("Located In", 200, 1));

         lsErrors = new Gtk.ListStore(typeof (string), typeof (string), typeof(string));
         tvErrors.Model = lsErrors;
         tvErrors.Selection.Changed += new EventHandler(tvErrors_Selection_Changed);
                  
         treeWin.Add(tvErrors);
         treeFrame.BorderWidth = 0;
         treeFrame.Add(treeWin);

         for (int i=0; i< alErrors.Count; i++)
         {
            MessageEventArgs args = (MessageEventArgs)alErrors[i];
            if (args.ErrorFile == null)
               lsErrors.AppendValues("General Error", string.Empty, args.Message);
            else
               lsErrors.AppendValues(args.ErrorFile.Name, args.ErrorFile.DirectoryName, args.Message);
         }

         Gtk.Frame txtFrame = new Gtk.Frame();
         txtFrame.Shadow = ShadowType.In;
         Gtk.ScrolledWindow txtScrollWin = new Gtk.ScrolledWindow();
         txtErrorDetails = new TextView();
         txtErrorDetails.WrapMode = Gtk.WrapMode.WordChar;
         txtErrorDetails.Editable = false;
         txtScrollWin.Add(txtErrorDetails);
         txtFrame.BorderWidth = 0;
         txtFrame.Add(txtScrollWin);

         this.VBox.PackStart(treeFrame, true, true, 3);
         this.VBox.PackEnd(txtFrame, true, true, 3);
         this.VBox.ShowAll();
      }
      #endregion

      private void tvErrors_Selection_Changed(object sender, EventArgs e)
      {
         // For single mode only
         TreeModel model;
         TreeIter iter;

         if (((TreeSelection)sender).GetSelected (out model, out iter))
         {
            string error = (string) model.GetValue (iter, 2);
            txtErrorDetails.Buffer.Text = error;
         }
      }

      private TreeViewColumn CreateTreeViewColumn(string title, int minSize, int index)
      {
         Gtk.TreeViewColumn col = new TreeViewColumn("", new Gtk.CellRendererText(), "text", index);
         col.MinWidth = minSize;
         col.Resizable = true;
         col.Clickable = true;
         col.Sizing = TreeViewColumnSizing.GrowOnly;
         col.Title = title;

         return col;
      }
   }
}
