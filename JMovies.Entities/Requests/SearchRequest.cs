using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities.Requests
{
    public class SearchRequest: BaseRequest
    {
        public string SearchKey { get; set; }
    }
}
