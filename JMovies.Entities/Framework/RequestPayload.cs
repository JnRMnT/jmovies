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
        public string Action { get; set; }
        public Context Context { get; set; }
        public object Request { get; set; }
    }
}
