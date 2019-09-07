using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Entities.Movies;

namespace JMovies.Web.Controllers
{
    public class BaseProductionController : BaseApiController
    {
        protected IIMDbDataProvider imdbDataProvider;
        public BaseProductionController (IIMDbDataProvider imdbDataProvider)
        {
            this.imdbDataProvider = imdbDataProvider;
        }

        protected Production GetProductionDetails(long id)
        {
            return imdbDataProvider.GetProduction(id); 
        }
    }
}
