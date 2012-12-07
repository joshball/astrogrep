using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AstroGrep.Core;

namespace AstroGrep.Windows.Forms
{
    /// <summary>
    /// Display a filterable view of the log items.
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
    /// [Curtis_Beard]	   12/06/2012	ADD: 1741735, initial dialog for filterable log items viewer.
    /// </history>
    public partial class frmLogDisplay : Form
    {
        /// <summary>Collection of messages to display</summary>
        public List<LogItem> LogItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public enum DefaultFilterTypes
        {
            /// <summary></summary>
            None,
            /// <summary></summary>
            Status,
            /// <summary></summary>
            Exclusions,
            /// <summary></summary>
            Error
        }

        /// <summary>
        /// 
        /// </summary>
        public DefaultFilterTypes DefaultFilterType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public frmLogDisplay()
        {
            InitializeComponent();
            API.ListViewExtensions.SetTheme(lstLog);
            DefaultFilterType = DefaultFilterTypes.None;

            toolStrip1.RenderMode = ToolStripRenderMode.System;
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogDisplay_Load(object sender, EventArgs e)
        {
            // safety check
            if (LogItems == null || LogItems.Count == 0)
            {
                this.Close();
            }

            Language.ProcessForm(this);

            lstLog.Columns[1].Width = 600;

            switch (DefaultFilterType)
            {
                case DefaultFilterTypes.Status:
                    sbtnExclusions.Checked = sbtnError.Checked = false;
                    sbtnStatus.Checked = true;
                    break;
                case DefaultFilterTypes.Exclusions:
                    sbtnError.Checked = sbtnStatus.Checked = false;
                    sbtnExclusions.Checked = true;
                    break;
                case DefaultFilterTypes.Error:
                    sbtnExclusions.Checked = sbtnStatus.Checked = false;
                    sbtnError.Checked = true;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogDisplay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstLog_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        //private class MyRenderer : ToolStripProfessionalRenderer
        //{
        //    protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        //    {
        //        var btn = e.Item as ToolStripButton;
        //        if (btn != null && btn.CheckOnClick && btn.Checked)
        //        {
        //            Rectangle bounds = new Rectangle(Point.Empty, e.Item.Size);
        //            e.Graphics.FillRectangle(SystemBrushes.ButtonHighlight, bounds);
        //        }
        //        else 
        //            base.OnRenderButtonBackground(e);
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        private void AddLogItemType(LogItem.LogItemTypes type)
        {
            lstLog.BeginUpdate();

            foreach (LogItem item in LogItems)
            {
                if (item.ItemType == type)
                {
                    ListViewItem lstItem = new ListViewItem(new string[3] { type.ToString(), item.Value, item.Details });
                    lstItem.Tag = item;
                    lstLog.Items.Add(lstItem);
                }
            }

            lstLog.ListViewItemSorter = new LogItemComparer();
            lstLog.Sort();

            lstLog.EndUpdate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        private void RemoveLogItemType(LogItem.LogItemTypes type)
        {
            lstLog.BeginUpdate();

            foreach (ListViewItem lstItem in lstLog.Items)
            {
                LogItem item = lstItem.Tag as LogItem;
                if (item.ItemType == type)
                {
                    lstLog.Items.Remove(lstItem);
                }
            }

            lstLog.ListViewItemSorter = new LogItemComparer();
            lstLog.Sort();

            lstLog.EndUpdate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (sbtnStatus.Checked)
            {
                // include status
                sbtnStatus.Checked = true;
                AddLogItemType(LogItem.LogItemTypes.Status);
            }
            else
            {
                // remove status
                RemoveLogItemType(LogItem.LogItemTypes.Status);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnExclusions_CheckedChanged(object sender, EventArgs e)
        {
            if (sbtnExclusions.Checked)
            {
                // include exclusions
                sbtnExclusions.Checked = true;
                AddLogItemType(LogItem.LogItemTypes.Exclusion);
            }
            else
            {
                // remove exclusions
                RemoveLogItemType(LogItem.LogItemTypes.Exclusion);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnError_CheckedChanged(object sender, EventArgs e)
        {
            if (sbtnError.Checked)
            {
                // include error
                sbtnError.Checked = true;
                AddLogItemType(LogItem.LogItemTypes.Error);    
            }
            else
            {
                // remove error
                sbtnError.Checked = false;
                RemoveLogItemType(LogItem.LogItemTypes.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal class LogItemComparer : System.Collections.IComparer
        {
            /// <summary>
            /// 
            /// </summary>
            public LogItemComparer()
            {

            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(object x, object y)
            {
                LogItem xItem = (x as ListViewItem).Tag as LogItem;
                LogItem yItem = (y as ListViewItem).Tag as LogItem;

                return xItem.Date.CompareTo(yItem.Date);
            }
        }
    }
}
