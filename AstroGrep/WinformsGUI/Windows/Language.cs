using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

using AstroGrep.Common;
using AstroGrep.Common.Logging;

namespace AstroGrep.Windows
{
   /// <summary>
   /// Used to retrieve language specific text for controls and generic messages.
   /// </summary>
   /// <remarks>
   /// AstroGrep File Searching Utility. Written by Theodore L. Ward
   /// Copyright (C) 2002 AstroComma Incorporated.
   /// 
   /// This program is free software; you can redistribute it and/or
   /// modify it under the terms of the GNU General Public License
   /// as published by the Free Software Foundation; either version 2
   /// of the License, or (at your option) any later version.
   /// 
   /// This program is distributed in the hope that it will be useful,
   /// but WITHOUT ANY WARRANTY; without even the implied warranty of
   /// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   /// GNU General Public License for more details.
   /// 
   /// You should have received a copy of the GNU General Public License
   /// along with this program; if not, write to the Free Software
   /// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
   /// 
   /// The author may be contacted at:
   /// ted@astrocomma.com or curtismbeard@gmail.com
   /// </remarks>
   /// <history>
   /// [Curtis_Beard]      07/31/2006	Created
   /// </history>
   public class Language
   {
      #region Declarations
      private static XmlDocument __XmlDoc = null;
      private static XmlNode __RootNode = null;
      private static XmlNode __XmlGenericNode = null;
      private static List<LanguageItem> internalLanguages = null;
      #endregion

      /// <summary>
      /// Initializes an instance of the Language class.
      /// </summary>
      private Language()
      { }

      private static void LoadInternalLanguages()
      {
         if (internalLanguages == null)
         {
            internalLanguages = new List<LanguageItem>();

            // load our internally defined languages
            internalLanguages.Add(new LanguageItem("English", "en-us"));
            internalLanguages.Add(new LanguageItem("Español", "es-mx"));
            internalLanguages.Add(new LanguageItem("Deutsch", "de-de"));
            internalLanguages.Add(new LanguageItem("Italiano", "it-it"));
            internalLanguages.Add(new LanguageItem("Danish", "da-dk"));
            internalLanguages.Add(new LanguageItem("Polski", "pl-pl"));
         }
      }

      #region Public Methods
      
      /// <summary>
      /// Loads the given language's file.
      /// </summary>
      /// <param name="language">String containing language</param>
      /// <history>
      /// [Curtis_Beard]		07/31/2006	Created
      /// [Curtis_Beard]		10/11/2006	CHG: Close stream
      /// [Curtis_Beard]		06/15/205	CHG: 57, support external language files
      /// </history>
      public static void Load(string culture)
      {
         LoadInternalLanguages();

         if (!LoadInternal(culture))
         {
            if (!LoadExternal(culture))
            {
               // failed to load internal and external, so try default
               if (LoadInternal(Constants.DEFAULT_LANGUAGE))
               {
                  // success in loading default, make sure to update settings value
                  Core.GeneralSettings.Language = Constants.DEFAULT_LANGUAGE;
               }               
            }
         }
      }

      /// <summary>
      /// Loads the given ComboBox with the available languages.
      /// </summary>
      /// <param name="combo">ComboBox to load</param>
      /// <history>
      /// [Curtis_Beard]		05/18/2007	Created
      /// [Curtis_Beard]		06/15/2015	CHG: add polish language entry
      /// [Curtis_Beard]		06/15/2015	CHG: 57, support external language files
      /// </history>
      public static void LoadComboBox(ComboBox combo)
      {
         LoadInternalLanguages();

         combo.Items.Clear();
         combo.DisplayMember = "DisplayName";
         combo.ValueMember = "Culture";

         // load any external languages
         var items = new List<LanguageItem>();
         items.AddRange(internalLanguages);
         items.AddRange(GetExternalLanguages());         

         // add languages to ComboBox
         combo.Items.AddRange(items.ToArray());
      }

      /// <summary>
      /// Gets the location of all the language files.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		05/22/2007	Created
      /// </history>
      public static string LanguageLocation
      {
         get 
         {
            return Path.Combine(ApplicationPaths.DataFolder, "Language");
         }
      }

