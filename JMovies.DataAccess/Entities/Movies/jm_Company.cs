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
    /// Entity definition of a company related to the industry
    /// </summary>
    [Table("Company")]
    public class jm_Company
    {
        /// <summary>
        /// ID of the company
        /// </summary>
        [Key]
        public long ID { get; set; }

        /// <summary>
        /// Name of the company
        /// </summary>
        [MaxLength(128)]
        public string Name { get; set; }
    }
}
