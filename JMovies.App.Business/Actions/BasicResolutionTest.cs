using JM.Entities.Framework;
using JMovies.Entities;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.App.Business.Actions
{
    public class BasicResolutionTest : IActionClass
    {
        public void ExecuteAction(IServiceProvider serviceProvider, ref object request, ref BaseResponse response)
        {
            if (serviceProvider == null)
            {
                throw new JMException("ContextNotFound");
            }
            else
            {
                IContextProvider contextProvider = serviceProvider.GetService(typeof(IContextProvider)) as IContextProvider;
                if (contextProvider == null || contextProvider.GetContext() == null)
                {
                    throw new JMException("ContextNotFound");
                }
            }
        }
    }
}
