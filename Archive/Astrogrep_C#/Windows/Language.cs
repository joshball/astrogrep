using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

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
      #endregion

      /// <summary>
      /// Initializes an instance of the Language class.
      /// </summary>
      private Language()
      {}

      #region Public Methods
      /// <summary>
      /// Loads the given language's file.
      /// </summary>
      /// <param name="language">String containing language</param>
      /// <history>
      /// [Curtis_Beard]		07/31/2006	Created
      /// [Curtis_Beard]		10/11/2006	CHG: Close stream
      /// </history>
      public static void Load(string language)
      {
         try
         {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            string _name = _assembly.GetName().Name;

            Stream _stream = _assembly.GetManifestResourceStream(String.Format("{0}.Language.{1}.xml", _name, language));

            if (_stream != null)
            {
               string _contents = string.Empty;

               using (StreamReader _reader = new StreamReader(_stream))
               {
                  _contents = _reader.ReadToEnd();
               }

               _stream.Close();

               if (!_contents.Equals(string.Empty))
               {
                  __XmlDoc = new XmlDocument();
                  __XmlDoc.LoadXml(_contents);

                  __RootNode = __XmlDoc.SelectSingleNode("language");

                  if (__RootNode != null)
                     __XmlGenericNode = __RootNode.SelectSingleNode("generic");
               }
            }

            _assembly = null;
         }
         catch {}
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
                  //found control node

                  //text
                  if (controlNode.Attributes["value"] != null)
                     return controlNode.Attributes["value"].Value;
               }
            }
         }

         return string.Empty;
      }

      /// <summary>
      /// Sets the given menuitem's text property.
      /// </summary>
      /// <param name="item">MenuItem to set</param>
      /// <param name="index">Index of item's parent MenuItem</param>
      /// <history>
      /// [Curtis_Beard]		07/31/2006	Created
      /// </history>
      public static void SetMenuItemText(MenuItem item, int index)
      {
         if (__RootNode != null)
         {
            string formName = item.GetMainMenu().GetForm().Name;
            XmlNode node = __RootNode.SelectSingleNode("screen[@name='" + formName + "']/menu[@index='" + index + "']/menuitem[@index='" + item.Index + "']");

            if (node != null)
            {
               if (node.Attributes["value"] != null)
                  item.Text = node.Attributes["value"].Value;
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
         if (__XmlGenericNode != null)
         {
            XmlNode node = __XmlGenericNode.SelectSingleNode("text[@name='" + name + "']");

            if (node != null && node.Attributes["value"] != null)
               return node.Attributes["value"].Value;
         }

         return string.Empty;
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
               ProcessControl(control, tip);

            //process menu items on form
            if (frm.Menu != null)
            {
               foreach (MenuItem item in frm.Menu.MenuItems)
               {
                  XmlNode menuNode = __RootNode.SelectSingleNode("screen[@name='" + frm.Name + "']/menu[@index='" + item.Index + "']");

                  if (menuNode != null && menuNode.Attributes["value"] != null)
                  {
                     item.Text = menuNode.Attributes["value"].Value;

                     ProcessMenuItem(item, item.Index);
                  }
               }
            }
         }
      }

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
      /// </history>
      private static void ProcessControl(Control control, ToolTip tip)
      {
         if (control.Controls.Count == 0)
            SetControlText(control, tip);
         else
         {
            foreach (Control child in control.Controls)
               ProcessControl(child, tip);

            SetControlText(control, tip);
         }
      }

      /// <summary>
      /// Processa given MenuItem to set its text property.
      /// </summary>
      /// <param name="item">MenuItem to process</param>
      /// <param name="index">Index of MenuItem's parent</param>
      /// <history>
      /// [Curtis_Beard]		07/31/2006	Created
      /// </history>
      private static void ProcessMenuItem(MenuItem item, int index)
      {
         if (item.MenuItems.Count == 0)
            SetMenuItemText(item, index);
         else
         {
            foreach (MenuItem child in item.MenuItems)
               ProcessMenuItem(child, index);

            //SetMenuItemText(item, index);
         }
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
      #endregion
	}
}
