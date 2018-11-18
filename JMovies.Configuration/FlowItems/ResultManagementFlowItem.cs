using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMovies.Entities;
using JMovies.Entities.Framework;
using JMovies.Utilities.Unity;

namespace JMovies.Configuration.FlowItems
{
    public class ResultManagementFlowItem : BaseFlowItem
    {
        protected override void OnExecuteFlow(ref BaseRequest request, ref BaseResponse response)
        {
            IContextProvider contextProvider = SingletonUnity.Resolve<IContextProvider>();
            Context context = contextProvider.GetContext();
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
