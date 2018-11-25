using JMovies.Entities;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;
using JMovies.Utilities.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.App.Business.Actions
{
    public class StaticDataManagement : IActionClass
    {
        public void ExecuteAction(ref BaseRequest request, ref BaseResponse response)
        {
            StaticDataManagementRequest requestMessage = request as StaticDataManagementRequest;
            StaticDataManagementResponse responseMessage = response as StaticDataManagementResponse;

            IStaticDataProvider staticDataProvider = SingletonUnity.Resolve(requestMessage.StaticDataProviderType) as IStaticDataProvider;
            responseMessage.StaticData = staticDataProvider.GetData();
        }
    }
}