      /// <summary>
      /// Sets the given control's text property.
      /// </summary>
      /// <param name="control">Control to set</param>
      /// <history>
      /// [Curtis_Beard]		07/31/2006	Created
      /// </history>
      public static void SetControlText(Control control)
      {
         SetControlText(control, null);
      }

      /// <summary>
      /// Sets the given control's text property.
      /// </summary>
      /// <param name="control">Control to set</param>
      /// <param name="tip">ToolTip control</param>
      /// <history>
      /// [Curtis_Beard]		07/31/2006	Created
      /// </history>
      public static void SetControlText(Control control, ToolTip tip)
      {
         if (__RootNode != null)
         {
            string formName = control.FindForm().Name;
            XmlNode node = __RootNode.SelectSingleNode("screen[@name='" + formName + "']");
            XmlNode controlNode;

            if (node != null)
            {
               //node found, find control
               controlNode = node.SelectSingleNode("control[@name='" + control.Name + "']");

               if (controlNode != null)
               {
                  //found control node

                  //text
                  if (controlNode.Attributes["value"] != null)
                     control.Text = controlNode.Attributes["value"].Value;

                  //tooltip
                  if (tip != null && controlNode.Attributes["tooltip"] != null)
                     tip.SetToolTip(control, controlNode.Attributes["tooltip"].Value);
               }
            }
         }
      }

      /// <summary>
      /// Sets the given control's text and tooltip.
      /// </summary>
      /// <param name="control">ToolStripItem to set text/tooltip for.</param>
      /// <param name="tip">ToolTip object</param>
      /// <history>
      /// [Curtis_Beard]		09/27/2012	Initial: 1741735, support ToolStripItem
      /// </history>
      public static void SetToolStripItemText(ToolStripItem control, ToolTip tip)
      {
         if (__RootNode != null)
         {
            string formName = control.Owner.FindForm().Name;
            XmlNode node = __RootNode.SelectSingleNode("screen[@name='" + formName + "']");
            XmlNode controlNode;

            if (node != null)
            {
               //node found, find control
               controlNode = node.SelectSingleNode("control[@name='" + control.Name + "']");

               if (controlNode != null)
               {
                  //found control node

                  //text
                  if (controlNode.Attributes["value"] != null)
                     control.Text = controlNode.Attributes["value"].Value;

                  //tooltip
                  if (tip != null && controlNode.Attributes["tooltip"] != null)
                  {
                     control.ToolTipText = controlNode.Attributes["tooltip"].Value;
                  }
               }
            }
         }
      }

      /// <summary>
      /// Retrieve the control's text value in the loaded language file.
      /// </summary>
      /// <param name="control">Control to set</param>
      /// <returns>value of control's text in language file</returns>
      /// <history>
      /// [Curtis_Beard]		11/02/2006	Created
      /// </history>
      public static string GetControlText(Control control)
      {
         if (__RootNode != null)
         {
            string formName = control.FindForm().Name;
            XmlNode node = __RootNode.SelectSingleNode("screen[@name='" + formName + "']");
            XmlNode controlNode;

            if (node != null)
            {
               //node found, find control
               controlNode = node.SelectSingleNode("control[@name='" + control.Name + "']");

               if (controlNode != null)
               {
                  //text
                  if (controlNode.Attributes["value"] != null)
                     return controlNode.Attributes["value"].Value;
               }
            }
         }

         return string.Empty;
      }

      /// <summary>
      /// Sets the given context menuitem's text property.
      /// </summary>
      /// <param name="holder">Control containing ContextMenu</param>
      /// <param name="item">MenuItem to set</param>
      /// <history>
      /// [Curtis_Beard]		02/01/2012	Created
      /// </history>
      public static void SetContextMenuItemText(Control holder, MenuItem item)
      {
         SetContextMenuItemText(holder, item, null);
      }

