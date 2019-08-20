using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JMovies.DataAccess.Entities.Movies
{
    /// <summary>
    /// Entity Definition for jm_TVSeries
    /// </summary>
    [Table("TVSeries")]
    public class jm_TVSeries: jm_Movie
    {
        /// <summary>
        /// End Year of the series
        /// </summary>
        [MaxLength(4)]
        public int? EndYear { get; set; }
    }
}
