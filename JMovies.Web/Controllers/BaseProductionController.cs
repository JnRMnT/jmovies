using JMovies.IMDb.Entities.Common;
using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Entities.Movies;
using JMovies.IMDb.Entities.Settings;
using JMovies.Web.Helpers;

namespace JMovies.Web.Controllers
{
    public class BaseProductionController : BaseApiController
    {
        protected IIMDbDataProvider imdbDataProvider;
        public BaseProductionController(IIMDbDataProvider imdbDataProvider)
        {
            this.imdbDataProvider = imdbDataProvider;
        }

        protected Production GetProductionDetails(long id)
        {
            Production production = imdbDataProvider.GetProduction(id);
            if (production != null)
            {
                ImageHelper.WrapImageUrl(production.Poster);
                if (production.MediaImages != null)
                {
                    foreach (Image image in production.MediaImages)
                    {
                        ImageHelper.WrapImageUrl(image);
                    }
                }

                if (production is Movie)
                {
                    Movie movie = (Movie)production;
                    if (movie.Credits != null)
                    {
                        foreach (Credit credit in movie.Credits)
                        {
                            PersonHelper.WrapPersonImageUrls(credit.Person);
                        }
                    }
                }
            }

            return production;
        }
    }
}
