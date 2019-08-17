using JM.Entities;
using JMovies.Entities.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities
{
    public class Context
    {
        public JMResult ActiveResult { get; set; }
        public FlowConfiguration ActiveFlowConfiguration { get; set; }
    }
}