      /// <summary>
      /// Sets the given context menuitem's text property or sub menuitem if specified.
      /// </summary>
      /// <param name="holder">Control containing ContextMenu</param>
      /// <param name="item">MenuItem</param>
      /// <param name="subItem">MenuItem's child MenuItem to set</param>
      /// <history>
      /// [Curtis_Beard]		09/18/2013	ADD: 65, support for contextmenu sub items
      /// </history>
      public static void SetContextMenuItemText(Control holder, MenuItem item, MenuItem subItem)
      {
         if (__RootNode != null)
         {
            string formName = GetParentControl(holder).Name;
            XmlNode node = null;

            if (subItem == null)
            {
               node = __RootNode.SelectSingleNode("screen[@name='" + formName + "']/control[@name='" + holder.Name + "']/menuitem[@index='" + item.Index + "']");
            }
            else
            {
               node = __RootNode.SelectSingleNode("screen[@name='" + formName + "']/control[@name='" + holder.Name + "']/menuitem[@index='" + item.Index + "']/menuitem[@index='" + subItem.Index + "']");
            }

            if (node != null)
            {
               if (node.Attributes["value"] != null)
               {
                  if (subItem == null)
                  {
                     item.Text = node.Attributes["value"].Value;
                  }
                  else
                  {
                     subItem.Text = node.Attributes["value"].Value;
                  }
               }
            }
         }
      }

      /// <summary>
      /// Gets a string value from the generic text section of a language file.
      /// </summary>
      /// <param name="name">Key name to retrieve</param>
      /// <returns>string containing text or string.empty if not found</returns>
      /// <history>
      /// [Curtis_Beard]		07/31/2006	Created
      /// </history>
      public static string GetGenericText(string name)
      {
         return GetGenericText(name, string.Empty);
      }

      /// <summary>
      /// Gets a string value from the generic text section of a language file.
      /// </summary>
      /// <param name="name">Key name to retrieve</param>
      /// <param name="defaultValue">Default value to return if not found</param>
      /// <returns>string containing text or given default value if not found</returns>
      /// <history>
      /// [Curtis_Beard]		07/31/2006	Created
      /// </history>
      public static string GetGenericText(string name, string defaultValue)
      {
         if (__XmlGenericNode != null)
         {
            XmlNode node = __XmlGenericNode.SelectSingleNode("text[@name='" + name + "']");

            if (node != null && node.Attributes["value"] != null)
               return node.Attributes["value"].Value;
         }

         return defaultValue;
      }

      /// <summary>
      /// Processes the given form for all controls.
      /// </summary>
      /// <param name="frm">Form to process</param>
      /// <history>
      /// [Curtis_Beard]		07/31/2006	Created
      /// </history>
      public static void ProcessForm(Form frm)
      {
         ProcessForm(frm, null);
      }

      /// <summary>
      /// Processes the given form for all controls.
      /// </summary>
      /// <param name="frm">Form to process</param>
      /// <param name="tip">ToolTip control</param>
      /// <history>
      /// [Curtis_Beard]		07/31/2006	Created
      /// </history>
      public static void ProcessForm(Form frm, ToolTip tip)
      {
         if (__RootNode != null)
         {
            SetFormText(frm);

            //process controls on form
            foreach (Control control in frm.Controls)
            {
               if (control.GetType() == typeof(ToolStrip) || control.GetType() == typeof(StatusStrip))
                  ProcessToolStrip((ToolStrip)control, tip);
               else
                  ProcessControl(control, tip);
            }

            //process menu items on form
            if (frm.Menu != null)
            {
               foreach (MenuItem item in frm.Menu.MenuItems)
               {
                  XmlNode menuNode = __RootNode.SelectSingleNode("screen[@name='" + frm.Name + "']/menu[@index='" + item.Index + "']");

                  if (menuNode != null && menuNode.Attributes["value"] != null)
                  {
                     item.Text = menuNode.Attributes["value"].Value;

                     ProcessMainMenuItem(item);
                  }
               }
            }

            //process menu strip items on form
            if (frm.MainMenuStrip != null)
            {
               for (int i = 0; i < frm.MainMenuStrip.Items.Count; i++)
               {
                  ToolStripMenuItem item = frm.MainMenuStrip.Items[i] as ToolStripMenuItem;
                  XmlNode menuNode = __RootNode.SelectSingleNode("screen[@name='" + frm.Name + "']/menu[@index='" + i + "']");

                  if (menuNode != null && menuNode.Attributes["value"] != null)
                  {
                     item.Text = menuNode.Attributes["value"].Value;
                     List<int> indexes = new List<int>();
                     indexes.Add(i);
                     ProcessMainToolStripMenuItem(item, indexes);
                  }
               }
            }
         }
      }

