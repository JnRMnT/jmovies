using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JMovies.DataAccess.Entities.Movies
{
    /// <summary>
    /// Entity definition of a production
    /// </summary>
    public class jm_Production
    {
        /// <summary>
        /// Primary key of a production
        /// </summary>
        [Key]
        public long ID { get; set; }
        /// <summary>
        /// IMDb ID of the production
        /// </summary>
        public long IMDbID { get; set; }

        /// <summary>
        /// Title of the production
        /// </summary>
        [MaxLength(128)]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Production Year
        /// </summary>
        [Required]
        [MaxLength(4)]
        public int Year { get; set; }

        /// <summary>
        /// Current Rating of the production
        /// </summary>
        [Required]
        [ForeignKey("ProductionID")]
        public jm_Rating Rating { get; set; }

        /// <summary>
        /// Type of the Production
        /// </summary>
        [Required]
        [MaxLength(4)]
        public jm_ProductionTypeEnum ProductionType { get; set; }        
    }
}
