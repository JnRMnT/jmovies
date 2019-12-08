using JMovies.Common.Constants;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using System.Linq;
using JMovies.Entities.Responses;
using System.Collections.Generic;

namespace JMovies.Web.Providers.StaticDataProviders
{
    public class ResultConfigurationsStaticDataProvider : ActionBasedBaseStaticDataProvider<IResultConfigurationsStaticDataProvider>, IResultConfigurationsStaticDataProvider
    {
        private IEnumerable<ResultConfiguration> resultConfigurations = null;

        public ResultConfigurationsStaticDataProvider(IJMAppClientProvider jmAppClientProvider) : base(jmAppClientProvider)
        {
        }

        public object GetData()
        {
            return resultConfigurations;
        }

        public ResultConfiguration GetResultConfiguration(string errorCode)
        {
            if (resultConfigurations == null)
            {
                return null;
            }
            else
            {
                return resultConfigurations.FirstOrDefault(e => e.ErrorCode == errorCode);
            }
        }

        protected override void HandleStaticDataResponse(StaticDataManagementResponse response)
        {
            resultConfigurations = response.StaticData as IEnumerable<ResultConfiguration>;
        }
    }
}
