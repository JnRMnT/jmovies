using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Entities.Movies;
using JMovies.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace JMovies.Controllers
{
    [Route("api/tvseries")]
    public class TVSeriesController : BaseProductionController
    {
        public TVSeriesController(IIMDbDataProvider imdbDataProvider) : base(imdbDataProvider)
        {
        }

        [HttpGet("{id}")]
        public TVSeries Get(long id)
        {
            return base.GetProductionDetails(id) as TVSeries;
        }
    }
}
