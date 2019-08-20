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
    /// Entity definition of a character in a title
    /// </summary>
    public class jm_Character
    {
        /// <summary>
        /// Primary key of a character
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public long ID { get; set; }
        /// <summary>
        /// IMDb ID
        /// </summary>
        [Column(Order = 1)]
        public long? IMDbID { get; set; }

        /// <summary>
        /// Underlining Character Type
        /// </summary>
        [Required]
        [Column(Order = 2)]
        [MaxLength(2)]
        public jm_CharacterType CharacterType { get; set; }

        /// <summary>
        /// Name of the character
        /// </summary>
        [Required]
        [MaxLength(128)]
        [Column(Order = 3)]
        public string Name { get; set; }
    }
}
