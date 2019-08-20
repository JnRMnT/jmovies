using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JMovies.DataAccess.Entities.Common
{
    /// <summary>
    /// Entity definition for basic online image
    /// </summary>
    public class jm_Image
    {
        /// <summary>
        /// Title of the image
        /// </summary>
        [MaxLength(255)]
        public string Title { get; set; }
        /// <summary>
        /// URL of the image
        /// </summary>
        [MaxLength(255)]
        public string URL { get; set; }
    }
}
