using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities.Framework
{
    [Serializable]
    public class RequestPayload
    {
        public Context Context { get; set; }
        public BaseRequest Request { get; set; }
    }
}
