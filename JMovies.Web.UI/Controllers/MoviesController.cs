using JMovies.Entities.IMDB;
using JMovies.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JMovies.Web.UI.Controllers
{
    [RoutePrefix("api/movies")]
    public class MoviesController : ApiController
    {

        [HttpGet]
        [Route("{id}")]
        public GetMovieDetailsResponse Get(string id)
        {
            return new GetMovieDetailsResponse
            {
                Movie = new Movie
                {
                    Title = "Test"
                }
            };
        }
    }
}