using Gtk;
using System;

namespace AstroGrep.Linux.Forms
{
	/// <summary>
	/// Used to display user definable settings for this application.
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
   /// [Curtis_Beard]      11/06/2006	Created
   /// </history>
	public class frmOptions : Gtk.Dialog
	{
      private bool __LanguageChange = false;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="w"></param>
      /// <param name="f"></param>
		public frmOptions(Window w, DialogFlags f):base("", w, f)
		{
			InitializeComponent();
		}

      /// <summary>
      /// Checks to see if the language w changed.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		11/06/2006	Created
      /// </history>
      public bool IsLanguageChange
      {
         get { return __LanguageChange; }
      }

      #region Graphical Layout Code
      private Gtk.Notebook optionTabs;
      // General
      private Gtk.ComboBox cboLanguage;
      private Gtk.Entry txtExcludeExtensions;
      private Gtk.ComboBox cboMRU;

      // Text Editors

      // Results
      private Gtk.ColorButton btnMatchForeColor;
      private Gtk.ColorButton btnMatchBackColor;
      private Gtk.ColorButton btnResultsForeColor;
      private Gtk.ColorButton btnResultsBackColor;
      private Gtk.TextView txtResults;
      
      private void InitializeComponent()
      {
         if (Common.IsWindows)
         {
            this.Title = "Options";
            this.AddButton(Stock.Ok, Gtk.ResponseType.Ok);
            this.AddButton(Stock.Cancel, Gtk.ResponseType.Cancel);            
         }
         else
         {
            this.Title = Stock.Lookup(Stock.Preferences).Label.Replace("_", "");
            this.AddButton(Stock.Cancel, Gtk.ResponseType.Cancel);
            this.AddButton(Stock.Ok, Gtk.ResponseType.Ok);
         }

         this.Modal = true;
         this.WindowPosition = Gtk.WindowPosition.CenterOnParent;
         this.Resizable = false;
         this.SetDefaultSize(518, 399);         
         this.Response += new ResponseHandler(frmOptions_Response);
         this.IconName = Stock.Preferences;

         optionTabs = new Notebook();
         optionTabs.ShowTabs = true;
         optionTabs.BorderWidth = 5;
         optionTabs.TabPos = PositionType.Top;
         optionTabs.SetSizeRequest(515, 300);

         GenerateTabs();

         this.VBox.PackStart(optionTabs, true, true, 0);
         this.VBox.ShowAll();

         LoadSettings();
      }

      private void GenerateTabs()
      {
         VBox tab = new VBox();
         Image img = new Image();
         img.Pixbuf = Images.GetPixbuf("options-general.png");
         tab.PackStart(img, false, false, 2);
         tab.PackEnd(new Label("General"), false, false, 0);
         tab.SetSizeRequest(60,50);
         tab.ShowAll();
         optionTabs.AppendPage(GenerateGeneral(), tab);

         tab = new VBox();
         img = new Image();
         img.Pixbuf = Images.GetPixbuf("options-text-editors.png");
         tab.PackStart(img, false, false, 2);
         tab.PackEnd(new Label("Text Editors"), false, false, 0);
         tab.SetSizeRequest(60,50);
         tab.ShowAll();
         optionTabs.AppendPage(GenerateTextEditors(), tab);

         tab = new VBox();
         img = new Image();
         img.Pixbuf = Images.GetPixbuf("options-results.png");
         tab.PackStart(img, false, false, 2);
         tab.PackEnd(new Label("Results"), false, false, 0);
         tab.SetSizeRequest(60,50);
         tab.ShowAll();
         optionTabs.AppendPage(GenerateResults(), tab);

         tab = new VBox();
         img = new Image();
         img.Pixbuf = Images.GetPixbuf("options-plugins.png");
         tab.PackStart(img, false, false, 2);
         tab.PackEnd(new Label("Plugins"), false, false, 0);
         tab.SetSizeRequest(60,50);
         tab.ShowAll();
         optionTabs.AppendPage(GeneratePlugins(), tab);

         tab.Dispose();
         img.Dispose();

         optionTabs.SwitchPage += new SwitchPageHandler(optionTabs_SwitchPage);
      }

