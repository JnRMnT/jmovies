﻿using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Entities.People;
using JMovies.Web.Helpers;

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
            return person;
        }
    }
}
