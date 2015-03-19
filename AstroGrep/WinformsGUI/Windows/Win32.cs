using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

using System.Runtime.CompilerServices;

namespace AstroGrep.Windows
{
   /// <summary>
   /// Window's API methods layer.
   /// </summary>
   public class API
   {

      private API() { }

      /// <summary>
      /// Deteremines if current operating system is Vista+.
      /// </summary>
      public static bool IsWindowsVistaOrLater
      {
         get
         {
            return Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version >= new Version(6, 0, 6000);
         }
      }

      /// <summary>
      /// Determines if current operating system is Windows 7+.
      /// </summary>
      public static bool IsWindows7OrLater
      {
         get
         {
            return Environment.OSVersion.Version >= new Version(6, 1);
         }
      }

      #region File Size Display

      /// <summary>
      /// API declaration to display explorer style of file size.
      /// </summary>
      /// <param name="fileSize">File size in bytes</param>
      /// <param name="buffer">buffer to hold value</param>
      /// <param name="bufferSize">buffer size</param>
      /// <returns>explorer style display of file size</returns>
      [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
      private static extern long StrFormatByteSize(
              long fileSize
              , [MarshalAs(UnmanagedType.LPTStr)] System.Text.StringBuilder buffer
              , int bufferSize);


      /// <summary>
      /// Converts a numeric value into a string that represents the number expressed as a size value in bytes, kilobytes, megabytes, or gigabytes, depending on the size.
      /// </summary>
      /// <param name="filesize">The numeric value to be converted.</param>
      /// <returns>the converted string</returns>
      public static string StrFormatByteSize(long filesize)
      {
         System.Text.StringBuilder sb = new System.Text.StringBuilder(11);
         StrFormatByteSize(filesize, sb, sb.Capacity);
         return sb.ToString();
      }
      
      /// <summary>
      /// DPI Font scaling sizes.
      /// </summary>
      public enum DPIFontScalingSizes
      {
         /// <summary>Normal 100% font scaling</summary>
         Normal = 0,
         /// <summary>Medium 125% font scaling</summary>
         Medium = 1,
         /// <summary>Large 150% font scaling</summary>
         Large = 2,
         /// <summary>Other font scaling values</summary>
         Other
      }

      /// <summary>
      /// Gets the current Window's DPI setting.
      /// </summary>
      /// <param name="g">Current graphics context</param>
      /// <returns>DPIFontScalingSize value</returns>
      /// <remarks>We don't close the Graphics parameter here and rely on calling method to handle it since it could be used later.</remarks>
      /// <history>
      /// [Curtis_Beard]	   03/02/2015	FIX: 49, graphical glitch when using 125% dpi setting
      /// </history>
      public static DPIFontScalingSizes GetCurrentDPIFontScalingSize(Graphics g)
      {
         DPIFontScalingSizes size = DPIFontScalingSizes.Normal;

         float dpiX = g.DpiX;

         if (dpiX == 96.0)
         {
            size = DPIFontScalingSizes.Normal;
         }
         if (dpiX == 120.0)
         {
            size = DPIFontScalingSizes.Medium;
         }
         else if (dpiX == 144.0)
         {
            size = DPIFontScalingSizes.Large;
         }
         else 
         {
            size = DPIFontScalingSizes.Other;
         }

         return size;
      }

      #endregion

      #region ShellLink Object
      /// <summary>
      /// Summary description for ShellLink.
      /// </summary>
      public class ShellLink : IDisposable
      {
         #region ComInterop for IShellLink

         #region IPersist Interface
         [ComImportAttribute()]
         [GuidAttribute("0000010C-0000-0000-C000-000000000046")]
         [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
         private interface IPersist
         {
            [PreserveSig]
            //[helpstring("Returns the class identifier for the component object")]
            void GetClassID(out Guid pClassID);
         }
         #endregion

         #region IPersistFile Interface
         [ComImportAttribute()]
         [GuidAttribute("0000010B-0000-0000-C000-000000000046")]
         [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
         private interface IPersistFile
         {
            // can't get this to go if I extend IPersist, so put it here:
            [PreserveSig]
            void GetClassID(out Guid pClassID);

            //[helpstring("Checks for changes since last file write")]		
            void IsDirty();

            //[helpstring("Opens the specified file and initializes the object from its contents")]		
            void Load(
               [MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
               uint dwMode);

            //[helpstring("Saves the object into the specified file")]		
            void Save(
               [MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
               [MarshalAs(UnmanagedType.Bool)] bool fRemember);

            //[helpstring("Notifies the object that save is completed")]		
            void SaveCompleted(
               [MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

            //[helpstring("Gets the current name of the file associated with the object")]		
            void GetCurFile(
               [MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
         }
         #endregion

         #region IShellLink Interface
         [ComImportAttribute()]
         [GuidAttribute("000214EE-0000-0000-C000-000000000046")]
         [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
         private interface IShellLinkA
         {
            //[helpstring("Retrieves the path and filename of a shell link object")]
            void GetPath(
               [Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszFile,
               int cchMaxPath,
               ref _WIN32_FIND_DATAA pfd,
               uint fFlags);

            //[helpstring("Retrieves the list of shell link item identifiers")]
            void GetIDList(out IntPtr ppidl);

            //[helpstring("Sets the list of shell link item identifiers")]
            void SetIDList(IntPtr pidl);

            //[helpstring("Retrieves the shell link description string")]
            void GetDescription(
               [Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszFile,
               int cchMaxName);

            //[helpstring("Sets the shell link description string")]
            void SetDescription(
               [MarshalAs(UnmanagedType.LPStr)] string pszName);

            //[helpstring("Retrieves the name of the shell link working directory")]
            void GetWorkingDirectory(
               [Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszDir,
               int cchMaxPath);

            //[helpstring("Sets the name of the shell link working directory")]
            void SetWorkingDirectory(
               [MarshalAs(UnmanagedType.LPStr)] string pszDir);

            //[helpstring("Retrieves the shell link command-line arguments")]
            void GetArguments(
               [Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszArgs,
               int cchMaxPath);

            //[helpstring("Sets the shell link command-line arguments")]
            void SetArguments(
               [MarshalAs(UnmanagedType.LPStr)] string pszArgs);

            //[propget, helpstring("Retrieves or sets the shell link hot key")]
            void GetHotkey(out short pwHotkey);
            //[propput, helpstring("Retrieves or sets the shell link hot key")]
            void SetHotkey(short pwHotkey);

            //[propget, helpstring("Retrieves or sets the shell link show command")]
            void GetShowCmd(out uint piShowCmd);
            //[propput, helpstring("Retrieves or sets the shell link show command")]
            void SetShowCmd(uint piShowCmd);

            //[helpstring("Retrieves the location (path and index) of the shell link icon")]
            void GetIconLocation(
               [Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszIconPath,
               int cchIconPath,
               out int piIcon);

            //[helpstring("Sets the location (path and index) of the shell link icon")]
            void SetIconLocation(
               [MarshalAs(UnmanagedType.LPStr)] string pszIconPath,
               int iIcon);

            //[helpstring("Sets the shell link relative path")]
            void SetRelativePath(
               [MarshalAs(UnmanagedType.LPStr)] string pszPathRel,
               uint dwReserved);

            //[helpstring("Resolves a shell link. The system searches for the shell link object and updates the shell link path and its list of identifiers (if necessary)")]
            void Resolve(
               IntPtr hWnd,
               uint fFlags);

            //[helpstring("Sets the shell link path and filename")]
            void SetPath(
               [MarshalAs(UnmanagedType.LPStr)] string pszFile);
         }


         [ComImportAttribute()]
         [GuidAttribute("000214F9-0000-0000-C000-000000000046")]
         [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
         private interface IShellLinkW
         {
            //[helpstring("Retrieves the path and filename of a shell link object")]
            void GetPath(
               [Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile,
               int cchMaxPath,
               ref _WIN32_FIND_DATAW pfd,
               uint fFlags);

            //[helpstring("Retrieves the list of shell link item identifiers")]
            void GetIDList(out IntPtr ppidl);

            //[helpstring("Sets the list of shell link item identifiers")]
            void SetIDList(IntPtr pidl);

            //[helpstring("Retrieves the shell link description string")]
            void GetDescription(
               [Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile,
               int cchMaxName);

            //[helpstring("Sets the shell link description string")]
            void SetDescription(
               [MarshalAs(UnmanagedType.LPWStr)] string pszName);

            //[helpstring("Retrieves the name of the shell link working directory")]
            void GetWorkingDirectory(
               [Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir,
               int cchMaxPath);

            //[helpstring("Sets the name of the shell link working directory")]
            void SetWorkingDirectory(
               [MarshalAs(UnmanagedType.LPWStr)] string pszDir);

            //[helpstring("Retrieves the shell link command-line arguments")]
            void GetArguments(
               [Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs,
               int cchMaxPath);

            //[helpstring("Sets the shell link command-line arguments")]
            void SetArguments(
               [MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

            //[propget, helpstring("Retrieves or sets the shell link hot key")]
            void GetHotkey(out short pwHotkey);
            //[propput, helpstring("Retrieves or sets the shell link hot key")]
            void SetHotkey(short pwHotkey);

            //[propget, helpstring("Retrieves or sets the shell link show command")]
            void GetShowCmd(out uint piShowCmd);
            //[propput, helpstring("Retrieves or sets the shell link show command")]
            void SetShowCmd(uint piShowCmd);

            //[helpstring("Retrieves the location (path and index) of the shell link icon")]
            void GetIconLocation(
               [Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath,
               int cchIconPath,
               out int piIcon);

            //[helpstring("Sets the location (path and index) of the shell link icon")]
            void SetIconLocation(
               [MarshalAs(UnmanagedType.LPWStr)] string pszIconPath,
               int iIcon);

            //[helpstring("Sets the shell link relative path")]
            void SetRelativePath(
               [MarshalAs(UnmanagedType.LPWStr)] string pszPathRel,
               uint dwReserved);

            //[helpstring("Resolves a shell link. The system searches for the shell link object and updates the shell link path and its list of identifiers (if necessary)")]
            void Resolve(
               IntPtr hWnd,
               uint fFlags);

            //[helpstring("Sets the shell link path and filename")]
            void SetPath(
               [MarshalAs(UnmanagedType.LPWStr)] string pszFile);
         }
         #endregion

         #region ShellLinkCoClass
         [GuidAttribute("00021401-0000-0000-C000-000000000046")]
         [ClassInterfaceAttribute(ClassInterfaceType.None)]
         [ComImportAttribute()]
         private class CShellLink { }

         #endregion

         #region Private IShellLink enumerations
         private enum EShellLinkGP : uint
         {
            SLGP_SHORTPATH = 1,
            SLGP_UNCPRIORITY = 2
         }

         [Flags]
         private enum EShowWindowFlags : uint
         {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_MAX = 10
         }
         #endregion

         #region IShellLink Private structs

         [StructLayoutAttribute(LayoutKind.Sequential, Pack = 4, Size = 0, CharSet = CharSet.Unicode)]
         private struct _WIN32_FIND_DATAW
         {
            public uint dwFileAttributes;
            public _FILETIME ftCreationTime;
            public _FILETIME ftLastAccessTime;
            public _FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] // MAX_PATH
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
         }

         [StructLayoutAttribute(LayoutKind.Sequential, Pack = 4, Size = 0, CharSet = CharSet.Ansi)]
         private struct _WIN32_FIND_DATAA
         {
            public uint dwFileAttributes;
            public _FILETIME ftCreationTime;
            public _FILETIME ftLastAccessTime;
            public _FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] // MAX_PATH
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
         }

         [StructLayoutAttribute(LayoutKind.Sequential, Pack = 4, Size = 0)]
         private struct _FILETIME
         {
            public uint dwLowDateTime;
            public uint dwHighDateTime;
         }
         #endregion

         #region UnManaged Methods
         private class UnManagedMethods
         {
            [DllImport("Shell32", CharSet = CharSet.Auto)]
            internal extern static int ExtractIconEx(
               [MarshalAs(UnmanagedType.LPTStr)] 
				string lpszFile,
               int nIconIndex,
               IntPtr[] phIconLarge,
               IntPtr[] phIconSmall,
               int nIcons);

            [DllImport("user32")]
            internal static extern int DestroyIcon(IntPtr hIcon);
         }
         #endregion

         #endregion

         #region Enumerations
         /// <summary>
         /// Flags determining how the links with missing
         /// targets are resolved.
         /// </summary>
         [Flags]
         public enum EShellLinkResolveFlags : uint
         {
            /// <summary>
            /// Allow any match during resolution.  Has no effect
            /// on ME/2000 or above, use the other flags instead.
            /// </summary>
            SLR_ANY_MATCH = 0x2,
            /// <summary>
            /// Call the Microsoft Windows Installer. 
            /// </summary>
            SLR_INVOKE_MSI = 0x80,
            /// <summary>
            /// Disable distributed link tracking. By default, 
            /// distributed link tracking tracks removable media 
            /// across multiple devices based on the volume name. 
            /// It also uses the UNC path to track remote file 
            /// systems whose drive letter has changed. Setting 
            /// SLR_NOLINKINFO disables both types of tracking.
            /// </summary>
            SLR_NOLINKINFO = 0x40,
            /// <summary>
            /// Do not display a dialog box if the link cannot be resolved. 
            /// When SLR_NO_UI is set, a time-out value that specifies the 
            /// maximum amount of time to be spent resolving the link can 
            /// be specified in milliseconds. The function returns if the 
            /// link cannot be resolved within the time-out duration. 
            /// If the timeout is not set, the time-out duration will be 
            /// set to the default value of 3,000 milliseconds (3 seconds). 
            /// </summary>										    
            SLR_NO_UI = 0x1,
            /// <summary>
            /// Not documented in SDK.  Assume same as SLR_NO_UI but 
            /// intended for applications without a hWnd.
            /// </summary>
            SLR_NO_UI_WITH_MSG_PUMP = 0x101,
            /// <summary>
            /// Do not update the link information. 
            /// </summary>
            SLR_NOUPDATE = 0x8,
            /// <summary>
            /// Do not execute the search heuristics. 
            /// </summary>																																																																																																																																																																																																														
            SLR_NOSEARCH = 0x10,
            /// <summary>
            /// Do not use distributed link tracking. 
            /// </summary>
            SLR_NOTRACK = 0x20,
            /// <summary>
            /// If the link object has changed, update its path and list 
            /// of identifiers. If SLR_UPDATE is set, you do not need to 
            /// call IPersistFile::IsDirty to determine whether or not 
            /// the link object has changed. 
            /// </summary>
            SLR_UPDATE = 0x4
         }

         /// <summary>
         /// 
         /// </summary>
         public enum LinkDisplayMode : uint
         {
            /// <summary></summary>
            edmNormal = EShowWindowFlags.SW_NORMAL,
            /// <summary></summary>
            edmMinimized = EShowWindowFlags.SW_SHOWMINNOACTIVE,
            /// <summary></summary>
            edmMaximized = EShowWindowFlags.SW_MAXIMIZE
         }
         #endregion

         #region Member Variables
         // Use Unicode (W) under NT, otherwise use ANSI		
         private IShellLinkW linkW;
         private IShellLinkA linkA;
         private string shortcutFile = "";
         #endregion

         #region Constructor
         /// <summary>
         /// Creates an instance of the Shell Link object.
         /// </summary>
         public ShellLink()
         {
            if (System.Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
               linkW = (IShellLinkW)new CShellLink();
            }
            else
            {
               linkA = (IShellLinkA)new CShellLink();
            }
         }

         /// <summary>
         /// Creates an instance of a Shell Link object
         /// from the specified link file
         /// </summary>
         /// <param name="linkFile">The Shortcut file to open</param>
         public ShellLink(string linkFile)
            : this()
         {
            Open(linkFile);
         }
         #endregion

         #region Destructor and Dispose
         /// <summary>
         /// Call dispose just in case it hasn't happened yet
         /// </summary>
         ~ShellLink()
         {
            Dispose();
         }

         /// <summary>
         /// Dispose the object, releasing the COM ShellLink object
         /// </summary>
         public void Dispose()
         {
            if (linkW != null)
            {
               Marshal.ReleaseComObject(linkW);
               linkW = null;
            }
            if (linkA != null)
            {
               Marshal.ReleaseComObject(linkA);
               linkA = null;
            }
         }
         #endregion

         #region Implementation
         /// <summary>
         /// 
         /// </summary>
         public string ShortCutFile
         {
            get
            {
               return this.shortcutFile;
            }
            set
            {
               this.shortcutFile = value;
            }
         }

         /// <summary>
         /// Gets a System.Drawing.Icon containing the icon for this
         /// ShellLink object.
         /// </summary>
         public Icon LargeIcon
         {
            get
            {
               return getIcon(true);
            }
         }

         /// <summary>
         /// 
         /// </summary>
         public Icon SmallIcon
         {
            get
            {
               return getIcon(false);
            }
         }

         private Icon getIcon(bool large)
         {
            // Get icon index and path:
            int iconIndex = 0;
            StringBuilder iconPath = new StringBuilder(260, 260);
            if (linkA == null)
            {
               linkW.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
            }
            else
            {
               linkA.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
            }
            string iconFile = iconPath.ToString();

            // If there are no details set for the icon, then we must use
            // the shell to get the icon for the target:
            if (iconFile.Length == 0)
            {
               // Use the FileIcon object to get the icon:
               FileIcon.SHGetFileInfoConstants flags = FileIcon.SHGetFileInfoConstants.SHGFI_ICON |
                  FileIcon.SHGetFileInfoConstants.SHGFI_ATTRIBUTES;
               if (large)
               {
                  flags = flags | FileIcon.SHGetFileInfoConstants.SHGFI_LARGEICON;
               }
               else
               {
                  flags = flags | FileIcon.SHGetFileInfoConstants.SHGFI_SMALLICON;
               }
               FileIcon fileIcon = new FileIcon(Target, flags);
               return fileIcon.ShellIcon;
            }
            else
            {
               // Use ExtractIconEx to get the icon:
               IntPtr[] hIconEx = new IntPtr[1] { IntPtr.Zero };
               int iconCount = 0;
               if (large)
               {
                  iconCount = UnManagedMethods.ExtractIconEx(
                     iconFile,
                     iconIndex,
                     hIconEx,
                     null,
                     1);
               }
               else
               {
                  iconCount = UnManagedMethods.ExtractIconEx(
                     iconFile,
                     iconIndex,
                     null,
                     hIconEx,
                     1);
               }
               // If success then return as a GDI+ object
               Icon icon = null;
               if (hIconEx[0] != IntPtr.Zero)
               {
                  icon = Icon.FromHandle(hIconEx[0]);
                  //UnManagedMethods.DestroyIcon(hIconEx[0]);
               }
               return icon;
            }
         }

         /// <summary>
         /// Gets the path to the file containing the icon for this shortcut.
         /// </summary>
         public string IconPath
         {
            get
            {
               StringBuilder iconPath = new StringBuilder(260, 260);
               int iconIndex = 0;
               if (linkA == null)
               {
                  linkW.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
               }
               else
               {
                  linkA.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
               }
               return iconPath.ToString();
            }
            set
            {
               StringBuilder iconPath = new StringBuilder(260, 260);
               int iconIndex = 0;
               if (linkA == null)
               {
                  linkW.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
               }
               else
               {
                  linkA.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
               }
               if (linkA == null)
               {
                  linkW.SetIconLocation(value, iconIndex);
               }
               else
               {
                  linkA.SetIconLocation(value, iconIndex);
               }
            }
         }

         /// <summary>
         /// Gets the index of this icon within the icon path's resources
         /// </summary>
         public int IconIndex
         {
            get
            {
               StringBuilder iconPath = new StringBuilder(260, 260);
               int iconIndex = 0;
               if (linkA == null)
               {
                  linkW.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
               }
               else
               {
                  linkA.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
               }
               return iconIndex;
            }
            set
            {
               StringBuilder iconPath = new StringBuilder(260, 260);
               int iconIndex = 0;
               if (linkA == null)
               {
                  linkW.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
               }
               else
               {
                  linkA.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
               }
               if (linkA == null)
               {
                  linkW.SetIconLocation(iconPath.ToString(), value);
               }
               else
               {
                  linkA.SetIconLocation(iconPath.ToString(), value);
               }
            }
         }

         /// <summary>
         /// Gets/sets the fully qualified path to the link's target
         /// </summary>
         public string Target
         {
            get
            {
               StringBuilder target = new StringBuilder(260, 260);
               if (linkA == null)
               {
                  _WIN32_FIND_DATAW fd = new _WIN32_FIND_DATAW();
                  linkW.GetPath(target, target.Capacity, ref fd, (uint)EShellLinkGP.SLGP_UNCPRIORITY);
               }
               else
               {
                  _WIN32_FIND_DATAA fd = new _WIN32_FIND_DATAA();
                  linkA.GetPath(target, target.Capacity, ref fd, (uint)EShellLinkGP.SLGP_UNCPRIORITY);
               }
               return target.ToString();
            }
            set
            {
               if (linkA == null)
               {
                  linkW.SetPath(value);
               }
               else
               {
                  linkA.SetPath(value);
               }
            }
         }

         /// <summary>
         /// Gets/sets the Working Directory for the Link
         /// </summary>
         public string WorkingDirectory
         {
            get
            {
               StringBuilder path = new StringBuilder(260, 260);
               if (linkA == null)
               {
                  linkW.GetWorkingDirectory(path, path.Capacity);
               }
               else
               {
                  linkA.GetWorkingDirectory(path, path.Capacity);
               }
               return path.ToString();
            }
            set
            {
               if (linkA == null)
               {
                  linkW.SetWorkingDirectory(value);
               }
               else
               {
                  linkA.SetWorkingDirectory(value);
               }
            }
         }

         /// <summary>
         /// Gets/sets the description of the link
         /// </summary>
         public string Description
         {
            get
            {
               StringBuilder description = new StringBuilder(1024, 1024);
               if (linkA == null)
               {
                  linkW.GetDescription(description, description.Capacity);
               }
               else
               {
                  linkA.GetDescription(description, description.Capacity);
               }
               return description.ToString();
            }
            set
            {
               if (linkA == null)
               {
                  linkW.SetDescription(value);
               }
               else
               {
                  linkA.SetDescription(value);
               }
            }
         }

         /// <summary>
         /// Gets/sets any command line arguments associated with the link
         /// </summary>
         public string Arguments
         {
            get
            {
               StringBuilder arguments = new StringBuilder(260, 260);
               if (linkA == null)
               {
                  linkW.GetArguments(arguments, arguments.Capacity);
               }
               else
               {
                  linkA.GetArguments(arguments, arguments.Capacity);
               }
               return arguments.ToString();
            }
            set
            {
               if (linkA == null)
               {
                  linkW.SetArguments(value);
               }
               else
               {
                  linkA.SetArguments(value);
               }
            }
         }

         /// <summary>
         /// Gets/sets the initial display mode when the shortcut is
         /// run
         /// </summary>
         public LinkDisplayMode DisplayMode
         {
            get
            {
               uint cmd = 0;
               if (linkA == null)
               {
                  linkW.GetShowCmd(out cmd);
               }
               else
               {
                  linkA.GetShowCmd(out cmd);
               }
               return (LinkDisplayMode)cmd;
            }
            set
            {
               if (linkA == null)
               {
                  linkW.SetShowCmd((uint)value);
               }
               else
               {
                  linkA.SetShowCmd((uint)value);
               }
            }
         }

         /// <summary>
         /// Gets/sets the HotKey to start the shortcut (if any)
         /// </summary>
         public Keys HotKey
         {
            get
            {
               short key = 0;
               if (linkA == null)
               {
                  linkW.GetHotkey(out key);
               }
               else
               {
                  linkA.GetHotkey(out key);
               }
               return (Keys)key;
            }
            set
            {
               if (linkA == null)
               {
                  linkW.SetHotkey((short)value);
               }
               else
               {
                  linkA.SetHotkey((short)value);
               }
            }
         }

         /// <summary>
         /// Saves the shortcut to ShortCutFile.
         /// </summary>
         public void Save()
         {
            Save(shortcutFile);
         }

         /// <summary>
         /// Saves the shortcut to the specified file
         /// </summary>
         /// <param name="linkFile">The shortcut file (.lnk)</param>
         public void Save(
            string linkFile
            )
         {
            // Save the object to disk
            if (linkA == null)
            {
               ((IPersistFile)linkW).Save(linkFile, true);
               shortcutFile = linkFile;
            }
            else
            {
               ((IPersistFile)linkA).Save(linkFile, true);
               shortcutFile = linkFile;
            }
         }

         /// <summary>
         /// Loads a shortcut from the specified file
         /// </summary>
         /// <param name="linkFile">The shortcut file (.lnk) to load</param>
         public void Open(
            string linkFile
            )
         {
            Open(linkFile,
               IntPtr.Zero,
               (EShellLinkResolveFlags.SLR_ANY_MATCH | EShellLinkResolveFlags.SLR_NO_UI),
               1);
         }

         /// <summary>
         /// Loads a shortcut from the specified file, and allows flags controlling
         /// the UI behaviour if the shortcut's target isn't found to be set.
         /// </summary>
         /// <param name="linkFile">The shortcut file (.lnk) to load</param>
         /// <param name="hWnd">The window handle of the application's UI, if any</param>
         /// <param name="resolveFlags">Flags controlling resolution behaviour</param>
         public void Open(
            string linkFile,
            IntPtr hWnd,
            EShellLinkResolveFlags resolveFlags
            )
         {
            Open(linkFile,
               hWnd,
               resolveFlags,
               1);
         }

         /// <summary>
         /// Loads a shortcut from the specified file, and allows flags controlling
         /// the UI behaviour if the shortcut's target isn't found to be set.  If
         /// no SLR_NO_UI is specified, you can also specify a timeout.
         /// </summary>
         /// <param name="linkFile">The shortcut file (.lnk) to load</param>
         /// <param name="hWnd">The window handle of the application's UI, if any</param>
         /// <param name="resolveFlags">Flags controlling resolution behaviour</param>
         /// <param name="timeOut">Timeout if SLR_NO_UI is specified, in ms.</param>
         public void Open(
            string linkFile,
            IntPtr hWnd,
            EShellLinkResolveFlags resolveFlags,
            ushort timeOut
            )
         {
            uint flags;

            if ((resolveFlags & EShellLinkResolveFlags.SLR_NO_UI)
               == EShellLinkResolveFlags.SLR_NO_UI)
            {
               flags = (uint)((int)resolveFlags | (timeOut << 16));
            }
            else
            {
               flags = (uint)resolveFlags;
            }

            if (linkA == null)
            {
               ((IPersistFile)linkW).Load(linkFile, 0); //STGM_DIRECT)
               linkW.Resolve(hWnd, flags);
               this.shortcutFile = linkFile;
            }
            else
            {
               ((IPersistFile)linkA).Load(linkFile, 0); //STGM_DIRECT)
               linkA.Resolve(hWnd, flags);
               this.shortcutFile = linkFile;
            }
         }
         #endregion
      }
      #endregion

      #region FileIcon
      /// <summary>
      /// Enables extraction of icons for any file type from
      /// the Shell.
      /// </summary>
      public class FileIcon
      {

         #region UnmanagedCode
         private const int MAX_PATH = 260;

         [StructLayout(LayoutKind.Sequential)]
         private struct SHFILEINFO
         {
            public IntPtr hIcon;
            public int iIcon;
            public int dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
         }

         [DllImport("shell32")]
         private static extern int SHGetFileInfo(
            string pszPath,
            int dwFileAttributes,
            ref SHFILEINFO psfi,
            uint cbFileInfo,
            uint uFlags);

         [DllImport("user32.dll")]
         private static extern int DestroyIcon(IntPtr hIcon);

         private const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x100;
         private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x2000;
         private const int FORMAT_MESSAGE_FROM_HMODULE = 0x800;
         private const int FORMAT_MESSAGE_FROM_STRING = 0x400;
         private const int FORMAT_MESSAGE_FROM_SYSTEM = 0x1000;
         private const int FORMAT_MESSAGE_IGNORE_INSERTS = 0x200;
         private const int FORMAT_MESSAGE_MAX_WIDTH_MASK = 0xFF;
         [DllImport("kernel32")]
         private extern static int FormatMessage(
            int dwFlags,
            IntPtr lpSource,
            int dwMessageId,
            int dwLanguageId,
            string lpBuffer,
            uint nSize,
            int argumentsLong);

         [DllImport("kernel32")]
         private extern static int GetLastError();
         #endregion

         #region Member Variables
         private string fileName;
         private string displayName;
         private string typeName;
         private SHGetFileInfoConstants flags;
         private Icon fileIcon;
         #endregion

         #region Enumerations
         /// <summary>
         /// 
         /// </summary>
         [Flags]
         public enum SHGetFileInfoConstants : int
         {
            /// <summary>get icon</summary>
            SHGFI_ICON = 0x100,
            /// <summary>get display name</summary>
            SHGFI_DISPLAYNAME = 0x200,
            /// <summary>get type name</summary>
            SHGFI_TYPENAME = 0x400,
            /// <summary>get attributes</summary>
            SHGFI_ATTRIBUTES = 0x800,
            /// <summary>get icon location </summary>
            SHGFI_ICONLOCATION = 0x1000,
            /// <summary>return exe type </summary>
            SHGFI_EXETYPE = 0x2000,
            /// <summary>get system icon index </summary>
            SHGFI_SYSICONINDEX = 0x4000,
            /// <summary>put a link overlay on icon </summary>
            SHGFI_LINKOVERLAY = 0x8000,
            /// <summary>show icon in selected state </summary>
            SHGFI_SELECTED = 0x10000,
            /// <summary>get only specified attributes </summary>
            SHGFI_ATTR_SPECIFIED = 0x20000,
            /// <summary>get large icon </summary>
            SHGFI_LARGEICON = 0x0,
            /// <summary>get small icon </summary>
            SHGFI_SMALLICON = 0x1,
            /// <summary>get open icon </summary>
            SHGFI_OPENICON = 0x2,
            /// <summary>get shell size icon </summary>
            SHGFI_SHELLICONSIZE = 0x4,
            /// <summary>use passed dwFileAttribute</summary>
            //SHGFI_PIDL = 0x8,                  // pszPath is a pidl 
            SHGFI_USEFILEATTRIBUTES = 0x10,
            /// <summary>apply the appropriate overlays</summary>
            SHGFI_ADDOVERLAYS = 0x000000020,
            /// <summary>Get the index of the overlay</summary>
            SHGFI_OVERLAYINDEX = 0x000000040
         }
         #endregion

         #region Implementation
         /// <summary>
         /// Gets/sets the flags used to extract the icon
         /// </summary>
         public FileIcon.SHGetFileInfoConstants Flags
         {
            get
            {
               return flags;
            }
            set
            {
               flags = value;
            }
         }

         /// <summary>
         /// Gets/sets the filename to get the icon for
         /// </summary>
         public string FileName
         {
            get
            {
               return fileName;
            }
            set
            {
               fileName = value;
            }
         }

         /// <summary>
         /// Gets the icon for the chosen file
         /// </summary>
         public Icon ShellIcon
         {
            get
            {
               return fileIcon;
            }
         }

         /// <summary>
         /// Gets the display name for the selected file
         /// if the SHGFI_DISPLAYNAME flag was set.
         /// </summary>
         public string DisplayName
         {
            get
            {
               return displayName;
            }
         }

         /// <summary>
         /// Gets the type name for the selected file
         /// if the SHGFI_TYPENAME flag was set.
         /// </summary>
         public string TypeName
         {
            get
            {
               return typeName;
            }
         }

         /// <summary>
         ///  Gets the information for the specified 
         ///  file name and flags.
         /// </summary>
         public void GetInfo()
         {
            fileIcon = null;
            typeName = "";
            displayName = "";

            SHFILEINFO shfi = new SHFILEINFO();
            uint shfiSize = (uint)Marshal.SizeOf(shfi.GetType());

            int ret = SHGetFileInfo(
               fileName, 0, ref shfi, shfiSize, (uint)(flags));
            if (ret != 0)
            {
               if (shfi.hIcon != IntPtr.Zero)
               {
                  fileIcon = System.Drawing.Icon.FromHandle(shfi.hIcon);
                  // Now owned by the GDI+ object
                  //DestroyIcon(shfi.hIcon);
               }
               typeName = shfi.szTypeName;
               displayName = shfi.szDisplayName;
            }
            else
            {

               int err = GetLastError();
               Console.WriteLine("Error {0}", err);
               string txtS = new string('\0', 256);
               int len = FormatMessage(
                  FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
                  IntPtr.Zero, err, 0, txtS, 256, 0);
               Console.WriteLine("Len {0} text {1}", len, txtS);

               // throw exception

            }
         }

         /// <summary>
         /// Constructs a new, default instance of the FileIcon
         /// class.  Specify the filename and call GetInfo()
         /// to retrieve an icon.
         /// </summary>
         public FileIcon()
         {
            flags = SHGetFileInfoConstants.SHGFI_ICON |
               SHGetFileInfoConstants.SHGFI_DISPLAYNAME |
               SHGetFileInfoConstants.SHGFI_TYPENAME |
               SHGetFileInfoConstants.SHGFI_ATTRIBUTES |
               SHGetFileInfoConstants.SHGFI_EXETYPE;
         }
         /// <summary>
         /// Constructs a new instance of the FileIcon class
         /// and retrieves the icon, display name and type name
         /// for the specified file.		
         /// </summary>
         /// <param name="fileName">The filename to get the icon, 
         /// display name and type name for</param>
         public FileIcon(string fileName)
            : this()
         {
            this.fileName = fileName;
            GetInfo();
         }
         /// <summary>
         /// Constructs a new instance of the FileIcon class
         /// and retrieves the information specified in the 
         /// flags.
         /// </summary>
         /// <param name="fileName">The filename to get information
         /// for</param>
         /// <param name="flags">The flags to use when extracting the
         /// icon and other shell information.</param>
         public FileIcon(string fileName, FileIcon.SHGetFileInfoConstants flags)
         {
            this.fileName = fileName;
            this.flags = flags;
            GetInfo();
         }

         #endregion
      }
      #endregion

      #region UAC Helper

      /// <summary>
      /// Helper class for UAC based functions.
      /// </summary>
      public class UACHelper
      {
         [DllImport("user32")]
         private static extern UInt32 SendMessage
             (IntPtr hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);

         internal const int BCM_FIRST = 0x1600; //Normal button
         internal const int BCM_SETSHIELD = (BCM_FIRST + 0x000C); //Elevated button

         /// <summary>
         /// Add the UAC shield to the given Button.
         /// </summary>
         /// <param name="b">Button to add shield</param>
         public static void AddShieldToButton(Button b)
         {
            if (IsWindowsVistaOrLater)// System.Environment.OSVersion.Version.Major >= 6)
            {
               b.FlatStyle = FlatStyle.System;
               SendMessage(b.Handle, BCM_SETSHIELD, 0, 0xFFFFFFFF);
            }
         }

         /// <summary>
         /// Removes the UAC shield from the given Button.
         /// </summary>
         /// <param name="b">Button to remove shield</param>
         public static void RemoveShieldFromButton(Button b)
         {
            if (IsWindowsVistaOrLater)// System.Environment.OSVersion.Version.Major >= 6)
            {
               b.FlatStyle = FlatStyle.System;
               SendMessage(b.Handle, BCM_SETSHIELD, 0, 0x0);
            }
         }

         /// <summary>
         /// Determines if current user has admin privileges.
         /// </summary>
         /// <returns>true if does, false if not.</returns>
         public static bool HasAdminPrivileges()
         {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal p = new WindowsPrincipal(id);
            return p.IsInRole(WindowsBuiltInRole.Administrator);
         }

         /// <summary>
         /// Attempts to run the given process as an admim process.
         /// </summary>
         /// <param name="path">Full path to process</param>
         /// <param name="args">Arguments to process</param>
         /// <param name="runas">true will use runas verb, false assumes manifest is part of process</param>
         public static void AttemptPrivilegeEscalation(string path, string args, bool runas)
         {
            if (String.IsNullOrEmpty(path))
               throw new ArgumentNullException("path");

            if (!File.Exists(path))
               throw new FileNotFoundException("path");

            // commented out so that we can call it no matter what
            //if (HasAdminPrivileges())
            //   throw new SecurityException("Already have administrator privileges.");


            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.FileName = path;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            // only do this for Vista+ since xp has an older runas dialog
            // also, runas set to false will assume that the application has a manifest and so we don't need this
            if (IsWindowsVistaOrLater && runas)// System.Environment.OSVersion.Version.Major >= 6 && runas)
            {
               startInfo.Verb = "runas"; // will bring up the UAC run-as menu when this ProcessStartInfo is used
            }

            if (!string.IsNullOrEmpty(args))
            {
               startInfo.Arguments = args;
            }

            // Make the UAC dialog modal to the app
            //if (modal)
            //{
            //    startInfo.ErrorDialog = true;
            //    startInfo.ErrorDialogParentHandle = mainForm.Handle;
            //}

            try
            {
               Process p = Process.Start(startInfo);

               // block this UI until the launched process exits
               // I.e. make it modal
               //if (modal)
               //{
               //    p.WaitForExit();
               //}
            }

            catch (System.ComponentModel.Win32Exception) //occurs when the user has clicked Cancel on the UAC prompt.
            {
               return; // By returning, we are ignoring the user tried to get UAC priviliges but then hit cancel at the "Run-As" prompt.
            }
         }
      }

      #endregion

      #region ListViewExtensions

      /// <summary>
      /// Extension methods for a ListView control.
      /// </summary>
      [EditorBrowsable(EditorBrowsableState.Never)]
      public static class ListViewExtensions
      {
         [StructLayout(LayoutKind.Sequential)]
         private struct HDITEM
         {
            public Mask mask;
            public int cxy;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pszText;
            public IntPtr hbm;
            public int cchTextMax;
            public Format fmt;
            public IntPtr lParam;
            // _WIN32_IE >= 0x0300 
            public int iImage;
            public int iOrder;
            // _WIN32_IE >= 0x0500
            public uint type;
            public IntPtr pvFilter;
            // _WIN32_WINNT >= 0x0600
            public uint state;

            [Flags]
            public enum Mask
            {
               Format = 0x4,       // HDI_FORMAT
            };

            [Flags]
            public enum Format
            {
               SortDown = 0x200,   // HDF_SORTDOWN
               SortUp = 0x400,     // HDF_SORTUP
            };
         };

         private const int LVM_FIRST = 0x1000;
         private const int LVM_GETHEADER = LVM_FIRST + 31;
         private const int LVM_SETSELECTEDCOLUMN = LVM_FIRST + 140;
         private const int LVM_SETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 54;
         private const int LVS_EX_DOUBLEBUFFER = 0x00010000;

         private const int HDM_FIRST = 0x1200;
         private const int HDM_GETITEM = HDM_FIRST + 11;
         private const int HDM_SETITEM = HDM_FIRST + 12;

         [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
         private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

         [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
         private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, ref HDITEM lParam);

         [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
         private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

         /// <summary>
         /// Sets the given column to show the current sort direction.
         /// </summary>
         /// <param name="listViewControl">ListView control</param>
         /// <param name="columnIndex">Selected column</param>
         /// <param name="order">Selected sort order</param>
         public static void SetSortIcon(ListView listViewControl, int columnIndex, SortOrder order)
         {
            IntPtr columnHeader = SendMessage(listViewControl.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);
            for (int columnNumber = 0; columnNumber <= listViewControl.Columns.Count - 1; columnNumber++)
            {
               var columnPtr = new IntPtr(columnNumber);
               var item = new HDITEM
               {
                  mask = HDITEM.Mask.Format
               };

               if (SendMessage(columnHeader, HDM_GETITEM, columnPtr, ref item) == IntPtr.Zero)
               {
                  throw new Win32Exception();
               }

               if (order != SortOrder.None && columnNumber == columnIndex)
               {
                  switch (order)
                  {
                     case SortOrder.Ascending:
                        item.fmt &= ~HDITEM.Format.SortDown;
                        item.fmt |= HDITEM.Format.SortUp;
                        break;
                     case SortOrder.Descending:
                        item.fmt &= ~HDITEM.Format.SortUp;
                        item.fmt |= HDITEM.Format.SortDown;
                        break;
                  }

                  // only display selected column on XP
                  if (System.Environment.OSVersion.Version.Major < 6)
                  {
                     SendMessage(listViewControl.Handle, LVM_SETSELECTEDCOLUMN, columnPtr, IntPtr.Zero);
                  }
               }
               else
               {
                  item.fmt &= ~HDITEM.Format.SortDown & ~HDITEM.Format.SortUp;
               }

               if (SendMessage(columnHeader, HDM_SETITEM, columnPtr, ref item) == IntPtr.Zero)
               {
                  throw new Win32Exception();
               }
            }
         }

         /// <summary>
         /// Uses the current OS theme for the list view (row highlight, hover, columns, etc).
         /// </summary>
         /// <param name="listViewControl">ListView control</param>
         public static void SetTheme(ListView listViewControl)
         {
            SetWindowTheme(listViewControl.Handle, "Explorer", null);
            SendMessage(listViewControl.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, new IntPtr(LVS_EX_DOUBLEBUFFER), new IntPtr(LVS_EX_DOUBLEBUFFER));
         }
      }
      #endregion

      #region File Deletion
      /// <summary>
      /// Helper class to delete a file via the recycle bin.
      /// </summary>
      public class FileDeletion
      {
         [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
         private struct SHFILEOPSTRUCT
         {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public int wFunc;
            public string pFrom;
            public string pTo;
            public short fFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
         }

         [DllImport("shell32.dll", CharSet = CharSet.Auto)]
         private static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);

         private const int FO_DELETE = 3;
         private const int FOF_ALLOWUNDO = 0x40;
         private const int FOF_NOCONFIRMATION = 0x10;    //Don't prompt the user.;

         /// <summary>
         /// Do not show a dialog during the process
         /// </summary>
         private const int FOF_SILENT = 0x0004;
         ///// <summary>
         ///// Do not ask the user to confirm selection
         ///// </summary>
         //private const int FOF_NOCONFIRMATION = 0x0010;
         ///// <summary>
         ///// Delete the file to the recycle bin.  (Required flag to send a file to the bin
         ///// </summary>
         //private const int FOF_ALLOWUNDO = 0x0040;
         /// <summary>
         /// Do not show the names of the files or folders that are being recycled.
         /// </summary>
         private const int FOF_SIMPLEPROGRESS = 0x0100;
         /// <summary>
         /// Surpress errors, if any occur during the process.
         /// </summary>
         private const int FOF_NOERRORUI = 0x0400;
         /// <summary>
         /// Warn if files are too big to fit in the recycle bin and will need
         /// to be deleted completely.
         /// </summary>
         private const int FOF_WANTNUKEWARNING = 0x4000;

         /// <summary>
         /// Deletes the file using the recycle bin.
         /// </summary>
         /// <param name="path"></param>
         public static void Delete(string path)
         {
            SHFILEOPSTRUCT shf = new SHFILEOPSTRUCT();
            shf.wFunc = FO_DELETE;
            shf.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION | FOF_NOERRORUI | FOF_SILENT;
            shf.pFrom = path + "\0" + "\0";

            SHFileOperation(ref shf);
         }
      }
      #endregion

      #region Vista FolderBrowseDialog

      /// <summary>
      /// Show a Vista+ style folder browse dialog.
      /// </summary>
      public class VistaFolderBrowseDialogExt
      {

      }

      /// <summary>
      /// Display enumeration
      /// </summary>
      public enum SIGDN : uint
      {
         /// <summary>SHGDN_NORMAL</summary>
         SIGDN_NORMALDISPLAY = 0x00000000,
         /// <summary>SHGDN_INFOLDER | SHGDN_FORPARSING</summary>
         SIGDN_PARENTRELATIVEPARSING = 0x80018001,
         /// <summary>SHGDN_FORPARSING</summary>
         SIGDN_DESKTOPABSOLUTEPARSING = 0x80028000,
         /// <summary>SHGDN_INFOLDER | SHGDN_FOREDITING</summary>
         SIGDN_PARENTRELATIVEEDITING = 0x80031001,
         /// <summary>SHGDN_FORPARSING | SHGDN_FORADDRESSBAR</summary>
         SIGDN_DESKTOPABSOLUTEEDITING = 0x8004c000,
         /// <summary>SHGDN_FORPARSING</summary>
         SIGDN_FILESYSPATH = 0x80058000,
         /// <summary>SHGDN_FORPARSING</summary>
         SIGDN_URL = 0x80068000,
         /// <summary>SHGDN_INFOLDER | SHGDN_FORPARSING | SHGDN_FORADDRESSBAR</summary>
         SIGDN_PARENTRELATIVEFORADDRESSBAR = 0x8007c001,
         /// <summary>SHGDN_INFOLDER</summary>
         SIGDN_PARENTRELATIVE = 0x80080001
      }

      [Flags]
      internal enum FOS : uint
      {
         FOS_OVERWRITEPROMPT = 0x00000002,
         FOS_STRICTFILETYPES = 0x00000004,
         FOS_NOCHANGEDIR = 0x00000008,
         FOS_PICKFOLDERS = 0x00000020,
         FOS_FORCEFILESYSTEM = 0x00000040, // Ensure that items returned are filesystem items.
         FOS_ALLNONSTORAGEITEMS = 0x00000080, // Allow choosing items that have no storage.
         FOS_NOVALIDATE = 0x00000100,
         FOS_ALLOWMULTISELECT = 0x00000200,
         FOS_PATHMUSTEXIST = 0x00000800,
         FOS_FILEMUSTEXIST = 0x00001000,
         FOS_CREATEPROMPT = 0x00002000,
         FOS_SHAREAWARE = 0x00004000,
         FOS_NOREADONLYRETURN = 0x00008000,
         FOS_NOTESTFILECREATE = 0x00010000,
         FOS_HIDEMRUPLACES = 0x00020000,
         FOS_HIDEPINNEDPLACES = 0x00040000,
         FOS_NODEREFERENCELINKS = 0x00100000,
         FOS_DONTADDTORECENT = 0x02000000,
         FOS_FORCESHOWHIDDEN = 0x10000000,
         FOS_DEFAULTNOMINIMODE = 0x20000000
      }

      [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
      internal struct COMDLG_FILTERSPEC
      {
         [MarshalAs(UnmanagedType.LPWStr)]
         internal string pszName;
         [MarshalAs(UnmanagedType.LPWStr)]
         internal string pszSpec;
      }

      internal enum FDAP
      {
         FDAP_BOTTOM = 0x00000000,
         FDAP_TOP = 0x00000001,
      }

      internal static class IIDGuid
      {
         internal const string IModalWindow = "b4db1657-70d7-485e-8e3e-6fcb5a5c1802";
         internal const string IFileDialog = "42f85136-db7e-439c-85f1-e4075d135fc8";
         internal const string IFileDialogEvents = "973510DB-7D7F-452B-8975-74A85828D354";
         internal const string IFileDialogCustomize = "e6fdd21a-163f-4975-9c8c-a69f1ba37034";
         internal const string IFileOpenDialog = "d57c7288-d4ad-4768-be02-9d969532d960";
         internal const string IShellItem = "43826D1E-E718-42EE-BC55-A1E261C37BFE";
         internal const string IShellItemArray = "B63EA76D-1F85-456F-A19C-48159EFA858B";
      }

      internal static class CLSIDGuid
      {
         internal const string FileOpenDialog = "DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7";
      }

      /// <summary>
      /// 
      /// </summary>
      [ComImport,
      Guid(IIDGuid.IShellItem),
      InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
      public interface IShellItem
      {
         // Not supported: IBindCtx
         /// <summary>
         /// 
         /// </summary>
         /// <param name="pbc"></param>
         /// <param name="bhid"></param>
         /// <param name="riid"></param>
         /// <param name="ppv"></param>
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void BindToHandler([In, MarshalAs(UnmanagedType.Interface)] IntPtr pbc, [In] ref Guid bhid, [In] ref Guid riid, out IntPtr ppv);

         /// <summary>
         /// 
         /// </summary>
         /// <param name="ppsi"></param>
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetParent([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

         /// <summary>
         /// 
         /// </summary>
         /// <param name="sigdnName"></param>
         /// <param name="ppszName"></param>
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetDisplayName([In] SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

         /// <summary>
         /// 
         /// </summary>
         /// <param name="sfgaoMask"></param>
         /// <param name="psfgaoAttribs"></param>
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetAttributes([In] uint sfgaoMask, out uint psfgaoAttribs);

         /// <summary>
         /// 
         /// </summary>
         /// <param name="psi"></param>
         /// <param name="hint"></param>
         /// <param name="piOrder"></param>
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void Compare([In, MarshalAs(UnmanagedType.Interface)] IShellItem psi, [In] uint hint, out int piOrder);
      }

      [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
      private static extern int SHCreateItemFromParsingName([MarshalAs(UnmanagedType.LPWStr)] string pszPath, IntPtr pbc, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppv);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="path"></param>
      /// <returns></returns> 
      public static IShellItem CreateItemFromParsingName(string path)
      {
         object item;
         //Guid guid = new Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe"); // IID_IShellItem
         Guid guid = new Guid(IIDGuid.IShellItem); // IID_IShellItem
         int hr = SHCreateItemFromParsingName(path, IntPtr.Zero, ref guid, out item);
         if (hr != 0)
            throw new System.ComponentModel.Win32Exception(hr);

         return (IShellItem)item;
      }

      internal enum HRESULT : long
      {
         S_FALSE = 0x0001,
         S_OK = 0x0000,
         E_INVALIDARG = 0x80070057,
         E_OUTOFMEMORY = 0x8007000E,
         ERROR_CANCELLED = 0x800704C7
      }

      internal enum FDE_SHAREVIOLATION_RESPONSE
      {
         FDESVR_DEFAULT = 0x00000000,
         FDESVR_ACCEPT = 0x00000001,
         FDESVR_REFUSE = 0x00000002
      }

      internal enum FDE_OVERWRITE_RESPONSE
      {
         FDEOR_DEFAULT = 0x00000000,
         FDEOR_ACCEPT = 0x00000001,
         FDEOR_REFUSE = 0x00000002
      }

      [ComImport,
      Guid(IIDGuid.IFileDialogEvents),
      InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
      internal interface IFileDialogEvents
      {
         // NOTE: some of these callbacks are cancelable - returning S_FALSE means that 
         // the dialog should not proceed (e.g. with closing, changing folder); to 
         // support this, we need to use the PreserveSig attribute to enable us to return
         // the proper HRESULT
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
         PreserveSig]
         HRESULT OnFileOk([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
         PreserveSig]
         HRESULT OnFolderChanging([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd, [In, MarshalAs(UnmanagedType.Interface)] IShellItem psiFolder);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void OnFolderChange([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void OnSelectionChange([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void OnShareViolation([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd, [In, MarshalAs(UnmanagedType.Interface)] IShellItem psi, out FDE_SHAREVIOLATION_RESPONSE pResponse);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void OnTypeChange([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void OnOverwrite([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd, [In, MarshalAs(UnmanagedType.Interface)] IShellItem psi, out FDE_OVERWRITE_RESPONSE pResponse);
      }

      [ComImport(),
      Guid(IIDGuid.IModalWindow),
      InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
      internal interface IModalWindow
      {
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
         PreserveSig]
         int Show([In] IntPtr parent);
      }

      [ComImport(),
      Guid(IIDGuid.IFileDialog),
      InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
      internal interface IFileDialog : IModalWindow
      {
         // Defined on IModalWindow - repeated here due to requirements of COM interop layer
         // --------------------------------------------------------------------------------
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
         PreserveSig]
         int Show([In] IntPtr parent);

         // IFileDialog-Specific interface members
         // --------------------------------------------------------------------------------
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetFileTypes([In] uint cFileTypes, [In, MarshalAs(UnmanagedType.LPArray)] COMDLG_FILTERSPEC[] rgFilterSpec);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetFileTypeIndex([In] uint iFileType);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetFileTypeIndex(out uint piFileType);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void Advise([In, MarshalAs(UnmanagedType.Interface)] IFileDialogEvents pfde, out uint pdwCookie);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void Unadvise([In] uint dwCookie);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetOptions([In] FOS fos);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetOptions(out FOS pfos);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetDefaultFolder([In, MarshalAs(UnmanagedType.Interface)] IShellItem psi);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetFolder([In, MarshalAs(UnmanagedType.Interface)] IShellItem psi);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetFolder([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetCurrentSelection([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetFileName([In, MarshalAs(UnmanagedType.LPWStr)] string pszName);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetTitle([In, MarshalAs(UnmanagedType.LPWStr)] string pszTitle);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetOkButtonLabel([In, MarshalAs(UnmanagedType.LPWStr)] string pszText);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetFileNameLabel([In, MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetResult([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void AddPlace([In, MarshalAs(UnmanagedType.Interface)] IShellItem psi, FDAP fdap);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetDefaultExtension([In, MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void Close([MarshalAs(UnmanagedType.Error)] int hr);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetClientGuid([In] ref Guid guid);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void ClearClientData();

         // Not supported:  IShellItemFilter is not defined, converting to IntPtr
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);
      }

      internal enum CDCONTROLSTATE
      {
         CDCS_INACTIVE = 0x00000000,
         CDCS_ENABLED = 0x00000001,
         CDCS_VISIBLE = 0x00000002
      }

      [ComImport,
      Guid(IIDGuid.IFileDialogCustomize),
      InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
      internal interface IFileDialogCustomize
      {
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void EnableOpenDropDown([In] int dwIDCtl);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void AddMenu([In] int dwIDCtl, [In, MarshalAs(UnmanagedType.LPWStr)] string pszLabel);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void AddPushButton([In] int dwIDCtl, [In, MarshalAs(UnmanagedType.LPWStr)] string pszLabel);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void AddComboBox([In] int dwIDCtl);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void AddRadioButtonList([In] int dwIDCtl);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void AddCheckButton([In] int dwIDCtl, [In, MarshalAs(UnmanagedType.LPWStr)] string pszLabel, [In] bool bChecked);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void AddEditBox([In] int dwIDCtl, [In, MarshalAs(UnmanagedType.LPWStr)] string pszText);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void AddSeparator([In] int dwIDCtl);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void AddText([In] int dwIDCtl, [In, MarshalAs(UnmanagedType.LPWStr)] string pszText);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetControlLabel([In] int dwIDCtl, [In, MarshalAs(UnmanagedType.LPWStr)] string pszLabel);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetControlState([In] int dwIDCtl, [Out] out CDCONTROLSTATE pdwState);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetControlState([In] int dwIDCtl, [In] CDCONTROLSTATE dwState);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetEditBoxText([In] int dwIDCtl, [Out] IntPtr ppszText);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetEditBoxText([In] int dwIDCtl, [In, MarshalAs(UnmanagedType.LPWStr)] string pszText);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetCheckButtonState([In] int dwIDCtl, [Out] out bool pbChecked);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetCheckButtonState([In] int dwIDCtl, [In] bool bChecked);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void AddControlItem([In] int dwIDCtl, [In] int dwIDItem, [In, MarshalAs(UnmanagedType.LPWStr)] string pszLabel);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void RemoveControlItem([In] int dwIDCtl, [In] int dwIDItem);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void RemoveAllControlItems([In] int dwIDCtl);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetControlItemState([In] int dwIDCtl, [In] int dwIDItem, [Out] out CDCONTROLSTATE pdwState);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetControlItemState([In] int dwIDCtl, [In] int dwIDItem, [In] CDCONTROLSTATE dwState);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetSelectedControlItem([In] int dwIDCtl, [Out] out int pdwIDItem);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetSelectedControlItem([In] int dwIDCtl, [In] int dwIDItem); // Not valid for OpenDropDown
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void StartVisualGroup([In] int dwIDCtl, [In, MarshalAs(UnmanagedType.LPWStr)] string pszLabel);
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void EndVisualGroup();
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void MakeProminent([In] int dwIDCtl);
      }

      [ComImport(),
      Guid(IIDGuid.IFileOpenDialog),
      InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
      internal interface IFileOpenDialog : IFileDialog
      {
         // Defined on IModalWindow - repeated here due to requirements of COM interop layer
         // --------------------------------------------------------------------------------
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
         PreserveSig]
         int Show([In] IntPtr parent);

         // Defined on IFileDialog - repeated here due to requirements of COM interop layer
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetFileTypes([In] uint cFileTypes, [In] ref COMDLG_FILTERSPEC rgFilterSpec);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetFileTypeIndex([In] uint iFileType);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetFileTypeIndex(out uint piFileType);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void Advise([In, MarshalAs(UnmanagedType.Interface)] IFileDialogEvents pfde, out uint pdwCookie);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void Unadvise([In] uint dwCookie);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetOptions([In] FOS fos);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetOptions(out FOS pfos);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetDefaultFolder([In, MarshalAs(UnmanagedType.Interface)] IShellItem psi);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetFolder([In, MarshalAs(UnmanagedType.Interface)] IShellItem psi);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetFolder([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetCurrentSelection([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetFileName([In, MarshalAs(UnmanagedType.LPWStr)] string pszName);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetTitle([In, MarshalAs(UnmanagedType.LPWStr)] string pszTitle);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetOkButtonLabel([In, MarshalAs(UnmanagedType.LPWStr)] string pszText);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetFileNameLabel([In, MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetResult([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void AddPlace([In, MarshalAs(UnmanagedType.Interface)] IShellItem psi, FDAP fdap);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetDefaultExtension([In, MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void Close([MarshalAs(UnmanagedType.Error)] int hr);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetClientGuid([In] ref Guid guid);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void ClearClientData();

         // Not supported:  IShellItemFilter is not defined, converting to IntPtr
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);

         // Defined by IFileOpenDialog
         // ---------------------------------------------------------------------------------
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetResults([MarshalAs(UnmanagedType.Interface)] out IShellItemArray ppenum);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetSelectedItems([MarshalAs(UnmanagedType.Interface)] out IShellItemArray ppsai);
      }

      [ComImport,
      ClassInterface(ClassInterfaceType.None),
      TypeLibType(TypeLibTypeFlags.FCanCreate),
      Guid(CLSIDGuid.FileOpenDialog)]
      internal class FileOpenDialogRCW
      {
      }

      [ComImport,
      Guid(IIDGuid.IFileOpenDialog),
      CoClass(typeof(FileOpenDialogRCW))]
      internal interface NativeFileOpenDialog : IFileOpenDialog
      {
      }

      [ComImport,
      Guid(IIDGuid.IShellItemArray),
      InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
      internal interface IShellItemArray
      {
         // Not supported: IBindCtx
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void BindToHandler([In, MarshalAs(UnmanagedType.Interface)] IntPtr pbc, [In] ref Guid rbhid, [In] ref Guid riid, out IntPtr ppvOut);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetPropertyStore([In] int Flags, [In] ref Guid riid, out IntPtr ppv);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetPropertyDescriptionList([In] ref PROPERTYKEY keyType, [In] ref Guid riid, out IntPtr ppv);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetAttributes([In] SIATTRIBFLAGS dwAttribFlags, [In] uint sfgaoMask, out uint psfgaoAttribs);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetCount(out uint pdwNumItems);

         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void GetItemAt([In] uint dwIndex, [MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

         // Not supported: IEnumShellItems (will use GetCount and GetItemAt instead)
         [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
         void EnumItems([MarshalAs(UnmanagedType.Interface)] out IntPtr ppenumShellItems);
      }

      internal enum SIATTRIBFLAGS
      {
         SIATTRIBFLAGS_AND = 0x00000001, // if multiple items and the attirbutes together.
         SIATTRIBFLAGS_OR = 0x00000002, // if multiple items or the attributes together.
         SIATTRIBFLAGS_APPCOMPAT = 0x00000003, // Call GetAttributes directly on the ShellFolder for multiple attributes
      }

      // Property System structs and consts
      [StructLayout(LayoutKind.Sequential, Pack = 4)]
      internal struct PROPERTYKEY
      {
         internal Guid fmtid;
         internal uint pid;
      }

      /// <summary>
      /// 
      /// </summary>
      public class WindowHandleWrapper : IWin32Window
      {
         private IntPtr _handle;

         /// <summary>
         /// 
         /// </summary>
         /// <param name="handle"></param>
         public WindowHandleWrapper(IntPtr handle)
         {
            _handle = handle;
         }

         #region IWin32Window Members

         /// <summary>
         /// 
         /// </summary>
         public IntPtr Handle
         {
            get { return _handle; }
         }

         #endregion
      }

      #endregion

      #region Windows 7+ TaskBar Progress

      /// <summary>
      /// Helper class to set taskbar progress on Windows 7+ systems.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]	   03/19/2015	FIX: 67, correct instance declaration when not supported
      /// </history>
      public static class TaskbarProgress
      {
         /// <summary>
         /// Available taskbar progress states
         /// </summary>
         public enum TaskbarStates
         {
            /// <summary>No progress displayed</summary>
            NoProgress = 0,
            /// <summary>Indeterminate </summary>
            Indeterminate = 0x1,
            /// <summary>Normal</summary>
            Normal = 0x2,
            /// <summary>Error</summary>
            Error = 0x4,
            /// <summary>Paused</summary>
            Paused = 0x8
         }

         [ComImportAttribute()]
         [GuidAttribute("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")]
         [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
         private interface ITaskbarList3
         {
            // ITaskbarList
            [PreserveSig]
            void HrInit();
            [PreserveSig]
            void AddTab(IntPtr hwnd);
            [PreserveSig]
            void DeleteTab(IntPtr hwnd);
            [PreserveSig]
            void ActivateTab(IntPtr hwnd);
            [PreserveSig]
            void SetActiveAlt(IntPtr hwnd);

            // ITaskbarList2
            [PreserveSig]
            void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

            // ITaskbarList3
            [PreserveSig]
            void SetProgressValue(IntPtr hwnd, UInt64 ullCompleted, UInt64 ullTotal);
            [PreserveSig]
            void SetProgressState(IntPtr hwnd, TaskbarStates state);
         }

         [GuidAttribute("56FDF344-FD6D-11d0-958A-006097C9A090")]
         [ClassInterfaceAttribute(ClassInterfaceType.None)]
         [ComImportAttribute()]
         private class TaskbarInstance
         {
         }

         private static bool taskbarSupported = IsWindows7OrLater;
         private static ITaskbarList3 taskbarInstance = taskbarSupported ? (ITaskbarList3)new TaskbarInstance() : null;

         /// <summary>
         /// Sets the state of the taskbar progress.
         /// </summary>
         /// <param name="windowHandle">current form handle</param>
         /// <param name="taskbarState">desired state</param>
         public static void SetState(IntPtr windowHandle, TaskbarStates taskbarState)
         {
            if (taskbarSupported)
            {
               taskbarInstance.SetProgressState(windowHandle, taskbarState);
            }
         }

         /// <summary>
         /// Sets the value of the taskbar progress.
         /// </summary>
         /// <param name="windowHandle">currnet form handle</param>
         /// <param name="progressValue">desired progress value</param>
         /// <param name="progressMax">maximum progress value</param>
         public static void SetValue(IntPtr windowHandle, double progressValue, double progressMax)
         {
            if (taskbarSupported)
            {
               taskbarInstance.SetProgressValue(windowHandle, (ulong)progressValue, (ulong)progressMax);
            }
         }
      }

      #endregion
   }
}