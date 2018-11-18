using JMovies.Entities.Framework;
using JMovies.Utilities.Logging;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace JMovies.WebJobs.FetchIMDBData
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                XmlConfigurator.Configure();

            }
            catch (Exception e)
            {
                DefaultLogger.Error(e);
                throw;
            }
        }
    }
}