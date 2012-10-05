using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AstroGrep.Windows
{
	/// <summary>
	/// Used to manage file icons for display in a ListView.
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
	/// 
	/// NOTE: this class also contains the following terms of use:
	/// *************************************************
	/// * Programmer: Narb M (webmaster@narbware.com)
	/// * Date: 11/30/04
	/// * Company Info: www.Nwarbware.com
	/// * *************************************************
	/// * 
	/// * TERMS OF USE:
	/// * -------------
	/// * You may freely include and compile and modify this class in your applications with 
	/// * the following limitations:
	/// * 
	/// * - The TERMS OF USE section of this file must remain unmodified in the source.
	/// * - This source or modified versions of this source cannot be sold for profit or claimed your own.
	/// * - Use of this code or any part for distrabution must include the TERMS OF USE in the source.
	/// * - Narbware cannot be held responsible for any damage resulting from this source.
	/// * - Any questions about the source or terms of use can be directed to: webmaster@narbware.com.
	/// </remarks>
	/// <history>
   /// [Curtis_Beard]		03/06/2012	ADD: listview image for file type
	/// </history>
	public sealed class ListViewImageManager
	{
		#region Declarations
		private static Hashtable __ExtensionTable = new Hashtable();
		private enum IconSize { Large, Small, Shell };

		/// <summary>
		/// API Constants
		/// </summary>
		private const uint SHGFI_ICON = 0x100;	// get icon
		private const uint SHGFI_LINKOVERLAY = 0x8000;	// put a link overlay on icon
		private const uint SHGFI_SELECTED = 0x10000;	// show icon in selected state
		private const uint SHGFI_LARGEICON = 0x0;		// get large icon
		private const uint SHGFI_SMALLICON = 0x1;		// get small icon
		private const uint SHGFI_OPENICON = 0x2;		// get open icon
		private const uint SHGFI_SHELLICONSIZE = 0x4;		// get shell size icon
		private const uint SHGFI_USEFILEATTRIBUTES = 0x10;		// use passed dwFileAttribute
		private const uint SHGFI_TYPENAME = 0x400;	// get file type name

		/// <summary>
		/// A SHFILEINFO structure used to extract the icon resource of a file or folder.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		private struct SHFILEINFO
		{
			public IntPtr hIcon;
			public IntPtr iIcon;
			public uint dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		};

		[DllImport("shell32.dll")]
		private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
		[DllImport("user32.dll")]
		private static extern IntPtr DestroyIcon(IntPtr hIcon);
		#endregion

		/// <summary>
		/// Retrieve the image index in the given ImageList of the given file.
		/// </summary>
		/// <param name="file">FileInfo to get image for</param>
		/// <param name="list">ImageList to hold image, get index from</param>
		/// <returns>Index of image in image list for the given file</returns>
		/// <history>
      /// [Curtis_Beard]		03/06/2012	ADD: listview image for file type
		/// </history>
		static public int GetImageIndex(FileInfo file, ImageList list)
		{
			string ext = file.Extension.ToLower();

			switch (ext)
			{
				case ".ico":
				case ".exe":
					ext = file.Name.ToLower();
					break;
			}
			
			if (__ExtensionTable.Contains(ext))
			{
				return int.Parse(__ExtensionTable[ext].ToString());
			}
			else
			{
				AddIconOfFile(file.FullName, IconSize.Small, false, false, false, list);
				__ExtensionTable.Add(ext, list.Images.Count - 1);

				return list.Images.Count - 1;
			}
		}

		/// <summary>
		/// Extracts the icon of an existing file or folder on the system and adds the icon to an imagelist.
		/// </summary>
		/// <param name="fileName">A file or folder path to extract the icon from.</param>
		/// <param name="iconSize">The size to extract.</param>
		/// <param name="selectedState">A flag to add the selected state of the icon.</param>
		/// <param name="openState">A flag to add the open state of the icon.</param>
		/// <param name="linkOverlay">A flag to add a shortcut overlay to the icon.</param>
		/// <param name="destinationImagelist">An existing imagelist to add the extracted icon to.</param>
		/// <example>.AddIconOfFile( "C:\\folder\file.exe", IconSize.Small, false, false, false, ImageList );</example>
		/// <history>
      /// [Curtis_Beard]		03/06/2012	ADD: listview image for file type
		/// </history>
		static private void AddIconOfFile(string fileName, IconSize iconSize, bool selectedState, bool openState, bool linkOverlay, ImageList destinationImagelist)
		{
			uint uFlags = ((iconSize == IconSize.Large) ? SHGFI_LARGEICON : 0) |
							((iconSize == IconSize.Small) ? SHGFI_SMALLICON : 0) |
							((iconSize == IconSize.Shell) ? SHGFI_SHELLICONSIZE : 0) |
							((selectedState) ? SHGFI_SELECTED : 0) |
							((openState) ? SHGFI_OPENICON : 0) |
							((linkOverlay) ? SHGFI_LINKOVERLAY : 0);

			Add(fileName, destinationImagelist, uFlags);
		}

		/// <summary>
		/// This method is used to add alphablended system icons to an imagelist using win32 api. 
		/// This an added feature which is not included in the framework.
		/// </summary>
		/// <param name="pszPath">Path of the file to extract icon from.</param>
		/// <param name="il">Imagelist to which the image will be added to.</param>
		/// <param name="uFlags">Options that return the specified icon.</param>
		/// <history>
      /// [Curtis_Beard]		03/06/2012	ADD: listview image for file type
		/// </history>
		static private void Add(string pszPath, ImageList il, uint uFlags)
		{
			SHFILEINFO SHInfo = new SHFILEINFO();

			IntPtr hImg = SHGetFileInfo(pszPath, 0, ref SHInfo, (uint)Marshal.SizeOf(SHInfo), SHGFI_ICON | uFlags);
			il.Images.Add(System.Drawing.Icon.FromHandle(SHInfo.hIcon));
			DestroyIcon(SHInfo.hIcon);
		}
	}
}
