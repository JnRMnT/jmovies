using JMovies.IMDb.Entities.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Entities.Responses
{
    public class GetPersonDetailsResponse : BaseResponse
    {
        public Person Person { get; set; }
    }
}
