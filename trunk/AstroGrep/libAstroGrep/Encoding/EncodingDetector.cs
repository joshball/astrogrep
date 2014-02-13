using System;
using System.Linq;
using System.Text;

namespace libAstroGrep
{
   /// <summary>
   /// 
   /// </summary>
   public class EncodingDetector
   {
      /// <summary>
      /// 
      /// </summary>
      [Flags]
      public enum Options
      {
         KlerkSoftBom = 1,
         KlerkSoftHeuristics = 2,
         MLang = 4,
         WinMerge = 5
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="bytes"></param>
      /// <param name="opts"></param>
      /// <param name="defaultEncoding"></param>
      /// <returns></returns>
      public static Encoding Detect(byte[] bytes, EncodingDetector.Options opts = Options.KlerkSoftBom | Options.KlerkSoftHeuristics | Options.MLang | Options.WinMerge, Encoding defaultEncoding = null)
      {
         Encoding encoding = null;

         if ((opts & Options.KlerkSoftBom) == Options.KlerkSoftBom)
         {
            encoding = DetectEncodingUsingKlerksSoftBom(bytes);

            if (encoding != null)
               return encoding;
         }

         if ((opts & Options.KlerkSoftHeuristics) == Options.KlerkSoftHeuristics)
         {
            encoding = DetectEncodingUsingKlerksSoftHeuristics(bytes);

            if (encoding != null)
               return encoding;
         }

         if ((opts & Options.WinMerge) == Options.WinMerge)
         {
            encoding = DetectEncodingUsingWinMerge(bytes);

            if (encoding != null)
               return encoding;
         }         

         if ((opts & Options.MLang) == Options.MLang)
         {
            encoding = DetectEncodingUsingMLang(bytes);
         }

         // default encoding use since nothing was found
         if (encoding == null)
            encoding = defaultEncoding;

         return encoding;
      }

      #region Private Methods

      /// <summary>
      /// 
      /// </summary>
      /// <param name="bytes"></param>
      /// <returns></returns>
      private static Encoding DetectEncodingUsingKlerksSoftBom(byte[] bytes)
      {
         Encoding encoding = null;
         if (bytes.Count() >= 4)
            encoding = KlerksSoftEncodingDetector.DetectBOMBytes(bytes);

         return encoding;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="bytes"></param>
      /// <returns></returns>
      private static Encoding DetectEncodingUsingKlerksSoftHeuristics(byte[] bytes)
      {
         Encoding encoding = KlerksSoftEncodingDetector.DetectUnicodeInByteSampleByHeuristics(bytes);

         return encoding;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="bytes"></param>
      /// <returns></returns>
      private static Encoding DetectEncodingUsingMLang(Byte[] bytes)
      {
         try
         {
            Encoding[] detected = EncodingTools.DetectInputCodepages(bytes, 1);
            if (detected.Length > 0)
            {
               return detected[0];
            }
         }
         catch //(COMException ex)
         {
            // return default codepage on error
         }

         return null;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="bytes"></param>
      /// <returns></returns>
      private static Encoding DetectEncodingUsingWinMerge(Byte[] bytes)
      {
         return WinMergeEncodingDetector.DetectEncoding(bytes, bytes.Length);
      }

      #endregion
   }
}
