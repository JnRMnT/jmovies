using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities.Framework
{
    public class FlowConfiguration
    {
        public string Name { get; set; }
        public string ActionClassIdentifier { get; set; }
        public string RequestIdentifier { get; set; }
        public string ResponseIdentifier { get; set; }
    }
}
