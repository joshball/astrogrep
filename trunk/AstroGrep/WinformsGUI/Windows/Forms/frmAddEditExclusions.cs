using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AstroGrep.Windows.Controls;
using libAstroGrep;

namespace AstroGrep.Windows.Forms
{
   /// <summary>
   /// Used to edit a single exclusion item.
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
   /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
   /// [Curtis_Beard]	   11/11/2014	CHG: use FilterItem
   /// </history>
   public partial class frmAddEditExclusions : Form
   {
      private List<FilterType> defaultFilterTypes = null;
      private FilterItem _item = null;
      private List<FilterItem> filterItems = new List<FilterItem>();

      /// <summary>
      /// Gets the current exclusion item for this dialog.
      /// </summary>
      public FilterItem CurrentItem { get { return _item; } }

      /// <summary>
      /// Create an instance of this form.
      /// </summary>
      /// <param name="items"></param>
      /// <param name="item">FilterItem object to edit, null if adding a new one.</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// [Curtis_Beard]	   11/11/2014	CHG: use FilterItem, pass all FilterItems from display form
      /// </history>
      public frmAddEditExclusions(List<FilterItem> items, FilterItem item)
      {
         InitializeComponent();

         _item = item;

         defaultFilterTypes = FilterType.GetDefaultFilterTypes();

         filterItems = items;
      }

      /// <summary>
      /// Setup the form.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// [Curtis_Beard]	   11/11/2014	CHG: use FilterItem
      /// </history>
      private void frmExclusions_Load(object sender, EventArgs e)
      {
         Language.ProcessForm(this);

         cboCategories.DisplayMember = "DisplayName";
         foreach (string name in Enum.GetNames(typeof(FilterType.Categories)))
         {
            ComboBoxEntry entry = new ComboBoxEntry();
            entry.DisplayName = Language.GetGenericText("Exclusions." + name);
            entry.ValueName = name;
            cboCategories.Items.Add(entry);
         }

         // load fields with values if in edit mode.
         if (_item != null)
         {
            cboCategories.SelectedIndex = cboCategories.FindStringExact(Language.GetGenericText("Exclusions." + _item.FilterType.Category.ToString()));
            cboTypes.SelectedIndex = cboTypes.FindStringExact(Language.GetGenericText("Exclusions." + _item.FilterType.SubCategory.ToString()));
            switch (_item.FilterType.ValueType)
            {
               case FilterType.ValueTypes.DateTime:
                  fvtValue.SetViewType(FilterValueType.ViewTypes.DateTime);
                  break;
               case FilterType.ValueTypes.Long:
                  fvtValue.SetViewType(FilterValueType.ViewTypes.Numeric);
                  break;
               case FilterType.ValueTypes.Size:
                  fvtValue.SetViewType(FilterValueType.ViewTypes.Size);
                  fvtValue.SetSizeDropDown(_item.ValueSizeOption);
                  break;
               case FilterType.ValueTypes.String:
                  fvtValue.SetViewType(FilterValueType.ViewTypes.String);
                  break;
            }
            if (_item.FilterType.ValueType != FilterType.ValueTypes.Null)
            {
               if (_item.FilterType.ValueType == FilterType.ValueTypes.Size)
               {
                  fvtValue.Value = AstroGrep.Core.Convertors.ConvertFileSizeForDisplay(_item.Value, _item.ValueSizeOption);
               }
               else
               {
                  fvtValue.Value = _item.Value;
               }
            }
            cboOptions.SelectedIndex = cboOptions.FindStringExact(Language.GetGenericText("Exclusions." + _item.ValueOption.ToString()));
            chkIgnoreCase.Checked = _item.ValueIgnoreCase;
         }
      }

      /// <summary>
      /// Save the new/edit item.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// [Curtis_Beard]	   11/11/2014	CHG: use FilterItem
      /// </history>
      private void btnOK_Click(object sender, EventArgs e)
      {
         this.DialogResult = DialogResult.None;
         FilterItem item = VerifyInterface();

         if (item != null)
         {
            _item = item;

            this.DialogResult = DialogResult.OK;
            this.Close();
         }
      }

      /// <summary>
      /// Close the dialog.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// </history>
      private void btnCancel_Click(object sender, EventArgs e)
      {
         this.DialogResult = DialogResult.Cancel;
         this.Close();
      }

