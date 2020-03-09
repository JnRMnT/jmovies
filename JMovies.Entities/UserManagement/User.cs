using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System;

namespace JMovies.Entities.UserManagement
{
    [Table("User")]
    public class User
    {
        [Key]
        [Required]
        public long ID { get; set; }

        [Required]
        [MaxLength(55)]
        public string UserName { get; set; }

        [ForeignKey("PasswordID")]
        [JsonIgnore]
        public virtual Password Password { get; set; }

        [Required]
        [MaxLength(128)]
        public string Email { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }
    }
}
