using JMovies.IMDb.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities. Responses
{
    public class GetImageContentsResponse : BaseResponse
    {
        public Image Image { get; set; }
    }
}
