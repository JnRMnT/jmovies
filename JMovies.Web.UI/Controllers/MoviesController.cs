using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JMovies.Entities.Responses;
using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Entities.Settings;
using JMovies.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JMovies.Controllers
{
    [Route("api/movies")]
    public class MoviesController : BaseApiController
    {
        private IIMDbDataProvider imdbDataProvider;
        public MoviesController(IIMDbDataProvider imdbDataProvider)
        {
            this.imdbDataProvider = imdbDataProvider;
        }

        [HttpGet("{id}")]
        public GetMovieDetailsResponse Get(string id)
        {
            ProductionDataFetchSettings productionDataFetchSettings = new ProductionDataFetchSettings { FetchDetailedCast = false, FetchImageContents = true };
            return new GetMovieDetailsResponse
            {
                Movie = imdbDataProvider.GetMovie(id.ToLong(), productionDataFetchSettings)
            };
        }
    }
}
