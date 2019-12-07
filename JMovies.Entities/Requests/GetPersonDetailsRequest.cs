using JMovies.IMDb.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Entities.Requests
{
    public class GetPersonDetailsRequest: BaseRequest
    {
        public long ID { get; set; }
        public PersonDataFetchSettings Settings { get; set; }
    }
}