      #region MenuItem Methods

      private static void ProcessMainMenuItem(MenuItem mainMenuItem)
      {
         if (mainMenuItem.MenuItems != null && mainMenuItem.MenuItems.Count > 0)
         {
            foreach (MenuItem item in mainMenuItem.MenuItems)
            {
               ProcessMenuItems(item, mainMenuItem.Index);
            }
         }
      }

      private static void ProcessMenuItems(MenuItem menuItem, int mainMenuIndex)
      {
         if (menuItem.MenuItems.Count == 0)
         {
            SetMenuItemText(menuItem, mainMenuIndex);
         }
         else
         {
            // set text, then process children
            SetMenuItemText(menuItem, mainMenuIndex);

            foreach (MenuItem subItem in menuItem.MenuItems)
            {
               ProcessMenuItems(subItem, mainMenuIndex);
            }
         }
      }

      private static void SetMenuItemText(MenuItem item, int mainMenuIndex)
      {
         if (__RootNode != null)
         {
            string formName = item.GetMainMenu().GetForm().Name;
            MenuItem mainMenuItem = item.GetMainMenu().MenuItems[mainMenuIndex];
            Menu parentItem = item.Parent;

            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            // start at current level
            builder.Insert(0, string.Format("/menuitem[@index='{0}']", item.Index));

            while (parentItem != null && parentItem is MenuItem && mainMenuItem != (parentItem as MenuItem))
            {
               MenuItem currentItem = parentItem as MenuItem;
               builder.Insert(0, string.Format("/menuitem[@index='{0}']", currentItem.Index));

               parentItem = currentItem.Parent;
            }

            builder.Insert(0, string.Format("screen[@name='{0}']/menu[@index='{1}']", formName, mainMenuIndex));

            XmlNode node = __RootNode.SelectSingleNode(builder.ToString());
            if (node != null)
            {
               if (node.Attributes["value"] != null)
                  item.Text = node.Attributes["value"].Value;
            }
         }
      }

      #endregion

      #region ToolStrip Methods

      private static void ProcessMainToolStripMenuItem(ToolStripMenuItem mainMenuItem, List<int> indexes)
      {
         if (mainMenuItem.DropDownItems != null && mainMenuItem.DropDownItems.Count > 0)
         {
            for (int i = 0; i < mainMenuItem.DropDownItems.Count; i++)
            {
               indexes.Add(i);
               ProcessToolStripMenuItems(mainMenuItem.DropDownItems[i], indexes);
               indexes.RemoveAt(indexes.Count - 1);
            }
         }
      }

      private static void ProcessToolStripMenuItems(ToolStripItem menuItem, List<int> indexes)
      {
         if ((menuItem is ToolStripMenuItem && ((menuItem as ToolStripMenuItem).DropDownItems == null || (menuItem as ToolStripMenuItem).DropDownItems.Count == 0)) || menuItem is ToolStripSeparator)
         {
            SetToolStripMenuItemText(menuItem, indexes);
         }
         else
         {
            // set text, then process children
            SetToolStripMenuItemText(menuItem, indexes);

            if (menuItem is ToolStripMenuItem)
            {
               var item = menuItem as ToolStripMenuItem;
               for (int i = 0; i < item.DropDownItems.Count; i++)
               {
                  indexes.Add(i);
                  SetToolStripMenuItemText(item.DropDownItems[i], indexes);
                  indexes.RemoveAt(indexes.Count - 1);
               }
            }
         }
      }

      private static void SetToolStripMenuItemText(ToolStripItem item, List<int> indexes)
      {
         if (__RootNode != null)
         {
            string formName = string.Empty;
            ToolStrip owner = item.Owner;
            while (owner is ToolStripDropDownMenu)
            {
               owner = (owner as ToolStripDropDownMenu).OwnerItem.Owner;
            }
            formName = owner.FindForm().Name;

            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            builder.AppendFormat("screen[@name='{0}']", formName);

            for (int i = 0; i < indexes.Count; i++)
            {
               builder.AppendFormat("/menu{0}[@index='{1}']", i == 0 ? string.Empty : "item", indexes[i]);
            }

            XmlNode node = __RootNode.SelectSingleNode(builder.ToString());
            if (node != null)
            {
               if (node.Attributes["value"] != null)
                  item.Text = node.Attributes["value"].Value;
            }
         }
      }

