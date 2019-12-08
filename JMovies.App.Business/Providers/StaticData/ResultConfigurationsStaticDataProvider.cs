using JMovies.DataAccess;
using JMovies.DataAccess.Entities.ResultHandling;
using FWEntities = JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace JMovies.App.Business.Providers.StaticData
{
    public class ResultConfigurationsStaticDataProvider : IResultConfigurationsStaticDataProvider
    {
        private IEnumerable<FWEntities.ResultConfiguration> resultConfigurations = null;
        public object GetData()
        {
            return resultConfigurations;
        }

        public FWEntities.ResultConfiguration GetResultConfiguration(string errorCode)
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

        public void Initialize()
        {
            using (JMoviesEntities entities = new JMoviesEntities())
            {
                List<FWEntities.ResultConfiguration> resultConfigurations = new List<FWEntities.ResultConfiguration>();
                foreach (ResultConfiguration resultConfiguration in entities.ResultConfiguration.ToArray())
                {
                    resultConfiguration.ResultMessages = entities.ResultMessage.Where(e => e.ResultConfigurationID == resultConfiguration.ID).ToArray();
                    if (resultConfiguration.ResultMessages != null)
                    {
                        resultConfigurations.Add(new FWEntities.ResultConfiguration
                        {
                            ErrorCode = resultConfiguration.ErrorCode,
                            RedirectionParameter = resultConfiguration.RedirectionParameter,
                            RedirectionType = resultConfiguration.RedirectionType,
                            ResultMessages = resultConfiguration.ResultMessages.Select(e => new FWEntities.ResultMessage
                            {
                                Content = e.Content,
                                Culture = e.Culture
                            }).ToArray()
                        });
                    }
                }
                this.resultConfigurations = resultConfigurations.ToArray();
            }
        }
    }
}
