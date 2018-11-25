using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities.Requests
{
    public class StaticDataManagementRequest: BaseRequest
    {
        public Type StaticDataProviderType { get; set; }
    }
}