      private Gtk.VBox GenerateGeneral()
      {
         VBox box = new VBox();

         // Max mru listings
         HBox mruBox = new HBox();
         mruBox.BorderWidth = 3;         
         Label lblMRU = new Label("Number of most recently used paths to store");
         lblMRU.SetAlignment(0,.5F);
         cboMRU = ComboBox.NewText();
         cboMRU.WidthRequest = 100;
         cboMRU.WrapWidth = 5;
         for (int i = 1; i < 26; i++)
            cboMRU.AppendText(i.ToString());
         
         cboMRU.Active = 9;
         mruBox.PackStart(cboMRU, false, false, 3);
         mruBox.PackStart(lblMRU, true, true, 3);

         // language
         Frame langFrame = new Frame("Language");
         langFrame.BorderWidth = 5;
         HBox langBox = new HBox();
         langBox.BorderWidth = 5;
         cboLanguage = ComboBox.NewText();
         cboLanguage.AppendText("Deutsch");
         cboLanguage.AppendText("English");
         cboLanguage.AppendText("Español");
         cboLanguage.AppendText("Italiano");

         // set language index
         int index = FindComboBoxEntry(cboLanguage, Core.GeneralSettings.Language);
         if (index == -1)
            index = 1;  // English
         cboLanguage.Active = index;

         langBox.PackStart(cboLanguage, true, true, 5);
         langFrame.Add(langBox);

         // Exclusion list
         Frame excludeFrame = new Frame("Exclude File Extensions");
         excludeFrame.BorderWidth = 5;
         HBox frameBox = new HBox();
         frameBox.BorderWidth = 5;
         txtExcludeExtensions = new Entry();
         frameBox.PackStart(txtExcludeExtensions, true, true, 5);
         excludeFrame.Add(frameBox);

         box.PackStart(mruBox, false, true, 3);
         box.PackStart(langFrame, false, true, 3);
         box.PackStart(excludeFrame, false, true, 3);
         return box;
      }

      private Gtk.VBox GenerateTextEditors()
      {
			VBox box = new VBox();
			Label lblNA = new Label("Not implemented in this version.");
			
			box.PackStart(lblNA, false, true, 3);
         return box;
      }

      private Gtk.VBox GenerateResults()
      {
         VBox box = new VBox();
         Gdk.Color white = new Gdk.Color(255,255,255);
         Gdk.Color black = new Gdk.Color(0,0,0);
         Gdk.Color blue = new Gdk.Color(0,0,255);

         // Match Colors
         btnMatchForeColor = new ColorButton(blue);
         btnMatchBackColor = new ColorButton(white);
         btnMatchForeColor.ColorSet += new EventHandler(btnMatchForeColor_ColorSet);
         btnMatchBackColor.ColorSet += new EventHandler(btnMatchBackColor_ColorSet);

         Frame matchFrame = new Frame("Results Match");
         matchFrame.BorderWidth = 5;
         HBox frameBox = new HBox();
         frameBox.BorderWidth = 5;

         HBox foreBox = new HBox(false, 3);
         btnMatchForeColor.WidthRequest = 100;
         foreBox.PackStart(new Label("Fore Color"), false, false, 3);
         foreBox.PackStart(btnMatchForeColor, false, true, 3);

         HBox backBox = new HBox(false, 3);
         btnMatchBackColor.WidthRequest = 100;
         backBox.PackStart(new Label("Back Color"), false, false, 3);
         backBox.PackStart(btnMatchBackColor, false, true, 3);

         frameBox.PackStart(foreBox, true, true, 5);
         frameBox.PackStart(backBox, true, true, 5);
         matchFrame.Add(frameBox);

         // Window Colors
         btnResultsForeColor = new ColorButton(black);
         btnResultsBackColor = new ColorButton(white);
         btnResultsForeColor.ColorSet += new EventHandler(btnResultsForeColor_ColorSet);
         btnResultsBackColor.ColorSet += new EventHandler(btnResultsBackColor_ColorSet);
         
         Frame resultsFrame = new Frame("Results Window");
         resultsFrame.BorderWidth = 5;
         frameBox = new HBox();
         frameBox.BorderWidth = 5;

         foreBox = new HBox(false, 3);
         btnResultsForeColor.WidthRequest = 100;
         foreBox.PackStart(new Label("Fore Color"), false, false, 3);
         foreBox.PackStart(btnResultsForeColor, false, true, 3);

         backBox = new HBox(false, 3);
         btnResultsBackColor.WidthRequest = 100;
         backBox.PackStart(new Label("Back Color"), false, false, 3);
         backBox.PackStart(btnResultsBackColor, false, true, 3);

         frameBox.PackStart(foreBox, true, true, 5);
         frameBox.PackStart(backBox, true, true, 5);
         resultsFrame.Add(frameBox);

         // Results display
         Frame results = new Frame("Results Preview");
         results.BorderWidth = 5;
         Frame ScrolledWindowFrm = new Gtk.Frame();
         ScrolledWindowFrm.Shadow = ShadowType.In;
         ScrolledWindowFrm.BorderWidth = 5;
         txtResults = new TextView();
         txtResults.Buffer.Text = "(21)  Example results line and, match, displayed";
         txtResults.Editable = false;
         ScrolledWindowFrm.HeightRequest = 50;
         ScrolledWindowFrm.Add(txtResults);
         results.Add(ScrolledWindowFrm);

         box.PackStart(matchFrame, false, true, 3);
         box.PackStart(resultsFrame, false, true, 3);
         box.PackEnd(results, false, true, 3);
         return box;
      }

