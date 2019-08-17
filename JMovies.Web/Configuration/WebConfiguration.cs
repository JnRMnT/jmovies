using JMovies.Configuration;
using JMovies.Entities.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Web.Configuration
{
    public class WebConfiguration: CustomConfiguration
    {
        public string AppEndpoint { get; set; }
    }
}
