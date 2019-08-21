using JMovies.DataAccess.Entities.Common;
using JMovies.DataAccess.Entities.Movies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.DataAccess.Entities.People
{
    /// <summary>
    /// Entity definition of a person
    /// </summary>
    public class jm_Person
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public long ID { get; set; }

        /// <summary>
        /// Type of the underlining person
        /// </summary>
        [Required]
        [MaxLength(4)]
        public jm_PersonType PersonType { get; set; }
        /// <summary>
        /// IMDb ID of the person
        /// </summary>
        public long IMDbID { get; set; }

        [Required]
        [MaxLength(128)]
        /// <summary>
        /// Full Name of the person
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Main image of the person
        /// </summary>
        [MaxLength(255)]
        public string PrimaryImage { get; set; }

        /// <summary>
        /// Roles of the person
        /// </summary>
        [MaxLength(128)]
        public ICollection<jm_CreditRoleType> Roles { get; set; }

        /// <summary>
        /// Birth Date of the person
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Birth Place of the person
        /// </summary>
        [MaxLength(128)]
        public string BirthPlace { get; set; }

        /// <summary>
        /// Birth Place of the person
        /// </summary>
        [MaxLength(128)]
        public string BirthName { get; set; }

        /// <summary>
        /// Height of the person
        /// </summary>
        [MaxLength(4)]
        public jm_Length Height { get; set; }

        /// <summary>
        /// NickName of the person
        /// </summary>
        [MaxLength(128)]
        public string NickName { get; set; }

        /// <summary>
        /// Mini Biography of the person
        /// </summary>
        public string MiniBiography { get; set; }

        /// <summary>
        /// Available Photos of the person
        /// </summary>
        public jm_Image[] Photos { get; set; }

        /// <summary>
        /// Known credits of the person
        /// </summary>
        [ForeignKey("PersonID")]
        public ICollection<jm_ProductionCredit> KnownFor { get; set; }

        /// <summary>
        /// Gender of the person
        /// </summary>
        [MaxLength(2)]
        public jm_Gender Gender { get; set; }
    }
}