      private Gtk.VBox GeneratePlugins()
      {
         VBox box = new VBox();
         Label lblNA = new Label("Not implemented in this version.");
			
         box.PackStart(lblNA, false, true, 3);
         return box;
      }
      #endregion

      /// <summary>
      /// Handles all processing when the user selects the OK or Cancel buttons.
      /// </summary>
      /// <param name="o">system parameter</param>
      /// <param name="args">system parameter</param>
      private void frmOptions_Response(object o, ResponseArgs args)
      {
         if (args.ResponseId == ResponseType.Ok)
         {
            // General
            Core.GeneralSettings.MaximumMRUPaths = cboMRU.Active + 1;
            Core.GeneralSettings.ExtensionExcludeList = txtExcludeExtensions.Text;

            // Only load new language on a change
            if (!Core.GeneralSettings.Language.Equals(cboLanguage.ActiveText))
            {
               Core.GeneralSettings.Language = cboLanguage.ActiveText;
               __LanguageChange = true;
            }

            // Results
            Core.GeneralSettings.HighlightForeColor = Common.ConvertColorToString(btnMatchForeColor.Color);
            Core.GeneralSettings.HighlightBackColor = Common.ConvertColorToString(btnMatchBackColor.Color);
            Core.GeneralSettings.ResultsForeColor = Common.ConvertColorToString(btnResultsForeColor.Color);
            Core.GeneralSettings.ResultsBackColor = Common.ConvertColorToString(btnResultsBackColor.Color);

            // Save all options
            Core.GeneralSettings.Save();
         }
      }

      private void btnMatchForeColor_ColorSet(object sender, EventArgs e)
      {
         UpdateResultsDisplay();
      }

      private void btnMatchBackColor_ColorSet(object sender, EventArgs e)
      {
         UpdateResultsDisplay();
      }

      private void btnResultsForeColor_ColorSet(object sender, EventArgs e)
      {
         UpdateResultsDisplay();
      }

      private void btnResultsBackColor_ColorSet(object sender, EventArgs e)
      {
         UpdateResultsDisplay();
      }

      private void UpdateResultsDisplay()
      {
         //update the display with new colors
         txtResults.ModifyBase(txtResults.State, btnResultsBackColor.Color);
         txtResults.ModifyText(txtResults.State, btnResultsForeColor.Color);

         // remove the tag if exists
         TextTag temp = txtResults.Buffer.TagTable.Lookup("highlight");
         if (temp != null)
            txtResults.Buffer.TagTable.Remove(temp);

         // create new tag and add
         TextTag tag = new TextTag("highlight");
         tag.BackgroundGdk = btnMatchBackColor.Color;
         tag.ForegroundGdk = btnMatchForeColor.Color;
         txtResults.Buffer.TagTable.Add(tag);

         // highlight match
         int pos = txtResults.Buffer.Text.IndexOf("match");
         TextIter start = txtResults.Buffer.GetIterAtLineOffset(1,pos);
         TextIter end = txtResults.Buffer.GetIterAtLineOffset(1, pos + "match".Length);
         txtResults.Buffer.ApplyTag("highlight", start, end);
      }

      private void optionTabs_SwitchPage(object o, SwitchPageArgs args)
      {
         // redraw results preview when tab selected
         if (args.PageNum == 2)
            UpdateResultsDisplay();
      }

      /// <summary>
      /// Set the dialog's control values from the settings.
      /// </summary>
      private void LoadSettings()
      {
         cboMRU.Active = Core.GeneralSettings.MaximumMRUPaths - 1;
         txtExcludeExtensions.Text = Core.GeneralSettings.ExtensionExcludeList;

         btnMatchForeColor.Color = Common.ConvertStringToColor(Core.GeneralSettings.HighlightForeColor);
         btnMatchBackColor.Color = Common.ConvertStringToColor(Core.GeneralSettings.HighlightBackColor);
         btnResultsForeColor.Color = Common.ConvertStringToColor(Core.GeneralSettings.ResultsForeColor);
         btnResultsBackColor.Color = Common.ConvertStringToColor(Core.GeneralSettings.ResultsBackColor);
      }

      /// <summary>
      /// Find the first entry of the item in the Gtk.ComboBox.
      /// </summary>
      /// <param name="combo">Gtk.ComboBox</param>
      /// <param name="item">string value to find</param>
      /// <returns>Position in Gtk.ComboBoxEntry, -1 if not found</returns>
      private int FindComboBoxEntry(Gtk.ComboBox combo, string item)
      {
         TreeIter iter;
         int index = -1;
         
         if (combo.Model.GetIterFirst(out iter))
         {
            do
            {
               string val = (string) combo.Model.GetValue(iter, 0);
               if (val.Equals(item))
               {
                  index = combo.Model.GetPath(iter).Indices[0];
                  break;
               }

            } while (combo.Model.IterNext(ref iter));
         }

         return index;
      }
   }
}
