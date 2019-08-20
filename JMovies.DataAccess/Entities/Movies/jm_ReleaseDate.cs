using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.DataAccess.Entities.Movies
{
    /// <summary>
    /// Entity definition of release dates
    /// </summary>
    [Table("ReleaseDate")]
    public class jm_ReleaseDate
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public long ID { get; set; }

        /// <summary>
        /// Date of the release
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Country of release date
        /// </summary>
        public jm_Country Country { get; set; }
    }
}
