using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Entities.Requests
{
    public class GetProductionCastRequest: BaseRequest
    {
        public long ProductionID { get; set; }
    }
}
