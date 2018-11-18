using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities.Framework
{
    [Serializable]
    public class ResponsePayload
    {
        public Context Context { get; set; }
        public BaseResponse Response { get; set; }
    }
}
