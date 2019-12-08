using JMovies.IMDb.Entities.Movies;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Entities.Responses
{
    public class GetProductionCastResponse: BaseResponse
    {
        public Credit[] Cast { get; set; }
    }
}
