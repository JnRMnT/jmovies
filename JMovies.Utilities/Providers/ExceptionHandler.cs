using JM.Entities;
using JM.Entities.Framework;
using JM.Entities.Interfaces;
using JMovies.Entities;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using JMovies.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Utilities.Providers
{
    public class ExceptionHandler : IExceptionHandler
    {
        private IResultConfigurationsStaticDataProvider resultConfigurationsStaticDataProvider;

        public ExceptionHandler(IResultConfigurationsStaticDataProvider resultConfigurationsStaticDataProvider)
        {
            this.resultConfigurationsStaticDataProvider = resultConfigurationsStaticDataProvider;
        }

        public JMResult HandleException(Exception e)
        {
            DefaultLogger.Error(e);
            string code = "System";
            if (e is JMException)
            {
                code = (e as JMException).Code;
            }

            JMResult result = null;
            ResultConfiguration resultConfiguration = resultConfigurationsStaticDataProvider.GetResultConfiguration(code);
            if (resultConfiguration != null)
            {
                result = new JMResult
                {
                    RedirectionInfo = new JMRedirectionInfo
                    {
                        RedirectionParameter = resultConfiguration.RedirectionParameter,
                        RedirectionType = resultConfiguration.RedirectionType
                    },
                    Errors = new JMResultItem[]
                    {
                        new JMResultItem
                        {
                            Code = code,
                            Message = resultConfiguration.ResultMessages.FirstOrDefault(e=> e.Culture == CultureInfo.CurrentCulture.Name)?.Content
                        }
                    }
                };
            }
            else
            {
                result = new JMResult
                {
                    Errors = new JMResultItem[]
                    {
                        new JMResultItem
                        {
                            Code = code,
                            Message = e.Message
                        }
                    }
                };
            }

            return result;
        }
    }
}
