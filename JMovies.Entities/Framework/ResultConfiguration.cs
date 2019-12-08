using JM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Entities.Framework
{
    public class ResultConfiguration
    {
        public string ErrorCode { get; set; }
        public RedirectionTypeEnum RedirectionType { get; set; }
        public string RedirectionParameter { get; set; }
        public ICollection<ResultMessage> ResultMessages { get; set; }
    }
}
