using JMovies.Entities;
using JMovies.Entities.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;
using JMovies.IMDb.Entities.Interfaces;
using JM.Entities.Framework;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;

namespace JMovies.App.Business.Actions
{
    public class GetPersonDetails : IActionClass
    {
        public void ExecuteAction(IServiceProvider serviceProvider, ref object request, ref BaseResponse response)
        {
            GetPersonDetailsRequest requestMessage = request as GetPersonDetailsRequest;
            GetPersonDetailsResponse responseMessage = response as GetPersonDetailsResponse;

            IIMDbDataProvider imdbDataProvider = serviceProvider.GetRequiredService<IIMDbDataProvider>();
            responseMessage.Person = imdbDataProvider.GetPerson(requestMessage.ID);
        }
    }
}
