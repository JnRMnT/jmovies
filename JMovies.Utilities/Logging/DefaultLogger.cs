using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace JMovies.Utilities.Logging
{
    public class DefaultLogger
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Debug(string message)
        {
            logger.Debug(message);
        }

        public static void Warn(string message)
        {
            logger.Warn(message);
        }

        public static void Info(string message)
        {
            logger.Info(message);
        }

        public static void Error(Exception e)
        {
            logger.Fatal(e, e);
        }

        public static void Error(string message, Exception e)
        {
            logger.Fatal(message, e);
        }
    }
}
