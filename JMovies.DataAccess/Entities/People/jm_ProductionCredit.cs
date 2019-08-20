using JMovies.DataAccess.Entities.Movies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JMovies.DataAccess.Entities.People
{
    /// <summary>
    /// Credit of a person in a specific production
    /// </summary>
    [Table("ProductionCredit")]
    public class jm_ProductionCredit
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public long ID { get; set; }

        /// <summary>
        /// Credit
        /// </summary>
        public jm_Credit Credit { get; set; }

        /// <summary>
        /// The production that person is credited in
        /// </summary>
        public jm_Production Production { get; set; }
    }
}
