using JMovies.Entities.IMDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities.Responses
{
    public class GetMovieDetailsResponse: BaseResponse
    {
        public Movie Movie { get; set; }
    }
}
