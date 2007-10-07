using Gtk;
using System;

namespace AstroGrep.Linux.Forms
{
   /// <summary>
   /// About Dialog
   /// </summary>
   /// <remarks>
   ///   AstroGrep File Searching Utility. Written by Theodore L. Ward
   ///   Copyright (C) 2002 AstroComma Incorporated.
   ///   
   ///   This program is free software; you can redistribute it and/or
   ///   modify it under the terms of the GNU General Public License
   ///   as published by the Free Software Foundation; either version 2
   ///   of the License, or (at your option) any later version.
   ///   
   ///   This program is distributed in the hope that it will be useful,
   ///   but WITHOUT ANY WARRANTY; without even the implied warranty of
   ///   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   ///   GNU General Public License for more details.
   ///   
   ///   You should have received a copy of the GNU General Public License
   ///   along with this program; if not, write to the Free Software
   ///   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
   /// 
   ///   The author may be contacted at:
   ///   ted@astrocomma.com or curtismbeard@gmail.com
   /// </remarks>
   /// <history>
   /// [Curtis_Beard]	   11/03/2006	Created
   /// </history>
   public class frmAbout : Gtk.Dialog
   {
      /// <summary>
      /// Creates an instance of the frmAbout class.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      11/02/2006  Created
      /// </history>
      public frmAbout(Window w, DialogFlags f):base("", w, f)
      {
         InitializeComponent();

         SetVersion();
      }

      #region Graphical Layout Code
      private Image HeaderPanel;
      private Label lblDescription;
      private Label lblTitle;
      private Label lblAuthors;
      //private Label lnkHomePage;
      private AstroGrep.Linux.Controls.LinkLabel lnkHomePage;
      private Label lblVersion;
      private Label lblCopyright;
      private Label lnkLicense;
      private Label lblGraphics;

      /// <summary>
      /// 
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      11/02/2006  Created
      /// </history>
      private void InitializeComponent()
      {
         this.Title = Stock.Lookup(Stock.About).Label.Replace("_", "") + " AstroGrep";
         this.Modal = true;
         this.WindowPosition = Gtk.WindowPosition.CenterOnParent;
         this.Resizable = false;
         this.SetDefaultSize(434, 348);
         this.IconName = Stock.About;

         // Initialize all widgets
         HeaderPanel = new Image();
         lblDescription = new Label();
         lblTitle = new Label();
         lblAuthors = new Label();
         //lnkHomePage = new Label();
         lnkHomePage = new AstroGrep.Linux.Controls.LinkLabel();
         lblVersion = new Label();
         lblCopyright = new Label();
         lnkLicense = new Label();
         lblGraphics = new Label();

         // left align
         lblTitle.SetAlignment(0,0);
         lblVersion.SetAlignment(0,0);
         lblCopyright.SetAlignment(0,0);
         lblDescription.SetAlignment(0,0);
         lnkLicense.SetAlignment(0,0);
         lblGraphics.SetAlignment(0,0);
         lblAuthors.SetAlignment(0,0);
         //lnkHomePage.SetAlignment(0,0);

         // Set widgets' properties
         Gdk.Pixbuf headPix = Images.GetPixbuf("AstroGrep_Banner.png", 434, 100);
         HeaderPanel.Pixbuf = headPix;
         lblTitle.Text = "AstroGrep";
         lblVersion.Text = "Version 0.0.0";
         lblCopyright.Text = "Copyright (C) 2002-2006 AstroComma Inc.";
         lblDescription.Text = "\nAdditional Copyright (C) 2002 to Theodore L. Ward. AstroGrep comes with \nABSOLUTELY NO WARRANTY; for details visit http://www.gnu.org/copyleft/gpl.html. \nThis is free software, and you are welcome to redistribute it under certain conditions;\n http://www.gnu.org/copyleft/gpl.html#SEC3";
         lnkLicense.Text = "\nGNU License";
         lblGraphics.Text = "\nImages from Crystal Clear icon set for KDE.  Developed by Everaldo.";
         lblAuthors.Text = "\nCreated by Theodore Ward and converted to .Net by Curtis Beard";
         lnkHomePage.Text = "\nAstroGrep Home Page";
         lnkHomePage.UriString = "http://astrogrep.sourceforge.net";

         // Add widgets to dialog
         this.VBox.BorderWidth = 0;
         this.VBox.PackStart(HeaderPanel, false, true, 0);
         this.VBox.PackStart(new HSeparator(), false, true, 2);
         this.VBox.PackStart(lblTitle, false, true, 0);
         this.VBox.PackStart(lblVersion, false, true, 0);
         this.VBox.PackStart(lblCopyright, false, true, 0);
         this.VBox.PackStart(lblDescription, false, true, 0);
         this.VBox.PackStart(lnkLicense, false, true, 0);
         this.VBox.PackStart(lblGraphics, false, true, 0);
         this.VBox.PackStart(lblAuthors, false, true, 0);
         this.VBox.PackStart(lnkHomePage, false, true, 0);
         
         // Add actions to dialog
         this.AddButton(Stock.Close, Gtk.ResponseType.Close);

         // make sure and display all widgets
         this.VBox.ShowAll();
      }
      #endregion

      /// <summary>
      /// Sets the version label to the application's version number.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]      11/02/2006  Created
      /// </history>
      private void SetVersion()
      {
         System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
         System.Diagnostics.FileVersionInfo _info = System.Diagnostics.FileVersionInfo.GetVersionInfo(_assembly.Location);

         lblVersion.Text = string.Format("Version {0}.{1}.{2}", _info.FileMajorPart, _info.FileMinorPart, _info.FileBuildPart);
      }
   }
}
