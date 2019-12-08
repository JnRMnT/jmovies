using JM.Entities;
using JM.Entities.Framework;
using JM.Entities.Interfaces;
using JMovies.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Utilities.Providers
{
    public class ExceptionHandler : IExceptionHandler
    {
        public JMResult HandleException(Exception e)
        {
            DefaultLogger.Error(e);
            string code = "System";
            if (e is JMException)
            {
                code = (e as JMException).Code;
            }
            return new JMResult
            {
                Errors = new JMResultItem[]
                {
                    new JMResultItem
                    {
                        Code = code,
                        Message = e.Message
                    }
                }
            };
        }
    }
}