      /// <summary>
      /// Setup controls based on selected category.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   11/11/2014	CHG: use FilterItem
      /// </history>
      private void cboCategories_SelectedIndexChanged(object sender, EventArgs e)
      {
         cboTypes.Items.Clear();
         cboTypes.DisplayMember = "DisplayName";
         cboOptions.Items.Clear();
         chkIgnoreCase.Checked = false;
         fvtValue.ResetValue();

         if (cboCategories.SelectedIndex == cboCategories.FindStringExact(Language.GetGenericText("Exclusions." + FilterType.Categories.File.ToString())))
         {
            var fileList = from l in defaultFilterTypes where l.Category == FilterType.Categories.File select l;
            foreach (var entry in fileList)
            {
               ComboBoxEntry cbEntry = new ComboBoxEntry();
               cbEntry.DisplayName = Language.GetGenericText("Exclusions." + entry.SubCategory.ToString());
               cbEntry.ValueName = entry.SubCategory.ToString();
               cbEntry.Value = entry;
               cboTypes.Items.Add(cbEntry);
            }
         }
         else if (cboCategories.SelectedIndex == cboCategories.FindStringExact(Language.GetGenericText("Exclusions." + FilterType.Categories.Directory.ToString())))
         {
            var dirList = from l in defaultFilterTypes where l.Category == FilterType.Categories.Directory select l;
            foreach (var entry in dirList)
            {
               ComboBoxEntry cbEntry = new ComboBoxEntry();
               cbEntry.DisplayName = Language.GetGenericText("Exclusions." + entry.SubCategory.ToString());
               cbEntry.ValueName = entry.SubCategory.ToString();
               cbEntry.Value = entry;
               cboTypes.Items.Add(cbEntry);
            }
         }
      }

      /// <summary>
      /// Setup controls based on selected type.
      /// </summary>
      /// <param name="sender">system parameter</param>
      /// <param name="e">system parameter</param>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// [Curtis_Beard]	   08/13/2014	ADD: 78, add binary files exclusion
      /// [Curtis_Beard]	   11/11/2014	CHG: use FilterItem
      /// </history>
      private void cboTypes_SelectedIndexChanged(object sender, EventArgs e)
      {
         var cboItem = cboTypes.SelectedItem as ComboBoxEntry;
         var et = cboItem.Value as FilterType;

         // default value field to string and clear values
         fvtValue.SetViewType(FilterValueType.ViewTypes.String);
         fvtValue.ResetValue();

         // set ignore case checkbox
         chkIgnoreCase.Checked = false;
         chkIgnoreCase.Enabled = et.SupportsIgnoreCase;

         // setup options drop down
         cboOptions.Items.Clear();
         cboOptions.DisplayMember = "DisplayName";
         if (et.SupportedValueOptions == null || (et.SupportedValueOptions.Count == 1 && et.SupportedValueOptions[0] == FilterType.ValueOptions.None))
         {
            cboOptions.Enabled = false;
         }
         else
         {
            cboOptions.Enabled = true;
            foreach (var item in et.SupportedValueOptions)
            {
               ComboBoxEntry entry = new ComboBoxEntry();
               entry.DisplayName = Language.GetGenericText(string.Format("Exclusions.{0}", item));
               entry.ValueName = item.ToString();
               cboOptions.Items.Add(entry);
            }
         }

         // set value type/view
         switch (et.ValueType)
         {
            case FilterType.ValueTypes.Null:
               fvtValue.Enabled = false;
               fvtValue.SetViewType(FilterValueType.ViewTypes.String);
               break;

            case FilterType.ValueTypes.String:
               fvtValue.Enabled = true;
               fvtValue.SetViewType(FilterValueType.ViewTypes.String);
               break;

            case FilterType.ValueTypes.DateTime:
               fvtValue.Enabled = true;
               fvtValue.SetViewType(FilterValueType.ViewTypes.DateTime);
               break;

            case FilterType.ValueTypes.Long:
               fvtValue.Enabled = true;
               fvtValue.SetViewType(FilterValueType.ViewTypes.Numeric);
               break;

            case FilterType.ValueTypes.Size:
               fvtValue.Enabled = true;
               fvtValue.SetViewType(FilterValueType.ViewTypes.Size);
               break;
         }
      }

