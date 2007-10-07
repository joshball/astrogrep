using System;
using Gtk;

namespace AstroGrep.Linux
{
	/// <summary>
	/// 
	/// </summary>
	public class Program
	{
      /// <summary>
      /// 
      /// </summary>
		public static void Main()
      {
         try
         {
            Application.Init();
            new Linux.Forms.frmMain();
            Application.Run();
         }
         catch (Exception ex)
         {
            System.Console.WriteLine(ex.ToString());
         }
      }
	}
}
