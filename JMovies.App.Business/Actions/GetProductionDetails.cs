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
    public class GetProductionDetails : IActionClass
    {
        public void ExecuteAction(IServiceProvider serviceProvider, ref object request, ref BaseResponse response)
        {
            GetProductionDetailsRequest requestMessage = request as GetProductionDetailsRequest;
            GetProductionDetailsResponse responseMessage = response as GetProductionDetailsResponse;

            IIMDbDataProvider imdbDataProvider = serviceProvider.GetRequiredService<IIMDbDataProvider>();
            responseMessage.Production = imdbDataProvider.GetProduction(requestMessage.ID, requestMessage.Settings);
        }
    }
}
