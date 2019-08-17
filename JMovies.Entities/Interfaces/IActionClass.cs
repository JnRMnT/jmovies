using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities.Interfaces
{
    public interface IActionClass
    {
        void ExecuteAction(IServiceProvider serviceProvider, ref object request, ref BaseResponse response);
    }
}
