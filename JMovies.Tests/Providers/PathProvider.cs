using JMovies.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Tests.Providers
{
    public class PathProvider : IPathProvider
    {
        public string GetCurrentPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