      #endregion

      /// <summary>
      /// Generates an xml document for the given form with all controls.
      /// </summary>
      /// <param name="frm">Form to process</param>
      /// <param name="path">Fully qualified file path</param>
      /// <history>
      /// [Curtis_Beard]		07/31/2006	Created
      /// </history>
      public static void GenerateXml(Form frm, string path)
      {
         XmlDocument xmlDoc = new XmlDocument();
         XmlNode rootNode;
         XmlAttribute attrib;

         try
         {
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

            rootNode = xmlDoc.CreateElement("screen");
            attrib = xmlDoc.CreateAttribute("name");
            attrib.Value = frm.Name;
            rootNode.Attributes.Append(attrib);
            attrib = xmlDoc.CreateAttribute("value");
            attrib.Value = frm.Text;
            rootNode.Attributes.Append(attrib);

            //process all controls on form
            foreach (Control control in frm.Controls)
               GenerateXmlControls(control, rootNode, xmlDoc);
            
            xmlDoc.AppendChild(rootNode);

            //process menu items on form
            if (frm.Menu != null)
            {
               foreach (MenuItem item in frm.Menu.MenuItems)
               {
                  XmlNode menuNode = xmlDoc.CreateElement("menu");

                  attrib = xmlDoc.CreateAttribute("index");
                  attrib.Value = item.Index.ToString();
                  menuNode.Attributes.Append(attrib);

                  attrib = xmlDoc.CreateAttribute("value");
                  attrib.Value = item.Text;
                  menuNode.Attributes.Append(attrib);

                  GenerateXmlMenuItems(item, menuNode, xmlDoc);

                  rootNode.AppendChild(menuNode);
               }
            }
            xmlDoc.Save(path);
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.ToString());
         }
      }
      #endregion

      #region Private Methods
      /// <summary>
      /// Process a given control to set its text property.
      /// </summary>
      /// <param name="control">Control to process</param>
      /// <param name="tip">ToolTip for control</param>
      /// <history>
      /// [Curtis_Beard]		07/31/2006	Created
      /// [Curtis_Beard]		02/01/2012	ADD: support for control's contextmenu
      /// [Curtis_Beard]		09/18/2013	ADD: 65, support for contextmenu sub items
      /// </history>
      private static void ProcessControl(Control control, ToolTip tip)
      {
         if (control.Controls.Count == 0)
         {
            SetControlText(control, tip);

            // set context menu if available
            if (control.ContextMenu != null && control.ContextMenu.MenuItems.Count > 0)
            {
               foreach (MenuItem item in control.ContextMenu.MenuItems)
               {
                  SetContextMenuItemText(control, item);

                  // process any sub menu items
                  if (item.MenuItems != null && item.MenuItems.Count > 0)
                  {
                     foreach (MenuItem subItem in item.MenuItems)
                     {
                        SetContextMenuItemText(control, item, subItem);
                     }
                  }
               }
            }
         }
         else
         {
            foreach (Control child in control.Controls)
            {
               ProcessControl(child, tip);
            }

            SetControlText(control, tip);
         }
      }

      /// <summary>
      /// Process each ToolStripItem in the ToolStrip
      /// </summary>
      /// <param name="control">ToolStrip to process</param>
      /// <param name="tip">ToolTip object</param>
      /// <history>
      /// [Curtis_Beard]		09/27/2012	Initial: 1741735, support ToolStripItem
      /// </history>
      private static void ProcessToolStrip(ToolStrip control, ToolTip tip)
      {
         foreach (ToolStripItem child in control.Items)
         {
            ProcessToolStripItem(child, tip);
         }
      }

      /// <summary>
      /// Process each ToolStripItem.
      /// </summary>
      /// <param name="control">ToolStripItem to process</param>
      /// <param name="tip">ToolTip object</param>
      /// <history>
      /// [Curtis_Beard]		09/27/2012	Initial: 1741735, support ToolStripItem
      /// </history>
      private static void ProcessToolStripItem(ToolStripItem control, ToolTip tip)
      {
         SetToolStripItemText(control, tip);
      }