      /// <summary>
      /// Verify that the values selected are valid. [MessageBox displayed based on error]
      /// </summary>
      /// <returns>FilterItem object if valid, null if not.</returns>
      /// <history>
      /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions
      /// [Curtis_Beard]	   11/11/2014	CHG: use FilterItem
      /// </history>
      private FilterItem VerifyInterface()
      {
         // needs to be a category
         if (cboCategories.SelectedIndex == -1)
         {
            MessageBox.Show(this, Language.GetGenericText("Exclusions.Error.NoCategory"), Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return null;
         }

         // needs to be a type
         if (cboTypes.SelectedIndex == -1)
         {
            MessageBox.Show(this, Language.GetGenericText("Exclusions.Error.NoType"), Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return null;
         }

         // create item and set type, value
         var item = new FilterItem();
         var cboTypeItem = cboTypes.SelectedItem as ComboBoxEntry;
         item.FilterType = cboTypeItem.Value as FilterType;
         item.Value = fvtValue.Value;
         item.ValueIgnoreCase = chkIgnoreCase.Checked;
         item.Enabled = true;

         // size needs size option
         if (item.FilterType.Category == FilterType.Categories.File && item.FilterType.SubCategory == FilterType.SubCategories.Size)
         {
            item.ValueSizeOption = fvtValue.GetSizeDropDown();
         }

         // get selected option
         if (cboOptions.SelectedIndex == -1)
         {
            item.ValueOption = FilterType.ValueOptions.None;
         }
         else
         {
            var cboOptionsItem = cboOptions.SelectedItem as ComboBoxEntry;
            item.ValueOption = (FilterType.ValueOptions)Enum.Parse(typeof(FilterType.ValueOptions), cboOptionsItem.ValueName);
         }

         // needs to be a value
         if (item.FilterType.ValueType != FilterType.ValueTypes.Null && string.IsNullOrEmpty(item.Value.Trim()))
         {
            MessageBox.Show(this, Language.GetGenericText("Exclusions.Error.NoValue"), Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return null;
         }

         // if options supported then options value must be something other than None
         if (item.ValueOption == FilterType.ValueOptions.None &&
            (item.FilterType.SupportedValueOptions != null && item.FilterType.SupportedValueOptions.Count > 0 && item.FilterType.SupportedValueOptions[0] != FilterType.ValueOptions.None))
         {
            MessageBox.Show(this, Language.GetGenericText("Exclusions.Error.InvalidOptions"), Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return null;
         }

         // check how many we can have
         if (!item.FilterType.SupportsMulitpleItems)
         {
            if (_item == null)
            {
               // adding
               if (filterItems != null)
               {
                  var existing = (from i in filterItems where i.FilterType.Category == item.FilterType.Category && i.FilterType.SubCategory == item.FilterType.SubCategory select i).ToList();
                  if (existing.Count > 0)
                  {
                     MessageBox.Show(this, string.Format(Language.GetGenericText("Exclusions.Error.TypeCategoryLimit"), item.FilterType.SubCategory, item.FilterType.Category), Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                     return null;
                  }
               }
            }
            else if (_item.FilterType.Category != item.FilterType.Category || _item.FilterType.SubCategory != item.FilterType.SubCategory)
            {
               if (filterItems != null)
               {
                  var existing = (from i in filterItems where i.FilterType.Category == item.FilterType.Category && i.FilterType.SubCategory == item.FilterType.SubCategory select i).ToList();
                  if (existing.Count > 0)
                  {
                     MessageBox.Show(this, string.Format(Language.GetGenericText("Exclusions.Error.TypeCategoryLimit"), item.FilterType.SubCategory, item.FilterType.Category), Constants.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                     return null;
                  }
               }
            }
         }

         return item;
      }

      /// <summary>
      /// Used for ComboBox items to store display and actual values of an item.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   11/11/2014	Initial
      /// </history>
      private class ComboBoxEntry
      {
         /// <summary>
         /// Display name
         /// </summary>
         public string DisplayName { get; set; }

         /// <summary>
         /// Value name
         /// </summary>
         public string ValueName { get; set; }

         /// <summary>
         /// Actual value
         /// </summary>
         public object Value { get; set; }
      }
   }
}
