using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Entities.Movies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JMovies.Controllers
{
    [Authorize]
    [Route("api/production")]
    public class ProductionController : Web.Controllers.BaseProductionController 
    {
        public ProductionController(IIMDbDataProvider imdbDataProvider) : base(imdbDataProvider)
        {
        }

        [HttpGet("{id}")]
        public Production Get(long id)
        {
            return base.GetProductionDetails(id);
        }
    }
}
