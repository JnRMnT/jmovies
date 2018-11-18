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

        public static void Error(Exception e)
        {
            logger.Error(e);
        }

        public static void Error(string message, Exception e)
        {
            logger.Error(message, e);
        }
    }
}
