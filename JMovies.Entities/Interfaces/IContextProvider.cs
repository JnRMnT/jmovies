using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities
{
    public interface IContextProvider
    {
        Context GetContext();
        void SetContext(Context context);
    }
}
