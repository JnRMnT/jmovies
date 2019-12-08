using JMovies.Configuration;
using JMovies.Entities.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.App.Business.Configuration
{
    public class AppConfiguration : CustomConfiguration
    {
        public string[] WhiteList { get; set; }
    }
}
