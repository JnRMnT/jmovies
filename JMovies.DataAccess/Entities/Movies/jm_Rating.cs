using JMovies.DataAccess.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JMovies.DataAccess.Entities.Movies
{
    /// <summary>
    /// Entity definition of Ratings
    /// </summary>
    [Table("Rating")]
    public class jm_Rating
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public long ID { get; set; }

        /// <summary>
        /// Value of the rating
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Count of rates
        /// </summary>
        public long RateCount { get; set; }

        public jm_DataSource DataSource { get; set; }
    }
}
