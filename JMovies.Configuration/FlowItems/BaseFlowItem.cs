using JMovies.Entities;
using JMovies.Utilities.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JM.Entities.Interfaces;

namespace JMovies.Configuration.FlowItems
{
    public class BaseFlowItem
    {
        //private Logger
        private bool stopFlowOnException;
        public BaseFlowItem() : this(true)
        {

        }

        public BaseFlowItem(bool stopFlowOnException)
        {
            this.stopFlowOnException = stopFlowOnException;
        }

        public void ExecuteFlow(IServiceProvider serviceProvider, ref object request, ref BaseResponse response)
        {
            try
            {
                OnExecuteFlow(serviceProvider, ref request, ref response);
            }
            catch (Exception e)
            {
                serviceProvider.GetRequiredService<IExceptionHandler>().HandleException(e);
                if (stopFlowOnException)
                {
                    throw;
                }
            }
        }

        protected virtual void OnExecuteFlow(IServiceProvider serviceProvider, ref object request, ref BaseResponse response)
        {

        }
    }
}
