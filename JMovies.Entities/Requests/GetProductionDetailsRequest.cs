using JMovies.IMDb.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities.Requests
{
    public class GetProductionDetailsRequest: BaseRequest
    {
        public long ID { get; set; }
        public ProductionDataFetchSettings Settings { get; set; }
    }
}
