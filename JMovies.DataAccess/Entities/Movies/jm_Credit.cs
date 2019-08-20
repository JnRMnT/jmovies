﻿
using JMovies.DataAccess.Entities.People;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JMovies.DataAccess.Entities.Movies
{
    /// <summary>
    /// Entity definition of a credit in a title
    /// </summary>
    [Table("Credit")]
    public class jm_Credit
    {

        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public long ID { get; set; }

        /// <summary>
        /// Person related to the credit
        /// </summary>
        public jm_Person Person { get; set; }

        /// <summary>
        /// Role of the person in the title
        /// </summary>
        [MaxLength(2)]
        public jm_CreditRoleType RoleType { get; set; }
    }
}