      /// <summary>
      /// Set a given form's text property.
      /// </summary>
      /// <param name="frm">Form to set text</param>
      /// <history>
      /// [Curtis_Beard]		07/31/2006	Created
      /// </history>
      public static void SetFormText(Form frm)
      {
         if (__RootNode != null)
         {
            XmlNode node = __RootNode.SelectSingleNode("screen[@name='" + frm.Name + "']");

            if (node != null && node.Attributes["value"] != null )
            {
               //node found
               frm.Text = node.Attributes["value"].Value;
            }
         }
      }

      /// <summary>
      /// Generate xml data for a given control and its children.
      /// </summary>
      /// <param name="control">Control to process</param>
      /// <param name="rootNode">Root xml node</param>
      /// <param name="xmlDoc">Xml Document</param>
      /// <history>
      /// [Curtis_Beard]		07/31/2006	Created
      /// </history>
      private static void GenerateXmlControls(Control control, XmlNode rootNode, XmlDocument xmlDoc)
      {
         if (control.Controls.Count == 0)
            GenerateXmlControl(control, rootNode, xmlDoc);
         else
         {
            foreach (Control child in control.Controls)
               GenerateXmlControls(child, rootNode, xmlDoc);
            
            GenerateXmlControl(control, rootNode, xmlDoc);
         }
      }

      /// <summary>
      /// Generate xml data for a given control.
      /// </summary>
      /// <param name="control">Control to process</param>
      /// <param name="rootNode">Root xml node</param>
      /// <param name="xmlDoc">Xml Document</param>
      /// <history>
      /// [Curtis_Beard]		07/31/2006	Created
      /// </history>
      private static void GenerateXmlControl(Control control, XmlNode rootNode, XmlDocument xmlDoc)
      {
         if (rootNode != null)
         {
            XmlNode node = xmlDoc.CreateElement("control");
            XmlAttribute attrib = xmlDoc.CreateAttribute("name");

            if (!control.Name.Equals(string.Empty) && !control.Text.Equals(string.Empty))
            {
               //Name
               attrib.Value = control.Name;
               node.Attributes.Append(attrib);

               //Text
               attrib = xmlDoc.CreateAttribute("value");
               attrib.Value = control.Text;
               node.Attributes.Append(attrib);

               //Tooltip

               rootNode.AppendChild(node);
            }
         }
      }

      /// <summary>
      /// Generate xml data for a given MenuItem control and its children.
      /// </summary>
      /// <param name="item">MenuItem to process</param>
      /// <param name="rootNode">Root xml node</param>
      /// <param name="xmlDoc">Xml Document</param>
      /// <history>
      /// [Curtis_Beard]		07/31/2006	Created
      /// </history>
      private static void GenerateXmlMenuItems(MenuItem item, XmlNode rootNode, XmlDocument xmlDoc)
      {
         if (item.MenuItems.Count == 0)
            GenerateXmlMenuItem(item, rootNode, xmlDoc);
         else
         {
            foreach (MenuItem child in item.MenuItems)
               GenerateXmlMenuItems(child, rootNode, xmlDoc);
         }
      }

      /// <summary>
      /// Generate xml data for a given MenuItem control.
      /// </summary>
      /// <param name="item">MenuItem to process</param>
      /// <param name="rootNode">Root xml node</param>
      /// <param name="xmlDoc">Xml Document</param>
      /// <history>
      /// [Curtis_Beard]		07/31/2006	Created
      /// </history>
      private static void GenerateXmlMenuItem(MenuItem item, XmlNode rootNode, XmlDocument xmlDoc)
      {
         if (rootNode != null)
         {
            if (!item.Text.Equals(string.Empty))
            {
               XmlNode node = xmlDoc.CreateElement("menuitem");
               XmlAttribute attrib = xmlDoc.CreateAttribute("index");

               //index
               attrib.Value = item.Index.ToString();
               node.Attributes.Append(attrib);

               //Text
               attrib = xmlDoc.CreateAttribute("value");
               attrib.Value = item.Text;
               node.Attributes.Append(attrib);

               rootNode.AppendChild(node);
            }
         }
      }

