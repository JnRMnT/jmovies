﻿using JMovies.DataAccess.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Jobs.Common.Configuration
{
    /// <summary>
    /// Holds base configuration of a cron job
    /// </summary>
    public class BaseJobConfiguration
    {
        /// <summary>
        /// Max record count to be processed
        /// </summary>
        public int MaxRecordCount { get; set; }

        /// <summary>
        /// ID of the start record
        /// </summary>
        public long StartRecordID { get; set; }


        /// <summary>
        /// Working type of the job
        /// </summary>
        public PersisterWorkingTypeEnum WorkingType { get; set; }

        /// <summary>
        /// Contains App Insights related configurations
        /// </summary>
        public ApplicationInsightsConfiguration ApplicationInsights;
    }

    public class ApplicationInsightsConfiguration
    {
        /// <summary>
        /// Instrument Key of the App Insights Resource
        /// </summary>
        public string InstrumentationKey { get; set; }
    }
}
