using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities.Framework
{
    public class JMResult
    {
        public JMResultItem[] Errors
        {
            get;set;
        }

        public bool IsSuccess
        {
            get
            {
                return Errors == null || Errors.Length == 0;
            }
        }
    }
}
