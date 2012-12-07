using System;
using System.Runtime.InteropServices;
using System.Text;

//
// this code was developed by Dan Letecky
// http://www.codeproject.com/csharp/DesktopSearch1.asp?df=100&forumid=190772&exp=0&fr=26
//
namespace Plugin.IFilter
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum IFILTER_INIT : uint
    {
        /// <summary></summary>
        NONE = 0,
        /// <summary></summary>
        CANON_PARAGRAPHS = 1,
        /// <summary></summary>
        HARD_LINE_BREAKS = 2,
        /// <summary></summary>
        CANON_HYPHENS = 4,
        /// <summary></summary>
        CANON_SPACES = 8,
        /// <summary></summary>
        APPLY_INDEX_ATTRIBUTES = 16,
        /// <summary></summary>
        APPLY_CRAWL_ATTRIBUTES = 256,
        /// <summary></summary>
        APPLY_OTHER_ATTRIBUTES = 32,
        /// <summary></summary>
        INDEXING_ONLY = 64,
        /// <summary></summary>
        SEARCH_LINKS = 128,
        /// <summary></summary>
        FILTER_OWNED_VALUE_OK = 512
    }

    /// <summary>
    /// 
    /// </summary>
    public enum CHUNK_BREAKTYPE
    {
        /// <summary></summary>
        CHUNK_NO_BREAK = 0,
        /// <summary></summary>
        CHUNK_EOW = 1,
        /// <summary></summary>
        CHUNK_EOS = 2,
        /// <summary></summary>
        CHUNK_EOP = 3,
        /// <summary></summary>
        CHUNK_EOC = 4
    }

    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum CHUNKSTATE
    {
        /// <summary></summary>
        CHUNK_TEXT = 0x1,
        /// <summary></summary>
        CHUNK_VALUE = 0x2,
        /// <summary></summary>
        CHUNK_FILTER_OWNED_VALUE = 0x4
    }

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PROPSPEC
    {
        /// <summary></summary>
        public uint ulKind;

        /// <summary></summary>
        public uint propid;

        /// <summary></summary>
        public IntPtr lpwstr;
    }

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FULLPROPSPEC
    {
        /// <summary></summary>
        public Guid guidPropSet;

        /// <summary></summary>
        public PROPSPEC psProperty;
    }

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct STAT_CHUNK
    {
        /// <summary></summary>
        public uint idChunk;

        /// <summary></summary>
        [MarshalAs(UnmanagedType.U4)]
        public CHUNK_BREAKTYPE breakType;

        /// <summary></summary>
        [MarshalAs(UnmanagedType.U4)]
        public CHUNKSTATE flags;

        /// <summary></summary>
        public uint locale;

        /// <summary></summary>
        [MarshalAs(UnmanagedType.Struct)]
        public FULLPROPSPEC attribute;

        /// <summary></summary>
        public uint idChunkSource;

        /// <summary></summary>
        public uint cwcStartSource;

        /// <summary></summary>
        public uint cwcLenSource;
    }

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FILTERREGION
    {
        /// <summary></summary>
        public uint idChunk;

        /// <summary></summary>
        public uint cwcStart;

        /// <summary></summary>
        public uint cwcExtent;
    }

    /// <summary>
    /// 
    /// </summary>
    [ComImport]
    [Guid("89BCB740-6119-101A-BCB7-00DD010655AF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grfFlags"></param>
        /// <param name="cAttributes"></param>
        /// <param name="aAttributes"></param>
        /// <param name="pdwFlags"></param>
        void Init([MarshalAs(UnmanagedType.U4)] IFILTER_INIT grfFlags, uint cAttributes, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] FULLPROPSPEC[] aAttributes, ref uint pdwFlags);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pStat"></param>
        /// <returns></returns>
        [PreserveSig]
        int GetChunk(out STAT_CHUNK pStat);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pcwcBuffer"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        [PreserveSig]
        int GetText(ref uint pcwcBuffer, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder buffer);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ppPropValue"></param>
        void GetValue(ref UIntPtr ppPropValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origPos"></param>
        /// <param name="riid"></param>
        /// <param name="ppunk"></param>
        void BindRegion([MarshalAs(UnmanagedType.Struct)]FILTERREGION origPos, ref Guid riid, ref UIntPtr ppunk);
    }

    /// <summary>
    /// 
    /// </summary>
    [ComImport]
    [Guid("f07f3920-7b8c-11cf-9be8-00aa004b9986")]
    public class CFilter
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class Constants
    {
        /// <summary></summary>
        public const uint PID_STG_DIRECTORY = 0x00000002;
        /// <summary></summary>
        public const uint PID_STG_CLASSID = 0x00000003;
        /// <summary></summary>
        public const uint PID_STG_STORAGETYPE = 0x00000004;
        /// <summary></summary>
        public const uint PID_STG_VOLUME_ID = 0x00000005;
        /// <summary></summary>
        public const uint PID_STG_PARENT_WORKID = 0x00000006;
        /// <summary></summary>
        public const uint PID_STG_SECONDARYSTORE = 0x00000007;
        /// <summary></summary>
        public const uint PID_STG_FILEINDEX = 0x00000008;
        /// <summary></summary>
        public const uint PID_STG_LASTCHANGEUSN = 0x00000009;
        /// <summary></summary>
        public const uint PID_STG_NAME = 0x0000000a;
        /// <summary></summary>
        public const uint PID_STG_PATH = 0x0000000b;
        /// <summary></summary>
        public const uint PID_STG_SIZE = 0x0000000c;
        /// <summary></summary>
        public const uint PID_STG_ATTRIBUTES = 0x0000000d;
        /// <summary></summary>
        public const uint PID_STG_WRITETIME = 0x0000000e;
        /// <summary></summary>
        public const uint PID_STG_CREATETIME = 0x0000000f;
        /// <summary></summary>
        public const uint PID_STG_ACCESSTIME = 0x00000010;
        /// <summary></summary>
        public const uint PID_STG_CHANGETIME = 0x00000011;
        /// <summary></summary>
        public const uint PID_STG_CONTENTS = 0x00000013;
        /// <summary></summary>
        public const uint PID_STG_SHORTNAME = 0x00000014;
        /// <summary></summary>
        public const int FILTER_E_END_OF_CHUNKS = (unchecked((int)0x80041700));
        /// <summary></summary>
        public const int FILTER_E_NO_MORE_TEXT = (unchecked((int)0x80041701));
        /// <summary></summary>
        public const int FILTER_E_NO_MORE_VALUES = (unchecked((int)0x80041702));
        /// <summary></summary>
        public const int FILTER_E_NO_TEXT = (unchecked((int)0x80041705));
        /// <summary></summary>
        public const int FILTER_E_NO_VALUES = (unchecked((int)0x80041706));
        /// <summary></summary>
        public const int FILTER_S_LAST_TEXT = (unchecked((int)0x00041709));
    }

    /// <summary> 
    /// IFilter return codes 
    /// </summary> 
    public enum IFilterReturnCodes : uint
    {
        /// <summary> 
        /// Success 
        /// </summary> 
        S_OK = 0,
        /// <summary> 
        /// The function was denied access to the filter file.  
        /// </summary> 
        E_ACCESSDENIED = 0x80070005,
        /// <summary> 
        /// The function encountered an invalid handle, probably due to a low-memory situation.  
        /// </summary> 
        E_HANDLE = 0x80070006,
        /// <summary> 
        /// The function received an invalid parameter. 
        /// </summary> 
        E_INVALIDARG = 0x80070057,
        /// <summary> 
        /// Out of memory 
        /// </summary> 
        E_OUTOFMEMORY = 0x8007000E,
        /// <summary> 
        /// Not implemented 
        /// </summary> 
        E_NOTIMPL = 0x80004001,
        /// <summary> 
        /// Unknown error 
        /// </summary> 
        E_FAIL = 0x80000008,
        /// <summary> 
        /// File not filtered due to password protection 
        /// </summary> 
        FILTER_E_PASSWORD = 0x8004170B,
        /// <summary> 
        /// The document format is not recognised by the filter 
        /// </summary> 
        FILTER_E_UNKNOWNFORMAT = 0x8004170C,
        /// <summary> 
        /// No text in current chunk 
        /// </summary> 
        FILTER_E_NO_TEXT = 0x80041705,
        /// <summary> 
        /// No more chunks of text available in object 
        /// </summary> 
        FILTER_E_END_OF_CHUNKS = 0x80041700,
        /// <summary> 
        /// No more text available in chunk 
        /// </summary> 
        FILTER_E_NO_MORE_TEXT = 0x80041701,
        /// <summary> 
        /// No more property values available in chunk 
        /// </summary> 
        FILTER_E_NO_MORE_VALUES = 0x80041702,
        /// <summary> 
        /// Unable to access object 
        /// </summary> 
        FILTER_E_ACCESS = 0x80041703,
        /// <summary> 
        /// Moniker doesn't cover entire region 
        /// </summary> 
        FILTER_W_MONIKER_CLIPPED = 0x00041704,
        /// <summary> 
        /// Unable to bind IFilter for embedded object 
        /// </summary> 
        FILTER_E_EMBEDDING_UNAVAILABLE = 0x80041707,
        /// <summary> 
        /// Unable to bind IFilter for linked object 
        /// </summary> 
        FILTER_E_LINK_UNAVAILABLE = 0x80041708,
        /// <summary> 
        /// This is the last text in the current chunk 
        /// </summary> 
        FILTER_S_LAST_TEXT = 0x00041709,
        /// <summary> 
        /// This is the last value in the current chunk 
        /// </summary> 
        FILTER_S_LAST_VALUES = 0x0004170A
    }

    /// <summary>
    /// 
    /// </summary>
    public class Parser
    {
        [DllImport("query.dll", CharSet = CharSet.Unicode)]
        private extern static int LoadIFilter(string pwcsPath, ref IUnknown pUnkOuter, ref IFilter ppIUnk);

        [ComImport, Guid("00000000-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IUnknown
        {
            [PreserveSig]
            IntPtr QueryInterface(ref Guid riid, out IntPtr pVoid);

            [PreserveSig]
            IntPtr AddRef();

            [PreserveSig]
            IntPtr Release();
        }

        private static IFilter loadIFilter(string filename)
        {
            IUnknown iunk = null;
            IFilter filter = null;

            // Try to load the corresponding IFilter 
            int resultLoad = LoadIFilter(filename, ref iunk, ref filter);
            if (resultLoad != (int)IFilterReturnCodes.S_OK)
            {
                return null;
            }
            return filter;
        }

        /// <summary>
        /// Determines if filename has an IFilter.
        /// </summary>
        /// <param name="filename">Current file</param>
        /// <returns>true if IFilter, false if not</returns>
        public static bool IsParseable(string filename)
        {
            return loadIFilter(filename) != null;
        }

        /// <summary>
        /// Get the file's content as a string.
        /// </summary>
        /// <param name="filename">Currnet file's path</param>
        /// <returns>Content as a string, string.empty on error</returns>
        public static string Parse(string filename)
        {
            IFilter filter = null;

            try
            {
                StringBuilder plainTextResult = new StringBuilder();
                filter = loadIFilter(filename);

                STAT_CHUNK ps = new STAT_CHUNK();
                IFILTER_INIT mFlags = 0;

                uint i = 0;
                filter.Init(mFlags, 0, null, ref i);

                int resultChunk = 0;

                resultChunk = filter.GetChunk(out ps);
                while (resultChunk == 0)
                {
                    if (ps.flags == CHUNKSTATE.CHUNK_TEXT)
                    {
                        uint sizeBuffer = 60000;
                        int resultText = 0;
                        while (resultText == Constants.FILTER_S_LAST_TEXT || resultText == 0)
                        {
                            sizeBuffer = 60000;
                            System.Text.StringBuilder sbBuffer = new System.Text.StringBuilder((int)sizeBuffer);
                            resultText = filter.GetText(ref sizeBuffer, sbBuffer);

                            if (sizeBuffer > 0 && sbBuffer.Length > 0)
                            {
                                string chunk = sbBuffer.ToString(0, (int)sizeBuffer);
                                plainTextResult.Append(chunk);
                            }
                        }
                    }
                    resultChunk = filter.GetChunk(out ps);
                }
                return plainTextResult.ToString();
            }
            finally
            {
                if (filter != null)
                    Marshal.ReleaseComObject(filter);
            }
        }
    }
}
