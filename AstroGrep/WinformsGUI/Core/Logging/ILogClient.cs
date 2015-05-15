using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NLog;

namespace AstroGrep.Core.Logging
{
   /// <summary>
   /// 
   /// </summary>
   public interface ILogClient
   {
      /// <summary>
      /// 
      /// </summary>
      string LogFile
      {
         get;
         set;
      }

      /// <summary>
      /// 
      /// </summary>
      Logger Logger
      {
         get;
         set;
      }
   }
}