      /// <summary>
      /// Retrieve the language information from the file specified.
      /// </summary>
      /// <param name="path">Full path to language file</param>
      /// <returns>LanguageItem object containing file information, otherwise null</returns>
      /// <history>
      /// [Curtis_Beard]		06/15/2015	CHG: 57, support external language files
      /// </history>
      private static LanguageItem GetLanguageItemFromFile(string path)
      {
         LanguageItem item = null;

         try
         {
            if (File.Exists(path))
            {
               XmlDocument doc = new XmlDocument();

               doc.Load(path);

               XmlNode root = doc.SelectSingleNode("language");

               if (root != null && root.Attributes.Count > 0)
               {
                  string displayName = string.Empty;
                  string culture = string.Empty;

                  if (root.Attributes["displayName"] != null)
                     displayName = root.Attributes["displayName"].Value;

                  if (root.Attributes["culture"] != null)
                     culture = root.Attributes["culture"].Value;

                  if (!string.IsNullOrEmpty(displayName) && !string.IsNullOrEmpty(culture))
                  {
                     item = new LanguageItem(displayName, culture, true, path);
                  }
               }
            }
         }
         catch (Exception ex)
         {
            LogClient.Instance.Logger.Error("Unable to retrieve external language information from file {0}, {1}", path, LogClient.GetAllExceptions(ex));
         }

         return item;
      }

      /// <summary>
      /// Retrieve all external language files from LanguageLocation property.
      /// </summary>
      /// <returns>List of all external language items</returns>
      /// <history>
      /// [Curtis_Beard]		06/15/2015	CHG: 57, support external language files
      /// </history>
      private static List<LanguageItem> GetExternalLanguages()
      {
         var items = new List<LanguageItem>();

         try
         {
            if (Directory.Exists(LanguageLocation))
            {
               string[] files = Directory.GetFiles(LanguageLocation, "*.xml");

               if (files.Length > 0)
               {
                  foreach (string file in files)
                  {
                     var item = GetLanguageItemFromFile(file);
                     if (item != null && !DoesExistInList(internalLanguages, item.Culture) && !DoesExistInList(items, item.Culture))
                     {
                        items.Add(item);
                     }
                     else
                     {
                        LogClient.Instance.Logger.Info("External language already exists {0}", item != null ? item.Culture : string.Empty);
                     }
                  }
               }
            }
         }
         catch (Exception ex)
         {
            LogClient.Instance.Logger.Error("Unable to retrieve external languages {0}", LogClient.GetAllExceptions(ex));
         }

         return items;
      }

      /// <summary>
      /// Check the given LanguageItem list for a given culture.
      /// </summary>
      /// <param name="items">List to check against</param>
      /// <param name="culture">Culture to check</param>
      /// <returns>true if list contains culture, false otherwise</returns>
      /// <history>
      /// [Curtis_Beard]		06/15/2015	CHG: 57, support external language files
      /// </history>
      private static bool DoesExistInList(List<LanguageItem> items, string culture)
      {
         if (items != null && !string.IsNullOrEmpty(culture))
         {
            foreach (var item in items)
            {
               if (item.Culture.Equals(culture, StringComparison.OrdinalIgnoreCase))
               {
                  return true;
               }
            }
         }

         return false;
      }

      /// <summary>
      /// Retrieves the top most control (parent) of the given control.
      /// </summary>
      /// <param name="ctrl">Control to find parent</param>
      /// <returns>Control that is the top most parent of the given control</returns>
      private static Control GetParentControl(Control ctrl)
      {
         if (ctrl.Parent == null)
         {
            return ctrl;
         }
         else
         {
            return GetParentControl(ctrl.Parent);
         }
      }

      /// <summary>
      /// Attempts to load an internal language file for the given culture.
      /// </summary>
      /// <param name="culture">Culture to load</param>
      /// <returns>true on success, false otherwise</returns>
      /// <history>
      /// [Curtis_Beard]		06/15/2015	CHG: 57, support external language files
      /// </history>
      private static bool LoadInternal(string culture)
      {
         try
         {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            string _name = assembly.GetName().Name;

            using (Stream stream = assembly.GetManifestResourceStream(String.Format("{0}.Language.{1}.xml", _name, culture)))
            {
               if (stream != null)
               {
                  string contents = string.Empty;

                  using (StreamReader _reader = new StreamReader(stream))
                  {
                     contents = _reader.ReadToEnd();
                  }

                  stream.Close();

                  if (!contents.Equals(string.Empty))
                  {
                     __XmlDoc = new XmlDocument();
                     __XmlDoc.LoadXml(contents);

                     __RootNode = __XmlDoc.SelectSingleNode("language");

                     if (__RootNode != null)
                     {
                        __XmlGenericNode = __RootNode.SelectSingleNode("generic");

                        if (__XmlGenericNode != null)
                        {
                           return true;
                        }
                     }
                  }
               }
            }
         }
         catch (Exception ex)
         {
            LogClient.Instance.Logger.Error("Error loading internal language {0}, {1}", culture, LogClient.GetAllExceptions(ex));
         }

         return false;
      }

