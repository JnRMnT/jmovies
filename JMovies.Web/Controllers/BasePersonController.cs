using JM.Entities.Framework;
using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Entities.People;
using JMovies.Utilities.Helpers;

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
            Person person = imdbDataProvider.GetPerson(id);
            PersonHelper.WrapPersonImageUrls(person);

            if (person is null)
            {
                throw new JMException("PersonNotFound");
            }

            return person;
        }
    }
}
