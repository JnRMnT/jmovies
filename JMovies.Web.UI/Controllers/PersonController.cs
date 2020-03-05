using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Entities.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JMovies.Controllers
{
    [Route("api/person")]
    public class PersonController : Web.Controllers.BasePersonController
    {
        public PersonController(IIMDbDataProvider imdbDataProvider) : base(imdbDataProvider)
        {
        }

        [HttpGet("{id}")]
        public Person Get(long id)
        {
            return base.GetPersonDetails(id);
        }
    }
}
