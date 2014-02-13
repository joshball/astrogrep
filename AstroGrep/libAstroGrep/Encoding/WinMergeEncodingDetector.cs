using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libAstroGrep
{
   /// <summary>
   /// 
   /// </summary>
   public class WinMergeEncodingDetector
   {
      /// <summary>
      /// Determine encoding from byte buffer.
      /// </summary>
      /// <param name="pBuffer">sample bytes of file</param>
      /// <param name="size">size of the buffer</param>
      /// <returns>Found encoding, null if not found</returns>
      public static Encoding DetectEncoding(byte[] pBuffer, int size)
      {
         //UNICODESET unicoding = ucr::NONE;
         Encoding enc = null;

         try
         {

            //*pBom = false;

            if (size >= 2)
            {
               if (pBuffer[0] == 0xFF && pBuffer[1] == 0xFE)
               {
                  //unicoding = ucr::UCS2LE;
                  enc = Encoding.Unicode;
                  //*pBom = true;
               }
               else if (pBuffer[0] == 0xFE && pBuffer[1] == 0xFF)
               {
                  //unicoding = ucr::UCS2BE;
                  enc = Encoding.Unicode;
                  //*pBom = true;
               }
            }
            if (size >= 3)
            {
               if (pBuffer[0] == 0xEF && pBuffer[1] == 0xBB && pBuffer[2] == 0xBF)
               {
                  //unicoding = ucr::UTF8;
                  enc = Encoding.UTF8;
                  //*pBom = true;
               }
            }

            // If not any of the above, check if it is UTF-8 without BOM?
            //if (unicoding == ucr::NONE)
            if (enc == null)
            {
               int bufSize = (int)Math.Min(size, 8 * 1024);
               bool invalidUtf8 = CheckForInvalidUtf8(pBuffer, bufSize);
               if (!invalidUtf8)
               {
                  // No BOM!
                  //unicoding = ucr::UTF8;
                  enc = Encoding.UTF8;
               }
            }

         }
         catch
         {

         }

         //return unicoding;
         return enc;
      }

      /// <summary>
      /// Check for invalid UTF-8 bytes in buffer.
      /// This function checks if there are invalid UTF-8 bytes in the given buffer.
      /// If such bytes are found, caller knows this buffer is not valid UTF-8 file.
      /// </summary>
      /// <param name="pBuffer">sample bytes of file</param>
      /// <param name="size">size of buffer</param>
      /// <returns>true if invalid bytes found, false otherwise</returns>
      private static bool CheckForInvalidUtf8(byte[] pBuffer, int size)
      {
         //UINT8* pVal2 = (UINT8*)pBuffer;
         for (int i = 0; i < size; ++i)
         {
            if ((pBuffer[i] == 0xC0) || (pBuffer[i] == 0xC1) || (pBuffer[i] >= 0xF5))
               return true;
            //pVal2++;
         }

         //pVal2 = (UINT8*)pBuffer;
         bool bUTF8 = false;
         for (int i = 0; i < (size - 3); ++i)
         {
            if ((pBuffer[i] & 0xE0) == 0xC0)
            {
               //pVal2++;
               i++;
               if ((pBuffer[i] & 0xC0) != 0x80)
                  return true;
               bUTF8 = true;
            }
            if ((pBuffer[i] & 0xF0) == 0xE0)
            {
               //pVal2++;
               i++;
               if ((pBuffer[i] & 0xC0) != 0x80)
                  return true;
               //pVal2++;
               i++;
               if ((pBuffer[i] & 0xC0) != 0x80)
                  return true;
               bUTF8 = true;
            }
            if ((pBuffer[i] & 0xF8) == 0xF0)
            {
               //pVal2++;
               i++;
               if ((pBuffer[i] & 0xC0) != 0x80)
                  return true;
               //pVal2++;
               i++;
               if ((pBuffer[i] & 0xC0) != 0x80)
                  return true;
               //pVal2++;
               i++;
               if ((pBuffer[i] & 0xC0) != 0x80)
                  return true;
               bUTF8 = true;
            }
            //pVal2++;
         }
         
         if (bUTF8)
            return false;

         return true;
      }
   }
}
