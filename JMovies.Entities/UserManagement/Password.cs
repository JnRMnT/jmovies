using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JMovies.Entities.UserManagement
{
    [Table("Password")]
    public class Password
    {
        [Key]
        public long ID { get; set; }

        [Required]
        [MaxLength(128)]
        public string Salt { get; set; }

        [Required]
        [MaxLength(256)]
        public string Hash { get; set; }

        [Required]
        public HashTypeEnum HashType { get; set; }

        [Required]
        [DefaultValue(0)]
        public int RetryCount { get; set; }

        [Required]
        public DateTime ModifyDate { get; set; }
    }
}
