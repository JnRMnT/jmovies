using JMovies.Entities.IMDB;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Responses;
using JMovies.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JMovies.Web.UI.Controllers
{
    [RoutePrefix("api/movies")]
    public class MoviesController : BaseApiController
    {
        private IIMDbDataProvider imdbDataProvider;
        public MoviesController(IIMDbDataProvider imdbDataProvider)
        {
            this.imdbDataProvider = imdbDataProvider;
        }

        [HttpGet]
        [Route("{id}")]
        public GetMovieDetailsResponse Get(string id)
        {
            return new GetMovieDetailsResponse
            {
                Movie = imdbDataProvider.GetMovie(id.ToLong(), false)
            };
        }
    }
}