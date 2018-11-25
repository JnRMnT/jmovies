using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMovies.Entities;
using JMovies.Entities.Interfaces;
using JMovies.Utilities.Unity;

namespace JMovies.Configuration.FlowItems
{
    public class FlowActionCallItem : BaseFlowItem
    {
        protected override void OnExecuteFlow(ref BaseRequest request, ref BaseResponse response)
        {
            IContextProvider contextProvider = SingletonUnity.Resolve<IContextProvider>();
            Context context = contextProvider.GetContext();
            Type actionClassType = Type.GetType(context.ActiveFlowConfiguration.ActionClassIdentifier);
            IActionClass actionClass = Activator.CreateInstance(actionClassType) as IActionClass;
            actionClass.ExecuteAction(ref request, ref response);
        }
    }
}
