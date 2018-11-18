using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities.Framework
{
    public class JMException : Exception
    {
        public string Code { get; set; }
        public JMException(string code): this(code, code)
        {

        }

        public JMException(string code, string message) : base(message)
        {

        }
    }
}
