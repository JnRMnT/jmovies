using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities.Interfaces
{
    public interface IActionClass
    {
        void ExecuteAction(ref BaseRequest request, ref BaseResponse response);
    }
}
