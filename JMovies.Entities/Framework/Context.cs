using JM.Entities;
using JMovies.Entities.Framework;
using JMovies.Entities.UserManagement;

namespace JMovies.Entities
{
    public class Context
    {
        public User User { get; set; }
        public string ClientCulture { get; set; }
        public JMResult ActiveResult { get; set; }
        public FlowConfiguration ActiveFlowConfiguration { get; set; }
    }
}
