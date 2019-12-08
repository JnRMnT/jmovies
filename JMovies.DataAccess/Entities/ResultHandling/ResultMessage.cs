using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JMovies.DataAccess.Entities.ResultHandling
{
    /// <summary>
    /// Result Message Associated with the Result Configuration
    /// </summary>
    public class ResultMessage
    {

        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public long ID { get; set; }

        /// <summary>
        /// Foreign Key to the Result Configuration
        /// </summary>
        public long ResultConfigurationID { get; set; }

        /// <summary>
        /// Culture of the result message
        /// </summary>
        [Required]
        [MaxLength(8)]
        public string Culture { get; set; }

        /// <summary>
        /// Content of the result message
        /// </summary>
        [Required]
        public string Content { get; set; }
    }
}
