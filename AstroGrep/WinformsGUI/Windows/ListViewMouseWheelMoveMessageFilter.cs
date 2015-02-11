using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AstroGrep.Windows
{
   /// <summary>
   /// IMessageFilter for handling mouse wheel movements when mouse is over a ListView.
   /// </summary>
   public class ListViewMouseWheelMoveMessageFilter : IMessageFilter
   {
      /// <summary>
      /// Creates an instance of this class.
      /// </summary>
      public ListViewMouseWheelMoveMessageFilter()
      {

      }

      /// <summary>
      /// Filters the messages and handles the mouse wheel movements.
      /// </summary>
      /// <param name="m">Current message to process</param>
      /// <returns>true if processed, false if not</returns>
      public bool PreFilterMessage(ref Message m)
      {
         switch (m.Msg)
         {
            case WM_MOUSEWHEEL:   // 0x020A
            case WM_MOUSEHWHEEL:  // 0x020E
               IntPtr hControlUnderMouse = WindowFromPoint(new Point((int)m.LParam));
               if (hControlUnderMouse == m.HWnd)
                  return false; // already headed for the right control
               else
               {
                  // redirect the message to the control under the mouse
                  var ctrl = Control.FromHandle(hControlUnderMouse);
                  if (ctrl != null && ctrl is ListView)
                  {
                     SendMessage(hControlUnderMouse, m.Msg, m.WParam, m.LParam);
                     return true;
                  }
                  else
                  {
                     return false;
                  }
               }
            default:
               return false;
         }
      }

      private const int WM_MOUSEWHEEL = 0x20a;
      private const int WM_MOUSEHWHEEL = 0x20e;

      [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = false)]
      private static extern IntPtr SendMessage(
         IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

      [System.Runtime.InteropServices.DllImport("user32.dll")]
      static extern IntPtr WindowFromPoint(POINT Point);

      [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
      private struct POINT
      {
         public int X;
         public int Y;

         public POINT(int x, int y)
         {
            this.X = x;
            this.Y = y;
         }

         public static implicit operator System.Drawing.Point(POINT p)
         {
            return new System.Drawing.Point(p.X, p.Y);
         }

         public static implicit operator POINT(System.Drawing.Point p)
         {
            return new POINT(p.X, p.Y);
         }
      }
   }
}
