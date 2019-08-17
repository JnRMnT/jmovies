using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JM.Entities;
using JMovies.Entities;
using JMovies.Entities.Framework;

namespace JMovies.Configuration.FlowItems
{
    public class ResultManagementFlowItem : BaseFlowItem
    {
        protected override void OnExecuteFlow(IServiceProvider serviceProvider, ref object request, ref BaseResponse response)
        {
            Context context = (serviceProvider.GetService(typeof(IContextProvider)) as IContextProvider).GetContext();
            if (context.ActiveResult == null)
            {
                context.ActiveResult = new JMResult();
            }
            else if (!context.ActiveResult.IsSuccess)
            {

            }
        }
    }
}
