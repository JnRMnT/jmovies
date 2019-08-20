using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JMovies.DataAccess.Entities.Common
{
    /// <summary>
    /// Entity Definition of a data source
    /// </summary>
    public class jm_DataSource
    {
        public jm_DataSource()
        {

        }

        public jm_DataSource(jm_DataSourceType dataSourceType)
        {
            this.DataSourceType = dataSourceType;
        }

        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int ID
        {
            get
            {
                return (int)DataSourceType;
            }
            set
            {
                DataSourceType = (jm_DataSourceType)value;
            }
        }

        /// <summary>
        /// Name of the source
        /// </summary>
        [MaxLength(32)]
        public string Name
        {
            get
            {
                return DataSourceType.ToString();
            }
            set { }
        }

        /// <summary>
        /// Type of the data source
        /// </summary>
        [NotMapped]
        public jm_DataSourceType DataSourceType { get; set; }
    }
}
