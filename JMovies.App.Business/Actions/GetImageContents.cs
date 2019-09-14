using JMovies.Entities;
using JMovies.Entities.Interfaces;
using System;
using System.Linq;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;
using JMovies.DataAccess;

namespace JMovies.App.Business.Actions
{
    public class GetImageContents : IActionClass
    {
        public void ExecuteAction(IServiceProvider serviceProvider, ref object request, ref BaseResponse response)
        {
            GetImageContentsRequest requestMessage = request as GetImageContentsRequest;
            GetImageContentsResponse responseMessage = response as GetImageContentsResponse;

            using (JMoviesEntities entities = new JMoviesEntities())
            {
                responseMessage.Image = entities.Image.FirstOrDefault(e => e.ID == requestMessage.ID);
            }
        }
    }
}
