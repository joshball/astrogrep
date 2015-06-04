using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace libAstroGrep.EncodingDetection
{
   /// <summary>
   /// File detection encoding options.
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
   /// [Curtis_Beard]		05/28/2015	FIX: 69, Created for speed improvements for encoding detection
   /// </history>
   public class EncodingOptions
   {
      private const int SampleSize_Speed = 1024;
      private const int SampleSize_Default = 5120;
      private const int SampleSize_Accuracy = 10240;

      /// <summary>
      /// Performance levels that determine what detection methods are used.
      /// </summary>
      public enum Performance
      {
         /// <summary></summary>
         Speed = 1,
         /// <summary></summary>
         Default = 2,
         /// <summary></summary>
         Accuracy = 4
      }

      /// <summary>
      /// Determines whether file encoding detection is enabled.
      /// </summary>
      public bool DetectFileEncoding
      { 
         get;
         set; 
      }

      /// <summary>
      /// Gets/Sets the current performance level.
      /// </summary>
      public Performance PerformanceSetting
      {
         get;
         set;
      }

      /// <summary>
      /// Gets/Sets whether the encoding cached is used.
      /// </summary>
      public bool UseEncodingCache
      {
         get;
         set;
      }

      /// <summary>
      /// Creates an instance of the EncodingOptions class.
      /// </summary>
      public EncodingOptions()
      {
         DetectFileEncoding = true;
         PerformanceSetting = Performance.Default;
         UseEncodingCache = true;
      }

      /// <summary>
      /// Creates an instance of the EncodingOptions class.
      /// </summary>
      /// <param name="detectFileEncoding">determines if file encoding detection is used</param>
      /// <history>
      /// [Curtis_Beard]		06/02/2015	FIX: 75, created to get a sample size based on performance setting
      /// </history>
      public EncodingOptions(bool detectFileEncoding)
         : this()
      {
         DetectFileEncoding = detectFileEncoding;
      }
      /// <summary>
      /// Creates an instance of the EncodingOptions class.
      /// </summary>
      /// <param name="detectFileEncoding">determines if file encoding detection is used</param>
      /// <param name="performanceSetting">current performance setting</param>
      /// <history>
      /// [Curtis_Beard]		06/02/2015	FIX: 75, created to get a sample size based on performance setting
      /// </history>
      public EncodingOptions(bool detectFileEncoding, Performance performanceSetting)
         : this(detectFileEncoding)
      {
         PerformanceSetting = performanceSetting;
      }

      /// <summary>
      /// Creates an instance of the EncodingOptions class.
      /// </summary>
      /// <param name="performanceSetting">current performance setting</param>
      /// <history>
      /// [Curtis_Beard]		06/02/2015	FIX: 75, created to get a sample size based on performance setting
      /// </history>
      public EncodingOptions(Performance performanceSetting)
         : this()
      {
         PerformanceSetting = performanceSetting;
      }

      /// <summary>
      /// Retrieves the desired encoding detectors based on the given performance setting.
      /// </summary>
      /// <param name="performanceSetting">current performance setting</param>
      /// <returns>EncodingDetector.Options bit flag representation of selected detectors</returns>
      /// <history>
      /// [Curtis_Beard]	   05/26/2015	FIX: 69, add performance setting for file detection
      /// </history>
      public static EncodingDetector.Options GetEncodingDetectorOptionsByPerformance(Performance performanceSetting)
      {
         EncodingDetector.Options opts = EncodingDetector.Options.KlerkSoftBom | EncodingDetector.Options.WinMerge | EncodingDetector.Options.MLang;

         switch (performanceSetting)
         {
            case Performance.Speed:
               opts = EncodingDetector.Options.KlerkSoftBom | EncodingDetector.Options.WinMerge;
               break;

            case Performance.Default:
            default:
               opts = EncodingDetector.Options.KlerkSoftBom | EncodingDetector.Options.WinMerge | EncodingDetector.Options.MLang;
               break;

            case Performance.Accuracy:
               opts = EncodingDetector.Options.KlerkSoftBom | EncodingDetector.Options.WinMerge | EncodingDetector.Options.MozillaUCD | EncodingDetector.Options.MLang;
               break;
         }

         return opts;
      }

      /// <summary>
      /// Retrieves the maximum number of bytes for a sample size based on a given performance setting.
      /// </summary>
      /// <param name="performanceSetting">current performance setting</param>
      /// <returns>the maximum number of bytes used in the sample size</returns>
      /// <history>
      /// [Curtis_Beard]		06/02/2015	FIX: 75, created to get a sample size based on performance setting
      /// </history>
      public static int GetSampleSizeByPerformance(Performance performanceSetting)
      {
         int size = SampleSize_Default;

         switch (performanceSetting)
         {
            case Performance.Speed:
               size = SampleSize_Speed;
               break;

            case Performance.Default:
            default:
               size = SampleSize_Default;
               break;

            case Performance.Accuracy:
               size = SampleSize_Accuracy;
               break;
         }

         return size;
      }
   }
}
