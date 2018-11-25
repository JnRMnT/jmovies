using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities.Interfaces
{
    public interface IStaticDataProvider
    {
        void Initialize();
        object GetData();
    }
}
