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
    }
}
