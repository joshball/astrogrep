using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AstroGrep.Windows.Forms
{
   /// <summary>
   /// Displays the command line options for this program.
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
   /// [Curtis_Beard]	   09/26/2012	ADD: 3572487, display command line flags
   /// </history>
   public partial class frmCommandLine : Form
   {
      /// <summary>
      /// Creates an instance of the frmCommandLine class.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   09/26/2012	ADD: 3572487, display command line flags
      /// </history>
      public frmCommandLine()
      {
         InitializeComponent();
      }

      /// <summary>
      /// Form Load Event
      /// </summary>
      /// <param name="sender">system parm</param>
      /// <param name="e">system parm</param>
      /// <history>
      /// [Curtis_Beard]	   09/26/2012	ADD: 3572487, display command line flags
      /// </history>
      private void frmCommandLine_Load(object sender, EventArgs e)
      {
         //Language.GenerateXml(this, Application.StartupPath + "\\" + this.Name + ".xml");
         Language.ProcessForm(this);

         // setup command line flags text
         System.Text.StringBuilder builder = new System.Text.StringBuilder();
         builder.AppendFormat("Command Line Arguments {0}", Environment.NewLine);
         builder.Append(Environment.NewLine);
         builder.AppendFormat("/spath=\"value\"     - Start Path{0}", Environment.NewLine);
         builder.AppendFormat("/stypes=\"value\"    - File Types{0}", Environment.NewLine);
         builder.AppendFormat("/stext=\"value\"     - Search Text{0}", Environment.NewLine);
         builder.AppendFormat("/local             - Store config files in local directory{0}", Environment.NewLine);
         builder.AppendFormat("/e                 - Use regular expressions{0}", Environment.NewLine);
         builder.AppendFormat("/c                 - Case sensitive{0}", Environment.NewLine);
         builder.AppendFormat("/w                 - Whole Word{0}", Environment.NewLine);
         builder.AppendFormat("/r                 - Recursive search (search subfolders){0}", Environment.NewLine);
         builder.AppendFormat("/n                 - Negation{0}", Environment.NewLine);
         builder.AppendFormat("/l                 - Line numbers{0}", Environment.NewLine);
         builder.AppendFormat("/f                 - File names only{0}", Environment.NewLine);
         builder.AppendFormat("/cl=\"value\"        - Number of context lines{0}", Environment.NewLine);
         builder.AppendFormat("/sh                - Skip hidden files and folders{0}", Environment.NewLine);
         builder.AppendFormat("/ss                - Skip system files and directories{0}", Environment.NewLine);
         builder.AppendFormat("/s                 - Start searching immediately{0}", Environment.NewLine);

         txtDisplay.Text = builder.ToString();
         txtDisplay.Select(0, 0);
      }

      /// <summary>
      /// Closes this form.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   09/26/2012	ADD: 3572487, display command line flags
      /// </history>
      private void btnOK_Click(object sender, EventArgs e)
      {
         this.Close();
      }
   }
}
