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
   /// Display messages Dialog for Exclusions/Errors.
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
   /// [Curtis_Beard]        10/05/2012  Initial: 1741735, show dialog to user about filter/error
   /// </history>
   public partial class frmDisplayMessages : Form
   {
      /// <summary>Collection of messages to display</summary>
      public System.Collections.Specialized.StringCollection Messages { get; set; }

      /// <summary>The title text</summary>
      public string TitleMessage { get; set; }

      /// <summary>
      /// Initializes an instance of this class.
      /// </summary>
      public frmDisplayMessages()
      {
         InitializeComponent();
      }

      /// <summary>
      /// Closes the dialog.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      private void btnOK_Click(object sender, EventArgs e)
      {
         this.Close();
      }

      /// <summary>
      /// Setup the messages for display.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      private void frmDisplayMessages_Load(object sender, EventArgs e)
      {
         // safety check
         if (Messages == null || Messages.Count == 0)
         {
            this.Close();
         }

         Language.ProcessForm(this);

         // set window title
         if (!string.IsNullOrEmpty(TitleMessage))
         {
            Text = TitleMessage;
         }

         // set text display
         string[] msgs = new string[Messages.Count];
         Messages.CopyTo(msgs, 0);

         if (msgs.Length > 0)
         {
            txtMessages.Lines = msgs;
         }
      }
   }
}
