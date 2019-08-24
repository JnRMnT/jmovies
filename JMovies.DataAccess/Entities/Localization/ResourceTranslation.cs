using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JMovies.DataAccess.Entities
{
    /// <summary>
    /// Translation of the resources
    /// </summary>
    public class ResourceTranslation
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public long ID { get; set; }

        /// <summary>
        /// Foreign Key to Resource
        /// </summary>
        public long ResourceID { get; set; }

        [Required]
        [MaxLength(8)]
        public string Culture { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
