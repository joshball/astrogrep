using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libAstroGrep.EncodingDetection
{
   /// <summary>
   /// Detect file encodings.
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
   /// </remarks>
   /// <history>
   /// [Curtis_Beard]		02/12/2014	Created
   /// </history>
   public class EncodingDetector
   {
      /// <summary>
      /// The available encoding detectors.
      /// </summary>
      [Flags]
      public enum Options
      {
         KlerkSoftBom = 1,
         KlerkSoftHeuristics = 2,
         WinMerge = 4,
         MozillaUCD = 8,
         MLang = 16
      }

      /// <summary>
      /// Wrapper class for use with DetectAll method.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		12/01/2014	Created
      /// </history>
      public class EncodingValue
      {
         /// <summary>
         /// The encoding found by detector
         /// </summary>
         public Encoding Encoding { get; set; }

         /// <summary>
         /// The dector used
         /// </summary>
         public Options Option { get; set; }
      }

      /// <summary>
      /// Gets all detected encodings.
      /// </summary>
      /// <param name="bytes">Sample bytes to detect encoding</param>
      /// <returns>List of EncodingValue object</returns>
      /// <history>
      /// [Curtis_Beard]		12/01/2014	Created
      /// </history>
      public static List<EncodingValue> DetectAll(byte[] bytes)
      {
         List<EncodingValue> values = new List<EncodingValue>();

         EncodingValue value = new EncodingValue();
         value.Encoding = DetectEncodingUsingKlerksSoftBom(bytes);
         value.Option = Options.KlerkSoftBom;
         values.Add(value);
         
         value = new EncodingValue();
         value.Encoding = DetectEncodingUsingKlerksSoftHeuristics(bytes);
         value.Option = Options.KlerkSoftHeuristics;
         values.Add(value);

         value = new EncodingValue();
         value.Encoding = DetectEncodingUsingWinMerge(bytes);
         value.Option = Options.WinMerge;
         values.Add(value);

         value = new EncodingValue();
         value.Encoding = DetectEncodingUsingMozillaUCD(bytes);
         value.Option = Options.MozillaUCD;
         values.Add(value);

         value = new EncodingValue();
         value.Encoding = DetectEncodingUsingMLang(bytes);
         value.Option = Options.MLang;
         values.Add(value);

         return values;
      }

      /// <summary>
      /// Detects the byte array's encoding based on options passed in.
      /// </summary>
      /// <param name="bytes">File sample byte array</param>
      /// <param name="opts">Flags of encoding dectors to use</param>
      /// <param name="defaultEncoding">Default encoding if nothing detected</param>
      /// <returns>Encoding detected or default if not detected</returns>
      /// <history>
      /// [Curtis_Beard]		02/12/2014	Created
      /// [Curtis_Beard]		12/01/2014	ADD: support for Mozilla encoding detection, remove KlerkSoftHeuristics as a default
      /// </history>
      public static Encoding Detect(byte[] bytes, out string usedEncoder, EncodingDetector.Options opts = Options.KlerkSoftBom | Options.WinMerge | Options.MozillaUCD | Options.MLang, Encoding defaultEncoding = null)
      {
         Encoding encoding = null;

         // NOTE: this order determines which is run first, usually Mozilla is better than MLang
         if ((opts & Options.KlerkSoftBom) == Options.KlerkSoftBom)
         {
            encoding = DetectEncodingUsingKlerksSoftBom(bytes);

            if (encoding != null)
            {
               usedEncoder = Options.KlerkSoftBom.ToString();
               return encoding;
            }
         }

         if ((opts & Options.KlerkSoftHeuristics) == Options.KlerkSoftHeuristics)
         {
            encoding = DetectEncodingUsingKlerksSoftHeuristics(bytes);

            if (encoding != null)
            {
               usedEncoder = Options.KlerkSoftHeuristics.ToString();
               return encoding;
            }
         }

         if ((opts & Options.WinMerge) == Options.WinMerge)
         {
            encoding = DetectEncodingUsingWinMerge(bytes);

            if (encoding != null)
            {
               usedEncoder = Options.WinMerge.ToString();
               return encoding;
            }
         }

         if ((opts & Options.MozillaUCD) == Options.MozillaUCD)
         {
            encoding = DetectEncodingUsingMozillaUCD(bytes);

            if (encoding != null)
            {
               usedEncoder = Options.MozillaUCD.ToString();
               return encoding;
            }
         }

         if ((opts & Options.MLang) == Options.MLang)
         {
            encoding = DetectEncodingUsingMLang(bytes);

            if (encoding != null)
            {
               usedEncoder = Options.MLang.ToString();
               return encoding;
            }
         }

         // default encoding use since nothing was found
         usedEncoder = "Default";
         if (encoding == null)
         {
            encoding = defaultEncoding;            
         }

         return encoding;
      }

      #region Private Methods

      /// <summary>
      /// Detects encoding using bom bytes.
      /// </summary>
      /// <param name="bytes">sample data</param>
      /// <returns>Detected encoding or null if not detected</returns>
      /// <history>
      /// [Curtis_Beard]		02/12/2014	Created
      /// </history>
      private static Encoding DetectEncodingUsingKlerksSoftBom(byte[] bytes)
      {
         Encoding encoding = null;
         if (bytes.Count() >= 4)
            encoding = KlerksSoftEncodingDetector.DetectBOMBytes(bytes);

         return encoding;
      }

      /// <summary>
      /// Detects encoding using heuristics.
      /// </summary>
      /// <param name="bytes">sample data</param>
      /// <returns>Detected encoding or null if not detected</returns>
      /// <history>
      /// [Curtis_Beard]		02/12/2014	Created
      /// </history>
      private static Encoding DetectEncodingUsingKlerksSoftHeuristics(byte[] bytes)
      {
         Encoding encoding = KlerksSoftEncodingDetector.DetectUnicodeInByteSampleByHeuristics(bytes);

         return encoding;
      }

      /// <summary>
      /// Detects encoding using MLang from Microsoft.
      /// </summary>
      /// <param name="bytes">sample data</param>
      /// <returns>Detected encoding or null if not detected</returns>
      /// <history>
      /// [Curtis_Beard]		02/12/2014	Created
      /// </history>
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
         catch { }

         return null;
      }

      /// <summary>
      /// Detects encoding using WinMerge algorithm.
      /// </summary>
      /// <param name="bytes">sample data</param>
      /// <returns>Detected encoding or null if not detected</returns>
      /// <history>
      /// [Curtis_Beard]		02/12/2014	Created
      /// </history>
      private static Encoding DetectEncodingUsingWinMerge(Byte[] bytes)
      {
         return WinMergeEncodingDetector.DetectEncoding(bytes, bytes.Length);
      }

      /// <summary>
      /// Detects encoding using mozilla universal character detector.
      /// </summary>
      /// <param name="bytes">sample data</param>
      /// <returns>Detected encoding or null if not detected</returns>
      /// <history>
      /// [Curtis_Beard]		12/01/2014	Created
      /// </history>
      private static Encoding DetectEncodingUsingMozillaUCD(Byte[] bytes)
      {
         try
         {
            Ude.ICharsetDetector cdet = new Ude.CharsetDetector();
            cdet.Feed(bytes, 0, bytes.Length);
            cdet.DataEnd();

            if (cdet.Charset != null)
            {
               return Encoding.GetEncoding(cdet.Charset);
            }
         }
         catch { }

         return null;
      }

      #endregion
   }
}
