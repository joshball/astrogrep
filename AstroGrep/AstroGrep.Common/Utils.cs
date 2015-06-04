using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroGrep.Common
{
   public sealed class Utils
   {
      /// <summary>
      /// Split the given string by a string.
      /// </summary>
      /// <param name="stringToSplit">string to split</param>
      /// <param name="separator">separator as string</param>
      /// <returns>string array</returns>
      /// <history>
      /// [Curtis_Beard]	   11/03/2006	Created
      /// </history>
      public static string[] SplitByString(string stringToSplit, string separator)
      {
         int offset = 0;
         int index = 0;
         int[] offsets = new int[stringToSplit.Length + 1];

         while (index < stringToSplit.Length)
         {
            int indexOf = stringToSplit.IndexOf(separator, index);
            if (indexOf != -1)
            {
               offsets[offset++] = indexOf;
               index = (indexOf + separator.Length);
            }
            else
            {
               index = stringToSplit.Length;
            }
         }

         string[] final = new string[offset + 1];
         if (offset == 0) //changed from 1, to fix when no split found
         {
            final[0] = stringToSplit;
         }
         else
         {
            offset--;

            final[0] = stringToSplit.Substring(0, offsets[0]);
            for (int i = 0; i < offset; i++)
            {
               final[i + 1] = stringToSplit.Substring(offsets[i] + separator.Length, offsets[i + 1] - offsets[i] - separator.Length);
            }
            final[offset + 1] = stringToSplit.Substring(offsets[offset] + separator.Length);
         }

         return final;
      }
   }
}
