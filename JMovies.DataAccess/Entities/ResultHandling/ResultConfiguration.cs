using JM.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JMovies.DataAccess.Entities.ResultHandling
{
    /// <summary>
    /// Result Configuration to handle exceptions
    /// </summary>
    public class ResultConfiguration
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public long ID { get; set; }

        /// <summary>
        /// Code of the error to be mapped
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string ErrorCode { get; set; }

        /// <summary>
        /// Redirection Type when this error occurs
        /// </summary>
        [Required]
        public RedirectionTypeEnum RedirectionType { get; set; }


        /// <summary>
        /// Redirection Parameter for the redirection type
        /// </summary>
        [MaxLength(512)]
        public string RedirectionParameter { get; set; }

        /// <summary>
        /// Messages associated with the result configuration
        /// </summary>
        [ForeignKey("ResultConfigurationID")]
        public virtual ICollection<ResultMessage> ResultMessages { get; set; }
    }
}
