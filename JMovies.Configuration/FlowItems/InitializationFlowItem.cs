﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMovies.Entities;

namespace JMovies.Configuration.FlowItems
{
    public class InitializationFlowItem : BaseFlowItem
    {
        protected override void OnExecuteFlow(ref BaseRequest request, ref BaseResponse response)
        {
            if (response == null)
            {
                response = new BaseResponse();
            }
        }
    }
}
