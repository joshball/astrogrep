using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AstroGrep.Windows.Controls
{

   /// <summary>
   /// Used to display a picture and text for use in a Options selection dialog.
   /// </summary>
   /// <history>
   /// 	[Curtis_Beard]		07/19/2006	Created
   /// </history>
   [DefaultEvent("Click")]
   public class OptionsCategoryItem : System.Windows.Forms.Panel
   {

      #region Declarations
      private string __Text = string.Empty;
      private Image __Image = null;
      private bool __MnemonicKeyVisible = false;
      private bool __Selected = false;
      private bool __Hovering = false;
      private Color __SelectedColor = Color.FromArgb(193, 210, 238);
      private Color __HoveringColor = Color.FromArgb(224, 232, 246);
      #endregion

      /// <summary>
      /// Raised when control is clicked.
      /// </summary>
      /// <history>
      /// 	[Curtis_Beard]		07/19/2006	Created
      /// </history>
      public new event EventHandler Click;

      /// <summary>
      /// Initializes a new instance of the OptionsCategoryItem class.
      /// </summary>
      /// <history>
      /// 	[Curtis_Beard]		07/19/2006	Created
      /// </history>
      public OptionsCategoryItem() : base()
      {
         SetStyle(ControlStyles.ResizeRedraw, true);
         SetStyle(ControlStyles.UserPaint, true);
         SetStyle(ControlStyles.AllPaintingInWmPaint, true);
         SetStyle(ControlStyles.DoubleBuffer, true);
         SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      }

      #region Overrides Methods
      /// <summary>
      /// Handles drawing the items for this control during different states.
      /// </summary>
      /// <param name="e">PaintEventArgs</param>
      /// <history>
      /// 	[Curtis_Beard]		07/19/2006	Created
      /// </history>
      protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
      {
         base.OnPaint(e);

         const int IMAGE_SIZE = 32;
         Graphics g = e.Graphics;
         Rectangle rect = new Rectangle(Convert.ToInt32((this.Width - IMAGE_SIZE) / 2), 5, IMAGE_SIZE, IMAGE_SIZE);
         bool imageDrawn = false;

         if (__Selected)
            g.FillRectangle(new SolidBrush(__SelectedColor), this.ClientRectangle);

         if (!__Selected && __Hovering)
            g.FillRectangle(new SolidBrush(__HoveringColor), this.ClientRectangle);

         if (__Image != null)
         {
            g.DrawImage(__Image, rect);
            imageDrawn = true;
         }

         if (!base.Text.Equals(string.Empty))
         {
            RectangleF rectF;
            StringFormat format  = new StringFormat();
            string textToDraw  = base.Text;

            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            if (__MnemonicKeyVisible)
               format.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
            else
            {
               format.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
               textToDraw = textToDraw.Replace("&", string.Empty);
            }

            if (imageDrawn)
               rectF = new RectangleF(0, rect.Y + rect.Height + 1, this.Width, this.Height - rect.Bottom - 5);
            else
               rectF = new RectangleF(new PointF(0, Convert.ToInt32((this.Height - 20) / 2)), new SizeF(this.Width, 20));

            g.DrawString(textToDraw, this.Font, Brushes.Black, rectF, format);

            format.Dispose();
         }
      }

      /// <summary>
      /// Enable hovering when mouse enters.
      /// </summary>
      /// <param name="e">EventArgs</param>
      /// <history>
      /// 	[Curtis_Beard]		07/19/2006	Created
      /// </history>
      protected override void OnMouseEnter(System.EventArgs e)
      {
         __Hovering = true;
         this.Invalidate();
      }

      /// <summary>
      /// Disable hovering when mouse leaves.
      /// </summary>
      /// <param name="e">EventArgs</param>
      /// <history>
      /// 	[Curtis_Beard]		07/19/2006	Created
      /// </history>
      protected override void OnMouseLeave(System.EventArgs e)
      {
         __Hovering = false;
         this.Invalidate();
      }

      /// <summary>
      /// Raise the click event when the left mouse button selects this control.
      /// </summary>
      /// <param name="e">MouseEventArgs</param>
      /// <history>
      /// 	[Curtis_Beard]		07/19/2006	Created
      /// </history>
      protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
      {
         if (e.Button == MouseButtons.Left)
         {
            if (Click != null)
               Click(this, null);
            this.IsSelected = true;
         }
      }

      /// <summary>
      /// Raise the click event when this control's mnemonic character is detected.
      /// </summary>
      /// <param name="charCode">character pressed</param>
      /// <returns>true if processed, false otherwise</returns>
      /// <history>
      /// 	[Curtis_Beard]		07/20/2006	Created
      /// </history>
      protected override bool ProcessMnemonic(char charCode)
      {
         if (Control.IsMnemonic(charCode, base.Text))
         {
            if (Click != null)
               Click(this, null);

            this.IsSelected = true;
            return true;
         }

         return base.ProcessMnemonic(charCode);
      }
      #endregion

      #region Properties
      /// <summary>
      /// The textsociated with this control.
      /// </summary>
      /// <history>
      /// 	[Curtis_Beard]		07/19/2006	Created
      /// </history>
      [Browsable(true),
      Bindable(true),
      EditorBrowsable(EditorBrowsableState.Always),
      DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
      Description("The textsociated with this control.")]
      public override string Text
      {
         get { return base.Text; }
         set 
         {
            base.Text = value;
            this.Invalidate();
         }
      }

      /// <summary>
      /// The imagesociated with this control.
      /// </summary>
      /// <history>
      /// 	[Curtis_Beard]		07/19/2006	Created
      /// </history>
      [Description("The imagesociated with this control.")]
      public Image Image
      {
         get { return __Image; }
         set 
         {
            __Image = value;
            this.Invalidate();
         }
      }

      /// <summary>
      /// Determines whether the control is selected.
      /// </summary>
      /// <history>
      /// 	[Curtis_Beard]		07/19/2006	Created
      /// </history>
      [Description("Determines whether the control is selected.")]
      public bool IsSelected
      {
         get { return __Selected; }
         set
         {
            __Selected = value;
            this.Invalidate();
         }
      }

      /// <summary>
      /// Determines whether the control displays the mnemonic key indicator.
      /// </summary>
      /// <history>
      /// 	[Curtis_Beard]		07/20/2006	Created
      /// </history>
      [Description("Determines whether the control displays the mnemonic key indicator.")]
      public bool IsMnemonicKeyVisible
      {
         get { return __MnemonicKeyVisible; }
         set 
         {
            __MnemonicKeyVisible = value;
            this.Invalidate();
         }
      }
      #endregion
   }
}