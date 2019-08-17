using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMovies.Entities;
using JMovies.Entities.Interfaces;

namespace JMovies.Configuration.FlowItems
{
    public class FlowActionCallItem : BaseFlowItem
    {
        protected override void OnExecuteFlow(IServiceProvider serviceProvider, ref object request, ref BaseResponse response)
        {
            Context context = (serviceProvider.GetService(typeof(IContextProvider)) as IContextProvider).GetContext();
            Type actionClassType = Type.GetType(context.ActiveFlowConfiguration.ActionClassIdentifier);
            IActionClass actionClass = Activator.CreateInstance(actionClassType) as IActionClass;
            actionClass.ExecuteAction(serviceProvider, ref request, ref response);
        }
    }
}
