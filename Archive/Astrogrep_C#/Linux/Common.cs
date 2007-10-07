using System;
using System.Reflection;
using Gtk;

namespace AstroGrep.Linux
{
	#region Images
   /// <summary>
   /// 
   /// </summary>
   public class Images
   {
      // This class is fully static.
      private Images () { }

      static private Gdk.Pixbuf GetImageFromResource(string name)
      {
         Gdk.Pixbuf pixbuf = null;

         try
         {
            pixbuf = new Gdk.Pixbuf(Assembly.GetExecutingAssembly(), "AstroGrep.Images." + name);
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.ToString());
         }

         return pixbuf;
      }

      static public Gdk.Pixbuf GetPixbuf (string name)
      {
         return GetImageFromResource(name);         
      }

      static public Gdk.Pixbuf GetPixbuf (string name, int maxWidth, int maxHeight)
      {
         Gdk.Pixbuf pixbuf = GetPixbuf (name);
         if (pixbuf == null)
            return null;
			
         double scaleWidth = maxWidth / (double)pixbuf.Width;
         double scaleHeight = maxHeight / (double)pixbuf.Height;

         double s = Math.Min (scaleWidth, scaleHeight);
         if (s >= 1.0)
            return pixbuf;

         int w = (int) Math.Round (s * pixbuf.Width);
         int h = (int) Math.Round (s * pixbuf.Height);
			
         return pixbuf.ScaleSimple (w, h, Gdk.InterpType.Bilinear);
      }

      static public Gtk.Widget GetWidget (string name)
      {
         Gdk.Pixbuf pixbuf = GetPixbuf (name);
         return pixbuf != null ? new Gtk.Image (pixbuf) : null;
      }
		
      static public Gtk.Widget GetWidget (string name, int maxWidth, int maxHeight)
      {
         Gdk.Pixbuf pixbuf = GetPixbuf (name, maxWidth, maxHeight);
         return pixbuf != null ? new Gtk.Image (pixbuf) : null;
      }
   }
   #endregion
   
   #region General
   /// <summary>
   /// 
   /// </summary>
   public class Common
   {
		private Common(){}
		
		public static Gdk.Color ASTROGREP_ORANGE = new Gdk.Color(251, 127, 6);
		
      /// <summary>
      /// 
      /// </summary>
      public static bool IsWindows
      {
         get 
         {
            if (System.IO.Path.DirectorySeparatorChar == '\\')
               return true;
            else
               return false;
         }
      }
      
      #region Conversion Methods
      public static string ConvertColorToString(Gdk.Color color)
      {
         return string.Format("{0}{4}{1}{4}{2}{4}{3}", color.Red.ToString(), color.Green.ToString(), color.Blue.ToString(), color.Pixel.ToString(), Constants.COLOR_SEPARATOR);
      }

      public static Gdk.Color ConvertStringToColor(string color)
      {
         string[] rgba = color.Split(char.Parse(Constants.COLOR_SEPARATOR));
         
         Gdk.Color clr = new Gdk.Color(0,0,0);
         
			clr.Red = ushort.Parse(rgba[0]);
			clr.Green = ushort.Parse(rgba[1]);
			clr.Blue = ushort.Parse(rgba[2]);
			clr.Pixel = uint.Parse(rgba[3]);

         return clr;
      }

      public static string GetComboBoxEntriesAsString(Gtk.ComboBoxEntry combo)
      {
         int index = 0;
         TreeIter iter;
         string[] entries = new string[combo.Model.IterNChildren()];
         
         if (combo.Model.IterChildren(out iter))
         {
            do
            {
               entries[index] = (string) combo.Model.GetValue(iter, 0);
               index++;

            }while (combo.Model.IterNext(ref iter));
         }

         return string.Join(Constants.SEARCH_ENTRIES_SEPARATOR, entries);
      }

      public static string[] GetComboBoxEntriesFromString(string values)
      {
         string[] entries = Core.Common.SplitByString(values, Constants.SEARCH_ENTRIES_SEPARATOR);

         return entries;
      }
      #endregion
   }
   #endregion
}