using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Entities.People;

namespace JMovies.Web.Controllers
{
    public class BasePersonController : BaseApiController
    {
        protected IIMDbDataProvider imdbDataProvider;
        public BasePersonController(IIMDbDataProvider imdbDataProvider)
        {
            this.imdbDataProvider = imdbDataProvider;
        }

        protected Person GetPersonDetails(long id)
        {
            return imdbDataProvider.GetPerson(id);
        }
    }
}