      /// <summary>
      /// Attempts to load an external language file for the given culture.
      /// </summary>
      /// <param name="culture">Culture to load</param>
      /// <returns>true on success, false otherwise</returns>
      /// <history>
      /// [Curtis_Beard]		06/15/2015	CHG: 57, support external language files
      /// </history>
      private static bool LoadExternal(string culture)
      {
         try
         {
            if (Directory.Exists(LanguageLocation))
            {
               var items = GetExternalLanguages();
               if (items != null && items.Count > 0)
               {
                  var item = (from i in items where i.Culture.Equals(culture, StringComparison.OrdinalIgnoreCase) select i).FirstOrDefault();
                  if (item != null)
                  {
                     // found external language match, load content from file                     
                     __XmlDoc = new XmlDocument();
                     __XmlDoc.Load(item.ExternalFilePath);

                     XmlNode root = __XmlDoc.SelectSingleNode("language");

                     if (root != null && root.Attributes.Count > 0)
                     {
                        __RootNode = root;

                        if (__RootNode != null)
                        {
                           __XmlGenericNode = __RootNode.SelectSingleNode("generic");

                           if (__XmlGenericNode != null)
                           {
                              return true;
                           }
                        }
                     }
                  }
               }
            }
         }
         catch (Exception ex)
         {
            LogClient.Instance.Logger.Error("Error loading external language {0}, {1}", culture, LogClient.GetAllExceptions(ex));
         }

         return false;
      }
      #endregion
   }

   /// <summary>
   /// Used to contain a language file.
   /// </summary>
   /// <history>
   /// [Curtis_Beard]		05/22/2007	Created
   /// [Curtis_Beard]		06/15/2015	CHG: 57, support external language files
   /// </history>
   internal class LanguageItem
   {
      /// <summary>
      /// Gets/Sets the language's display name.
      /// </summary>
      public string DisplayName
      {
         get;
         set;
      }

      /// <summary>
      /// Gets/Sets the language's culture string.
      /// </summary>
      public string Culture
      {
         get;
         set;
      }

      /// <summary>
      /// Gets/Sets whether language is from external file.
      /// </summary>
      public bool IsExternal
      {
         get;
         set;
      }

      /// <summary>
      /// Gets/Sets the external language's file path.
      /// </summary>
      /// <remarks>
      /// Will be null if internal
      /// </remarks>
      public string ExternalFilePath
      {
         get;
         set;
      }

      /// <summary>
      /// Creates a new instance of the LanguageItem class.
      /// </summary>
      /// <param name="displayName">Display name</param>
      /// <param name="culture">Culture string</param>
      /// <history>
      /// [Curtis_Beard]		05/22/2007	Created
      /// [Curtis_Beard]		06/15/2015	CHG: 57, support external language files
      /// </history>
      public LanguageItem(string displayName, string culture)
      {
         DisplayName = displayName;
         Culture = culture;
         IsExternal = false;
         ExternalFilePath = null;
      }

      /// <summary>
      /// Creates a new instance of the LanguageItem class.
      /// </summary>
      /// <param name="displayName">Display name</param>
      /// <param name="culture">Culture string</param>
      /// <param name="isExternal">Language is from external file</param>
      /// <history>
      /// [Curtis_Beard]		06/15/2015	CHG: 57, support external language files
      /// </history>
      public LanguageItem(string displayName, string culture, bool isExternal, string filePath)
         : this(displayName, culture)
      {
         IsExternal = isExternal;

         if (IsExternal)
         {
            ExternalFilePath = filePath;
         }
      }
   }
}
