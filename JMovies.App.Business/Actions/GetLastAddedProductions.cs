using JMovies.DataAccess;
using JMovies.Entities;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using JMovies.IMDb.Entities.Movies;
using System;
using JMovies.Utilities.Helpers;

namespace JMovies.App.Business.Actions
{
    public class GetLastAddedProductions : IActionClass
    {
        public void ExecuteAction(IServiceProvider serviceProvider, ref object request, ref BaseResponse response)
        {
            GetLastAddedProductionsRequest requestMessage = request as GetLastAddedProductionsRequest;
            GetLastAddedProductionsResponse responseMessage = response as GetLastAddedProductionsResponse;
            using (JMoviesEntities entities = new JMoviesEntities())
            {
                Production[] productions = entities.Production.Include(e=> e.Poster).OrderByDescending(e => e.ID).Take(16).ToArray();
                if (productions != null)
                {
                    foreach (Production production in productions)
                    {
                        if (production.Poster != null)
                        {
                            production.Poster.Content = null;
                            ImageHelper.WrapImageUrl(production.Poster);
                        }
                    }
                }
                responseMessage.Productions = productions;
            }
        }
    }
}
