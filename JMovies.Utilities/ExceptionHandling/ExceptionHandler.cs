using JM.Entities;
using JM.Entities.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Utilities.ExceptionHandling
{
    public class ExceptionHandler
    {
        public static JMResult HandleException(Exception e)
        {
            string code = "System";
            if(e is JMException)
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
