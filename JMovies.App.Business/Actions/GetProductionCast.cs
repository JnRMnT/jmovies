using JMovies.DataAccess;
using JMovies.Entities;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using JMovies.IMDb.Entities.Movies;
using JMovies.Utilities.Helpers;

namespace JMovies.App.Business.Actions
{
    public class GetProductionCast : IActionClass
    {
        public void ExecuteAction(IServiceProvider serviceProvider, ref object request, ref BaseResponse response)
        {
            GetProductionCastRequest requestMessage = request as GetProductionCastRequest;
            GetProductionCastResponse responseMessage = response as GetProductionCastResponse;
            using (JMoviesEntities entities = new JMoviesEntities())
            {
                responseMessage.Cast = entities.Credit.Include(e => (e as ActingCredit).Characters).Include(e=> e.Person).ThenInclude(e=> e.PrimaryImage).Where(e => e.ProductionID == requestMessage.ProductionID).ToArray();

                if (responseMessage.Cast != null)
                {
                    foreach (Credit credit in responseMessage.Cast)
                    {
                        if(credit.Person.PrimaryImage != null)
                        {
                            credit.Person.PrimaryImage.Content = null;
                        }
                        PersonHelper.WrapPersonImageUrls(credit.Person);
                    }
                }
            }
        }
    }
}
