using JMovies.Entities.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities.Responses
{
    public class SearchResponse : BaseResponse
    {
        public SearchResult[] SearchResults { get; set; }
    }
}
