using JMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.App.Business.Context
{
    public class ContextProvider : IContextProvider
    {
        public Entities.Context GetContext()
        {
            return WcfOperationContext.Current.Items["Context"] as Entities.Context;
        }

        public void SetContext(Entities.Context context)
        {
            WcfOperationContext.Current.Items["Context"] = context;
        }
    }
}
