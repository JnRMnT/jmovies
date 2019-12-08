using JMovies.IMDb.Entities.Movies;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Entities.Responses
{
    public class GetLastAddedProductionsResponse : BaseResponse
    {
        public Production[] Productions { get; set; }
    }
}
