using JMovies.DataAccess.Entities.Enums;
using JMovies.IMDb.Entities.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JMovies.DataAccess.Entities.Persisters
{
    public class PersisterHistory
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public long ID { get; set; }

        /// <summary>
        /// Source of the persistence
        /// </summary>
        [Required]
        [ForeignKey("DataSourceID")]
        public virtual DataSource DataSource { get; set; }

        /// <summary>
        /// Reference id of the data source
        /// </summary>
        public int DataSourceID;

        /// <summary>
        /// Type of the entity that was persisted
        /// </summary>
        [NotMapped]
        public EntityTypeEnum EntityType { get; set; }

        /// <summary>
        /// Short wrapper for EntityTypeEnum
        /// </summary>
        [Required]
        [MaxLength(4)]
        public int EntityTypeID
        {
            get
            {
                return (short)EntityType;
            }
            set
            {
                EntityType = (EntityTypeEnum)value;
            }
        }

        /// <summary>
        /// ID of the data that was persisted
        /// </summary>
        public long DataID { get; set; }

        /// <summary>
        /// Data of the execution
        /// </summary>
        [Required]
        public DateTime ExecuteDate { get; set; }

        /// <summary>
        /// If the run was successful
        /// </summary>
        [Required]
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Error message, if occured
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
