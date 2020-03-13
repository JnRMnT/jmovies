using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Entities.Movies;
using JMovies.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JMovies.Controllers
{
    [Route("api/movie")]
    public class MovieController : BaseProductionController
    {
        public MovieController(IIMDbDataProvider imdbDataProvider) : base(imdbDataProvider)
        {
        }

        [HttpGet("{id}")]
        public Movie Get(long id)
        {
            return base.GetProductionDetails(id) as Movie;
        }
    }
}
