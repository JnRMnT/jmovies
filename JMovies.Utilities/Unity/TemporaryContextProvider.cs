using JMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Utilities.Unity
{
    public class TemporaryContextProvider : IContextProvider
    {
        [ThreadStatic]
        private Context context;
        public Context GetContext()
        {
            return context;
        }

        public void SetContext(Context context)
        {
            this.context = context;
        }
    }
}
