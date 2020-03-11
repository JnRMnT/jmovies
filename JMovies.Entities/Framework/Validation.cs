using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Entities.Framework
{
    public class Validation
    {
        public string ErrorResource { get; set; }
        public string ServerValidator { get; set; }
        public string[] RequestProperties { get; set; }
    }
}
