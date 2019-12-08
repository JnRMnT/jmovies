using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JMovies.DataAccess.Entities
{
    /// <summary>
    /// Text Resources Class Definition
    /// </summary>
    public class Resource
    {

        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public long ID { get; set; }


        [Required]
        [MaxLength(128)]
        public string Key { get; set; }

        /// <summary>
        /// Translations associated with the resource
        /// </summary>
        [ForeignKey("ResourceID")]
        public virtual ICollection<ResourceTranslation> Translations { get; set; }

    }
}
