using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.DataAccess.Entities.Common
{
    /// <summary>
    /// Entity definition of Length that contains both Imperial and Metric length information
    /// </summary>
    public class jm_Length
    {
        /// <summary>
        /// Default empty constructor
        /// </summary>
        public jm_Length()
        {

        }

        /// <summary>
        /// Metric based Length constructor
        /// </summary>
        /// <param name="metricLength">Metric length</param>
        public jm_Length(int metricLength)
        {
            this.Metric = metricLength;
        }

        /// <summary>
        /// Metric Length
        /// </summary>
        public int Metric { get; set; }

        /// <summary>
        /// Imperial Length
        /// </summary>
        public double Imperial
        {
            get
            {
                return (double)Metric / 2.54;
            }
        }
    }
}
