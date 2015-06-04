using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

using AstroGrep.Core.Logging;
using libAstroGrep;

namespace AstroGrep.Core.Caching
{
   /// <summary>
   /// Provides the ability to cache file encodings and save/load them to disk.  
   /// Each performance setting is a separate cache file.
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
   public class EncodingCache
   {
      private static EncodingCache instance;
      private Dictionary<string, EncodingCacheItem> cache = null;

      /// <summary>
      /// Retrieves the current instance of the EncodingCache.
      /// </summary>
      public static EncodingCache Instance
      {
         get
         {
            if (EncodingCache.instance == null)
            {
               EncodingCache.instance = new EncodingCache();
            }
            return EncodingCache.instance;
         }
      }

      /// <summary>
      /// 
      /// </summary>
      private EncodingCache()
      {
         cache = new Dictionary<string, EncodingCacheItem>();
      }

      /// <summary>
      /// 
      /// </summary>
      public Dictionary<string, EncodingCacheItem> Cache
      {
         get { return cache; }
         set { cache = value; }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		05/28/2015	FIX: 69, Created for speed improvements for encoding detection
      /// </history>
      public void Save(EncodingOptions.Performance performanceSetting)
      {
         string path = GetFilePath(performanceSetting);
         LogClient.Instance.Logger.Info("Saving encoding cache to disk at {0}", path);

         try
         {
            FileInfo fInfo = new FileInfo(path);

            if (!fInfo.Directory.Exists)
            {
               fInfo.Directory.Create();
            }

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
               using (var deflate = new DeflateStream(fs, CompressionMode.Compress))
               {
                  // Construct a BinaryFormatter and use it to serialize the data to the stream.
                  BinaryFormatter formatter = new BinaryFormatter();

                  formatter.Serialize(deflate, cache);
               }
            }
         }
         catch (SerializationException e)
         {
            LogClient.Instance.Logger.Error("Serialization error: {0}", LogClient.GetAllExceptions(e));
         }
         catch (Exception ex)
         {
            LogClient.Instance.Logger.Error("Saving generic error: {0}", LogClient.GetAllExceptions(ex));
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		05/28/2015	FIX: 69, Created for speed improvements for encoding detection
      /// </history>
      public void Load(EncodingOptions.Performance performanceSetting)
      {
         string path = GetFilePath(performanceSetting);
         LogClient.Instance.Logger.Info("Loading encoding cache from disk at {0}", path);

         try
         {
            Clear();

            if (File.Exists(path))
            {
               using (FileStream fs = new FileStream(path, FileMode.Open))
               {
                  using (var deflate = new DeflateStream(fs, CompressionMode.Decompress))
                  {
                     BinaryFormatter formatter = new BinaryFormatter();

                     // Deserialize the cache from the file and  
                     // assign the reference to the local variable.
                     cache = (Dictionary<string, EncodingCacheItem>)formatter.Deserialize(deflate);
                  }
               }
            }
         }
         catch (SerializationException e)
         {
            LogClient.Instance.Logger.Error("Deserialization error: {0}", LogClient.GetAllExceptions(e));
         }
         catch (Exception ex)
         {
            LogClient.Instance.Logger.Error("Loading generic error: {0}", LogClient.GetAllExceptions(ex));
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="deletePhysical"></param>
      /// <history>
      /// [Curtis_Beard]		05/28/2015	FIX: 69, Created for speed improvements for encoding detection
      /// </history>
      public void Clear(bool deletePhysical = false)
      {
         if (cache != null)
         {
            cache.Clear();
         }

         // delete all cache files by going through all values of enumeration
         if (deletePhysical)
         {
            Array values = Enum.GetValues(typeof(EncodingOptions.Performance));
            foreach (var val in values)
            {
               try
               {
                  FileInfo fInfo = new FileInfo(GetFilePath((EncodingOptions.Performance)val));
                  if (fInfo.Exists)
                  {
                     fInfo.Delete();
                  }
               }
               catch (Exception ex)
               {
                  LogClient.Instance.Logger.Error("Error deleting cache file: {0}", LogClient.GetAllExceptions(ex));
               }
            }
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      /// <history>
      /// [Curtis_Beard]		05/28/2015	FIX: 69, Created for speed improvements for encoding detection
      /// </history>
      private string GetFilePath(EncodingOptions.Performance performanceSetting)
      {
         return Path.Combine(Constants.DataDirectory, "Cache", string.Format("encodings_{0}.cache", performanceSetting));
      }
   }
}
