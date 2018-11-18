using JMovies.Entities;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using JMovies.Utilities.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.App.Business.Actions
{
    public class BasicResolutionTest : IActionClass
    {
        public void ExecuteAction(ref BaseRequest request, ref BaseResponse response)
        {
            IContextProvider contextProvider = SingletonUnity.Resolve<IContextProvider>();
            if(contextProvider == null || contextProvider.GetContext() == null)
            {
                throw new JMException("ContextNotFound");
            }
        }
    }
}
